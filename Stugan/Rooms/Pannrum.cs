using static System.Console;

namespace Stugan.Rooms
{
    public class Pannrum : Rum
    {
        public Pannrum() : base("Pannrummet", "Du har kommit in i ett pannrum. Här står det decimeterdjupt med vatten." + "Du ser direkt att det kommer från ett läckande rör.", false)
        {
            VisaNamn = "i pannrummet";
            OppnaUtgangar("Vaster");
            SakerIRummet.Add(new AllmanSak("Rör", "Ett läckande rör. Du måste ha ett verktyg för att kunna laga det.", false, false));
        }

        public override bool KanGaIn(Spelare s)
        {
            // Vi hämtar ALLA aktiva skyddskläder en gång för alla
            var aktivaSkydd = s.Ryggsack.GetAllaSaker()
                .OfType<Klader>()
                .Where(k => k.ArAktiv && k.Typ == Bekladnadstyp.Skydd)
                .ToList();

            // 1. "Naken-kontrollen" - har man inget skydd alls på sig?
            if (!aktivaSkydd.Any())
            {
                WriteLine("Det ser farligt ut där inne. Du behöver nog någon form av skyddsutrustning.");
                return false;
            }

            // 2. Switchen kollar nu bara VAD som saknas i listan
            switch (true)
            {
                case bool _ when !aktivaSkydd.Any(k => k.Placering == Kroppsdel.Fot):
                    WriteLine("Du kan inte gå in där utan skydd på fötterna, det är alldeles för blött.");
                    return false;

                case bool _ when !aktivaSkydd.Any(k => k.Placering == Kroppsdel.Helakroppen):
                    WriteLine("Du behöver skydd för kroppen innan du kliver in.");
                    return false;

                case bool _ when !aktivaSkydd.Any(k => k.Placering == Kroppsdel.Hand):
                    WriteLine("Du kan inte gå in utan skydd för händerna.");
                    return false;

                default:
                    // Om vi har något skydd (if-satsen ovan) och inget saknas i switchen...
                    return true;
            }
        }
    }
}
