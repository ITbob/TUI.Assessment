using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TUI.Model.Shared.Source;

namespace TUI.Login.source
{
    public class User:IIdContainer
    {
        public int Id { get; set; }

        [Required]
        public String Name { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public String Password { get; set; }

        public override string ToString()
        {
            return $"[{this.Id}] {this.Name}";
        }
    }
}
