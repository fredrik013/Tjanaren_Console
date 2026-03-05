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

        public override string Anvand(Spelare s)
        {
            // Vi växlar status: på blir av, av blir på.
            ArAktiv = !ArAktiv;

            string statusText = ArAktiv ? "tar på dig" : "tar av dig";
            string meddelande = $"Du {statusText} {Namn}.";

            if (ArAktiv && SkyddarMotVatten)
            {
                meddelande += " Den här kommer hålla dig torr!";
            }

            return meddelande;
        }
    }
}
