namespace Stugan.Modeller.Saker
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
            ForsvinnerVidAnvandning = true;
            return Anvandningsmeddelande;
        }

    }
}
