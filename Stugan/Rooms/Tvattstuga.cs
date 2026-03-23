using Stugan.Modeller.Saker;

namespace Stugan.Rooms
{
    public class Tvattstuga : Rum
    {
        public Tvattstuga() : base("Tvättstugan", "Du har kommit in i en tvättstuga. Här råder det god ordning och det luktar starkt av tvättmedel.Här finns det både tvättmaskin och torktumlare..")
        {
            VisaNamn = "i tvättstugan";
            OppnaUtgangar("Oster");

            SakerIRummet.Add(new Inredning("Tvättmaskin", "En modern tvättmaskin som ser ut att vara i gott skick. Den är fylld med smutsiga kläder som väntar på att bli rena.", false, false));
            SakerIRummet.Add(new Inredning("Torktumlare", "En torktumlare som matchar tvättmaskinen."));

        }
    }
}
