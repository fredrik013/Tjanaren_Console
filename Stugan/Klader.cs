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
            string meddelande = ArAktiv ? MeddelandePa : MeddelandeAv;

            // Vi skickar med Typ (t.ex. "ute" eller "inne") istället för Namn
            string handlingStrang = (ArAktiv ? "pa_" : "av_") + Typ.ToLower();
            s.NuvarandeRum.ReageraPaHandling(handlingStrang);

            return meddelande;
        }
    }
}
