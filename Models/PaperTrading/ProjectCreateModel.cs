using System.ComponentModel.DataAnnotations;

namespace InvestingOak.Models.PaperTrading
{
    public class ProjectCreateModel
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public decimal InitialBalance { get; set; }

        public string Description { get; set; }
    }
}
