using static System.Console;

namespace Stugan.Rooms
{
    public class Kok : Rum
    {
        public Kok() : base("Köket",
            "Du kommer in i ett ljust och rymligt kök. Här doftar det av nybakat bröd och en hint av humle. " +
            "Köksbordet står dukat vid fönstret.")
        {
            // Vi lägger till sakerna direkt här
            SakerIRummet.Add(new AllmanSak("Eriksberg", "En immande kall Eriksberg Karaktär."));
            SakerIRummet.Add(new AllmanSak("Lunchbröd", "Ett nybakat bröd, perfekt för en vardagslunch.", true, true));
        }

        public override void UndersokRum(Spelare spelare)
        {
            var rum = spelare.NuvarandeRum;
            var brod = rum.SakerIRummet.FirstOrDefault(s => s.Namn == "Lunchbröd");

            if (brod != null && brod.ArGomd)
            {
                brod.ArGomd = false; // Nu dyker det upp i rummets lista!
                WriteLine("\nDu undersöker det dukade bordet och ser bland annat ett brödfat som är täckt med en handduk.");
                WriteLine("Du lyfter på handduken och hittar lunchbröd!");
            }
            else
            {
                WriteLine("\nKöket är rent och snyggt. Brödfatet står tomt på bordet.");
            }
        }

        public override bool KanGaIn(Spelare s)
        {
            if (s.AktivtSkodon == "Boots")
            {
                WriteLine("Stopp! Du kan inte gå in i köket med leriga boots.");
                return false;
            }
            return true;
        }
    }
}