using static System.Console;

namespace Stugan
{
    public class Klader : Spelsak
    {
        public bool SkyddarMotVatten { get; set; }

        public string Typ { get; set; } // t.ex. "Huvud", "Kropp", "Fot"

        public Klader(string namn, string beskrivning, string typ, bool skyddarmotvatten, bool kanplockasupp, bool argomd, bool araktiv)
: base(namn, beskrivning, kanplockasupp, argomd)
        {
            Namn = namn;
            Beskrivning = beskrivning;
            Typ = typ;
            SkyddarMotVatten = skyddarmotvatten;
            KanPlockasUpp = kanplockasupp;
            ArGomd = argomd;
            ArAktiv = false;
        }

        public override void Anvand(Spelare s)
        {
            // Här kan vi senare lägga till logik som kollar 'Typ' 
            // så att man inte kan ha både tofflor och stövlar samtidigt.
            this.ArAktiv = true;
            WriteLine($"Du tar på dig {this.Namn}.");

            if (SkyddarMotVatten)
            {
                WriteLine("Den här kommer hålla dig torr!");
            }
        }
    }
}
