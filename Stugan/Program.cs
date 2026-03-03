using Stugan;
using Stugan.Rooms;

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

    Hall hall = new Hall();
    gardsplan.Norr = hall;
    hall.Soder = gardsplan;


    var tofflor = new Klader("Tofflor", "Ett par blå plasttofflor.", "skodon", true, true, true, false);
    hall.SakerIRummet.Add(tofflor);

    var hall2 = new Rum("Hallen2", "Du fortsätter längre in i hallen och kommer in på en mjuk heltäckande matta. Här ska man säkert inte gå med några leriga boots." + "Till vänster hör du en TV. Kanske det är vardagsrummet.");
    hall2.VisaNamn = "Hallen";
    hall.Norr = hall2;
    hall2.Soder = hall;

    // Skapa rummet
    Rum vardagsrum = new Rum("Vardagsrummet",
        "Du kliver in i vardagsrummet. En stor, mjuk soffa står framför en gammal tjock-TV som står och brusar. " +
        "Ljuset från skärmen fladdrar mot de mörka tapeterna.");

    // Lägg till en kort beskrivning (för framtida besök)
    vardagsrum.VisaNamn = "Vardagsrummet";

    // Lägg till TV:n som en sak man kan titta på
    vardagsrum.SakerIRummet.Add(new AllmanSak("TV", "En gammal Philips-TV. Den visar bara myrornas krig, men ljudet är öronbedövande.", false, false));

    // Koppla ihop det med Hallen 2 (Vardagsrummet ligger till vänster, alltså Väster)
    hall2.Vaster = vardagsrum;
    vardagsrum.Oster = hall2;


    Kok kok = new Kok();
    hall.Vaster = kok;
    kok.Oster = hall;

    Kallare kallare = new Kallare();
    // Koppla ihop källaren med Hallen (Söder ut från Hallen)
    // Se till att variabelnamnet 'hall' matchar det du har i din kod
    hall.Oster = kallare;
    kallare.Vaster = hall;

    return gardsplan;
}