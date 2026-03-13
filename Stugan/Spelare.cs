namespace Stugan
{
    public class Spelare
    {
        public Rum NuvarandeRum { get; set; }

        public Core.Position? Position => NuvarandeRum.Plats;

        public Inventory Ryggsack { get; private set; }

        public string AktivtSkodon { get; set; } // T.ex. "Ytterskor", "Tofflor", "Stövlar"

        public bool ArSkadad { get; set; }

        public Spelare(Rum startRum)
        {
            NuvarandeRum = startRum;
            Ryggsack = new Inventory();
            Ryggsack.LaggTill(new Klader(
                    "Boots",
                    "Dina rejäla, leriga kängor.",
                    Bekladnadstyp.Ute, Kroppsdel.Fot, BekladnadsLager.Mellan,
                    true,  // Skyddar mot vatten
                    true,  // Kan plockas upp
                    false, // Inte gömda
                    "Du snörar på dig de tunga bootsen.",
                    "Du sparkar av dig bootsen med en lättad suck."
                )
            { ArAktiv = true }); // Den här lilla måsvingen sätter egenskapen direkt!

            AktivtSkodon = "ute";
            ArSkadad = false;
        }

        public string AktivtSkodonNamn
        {
            get
            {
                // Vi letar i ryggsäcken efter det plagg som är aktivt 
                // och som matchar de vanliga skotyperna.
                var skodon = Ryggsack.GetAllaSaker()
                    .OfType<Klader>()
                    .FirstOrDefault(k => k.ArAktiv && (k.Typ == Bekladnadstyp.Inne || k.Typ == Bekladnadstyp.Ute));

                // Om vi hittar ett plagg, returnera dess namn. 
                // Annars returnera en tom sträng.
                return skodon?.Namn ?? string.Empty;
            }
        }
    }
}
