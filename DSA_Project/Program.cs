using System;

namespace VoteAnalyzingSystem
{
    class Program
    {
        static void Main(string[] args)
        {
            VoteAnalyzer analyzer = new VoteAnalyzer();
            bool exit = false;

            while (!exit)
            {
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("=============================================");
                Console.WriteLine("      Welcome to Vote Analyzing System       ");
                Console.WriteLine("=============================================");
                Console.ResetColor();

                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("\nPlease select an option:\n");
                Console.WriteLine("1: View database");
                
                Console.WriteLine("2: Add region");
                Console.WriteLine("3: Add candidate");
                Console.WriteLine("4: Delete candidate");
                Console.WriteLine("5: Analyze voter turnout");
                Console.WriteLine("6: Rank candidates");
                Console.WriteLine("7: Analyze regional support");
                Console.WriteLine("8: Search Candidate");
                Console.WriteLine("9: Exit");
                Console.ResetColor();

                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write("\nEnter your choice: ");
                Console.ResetColor();

                int choice;
                if (!int.TryParse(Console.ReadLine(), out choice))
                {
                    Console.WriteLine("Invalid input! Please enter a number.");
                    continue;
                }

                switch (choice)
                {
                    case 1:
                        analyzer.ViewDatabase();
                        break;
                    case 3:
                        Console.Write("Enter candidate name: ");
                        string name = Console.ReadLine();
                        Console.Write("Enter vote count: ");
                        int voteCount;
                        if (!int.TryParse(Console.ReadLine(), out voteCount))
                        {
                            Console.WriteLine("Invalid vote count! Please enter a valid number.");
                            break;
                        }
                        Console.Write("Enter region: ");
                        string region = Console.ReadLine();
                        analyzer.AddCandidate(new Candidate(name, voteCount, region));
                        Console.WriteLine("Candidate added successfully!");
                        break;
                    case 2:
                        Console.Write("Enter region name: ");
                        string regionName = Console.ReadLine();
                        Console.Write("Enter total voters in the region: ");
                        int totalVoters;
                        if (!int.TryParse(Console.ReadLine(), out totalVoters))
                        {
                            Console.WriteLine("Invalid total voters! Please enter a valid number.");
                            break;
                        }
                        Console.Write("Enter number of voted voters: ");
                        int votedVoters;
                        if (!int.TryParse(Console.ReadLine(), out votedVoters))
                        {
                            Console.WriteLine("Invalid voted voters! Please enter a valid number.");
                            break;
                        }
                        analyzer.AddRegion(new Region(regionName, totalVoters, votedVoters));
                        Console.WriteLine("Region added successfully!");
                        break;
                    case 4:
                        Console.Write("Enter candidate name to delete: ");
                        string candidateName = Console.ReadLine();
                        analyzer.DeleteCandidate(candidateName);
                        break;
                    case 5:
                        analyzer.AnalyzeVoterTurnout();
                        break;
                    case 6:
                        analyzer.RankCandidates(new BubbleSort());
                        analyzer.RankCandidates(new MergeSort());
                        analyzer.RankCandidates(new QuickSort());
                        break;
                    case 7:
                        analyzer.AnalyzeRegionalSupport();
                        break;
                    case 9:
                        exit = true;
                        break;
                    case 8: 
                        Console.Write("Enter candidate name to search: ");
                        string searchName = Console.ReadLine();
                        analyzer.SearchCandidate(searchName);
                        break;
                    default:
                        Console.WriteLine("Invalid choice!");
                        break;
                }
            }
        }
    }
}
