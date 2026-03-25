namespace Stugan.Rooms
{
    public class Matkallare : Rum
    {
        public Matkallare() : base("Matkällaren", "Du är i en matkällare. Det är kallt och fuktigt här. Det finns en dörr som leder ut.")
        {
            VisaNamn = "i matkällaren";
            OppnaUtgangar("Soder");

        }
    }
}
