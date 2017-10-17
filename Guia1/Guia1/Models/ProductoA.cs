using Guia1.Behaviors;
using SQLite.Net.Attributes;
using SQLiteNetExtensions.Attributes;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

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
        //public string Clasificacion { get; set; }
        //public string InvDisponible { get; set; }
        public string TiempoFabricacion { get; set; }
        public string SemanasDias { get; set; }
        public string Dependencia { get; set; }
        public bool IsEnabled { get; set; }
        public bool IsEnabledNombre { get; set; }
        public bool IsEnabledDependencia { get; set; }
        public string InventarioInicial { get; set; }
        
        [ManyToOne]
        public  ProductoPrincipal ProductoPrincipal { get; set; }

        [OneToMany(CascadeOperations = CascadeOperation.All)]
        public List<SemanasA> Semanas { get; set; }

        public override int GetHashCode()
        {
            return ProductoId;
        }
    }
}
