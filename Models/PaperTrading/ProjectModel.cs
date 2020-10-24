using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace InvestingOak.Models.PaperTrading
{
    public class ProjectModel
    {
        [Required]
        public string Name { get; set; }

        public string Description { get; set; }

        [Required]
        public DateTimeOffset CreationDate { get; set; }

        [Required]
        public decimal InitialBalance { get; set; }

        [Required]
        public decimal Balance { get; set; }

        public List<PositionModel> Positions { get; set; }
    }
}
