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

    public class TurnajEditable
    {
        [ScaffoldColumn(false)]//nebude nikde zobrazen
        public int TurnajId { get; set; }

        [Display(Name = "Název turnaje")]
        [Required]
        public string Nazev { get; set; }

        [Display(Name = "Ředitel turnaje")]
        [Required]
        [UIHint("GridForeignKey")]
        [Range(1, int.MaxValue, ErrorMessage = "Ředitel turnaje musí být vyplněn.")]
        public int ReditelTurnajeId { get; set; }

        [Display(Name = "Termín turnaje Od")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:d.M.yyyy}", ApplyFormatInEditMode = true)]
        [Required]
        public System.DateTime DatumOd { get; set; }

        [Display(Name = "Termín turnaje Do")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:d.M.yyyy}", ApplyFormatInEditMode = true)]
        [Required]
        public System.DateTime DatumDo { get; set; }

        [Display(Name = "Článek turnaje")]
        [UIHint("Editor")]
        public string HtmlClanek { get; set; }

        [Display(Name = "Obrázek turnaje")]
        public string Obrazek { get; set; }

        [Display(Name = "Webová stránka turnaje")]
        [UIHint("GridForeignKey")]
        public int WebPageId { get; set; }


        [Display(Name = "Turnaj existuje")]
        public bool Existuje { get; set; }

    }
}