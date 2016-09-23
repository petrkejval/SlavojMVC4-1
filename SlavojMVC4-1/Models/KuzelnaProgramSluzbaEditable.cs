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

    public partial class KuzelnaProgramSluzbaEditable
    {
        [ScaffoldColumn(false)]//nebude nikde zobrazen
        [Key]
        public int KuzelnaProgramSluzbaId { get; set; }

        [ScaffoldColumn(false)]//nebude nikde zobrazen
        [Key]
        public int KuzelnaProgramId { get; set; }

        [Display(Name = "Člen")]
        [UIHint("GridForeignKey")]
        [Range(1, int.MaxValue, ErrorMessage = "Člen musí být vyplněn.")]
        public int ClenId { get; set; }

        [Display(Name = "Služba od (Datuma a čas)")]
        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:d.M.yyyy HH:mm}", ApplyFormatInEditMode = true)]
        public Nullable<System.DateTime> SluzbaDatumCasOd { get; set; }

        [Display(Name = "Služba do (Datuma a čas)")]
        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:d.M.yyyy HH:mm}", ApplyFormatInEditMode = true)]
        public Nullable<System.DateTime> SluzbaDatumCasDo { get; set; }
        
    }
}
