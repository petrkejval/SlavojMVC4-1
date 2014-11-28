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

    public class KlubEditable
    {
        [ScaffoldColumn(false)]//nebude nikde zobrazen
        public int KlubId { get; set; }

        [Display(Name = "Název")]
        [Required]
        public string Nazev { get; set; }

        [Display(Name = "IČ")]
        [Required]
        public string IC { get; set; }

        [Display(Name = "Číslo účtu")]
        [Required]
        public string CisloUctu { get; set; }

        [Display(Name = "E-mail")]
        public string Mail { get; set; }

        [Display(Name = "WWW stránky")]
        public string WWW { get; set; }

        [Display(Name = "Kód klubu u ČKA")]
        [Required]
        public int KodKlubu { get; set; }

        [Display(Name = "Obrázek")]
        public string Image { get; set; }

        [Display(Name = "Webová stránka Kontaktů")]
        [UIHint("GridForeignKey")]
        [Required]
        public int WebPageId { get; set; }
    }
}