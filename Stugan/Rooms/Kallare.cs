using static System.Console;

namespace Stugan.Rooms
{
    public class Kallare : Rum
    {
        public Kallare() : base("Källaren",
                    "Trappan gnisslar betänkligt... Luften är kall och luktar fuktig jord. " +
                    "Ett släpande ljud hörs inifrån mörkret.")
        {
            // Vi lägger in tofflorna här direkt
            SakerIRummet.Add(new Klader("Innetofflor", "Mjuka innetofflor för fina mattor.", "inne", false, true, true, false));
        }

        public override void UndersokRum(Spelare spelare)
        {
            var rum = spelare.NuvarandeRum;

            // Vi letar i rummets lista efter saken som heter "Innetofflor"
            var tofflor = rum.SakerIRummet.FirstOrDefault(s => s.Namn.Equals("Innetofflor", StringComparison.OrdinalIgnoreCase));

            // 1. Ljudlogik för JAWS (Fötterna)
            if (string.IsNullOrEmpty(spelare.AktivtSkodon))
            {
                WriteLine("\nDet iskalla källarvattnet klafsar obehagligt mellan tårna.");
            }
            else if (spelare.AktivtSkodon == "Boots")
            {
                WriteLine("\nDina tunga boots dunsar mot betongen. De håller vätan ute men lortar ner.");
            }
            else if (spelare.AktivtSkodon == "Plasttofflor")
            {
                WriteLine("\nDet 'ploppar' hemtrevligt om plasttofflorna i vätan.");
            }

            // 2. Själva sökandet
            if (tofflor != null && tofflor.ArGomd)
            {
                // Här hittar vi dem!
                WriteLine("\nDu undersöker den torra hyllan högt upp på väggen.");
                WriteLine("Dina fingrar nuddar något mjukt... det är innetofflorna!");

                // VIKTIGT: Vi sätter ArGomd till false så de blir synliga i rummet/kan tas upp
                tofflor.ArGomd = false;

                WriteLine("\nEtt svagt, belåtet mumlande hörs från mörkret.");
            }
            else if (tofflor != null && !tofflor.ArGomd)
            {
                // Om vi redan har hittat dem men inte plockat upp dem
                WriteLine("\nDu ser innetofflorna ligga på hyllan där du hittade dem.");
            }
            else
            {
                // Om 'tofflor' är null (dvs hittas inte i listan alls)
                WriteLine("\nDu letar noga på hyllorna men hittar inget mer än damm och spindelväv.");
            }
        }
    }
}