using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stugan
{
    public class Spelsak
    {
        public string Namn { get; set; }

        public string Beskrivning { get; set; }

        public bool KanPlockasUpp { get; set; }

        public Spelsak(string namn, string beskrivning, bool kanplockasupp = true)
        {
            Namn = namn;
            Beskrivning = beskrivning;
            KanPlockasUpp = kanplockasupp;
                    }
    }
}
