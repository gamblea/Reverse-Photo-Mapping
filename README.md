# Reverse Photo Mapping

This program reverse geotags images based on a gps track and the time they were taken. 
Along with adding a description to the photo's metadata using Microsoft's Vision API. The description is only added if it has over a 50% confidence.

The photos are imported and then must be exported to a new directory to be saved with the geotag added to the photo's metadata. 

This program also uses the Metadata Extractor library to read the metadata.
