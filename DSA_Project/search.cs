using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VoteAnalyzingSystem
{
    public class CandidateNode
    {
        public Candidate Candidate { get; set; }
        public CandidateNode Left { get; set; }
        public CandidateNode Right { get; set; }

        public CandidateNode(Candidate candidate)
        {
            Candidate = candidate;
            Left = Right = null;
        }
    }

    public class CandidateBinarySearchTree
    {
        public CandidateNode Root { get; private set; }

        public CandidateBinarySearchTree()
        {
            Root = null;
        }

        // Method to insert a candidate into the tree
        public void Insert(Candidate candidate)
        {
            Root = InsertRecursive(Root, candidate);
        }

        // Recursive helper method for insertion
        private CandidateNode InsertRecursive(CandidateNode root, Candidate candidate)
        {
            if (root == null)
            {
                return new CandidateNode(candidate);
            }

            int comparison = string.Compare(candidate.Name, root.Candidate.Name, StringComparison.OrdinalIgnoreCase);
            if (comparison < 0)  // candidate.Name < root.Candidate.Name
            {
                root.Left = InsertRecursive(root.Left, candidate);
            }
            else if (comparison > 0)  // candidate.Name > root.Candidate.Name
            {
                root.Right = InsertRecursive(root.Right, candidate);
            }
            return root;
        }

        // Method to search for a candidate by name
        public Candidate Search(string candidateName)
        {
            return SearchRecursive(Root, candidateName);
        }

        // Recursive helper method for search
        private Candidate SearchRecursive(CandidateNode root, string candidateName)
        {
            if (root == null || string.Equals(root.Candidate.Name, candidateName, StringComparison.OrdinalIgnoreCase))
            {
                return root?.Candidate;
            }

            int comparison = string.Compare(candidateName, root.Candidate.Name, StringComparison.OrdinalIgnoreCase);
            if (comparison < 0)  // candidateName < root.Candidate.Name
            {
                return SearchRecursive(root.Left, candidateName);
            }
            else  // candidateName > root.Candidate.Name
            {
                return SearchRecursive(root.Right, candidateName);
            }
        }
    }

}
