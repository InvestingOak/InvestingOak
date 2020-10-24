using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace InvestingOak.Models.PaperTrading
{
    public class ProjectUpdateModel
    {
        [Required]
        public string Name { get; set; }

        public string Description { get; set; }

        public DateTimeOffset CreationDate { get; set; }

        public decimal InitialBalance { get; set; }

        public decimal Balance { get; set; }

        public List<PositionModel> Positions { get; set; }
    }
}
