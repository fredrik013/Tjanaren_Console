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
            SakerIRummet.Add(new Klader("Tofflor", "Ett par blå plasttofflor.", "skodon", true, true, true, false));
        }

        public override void UndersokRum(Spelare spelare)
        {
            var rum = spelare.NuvarandeRum;
            var tofflor = rum.SakerIRummet.FirstOrDefault(s => s.Namn.ToLower() == "tofflor");

            if (tofflor != null && tofflor.ArGomd)
            {
                tofflor.ArGomd = false;
                WriteLine("Du rotar i skohyllan och hittar ett par plasttofflor!");
                WriteLine("Perfekta för fuktiga utrymmen.");
            }
            else
            {
                WriteLine("Skohyllan är tom sånär som på lite grus.");
            }
        }
    }
}
