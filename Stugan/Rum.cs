using Stugan.Core;
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

        public Position? Plats { get; set; }

        public Dictionary<string, Rum> Utgangar { get; set; } = new Dictionary<string, Rum>();

        public Dictionary<string, string> UtgangsMeddelanden { get; set; } = new Dictionary<string, string>();

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

        public string? HamtaUtgangsMeddelande(string riktning)
        {
            if (UtgangsMeddelanden.TryGetValue(riktning, out string? meddelande))
            {
                return meddelande;
            }
            return null;
        }

        public void Koppla(string riktning, Rum nastaRum)
        {
            Utgangar[riktning] = nastaRum;
        }

        private Rum? HamtaUtgang(string riktning)
        {
            return Utgangar.TryGetValue(riktning, out var rum) ? rum : null;
        }

        private bool VisaExitMeddelande(string riktning)
        {
            // Vi hämtar meddelandet från oss själva (detta rum)
            // Vi skickar in riktningen till vår egen Hamta-metod som sköter ToUpper
            string? meddelande = HamtaUtgangsMeddelande(riktning);

            if (!string.IsNullOrEmpty(meddelande))
            {
                Clear();
                WriteLine($"\n{meddelande}");

                // JAWS-vänlig paus
                System.Threading.Thread.Sleep(2000);
                return true;
            }

            return false;
        }

        public void VisaBeskrivning(string? riktning = null)
        {
            // Vi kollar om vi ska visa en stämningstext först.
            // VisaExitMeddelande sköter Clear() internt om den hittar text.
            bool visadeExit = false;
            if (!string.IsNullOrEmpty(riktning))
            {
                visadeExit = VisaExitMeddelande(riktning);
            }

            // Om vi INTE visade en exit-text, så måste vi rensa skärmen här
            if (!visadeExit)
            {
                Clear();
            }

            // HÄR ÄR DIN ORIGINALKOD - HELT OFÖRÄNDRAD:
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
            VisaSakerIRummet();
        }

        // 2. Hitta saker som ligger framme och går att ta
        // Vi filtrerar bort de som är gömda (ArGomd == true)
        private void VisaSakerIRummet()
        {
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
            VisaSakerIRummet();
        }

        public virtual void UndersokRum(Spelare spelare)
        {
            // Som standard händer ingenting speciellt.
        }

        public virtual bool KanGaIn(Spelare s)
        {
            return true; // Standard: Alla får komma in!
        }

        public virtual void ReageraPaHandling(string handling)
        {
            // Som standard händer absolut ingenting här.
        }
    }
}
