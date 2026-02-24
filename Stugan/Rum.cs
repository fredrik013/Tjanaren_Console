using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stugan
{
    public class Rum
    {
        public string Namn { get; set; }

        public string Beskrivning { get; set; }

        public bool HarUndersokts { get; set; } = false; 

        public List<Spelsak> Saker { get; set; } = new List<Spelsak>();

        public Dictionary<string, Rum> Utgangar { get; set; } = new Dictionary<string, Rum>();

        public Rum(string namn, string beskrivning, bool harundersokts = false )
        {
            Namn = namn;
            Beskrivning = beskrivning;
            HarUndersokts = harundersokts;
        }

        public void Koppla(string riktning, Rum nastaRum)
        {
            Utgangar[riktning] = nastaRum;
        }
    }
}
