using Stugan;
using static System.Console;
using System.Linq;

// ==========================================
// 1. INITIALISERING
// ==========================================
// Vi skapar rummen med dina originalbeskrivningar
var garden = new Rum("Gårdsplanen", "Du står på en grusad gårdsplan utanför stugan i Ljungsbro. Norr ser du en inbjudande glasveranda.");
var verandan = new Rum("Verandan", "Du står på verandan.");
var hallen = new Rum("Hallen", "Du är i hallen. Till vänster finns köket och söderut ser du verandan. Vid väggen står en gammal skohylla.");
var koket = new Rum("Köket", "Ett hemtrevligt kök. Hallen ligger till höger.");

// Kopplingar
garden.Koppla("UpArrow", verandan);
verandan.Koppla("DownArrow", garden);
verandan.Koppla("UpArrow", hallen);
hallen.Koppla("DownArrow", verandan);
hallen.Koppla("LeftArrow", koket);
koket.Koppla("RightArrow", hallen);

var sax = new Spelsak("Sax", "En rostig skräddarsax.");
hallen.Saker.Add(sax);

Spelare spelare = new Spelare(garden);
bool speletKors = true;

// Starta skärmen
Clear();
WriteLine("(Piltangenter: Gå | I: Ryggsäck | T: Ta upp | U: Undersök | Alt+F4: Avsluta)");
WriteLine("----------------------------------");
WriteLine(spelare.NuvarandeRum.Beskrivning);

// ==========================================
// 2. SPEL-LOOPEN
// ==========================================
while (speletKors)
{
    var knapp = ReadKey(true);

    if (knapp.Key == ConsoleKey.I)
    {
        HanteraRyggsack(spelare.Ryggsack);
        // Efter ryggsäcken visar vi var vi är igen utan att rensa allt
        WriteLine($"\nDu är kvar på: {spelare.NuvarandeRum.Namn}");
    }
    else if (knapp.Key == ConsoleKey.T)
    {
        if (spelare.NuvarandeRum.Saker.Count > 0)
        {
            var sakAttTa = spelare.NuvarandeRum.Saker[0];
            spelare.Ryggsack.Add(sakAttTa);
            spelare.NuvarandeRum.Saker.Remove(sakAttTa);
            WriteLine($"Du plockar upp {sakAttTa.Namn}.");
        }
        else WriteLine("Här finns inget att plocka upp.");
    }
    else if (knapp.Key == ConsoleKey.U)
    {
        if (spelare.NuvarandeRum == hallen && !hallen.HarUndersokts)
        {
            var tofflor = new Spelsak("Tofflor", "Ett par varma, mjuka tofflor.");
            hallen.Saker.Add(tofflor);
            hallen.HarUndersokts = true;
            WriteLine("Du letar igenom skohyllan och hittar ett par tofflor!");
        }
        else
        {
            WriteLine(spelare.NuvarandeRum.HarUndersokts ? "Du ser inget mer av intresse här." : "Du ser inget särskilt när du tittar närmare.");
            spelare.NuvarandeRum.HarUndersokts = true;
        }
    }
    else
    {
        string riktning = knapp.Key.ToString();
        if (spelare.NuvarandeRum.Utgangar.ContainsKey(riktning))
        {
            var nastaRum = spelare.NuvarandeRum.Utgangar[riktning];

            if (spelare.NuvarandeRum == hallen && nastaRum == koket)
            {
                if (!spelare.Ryggsack.Any(s => s.Namn.ToLower() == "tofflor"))
                {
                    WriteLine("Stopp! Du kan inte gå in i köket med ytterskorna på. Det är nystädat!");
                }
                else
                {
                    spelare.NuvarandeRum = nastaRum;
                                        Clear();
                                        WriteLine(spelare.NuvarandeRum.Beskrivning);
                }
            }
            else
            {
                spelare.NuvarandeRum = nastaRum;
                Clear();
                                WriteLine(spelare.NuvarandeRum.Beskrivning);
            }
        }
    }
}

// ==========================================
// 3. HJÄLPMETODER
// ==========================================

void HanteraRyggsack(List<Spelsak> ryggsack)
{
    if (ryggsack.Count == 0) { WriteLine("Ryggsäcken är tom."); return; }

    int index = 0;
    bool tittar = true;
    WriteLine($"I ryggsäcken: {ryggsack[index].Namn}.");

    while (tittar)
    {
        var k = ReadKey(true);
        if (k.Key == ConsoleKey.DownArrow && index < ryggsack.Count - 1)
        {
            index++;
            WriteLine(ryggsack[index].Namn);
        }
        else if (k.Key == ConsoleKey.UpArrow && index > 0)
        {
            index--;
            WriteLine(ryggsack[index].Namn);
        }
        else if (k.Key == ConsoleKey.Escape || k.Key == ConsoleKey.I)
        {
            WriteLine("Du stänger ryggsäcken.");
            tittar = false;
        }
    }
}