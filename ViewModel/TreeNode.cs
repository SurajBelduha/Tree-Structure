namespace Task.ViewModel
{
    public class TreeNode
    {
        public int id { get; set; }
        public string text { get; set; }
        public List<TreeNode> children { get; set; } = new List<TreeNode>();
    }

}
