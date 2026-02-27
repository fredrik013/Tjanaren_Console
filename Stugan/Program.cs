using Stugan;

// 1. Skapa världen (Rum och föremål)

Rum startRum = SkapaVarlden();

// 2. Initiera spelaren
Spelare spelare = new Spelare(startRum);

// 3. Starta motorn
Spelmotor motor = new Spelmotor(spelare);
motor.Starta();

// --- Lokala funktioner (Längst ner i filen) ---

Rum SkapaVarlden()
{
    Rum hall = new Rum("Hallen", "En liten hall med en skohylla.");
    var tofflor = new Klader("Tofflor", "Ett par blå plasttofflor.", "Fötter", true, true, true);
    hall.SakerIRummet.Add(tofflor);

    Rum kok = new Rum("Köket", "Här doftar det Eriksberg och bröd."); // [cite: 2026-02-18]
    hall.Norr = kok;
    kok.Soder = hall;
    kok.SakerIRummet.Add(new AllmanSak("Eriksberg", "En kall Eriksberg Karaktär."));



    return hall;
}