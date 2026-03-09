using Stugan.Core;
using static System.Console;

namespace Stugan
{
    public class Spelmotor
    {
        private Spelare _spelare;

        private WorldMap _worldMap;

        private bool _isrunning = true;

        public Spelmotor(Spelare spelare, WorldMap worldMap)
        {
            _spelare = spelare;
            _worldMap = worldMap;
        }

        public void Starta()
        {
            // Första hälsningen till JAWS
            WriteLine("Välkommen till Stugan!");
            WriteLine("Du står utanför den gamla träbyggnaden.");
            WriteLine("Du har din ryggsäck på ryggen och dina boots är ordentligt snörade på fötterna.");

            // Visa rummet man startar i
            _spelare.NuvarandeRum.VisaBeskrivning();

            while (_isrunning)
            {
                // Här väntar vi på inmatning
                if (KeyAvailable)
                {
                    var tangent = ReadKey(true).Key;
                    HanteraInmatning(tangent);
                }
            }
        }

        private void HanteraInmatning(ConsoleKey tangent)
        {
            switch (tangent)
            {
                case ConsoleKey.I:
                    // Öppna din nya fina ryggsäck
                    _spelare.Ryggsack.Visa(_spelare);
                    // När vi kommer tillbaka, påminn om rummet
                    _spelare.NuvarandeRum.VisaBeskrivning();
                    break;

                case ConsoleKey.U:
                    WriteLine($"\nDu ser dig noga omkring i {_spelare.NuvarandeRum.Namn}...");
                    _spelare.NuvarandeRum.UndersokRum(_spelare);
                    break;
                case ConsoleKey.T:
                    TaUppSak();
                    break;
                case ConsoleKey.B:
                    _spelare.NuvarandeRum.LasLangBeskrivning();
                    break;
                case ConsoleKey.UpArrow:
                    FlyttaSpelare("Norr");
                    break;
                case ConsoleKey.DownArrow:
                    FlyttaSpelare("Soder");
                    break;
                case ConsoleKey.RightArrow:
                    FlyttaSpelare("Oster");
                    break;
                case ConsoleKey.LeftArrow:
                    FlyttaSpelare("Vaster");
                    break;
                default:
                    // Här kan vi lägga in ett litet ljud eller meddelande 
                    // om man trycker på en knapp som inte gör något
                    break;
            }
        }

        private void FlyttaSpelare(string riktning)
        {

            // 1. Kolla om det rum spelaren står i har en utgång åt det här hållet
            if (_spelare.NuvarandeRum.Utgangar.ContainsKey(riktning))
            {
                var nastaRum = _spelare.NuvarandeRum.Utgangar[riktning];
                if (nastaRum.KanGaIn(_spelare))
                {
                    _spelare.NuvarandeRum = nastaRum;
                    Clear();
                    System.Threading.Thread.Sleep(100); // JAWS-paus
                    _spelare.NuvarandeRum.VisaBeskrivning();
                }
                // Om KanGaIn returnerar false, gör vi ingenting – 
                // rummet har redan skrivit ut sitt felmeddelande.
            }

            else
            {
                // Om det inte finns en dörr där
                WriteLine("Där är det stopp, du kan inte gå åt det hållet.");
            }
        }

        private void TaUppSak()
        {
            var rum = _spelare.NuvarandeRum;
            var sakerAttTa = rum.SakerIRummet.Where(s => s.KanPlockasUpp && !s.ArGomd).ToList();

            if (sakerAttTa.Count == 0)
            {
                WriteLine("\nDet finns inget här som du kan ta upp.");
                return;
            }

            if (sakerAttTa.Count == 1)
            {
                var sak = sakerAttTa[0];
                _spelare.Ryggsack.LaggTill(sak);
                rum.SakerIRummet.Remove(sak);
                WriteLine($"\nDu plockar upp: {sak.Namn}.");
                System.Threading.Thread.Sleep(800);
            }
            else
            {
                WriteLine("\nDet finns flera saker här. Vilken vill du ta? (Tryck på siffran)");
                for (int i = 0; i < sakerAttTa.Count; i++)
                {
                    WriteLine($"{i + 1}. {sakerAttTa[i].Namn}");
                }

                // Här väntar vi på en siffertangent
                var knapp = ReadKey(true);

                // Vi gör om char-tecknet till en siffra (t.ex. '1' blir int 1)
                if (int.TryParse(knapp.KeyChar.ToString(), out int val) && val > 0 && val <= sakerAttTa.Count)
                {
                    var sak = sakerAttTa[val - 1]; // -1 eftersom listan börjar på 0
                    _spelare.Ryggsack.LaggTill(sak);
                    rum.SakerIRummet.Remove(sak);

                    WriteLine($"\nDu valde att ta: {sak.Namn}.");
                    System.Threading.Thread.Sleep(1000); // Paus för JAWS
                }
                else
                {
                    WriteLine("\nOgiltigt val, du plockade inte upp något.");
                    System.Threading.Thread.Sleep(800);
                }
            }
        }
    }
}
