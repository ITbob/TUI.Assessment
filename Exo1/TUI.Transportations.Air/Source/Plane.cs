using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TUI.Gasoline.Source;
using TUI.Model.Shared.Source;

namespace TUI.Transportations.Air.Source
{
    public class Plane: IIdContainer
    {
        [Key]
        public Int32 Id { get; set; }

        [Required]
        [ForeignKey("Kind")]
        public Int32 KindId { get; set; }
        public PlaneKind Kind { get; set; }

        [Required]
        [ForeignKey("GasKind")]
        public Int32 GasId { get; set; }
        public GasKind GasKind { get; set; }

        public String Description => this.ToString();

        public override string ToString()
        {
            return $"[{this.Id}] ({Kind.Name})";
        }

        //maintenance statistic, number of flights...
    }
}
