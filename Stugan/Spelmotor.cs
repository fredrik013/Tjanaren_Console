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

        private void Undersok()
        {
            Tolk.Output($"Du ser dig omkring i {_spelare.NuvarandeRum.Namn}.");
            // Här ska vi snart loopa igenom rummets saker...
        }
    }
}

