namespace ConsoleApplication2
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("dbSetProperty")]
    public partial class dbSetProperty
    {
        [Key]
        public long Key { get; set; }

        [Required]
        [StringLength(50)]
        public string Property { get; set; }
    }
}
