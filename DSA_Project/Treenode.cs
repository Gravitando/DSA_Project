namespace VoteAnalyzingSystem
{
    
    public class TreeNode
    {
        public Candidate Data { get; set; }
        public TreeNode Left { get; set; }
        public TreeNode Right { get; set; }

        public TreeNode(Candidate data)
        {
            Data = data;
            Left = null;
            Right = null;
        }
    }
}
