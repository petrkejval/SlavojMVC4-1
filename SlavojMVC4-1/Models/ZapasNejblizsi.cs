using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SlavojMVC4_1.Models
{
    public class ZapasNejblizsi
    {
        public int ZapasId { get; set; }

        public string SoutezNazev { get; set; }

        public string KuzelnaNazev { get; set; }

        public bool KuzelnaDomaci { get; set; }

        public System.DateTime ZapasDatumCasOd { get; set; }

        public System.DateTime ZapasDatumCasDo { get; set; }

        public string Druzstvo1Nazev { get; set; }

        public string Druzstvo2Nazev { get; set; }

        public Nullable<System.DateTime> SrazDatumCas { get; set; }

        public string MistoSrazu { get; set; }

        public string Poznamka { get; set; }
        public string Druzstvo1Barva { get; set; }
        public string Druzstvo2Barva { get; set; }
        public virtual Rozhodci Rozhodci { get; set; }


    }
}