using System.Collections.Generic;

namespace _1oT.Models
{
    public class ManageSims
    {
        public int found { get; set; }
        public int offset { get; set; }
        public int total { get; set; }
        public List<SimCard> sims { get; set; }
    }
}

