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
            rum.UndersokRum(spelare);
        }






    }
}
