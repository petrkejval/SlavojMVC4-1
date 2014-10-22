﻿//------------------------------------------------------------------------------
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
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class SlavojDBContainer : DbContext
    {
        public SlavojDBContainer()
            : base("name=SlavojDBContainer")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public DbSet<UserProfile> UserProfiles { get; set; }
        public DbSet<webpages_Membership> webpages_Membership { get; set; }
        public DbSet<webpages_OAuthMembership> webpages_OAuthMembership { get; set; }
        public DbSet<webpages_Roles> webpages_Roles { get; set; }
        public DbSet<Kategorie> Kategories { get; set; }
        public DbSet<Clanek> Clanky { get; set; }
        public DbSet<Clen> Cleni { get; set; }
        public DbSet<Adresa> Adresy { get; set; }
        public DbSet<CleniInRole> CleniInRoles { get; set; }
        public DbSet<CleniRole> CleniRoles { get; set; }
        public DbSet<Kontakt> Kontakty { get; set; }
        public DbSet<Kuzelna> Kuzelnas { get; set; }
        public DbSet<Registrace> Registraces { get; set; }
        public DbSet<Rozhodci> Rozhodcis { get; set; }
        public DbSet<KategorieSouteze> KategorieSoutezi { get; set; }
        public DbSet<Soutez> Souteze { get; set; }
        public DbSet<Trener> Treneri { get; set; }
        public DbSet<Druzstvo> Druzstva { get; set; }
        public DbSet<DruzstvaCleni> DruzstvoClen { get; set; }
    }
}
