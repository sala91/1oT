namespace _1oT.Models
{
    public class SimCard
    {
        public string iccid { get; set; }
        public string group { get; set; }
        public int data_limit { get; set; }
        public string type { get; set; }
        public string form { get; set; }
        public string name { get; set; }
        public SimCardStatus status { get; set; }
        public char Symbol { get; set; }
    }
}
