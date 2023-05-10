namespace KLC_API.Models
{
    public class PatientViewModel
    {
        public Patient Patient { get; set; }
        public List<MatningNews2> Matningar { get; set; }

        public Varden Varden = new Varden();
    }
}
