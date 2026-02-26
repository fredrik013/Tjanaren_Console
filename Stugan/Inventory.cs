using System;
using static System.Console;
using DavyKager;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        // Hjälpmetod för att se till att man bara har ett par skor åt gången [cite: 2026-02-24]
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
            Tolk.Output($"Ryggsäck. {_saker.Count} föremål.");
            PresenteraValdSak();

            while (iMenyn)
            {
                var tangent = ReadKey(true).Key;

                if (tangent == ConsoleKey.DownArrow && _markeratIndex < _saker.Count - 1)
                {
                    _markeratIndex++;
                    PresenteraValdSak();
                }
                else if (tangent == ConsoleKey.UpArrow && _markeratIndex > 0)
                {
                    _markeratIndex--;
                    PresenteraValdSak();
                }
                else if (tangent == ConsoleKey.Enter)
                {
                    var valdSak = _saker[_markeratIndex];

                    // Om det är kläder, stäng av andra plagg av samma typ först
                    if (valdSak is Klader klader)
                    {
                        AvaktiveraTyp(klader.Typ);
                    }

                    valdSak.Anvand(spelare);
                    iMenyn = false; // Stäng efter användning
                }
                else if (tangent == ConsoleKey.Escape || tangent == ConsoleKey.I)
                {
                    Tolk.Output("Stänger ryggsäcken.");
                    iMenyn = false;
                }
            }
        }

        private void PresenteraValdSak()
        {
            var sak = _saker[_markeratIndex];
            string status = sak.ArAktiv ? " (påtagen)" : "";
            Tolk.Output($"{sak.Namn} {status}");

            // För den som ser skärmen
            Clear();
            WriteLine($"--- RYGGSÄCK ---");
            WriteLine(sak.Namn + status);
            WriteLine("\n(Pilar för att bläddra, Enter för att använda)");
        }
    }
}