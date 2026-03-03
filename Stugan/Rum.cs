using static System.Console;
namespace Stugan
{
    public class Rum
    {
        public string Namn { get; set; }

        public string VisaNamn { get; set; }

        public string Beskrivning { get; set; }

        public bool HarBesokts { get; set; }

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
            VisaNamn = namn;
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
            WriteLine($"{VisaNamn}");
            // 1. Grundbeskrivningen av rummet
            if (!HarBesokts)
            {
                WriteLine($"{Beskrivning}");
                HarBesokts = true;
            }
            else
            {
                WriteLine($"Du är i {VisaNamn}.");
            }

            // 2. Hitta saker som ligger framme och går att ta
            // Vi filtrerar bort de som är gömda (ArGomd == true)
            var synligaSaker = SakerIRummet.Where(s => s.KanPlockasUpp && !s.ArGomd).ToList();

            if (synligaSaker.Count > 0)
            {
                WriteLine("Här ser du också:");
                foreach (var sak in synligaSaker)
                {
                    WriteLine(sak.Namn);
                }
            }
        }

        public void LasLangBeskrivning()
        {
            Clear();
            WriteLine($"{VisaNamn}");
            WriteLine(Beskrivning);
        }
    }
}
