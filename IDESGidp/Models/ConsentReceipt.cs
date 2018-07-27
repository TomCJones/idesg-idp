using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IDESGidp.Models
{
    public class ConsentReceipt
    {
        [Key]
        public Guid ID { get; set; }

        [Required]
        public DateTime CreatedOn { get; set; }

        public DateTime UpdatedOn { get; set; }

        public DateTime ExpiresOn { get; set; }

        [Required]
        public string Scopes { get; set; }

        [Required]
        public string SubjectID { get; set; }

        [Required]
        public string ClientID { get; set; }
    }
}
