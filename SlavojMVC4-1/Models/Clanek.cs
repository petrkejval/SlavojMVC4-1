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
    
    public partial class Clanek
    {
        public int ClanekId { get; set; }
        public int UserId { get; set; }
        public System.DateTime DatumVytvoreni { get; set; }
        public System.DateTime DatumZmeny { get; set; }
        public int KategorieId { get; set; }
    
        public virtual Kategorie Kategorie { get; set; }
        public virtual UserProfile UserProfile { get; set; }
    }
}
