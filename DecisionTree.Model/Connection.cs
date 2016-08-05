using System.ComponentModel.DataAnnotations;

namespace DecisionTree.Model
{
    using System;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("Connection")]
    public partial class Connection
    {
        [Key]
        public Guid Id { get; set; }

        public Guid SourceAnchorId { get; set; }

        [ForeignKey("TargetAnchor")]
        public Guid TargetAnchorId { get; set; }

        [ForeignKey("SourceAnchorId")]
        public Anchor SourceAnchor { get; set; }

        public Anchor TargetAnchor { get; set; }
    }
}
