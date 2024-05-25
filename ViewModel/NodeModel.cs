namespace Task.ViewModel
{
    public class NodeModel
    {
        public int NodeId { get; set; }
        public string NodeName { get; set; }
        public int? ParentNodeId { get; set; }
        public bool IsActive { get; set; }
        public DateTime StartDate { get; set; }
    }
}
