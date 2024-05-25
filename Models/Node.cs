namespace Task.Models
{
    public class Node
    {
        public int NodeId { get; set; }
        public string NodeName { get; set; }
        public int? ParentNodeId { get; set; }
        public bool IsActive { get; set; }
        public DateTime StartDate { get; set; }
        public virtual Node ParentNode { get; set; }
        public virtual ICollection<Node> ChildNodes { get; set; }
    }

}
