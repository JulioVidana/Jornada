using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Finiquito.Entidades
{

    public class EntSolicitudes
    {
        public EntSolicitudes()
        {
        }

        public int clave_empleado { get; set; }
        public string nombre { get; set; }
        public string area { get; set; }
        public string Estatus { get; set; } 
        public string idValida { get; set; }
        public string Concepto { get; set; }
    }


}