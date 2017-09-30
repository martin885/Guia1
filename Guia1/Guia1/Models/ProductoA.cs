using SQLite.Net.Attributes;
using SQLiteNetExtensions.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Guia1.Models
{
   public  class ProductoA
    {
        [PrimaryKey,AutoIncrement]
        public int ProductoId { get; set; }

        //Id del producto principal
        [ForeignKey(typeof(ProductoPrincipal))]
        public int ProductoPrincipalId { get; set; }

        public string Nombres { get; set; }
        public string Cantidad { get; set; }
        public string Clasificacion { get; set; }

        [ManyToOne]
        public  ProductoPrincipal ProductoPrincipal { get; set; }

        public override int GetHashCode()
        {
            return ProductoId;
        }
    }
}
