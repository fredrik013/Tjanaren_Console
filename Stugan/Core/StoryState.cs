namespace Stugan.Core
{
    public class StoryState
    {
        public bool HarHittatRortang { get; set; } = false;

        public bool RorArLagat { get; set; } = false;

        public bool GrovstadatPannrum { get; set; } = false;

        public bool HeltRentIKallaren { get; set; } = false;

        public bool MoppArTvattad { get; set; } = false;

        public bool TvattmaskinArIgang { get; set; }

        public bool HarAtitBrod { get; set; } = false;

        public bool HarSmutsatNerMattan { get; set; } = false;

        public bool HallenArSkurad { get; set; } = false;

        public bool HarArbetskladerPaSig { get; set; } = false;

        public bool HarNyckel { get; set; } = false;

        public string GetStatusBeskrivning()
        {
            // Vi switchar på 'true' för att kunna utvärdera dina flaggor fritt i varje case
            switch (true)
            {
                // Prioritet 1: Allt är klart
                case var _ when RorArLagat && HeltRentIKallaren && MoppArTvattad:
                    return "Arbetet är utfört till belåtenhet (under omständigheterna).";

                // Prioritet 2: Specifika hån (t.ex. brödtjuven)
                case var _ when HarAtitBrod && !RorArLagat:
                    return "Du ser misstänkt mätt ut för att inte ha rört ett finger i källaren än!";

                // Prioritet 3: Delmål i källaren
                case var _ when RorArLagat && !GrovstadatPannrum:
                    return "Läckan är stoppad, men det ser ut som en krigszon. Fram med skyffeln!";

                case var _ when RorArLagat:
                    return "Röret är lagat, men du har lämnat en rökig röra efter dig.";

                case var _ when HarHittatRortang:
                    return "Du har rörtången i säkert förvar. Nu är det väl bara att hitta pannrummet och börja jobba?";

                // Standard: Om ingenting har hänt än
                default:
                    return "Du har knappt börjat lyfta ett finger än.";
            }
        }
    }
}
