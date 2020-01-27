using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ApiSwaggers.Models
{
    public class Clave
    {
        [Key]
        public int id_clave { get; set; }
        public string clave { get; set; }
        public DateTime fecha_ingreso { get; set; }
        public int id_correo { get; set; }
    }
}
