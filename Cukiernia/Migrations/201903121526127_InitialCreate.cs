namespace Cukiernia.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Kategoria",
                c => new
                    {
                        KategoriaId = c.Int(nullable: false, identity: true),
                        NazwaKategorii = c.String(nullable: false, maxLength: 100),
                        OpisKategorii = c.String(nullable: false),
                        NazwaPlikuIkony = c.String(),
                    })
                .PrimaryKey(t => t.KategoriaId);
            
            CreateTable(
                "dbo.Produkt",
                c => new
                    {
                        ProduktId = c.Int(nullable: false, identity: true),
                        KategoriaId = c.Int(nullable: false),
                        TytulProdukt = c.String(nullable: false, maxLength: 100),
                        AutorProdukt = c.String(nullable: false, maxLength: 100),
                        DataDodania = c.DateTime(nullable: false),
                        NazwaPlikuObrazka = c.String(maxLength: 100),
                        OpisProdukt = c.String(),
                        CenaProdukt = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Bestseller = c.Boolean(nullable: false),
						
                        Ukryty = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.ProduktId)
                .ForeignKey("dbo.Kategoria", t => t.KategoriaId, cascadeDelete: true)
                .Index(t => t.KategoriaId);
            
            CreateTable(
                "dbo.PozycjaZamowienia",
                c => new
                    {
                        PozycjaZamowieniaId = c.Int(nullable: false, identity: true),
                        ZamowienieID = c.Int(nullable: false),
                        ProduktId = c.Int(nullable: false),
                        Ilosc = c.Int(nullable: false),
                        CenaZakupu = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.PozycjaZamowieniaId)
                .ForeignKey("dbo.Produkt", t => t.ProduktId, cascadeDelete: true)
                .ForeignKey("dbo.Zamowienie", t => t.ZamowienieID, cascadeDelete: true)
                .Index(t => t.ZamowienieID)
                .Index(t => t.ProduktId);
            
            CreateTable(
                "dbo.Zamowienie",
                c => new
                    {
                        ZamowienieID = c.Int(nullable: false, identity: true),
                        Imie = c.String(nullable: false, maxLength: 50),
                        Nazwisko = c.String(nullable: false, maxLength: 50),
                        Ulica = c.String(nullable: false, maxLength: 100),
                        Miasto = c.String(nullable: false, maxLength: 100),
                        KodPocztowy = c.String(nullable: false, maxLength: 6),
                        Telefon = c.String(),
                        Email = c.String(),
                        Komentarz = c.String(),
                        DataDodania = c.DateTime(nullable: false),
                        StanZamowienia = c.Int(nullable: false),
                        WartoscZamowienia = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.ZamowienieID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.PozycjaZamowienia", "ZamowienieID", "dbo.Zamowienie");
            DropForeignKey("dbo.PozycjaZamowienia", "ProduktId", "dbo.Produkt");
            DropForeignKey("dbo.Produkt", "KategoriaId", "dbo.Kategoria");
            DropIndex("dbo.PozycjaZamowienia", new[] { "ProduktId" });
            DropIndex("dbo.PozycjaZamowienia", new[] { "ZamowienieID" });
            DropIndex("dbo.Produkt", new[] { "KategoriaId" });
            DropTable("dbo.Zamowienie");
            DropTable("dbo.PozycjaZamowienia");
            DropTable("dbo.Produkt");
            DropTable("dbo.Kategoria");
        }
    }
}
