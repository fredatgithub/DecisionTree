namespace DecisionTree.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("Node")]
    public partial class Node
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Node()
        {
            Anchors = new HashSet<Anchor>();
        }

        [Key]
        public Guid Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        [ForeignKey("NodeType")]
        public int NodeTypeId { get; set; }

        [ForeignKey("Graph")]
        public Guid? GraphId { get; set; }

        public double PosX { get; set; }

        public double PosY { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Anchor> Anchors { get; set; }

        public virtual Graph Graph { get; set; }

        public virtual NodeType NodeType { get; set; }
    }
}
