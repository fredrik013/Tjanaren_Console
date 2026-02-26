using System;
using DavyKager;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stugan
{
    public class Klader : Spelsak
    {
        public bool SkyddarMotVatten { get; set; }

        public string Typ { get; set; } // t.ex. "Huvud", "Kropp", "Fot"

        public Klader(string namn, string beskrivning, string typ, bool skyddarmotvatten = false)
            : base(namn, beskrivning, true)
        {
            Typ = typ;
            SkyddarMotVatten = skyddarmotvatten;
        }

        public override void Anvand(Spelare s)
        {
            // Här kan vi senare lägga till logik som kollar 'Typ' 
            // så att man inte kan ha både tofflor och stövlar samtidigt.
            this.ArAktiv = true;
            Tolk.Output($"Du tar på dig {this.Namn}.");

            if (SkyddarMotVatten)
            {
                Tolk.Output("Den här kommer hålla dig torr!");
            }
        }
    }
        }
