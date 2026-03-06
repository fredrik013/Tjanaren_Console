using Stugan;
using Stugan.Rooms;

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
    Gardsplan gardsplan = new Gardsplan();
    Hall hall = new Hall();
    gardsplan.Norr = hall;
    hall.Soder = gardsplan;

    Kok kok = new Kok();
    hall.Vaster = kok;
    kok.Oster = hall;

    Kallare kallare = new Kallare();
    hall.Oster = kallare;
    kallare.Vaster = hall;

    InreHall hall2 = new InreHall();
    hall.Norr = hall2;
    hall2.Soder = hall;

    Vardagsrum vardagsrum = new Vardagsrum();
    hall2.Vaster = vardagsrum;
    vardagsrum.Oster = hall2;

    return gardsplan;
}