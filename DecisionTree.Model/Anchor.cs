using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace DecisionTree.Model
{
    [Table("Anchor")]
    public partial class Anchor
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Anchor()
        {
            Connections = new HashSet<Connection>();
        }

        public Guid Id { get; set; }

        public Guid? NodeId { get; set; }

        public int AnchorTypeId { get; set; }

        public virtual AnchorType AnchorType { get; set; }

        public virtual Node Node { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Connection> Connections { get; set; }

        public virtual ICollection<Connection> Connections1 { get; set; }
    }
}
