namespace KLC_API.Models
{
    public class MatningNews2
    {
        public int Id { get; set; }
        //Foreign key
        public int PatientId { get; set; }
        //Andnings frekvens
        public int? Andningsfrekvens { get; set; }
        //Syre Mättnad
        public int? SyreMattnad { get; set; }
        //Tillförd syrgas
        public int? TillfordSyrgas { get; set; }
        //Systoliskt blodtryck
        public int? SystolisktBlodtryck { get; set; }
        public int? Pulsfrekvens { get; set; }
        public int? Medvetandegrad { get; set; }
        public int? Temperatur { get; set; }
        //Tid för mätning
        public DateTime TidForMatning { get; set; }
    }
}
