﻿namespace MyMedCalendar.DTOs
{
    public class CreateDrugDTO
    {
        public string Name { get; set; } = string.Empty;
        public string Company { get; set; } = string.Empty;
        public int? PackageSize { get; set; }
    }
}
