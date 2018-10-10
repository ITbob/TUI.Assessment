using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TUI.Model.Shared.Source;

namespace TUI.Places.Source
{
    public class City: IIdContainer
    {
        [Key]
        public Int32 Id { get; set; }
        [Required]
        public String Name { get; set; }

        [Required]
        [ForeignKey("Location")]
        public Int32 LocationId { get; set; }
        [Display(Name = "Description")]
        public virtual Location Location { get; set; }

        [NotMapped]
        public Double Longitude
        {
            get
            {
                if (this.Location == null)
                {
                    this.Location = new Location();
                }
                return this.Location.Longitude;
            }
            set
            {
                if (this.Location == null)
                {
                    this.Location = new Location();
                }
                this.Location.Longitude = value;
            }
        }

        [NotMapped]
        public Double Latitude
        {
            get
            {
                if (this.Location == null)
                {
                    this.Location = new Location();
                }
                return this.Location.Latitude;
            }
            set
            {
                if (this.Location == null)
                {
                    this.Location = new Location();
                }
                this.Location.Latitude = value;
            }
        }

        public override string ToString()
        {
            return $"[{this.Id}] {this.Name} {this.Location.ToString()}";
        }
    }
}
