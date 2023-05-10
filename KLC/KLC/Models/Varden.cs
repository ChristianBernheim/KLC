namespace KLC.Models
{
    public class Varden
    {
        public string[] Andningsfrekvens = new string[]{ "≤8","", "9-11", "12-20","", "21-24", "≥25" };
        public string[] SyreMattnad1 = new string[] { "≤91", "92-93", "94-95", "≥96", "", "", "" };
        public string[] SyreMattnad2 = new string[] { "≤83", "84-85", "86-87", "88-92", "93-94", "95-96", "≥97" };
        public string[] TillfordSyrgas = new string[] { "", "Ja", "", "Nej", "", "", "" };
        public string[] SystolisktBlodtryck = new string[] { "≤90", "91-100", "101-110", "111-219", "", "", "≥220" };
        public string[] Pulsfrekvens = new string[] { "≤40", "", "41-50", "51-90", "91-100", "111-130", "≥131" };
        public string[] Medvetandegrad = new string[] { "", "", "", "A", "", "", "C V P U" };
        public string[] Temperatur = new string[] { "≤35.0", "", "35,1-36,0", "36,1-38,0", "38,1-39,0", "≥39,1", "" };
    }
}
