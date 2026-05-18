using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Welcome.Model;

namespace DataLayer.Model
{
    [Table("Users")]
    public class DatabaseUser : User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public override int Id { get; set; }

        [Required] // Add this if Email is a required property
        public string Email { get; set; } // Add this property
        public string FacultyNumber { get; set; } // ✅ Add this property
    }
}
