
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

    public class KuzelnaEditable
    {
        [ScaffoldColumn(false)]//nebude nikde zobrazen
        public int KuzelnaId { get; set; }

        [Display(Name = "Ulice")]
        [Required]
        public string Ulice { get; set; }

        [Display(Name = "Číslo popisné")]
        [Required]
        public int CisloPopisne { get; set; }

        [Display(Name = "Obec")]
        [Required]
        public string Obec { get; set; }

        [Display(Name = "Psč")]
        [Required]
        public int Psc { get; set; }

        [Display(Name = "Kolaudace platna do")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:d.M.yyyy}", ApplyFormatInEditMode = true)]
        [Required]
        public System.DateTime KolaudacePlatnaDo { get; set; }

        [Display(Name = "Obrázek")]
        public string Image { get; set; }

        [Display(Name = "Odkaz na naší kuželnu na www.kuzelky.cz")]
        public string LinkKuzelkyCz { get; set; }

        [Display(Name = "Odkaz na mapu")]
        public string Mapa { get; set; }

        [Display(Name = "Odkaz na mapu Street view")]
        public string StreeView { get; set; }

        [Display(Name = "GPS souřadnice")]
        public string GPS { get; set; }

        [Display(Name = "Webová stránka Kuželny")]
        [UIHint("GridForeignKey")]
        public int WebPageId { get; set; }

    }
}