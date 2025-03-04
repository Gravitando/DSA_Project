namespace VoteAnalyzingSystem
{
    
    public class Region
    {
        public string Name { get; set; }
        public int TotalVoters { get; set; }
        public int VotedVoters { get; set; }

        public Region(string name, int totalVoters, int votedVoters)
        {
            Name = name;
            TotalVoters = totalVoters;
            VotedVoters = votedVoters;
        }

        public double CalculateTurnoutPercentage()
        {
            return (VotedVoters / (double)TotalVoters) * 100;
        }
    }
}



