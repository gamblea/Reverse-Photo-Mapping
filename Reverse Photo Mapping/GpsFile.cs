using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Windows.Forms;


namespace Reverse_Geotag
{
    class GpsFile
    {
        public GpsFile(string path)
        {
            filePath = path;
            int index = path.LastIndexOf(@"\") + 1;
            fileName = path.Substring(index);
            Locations = new List<Location>();
            Locations = ReadGpsFile(filePath);
        }

        public string filePath { get; set; }
        public string fileName { get; set; }
        public List<Location> Locations { get; }
                
        //2016-07-31T22:07:36Z

        private DateTime parseGPXTime(String gpxTime)
        {
            int year = int.Parse(gpxTime.Substring(0, 4));
            int month = int.Parse(gpxTime.Substring(5, 2));
            int day = int.Parse(gpxTime.Substring(8, 2));
            int hour = int.Parse(gpxTime.Substring(11, 2));
            int minute = int.Parse(gpxTime.Substring(14, 2));
            int second = int.Parse(gpxTime.Substring(17, 2));

            DateTime time = new DateTime(year, month, day, hour, minute, second);
            return time;
        }

        public Location getClosestLocation(Photo photo)
        {
            DateTime actualTime = photo.TimeTaken;
            Location firstLocation = Locations.First();
            Location lastLocation = Locations.Last();
            if (lastLocation.Time < actualTime || firstLocation.Time > actualTime ) //checks if time is within range
            {
                photo.errorThrown = true;
                MessageBox.Show(photo.fileName + " could not be loaded." + Environment.NewLine + "Time of photo is not within gps recording.");
                return null;
            }
            Location closesttLocation = firstLocation; 
            TimeSpan smallestDifference = lastLocation.Time - firstLocation.Time; // set difference to max
            TimeSpan lastTimeDifference = lastLocation.Time - firstLocation.Time;
            //Location lastLocation = null;
            foreach (Location locationToTest in Locations)
            {
                TimeSpan timeDifference = actualTime - locationToTest.Time;
                if (timeDifference.Duration() < smallestDifference.Duration())
                {
                    smallestDifference = timeDifference;
                    closesttLocation = locationToTest;
                }
                if (lastTimeDifference.Duration() < timeDifference.Duration())
                {
                    break;
                }
                else
                {
                    lastTimeDifference = timeDifference;
                }
            }
            return closesttLocation;

        }

        private List<Location> ReadGpsFile(string path)
        {
            List<Location> locations = new List<Location>();
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(path);

            XmlNamespaceManager nsmgr = new XmlNamespaceManager(xmlDoc.NameTable);
            nsmgr.AddNamespace("x", "http://www.topografix.com/GPX/1/1");

            XmlNodeList nodeList = xmlDoc.SelectNodes("//x:trkpt", nsmgr);

            foreach (XmlNode node in nodeList)
            {
                double lat = -1;
                double lon = -1;
                double ele = -1;
                double speed = -1;
                string time = "";

                foreach (XmlAttribute attribute in node.Attributes)
                {
                    if (attribute.Name == "lat")
                    {
                        lat = double.Parse(attribute.Value);
                        continue;
                    }
                    if (attribute.Name == "lon")
                    {
                        lon = Double.Parse(attribute.Value);
                        continue;
                    }
                }
                foreach (XmlNode childNode in node.ChildNodes)
                {
                    if (childNode.Name == "ele")
                    {
                        ele = Double.Parse(childNode.InnerXml);
                        continue;
                    }
                    if (childNode.Name == "time")
                    {
                        time = childNode.InnerXml;
                        continue;
                    }
                    if (childNode.Name == "speed")
                    {
                        speed = Double.Parse(childNode.InnerXml);
                        continue;
                    }
                }

                locations.Add(new Location(lat, lon, ele, speed, parseGPXTime(time)));
            }
            
            
            return locations;  
        }

        


}
}
