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

    public class VysledekEditable
    {
        [ScaffoldColumn(false)]//nebude nikde zobrazen
        public int VysledekId { get; set; }

        [Display(Name = "Ročník")]
        [Required]
        [UIHint("GridForeignKey")]
        [Range(1, int.MaxValue, ErrorMessage = "Ročník musí být vyplněn.")]
        public int RocnikId { get; set; }


        [Display(Name = "Soutěž")]
        [Required]
        [UIHint("GridForeignKey")]
        [Range(1, int.MaxValue, ErrorMessage = "Soutěž musí být vyplněne.")]
        public int SoutezId { get; set; }

        [Display(Name = "Odkaz na obrázek aktuální tabulky soutěže")]
        public string Tabulka { get; set; }

        [Display(Name = "Webová stránka soutěže")]
        [UIHint("GridForeignKey")]
        public int WebPageIId { get; set; }

        [Display(Name = "Rozpis soutěže")]
        public string Rozpis { get; set; }

        [Display(Name = "Rozlosování soutěže")]
        public string Rozlosovani { get; set; }
        
        [Display(Name = "Soupiska podzim")]
        public string SoupiskaPodzim { get; set; }
        
        [Display(Name = "Soupiska jaro")]
        public string SoupiskaJaro { get; set; }

    }
}