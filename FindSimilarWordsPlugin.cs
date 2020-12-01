using System.Threading;
using System.Threading.Tasks;
using System.Text;
using System.Linq;
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Drawing;
using PluginContracts;
using OutputHelperLib;
using System.IO;
using TSOutputWriter;


namespace FindSimilarWordsPlugin
{
    public class FindSimilarWordsPlugin : LinearPlugin
    {


        public string[] InputType { get; } = { "Pre-trained Model File" };
        public string OutputType { get; } = "Cosine Similarity Table";

        public Dictionary<int, string> OutputHeaderData { get; set; } = new Dictionary<int, string>() { { 0, "TokenizedText" } };
        public bool InheritHeader { get; } = false;

        #region Plugin Details and Info

        public string PluginName { get; } = "Find Similar Words";
        public string PluginType { get; } = "Word Embeddings";
        public string PluginVersion { get; } = "1.0.1";
        public string PluginAuthor { get; } = "Ryan L. Boyd (ryan@ryanboyd.io)";
        public string PluginDescription { get; } = "Using a pre-trained word embedding model (e.g., Word2Vec, GloVe), this plugin takes user-supplied word lists and will return similar words. For example, if you provide the word group \"fun, exciting, wonderful\", this plugin will calculate the average vector for these 3 words. Then, this plugin will scan your pre-trained model for similar words using the cosine similarity metric, returning words that are similar to the average vector of your \"fun, exciting, wonderful\" word group. Note that your seed word list is case sensitive, and this method find word similarities for seed words that do not exist in your pre-trained model.";
        public bool TopLevel { get; } = true;
        public string PluginTutorial { get; } = "Coming Soon";

        public string StatusToReport { get; set; } = "";
        private ulong TotalNumRows { get; set; }

        public Icon GetPluginIcon
        {
            get
            {
                return Properties.Resources.icon;
            }
        }

        #endregion



        private string IncomingTextLocation { get; set; } = "";
        private string OutputLocation { get; set; } = "";
        private string SelectedEncoding { get; set; } = "utf-8";
        private int VocabSize { get; set; } = 0;
        private int VectorSize { get; set; } = 0;
        private string[] WordList { get; set; } = { "happy, fun, excited", "anxious, nervous, afraid", "cool, neat, groovy" };
        private double CosineCutoff { get; set; } = 0.50;
        private Dictionary<string, List<int>> ListOfAllWords { get; set; }
        private Dictionary<int, int> NumberOfWordsInGroup { get; set; }
        private Dictionary<int, double[]> WordGroupVectors { get; set; }
        private string Quotes = "\"";
        private string Delimiter = ",";

        public void ChangeSettings()
        {

            using (var form = new SettingsForm_FindSimilarWordsPlugin(IncomingTextLocation, OutputLocation, SelectedEncoding, VectorSize, VocabSize, WordList, CosineCutoff))
            {


                form.Icon = Properties.Resources.icon;
                form.Text = PluginName;


                var result = form.ShowDialog();
                if (result == DialogResult.OK)
                {
                    SelectedEncoding = form.SelectedEncoding;
                    IncomingTextLocation = form.InputFileName;
                    OutputLocation = form.OutputFileName;
                    VocabSize = form.VocabSize;
                    VectorSize = form.VectorSize;
                    WordList = form.WordList;
                    CosineCutoff = form.CosineCutoff;
                }
            }

        }




        //not used
        public Payload RunPlugin(Payload Input)
        {
            return new Payload();
        }



        public Payload RunPlugin(Payload Input, int ThreadsAvailable)
        {


            using (ThreadsafeOutputWriter OutputWriter = new ThreadsafeOutputWriter(OutputLocation, Encoding.GetEncoding(SelectedEncoding.ToString()), FileMode.Create))
            {


                //write the header here
                string HeaderRow = Quotes + "Word" + Quotes;
                for (int i = 0; i < OutputHeaderData.Keys.Count; i++) HeaderRow += Delimiter + Quotes + OutputHeaderData[i].Replace(Quotes, Quotes + Quotes) + Quotes;
                OutputWriter.WriteString(HeaderRow);

                //read the first row of input file
                var lines = File.ReadLines(IncomingTextLocation, Encoding.GetEncoding(SelectedEncoding));
                if (VocabSize != -1) lines = lines.Skip(1);

                int LineNumber = 0;



                TimeSpan reportPeriod = TimeSpan.FromMinutes(0.01);
                using (new System.Threading.Timer(
                            _ => SetUpdate(LineNumber),
                                 null, reportPeriod, reportPeriod))
                {
                    Parallel.ForEach((IEnumerable<object>)lines,
                    new ParallelOptions { MaxDegreeOfParallelism = ThreadsAvailable }, (line, state) =>
                    {


                        


                        string linetosplit = ((string)line).TrimEnd();
                        string[] splitLine = (linetosplit).Split(new[] { ' ' });
                        //string[] splitLine = (linetosplit).Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                        string RowWord = splitLine[0].Trim();


                        double[] RowVector = new double[VectorSize];
                        for (int i = 0; i < VectorSize; i++) RowVector[i] = Double.Parse(splitLine[i + 1]);


                        //let's calculate the cosine similarity between our mean vector
                        //and the token on the current row

                        //calculate cosine Similarities
                        double[] cosSims = new double[WordGroupVectors.Keys.Count];
                        bool WriteRow = false;

                        for (int wordlist_counter = 0; wordlist_counter < WordGroupVectors.Keys.Count; wordlist_counter++)
                        {

                            //https://janav.wordpress.com/2013/10/27/tf-idf-and-cosine-similarity/
                            //Cosine Similarity (d1, d2) =  Dot product(d1, d2) / ||d1|| * ||d2||
                            //
                            //Dot product (d1,d2) = d1[0] * d2[0] + d1[1] * d2[1] * … * d1[n] * d2[n]
                            //||d1|| = square root(d1[0]2 + d1[1]2 + ... + d1[n]2)
                            //||d2|| = square root(d2[0]2 + d2[1]2 + ... + d2[n]2)
                            double dotproduct = 0;
                            double d1 = 0;
                            double d2 = 0;

                            //calculate cosine similarity components
                            for (int i = 0; i < VectorSize; i++)
                            {
                                dotproduct += WordGroupVectors[wordlist_counter][i] * RowVector[i];
                                d1 += WordGroupVectors[wordlist_counter][i] * WordGroupVectors[wordlist_counter][i];
                                d2 += RowVector[i] * RowVector[i];
                            }

                            cosSims[wordlist_counter] = dotproduct / (Math.Sqrt(d1) * Math.Sqrt(d2));

                            if (Math.Abs(cosSims[wordlist_counter]) >= CosineCutoff) WriteRow = true;


                        }


                        if (WriteRow)
                        {
                            StringBuilder outputRow = new StringBuilder();
                            outputRow.Append(Quotes + RowWord.Replace(Quotes, Quotes + Quotes) + Quotes);
                            for (int i = 0; i < cosSims.Length; i++) outputRow.Append(Delimiter + cosSims[i].ToString());
                            OutputWriter.WriteString(outputRow.ToString());
                        }





                        Interlocked.Increment(ref LineNumber);





                    });
                }




                //end outputwriter
            }

            return new Payload();

        }





        public void Initialize()
        {

            StatusToReport = "Initializing...";
            OutputHeaderData = new Dictionary<int, string>();
            ListOfAllWords = new Dictionary<string, List<int>>();
            NumberOfWordsInGroup = new Dictionary<int, int>();
            WordGroupVectors = new Dictionary<int, double[]>();
            TotalNumRows = 0;

            string[] WordGroups = WordList.Where(x => !string.IsNullOrEmpty(x)).ToArray();

            for (int i = 0; i < WordGroups.Length; i++)
            {
                
                //set up the header with the info here
                OutputHeaderData.Add(i, WordGroups[i].Trim());

                //split into individual words
                string[] IndividualWords = WordGroups[i].Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);

                //for each word, add it to the appropriate dictionary...
                //and make sure that it is mapped to all word groups to which it belongs
                foreach (string Word in IndividualWords)
                {
                    string WordClean = Word.Trim();
                    if (!ListOfAllWords.ContainsKey(WordClean))
                    {
                        ListOfAllWords.Add(WordClean, new List<int> { i });
                    }
                    else
                    {
                        ListOfAllWords[WordClean].Add(i);
                    }
                    
                }

                //track the number of found words in the group
                NumberOfWordsInGroup.Add(i, 0);

                //lastly, initialize the vector for the word group
                WordGroupVectors.Add(i, new double[VectorSize]);
                for (int j = 0; j < VectorSize; j++) WordGroupVectors[i][j] = 0;

            }







            //now, during initialization, we actually go through and want to establish the word group vectors
            using (var stream = File.OpenRead(IncomingTextLocation))
            using (var reader = new StreamReader(stream, encoding: Encoding.GetEncoding(SelectedEncoding)))
            {

                if (VocabSize != -1)
                {
                    string[] firstLine = reader.ReadLine().Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                }

                int WordsFound = 0;
                int NumWords = ListOfAllWords.Keys.Count;

                while (!reader.EndOfStream)
                {

                    TotalNumRows++;
                    //if (TotalNumRows % 1000 == 0) StatusToReport = "Initializing... Model Row #" + TotalNumRows.ToString();

                    string line = reader.ReadLine().TrimEnd();
                    string[] splitLine = line.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                    string RowWord = splitLine[0].Trim();

                    //if the word is one that we want to capture, then we pull out the info that we want
                    if (ListOfAllWords.ContainsKey(RowWord))
                    {
                        WordsFound++;
                        double[] RowVector = new double[VectorSize];
                        for (int i = 0; i < VectorSize; i++) RowVector[i] = Double.Parse(splitLine[i + 1]);
                        for (int i = 0; i < ListOfAllWords[RowWord].Count; i++) NumberOfWordsInGroup[ListOfAllWords[RowWord][i]] += 1;
                        for (int i = 0; i < ListOfAllWords[RowWord].Count; i++)
                        {
                            for (int j = 0; j < VectorSize; j++)
                            {
                                WordGroupVectors[ListOfAllWords[RowWord][i]][j] += RowVector[j];
                            }
                        }
                        
                    }

                }
            }


            //last, we convert the word group vectors from sums into averages
            for (int i = 0; i < WordGroups.Length; i++)
            {
                for (int j = 0; j < VectorSize; j++)
                {
                    WordGroupVectors[i][j] = WordGroupVectors[i][j] / NumberOfWordsInGroup[i];
                }
            }


        }



        



        public bool InspectSettings()
        {

            if (string.IsNullOrEmpty(IncomingTextLocation) || string.IsNullOrEmpty(OutputLocation))
            {
                return false;
            }
            else
            {
                return true;
            }
            
        }

        public Payload FinishUp(Payload Input)
        {
            return (Input);
        }



        private void SetUpdate(int RowNum)
        {
                    StatusToReport = "Calculating Cosine Similarities: " + ((RowNum / (double)TotalNumRows) * 100).ToString("F2") + "%";
        }





        #region Import/Export Settings
        public void ImportSettings(Dictionary<string, string> SettingsDict)
        {
            SelectedEncoding = SettingsDict["SelectedEncoding"];
            IncomingTextLocation = SettingsDict["IncomingTextLocation"];
            OutputLocation = SettingsDict["OutputLocation"];
            VocabSize = int.Parse(SettingsDict["VocabSize"]);
            VectorSize = int.Parse(SettingsDict["VectorSize"]);
            CosineCutoff = double.Parse(SettingsDict["CosineCutoff"]);
            int WordListLength = int.Parse(SettingsDict["WordListLength"]);

            WordList = new string[WordListLength];

            for (int i =0; i < WordListLength; i++)
            {
                WordList[i] = SettingsDict["WordList" + i.ToString()];
            }

        }



        public Dictionary<string, string> ExportSettings(bool suppressWarnings)
        {
            Dictionary<string, string> SettingsDict = new Dictionary<string, string>();
            SettingsDict.Add("SelectedEncoding", SelectedEncoding);
            SettingsDict.Add("IncomingTextLocation", IncomingTextLocation);
            SettingsDict.Add("OutputLocation", OutputLocation);
            SettingsDict.Add("VocabSize", VocabSize.ToString());
            SettingsDict.Add("VectorSize", VectorSize.ToString());
            SettingsDict.Add("CosineCutoff", CosineCutoff.ToString());

            int WordListLength = 0;
            if (WordList != null) WordListLength = WordList.Length;

            SettingsDict.Add("WordListLength", WordListLength.ToString());

            for (int i = 0; i < WordListLength; i++)
            {
                SettingsDict.Add("WordList" + i.ToString(), WordList[i]);
            }

            return (SettingsDict);
        }
        #endregion




    }
}
