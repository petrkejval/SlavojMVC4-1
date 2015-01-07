using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SlavojMVC4_1.Models
{
    public class RekordGroup
    {
        public bool JeRegistrovan { get; set; }
        public string RegistrovanNazev { get; set; }
        public int RekordyKategorieId { get; set; }
        public string RekordyKategorieNazev { get; set; }
        public int DisciplinaId { get; set; }
        public int DisciplinaPocetHodu { get; set; }
        public string DisciplinaNazev { get; set; }
        public string DisciplinaKategorieNazev { get; set; }
        public int PocetHracu { get; set; }

    }
}