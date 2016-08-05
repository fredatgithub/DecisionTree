using DecisionTree.Model;

namespace DecisionTree.Repository
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

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
            modelBuilder.Entity<Anchor>()
                .HasMany(e => e.Connections)
                .WithRequired(e => e.Anchor)
                .HasForeignKey(e => e.SourceAnchorId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Anchor>()
                .HasMany(e => e.Connections1)
                .WithRequired(e => e.Anchor1)
                .HasForeignKey(e => e.TargetAnchorId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<AnchorType>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<AnchorType>()
                .HasMany(e => e.Anchors)
                .WithRequired(e => e.AnchorType)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Graph>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<Graph>()
                .Property(e => e.Description)
                .IsUnicode(false);

            modelBuilder.Entity<Node>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<NodeType>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<NodeType>()
                .HasMany(e => e.Nodes)
                .WithRequired(e => e.NodeType)
                .WillCascadeOnDelete(false);
        }
    }
}
