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
    
    public partial class Turnaj
    {
        public int TurnajId { get; set; }
        public string Nazev { get; set; }
        public int ReditelTurnajeId { get; set; }
        public System.DateTime DatumOd { get; set; }
        public System.DateTime DatumDo { get; set; }
        public string Obrazek { get; set; }
        public bool Existuje { get; set; }
        public int WebPageId { get; set; }
    
        public virtual Clen ReditelTurnaje { get; set; }
        public virtual WebPage WebPage { get; set; }
    }
}
