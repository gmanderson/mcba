using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AdminWebAPI.Models
{
    public class CustomerLock
    {
        // CustomerID must be 4 digits
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Range(1000, 9999)]
        public int CustomerID { get; set; }

        [Required, StringLength(50)]
        public string Name { get; set; }

        [Required]
        public bool IsLocked { get; set; }
    }
}
