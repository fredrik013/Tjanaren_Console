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

        public override string Anvand(Spelare spelare)
        {
            // Istället för WriteLine, så returnerar vi strängen 
            // så att spelmotorn kan bestämma när den ska läsas upp.
            return $"Du använder {Namn}.";
        }
    }
}
