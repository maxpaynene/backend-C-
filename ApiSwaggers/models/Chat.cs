using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ApiSwaggers.Models
{
    public class Chat
    {
        [Key]
        public int id_chat { get; set; }
        public string chat_id_bot_father { get; set; }
        public DateTime fecha_ingreso { get; set; }
        public int id_correo { get; set; }
    }
}
