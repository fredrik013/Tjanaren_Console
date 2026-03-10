using static System.Console;

namespace Stugan
{
    public class Inventory
    {
        private List<Spelsak> _saker = new List<Spelsak>();
        private int _markeratIndex = 0;

        public void LaggTill(Spelsak sak) => _saker.Add(sak);
        public List<Spelsak> GetAllaSaker() => _saker;

        public void AvaktiveraTyp(Bekladnadstyp typ)
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

            UppmarksammaRad();

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
                            UppmarksammaRad();
                        }
                        break;

                    case ConsoleKey.UpArrow:
                        if (_markeratIndex > 0)
                        {
                            _markeratIndex--;
                            UppmarksammaRad();
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

                        UppmarksammaRad();
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
                            if (plagg.Typ == Bekladnadstyp.Ute || plagg.Typ == Bekladnadstyp.Inne || plagg.Typ == Bekladnadstyp.Skydd)
                                s.AktivtSkodon = "";
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
            // 1. Hantera Kläder (Skodon etc)
            if (sak is Klader plagg)
            {
                string meddelandeFrånPlagget = plagg.Anvand(s);

                switch (plagg.Typ)
                {
                    case Bekladnadstyp.Ute:
                    case Bekladnadstyp.Inne:
                        if (inv != null && plagg.ArAktiv)
                        {
                            // Klä av andra skor om vi tar på oss nya
                            foreach (var annanSak in inv.GetAllaSaker())
                            {
                                if (annanSak is Klader k && k != plagg && (k.Typ == Bekladnadstyp.Ute || k.Typ == Bekladnadstyp.Inne))
                                {
                                    k.ArAktiv = false;
                                }
                            }
                            s.AktivtSkodon = plagg.Typ.ToString();
                        }
                        else if (!plagg.ArAktiv)
                        {
                            s.AktivtSkodon = "";
                        }
                        break;

                    case Bekladnadstyp.Skydd:
                        // Samma logik som för Ute och Inne
                        if (inv != null && plagg.ArAktiv)
                        {
                            foreach (var annanSak in inv.GetAllaSaker())
                            {
                                if (annanSak is Klader k && k != plagg &&
                                   (k.Typ == Bekladnadstyp.Ute || k.Typ == Bekladnadstyp.Inne || k.Typ == Bekladnadstyp.Skydd))
                                {
                                    k.ArAktiv = false;
                                }
                            }
                            s.AktivtSkodon = plagg.Typ.ToString();
                        }
                        else if (!plagg.ArAktiv)
                        {
                            s.AktivtSkodon = "";
                        }
                        break;
                }
                return meddelandeFrånPlagget;
            }

            // 2. Hantera Redskap (Spade, Rörtång etc)
            if (sak is Redskap redskap)
            {
                // Anropar redskapets egna logik (som vi ska fixa härnäst)
                string svar = redskap.Anvand(s);

                // Om vi just aktiverade (tog fram) ett redskap, avaktivera alla andra redskap
                if (redskap.ArAktiv && inv != null)
                {
                    foreach (var r in inv.GetAllaSaker().OfType<Redskap>())
                    {
                        if (r != redskap)
                        {
                            r.ArAktiv = false;
                        }
                    }
                }
                return svar;
            }

            // 3. Fallback för vanliga saker (Mat, nycklar etc)
            return sak.Anvand(s);
        }

        private void UppmarksammaRad(bool skaPrata = true)
        {
            if (_saker.Count == 0) return;

            // 1. Först rensar vi gamla markörer på alla rader (visuellt)
            for (int i = 0; i < _saker.Count; i++)
            {
                SetCursorPosition(0, i + 1);
                Write("  "); // Skriv två mellanslag för att sudda ut ev. gammal "> "
            }

            // 2. Flytta markören till den raden vi står på (+1 för rubriken)
            SetCursorPosition(0, _markeratIndex + 1);

            var sak = _saker[_markeratIndex];

            // 3. Fixa statussträngen så den matchar det du vill höra
            string status = "";
            if (sak is Klader p)
            {
                status = p.ArAktiv ? " (påtagen)" : " (i ryggsäcken)";
            }
            else if (sak is Redskap r)
            {
                status = r.ArAktiv ? " (i handen)" : " (i ryggsäcken)";
            }

            // 4. Skriv ut raden visuellt med markören "> "
            // Vi skriver "> " först och sen namnet + status.
            Write($" {sak.Namn}{status}".PadRight(45));

            // 5. För att JAWS ska läsa upp statusen korrekt:
            // Vi ställer markören i slutet av namnet.
            if (skaPrata)
            {
                SetCursorPosition(2, _markeratIndex + 1);
            }
        }
    }
}
