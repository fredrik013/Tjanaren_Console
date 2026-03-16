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
    }
}
