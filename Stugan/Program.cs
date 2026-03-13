using Stugan;
using Stugan.Core;
using Stugan.Rooms;

WorldMap karta = new WorldMap();
Rum startRum = SkapaVarlden(karta);

Spelare spelare = new Spelare(startRum);

Spelmotor motor = new Spelmotor(spelare, karta);
motor.Starta();

Rum SkapaVarlden(WorldMap karta)
{
    Gardsplan gardsplan = new Gardsplan();
    gardsplan.Plats = new Position(0, 0, 0);
    karta.LaggTillRum(gardsplan.Plats, gardsplan);

    Skjul skjul = new Skjul();
    skjul.Plats = new Position(1, 0, 0);
    karta.LaggTillRum(skjul.Plats, skjul);


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

    Tradgard tradgard = new Tradgard();
    tradgard.Plats = new Position(-2, 1, 0);
    karta.LaggTillRum(tradgard.Plats, tradgard);
    gardsplan.Koppla("Vaster", tradgard); // Pil Vänster på gården -> Trädgården
    tradgard.Koppla("Soder", gardsplan);  // Pil Höger i trädgården -> Gården

    return gardsplan;
}