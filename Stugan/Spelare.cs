namespace Stugan
{
    public class Spelare
    {
        public Rum NuvarandeRum { get; set; }

        public Inventory Ryggsack { get; private set; }

        public string AktivtSkodon { get; set; } // T.ex. "Ytterskor", "Tofflor", "Stövlar"

        public bool ArSkadad { get; set; }

        public Spelare(Rum startRum)
        {
            NuvarandeRum = startRum;
            Ryggsack = new Inventory();
            AktivtSkodon = "Ytterskor";
            ArSkadad = false;
        }
    }
}