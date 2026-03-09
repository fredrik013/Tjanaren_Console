namespace Stugan.Rooms
{
    public class Glasveranda : Rum
    {
        public Glasveranda() : base("Glasverandan", "Du öppnar dörren mot den inbjudande glasverandan. Här ser man ett möblemang med sköna rottingstolar som passar finfolket." + "i fönstren står det välskötta blommor. Under bordet ligger det en matta där man definitivt inte får sätta smutsiga fötter.")
        {

        }

        public override bool KanGaIn(Spelare s)
        {
            // Innan spelaren kliver in på verandan, kollar vi var han kommer ifrån
            if (s.NuvarandeRum is Vardagsrum v)
            {
                // Vi använder den befintliga signalen vi byggde förut!
                v.ReageraPaHandling("vadra");
            }

            return base.KanGaIn(s);
        }
    }
}
