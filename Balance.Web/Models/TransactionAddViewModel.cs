using System.ComponentModel.DataAnnotations;

namespace Balance.Web.Models
{
    public class TransactionAddViewModel
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public int Amount { get; set; }
    }
}
