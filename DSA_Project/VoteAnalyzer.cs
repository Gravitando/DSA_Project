using System;
using System.Diagnostics;

namespace VoteAnalyzingSystem
{
    public class VoteAnalyzer
    {
        private CustomLinkedList<Candidate> candidates;
        private DynamicArray<Region> regions;
        private CandidateTree candidateTree;

        public VoteAnalyzer()
        {
            candidates = new CustomLinkedList<Candidate>();
            regions = new DynamicArray<Region>();
            candidateTree = new CandidateTree();

            
            regions.Add(new Region("West", 100000, 0));
            regions.Add(new Region("North", 75000, 0));
            regions.Add(new Region("South", 82000, 0));
            regions.Add(new Region("Eastern", 98000, 0));

            // Add default candidates with random vote counts
            Random random = new Random();
            string[] candidateNames = { "Anura", "Tharindu", "Mahinda", "Vimal", "Supun", "Udana", "Desh" };
            string[] regionNames = { "West", "North", "South", "Eastern" };

            foreach (var name in candidateNames)
            {
                string region = regionNames[random.Next(regionNames.Length)];
                int voteCount = random.Next(100, 1000); // Random vote count between 100 and 1000
                var candidate = new Candidate(name, voteCount, region);
                candidates.AddLast(candidate);
                candidateTree.Insert(candidate);

                // Update voted voters in the region
                var regionObj = regions.Find(r => r.Name == region);
                if (regionObj != null)
                {
                    regionObj.VotedVoters += voteCount;
                }
            }

            // Add 100 randomly generated candidates
            for (int i = 0; i < 500; i++)
            {
                string randomName = "Candidate_" + (i + 1);
                string region = regionNames[random.Next(regionNames.Length)];
                int voteCount = random.Next(100, 1000); // Random vote count
                var candidate = new Candidate(randomName, voteCount, region);
                candidates.AddLast(candidate);
                candidateTree.Insert(candidate);

                // Update voted voters in the region
                var regionObj = regions.Find(r => r.Name == region);
                if (regionObj != null)
                {
                    regionObj.VotedVoters += voteCount;
                }
            }
        }

        public void AddCandidate(Candidate candidate)
        {
            candidates.AddLast(candidate);
            candidateTree.Insert(candidate);

            // Update voted voters in the region
            var region = regions.Find(r => r.Name == candidate.Region);
            if (region != null)
            {
                region.VotedVoters += candidate.VoteCount;
            }
            else
            {
                Console.WriteLine($"Region '{candidate.Region}' not found. Candidate added, but region data was not updated.");
            }
        }

        public void SearchCandidate(string name)
        {
            Candidate found = candidateTree.Search(name);
            if (found != null)
            {
                Console.WriteLine("\n=======================================");
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("         CANDIDATE SEARCH RESULT        ");
                Console.ResetColor();
                Console.WriteLine("=======================================\n");

                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine($"{"Name",-15} | {"Votes",-8} | {"Region",-10}");
                Console.WriteLine(new string('-', 40));
                Console.ResetColor();

                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"{found.Name,-15} | {found.VoteCount,-8} | {found.Region,-10}");
                Console.ResetColor();

                Console.WriteLine("\n=======================================");
            }
            else
            {
                Console.WriteLine("\n=======================================");
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("         CANDIDATE NOT FOUND!           ");
                Console.ResetColor();
                Console.WriteLine("=======================================\n");
            }
        }

        public void AddRegion(Region region)
        {
            regions.Add(region);
        }

        public void AnalyzeVoterTurnout()
        {
            Console.WriteLine("\n=======================================");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("         VOTER TURNOUT ANALYSIS        ");
            Console.ResetColor();
            Console.WriteLine("=======================================\n");

            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine($"{"Region",-10} | {"Turnout (%)",-10}");
            Console.WriteLine(new string('-', 25));
            Console.ResetColor();

            for (int i = 0; i < regions.Count; i++)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"{regions[i].Name,-10} | {regions[i].CalculateTurnoutPercentage():F2}%");
                Console.ResetColor();
            }
        }

        public void RankCandidates(Sorter sorter)
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            sorter.Sort(candidates);
            stopwatch.Stop();

            double elapsedTime = stopwatch.Elapsed.TotalMilliseconds;
            //stopwatch.Reset();

            Console.WriteLine("\n===============================================");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"  CANDIDATE RANKING ({sorter.AlgorithmName})  ");
            Console.ResetColor();
            Console.WriteLine($"  (Sorting Time: {elapsedTime:F2} ms) ");
            Console.WriteLine("===============================================\n");

            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine($"{"Rank",-5} | {"Candidate",-15} | {"Votes",-8}");
            Console.WriteLine(new string('-', 35));
            Console.ResetColor();

            int rank = 1;
            var current = candidates.Head;
            while (current != null)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"{rank,-5} | {current.Value.Name,-15} | {current.Value.VoteCount,-8}");
                Console.ResetColor();
                current = current.Next;
                rank++;
            }
        }

        public void AnalyzeRegionalSupport()
        {
            Console.WriteLine("\n=======================================");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("      REGIONAL SUPPORT ANALYSIS       ");
            Console.ResetColor();
            Console.WriteLine("=======================================\n");

            for (int i = 0; i < regions.Count; i++)
            {
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine($"Region: {regions[i].Name}");
                Console.ResetColor();
                Console.WriteLine(new string('-', 30));

                var current = candidates.Head;
                while (current != null)
                {
                    if (current.Value.Region == regions[i].Name)
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine($"  {current.Value.Name,-15}: {current.Value.VoteCount} votes");
                        Console.ResetColor();
                    }
                    current = current.Next;
                }
                Console.WriteLine();
            }
        }

        public void ViewDatabase()
        {
            Console.WriteLine("\n=======================================");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("         CANDIDATE DATABASE            ");
            Console.ResetColor();
            Console.WriteLine("=======================================\n");

            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine($"{"Candidate",-15} | {"Votes",-8} | {"Region",-10}");
            Console.WriteLine(new string('-', 40));
            Console.ResetColor();

            var current = candidates.Head;
            while (current != null)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"{current.Value.Name,-15} | {current.Value.VoteCount,-8} | {current.Value.Region,-10}");
                Console.ResetColor();
                current = current.Next;
            }

            Console.WriteLine("\n=======================================");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("         REGION DATABASE               ");
            Console.ResetColor();
            Console.WriteLine("=======================================\n");

            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine($"{"Region",-10} | {"Voted",-8} | {"Total Voters",-12} | {"Turnout (%)",-10}");
            Console.WriteLine(new string('-', 45));
            Console.ResetColor();

            for (int i = 0; i < regions.Count; i++)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"{regions[i].Name,-10} | {regions[i].VotedVoters,-8} | {regions[i].TotalVoters,-12} | {regions[i].CalculateTurnoutPercentage():F2}%");
                Console.ResetColor();
            }
        }

        public void DeleteCandidate(string name)
        {
            var current = candidates.Head;
            while (current != null)
            {
                if (current.Value.Name == name)
                {
                    // Update voted voters in the region
                    var region = regions.Find(r => r.Name == current.Value.Region);
                    if (region != null)
                    {
                        region.VotedVoters -= current.Value.VoteCount;
                    }

                    // Remove from candidates list
                    candidates.Remove(current);

                    // Remove from candidate tree
                    //candidateTree.Delete(current.Value);

                    Console.WriteLine($"Candidate {name} deleted successfully!");
                    return;
                }
                current = current.Next;
            }
            Console.WriteLine($"Candidate {name} not found!");
        }
    }
}