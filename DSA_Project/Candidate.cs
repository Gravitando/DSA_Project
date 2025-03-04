namespace VoteAnalyzingSystem
{
    
    public class Candidate
    {
        public string Name { get; set; }
        public int VoteCount { get; set; }
        public string Region { get; set; }

        public Candidate(string name, int voteCount, string region)
        {
            Name = name;
            VoteCount = voteCount;
            Region = region;
        }
    }
}



