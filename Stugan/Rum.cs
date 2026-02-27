using DavyKager;

namespace Stugan
{
    public class Rum
    {
        public string Namn { get; set; }

        public string Beskrivning { get; set; }

        public bool HarUndersokts { get; set; } = false;

        public List<Spelsak> SakerIRummet { get; set; } = new List<Spelsak>();

        public Dictionary<string, Rum> Utgangar { get; set; } = new Dictionary<string, Rum>();

        public Rum Norr { get => HamtaUtgang("Norr")!; set => Koppla("Norr", value); }

        public Rum Soder { get => HamtaUtgang("Soder")!; set => Koppla("Soder", value); }

        public Rum Oster { get => HamtaUtgang("Oster")!; set => Koppla("Oster", value); }

        public Rum Vaster { get => HamtaUtgang("Vaster")!; set => Koppla("Vaster", value); }

        public Rum(string namn, string beskrivning, bool harundersokts = false)
        {
            Namn = namn;
            Beskrivning = beskrivning;
            HarUndersokts = harundersokts;
        }

        public void Koppla(string riktning, Rum nastaRum)
        {
            Utgangar[riktning] = nastaRum;
        }

        private Rum? HamtaUtgang(string riktning)
        {
            if (Utgangar.ContainsKey(riktning))
            {
                return Utgangar.ContainsKey(riktning) ? Utgangar[riktning] : null;
            }
            return null;
        }

        public void VisaBeskrivning()
        {
            Tolk.Output($"{Namn}. {Beskrivning}");
        }
    }
}
