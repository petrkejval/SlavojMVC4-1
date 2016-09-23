using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SlavojMVC4_1.Models
{
    public class KuzelnaProgramZapas
    {
        public KuzelnaProgramZapas()
        {
            this.KuzelnaProgramSluzby = new HashSet<KuzelnaProgramSluzba>();
        }

        public int KuzelnaProgramId { get; set; }
        public string ProgramNazev { get; set; }
        public string Druzstvo1Nazev { get; set; }
        public string Druzstvo2Nazev { get; set; }
        public string Druzstvo1Barva { get; set; }
        public string Druzstvo2Barva { get; set; }
        public System.DateTime ProgramDatumCasOd { get; set; }
        public System.DateTime ProgramDatumCasDo { get; set; }
        public string Poznamka { get; set; }
        public bool OpakovatKazdyTyden { get; set; }
        public Nullable<System.DateTime> OpakovatDatumDo { get; set; }
        public bool JeAktivni { get; set; }
        public bool JeProgram { get; set; }

        public virtual ICollection<KuzelnaProgramSluzba> KuzelnaProgramSluzby { get; set; }
    }
}