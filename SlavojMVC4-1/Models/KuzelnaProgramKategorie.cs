//------------------------------------------------------------------------------
// <auto-generated>
//    Tento kód byl generován ze šablony.
//
//    Ruční změny tohoto souboru mohou způsobit neočekávané chování v aplikaci.
//    Ruční změny tohoto souboru budou přepsány, pokud bude kód vygenerován znovu.
// </auto-generated>
//------------------------------------------------------------------------------

namespace SlavojMVC4_1.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class KuzelnaProgramKategorie
    {
        public KuzelnaProgramKategorie()
        {
            this.KuzelnaProgramy = new HashSet<KuzelnaProgram>();
        }
    
        public int KuzelnaProgramKategorieId { get; set; }
        public string Nazev { get; set; }
        public string Barva { get; set; }
    
        public virtual ICollection<KuzelnaProgram> KuzelnaProgramy { get; set; }
    }
}
