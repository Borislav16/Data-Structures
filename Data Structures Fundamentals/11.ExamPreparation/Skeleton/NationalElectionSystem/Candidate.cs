using System.Collections.Generic;

namespace NationalElectionSystem
{
    public class Candidate
    {
        public Candidate()
        {
            Voters = new HashSet<Voter>();
        }

        public string Id { get; set; }

        public string Name { get; set; }

        public string Party { get; set; }

        public HashSet<Voter> Voters { get; set; }
    }
}