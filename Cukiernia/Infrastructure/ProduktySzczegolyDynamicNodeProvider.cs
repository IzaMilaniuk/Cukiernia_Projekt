using Cukiernia.DAL;
using Cukiernia.Models;
using MvcSiteMapProvider;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Cukiernia.Infrastructure
{
    public class ProduktySzczegolyDynamicNodeProvider :DynamicNodeProviderBase
    {
        private ProduktyContext db = new ProduktyContext();
        public override IEnumerable<DynamicNode> GetDynamicNodeCollection(ISiteMapNode nodee)
        {
            var returnValue =new  List<DynamicNode>();
            foreach (Produkt produkt in db.Produkty)
            {
                DynamicNode node = new DynamicNode();
                node.Title = produkt.TytulProdukt;
                node.Key = "Produkt_" + produkt.ProduktId;
                node.ParentKey = "Kategoria_" + produkt.KategoriaId;
                node.RouteValues.Add("id", produkt.ProduktId);
                returnValue.Add(node);
            }
            return returnValue;
        }
    }
}