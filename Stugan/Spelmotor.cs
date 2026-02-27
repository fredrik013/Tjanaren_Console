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
            Tolk.Output("Välkommen till Stugan. Spelet har startat.");

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
                case ConsoleKey.Escape:
                    Tolk.Output("Avslutar spelet och sparar framstegen i minnet.");
                    _isrunning = false;
                    break;

                default:
                    // Här kan vi lägga in ett litet ljud eller meddelande 
                    // om man trycker på en knapp som inte gör något
                    break;
            }
        }

        private void FlyttaSpelare(string riktning)
        {
            var nuvarandeRum = _spelare.NuvarandeRum;

            // Vi kollar om det finns en utgång i den valda riktningen
            if (nuvarandeRum.Utgangar.ContainsKey(riktning))
            {
                // Byt rum!
                _spelare.NuvarandeRum = nuvarandeRum.Utgangar[riktning];

                Tolk.Output($"Du går åt {riktning}.");

                // Presentera det nya rummet för JAWS
                _spelare.NuvarandeRum.VisaBeskrivning();
            }
            else
            {
                Tolk.Output("Där är det stopp, du kan inte gå åt det hållet.");
            }
        }


        private void Undersok()
        {
            Tolk.Output($"Du ser dig omkring i {_spelare.NuvarandeRum.Namn}.");
            // Här ska vi snart loopa igenom rummets saker...
        }
    }
}

