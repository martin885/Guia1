using SQLite.Net.Attributes;
using SQLiteNetExtensions.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Guia1.Models
{
   public class ProductoPrincipal
    {
        [PrimaryKey, AutoIncrement]
        public int ProductoPrincipalId { get; set; }

        public string Nombre { get; set; }
        

        [OneToMany(CascadeOperations = CascadeOperation.All)] 
        public  List<ProductoA> Productos { get; set; }

        public override int GetHashCode()
        {
            return ProductoPrincipalId;
        }
    }
}
