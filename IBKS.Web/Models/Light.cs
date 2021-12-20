using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace IBKS.Web.Models
{
    public class Light
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int? Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public int TypeId { get; set; }
        public virtual Type Type { get; set; }

        public int RoomId { get; set; }
        public virtual Room Room { get; set; }

    }
}
