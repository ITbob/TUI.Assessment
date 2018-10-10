using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TUI.Distance.Calculator.Source;
using TUI.Gasoline.Calculator.Source;
using TUI.Model.Shared.Source;
using TUI.Places.Source;
using TUI.TimeZone.Source;
using TUI.Transportations.Air.Source;

namespace TUI.Transportations.Air
{
    public class Flight : ITrip, IIdContainer
    {
        [Key]
        public Int32 Id { get; set; }

        [DataType(DataType.DateTime), Required]
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd HH:mm}", ApplyFormatInEditMode = true)]
        public DateTime StartDate { get; set; } = DateTime.MinValue;

        public Location Departure => DepartureAirport.Location;
        public Location Arrival => ArrivalAirport.Location;

        [Required]
        [ForeignKey("Plane")]
        public Int32 PlaneId { get; set; }
        public virtual Plane Plane {get;set;}

        [Required]
        [ForeignKey("DepartureAirport")]
        public Int32 DepartureId { get; set; }

        [Display(Name = "Departure")]
        public virtual Airport DepartureAirport { get; set; }

        [ForeignKey("ArrivalAirport")]
        public Int32 ArrivalId { get; set; }

        [Display(Name = "Arrival")]
        public virtual Airport ArrivalAirport { get; set; }

        public Flight()
        {

        }

        public Flight(Airport departure, Airport arrival)
        {
            this.DepartureAirport = departure;
            this.ArrivalAirport = arrival;
        }

        public Double GetDistanceKm()
        {
            return this.Departure.GetDistance(this.Arrival);
        }

        public Double GetConsumption()
        {
            return this.Plane.Kind.GetTotalConsumption(this.Plane.GasKind, this.GetDistanceKm());
        }

        public Double GetPrice()
        {
            return 
                Math.Round(
                this.Plane.GasKind.Price * this.Plane.Kind.GetTotalConsumption(this.Plane.GasKind, this.GetDistanceKm()),2);

        }

        //to put in a static extension method???
        public TimeSpan GetDuration()
        {
            var result = this.GetDistanceKm()
                / this.Plane.Kind.KmsHourSpeedAverage;
            return TimeSpan.FromHours(result);
        }

        public Boolean GetUtcDifference(ref TimeSpan duration)
        {
            var utc = TimeZoneHelper.GetDiff(
                this.ArrivalAirport.Location,
                this.DepartureAirport.Location,
                this.StartDate);

            if (utc.IsReceived)
            {
                duration = utc.Offset;
            }

            return utc.IsReceived;
        }

        public String Description => $"From {this.DepartureAirport} to {this.ArrivalAirport} ";

        public override string ToString()
        {
            return $"[{this.Id}] From {this.DepartureAirport} to {this.ArrivalAirport} {this.GetDistanceKm()}Km {this.StartDate}";
        }

        public DateTime GetEndDateWithoutUtc()
        {
            return this.StartDate.Add(this.GetDuration());
        }

        public DateTime GetEndDate()
        {
            var offset = TimeSpan.Zero;
            var isPassed = this.GetUtcDifference(ref offset);

            if (isPassed)
            {
                return this.StartDate.Add(this.GetDuration().Add(offset));
            }
            else
            {
                return this.StartDate.Add(this.GetDuration());
            }
        }
    }
}
