namespace SlavojMVC4_1.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    public class ZapasEditable
    {
        [ScaffoldColumn(false)]//nebude nikde zobrazen
        public int ZapasId { get; set; }

        [Display(Name = "Soutěž")]
        [UIHint("GridForeignKey")]
        [Range(1, int.MaxValue, ErrorMessage = "Soutěž musí být vyplněna.")]
        public int SoutezId { get; set; }

        [Display(Name = "Kuželna")]
        [Required]
        public string KuzelnaNazev { get; set; }

        [Display(Name = "Domácí kuželna")]
        [Required]
        public bool KuzelnaDomaci { get; set; }

        [Display(Name = "Začátek zápasu (Datuma a čas")]
        [Required]
        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:d.M.yyyy HH:mm}", ApplyFormatInEditMode = true)]
        public System.DateTime ZapasDatumCasOd { get; set; }

        [Display(Name = "Konec zápasu (Datuma a čas")]
        [Required]
        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:d.M.yyyy HH:mm}", ApplyFormatInEditMode = true)]
        public System.DateTime ZapasDatumCasDo { get; set; }

        [Display(Name = "Název družstva 1")]
        [Required]
        public string Druzstvo1Nazev { get; set; }

        [Display(Name = "Název družstva 2")]
        [Required]
        public string Druzstvo2Nazev { get; set; }

        [Display(Name = "Datum a čas srazu")]
        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:d.M.yyyy HH:mm}", ApplyFormatInEditMode = true)]
        public Nullable<System.DateTime> SrazDatumCas { get; set; }

        [Display(Name = "Místo srazu")]
        public string MistoSrazu { get; set; }

        [Display(Name = "Poznámka")]
        public string Poznamka { get; set; }

        [Display(Name = "Rozhodčí")]
        [UIHint("GridForeignKey")]
        [Range(0, int.MaxValue, ErrorMessage = "Musí být vyplněno")]
        public int RozhodciId { get; set; }

    }
}