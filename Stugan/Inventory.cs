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
                        string svar = Pusselmotor.HanteraUtrustning(valdSak, s, this);

                        // FIX: Om det vi precis drog på oss var skor, uppdatera spelarens sträng!
                        if (valdSak is Klader plagg && plagg.Typ.ToLower() == "skodon")
                        {
                            s.AktivtSkodon = plagg.ArAktiv ? plagg.Namn : "";
                        }

                        // Vi pratar bara om det som faktiskt hände (svar), 
                        // istället för att läsa upp hela raden igen.
                        UppmärksammaRad(true, svar);
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

        private void UppmärksammaRad(bool skaPrata = true, string extraMeddelande = "")
        {
            // 1. Flytta markören till rätt rad i listan
            SetCursorPosition(0, _markeratIndex + 1);
            var sak = _saker[_markeratIndex];

            // 2. Skapa status-texten för skärmen
            string status = "";
            if (sak is Klader p)
            {
                status = p.ArAktiv ? " - påtagen" : " - i ryggsäcken";
            }

            string textRad = $"{sak.Namn}{status}";
            // Skriv ut på skärmen (fyll ut med mellanslag så gammal text försvinner)
            Write(textRad.PadRight(40));

            // 3. Hantera vad JAWS ska säga
            if (skaPrata)
            {
                // Om vi har ett meddelande från pusselmotorn (vid Enter), läs BARA det.
                if (!string.IsNullOrEmpty(extraMeddelande))
                {
                    Tolk.Output(extraMeddelande);
                }
                else
                {
                    // Vid vanlig navigering (pilar), läs upp namnet och statusen.
                    Tolk.Output(textRad);
                }
            }
        }

        public List<Spelsak> GetAllaSaker()
        {
            return _saker;
        }
    }
}
