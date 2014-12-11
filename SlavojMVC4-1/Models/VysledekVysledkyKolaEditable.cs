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

    public class VysledekVysledkyKolaEditable
    {
        [ScaffoldColumn(false)]//nebude nikde zobrazen
        [Key]
        public int VysledkyKoloId { get; set; }

        [ScaffoldColumn(false)]//nebude nikde zobrazen
        [Key]
        public int VysledekId { get; set; }

        [Display(Name = "Pořadové číslo kola")]
        [Required]
        public int PorCisloKola { get; set; }

        [Display(Name = "Odkaz na Zpravodaj kola")]
        [Required]
        public string Zpravodaj { get; set; }
    }
}