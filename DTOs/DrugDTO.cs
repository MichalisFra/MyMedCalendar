using System.ComponentModel.DataAnnotations;

namespace MyMedCalendar.DTOs
{
    public class DrugDTO
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Company { get; set; } = string.Empty;
        public int? PackageSize { get; set; }
    }
}

