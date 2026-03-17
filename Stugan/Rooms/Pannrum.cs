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
            var aktivaSkydd = s.Ryggsack.GetAllaSaker().OfType<Klader>().Where(k => k.ArAktiv && k.Typ == Bekladnadstyp.Skydd);

            // Vi switchar direkt på det första "falska" tillståndet vi hittar
            switch (true)
            {
                case bool _ when s.AktivtSkodon != "Skydd":
                    WriteLine("Det ser farligt ut där inne. Du behöver nog någon form av skyddsutrustning.");
                    return false;

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
                    return true;
            }
        }
    }
}
