using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ApiSwaggers.Models
{
    [Table("cliente")]
    public class Cliente
    {

        [Key]
        public int id_cliente { get; set; }
        public string cliente { get; set; }
        public DateTime fecha_ingreso { get; set; }

    }
}
