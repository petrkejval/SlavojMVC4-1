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

    public class DruzstvoEditable
    {
        [ScaffoldColumn(false)]//nebude nikde zobrazen
        public int DruzstvoId { get; set; }

        [Required]
        [Display(Name = "Název družstva")]
        public string Nazev { get; set; }

        [Display(Name = "Písmeno")]
        public string Pismeno { get; set; }

        [Display(Name = "Soutěž")]
        [UIHint("GridForeignKey")]
        [Range(1, int.MaxValue, ErrorMessage = "Soutěž musí být vyplněna.")]
        public int SoutezId { get; set; }

        [Display(Name = "Vedoucí družstva")]
        [UIHint("GridForeignKey")]
        [Range(1, int.MaxValue, ErrorMessage = "Vedoucí musí být vyplněn.")]
        public int VedouciId { get; set; }

        [Display(Name = "Trenér družstva")]
        [UIHint("GridForeignKey")]
        public int TrenerId { get; set; }

        [Display(Name = "Fotografie družstva")]
        public string Image { get; set; }

        [Display(Name = "Webová stránka družstva")]
        [UIHint("GridForeignKey")]
        public int WebPageId { get; set; }

        [Required]
        [Display(Name = "Družstvo existuje")]
        public bool Existuje { get; set; }

    }
}