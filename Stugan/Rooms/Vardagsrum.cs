using static System.Console;

namespace Stugan.Rooms
{
    public class Vardagsrum : Rum
    {
        private bool luktarSvett = false;

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

            if (luktarSvett)
            {
                WriteLine("\nDet vilar en tung, unken doft av fotsvett i rummet.");
            }

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

        public override void ReageraPaHandling(string handling)
        {
            switch (handling.ToLower())
            {
                case "pa_ute":
                    WriteLine("\n[TV:N SPRAKAR TILL]");
                    WriteLine("'...smutsen från utsidan följer dina steg... det gillas inte...'");
                    break;

                case "pa_inne":
                    WriteLine("\n[TV:N FLIMRAR TILL]");
                    WriteLine("'...mjuka steg... ett klokt val... men sök vidare...'");
                    break;

                case "av_ute":
                    luktarSvett = true; // Nu sitter det i väggarna!
                    WriteLine("\n[TV:N GER IFRÅN SIG ETT DISKRET PIP]");
                    WriteLine("'...äntligen... men aromen av instängda fötter dröjer sig kvar...'");
                    WriteLine("En lätt dimma av tveksam doft sprider sig från de varma bootsen.");
                    break;

                case "av_inne":
                    WriteLine("\n[TV:N KNÄPPER TILL]");
                    WriteLine("Rummet känns plötsligt väldigt tyst när du tar av dig tofflorna.");
                    break;
            }
        }
    }
}
