using Stugan;

// 1. Skapa världen (Rum och föremål)

Rum startRum = SkapaVarlden();

// 2. Initiera spelaren
Spelare spelare = new Spelare(startRum);
var startBoots = new Klader("Boots", "Dina trogna men leriga boots.", "skodon", true, true, false, true);
spelare.Ryggsack.GetAllaSaker().Add(startBoots);
Pusselmotor.HanteraUtrustning(startBoots, spelare, null!);

// 3. Starta motorn
Spelmotor motor = new Spelmotor(spelare);
motor.Starta();

// --- Lokala funktioner (Längst ner i filen) ---

Rum SkapaVarlden()
{
    // Gårdsplanen - Första anhalten
    Rum gardsplan = new Rum("Gårdsplanen",
        "Du står på en grusad gårdsplan. Den röda stugan med sina vita knutar ser inbjudande ut i solskenet. " +
        "Rakt framför dig, åt norr, leder en gammal trädörr in till huset.");

    // Hallen - Nu med mer detaljer och hintar om dörrarna
    Rum hall = new Rum("Hallen",
        "Du kliver in i en hemtrevlig hall. Det doftar svagt av såpa och gammalt trä. " +
        "På väggen hänger en spegel och under den står en skohylla. " +
        "Till vänster ser du en dörr och till höger verkar det finnas en trappa mot källaren." + "Rakt fram fortsätter hallen längre in i huset.");

    // Koppla ihop gårdsplan och hall
    gardsplan.Norr = hall;
    hall.Soder = gardsplan;

    // Föremål i hallen (tofflorna är gömda i skohyllan tills man undersöker)
    var tofflor = new Klader("Tofflor", "Ett par blå plasttofflor.", "skodon", true, true, true, false);
    hall.SakerIRummet.Add(tofflor);

    var hall2 = new Rum("Hallen2", "Du fortsätter längre in i hallen och kommer in på en mjuk heltäckande matta. Här ska man säkert inte gå med några leriga boots." + "Till vänster hör du en TV. Kanske det är vardagsrummet.");
    hall2.VisaNamn = "Hallen";

    hall.Norr = hall2;
    hall2.Soder = hall;

    // Köket - Ligger rakt fram (Norr)
    Rum kok = new Rum("Köket",
        "Du kommer in i ett ljust och rymligt kök. Här doftar det av nybakat bröd och en hint av humle. " +
        "Köksbordet står dukat vid fönstret.");

    hall.Vaster = kok;
    kok.Oster = hall;

    // Eriksberg Karaktär - Din favorit! [cite: 2026-02-18]
    kok.SakerIRummet.Add(new AllmanSak("Eriksberg", "En immande kall Eriksberg Karaktär."));

    var brod = new AllmanSak("Lunchbröd", "Ett nybakat bröd, perfekt för en vardagslunch.", true, true);
    kok.SakerIRummet.Add(brod);

    // Källaren - Ligger till höger (Oster)
    Rum kallare = new Rum("Källaren",
        "En brant trätrappa leder ner till källaren. Här är luften sval och lite fuktig.");

    hall.Oster = kallare;
    kallare.Vaster = hall;

    return gardsplan;
}