using System.ComponentModel.DataAnnotations;

namespace InvestingOak.Models.PaperTrading
{
    public class ProjectDescModel
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public decimal Balance { get; set; }

        public string Description { get; set; }
    }
}
