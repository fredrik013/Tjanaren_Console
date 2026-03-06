namespace Stugan
{
    public class Klader : Spelsak
    {
        public string Typ { get; set; }

        public string MeddelandePa { get; set; } // Ny: Eget meddelande vid PÅ

        public string MeddelandeAv { get; set; } // Ny: Eget meddelande vid AV

        public bool SkyddarMotVatten { get; set; }

        public Klader(string namn, string beskrivning, string typ, bool skyddarmotvatten,
                              bool kanplockasupp, bool argomd, string meddelandePa = "", string meddelandeAv = "")
                    : base(namn, beskrivning, kanplockasupp, argomd)
        {
            Typ = typ;
            SkyddarMotVatten = skyddarmotvatten;
            MeddelandePa = string.IsNullOrEmpty(meddelandePa) ? $"Du tar på dig {namn}." : meddelandePa;
            MeddelandeAv = string.IsNullOrEmpty(meddelandeAv) ? $"Du tar av dig {namn}." : meddelandeAv;
        }

        public override string Anvand(Spelare s)
        {
            ArAktiv = !ArAktiv;

            // Använd de personliga meddelandena istället för generisk logik
            string meddelande = ArAktiv ? MeddelandePa : MeddelandeAv;

            if (ArAktiv && SkyddarMotVatten && !meddelande.Contains("torr"))
            {
                meddelande += " De här kommer hålla dig torr!";
            }

            // Här triggar vi även rummets reaktion om det finns en sådan
            s.NuvarandeRum.ReageraPaHandling(ArAktiv ? $"pa_{Namn.ToLower()}" : $"av_{Namn.ToLower()}");

            return meddelande;
        }
    }
}
