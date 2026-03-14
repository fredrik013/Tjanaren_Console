using static System.Console;

namespace Stugan.Rooms
{
    public class InreHall : Rum
    {
        public InreHall() : base("Hallen", "Du fortsätter längre in i hallen. Golvet här täcks av en tjock, ljus och extremt mjuk heltäckningsmatta " +
            "som verkar suga upp allt ljud. Till vänster hörs det dämpade ljudet från en TV – det måste vara vardagsrummet.")
        {
            VisaNamn = "i hallen";
            OppnaUtgangar("Soder", "Vaster");
        }

        public override void UndersokRum(Spelare spelare)
        {
            base.UndersokRum(spelare);

            // Vi switchar på typen, men hämtar namnet dynamiskt för texten
            switch (spelare.AktivtSkodon.ToLower())
            {
                case "ute":
                    WriteLine("\n[VARNING]");
                    // Vi använder spelarens aktiva skodon-namn istället för "Boots"
                    WriteLine($"Mörka lerfläckar från dina {spelare.AktivtSkodonNamn} breder ut sig på den ljusa mattan.");
                    WriteLine("En röst viskar strängt: 'Vissa skor hör hemma på bron, inte på finmattan...'");
                    break;

                case "inne":
                    WriteLine($"\nDina {spelare.AktivtSkodonNamn} glider ljudlöst över den mjuka mattan.");
                    break;

                case "":
                    WriteLine("\nDen mjuka mattan kittlar skönt mellan dina bara tår.");
                    break;
            }
        }
    }
}
