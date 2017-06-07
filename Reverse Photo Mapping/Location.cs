using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reverse_Geotag
{
    class Location
    {
        public Location(double latitude, double longitude, double elevation,double speed, DateTime time)
        {
            Latitude = latitude;
            Longitude = longitude;
            Elevation = elevation;
            Speed = speed;
            Time = time;
        }

        public double Latitude { get; }
        public double Longitude { get; }
        public double Elevation { get; }
        public double Speed { get; }
        public DateTime Time { get; }

        public override string ToString()
        {
            return "Latitude: " + Latitude + Environment.NewLine + "Longitude: " + Longitude + Environment.NewLine
                + "Elevation: " + Elevation + Environment.NewLine + "Speed: " + Speed + Environment.NewLine +
                "Time: " + Time.ToString();
        }

    }
}
