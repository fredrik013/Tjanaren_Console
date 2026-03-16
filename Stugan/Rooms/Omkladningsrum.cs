namespace Stugan.Rooms
{
    public class Omkladningsrum : Rum
    {
        public Omkladningsrum() : base("Omklädningsrummet", "Du har kommit in i ett omklädningsrum. Det är här arbetarna byter om. Runt väggarna finns det bänkar")
        {
            VisaNamn = "i omklädningsrummet";
            OppnaUtgangar("Vaster");
            SakerIRummet.Add(new Klader("Overall", "Det här verkar vara en skyddsoverall av något slag. Den ser ut att tåla vatten.", Bekladnadstyp.Skydd, Kroppsdel.Helakroppen, BekladnadsLager.Ytterst, true, true, false));
            SakerIRummet.Add(new Klader(
    "Arbetshandskar",
    "Ett par kraftiga gummihandskar som ser ut att tåla både fukt och smuts.",
    Bekladnadstyp.Skydd,
    Kroppsdel.Hand,
    BekladnadsLager.Ytterst,
    true, // Vattentäta
    true,
    false
));
        }
    }
}
