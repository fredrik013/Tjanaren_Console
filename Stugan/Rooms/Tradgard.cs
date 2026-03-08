using static System.Console;

namespace Stugan.Rooms
{
    public class Tradgard : Rum
    {
        public Tradgard() : base("Trädgården",
            "Du kliver ut i den vildvuxna trädgården. Gräset är högt och fuktigt, " +
            "och doften av våt jord är stark. Här och var ser man spår av gamla odlingar.")
        {
            // Nu använder vi de korrekta namnen från basen: KanPlockasUpp och ArGomd
            SakerIRummet.Add(new AllmanSak("Rörtång", "En tung och lite rostig rörtång. Perfekt för trilskande rör.", true, true));
        }

        public override void UndersokRum(Spelare spelare)
        {
            var rortang = SakerIRummet.FirstOrDefault(s => s.Namn == "Rörtång");

            if (rortang != null && rortang.ArGomd)
            {
                rortang.ArGomd = false;
                WriteLine("\nDu rotar runt i det höga gräset och den våta jorden.");
                WriteLine("Där, halvt begravd under några vissna växter, hittar du en rörtång!");
                WriteLine("Den ser ut att ha legat där ett tag, men den fungerar nog fortfarande.");
            }
            else
            {
                WriteLine("\nDu ser inget annat än blött gräs och lera här ute.");
            }
        }

        public override bool KanGaIn(Spelare s)
        {
            switch (s.AktivtSkodon.ToLower())
            {
                case "inne":
                    WriteLine("\n[EN STRÄNG RÖST FRÅN VERANDAN]");
                    WriteLine("'Men vad sysslar du med?! Gå inte ut här och skita ner dina tofflor och sen klampa runt inne!'");
                    WriteLine("'Visa lite hyfs och byt om till något som tål lera innan du sätter din fot på gräsmattan.'");
                    return false;

                case "ute":
                    WriteLine("\nDet klafsar till när bootsen möter den mjuka, våta jorden.");
                    WriteLine("Du ser hur sulorna omedelbart täcks av ett tjockt lager lera.");
                    WriteLine("En röst inifrån muttrar: 'Den där figuren får snart sparken om han fortsätter i den här stilen...'");
                    return true;

                default: // Barfota
                    WriteLine("\nDu kliver ut barfota i det våta gräset. Det är kallt och klibbigt mellan tårna,");
                    WriteLine("men du slipper i alla fall att dra in lera med grova sulor.");
                    return true;
            }
        }
    }
}