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
    
    public partial class Trener
    {
        public Trener()
        {
            this.Druzstvas = new HashSet<Druzstvo>();
        }
    
        public int ClenId { get; set; }
        public string CisloRegistrace { get; set; }
        public string Trida { get; set; }
        public System.DateTime PlatnaDo { get; set; }
    
        public virtual Clen Cleni { get; set; }
        public virtual ICollection<Druzstvo> Druzstvas { get; set; }
    }
}
