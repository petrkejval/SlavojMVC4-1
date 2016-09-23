namespace SlavojMVC4_1.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    public class ZapasBarvaEditable
    {
        [ScaffoldColumn(false)]//nebude nikde zobrazen
        public int ZapasBarvaId { get; set; }

        [Display(Name = "Název družstva")]
        [Required]
        public string DruzstvoNazev { get; set; }

        [Display(Name = "Barva družstva")]
        [Required]
        public string Barva { get; set; }
    }
}