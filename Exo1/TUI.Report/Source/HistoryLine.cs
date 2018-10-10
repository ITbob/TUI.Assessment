using System;
using System.ComponentModel.DataAnnotations;
using TUI.Model.Shared.Source;

namespace TUI.Report.Source
{
    public class HistoryLine : IIdContainer
    {
        [Key]
        public Int32 Id { get; set; }
        [Required]
        public String DateType { get; set; }
        [Required]
        public OperationType Operation { get; set; }
        [Required]
        public DateTime Datetime { get; set; }
        [Required]
        public String Description { get; set; }
    }
}
