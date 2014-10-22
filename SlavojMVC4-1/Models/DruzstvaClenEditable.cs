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

    public class DruzstvaClenEditable
    {
        [ScaffoldColumn(false)]//nebude nikde zobrazen
        [Key]
        public int DruzstvoClenId { get; set; }

        [ScaffoldColumn(false)]//nebude nikde zobrazen
        [Required]
        [Display(Name = "Družstvo")]
        public int DruzstvoId { get; set; }

        [Display(Name = "Člen")]
        [UIHint("GridForeignKey"), Required]
        [Range(1, int.MaxValue, ErrorMessage = "Pole musí být vyplněno.")]
        //[RegularExpression(@"/^[1-9][0-9]*$/", ErrorMessage = "Pole musí být vyplněno")]
        public int ClenId { get; set; }
    }
}