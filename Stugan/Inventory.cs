using static System.Console;

namespace Stugan
{
    public class Inventory
    {
        private int _forraIndex = 0;

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
                WriteLine("Ryggsäcken är tom.");
                return;
            }

            // Dessa körs bara EN gång när ryggsäcken öppnas
            Clear();
            WriteLine("RYGGSÄCK");

            // Rita den statiska listan (utan statusar än)
            for (int i = 0; i < _saker.Count; i++)
            {

                var sak = _saker[i];
                string status = "";
                if (sak is Klader p) status = p.ArAktiv ? " (påtagen)" : " (i ryggsäcken)";
                else if (sak is Redskap r) status = r.ArAktiv ? " (i handen)" : " (i ryggsäcken)";
                WriteLine($"   {sak.Namn}{status}");
            }

            _markeratIndex = 0;
            UppmarksammaRad();

            // HÄR startar vi en lokal loop istället för att anropa metoden på nytt
            bool tittar = true;
            while (tittar)
            {
                var k = ReadKey(true).Key;

                switch (k)
                {
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

                        // Uppdatera statusen på den raden vi står på (utan Clear)
                        UppmarksammaRad();

                        SetCursorPosition(0, 12);
                        WriteLine(svar.PadRight(Console.WindowWidth));

                        if (!_saker.Contains(valdSak))
                        {
                            System.Threading.Thread.Sleep(1000);
                            if (_saker.Count == 0) { tittar = false; break; }

                            // Om saken försvann (t.ex. äten), rita om listan på plats
                            RitaOmListanUtanClear();
                            if (_markeratIndex >= _saker.Count) _markeratIndex = _saker.Count - 1;
                            UppmarksammaRad();
                        }
                        break;

                    case ConsoleKey.Delete:
                        var sakAttSlappa = _saker[_markeratIndex];
                        if (sakAttSlappa is Klader plagg)
                        {
                            plagg.ArAktiv = false;
                            if (plagg.Typ == Bekladnadstyp.Ute || plagg.Typ == Bekladnadstyp.Inne || plagg.Typ == Bekladnadstyp.Skydd)
                                s.AktivtSkodon = "";
                        }

                        s.NuvarandeRum.SakerIRummet.Add(sakAttSlappa);
                        _saker.RemoveAt(_markeratIndex);

                        SetCursorPosition(0, 12);
                        WriteLine($"Du lämnade {sakAttSlappa.Namn} i {s.NuvarandeRum.Namn}.".PadRight(Console.WindowWidth));
                        System.Threading.Thread.Sleep(1000);

                        if (_saker.Count == 0) tittar = false;
                        else
                        {
                            if (_markeratIndex >= _saker.Count) _markeratIndex = _saker.Count - 1;
                            RitaOmListanUtanClear();
                            UppmarksammaRad();
                        }
                        break;

                    case ConsoleKey.Escape:
                    case ConsoleKey.I:
                        tittar = false;
                        break;
                }
            }
        }

        // Hjälpmetod för att rita om namnen om listans längd ändras (vid Delete/Äta)
        private void RitaOmListanUtanClear()
        {
            for (int i = 0; i < 10; i++) // Rensa gamla rader
            {
                SetCursorPosition(0, i + 1);
                Write("".PadRight(Console.WindowWidth));
            }
            for (int i = 0; i < _saker.Count; i++)
            {
                SetCursorPosition(0, i + 1);
                WriteLine($"   {_saker[i].Namn}");
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
            // 1. Hantera Kläder (Lager, Placering och Skostatus)
            if (sak is Klader plagg)
            {
                // Kontrollera krockar och lager-ordning INNAN vi tar på oss plagget
                if (inv != null && !plagg.ArAktiv)
                {
                    var aktivaPaSammaStalle = inv.GetAllaSaker().OfType<Klader>()
                                                 .Where(k => k.ArAktiv && k.Placering == plagg.Placering);

                    foreach (var annat in aktivaPaSammaStalle)
                    {
                        // REGEL A: Samma lager? Ta av det gamla plagget först.
                        if (annat.Lager == plagg.Lager)
                        {
                            annat.Anvand(s);
                        }

                        // REGEL B: Försöker vi sätta på något UNDER ett befintligt lager?
                        // (T.ex. Strumpor när Skor redan är på)
                        if (annat.Lager > plagg.Lager)
                        {
                            return $"Du kan inte ta på dig {plagg.Namn.ToLower()} utanpå {annat.Namn.ToLower()}!";
                        }
                    }
                }

                // Kör plaggets egen logik (växlar ArAktiv och pratar med rummet)
                string meddelandeFrånPlagget = plagg.Anvand(s);

                // Uppdatera spelarens AktivtSkodon baserat på dina original-switchar
                switch (plagg.Typ)
                {
                    case Bekladnadstyp.Ute:
                    case Bekladnadstyp.Inne:
                    case Bekladnadstyp.Skydd:
                        // Vi uppdaterar bara skostatus om det är fötternas yttersta/mellersta lager
                        if (plagg.Placering == Kroppsdel.Fot && plagg.Lager != BekladnadsLager.Underst)
                        {
                            s.AktivtSkodon = plagg.ArAktiv ? plagg.Typ.ToString() : "";
                        }
                        break;
                }

                return meddelandeFrånPlagget;
            }

            // 2. Hantera Redskap (Behåll din existerande logik)
            if (sak is Redskap redskap)
            {
                string svar = redskap.Anvand(s);

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

            // 3. Fallback för vanliga saker
            return sak.Anvand(s);
        }

        private void UppmarksammaRad(bool skaPrata = true)
        {
            if (_saker.Count == 0) return;

            // 1. Flytta cursorn till början av föremålets namn
            SetCursorPosition(3, _markeratIndex + 1);

            if (skaPrata)
            {
                var sak = _saker[_markeratIndex];
                string status = "";

                if (sak is Klader p) status = p.ArAktiv ? " (påtagen)" : " (i ryggsäcken)";
                else if (sak is Redskap r) status = r.ArAktiv ? " (i handen)" : " (i ryggsäcken)";

                // 2. Skriv ut namnet och statusen på nytt på just denna rad.
                // Detta gör att texten finns där för JAWS att läsa när cursorn landar.
                Write($"{sak.Namn}{status}".PadRight(40));

                // 3. Sätt tillbaka cursorn i början av namnet så JAWS börjar läsa därifrån.
                SetCursorPosition(3, _markeratIndex + 1);
            }
        }

        // En liten hjälpmetod för att faktiskt skriva ut raden korrekt
        private void RitaRad(int index, bool medMarkor)
        {
            if (index < 0 || index >= _saker.Count) return;

            var sak = _saker[index];
            string markor = medMarkor ? "" : " "; // Här sätter vi dit guldstjärnan (> markerar)

            string status = "";
            if (sak is Klader p) status = p.ArAktiv ? " (påtagen)" : " (i ryggsäcken)";
            else if (sak is Redskap r) status = r.ArAktiv ? " (i handen)" : " (i ryggsäcken)";

            // Skriv ut hela raden och fyll ut med tomrum för att rensa gammal text
            Write($"{markor} {sak.Namn}{status}".PadRight(45));
        }
    }
}
