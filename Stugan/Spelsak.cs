using System;
using DavyKager;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stugan
{
    public abstract class Spelsak
    {
        public string Namn { get; set; }

        public string Beskrivning { get; set; }

        public bool KanPlockasUpp { get; set; }

        public bool ArAktiv { get; set; }

        public Spelsak(string namn, string beskrivning, bool kanplockasupp = true)
        {
            Namn = namn;
            Beskrivning = beskrivning;
            KanPlockasUpp = kanplockasupp;
            ArAktiv = false;
                    }

        public abstract void Anvand(Spelare s);
    }
}
