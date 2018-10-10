using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TUI.Gasoline.Source;
using TUI.Model.Shared.Source;
using TUI.Transportations.Source;

namespace TUI.Transportations.Air.Source
{
    public class PlaneKind: ITransportationDeviceKind, IIdContainer
    {
        [Key]
        public Int32 Id { get; set; }

        [Required]
        [StringLength(100)]
        public String Name { get; set; }

        //average info
        public Double KmsHourSpeedAverage { get; set; }
        public Double Weight { get; set; }
        public Double EngineFactor { get; set; }

        //starting info
        public Double RequiredStartingDistance { get; set; }
        public Double StartingEffort { get; set; }

        public String Description => this.ToString();

        public override string ToString()
        {
            return  $"[{this.Id}] {Name}";
        }
    }
}
