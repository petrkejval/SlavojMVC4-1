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

    public class KuzelnaProgramEditable
    {
        [ScaffoldColumn(false)]//nebude nikde zobrazen
        public int KuzelnaProgramId { get; set; }

        [Display(Name = "Název programu")]
        [Required]
        public string ProgramNazev { get; set; }

        [Display(Name = "Kategorie programu")]
        [Required]
        [UIHint("GridForeignKey")]
        public int ProgramKategorieId { get; set; }


        [Display(Name = "Začátek programu (Datuma a čas)")]
        [Required]
        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:d.M.yyyy HH:mm}", ApplyFormatInEditMode = true)]
        public System.DateTime ProgramDatumCasOd { get; set; }

        [Display(Name = "Konec programu (Datuma a čas)")]
        [Required]
        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:d.M.yyyy HH:mm}", ApplyFormatInEditMode = true)]
        public System.DateTime ProgramDatumCasDo { get; set; }

        [Display(Name = "Poznámka")]
        public string Poznamka { get; set; }

        [Display(Name = "Opakovat každý týden")]
        public bool OpakovatKazdyTyden { get; set; }

        [Display(Name = "Opakovat do")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:d.M.yyyy}", ApplyFormatInEditMode = true)]
        public System.DateTime? OpakovatDatumDo { get; set; }

        [Display(Name = "Je program aktivní")]
        public bool JeAktivni { get; set; }
    }
}