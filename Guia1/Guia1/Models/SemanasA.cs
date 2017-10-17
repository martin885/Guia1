using SQLite.Net.Attributes;
using SQLiteNetExtensions.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Guia1.Models
{
    public class SemanasA
    {
        [PrimaryKey, AutoIncrement]
        public int SemanasId { get; set; }

        public string SemanasNumero { get; set; }
        public string ReqBruto { get; set; }
        public string InventarioInicial { get; set; }
        public string ReqNeto { get; set; }
        public string LibOrden { get; set; }
        public string InvFinal { get; set; }
        public string TiempoFab { get; set; }
        public string SemanasDiasCalculo { get; set; }
        public string DateSelectedLibOrden { get; set; }
        public bool IsVisibleDate { get; set; }
        public bool IsEnabled { get; set; }
        public string FechaDias { get; set; }


        [ForeignKey(typeof(ProductoA))]
        public int ProductoId { get; set; }


        [ManyToOne]
        public ProductoA ProductoA { get; set; }

        public override int GetHashCode()
        {
            return SemanasId;
        }

    }
}
