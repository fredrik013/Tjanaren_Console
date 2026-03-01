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
                Console.SetCursorPosition(0, 10);
                Console.WriteLine(meddelande.PadRight(40));

                return meddelande; // Returnera fortfarande för JAWS skull
            }

            sak.Anvand(s);
            return $"Du använder {sak.Namn}.";
        }

        // Den här kollar om vi får gå in i köket
        public static bool FarGaInIKoket(Inventory inv)
        {
            // Vi letar i ryggsäcken efter något som är "Skodon" OCH "ArAktiv"
            foreach (var sak in inv.GetAllaSaker())
            {
                if (sak is Klader plagg && plagg.Typ == "Skodon" && plagg.ArAktiv)
                {
                    // Om det är Boots (som vi bestämt är skitiga), returnera false
                    if (plagg.Namn.ToLower().Contains("boots"))
                    {
                        return false;
                    }
                }
            }
            return true; // Barfota eller tofflor är ok!
        }
    }
}