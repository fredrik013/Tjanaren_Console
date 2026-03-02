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
                    if (spelare.AktivtSkodon.ToLower().Contains("boots"))
                    {
                        SetCursorPosition(0, 10);
                        System.Threading.Thread.Sleep(100);
                        WriteLine("Stopp! Du kan inte gå in i köket med leriga boots. Sätt på tossorna!".PadRight(70));

                        return; // Avbryt flytten!
                    }
                    break;

                case "Källaren":
                    // Här kan vi senare lägga in krav på skor för att inte bli blöt om fötterna
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
                    // Lite finlir för köket: smulor från lunchbrödet
                    WriteLine("Det ligger lite smulor på bänken, annars är det rent och snyggt.");
                    break;

                case "Källaren":
                    WriteLine("Här är det mörkt och fuktigt på golvet. Tur om man har skor på sig.");
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
    }
}