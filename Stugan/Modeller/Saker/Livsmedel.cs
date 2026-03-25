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

        public override string Anvand(Spelare spelare, StoryState story)
        {
            // Allt livsmedel försvinner när det används
            ForsvinnerVidAnvandning = true;

            // Om det är mat, flagga för det i storyn
            if (Typ == Livsmedelstyp.Mat)
            {
                story.HarAtitBrod = true;
            }

            // Om det är dryck, visas meddelandet ändå, 
            // och vi kan lägga till törst-logik här i framtiden om vi vill.

            return Anvandningsmeddelande;
        }

    }
}
