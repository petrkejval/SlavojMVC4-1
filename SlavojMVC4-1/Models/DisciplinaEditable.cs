namespace SlavojMVC4_1.Models
{
    using System;
    using System.Linq;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.Runtime.Serialization;
    using System.Web.Mvc;
    using Foolproof;

    public class DisciplinaEditable
    {
        [ScaffoldColumn(false)]//nebude nikde zobrazen
        public int DisciplinaId { get; set; }

        [Display(Name = "Pocet hodů")]
        [Required]
        public int PocetHodu { get; set; }

        [Display(Name = "Kategorie disciplíny")]
        [Required]
        [UIHint("GridForeignKey")]
        [Range(1, int.MaxValue, ErrorMessage = "Kategorie disciplíny musí být vyplněna.")]
        public int DisciplinyKategorieId { get; set; }

        [Display(Name = "Popis")]
        public string Popis { get; set; }
    }
}