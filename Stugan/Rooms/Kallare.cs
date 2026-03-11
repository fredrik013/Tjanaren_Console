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
            SakerIRummet.Add(new Klader("Innetofflor", "Mjuka innetofflor för fina mattor.", Bekladnadstyp.Inne, Kroppsdel.Fot, BekladnadsLager.Mellan, false, true, true, "Du tar på dig tofflorna. Nu behöver du inte frysa om fötterna och finfolket kan inte klaga på några smutsiga skor.", "Du tar av dig tofflorna."));
        }

        public override void UndersokRum(Spelare spelare)
        {
            var rum = spelare.NuvarandeRum;

            // Vi letar i rummets lista efter saken som heter "Innetofflor"
            var tofflor = rum.SakerIRummet.FirstOrDefault(s => s.Namn.Equals("Innetofflor", StringComparison.OrdinalIgnoreCase));

            // 1. Hitta plagget (utan ToLower-slarv)
            var p = spelare.Ryggsack.GetAllaSaker()
                .OfType<Klader>()
                .FirstOrDefault(p => p.ArAktiv && (p.Typ == Bekladnadstyp.Ute || p.Typ == Bekladnadstyp.Inne));
            // 2. Switchen som sköter allt
            switch (p)
            {
                case null:
                    // Spelaren är barfota
                    WriteLine("\nDet iskalla källarvattnet klafsar obehagligt mellan tårna. Du blir dyngsur!");
                    break;

                case var k when !k.SkyddarMotVatten:
                    // Har skor, men de läcker (Använder Enum istället för sträng)
                    string ljudLäck = (k.Typ == Bekladnadstyp.Ute) ? "dunsar" : "ploppar";
                    WriteLine($"\nDina skor {ljudLäck} i vätan, men fukten tränger igenom. De skyddar inte mot vatten!");
                    break;

                case var k when k.SkyddarMotVatten:
                    // Har skor och de är täta (Använder Enum istället för sträng)
                    string ljudTät = (k.Typ == Bekladnadstyp.Ute) ? "dunsar tungt" : "ploppar hemtrevligt";
                    WriteLine($"\nDina skor {ljudTät} mot betongen och håller dina fötter torra.");
                    break;
            }
            // 2. Själva sökandet
            if (tofflor != null && tofflor.ArGomd)
            {
                // Här hittar vi dem!
                WriteLine("\nDu undersöker den torra hyllan högt upp på väggen.");
                WriteLine("Dina fingrar nuddar något mjukt... det är ett par innetofflor!");

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