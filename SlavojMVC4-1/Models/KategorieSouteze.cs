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
    
    public partial class KategorieSouteze
    {
        public KategorieSouteze()
        {
            this.Souteze = new HashSet<Soutez>();
        }
    
        public int KategorieSoutezeId { get; set; }
        public string Nazev { get; set; }
    
        public virtual ICollection<Soutez> Souteze { get; set; }
    }
}