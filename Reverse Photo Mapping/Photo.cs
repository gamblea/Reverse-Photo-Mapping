using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.IO;
using MetadataExtractor;
using MetadataExtractor.Formats.Exif;
using System.Drawing.Imaging;
using System.Windows.Forms;
using Microsoft.ProjectOxford.Vision.Contract;
using Microsoft.ProjectOxford.Vision;
using System.Drawing.Drawing2D;

public enum MetaProperty
{
    Title = 40091
    //Comment = 40092,
    //Author = 40093,
    //Keywords = 40094,
    //Subject = 40095,
    //Copyright = 33432,
    //Software = 11,
    //DateTime = 36867
};


namespace Reverse_Geotag
{
    class Photo
    {
        public string filePath { get; }
        public string fileName { get; }
        public DateTime TimeTaken { get; }
        public Location location { get; }
        public bool errorThrown { get; set; }
        public Image image { get; set; }
        public bool foundDescrition { get; set; }

        public Photo(string path)
        {
            errorThrown = false;
            filePath = path;
            int index = path.LastIndexOf(@"\") + 1;
            fileName = path.Substring(index);

            var directories = ImageMetadataReader.ReadMetadata(path);

            var subInfoDirectories = directories.OfType<MetadataExtractor.Formats.Exif.ExifSubIfdDirectory>();

            String time = "";
            foreach (var directory in subInfoDirectories)
            {
                foreach (MetadataExtractor.Tag tag in directory.Tags)
                {
                    if (tag.Name == @"Date/Time Original")
                    {
                        time = tag.Description; //2017:05:15 11:53:50
                        break;
                    }
                }
            }

            if (time == "")
            {
                MessageBox.Show(fileName + " could not be loaded." + Environment.NewLine + "No time was found in the Metadata of the photo.");
                errorThrown = true;
            }

            TimeTaken = parseTime(time);

         /*   using (var stream = new FileStream(path, FileMode.Open))
            {
                try
                {
                    image = new Bitmap(stream);
                }
                catch (Exception)
                {
                    MessageBox.Show(fileName + " could not be loaded." + Environment.NewLine + "Bitmap could not be generated from file.");
                    errorThrown = true;
                }
            } */

                try
                {
                    image = new Bitmap(new FileStream(path, FileMode.Open));
                }
                catch (Exception)
                {
                    MessageBox.Show(fileName + " could not be loaded." + Environment.NewLine + "Bitmap could not be generated from file.");
                    errorThrown = true;
                }
            

        }

        public void AddGeoTag(double lat, double lng)
        {
            Image tempImg = Geotag(image, lat, lng);
            image = tempImg;
        }

        public Bitmap ResizeImage(Image imageToResize, int width, int height)
        {
            var destRect = new System.Drawing.Rectangle(0, 0, width, height);
            var destImage = new Bitmap(width, height);

            destImage.SetResolution(imageToResize.HorizontalResolution, imageToResize.VerticalResolution);

            using (var graphics = Graphics.FromImage(destImage))
            {
                graphics.CompositingMode = CompositingMode.SourceCopy;
                graphics.CompositingQuality = CompositingQuality.HighQuality;
                graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                graphics.SmoothingMode = SmoothingMode.HighQuality;
                graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;

                using (var wrapMode = new ImageAttributes())
                {
                    wrapMode.SetWrapMode(WrapMode.TileFlipXY);
                    graphics.DrawImage(imageToResize, destRect, 0, 0, imageToResize.Width, imageToResize.Height, GraphicsUnit.Pixel, wrapMode);
                }
            }

            return destImage;
        }

        public async Task AddDescription()
        {
            VisionServiceClient client = new VisionServiceClient("d23d165915ab41d2a99371946a9eab45");

            AnalysisResult analysisResult;

            var features = new VisualFeature[] { VisualFeature.Description };


            Bitmap tempImg = new Bitmap(image);

            Bitmap resizedImage = ResizeImage(tempImg, 800, 600);


            var ms = new MemoryStream();
            resizedImage.Save(ms, ImageFormat.Jpeg);

            // If you're going to read from the stream, you may need to reset the position to the start
            //ms.Position = 0;

            ms.Seek(0,SeekOrigin.Begin);

            using (var fs = ms)
            {
                analysisResult = await client.AnalyzeImageAsync(fs, features);
            }

            string description = "";

            try
            {
                description = analysisResult.Description.Captions.FirstOrDefault(x => x.Confidence > 0.5).Text;
            }
            catch (Exception)
            {
                description = "";
            }

            if (description != "")
            {
                image = WriteDescription(image, description);
            }
            
        }



        private Image Geotag(Image original, double lat, double lng)
        {
            // These constants come from the CIPA DC-008 standard for EXIF 2.3
            const short ExifTypeByte = 1;
            const short ExifTypeAscii = 2;
            const short ExifTypeRational = 5;

            const int ExifTagGPSVersionID = 0x0000;
            const int ExifTagGPSLatitudeRef = 0x0001;
            const int ExifTagGPSLatitude = 0x0002;
            const int ExifTagGPSLongitudeRef = 0x0003;
            const int ExifTagGPSLongitude = 0x0004;

            char latHemisphere = 'N';
            if (lat < 0)
            {
                latHemisphere = 'S';
                lat = -lat;
            }
            char lngHemisphere = 'E';
            if (lng < 0)
            {
                lngHemisphere = 'W';
                lng = -lng;
            }

            MemoryStream ms = new MemoryStream();
            original.Save(ms, ImageFormat.Jpeg);
            ms.Seek(0, SeekOrigin.Begin);

            Image img = Image.FromStream(ms);
            AddProperty(img, ExifTagGPSVersionID, ExifTypeByte, new byte[] { 2, 3, 0, 0 });
            AddProperty(img, ExifTagGPSLatitudeRef, ExifTypeAscii, new byte[] { (byte)latHemisphere, 0 });
            AddProperty(img, ExifTagGPSLatitude, ExifTypeRational, ConvertToRationalTriplet(lat));
            AddProperty(img, ExifTagGPSLongitudeRef, ExifTypeAscii, new byte[] { (byte)lngHemisphere, 0 });
            AddProperty(img, ExifTagGPSLongitude, ExifTypeRational, ConvertToRationalTriplet(lng));

            return img;
        }

        private Image WriteDescription(Image original, string description)
        {
            const short ExifTypeAscii = 2;
            const int ExifTagDescription = 0x010e;
            byte[] bytes = Encoding.ASCII.GetBytes(description);

            MemoryStream ms = new MemoryStream();
            original.Save(ms, ImageFormat.Jpeg);
            ms.Seek(0, SeekOrigin.Begin);

            Image img = Image.FromStream(ms);

            AddProperty(img, ExifTagDescription, ExifTypeAscii, bytes);

            return img;
        }

        private void AddProperty(Image img, int id, short type, byte[] value)
        {
            PropertyItem pi = img.PropertyItems[0];
            pi.Id = id;
            pi.Type = type;
            pi.Len = value.Length;
            pi.Value = value;
            img.SetPropertyItem(pi);
        }

        private byte[] ConvertToRationalTriplet(double value)
        {
            int degrees = (int)Math.Floor(value);
            value = (value - degrees) * 60;
            int minutes = (int)Math.Floor(value);
            value = (value - minutes) * 60 * 100;
            int seconds = (int)Math.Round(value);
            byte[] bytes = new byte[3 * 2 * 4]; // Degrees, minutes, and seconds, each with a numerator and a denominator, each composed of 4 bytes
            int i = 0;
            Array.Copy(BitConverter.GetBytes(degrees), 0, bytes, i, 4); i += 4;
            Array.Copy(BitConverter.GetBytes(1), 0, bytes, i, 4); i += 4;
            Array.Copy(BitConverter.GetBytes(minutes), 0, bytes, i, 4); i += 4;
            Array.Copy(BitConverter.GetBytes(1), 0, bytes, i, 4); i += 4;
            Array.Copy(BitConverter.GetBytes(seconds), 0, bytes, i, 4); i += 4;
            Array.Copy(BitConverter.GetBytes(100), 0, bytes, i, 4);
            return bytes;
        }

        private DateTime parseTime(string exifTime)
        {
            int year, month, day, hour, minute, second = 0;
            try
            {
                year = int.Parse(exifTime.Substring(0, 4));
                month = int.Parse(exifTime.Substring(5, 2));
                day = int.Parse(exifTime.Substring(8, 2));
                hour = int.Parse(exifTime.Substring(11, 2));
                minute = int.Parse(exifTime.Substring(14, 2));
                second = int.Parse(exifTime.Substring(17, 2));
            }
            catch (Exception)
            {
                errorThrown = true;
                MessageBox.Show(fileName + " could not be loaded." + Environment.NewLine + "DateTime could not be parsed.");
                return new DateTime(0);
            }

            DateTime time = new DateTime(year, month, day, hour, minute, second);
            return time;

            //2017:05:15 11:53:50
        }

        public override string ToString()
        {
            return fileName;
        }
    }
}

