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

        public void Visa(Spelare spelare)
        {
            if (_saker.Count == 0)
            {
                Tolk.Output("Ryggsäcken är tom.");
                return;
            }

            _markeratIndex = 0;
            bool iMenyn = true;

            // Första presentationen
            Clear();
            WriteLine("--- RYGGSÄCK ---");
            PresenteraValdSak();

            while (iMenyn)
            {
                var tangent = ReadKey(true).Key;

                switch (tangent)
                {
                    case ConsoleKey.DownArrow:
                        if (_markeratIndex < _saker.Count - 1)
                        {
                            _markeratIndex++;
                            // VIKTIGT: Prata först, rita sen!
                            Tolk.Output(_saker[_markeratIndex].Namn);
                            PresenteraValdSak();
                        }
                        break;

                    case ConsoleKey.UpArrow:
                        if (_markeratIndex > 0)
                        {
                            _markeratIndex--;
                            Tolk.Output(_saker[_markeratIndex].Namn);
                            PresenteraValdSak();
                        }
                        break;

                    case ConsoleKey.Enter:
                        // ... (din enter-logik)
                        iMenyn = false;
                        break;

                    case ConsoleKey.Escape:
                    case ConsoleKey.I:
                        iMenyn = false;
                        break;
                }
            }
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
