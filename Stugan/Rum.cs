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

        public List<Spelsak> Saker { get; set; } = new List<Spelsak>();

        public Dictionary<string, Rum> Utgangar { get; set; } = new Dictionary<string, Rum>();

        public Rum(string namn, string beskrivning)
        {
            Namn = namn;
            Beskrivning = beskrivning;
        }

        public void Koppla(string riktning, Rum nastaRum)
        {
            Utgangar[riktning] = nastaRum;
        }
    }
}
