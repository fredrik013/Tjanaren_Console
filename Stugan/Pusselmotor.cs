using static System.Console;

namespace Stugan
{
    public static class Pusselmotor
    {
        // Den här metoden sköter allt med ArAktiv-logiken
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
                    if (plagg.Typ.ToLower() == "skodon")
                    {
                        inv.AvaktiveraTyp("skodon");
                    }
                    plagg.ArAktiv = true;
                    meddelande = $"Du tar på dig {plagg.Namn}.";
                }

                // HÄR SKRIVER VI UT DET PÅ SKÄRMEN!
                // Vi lägger det på en fast rad, t.ex. rad 10, så det inte krockar med listan
                SetCursorPosition(0, 10);
                WriteLine(meddelande.PadRight(40));

                return meddelande; // Returnera fortfarande för JAWS skull
            }

            sak.Anvand(s);
            return $"Du använder {sak.Namn}.";
        }

        public static void FarGaIn(Rum nastaRum, Spelare spelare)
        {
            switch (nastaRum.Namn)
            {
                case "Köket":
                    // Din klockrena boots-check
                    if (spelare.Ryggsack.GetAllaSaker().FirstOrDefault(s => s.Namn.ToLower().Contains("boots")) is Klader boots && boots.ArAktiv)
                    {
                        SetCursorPosition(0, 10);
                        WriteLine("Stopp! Du kan inte gå in i köket med leriga boots. Sätt på tossorna!".PadRight(60));
                        return; // Vi avbryter här, ingen förflyttning sker!
                    }
                    break;
                case "Källaren":
                    // Här kan vi lägga in källar-logiken sen (kanske boots/plasttofflor-krav?) [cite: 2026-02-24]
                    break;
            }

            // Om vi inte har blivit stoppade av en 'return' ovanför, så genomför vi flytten här!
            spelare.NuvarandeRum = nastaRum;
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

                case "Källaren":
                    WriteLine("Här är det mörkt och lite fuktigt på golvet. Tur om man har skor på sig.");
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


        public static bool HanteraKoket(Spelare spelare)
        {
            // Vi skippar loopen och går direkt på kärnan i din logik
            if (spelare.Ryggsack.GetAllaSaker().FirstOrDefault(s => s.Namn.ToLower().Contains("boots")) is Klader boots && boots.ArAktiv)
            {
                // Om bootsen hittas och är aktiva – Stopp!
                return false;
            }

            // Annars är allt grönt
            return true;
        }

    }
}
