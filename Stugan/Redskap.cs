using static System.Console;

namespace Stugan
{
    public class Redskap : Spelsak
    {
        public Redskap(string namn, string beskrivning, bool kanPlockasUpp = true, bool arGomd = false)
       : base(namn, beskrivning)
        {
            KanPlockasUpp = kanPlockasUpp;
            ArGomd = arGomd;
        }

        public override void Anvand(Spelare spelare)
        {
            WriteLine($"Du använder {Namn} för att {AnvandningsOmrade}.");
            // Här kan vi lägga till specifik logik senare
        }
    }
}
