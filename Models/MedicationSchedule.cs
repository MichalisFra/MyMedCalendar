using MyMedCalendar.Models;

public class MedicationSchedule : BaseEntity
{
    public int Id { get; set; } // Auto-generated primary key

    public string PatientId { get; set; } = null!; // Ensure it matches Patient.Id
    public virtual Patient Patient { get; set; } = null!;

    public int DrugId { get; set; }
    public virtual Drug Drug { get; set; } = null!;

    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public int FrequencyPerWeek { get; set; }
    public double Dosage { get; set; }


}

