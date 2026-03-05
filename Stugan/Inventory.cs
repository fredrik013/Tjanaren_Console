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
            WriteLine("RYGGSÄCK");
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
                    // Inne i switch (k) i Inventory.cs:

                    case ConsoleKey.U:
                        var sakAttSe = _saker[_markeratIndex];

                        // Vi hämtar den befintliga beskrivningen från objektet
                        string info = string.IsNullOrWhiteSpace(sakAttSe.Beskrivning)
                                      ? $"Det finns inget särskilt att notera om {sakAttSe.Namn}."
                                      : sakAttSe.Beskrivning;

                        // Skriv ut på rad 12 så JAWS läser upp det direkt
                        SetCursorPosition(0, 12);
                        WriteLine(info.PadRight(Console.WindowWidth));

                        // Vi pausar lite så man hinner höra beskrivningen innan man trycker vidare
                        System.Threading.Thread.Sleep(500);
                        break;

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

                    case ConsoleKey.Delete:
                        var sakAttSlappa = _saker[_markeratIndex];

                        // Om det är kläder vi har på oss, ta av dem först så statusen blir rätt
                        if (sakAttSlappa is Klader plagg)
                        {
                            plagg.ArAktiv = false;
                            if (plagg.Typ.ToLower() == "skodon") s.AktivtSkodon = "";
                        }

                        // Flytta från ryggsäck till rummet
                        s.NuvarandeRum.SakerIRummet.Add(sakAttSlappa);
                        _saker.RemoveAt(_markeratIndex);

                        // Bekräftelse till användaren
                        SetCursorPosition(0, 12);
                        WriteLine($"Du lämnade {sakAttSlappa.Namn} i {s.NuvarandeRum.Namn}.".PadRight(Console.WindowWidth));

                        System.Threading.Thread.Sleep(1000); // Paus för JAWS

                        if (_saker.Count == 0)
                        {
                            tittar = false;
                        }
                        else
                        {
                            // Justera index så vi inte hamnar utanför listan
                            if (_markeratIndex >= _saker.Count) _markeratIndex = _saker.Count - 1;

                            // Rita om och fortsätt
                            Visa(s);
                            return;
                        }
                        break;
                }
            }
        }

        // Hjälpmetod för att rita hela skärmen utan att krascha logiken
        private void RitaHelaMenyn()
        {
            Clear();
            WriteLine("RYGGSÄCK");
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
                plagg.ArAktiv = !plagg.ArAktiv; // Växla status (på/av)

                // Vi kollar på typen i gemener så vi inte råkar missa pga stor bokstav
                switch (plagg.Typ.ToLower())
                {
                    case "ute":
                    case "inne":
                        if (inv != null && plagg.ArAktiv)
                        {
                            // Om vi tar på oss något för fötterna, klä av alla andra fotsaker
                            inv.AvaktiveraTyp("ute");
                            inv.AvaktiveraTyp("inne");
                            plagg.ArAktiv = true; // Sätt på just detta plagg igen

                            s.AktivtSkodon = plagg.Typ; // Spara typen (Ute/Inne) hos spelaren
                        }
                        else if (!plagg.ArAktiv)
                        {
                            s.AktivtSkodon = ""; // Vi tog av oss skorna helt
                        }
                        break;

                    case "plagg":
                        // Mössor, vantar etc. behöver ingen extra logik för fötterna
                        break;

                    default:
                        // Om vi glömt sätta en typ, händer inget speciellt
                        break;
                }

                return plagg.ArAktiv ? $"Du tar på dig {plagg.Namn}." : $"Du tar av dig {plagg.Namn}.";
            }

            // Vanliga saker (mat, dryck, nycklar)
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

            // 1. Flytta markören till den raden vi står på (+1 för rubriken)
            SetCursorPosition(0, _markeratIndex + 1);

            var sak = _saker[_markeratIndex];

            // 2. Fixa statussträngen så den matchar det du vill höra
            string status = "";
            if (sak is Klader p)
            {
                status = p.ArAktiv ? " (påtagen)" : " (i ryggsäcken)";
            }

            // 3. Skriv ut raden visuellt med markören ">"
            // PadRight(40) är viktig för att sudda ut gammal text
            Write($"{sak.Namn}{status}".PadRight(40));

            // 4. För att JAWS ska läsa upp statusen korrekt utan Tolk:
            // Vi sätter markören i slutet av raden vi just skrev. 
            // Det tvingar skärmläsaren att fokusera på den nya texten.
            if (skaPrata)
            {
                // Vi behöver inte en extra Write här, piltangenterna och 
                // SetCursorPosition sköter snacket om vi har skrivit ut texten ovan.
            }
        }
    }
}