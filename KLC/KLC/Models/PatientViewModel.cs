namespace KLC.Models
{
    public class PatientViewModel
    {
        public Patient Patient { get; set; }
        public List<Patient> AllPatients { get; set; }
        public List<MatningNews2> Matningar { get; set; }

        public Varden Varden = new Varden();

        public int Action = 0;
    }
}
