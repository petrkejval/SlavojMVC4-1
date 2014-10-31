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

    public class WebPageEditable
    {
        [ScaffoldColumn(false)]//nebude nikde zobrazen
        public int WebPageId { get; set; }

        [Display(Name = "Název webové stránky"), Required]
        public string Nazev { get; set; }

        [Display(Name = "Text webové stránky")]
        [UIHint("Editor")]
        public string HtmlText { get; set; }

    }
}