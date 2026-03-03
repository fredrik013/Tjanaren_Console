using static System.Console;
namespace Stugan
{
    public static class Pusselmotor
    {
        // Den här metoden sköter allt med ArAktiv-logiken och utrustning
        public static string HanteraUtrustning(Spelsak sak, Spelare s, Inventory inv)
        {
            if (sak is Klader plagg)
            {
                string meddelande = "";
                if (plagg.ArAktiv)
                {
                    plagg.ArAktiv = false;
                    meddelande = $"Du tar av dig {plagg.Namn}.";
                }
                else
                {
                    // Om det är skor, se till att ta av andra skor först
                    if (plagg.Typ.ToLower() == "skodon" && inv != null)
                    {
                        inv.AvaktiveraTyp("skodon");
                    }

                    plagg.ArAktiv = true;
                    meddelande = $"Du tar på dig {plagg.Namn}.";
                }

                // VIKTIGT: Uppdatera spelarens sträng så dörrvakten fattar
                if (plagg.Typ.ToLower() == "skodon")
                {
                    s.AktivtSkodon = plagg.ArAktiv ? plagg.Namn : "";
                }

                // Skriv bara ut på skärmen om vi faktiskt är inne i ryggsäcks-menyn (inv != null)
                // Det här gör att uppstarten i Program.cs blir tyst och fin för JAWS.
                if (inv != null)
                {
                    SetCursorPosition(0, 10);
                    WriteLine(meddelande.PadRight(70));
                }

                return meddelande;
            }

            // Om det inte är kläder, använd saken som vanligt
            sak.Anvand(s);
            return $"Du använder {sak.Namn}.";
        }

        public static void FarGaIn(Rum nastaRum, Spelare spelare)
        {
            // DÖRRVAKT-LOGIK
            switch (nastaRum.Namn)
            {
                case "Köket":
                    // Kolla om spelaren har boots på fötterna
                    if (spelare.AktivtSkodon == "Boots")
                    {
                        SetCursorPosition(0, 10);
                        System.Threading.Thread.Sleep(100);
                        WriteLine("Stopp! Du kan inte gå in i köket med leriga boots.".PadRight(70));

                        return; // Avbryt flytten!
                    }
                    break;

            }

            // OM VI INTE BLEV STOPPADE: Genomför flytten
            spelare.NuvarandeRum = nastaRum;

            // Läs upp den nya rumsbeskrivningen direkt för JAWS
            Clear();
            System.Threading.Thread.Sleep(100);
            spelare.NuvarandeRum.VisaBeskrivning();
        }

        public static void Undersok(Spelare spelare)
        {
            var rum = spelare.NuvarandeRum;
            WriteLine($"\nDu ser dig noga omkring i {rum.Namn}...");

            switch (rum.Namn)
            {
                case "Hallen":
                    HanteraHallen(spelare);
                    break;

                case "Köket":
                    HanteraKoket(spelare);
                    break;
                case "Vardagsrummet":
                    HanteraVardagsrummet(spelare);
                    break;
                case "Källaren":
                    HanteraKallaren(spelare);
                    break;

                default:
                    WriteLine("Du hittar inget särskilt när du undersöker rummet.");
                    break;
            }
        }

        public static void HanteraHallen(Spelare spelare)
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

        public static void HanteraKoket(Spelare spelare)
        {
            var rum = spelare.NuvarandeRum;
            var brod = rum.SakerIRummet.FirstOrDefault(s => s.Namn == "Lunchbröd");

            if (brod != null && brod.ArGomd)
            {
                brod.ArGomd = false; // Nu dyker det upp i rummets lista!
                WriteLine("\nDu undersöker det dukade bordet och ser bland annat ett brödfat som är täckt med en handduk.");
                WriteLine("Du lyfter på handduken och hittar lunchbröd!");
                DavyKager.Tolk.Output("Du hittade lunchbröd under en handduk på bordet.");
            }
            else
            {
                WriteLine("\nKöket är rent och snyggt. Brödfatet står tomt på bordet.");
            }
        }

        public static void HanteraVardagsrummet(Spelare spelare)
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

        public static void HanteraKallaren(Spelare spelare)
        {
            var rum = spelare.NuvarandeRum;

            // Vi letar i rummets lista efter saken som heter "Innetofflor"
            var tofflor = rum.SakerIRummet.FirstOrDefault(s => s.Namn.Equals("Innetofflor", StringComparison.OrdinalIgnoreCase));

            // 1. Ljudlogik för JAWS (Fötterna)
            if (string.IsNullOrEmpty(spelare.AktivtSkodon))
            {
                WriteLine("\nDet iskalla källarvattnet klafsar obehagligt mellan tårna.");
            }
            else if (spelare.AktivtSkodon == "Boots")
            {
                WriteLine("\nDina tunga boots dunsar mot betongen. De håller vätan ute men lortar ner.");
            }
            else if (spelare.AktivtSkodon == "Plasttofflor")
            {
                WriteLine("\nDet 'ploppar' hemtrevligt om plasttofflorna i vätan.");
            }

            // 2. Själva sökandet
            if (tofflor != null && tofflor.ArGomd)
            {
                // Här hittar vi dem!
                WriteLine("\nDu undersöker den torra hyllan högt upp på väggen.");
                WriteLine("Dina fingrar nuddar något mjukt... det är innetofflorna!");

                // VIKTIGT: Vi sätter ArGomd till false så de blir synliga i rummet/kan tas upp
                tofflor.ArGomd = false;

                WriteLine("\nEtt svagt, belåtet mumlande hörs från mörkret.");
            }
            else if (tofflor != null && !tofflor.ArGomd)
            {
                // Om vi redan har hittat dem men inte plockat upp dem
                WriteLine("\nDu ser innetofflorna ligga på hyllan där du hittade dem.");
            }
            else
            {
                // Om 'tofflor' är null (dvs hittas inte i listan alls)
                WriteLine("\nDu letar noga på hyllorna men hittar inget mer än damm och spindelväv.");
            }
        }
    }
}
