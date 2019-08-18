namespace LoggingKata
{
  
    /// Parses a POI file to locate all the Taco Bells
    
    public class TacoParser
    {
        readonly ILog logger = new TacoLogger();
        
        public ITrackable Parse(string line)
        {
            if (line == null)
            {
                return null;
            }

            var cells = line.Split(',');

            if (cells.Length < 3)
            {
                logger.LogError("Not enough info to parse:" + line);
                return null;
            }

            string strLatitude = cells[0];
            string strLongitude = cells[1];
            string name = cells[2];

            if (string.IsNullOrWhiteSpace(name))
            {
                return null;
            }

            bool latitudeParseSuccess = double.TryParse(strLatitude, out double latitude);
            bool longitudeParseSuccess = double.TryParse(strLongitude, out double longitude);

            if (latitudeParseSuccess == false || longitudeParseSuccess == false)
            {
                return null;
            }

            TacoBell tacoBell = new TacoBell();

            Point tacoPoint = new Point();
            tacoPoint.Latitude = latitude;
            tacoPoint.Longitude = longitude;

            tacoBell.Name = name;
            tacoBell.Location = tacoPoint;

            return tacoBell;
        }
    }
}