using Stugan;
using Stugan.Core;
using Stugan.Rooms;

// 1. Skapa världen (Rum och föremål)

WorldMap karta = new WorldMap();
Rum startRum = SkapaVarlden(karta);

// 2. Initiera spelaren
Spelare spelare = new Spelare(startRum);

// 3. Starta motorn
Spelmotor motor = new Spelmotor(spelare, karta);
motor.Starta();

// --- Lokala funktioner (Längst ner i filen) ---

Rum SkapaVarlden(WorldMap karta)
{
    Gardsplan gardsplan = new Gardsplan();
    gardsplan.Plats = new Position(0, 0, 0);
    karta.LaggTillRum(gardsplan.Plats, gardsplan);

    Hall hall = new Hall();
    hall.Plats = new Position(0, 1, 0);
    karta.LaggTillRum(hall.Plats, hall);

    Kok kok = new Kok();
    kok.Plats = new Position(-1, 1, 0);
    karta.LaggTillRum(kok.Plats, kok);

    Kallare kallare = new Kallare();
    kallare.Plats = new Position(0, 1, -1);
    karta.LaggTillRum(kallare.Plats, kallare);


    InreHall hall2 = new InreHall();
    hall2.Plats = new Position(0, 2, 0);
    karta.LaggTillRum(hall2.Plats, hall2);

    Vardagsrum vardagsrum = new Vardagsrum();
    vardagsrum.Plats = new Position(-1, 2, 0);
    karta.LaggTillRum(vardagsrum.Plats, vardagsrum);

    Glasveranda glasveranda = new Glasveranda();
    glasveranda.Plats = new Position(-2, 2, 0);
    karta.LaggTillRum(glasveranda.Plats, glasveranda);

    return gardsplan;
}