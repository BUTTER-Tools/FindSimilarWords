namespace FindSimilarWordsPlugin
{
    partial class SettingsForm_FindSimilarWordsPlugin
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SettingsForm_FindSimilarWordsPlugin));
            this.SetFolderButton = new System.Windows.Forms.Button();
            this.SelectedFileTextbox = new System.Windows.Forms.TextBox();
            this.EncodingDropdown = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.OKButton = new System.Windows.Forms.Button();
            this.ModelDetailsTextbox = new System.Windows.Forms.TextBox();
            this.WordListTextbox = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.CosineThresholdBox = new System.Windows.Forms.NumericUpDown();
            this.label6 = new System.Windows.Forms.Label();
            this.OutputFileTextbox = new System.Windows.Forms.TextBox();
            this.ChooseOutputFileButton = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.CosineThresholdBox)).BeginInit();
            this.SuspendLayout();
            // 
            // SetFolderButton
            // 
            this.SetFolderButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SetFolderButton.Location = new System.Drawing.Point(448, 173);
            this.SetFolderButton.Name = "SetFolderButton";
            this.SetFolderButton.Size = new System.Drawing.Size(118, 40);
            this.SetFolderButton.TabIndex = 0;
            this.SetFolderButton.Text = "Choose File";
            this.SetFolderButton.UseVisualStyleBackColor = true;
            this.SetFolderButton.Click += new System.EventHandler(this.SetFolderButton_Click);
            // 
            // SelectedFileTextbox
            // 
            this.SelectedFileTextbox.Enabled = false;
            this.SelectedFileTextbox.Font = new System.Drawing.Font("Consolas", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SelectedFileTextbox.Location = new System.Drawing.Point(448, 144);
            this.SelectedFileTextbox.MaxLength = 2147483647;
            this.SelectedFileTextbox.Name = "SelectedFileTextbox";
            this.SelectedFileTextbox.Size = new System.Drawing.Size(516, 23);
            this.SelectedFileTextbox.TabIndex = 1;
            // 
            // EncodingDropdown
            // 
            this.EncodingDropdown.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.EncodingDropdown.Font = new System.Drawing.Font("Consolas", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.EncodingDropdown.FormattingEnabled = true;
            this.EncodingDropdown.Location = new System.Drawing.Point(450, 48);
            this.EncodingDropdown.Name = "EncodingDropdown";
            this.EncodingDropdown.Size = new System.Drawing.Size(268, 23);
            this.EncodingDropdown.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("MS Reference Sans Serif", 9.75F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(448, 125);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(120, 16);
            this.label1.TabIndex = 3;
            this.label1.Text = "Select Model File";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("MS Reference Sans Serif", 9.75F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(450, 29);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(184, 16);
            this.label2.TabIndex = 4;
            this.label2.Text = "Select Model File Encoding";
            // 
            // OKButton
            // 
            this.OKButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.OKButton.Location = new System.Drawing.Point(654, 419);
            this.OKButton.Name = "OKButton";
            this.OKButton.Size = new System.Drawing.Size(118, 40);
            this.OKButton.TabIndex = 6;
            this.OKButton.Text = "OK";
            this.OKButton.UseVisualStyleBackColor = true;
            this.OKButton.Click += new System.EventHandler(this.OKButton_Click);
            // 
            // ModelDetailsTextbox
            // 
            this.ModelDetailsTextbox.Enabled = false;
            this.ModelDetailsTextbox.Font = new System.Drawing.Font("Consolas", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ModelDetailsTextbox.Location = new System.Drawing.Point(587, 183);
            this.ModelDetailsTextbox.MaxLength = 2147483647;
            this.ModelDetailsTextbox.Name = "ModelDetailsTextbox";
            this.ModelDetailsTextbox.Size = new System.Drawing.Size(377, 23);
            this.ModelDetailsTextbox.TabIndex = 8;
            // 
            // WordListTextbox
            // 
            this.WordListTextbox.AcceptsReturn = true;
            this.WordListTextbox.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.WordListTextbox.Location = new System.Drawing.Point(23, 48);
            this.WordListTextbox.MaxLength = 2147483647;
            this.WordListTextbox.Multiline = true;
            this.WordListTextbox.Name = "WordListTextbox";
            this.WordListTextbox.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.WordListTextbox.Size = new System.Drawing.Size(389, 402);
            this.WordListTextbox.TabIndex = 9;
            this.WordListTextbox.Text = "happy, fun, excited\r\nanxious, nervous, afraid\r\ncool, neat, groovy";
            this.WordListTextbox.WordWrap = false;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("MS Reference Sans Serif", 9.75F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(20, 29);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(89, 16);
            this.label4.TabIndex = 10;
            this.label4.Text = "Word List(s)";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("MS Reference Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(592, 344);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(198, 16);
            this.label5.TabIndex = 11;
            this.label5.Text = "Retain |Cosine Similarity| >=";
            // 
            // CosineThresholdBox
            // 
            this.CosineThresholdBox.DecimalPlaces = 3;
            this.CosineThresholdBox.Font = new System.Drawing.Font("Consolas", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CosineThresholdBox.Increment = new decimal(new int[] {
            5,
            0,
            0,
            131072});
            this.CosineThresholdBox.Location = new System.Drawing.Point(796, 343);
            this.CosineThresholdBox.Maximum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.CosineThresholdBox.Name = "CosineThresholdBox";
            this.CosineThresholdBox.Size = new System.Drawing.Size(89, 23);
            this.CosineThresholdBox.TabIndex = 12;
            this.CosineThresholdBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("MS Reference Sans Serif", 9.75F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(450, 284);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(115, 16);
            this.label6.TabIndex = 15;
            this.label6.Text = "Select Save File";
            // 
            // OutputFileTextbox
            // 
            this.OutputFileTextbox.Enabled = false;
            this.OutputFileTextbox.Font = new System.Drawing.Font("Consolas", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.OutputFileTextbox.Location = new System.Drawing.Point(450, 303);
            this.OutputFileTextbox.MaxLength = 2147483647;
            this.OutputFileTextbox.Name = "OutputFileTextbox";
            this.OutputFileTextbox.Size = new System.Drawing.Size(516, 23);
            this.OutputFileTextbox.TabIndex = 14;
            // 
            // ChooseOutputFileButton
            // 
            this.ChooseOutputFileButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ChooseOutputFileButton.Location = new System.Drawing.Point(450, 332);
            this.ChooseOutputFileButton.Name = "ChooseOutputFileButton";
            this.ChooseOutputFileButton.Size = new System.Drawing.Size(118, 40);
            this.ChooseOutputFileButton.TabIndex = 13;
            this.ChooseOutputFileButton.Text = "Choose File";
            this.ChooseOutputFileButton.UseVisualStyleBackColor = true;
            this.ChooseOutputFileButton.Click += new System.EventHandler(this.ChooseOutputFileButton_Click);
            // 
            // SettingsForm_FindSimilarWordsPlugin
            // 
            this.AcceptButton = this.OKButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(990, 471);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.OutputFileTextbox);
            this.Controls.Add(this.ChooseOutputFileButton);
            this.Controls.Add(this.CosineThresholdBox);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.WordListTextbox);
            this.Controls.Add(this.ModelDetailsTextbox);
            this.Controls.Add(this.OKButton);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.EncodingDropdown);
            this.Controls.Add(this.SelectedFileTextbox);
            this.Controls.Add(this.SetFolderButton);
            this.DoubleBuffered = true;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "SettingsForm_FindSimilarWordsPlugin";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Read .txt Files from Folder Settings";
            ((System.ComponentModel.ISupportInitialize)(this.CosineThresholdBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button SetFolderButton;
        private System.Windows.Forms.TextBox SelectedFileTextbox;
        private System.Windows.Forms.ComboBox EncodingDropdown;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button OKButton;
        private System.Windows.Forms.TextBox ModelDetailsTextbox;
        private System.Windows.Forms.TextBox WordListTextbox;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.NumericUpDown CosineThresholdBox;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox OutputFileTextbox;
        private System.Windows.Forms.Button ChooseOutputFileButton;
    }
}