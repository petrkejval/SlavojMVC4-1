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
    
    public partial class RekordyKategorie
    {
        public RekordyKategorie()
        {
            this.Rekordy = new HashSet<Rekord>();
        }
    
        public int RekordyKategorieId { get; set; }
        public string Nazev { get; set; }
    
        public virtual ICollection<Rekord> Rekordy { get; set; }
    }
}