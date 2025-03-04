namespace VoteAnalyzingSystem
{
    
    public class CandidateTree
    {
        public TreeNode Root { get; private set; }

        public void Insert(Candidate candidate)
        {
            Root = InsertRec(Root, candidate);
        }

        private TreeNode InsertRec(TreeNode root, Candidate candidate)
        {
            if (root == null)
            {
                return new TreeNode(candidate);
            }

            if (string.Compare(candidate.Name, root.Data.Name) < 0)
            {
                root.Left = InsertRec(root.Left, candidate);
            }
            else
            {
                root.Right = InsertRec(root.Right, candidate);
            }

            return root;
        }

        public Candidate Search(string name)
        {
            return SearchRec(Root, name);
        }

        private Candidate SearchRec(TreeNode root, string name)
        {
            if (root == null || root.Data.Name == name)
            {
                return root?.Data;
            }

            if (string.Compare(name, root.Data.Name) < 0)
            {
                return SearchRec(root.Left, name);
            }
            else
            {
                return SearchRec(root.Right, name);
            }
        }
    }
}
