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

    public class RekordEditable
    {
        [ScaffoldColumn(false)]//nebude nikde zobrazen
        public int RekordId { get; set; }

        [Display(Name = "Název hráče/družstva")]
        [Required]
        public string Nazev { get; set; }

        [Display(Name = "Disciplína")]
        [Required]
        [UIHint("GridForeignKey")]
        [Range(1, int.MaxValue, ErrorMessage = "Disciplína musí být vyplněna.")]
        public int DisciplinaId { get; set; }

        [Display(Name = "Počet hráčů")]
        [Required]
        public int PocetHracu { get; set; }

        [Display(Name = "Kategorie rekordů")]
        [Required]
        [UIHint("GridForeignKey")]
        [Range(1, int.MaxValue, ErrorMessage = "Kategorie rekordů musí být vyplněna.")]
        public int RekordyKategorieId { get; set; }

        [Display(Name = "Je registrován")]
        [Required]
        public bool JeRegistrovan { get; set; }

        [Display(Name = "Nához hráče/družstva")]
        [Required]
        public int Nahoz { get; set; }

        [Display(Name = "Datum náhozu")]
        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:d.M.yyyy}", ApplyFormatInEditMode = true)]
        public System.DateTime DatumNahozu { get; set; }

        [Display(Name = "Bližší popis")]
        public string Popis { get; set; }
    }
}