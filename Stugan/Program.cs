using Stugan;

// 1. Skapa världen (Rum och föremål)

Rum startRum = SkapaVarlden();

// 2. Initiera spelaren
Spelare spelare = new Spelare(startRum);
var startBoots = new Klader("Boots", "Dina trogna men leriga boots.", "skodon", true, true, false, true);
// Vi använder .Add() direkt på listan för att slippa "Du plockar upp"-texten
spelare.Ryggsack.GetAllaSaker().Add(startBoots);
Pusselmotor.HanteraUtrustning(startBoots, spelare, spelare.Ryggsack);

// 3. Starta motorn
Spelmotor motor = new Spelmotor(spelare);
motor.Starta();

// --- Lokala funktioner (Längst ner i filen) ---

Rum SkapaVarlden()
{
    Rum hall = new Rum("Hallen", "En liten hall med en skohylla.");
    var tofflor = new Klader("Tofflor", "Ett par blå plasttofflor.", "skodon", true, true, true, false);
    hall.SakerIRummet.Add(tofflor);

    Rum kok = new Rum("Köket", "Här doftar det Eriksberg och bröd."); // [cite: 2026-02-18]
    hall.Norr = kok;
    kok.Soder = hall;
    kok.SakerIRummet.Add(new AllmanSak("Eriksberg", "En kall Eriksberg Karaktär."));



    return hall;
}