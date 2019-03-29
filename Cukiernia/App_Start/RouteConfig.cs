using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Cukiernia
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "ProduktySzczegoly",
                url: "produkt-{id}.html",
                defaults: new { controller = "Produkty", action = "Szczegoly" });


            //trasa dla kategorii 
            routes.MapRoute(
                name: "ProduktyList",
                url: "Kategoria/{nazwaKategori}",
                defaults: new { controller = "Produkty", action = "Lista" });

            //trasa dla stron statycznych
            routes.MapRoute(
                name: "StronyStatyczne", //nazwa trasy
                url: "strona/{nazwa}.html",  //strony/ <- mozna to zmieniac dowolnie
                defaults: new { controller = "Home", action = "StronyStatyczne" });
            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
