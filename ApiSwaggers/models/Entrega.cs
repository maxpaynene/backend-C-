using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ApiSwaggers.Models
{
    public class Entrega
    {
        [Key]
        public int id_entrega { get; set; }
        public string area { get; set; }
        public string jefe_servicio { get; set; }
        public string cliente { get; set; }
        public string usuario_solicitante { get; set; }
        public string admin_configuracion { get; set; }
        public string version { get; set; }
        public string numero_requerimiento { get; set; }
        public string nombre_entregable { get; set; }
        public string destinatario { get; set; }
        public string descripcion { get; set; }
        public DateTime fecha_ingreso { get; set; }
    }
}
