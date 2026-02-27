using DavyKager;
using static System.Console;

namespace Stugan
{
    public class Inventory
    {
        private List<Spelsak> _saker = new List<Spelsak>();
        private int _markeratIndex = 0;

        public void LaggTill(Spelsak sak)
        {
            _saker.Add(sak);
        }

        public void AvaktiveraTyp(string typ)
        {
            foreach (var sak in _saker)
            {
                if (sak is Klader klader && klader.Typ == typ)
                {
                    klader.ArAktiv = false;
                }
            }
        }

        public void Visa(Spelare s)
        {
            var ryggsack = _saker;
            if (ryggsack.Count == 0)
            {
                Tolk.Output("Ryggsäcken är tom.");
                return;
            }

            _markeratIndex = 0;
            bool tittar = true;

            // 1. Rita upp listan en gång (ingen Clear i loopen sen!)
            Clear();
            WriteLine("--- RYGGSÄCK ---");
            for (int i = 0; i < ryggsack.Count; i++)
            {
                WriteLine($"   {ryggsack[i].Namn}");
            }

            // 2. Sätt initialt fokus
            UppmärksammaRad();

            while (tittar)
            {
                var k = ReadKey(true).Key;

                switch (k)
                {
                    case ConsoleKey.DownArrow:
                        if (_markeratIndex < ryggsack.Count - 1)
                        {
                            _markeratIndex++;
                            UppmärksammaRad();
                        }
                        else Tolk.Output("Slut på listan.");
                        break;

                    case ConsoleKey.UpArrow:
                        if (_markeratIndex > 0)
                        {
                            _markeratIndex--;
                            UppmärksammaRad();
                        }
                        else Tolk.Output("Början på listan.");
                        break;

                    case ConsoleKey.Enter:
                        var valdSak = ryggsack[_markeratIndex];
                        if (valdSak.Namn.ToLower().Contains("tofflor"))
                        {
                            s.AktivtSkodon = "Tofflor";
                            Tolk.Output("Du tar på dig tofflorna.");
                            tittar = false;
                            System.Threading.Thread.Sleep(500);
                        }
                        else Tolk.Output($"Kan inte använda {valdSak.Namn}.");
                        break;

                    case ConsoleKey.Escape:
                    case ConsoleKey.I:
                        Tolk.Output("Stänger ryggsäcken.");
                        tittar = false;
                        break;

                    default:
                        // Gör ingenting vid andra tangenttryck
                        break;
                }
            }
        }

        private void UppmärksammaRad()
        {
            // Flytta markören till rätt rad (hoppa över rubriken på rad 0)
            SetCursorPosition(0, _markeratIndex + 1);

            string namn = _saker[_markeratIndex].Namn;

            // Skriv över raden för att trigga JAWS och visa pilen
            Write($"{namn}   ");

            // Prata
            Tolk.Output(namn);
        }

        private void PresenteraValdSak()
        {
            // Vi flyttar markören till toppen istället för att rensa helt, 
            // det är snällare mot skärmläsare.
            SetCursorPosition(0, 1);

            for (int i = 0; i < _saker.Count; i++)
            {
                string markor = (i == _markeratIndex) ? "-> " : "   ";
                string status = _saker[i].ArAktiv ? " [PÅTAGEN]" : "";
                WriteLine($"{markor}{_saker[i].Namn}{status}                   "); // Mellanslag för att sudda gammal text
            }
        }
    }
}
