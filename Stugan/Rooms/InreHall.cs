using static System.Console;

namespace Stugan.Rooms
{
    public class InreHall : Rum
    {
        public InreHall() : base("Hallen", "Du fortsätter längre in i hallen och kommer in på en mjuk heltäckande matta. Här ska man säkert inte gå med några leriga boots." + "Till vänster hör du en TV. Kanske det är vardagsrummet.")
        {
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
