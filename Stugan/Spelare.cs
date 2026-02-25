using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stugan
{
    public class Spelare
    {
        public Rum NuvarandeRum { get; set; }

        public List<Spelsak> Ryggsack { get; private set; }

        public string AktivtSkodon { get; set; } // T.ex. "Ytterskor", "Tofflor", "Stövlar"

        public bool ArSkadad { get; set; }

        public Spelare(Rum startRum)
        {
            NuvarandeRum = startRum;
            Ryggsack = new List<Spelsak>();
            AktivtSkodon = "Ytterskor";
            ArSkadad = false;
        }
    }
}