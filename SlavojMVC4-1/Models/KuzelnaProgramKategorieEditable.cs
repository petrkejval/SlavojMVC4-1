namespace SlavojMVC4_1.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    public class KuzelnaProgramKategorieEditable
    {
        [ScaffoldColumn(false)]//nebude nikde zobrazen
        public int KuzelnaProgramKategorieId { get; set; }

        [Display(Name = "Název kategorie")]
        [Required]
        public string Nazev { get; set; }

        [Display(Name = "Barva kategorie")]
        [Required]
        public string Barva { get; set; }
    }
}