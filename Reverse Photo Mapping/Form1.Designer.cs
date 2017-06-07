namespace Reverse_Geotag
{
    partial class Form1
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
            this.geotagPhotosButton = new System.Windows.Forms.Button();
            this.filesListBox = new System.Windows.Forms.ListBox();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.selectPhotosButton = new System.Windows.Forms.Button();
            this.pictureBoxDisplay = new System.Windows.Forms.PictureBox();
            this.selectGPXFileButton = new System.Windows.Forms.Button();
            this.gpxFileLabelHeader = new System.Windows.Forms.Label();
            this.gpxFileLabelName = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.openFileDialog2 = new System.Windows.Forms.OpenFileDialog();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.helpButton = new System.Windows.Forms.Button();
            this.imageLabel = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxDisplay)).BeginInit();
            this.SuspendLayout();
            // 
            // geotagPhotosButton
            // 
            this.geotagPhotosButton.Location = new System.Drawing.Point(564, 674);
            this.geotagPhotosButton.Name = "geotagPhotosButton";
            this.geotagPhotosButton.Size = new System.Drawing.Size(284, 73);
            this.geotagPhotosButton.TabIndex = 0;
            this.geotagPhotosButton.Text = "Geotag Selected Photos";
            this.geotagPhotosButton.UseVisualStyleBackColor = true;
            this.geotagPhotosButton.Click += new System.EventHandler(this.geotagPhotosButton_Click);
            // 
            // filesListBox
            // 
            this.filesListBox.FormattingEnabled = true;
            this.filesListBox.ItemHeight = 25;
            this.filesListBox.Items.AddRange(new object[] {
            " "});
            this.filesListBox.Location = new System.Drawing.Point(48, 50);
            this.filesListBox.Name = "filesListBox";
            this.filesListBox.Size = new System.Drawing.Size(302, 429);
            this.filesListBox.TabIndex = 1;
            this.filesListBox.SelectedIndexChanged += new System.EventHandler(this.PictureSelectedChanged);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            this.openFileDialog1.Multiselect = true;
            this.openFileDialog1.FileOk += new System.ComponentModel.CancelEventHandler(this.openFileDialog1_FileOk);
            // 
            // saveFileDialog1
            // 
            this.saveFileDialog1.FileName = "Photo";
            this.saveFileDialog1.Filter = "JPG files (*.jpg)|*.jpg|All files (*.*)|*.*";
            this.saveFileDialog1.Title = "Save Geotagged Photos";
            // 
            // selectPhotosButton
            // 
            this.selectPhotosButton.Location = new System.Drawing.Point(76, 521);
            this.selectPhotosButton.Name = "selectPhotosButton";
            this.selectPhotosButton.Size = new System.Drawing.Size(247, 73);
            this.selectPhotosButton.TabIndex = 3;
            this.selectPhotosButton.Text = "Select Photos";
            this.selectPhotosButton.UseVisualStyleBackColor = true;
            this.selectPhotosButton.Click += new System.EventHandler(this.selectPhotosButton_Click);
            // 
            // pictureBoxDisplay
            // 
            this.pictureBoxDisplay.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pictureBoxDisplay.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureBoxDisplay.Location = new System.Drawing.Point(400, 50);
            this.pictureBoxDisplay.Name = "pictureBoxDisplay";
            this.pictureBoxDisplay.Size = new System.Drawing.Size(639, 430);
            this.pictureBoxDisplay.TabIndex = 4;
            this.pictureBoxDisplay.TabStop = false;
            this.pictureBoxDisplay.Click += new System.EventHandler(this.pictureBox1_Click);
            // 
            // selectGPXFileButton
            // 
            this.selectGPXFileButton.Location = new System.Drawing.Point(76, 628);
            this.selectGPXFileButton.Name = "selectGPXFileButton";
            this.selectGPXFileButton.Size = new System.Drawing.Size(247, 73);
            this.selectGPXFileButton.TabIndex = 5;
            this.selectGPXFileButton.Text = "Select .GPX File";
            this.selectGPXFileButton.UseVisualStyleBackColor = true;
            this.selectGPXFileButton.Click += new System.EventHandler(this.selectGPXFileButton_Click);
            // 
            // gpxFileLabelHeader
            // 
            this.gpxFileLabelHeader.AutoSize = true;
            this.gpxFileLabelHeader.Location = new System.Drawing.Point(76, 722);
            this.gpxFileLabelHeader.Name = "gpxFileLabelHeader";
            this.gpxFileLabelHeader.Size = new System.Drawing.Size(103, 25);
            this.gpxFileLabelHeader.TabIndex = 6;
            this.gpxFileLabelHeader.Text = "GPX File:\r\n";
            // 
            // gpxFileLabelName
            // 
            this.gpxFileLabelName.AutoSize = true;
            this.gpxFileLabelName.Location = new System.Drawing.Point(76, 754);
            this.gpxFileLabelName.Name = "gpxFileLabelName";
            this.gpxFileLabelName.Size = new System.Drawing.Size(103, 25);
            this.gpxFileLabelName.TabIndex = 7;
            this.gpxFileLabelName.Text = "FileName";
            this.gpxFileLabelName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(48, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(85, 25);
            this.label1.TabIndex = 8;
            this.label1.Text = "Photos:";
            // 
            // openFileDialog2
            // 
            this.openFileDialog2.FileName = "openFileDialog2";
            // 
            // folderBrowserDialog1
            // 
            this.folderBrowserDialog1.Description = "Select Folder to Save Photos";
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(483, 606);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(458, 40);
            this.progressBar1.TabIndex = 9;
            // 
            // helpButton
            // 
            this.helpButton.Location = new System.Drawing.Point(960, 758);
            this.helpButton.Name = "helpButton";
            this.helpButton.Size = new System.Drawing.Size(120, 38);
            this.helpButton.TabIndex = 10;
            this.helpButton.Text = "Help";
            this.helpButton.UseVisualStyleBackColor = true;
            this.helpButton.Click += new System.EventHandler(this.helpButton_Click);
            // 
            // imageLabel
            // 
            this.imageLabel.Location = new System.Drawing.Point(551, 496);
            this.imageLabel.Name = "imageLabel";
            this.imageLabel.Size = new System.Drawing.Size(336, 28);
            this.imageLabel.TabIndex = 11;
            this.imageLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1092, 808);
            this.Controls.Add(this.imageLabel);
            this.Controls.Add(this.helpButton);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.gpxFileLabelName);
            this.Controls.Add(this.gpxFileLabelHeader);
            this.Controls.Add(this.selectGPXFileButton);
            this.Controls.Add(this.pictureBoxDisplay);
            this.Controls.Add(this.selectPhotosButton);
            this.Controls.Add(this.filesListBox);
            this.Controls.Add(this.geotagPhotosButton);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.Text = "Reverse Geotag";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxDisplay)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button geotagPhotosButton;
        private System.Windows.Forms.ListBox filesListBox;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.Button selectPhotosButton;
        private System.Windows.Forms.PictureBox pictureBoxDisplay;
        private System.Windows.Forms.Button selectGPXFileButton;
        private System.Windows.Forms.Label gpxFileLabelHeader;
        private System.Windows.Forms.Label gpxFileLabelName;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.OpenFileDialog openFileDialog2;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.Button helpButton;
        private System.Windows.Forms.Label imageLabel;
    }
}

