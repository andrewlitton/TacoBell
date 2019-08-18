using System;
using System.Linq;
using System.IO;
using GeoCoordinatePortable;

namespace LoggingKata
{
    class Program
    {
        static readonly ILog logger = new TacoLogger();
        const string csvPath = "TacoBell-US-AL.csv";

        static void Main(string[] args)
        {

            var lines = File.ReadAllLines(csvPath);

            var parser = new TacoParser();

            var locations = lines.Select(parser.Parse).ToArray();         

            ITrackable trackable1 = null;
            ITrackable trackable2 = null;
            double maxDistance = 0;

            for (int i = 0; i < locations.Length; i++)
            {
                ITrackable locA = locations[i];
                GeoCoordinate corA = new GeoCoordinate(locA.Location.Latitude, locA.Location.Longitude);

                for (int k = 0; k < locations.Length; k++)
                {
                    ITrackable locB = locations[k];
                    GeoCoordinate corB = new GeoCoordinate(locB.Location.Latitude, locB.Location.Longitude);

                    double distance = corB.GetDistanceTo(corA);

                    if (distance > maxDistance)
                    {
                        maxDistance = distance;
                        trackable1 = locA;
                        trackable2 = locB;
                    }
                }
            }

            Console.WriteLine(trackable1.Name);
            Console.WriteLine(trackable2.Name);
            Console.WriteLine(maxDistance); 

        }
    }
}