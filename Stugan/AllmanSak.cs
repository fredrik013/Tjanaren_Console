using static System.Console;

namespace Stugan
{
    public class AllmanSak : Spelsak
    {

        public AllmanSak(string namn, string beskrivning, bool kanplockasupp = true, bool argomd = false)
            : base(namn, beskrivning, kanplockasupp)
        {
            Namn = namn;
            Beskrivning = beskrivning;
            KanPlockasUpp = kanplockasupp;
            ArGomd = argomd;
        }

        public override void Anvand(Spelare s)
        {
            // En enkel standard-feedback för JAWS så länge
            WriteLine($"Du undersöker {this.Namn}. Det verkar inte gå att göra så mycket med den just nu.");
        }
    }
}
