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
            var tv = rum.SakerIRummet.FirstOrDefault(s => s.Namn.ToLower() == "tv");

            if (tv != null)
            {
                if (!rum.HarUndersokts)
                {
                    // FÖRSTA GÅNGEN: TV:n levererar budskapet
                    WriteLine("\nDu går fram till den brusande TV:n.");
                    WriteLine("Genom det gråa flimret hörs en raspig röst:");
                    WriteLine("'...vissa går på de fina mattorna med leriga boots... det gillas inte...'");
                    System.Threading.Thread.Sleep(500);
                    WriteLine("'...tofflorna i skohyllan räcker inte till alla... sök i källaren...'");
                    System.Threading.Thread.Sleep(500);
                    WriteLine("'...bzzzzt... prata med läskamraten... bzzzzt...'");

                    WriteLine("\nPlötsligt hörs ett knäppande ljud och skärmen slocknar helt.");
                    WriteLine("Rummet blir med ens mycket mörkare.");

                    // Här lägger vi till spänningen
                    WriteLine("I tystnaden som uppstår känner du plötsligt hur nackhåren reser sig.");
                    WriteLine("Du känner dig intensivt iakttagen från de mörka hörnen.");

                    rum.HarUndersokts = true;
                }
                else
                {
                    // ANDRA GÅNGEN: TV:n är död
                    WriteLine("\nTV:n står mörk och tyst. Den verkar ha dött för gott.");
                    WriteLine("Du ser din egen bleka spegelbild i det svarta glaset...");
                    WriteLine("...men för ett ögonblick ser det ut som om någon står precis bakom dig.");
                    WriteLine("Du vänder dig om, men vardagsrummet är tomt. Känslan av att vara iakttagen dröjer kvar.");
                }
            }
            else
            {
                // Om TV:n saknas - den ultimata "iakttagen"-känslan
                WriteLine("\nVardagsrummet är onaturligt tyst.");
                WriteLine("Du kan inte skaka av dig känslan av att någon ser varje steg du tar.");
            }
        }
    }
}
