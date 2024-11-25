using System;
using System.Collections.Generic;
using System.Linq;

namespace NationalElectionSystem
{
    public class ElectionManager : IElectionManager
    {
        public Dictionary<string, Candidate> candidates = new Dictionary<string, Candidate>();
        public Dictionary<string, Voter> voters = new Dictionary<string, Voter>();

        public void AddCandidate(Candidate candidate)
        {
            candidates.Add(candidate.Id, candidate);
        }

        public void AddVoter(Voter voter)
        {
            voters.Add(voter.Id, voter);
        }

        public bool Contains(Candidate candidate)
        {
            return candidates.ContainsKey(candidate.Id);
        }

        public bool Contains(Voter voter)
        {
            return voters.ContainsKey(voter.Id);
        }

        public IEnumerable<Candidate> GetCandidates()
            => candidates.Values;

        public IEnumerable<Voter> GetVoters()
            => voters.Values;

        public void Vote(string voterId, string candidateId)
        {
            if(!voters.ContainsKey(voterId)
                || !candidates.ContainsKey(candidateId)
                || voters[voterId].Age < 18
                || voters[voterId].Candidate != null)
            {
                throw new ArgumentException();
            }

            candidates[candidateId].Voters.Add(voters[voterId]);
            voters[voterId].Candidate = candidates[candidateId];
        }

        public int GetVotesForCandidate(string candidateId)
            => candidates[candidateId].Voters.Count;

        public IEnumerable<Candidate> GetWinner()
        {
            if (voters.Count() == 0)
            {
                return null;
            }

            return candidates.Values.OrderByDescending(c => c.Voters.Count()).Take(1);
        }

        public IEnumerable<Candidate> GetCandidatesByParty(string party)
        {
            return candidates.Values.Where(c => c.Party == party)
                    .OrderByDescending(c => c.Voters.Count());
        }
    }
}