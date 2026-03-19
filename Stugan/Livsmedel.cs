namespace Stugan
{
    public class Livsmedel : Spelsak
    {
        public Livsmedel(string namn, string beskrivning, bool kanPlockasUpp = true, bool arGomd = false)
            : base(namn, beskrivning)
        {
            KanPlockasUpp = kanPlockasUpp;
            ArGomd = arGomd;
        }

        public override string Anvand(Spelare spelare)
        {
            this.ForsvinnerVidAnvandning = true;
            return Anvandningsmeddelande;
        }

    }
}
