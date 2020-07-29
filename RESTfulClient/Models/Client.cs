using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RESTfulClient.Models
{
    [Table("Client", Schema = "dbo")]
    public class Client
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long ClientId { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Address { get; set; }
        
        public string PhoneNumber { get; set; }
        
        public string Email { get; set; }

        [Required]
        public string ModifiedBy { get; set; }

        [Required]
        public DateTime ModifiedOn { get; set; }
    }
}
