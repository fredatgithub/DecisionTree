using DecisionTree.Model;

namespace DecisionTree.Repository
{
    using System.Data.Entity;

    // ReSharper disable once PartialTypeWithSinglePart
    public partial class DecisionTreeContext : DbContext
    {
        public DecisionTreeContext()
            : base("name=DecisionTreeContext")
        {
        }

        public virtual DbSet<Anchor> Anchors { get; set; }
        public virtual DbSet<AnchorType> AnchorTypes { get; set; }
        public virtual DbSet<Connection> Connections { get; set; }
        public virtual DbSet<Graph> Graphs { get; set; }
        public virtual DbSet<Node> Nodes { get; set; }
        public virtual DbSet<NodeType> NodeTypes { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {

        }
    }
}
