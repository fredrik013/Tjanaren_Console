namespace Stugan.Rooms
{
    public class Gardsplan : Rum
    {
        public Gardsplan() : base("Gårdsplanen", "Du står på en grusad gårdsplan. Den röda stugan med sina vita knutar ser inbjudande ut i solskenet. " +
        "Rakt framför dig, åt norr, leder en gammal trädörr in till huset.")
        {
            SakerIRummet.Add(new Redskap(
                            "Spade",
                            "En rostig men rejäl spade.",
                            true,
                            false
                        ));
        }

        public override void UndersokRum(Spelare spelare)
        {
        }
    }
}
