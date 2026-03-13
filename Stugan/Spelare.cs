namespace Stugan
{
    public class Spelare
    {
        public Rum NuvarandeRum { get; set; }

        public Core.Position? Position => NuvarandeRum.Plats;

        public Inventory Ryggsack { get; private set; }

        public string AktivtSkodon { get; set; } // Vanlig egenskap så den kan ändras!

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
            AktivtSkodon = Bekladnadstyp.Ute.ToString();
            ArSkadad = false;
        }

        public string AktivtSkodonNamn
        {
            get
            {
                var skodon = Ryggsack.GetAllaSaker()
                    .OfType<Klader>()
                    .FirstOrDefault(k => k.ArAktiv && (k.Typ == Bekladnadstyp.Inne || k.Typ == Bekladnadstyp.Ute || k.Typ == Bekladnadstyp.Skydd));

                return skodon?.Namn ?? string.Empty;
            }
        }
    }
}
