using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace DecisionTree.Model
{
    [Table("Connection")]
    public partial class Connection
    {
        public Guid Id { get; set; }

        public Guid SourceAnchorId { get; set; }

        public Guid TargetAnchorId { get; set; }

        public virtual Anchor Anchor { get; set; }

        public virtual Anchor Anchor1 { get; set; }
    }
}
