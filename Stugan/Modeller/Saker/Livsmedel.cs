using Stugan.Core;

namespace Stugan.Modeller.Saker
{
    public enum Livsmedelstyp
    {
        Mat = 0,
        Dryck = 1

    }

    public class Livsmedel : Spelsak
    {
        public Livsmedelstyp Typ { get; set; }

        public Livsmedel(string namn, string beskrivning, Livsmedelstyp Typ, bool kanPlockasUpp = true, bool arGomd = false)
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
