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

    public class UserClenEditable
    {
        [ScaffoldColumn(false)]//nebude nikde zobrazen
        public int UserClenId { get; set; }

        [Display(Name = "Uživatel")]
        [UIHint("GridForeignKey")]
        [Range(1, int.MaxValue, ErrorMessage = "Uživate musí být vyplněn.")]
        public int UserId { get; set; }

        [Display(Name = "Člen")]
        [UIHint("GridForeignKey")]
        [Range(1, int.MaxValue, ErrorMessage = "Člen musí být vyplněn.")]
        public int ClenId { get; set; }
    }
}