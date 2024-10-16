using System.Collections.Generic;

namespace Exam.MoovIt
{
    public class Route
    {
        public string Id { get; set; }

        public double Distance { get; set; }

        public int Popularity { get; set; }

        public bool IsFavorite { get; set; }

        public List<string> LocationPoints { get; set; } = new List<string>();

        public Route(string id, double distance, int popularity, bool isFavorite, List<string> locationPoints)
        {
            Id = id;
            Distance = distance;
            Popularity = popularity;
            IsFavorite = isFavorite;
            LocationPoints = locationPoints;
        }
    }
}
