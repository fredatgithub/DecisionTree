using System.ComponentModel.DataAnnotations;

namespace DecisionTree.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("Anchor")]
    public partial class Anchor
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Anchor()
        {
            Connections = new HashSet<Connection>();
        }

        [Key]
        public Guid Id { get; set; }

        [ForeignKey("Node")]
        public Guid? NodeId { get; set; }

        [ForeignKey("AnchorType")]
        public int AnchorTypeId { get; set; }

        public virtual AnchorType AnchorType { get; set; }

        public virtual Node Node { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Connection> Connections { get; set; }
    }
}
