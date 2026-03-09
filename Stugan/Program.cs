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
    Hall hall = new Hall();
    Kok kok = new Kok();
    Kallare kallare = new Kallare();
    InreHall hall2 = new InreHall();
    Vardagsrum vardagsrum = new Vardagsrum();
    Glasveranda glasveranda = new Glasveranda();

    gardsplan.Plats = new Position(0, 0, 0);
    karta.LaggTillRum(gardsplan.Plats, gardsplan);

    hall.Plats = new Position(0, 1, 0);
    karta.LaggTillRum(hall.Plats, hall);

    kok.Plats = new Position(-1, 1, 0);
    karta.LaggTillRum(kok.Plats, kok);

    kallare.Plats = new Position(1, 1, -1);
    karta.LaggTillRum(kallare.Plats, kallare);

    hall2.Plats = new Position(0, 2, 0);
    karta.LaggTillRum(hall2.Plats, hall2);

    vardagsrum.Plats = new Position(-1, 2, 0);
    karta.LaggTillRum(vardagsrum.Plats, vardagsrum);

    glasveranda.Plats = new Position(-2, 2, 0);
    karta.LaggTillRum(glasveranda.Plats, glasveranda);

    return gardsplan;
}