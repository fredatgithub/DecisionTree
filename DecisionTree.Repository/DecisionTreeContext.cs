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

        public virtual DbSet<Node> Nodes { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Node>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<Node>()
                .HasOptional(n => n.FailNode)
                .WithMany()
                .HasForeignKey(n => n.FailNodeId);

            modelBuilder.Entity<Node>()
                .HasOptional(n => n.PassNode)
                .WithMany()
                .HasForeignKey(n => n.PassNodeId);
        }
    }
}
