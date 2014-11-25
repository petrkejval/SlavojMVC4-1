using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SlavojMVC4_1.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Web.Mvc;

    public partial class Clanek
    {
        [Display(Name = "Předmět")]
        [Required]
        public string Predmet { get; set; }

        [AllowHtml]
        [Required]
        [UIHint("EditorClanek")]
        public string Text { get; set; }

    }
}
