using static System.Console;

namespace Stugan.Rooms
{
    public class Hall : Rum
    {
        public Hall() : base("Hallen",
                    "Du kliver in i en hemtrevlig hall. Det doftar svagt av såpa och gammalt trä. " +
        "På väggen hänger en spegel och under den står en skohylla. " +
        "Till vänster ser du en dörr och till höger verkar det finnas en trappa mot källaren." + "Rakt fram fortsätter hallen längre in i huset.")
        {
            SakerIRummet.Add(new Klader("Plasttofflor", "Ett par blå plasttofflor.", Bekladnadstyp.Inne, Kroppsdel.Fot, BekladnadsLager.Mellan, true, true, true));
        }

        public override void UndersokRum(Spelare spelare)
        {
            var rum = spelare.NuvarandeRum;
            // Använd StringComparison för att slippa ToLower-bekymmer
            var plasttofflor = rum.SakerIRummet.FirstOrDefault(s => s.Namn.Equals("Plasttofflor", StringComparison.OrdinalIgnoreCase));

            if (plasttofflor != null && plasttofflor.ArGomd)
            {
                plasttofflor.ArGomd = false;
                WriteLine("\nDu rotar bland de få sakerna i skohyllan...");
                WriteLine("Där, längst in under en gammal tidning, hittar du ett par blå plasttofflor!");

                // Här kan vi "skryta" lite med vår nya prop
                if (plasttofflor is Klader k && k.SkyddarMotVatten)
                {
                    WriteLine("De ser ut att tåla både fukt och lort – perfekta för källaren.");
                }
            }
            else
            {
                // En lite mer beskrivande text om man letar igen
                WriteLine("\nDu ser inget mer av värde i skohyllan, bara lite grus och dammråttor.");

                if (spelare.AktivtSkodon.ToLower() == "ute")
                {
                    WriteLine("Du ser att dina boots har lämnat leriga spår på det rena golvet. Aj då.");
                }
            }
        }
    }
}
