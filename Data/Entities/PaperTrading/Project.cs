using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InvestingOak.Data.Entities.PaperTrading
{
    public class Project
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public string Description { get; set; }

        [Required]
        public DateTimeOffset CreationDate { get; set; }

        [Required]
        [Column(TypeName = "decimal(38, 2)")]
        public decimal InitialBalance { get; set; }

        [Required]
        [Column(TypeName = "decimal(38, 2)")]
        public decimal Balance { get; set; }

        [Required]
        public ApplicationUser User { get; set; }

        public List<Position> Positions { get; set; }
    }
}
