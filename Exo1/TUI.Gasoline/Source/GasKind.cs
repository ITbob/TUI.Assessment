using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TUI.Gasoline.Source
{
    public class GasKind:IGasKind
    {
        [Key]
        public Int32 Id { get; set; }

        [Required]
        public String Name { get; set; }
        [Required]
        public Double GasConsumptionCoefficient { get; set; }

        [Required]
        public Double Price { get; set; } //not that good, price change relentlessly

        public GasKind(String name, Double coef, Double price)
        {
            this.Name = name;
            this.GasConsumptionCoefficient = coef;
            this.Price = price;
        }

        public GasKind()
        {

        }
    }
}
