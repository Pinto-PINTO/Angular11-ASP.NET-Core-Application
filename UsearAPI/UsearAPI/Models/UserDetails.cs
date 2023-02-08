using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UsearAPI.Models
{
    public class UserDetails
    {
        [Key]
        public int UserId { get; set; }

        [Column(TypeName = "nvarchar(100)")]
        public string Name { get; set; }

        //dd/mm/yyyy
        [Column(TypeName = "nvarchar(10)")]
        public string DateOfBirth { get; set; }

        [Column(TypeName = "nvarchar(6)")]
        public string Gender { get; set; }

        [Column(TypeName = "nvarchar(15)")]
        public string PhoneNumber { get; set; }

    }
}
