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

    public class RocnikEditable
    {
        [ScaffoldColumn(false)]//nebude nikde zobrazen
        public int RocnikId { get; set; }

        [Display(Name = "Název ročníku")]
        [Required]
        public string Nazev { get; set; }

        [Display(Name = "Vybraný ročník")]
        [Required]
        public bool JeVybrany { get; set; }

    }
}