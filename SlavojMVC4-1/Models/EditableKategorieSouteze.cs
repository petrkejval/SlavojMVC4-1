using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

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

    public class EditableKategorieSouteze
    {
        [Required]
        [Display(Name = "Kategorie soutěže Id")]
        [Key]
        public int KategorieSoutezeId { get; set; }

        [Required]
        [Display(Name = "Název soutěže")]
        public string Nazev { get; set; }
    }
}