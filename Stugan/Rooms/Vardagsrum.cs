using static System.Console;

namespace Stugan.Rooms
{
    public class Vardagsrum : Rum
    {
        public Vardagsrum() : base("Vardagsrummet", "Du kliver in i vardagsrummet. En stor, mjuk soffa står framför en gammal tjock-TV som står och brusar. " +
        "Ljuset från skärmen fladdrar mot de mörka tapeterna.")
        {
            SakerIRummet.Add(new AllmanSak("TV", "En gammal Philips-TV. Den visar bara myrornas krig, men ljudet är öronbedövande.", false, false));
        }

        public override void UndersokRum(Spelare spelare)
        {
            var rum = spelare.NuvarandeRum;
            // Vi letar fortfarande efter TV-objektet för att veta om vi kan interagera
            var tv = rum.SakerIRummet.FirstOrDefault(s => s.Namn.Equals("TV", StringComparison.OrdinalIgnoreCase));

            if (tv == null)
            {
                WriteLine("\nVardagsrummet är onaturligt tyst.");
                return; // Avbryt tidigt om TV:n är borta
            }

            if (!rum.HarUndersokts)
            {
                WriteLine("\nDu går fram till den brusande TV:n.");
                WriteLine("Genom det gråa flimret hörs en raspig röst:");

                // HÄR ÄR DEN NYA RENA LOGIKEN (Ingen hårdkodning av namn!)
                switch (spelare.AktivtSkodon?.ToLower())
                {
                    case "ute":
                        WriteLine("'...smutsen från utsidan följer dina steg... det gillas inte...'");
                        break;
                    case "inne":
                        WriteLine("'...mjuka steg... ett klokt val... men de räcker inte till alla... sök i källaren...'");
                        break;
                    default:
                        WriteLine("'...barfota och blöt... jag känner doften av instängda boots ända hit...'");
                        break;
                }

                System.Threading.Thread.Sleep(500);
                WriteLine("'...bzzzzt... prata med läskamraten... bzzzzt...'");
                WriteLine("\nSkärmen dör med ett sista knäpp.");
                rum.HarUndersokts = true;
            }
            else
            {
                WriteLine("\nTV:n står mörk och tyst. Du ser din spegelbild i det svarta glaset.");
            }
        }
    }
}
