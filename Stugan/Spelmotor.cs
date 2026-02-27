using DavyKager;
using static System.Console;

namespace Stugan
{
    public class Spelmotor
    {
        private Spelare _spelare;

        private bool _isrunning = true;

        public Spelmotor(Spelare spelare)
        {
            _spelare = spelare;
        }

        public void Starta()
        {
            // Första hälsningen till JAWS
            WriteLine("Välkommen till Stugan!");

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
                    // En förberedelse för "Undersök"
                    Undersok();
                    break;
                    case ConsoleKey.T:
                    TaUppSak();
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
                // 2. Byt rum!
                _spelare.NuvarandeRum = _spelare.NuvarandeRum.Utgangar[riktning];

                // Skapa ett snyggt namn för utskriften
                string snyggRiktning = riktning switch
                {
                    "Norr" => "norr",
                    "Soder" => "söder",
                    "Oster" => "öster",
                    "Vaster" => "väster",
                    _ => riktning.ToLower()
                };

                WriteLine($"\nDu går åt {snyggRiktning}.");

                // 4. Visa det nya rummet automatiskt
                _spelare.NuvarandeRum.VisaBeskrivning();
            }
            else
            {
                // Om det inte finns en dörr där
                WriteLine("Där är det stopp, du kan inte gå åt det hållet.");
            }
        }

        private void Undersok()
        {
            var rum = _spelare.NuvarandeRum;
            WriteLine($"\nDu ser dig noga omkring i {rum.Namn}...");

            switch (rum.Namn)
            {
                case "Hallen":
                    HanteraHallen();
                    break;

                case "Köket":
                    WriteLine("Du kollar på köksbänken. Det ligger lite smulor från veckans lunchbröd här.");
                    break;

                case "Källaren":
                    WriteLine("Här är det mörkt och lite fuktigt på golvet. Tur om man har skor på sig.");
                    break;

                default:
                    WriteLine("Du hittar inget särskilt när du undersöker rummet.");
                    break;
            }
        }

        private void HanteraHallen()
        {
            var rum = _spelare.NuvarandeRum;
            var tofflor = rum.SakerIRummet.FirstOrDefault(s => s.Namn.ToLower() == "tofflor");

            if (tofflor != null && tofflor.ArGomd)
            {
                tofflor.ArGomd = false;
                WriteLine("Du rotar i skohyllan och hittar ett par plasttofflor!");
                WriteLine("Perfekta för fuktiga utrymmen.");
            }
            else
            {
                WriteLine("Skohyllan är tom sånär som på lite grus.");
            }
        }

        private void TaUppSak()
        {
            var rum = _spelare.NuvarandeRum;

            // Vi använder samma logik som i din Rum.cs för att hitta vad som faktiskt syns
            var sakerAttTa = rum.SakerIRummet.Where(s => s.KanPlockasUpp && !s.ArGomd).ToList();

            if (sakerAttTa.Count == 0)
            {
                WriteLine("\nDet finns inget här som du kan ta upp.");
            }
            else if (sakerAttTa.Count == 1)
            {
                var sak = sakerAttTa[0];

                // Flytta saken
                _spelare.Ryggsack.LaggTill(sak);
                rum.SakerIRummet.Remove(sak);

                WriteLine($"\nDu plockar upp: {sak.Namn}.");
            }
            else
            {
                // Om det ligger både öl och bröd framme (lyx!)
                WriteLine("\nDet finns flera saker här. Vilken vill du ta?");
                for (int i = 0; i < sakerAttTa.Count; i++)
                {
                    WriteLine($"{i + 1}. {sakerAttTa[i].Namn}");
                }

                // Här kan vi senare lägga till en ReadLine för att välja, 
                // men vi tar den första så länge så du får testa funktionen.
                var sak = sakerAttTa[0];
                _spelare.Ryggsack.LaggTill(sak);
                rum.SakerIRummet.Remove(sak);
                WriteLine($"\n(Du tog {sak.Namn})");
            }
        }
    }
}