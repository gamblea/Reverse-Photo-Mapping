using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reverse_Geotag
{
    class Tagger
    {
        public Tagger(GpsFile gpsFile)
        {
            this.gpsFile = gpsFile;
        }

        public GpsFile gpsFile { get; set; }

        public List<Photo> getGeoTagPhotos(List<Photo> photos)
        {
            foreach (Photo photo in photos)
            {
                Location photoLocation = gpsFile.getClosestLocation(photo); // if this fails photo.errorthrown will become true
                if (photo.errorThrown == false)
                { 
                    photo.AddGeoTag(photoLocation.Latitude, photoLocation.Longitude);
                }
            }

            return photos;
        }

        public async Task<List<Photo>> getPhotosWithDescritpion(List<Photo> photos)
        {
            foreach (Photo photo in photos)
            {
                if (photo.errorThrown == false)
                {
                    await photo.AddDescription();
                }
            }

            return photos;
        }

    }
}
