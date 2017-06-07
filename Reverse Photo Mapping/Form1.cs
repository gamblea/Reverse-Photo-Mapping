using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using Reverse_Geotag;
using System.Drawing.Imaging;


namespace Reverse_Geotag
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        PictureBox pictureBoxHorizontal;
        PictureBox pictureBoxVertical;
        GpsFile gpsFile;
        bool photosSelected = false;
        bool gpxFileSelected = false;


        private void InitilizePictureBoxes()
        {
            Rectangle horizontalRect = new Rectangle(210, 40, 300, 200);
            Rectangle verticalRect = new Rectangle(270,30,180,220);

            pictureBoxHorizontal = new PictureBox();
            pictureBoxHorizontal.Location = horizontalRect.Location;
            pictureBoxHorizontal.Size = horizontalRect.Size;
            pictureBoxHorizontal.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBoxHorizontal.BorderStyle = BorderStyle.FixedSingle;
            pictureBoxHorizontal.Hide();
            this.Controls.Add(pictureBoxHorizontal);

            pictureBoxVertical = new PictureBox();
            pictureBoxVertical.Location = verticalRect.Location;
            pictureBoxVertical.Size = verticalRect.Size;
            pictureBoxVertical.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBoxVertical.BorderStyle = BorderStyle.FixedSingle;
            pictureBoxVertical.Hide();
            this.Controls.Add(pictureBoxVertical);
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            InitializeOpenFileDialog();
            InitilizePictureBoxes();
        }

        private void InitializeOpenFileDialog()
        {
            // Set the file dialog to filter for graphics files.
            this.openFileDialog1.Filter =
                "Images (*.JPG;)|*.JPG;|" +
                "All files (*.*)|*.*";

            //  Allow the user to select multiple images.
            this.openFileDialog1.Multiselect = true;
            this.openFileDialog1.Title = "Select Images";

            this.openFileDialog2.Filter = 
                "GPS Files (*.GPX)|*.GPX|" +
                "All Files(*.*)|*.*";

            this.openFileDialog2.Multiselect = false;
            this.openFileDialog2.Title = "Select GPX File";
        }

        private void selectPhotosButton_Click(object sender, EventArgs e)
        {
            filesListBox.Items.Clear();
            DialogResult dialogResult = this.openFileDialog1.ShowDialog();
            
            if (dialogResult == DialogResult.OK)
            {
                
                foreach (String filePath in openFileDialog1.FileNames)
                {

                    Photo photo = new Photo(filePath);
                    filesListBox.Items.Add(photo);
                }
            }
            photosSelected = true;
        }

        private void openFileDialog1_FileOk(object sender, CancelEventArgs e)
        {

        }

        private void PictureSelectedChanged(object sender, EventArgs e)
        {
            pictureBoxDisplay.Hide();
            pictureBoxVertical.Hide();
            pictureBoxHorizontal.Hide();

            int fileIndex = filesListBox.SelectedIndex;

            if (fileIndex >= 0)
            {
                Photo photo = filesListBox.Items[fileIndex] as Photo;

                if (photo.errorThrown == true)
                {
                    imageLabel.Text = "File could not be loaded.";
                    return;
                }

                if (photo.image.Width > photo.image.Height)
                {
                    this.Controls.Add(pictureBoxHorizontal);
                    pictureBoxHorizontal.Image = photo.image;
                    pictureBoxHorizontal.Show();
                }
                else if (photo.image.Width <= photo.image.Height)
                {
                    this.Controls.Add(pictureBoxVertical);
                    pictureBoxVertical.Image = photo.image;
                    pictureBoxVertical.Show();
                }

                imageLabel.Text = "Taken at: " + photo.TimeTaken.ToShortTimeString();
                
            }
        }

        private void selectGPXFileButton_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = this.openFileDialog2.ShowDialog();

            if (dialogResult == DialogResult.OK)
            {
                String gpsFilePath = openFileDialog2.FileName;
                gpxFileLabelName.Text = openFileDialog2.SafeFileName;

                gpsFile = new GpsFile(gpsFilePath);
            }

            gpxFileSelected = true;
        }

        private async void geotagPhotosButton_Click(object sender, EventArgs e)
        {
            progressBar1.Value = 0;
            if (photosSelected == false || gpxFileSelected == false)
            {
                MessageBox.Show("Make sure to select a gpx file and photos.");
                return;
            }

            string directoryToSavePhotos = "";
            if(folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                directoryToSavePhotos = folderBrowserDialog1.SelectedPath; // ->/Desktop
            }


            List<Photo> imagesToGeoTag = new List<Photo>();
            foreach (Photo photo in filesListBox.Items)
	        {
                imagesToGeoTag.Add(photo);
	        }

            Tagger geoTagger = new Tagger(gpsFile);
            List<Photo> geotaggedPhotos = geoTagger.getGeoTagPhotos(imagesToGeoTag);
            List<Photo> finalPhotos = await geoTagger.getPhotosWithDescritpion(geotaggedPhotos);
            imagesToGeoTag = null;
            geotaggedPhotos = finalPhotos;

            
            progressBar1.Maximum = geotaggedPhotos.Count;
            for (int i = 0; i < geotaggedPhotos.Count; i++)
            {
                progressBar1.Value++;
                if (geotaggedPhotos[i].errorThrown == false)
                {
                    geotaggedPhotos[i].image.Save(directoryToSavePhotos + @"/" + geotaggedPhotos[i].fileName);
                }       
            }

            MessageBox.Show("Files have been saved at: " + Environment.NewLine + directoryToSavePhotos);

            gpxFileSelected = false;
        }

        private void helpButton_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Instructions: " + Environment.NewLine + Environment.NewLine + "Select the photos (must be .jpg files) you which to geotag based upon the gps track."
                + Environment.NewLine + Environment.NewLine + "*Select an empty directory to save the files to, do not save the files to the same directory they are selected from, the program will not override them."
                + Environment.NewLine + Environment.NewLine + "The program will also try and add a description to the photo using Microsofts's Vision API, but will only add if it has over a 50% confiedence in the result."
                );
        }

    }
}
