using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DecisionTree.Model
{
    [Table("Node")]
    public class Node
    {
        [Key]
        public Guid Id { get; set; }
        [Required]
        [StringLength(100)]
        public string Name { get; set; }
        public Guid? PassNodeId { get; set; }
        //[ForeignKey("Id")]
        public Node PassNode { get; set; }
        public Guid? FailNodeId { get; set; }
        //[ForeignKey("FailNodeId")]
        public Node FailNode { get; set; }
        public bool IsConclusion { get; set; }
    }
}