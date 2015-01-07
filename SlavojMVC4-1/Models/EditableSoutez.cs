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

    public class EditableSoutez
    {

        [ScaffoldColumn(false)]//nebude nikde zobrazen
        public int SoutezId { get; set; }

        [Required]
        [Display(Name = "Název soutěže")]
        public string Nazev { get; set; }
        
        [Display(Name = "Kategorie soutěže")]
        [UIHint("GridForeignKey"), Required]
        public int KategorieSoutezeId { get; set; }

        [Display(Name = "Minimální počet hráčů")]
        [Range(1, int.MaxValue, ErrorMessage = "Minimální počet hráčů musí být vyplněn.")]
        public int MinPocetHracu { get; set; }

        [Display(Name = "Disciplína")]
        [Required]
        [UIHint("GridForeignKey")]
        [Range(1, int.MaxValue, ErrorMessage = "Disciplína musí být vyplněna.")]
        public int DisciplinaId { get; set; }

        [Range(1,9)]
        [Display(Name = "Počet nutných drah")]
        public int PocetNutnychDrah { get; set; }
    }
}