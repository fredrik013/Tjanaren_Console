using Stugan;

// 1. Skapa världen (Rum och föremål)

Rum startRum = SkapaVarlden();

// 2. Initiera spelaren
Spelare spelare = new Spelare(startRum);
var startBoots = new Klader("Boots", "Dina trogna men leriga boots.", "skodon", true, true, false, true);
spelare.Ryggsack.GetAllaSaker().Add(startBoots);
Pusselmotor.HanteraUtrustning(startBoots, spelare, spelare.Ryggsack);

// 3. Starta motorn
Spelmotor motor = new Spelmotor(spelare);
motor.Starta();

// --- Lokala funktioner (Längst ner i filen) ---

Rum SkapaVarlden()
{
    Rum gardsplan = new Rum("Gårdsplanen", "Du står på en grusad gårdsplan. Solen lyser vackert på ett gammalt hus som ser väldigt inbjudande ut. Rakt framför dig ligger ingången.");

    Rum hall = new Rum("Hallen", "En liten hall med en skohylla.");
    gardsplan.Norr = hall;
    hall.Soder = gardsplan;
    var tofflor = new Klader("Tofflor", "Ett par blå plasttofflor.", "skodon", true, true, true, false);
    hall.SakerIRummet.Add(tofflor);

    Rum kok = new Rum("Köket", "Här doftar det Eriksberg och bröd."); // [cite: 2026-02-18]
    hall.Norr = kok;
    kok.Soder = hall;
    kok.SakerIRummet.Add(new AllmanSak("Eriksberg", "En kall Eriksberg Karaktär."));

    return gardsplan;
}