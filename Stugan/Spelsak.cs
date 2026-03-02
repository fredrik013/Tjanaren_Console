namespace Stugan
{
    public abstract class Spelsak
    {
        public string Namn { get; set; }

        public string Beskrivning { get; set; }

        public bool KanPlockasUpp { get; set; }

        public bool ArGomd { get; set; }

        public bool ArAktiv { get; set; }

        public Spelsak(string namn, string beskrivning, bool kanplockasupp = true, bool argomd = false)
        {
            Namn = namn;
            Beskrivning = beskrivning;
            KanPlockasUpp = kanplockasupp;
            ArGomd = false;
            ArAktiv = false;
        }



        public abstract void Anvand(Spelare s);
    }
}
