using Stugan.Core;

namespace Stugan.Modeller.Saker
{
    public enum Livsmedelstyp
    {
        Mat = 0,
        Dryck = 1,
        Sprit = 2
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

            // Logik baserad på typ
            switch (Typ)
            {
                case Livsmedelstyp.Mat:
                    story.HarAtitBrod = true;
                    break;

                case Livsmedelstyp.Sprit:
                    return "Du stinker sprit! Det var inte så smart att dricka det här. Du känner dig yr och illamående. Kanske borde du inte ha druckit det här?";
                    break;

                default:
                    break;

            }

            return Anvandningsmeddelande;
        }

    }
}
