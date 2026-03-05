using DavyKager;
using static System.Console;

namespace Stugan
{
    public class Inventory
    {
        private List<Spelsak> _saker = new List<Spelsak>();
        private int _markeratIndex = 0;

        public void LaggTill(Spelsak sak) => _saker.Add(sak);
        public List<Spelsak> GetAllaSaker() => _saker;

        public void AvaktiveraTyp(string typ)
        {
            foreach (var sak in _saker)
            {
                if (sak is Klader klader && klader.Typ == typ)
                    klader.ArAktiv = false;
            }
        }

        public void Visa(Spelare s)
        {
            if (_saker.Count == 0)
            {
                WriteLine("Ryggsäcken är tom."); // Standard WriteLine
                return;
            }

            bool tittar = true;
            _markeratIndex = 0;

            Clear();
            WriteLine("--- RYGGSÄCK ---");
            for (int i = 0; i < _saker.Count; i++)
            {
                WriteLine($"   {_saker[i].Namn}");
            }

            UppmärksammaRad();

            while (tittar)
            {
                var k = ReadKey(true).Key;

                switch (k)
                {
                    case ConsoleKey.DownArrow:
                        if (_markeratIndex < _saker.Count - 1)
                        {
                            _markeratIndex++;
                            UppmärksammaRad();
                        }
                        break;

                    case ConsoleKey.UpArrow:
                        if (_markeratIndex > 0)
                        {
                            _markeratIndex--;
                            UppmärksammaRad();
                        }
                        break;

                    case ConsoleKey.Enter:
                        var valdSak = _saker[_markeratIndex];
                        string svar = HanteraUtrustning(valdSak, s, this);

                        // VIKTIGT: Skriv ut svaret på rad 12 så det hamnar under listan
                        SetCursorPosition(0, 12);
                        WriteLine(svar.PadRight(Console.WindowWidth));

                        // Om saken försvann (ätit/druckit)
                        if (!_saker.Contains(valdSak))
                        {
                            // Vi pausar en sekund så JAWS hinner läsa WriteLine 
                            // innan vi stänger eller ritar om.
                            System.Threading.Thread.Sleep(1000);

                            if (_saker.Count == 0) tittar = false;
                            else Visa(s); // Starta om för att rensa listan
                            return;
                        }

                        UppmärksammaRad();
                        break;

                    case ConsoleKey.Escape:
                    case ConsoleKey.I:
                        tittar = false;
                        break;
                }
            }
        }

        // Hjälpmetod för att rita hela skärmen utan att krascha logiken
        private void RitaHelaMenyn()
        {
            Clear();
            WriteLine("--- RYGGSÄCK ---");
            for (int i = 0; i < _saker.Count; i++)
            {
                string markor = (i == _markeratIndex) ? "> " : "  ";
                string status = "";
                if (_saker[i] is Klader p) status = p.ArAktiv ? " (påtagen)" : " (i ryggsäcken)";
                WriteLine($"{markor}{_saker[i].Namn}{status}");
            }
        }

        public static string HanteraUtrustning(Spelsak sak, Spelare s, Inventory inv)
        {
            if (sak is Klader plagg)
            {
                plagg.ArAktiv = !plagg.ArAktiv;
                if (plagg.Typ.ToLower() == "skodon" && inv != null)
                {
                    if (plagg.ArAktiv)
                    {
                        inv.AvaktiveraTyp("skodon");
                        plagg.ArAktiv = true;
                    }
                    s.AktivtSkodon = plagg.ArAktiv ? plagg.Namn : "";
                }
                return plagg.ArAktiv ? $"Du tar på dig {plagg.Namn}." : $"Du tar av dig {plagg.Namn}.";
            }

            string meddelande = sak.Anvand(s);
            if (sak.ForsvinnerVidAnvandning && inv != null)
            {
                inv.GetAllaSaker().Remove(sak);
            }
            return meddelande;
        }

        private void UppmärksammaRad(bool skaPrata = true)
        {
            if (_saker.Count == 0) return;
            SetCursorPosition(0, _markeratIndex + 1);
            var sak = _saker[_markeratIndex];
            string status = (sak is Klader p && p.ArAktiv) ? " påtagen" : "";

            // Skriv ut raden igen för att visa markören visuellt
            Write($" {sak.Namn}{status}".PadRight(40));

            if (skaPrata) Tolk.Output($"{sak.Namn}{status}");
        }
    }
}