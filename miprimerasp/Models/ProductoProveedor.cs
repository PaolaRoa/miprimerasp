using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using miprimerasp.Models;

namespace miprimerasp.Models
{
    public class ProductoProveedor
    {
        public string producto { get; set; }

        public string proveedor { get; set; }

        public string telefono { get; set; }

        public Nullable<int> precioUnitario { get; set; }

        public string descripcion { get; set; }
    }
}