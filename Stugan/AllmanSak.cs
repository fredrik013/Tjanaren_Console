namespace Stugan
{
    public class AllmanSak : Spelsak
    {
        public Action<Spelare>? Specialeffekt { get; set; }

        public AllmanSak(string namn, string beskrivning, bool kanplockasupp = true, bool argomd = false)
            : base(namn, beskrivning, kanplockasupp, argomd)
        {

        }
        public override string Anvand(Spelare s)
        {
            // 1. Kör speciallogik om den finns (t.ex. s.ArSkadad = false)
            Specialeffekt?.Invoke(s);

            // 2. Om du har skrivit in ett meddelande, använd det
            if (!string.IsNullOrEmpty(Anvandningsmeddelande))
            {
                return Anvandningsmeddelande;
            }

            // 3. Annars, ge standardmeddelandet
            return $"Du undersöker {this.Namn}. Det verkar inte gå att göra så mycket med den just nu.";
        }
    }
}
