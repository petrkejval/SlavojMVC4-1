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

    public class DisciplinyKategorieEditable
    {
        [ScaffoldColumn(false)]//nebude nikde zobrazen
        public int DisciplinyKategorieId { get; set; }

        [Display(Name = "Název")]
        [Required]
        public string Nazev { get; set; }
    }
}