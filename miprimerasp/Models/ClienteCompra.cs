using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace miprimerasp.Models
{
    public class ClienteCompra
    {
        public string NombreCliente { get; set; }

        public Nullable<int> TotalCompra { get; set; }

        public Nullable<DateTime> FechaCompra { get; set; }

        public string NombreUsuario { get; set; }
    }
}