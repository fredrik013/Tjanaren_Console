using DavyKager;
using Stugan;
using static System.Console;
using System.Linq;

AppDomain.CurrentDomain.ProcessExit += (s, e) => { Tolk.Unload(); };
Tolk.Load();

var garden = new Rum("Gårdsplanen", "Du står på en grusad gårdsplan utanför stugan i Ljungsbro. Norr ser du en inbjudande glasveranda.");
var verandan = new Rum("Verandan", "Du står på verandan.");
var hallen = new Rum("Hallen", "Du är i hallen. Till vänster finns köket och söderut ser du verandan. Vid väggen står en gammal skohylla.");
var koket = new Rum("Köket", "Ett hemtrevligt kök. Hallen ligger till höger.");

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

Clear();
WriteLine("(Piltangenter: Gå | I: Ryggsäck | T: Ta upp | U: Undersök | Alt+F4: Avsluta)");
WriteLine("----------------------------------");
WriteLine(spelare.NuvarandeRum.Beskrivning);

while (speletKors)
{
    var knapp = ReadKey(true);

    if (knapp.Key == ConsoleKey.I)
    {
        HanteraRyggsack(spelare);
        Clear();
        WriteLine(spelare.NuvarandeRum.Beskrivning);
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
            WriteLine(spelare.NuvarandeRum.HarUndersokts ? "Du ser inget mer här." : "Du ser inget särskilt.");
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
                if (spelare.AktivtSkodon != "Tofflor")
                {
                    WriteLine("Stopp! Du kan inte gå in i köket med ytterskorna på. Det är nystädat!");
                }
                else
                {
                    BytRum(nastaRum, spelare);
                }
            }
            else
            {
                BytRum(nastaRum, spelare);
            }
        }
    }
}

void BytRum(Rum nasta, Spelare s)
{
    s.NuvarandeRum = nasta;
    Clear();
    System.Threading.Thread.Sleep(150);
    WriteLine(s.NuvarandeRum.Beskrivning);
}

void HanteraRyggsack(Spelare s)
{
    var ryggsack = s.Ryggsack;
    if (ryggsack.Count == 0)
    {
        Tolk.Output("Ryggsäcken är tom.");
        return;
    }

    int index = 0;
    bool tittar = true;

    Tolk.Output($"Ryggsäck. {ryggsack.Count} föremål. {ryggsack[index].Namn}");
    Clear();
    WriteLine(ryggsack[index].Namn);

    while (tittar)
    {
        var k = ReadKey(true);

        if (k.Key == ConsoleKey.DownArrow)
        {
            if (index < ryggsack.Count - 1)
            {
                index++;
                Tolk.Output(ryggsack[index].Namn);
                WriteLine(ryggsack[index].Namn);
            }
            else Tolk.Output("Slut på listan.");
        }
        else if (k.Key == ConsoleKey.UpArrow)
        {
            if (index > 0)
            {
                index--;
                Tolk.Output(ryggsack[index].Namn);
                WriteLine(ryggsack[index].Namn);
            }
            else Tolk.Output("Början på listan.");
        }
        else if (k.Key == ConsoleKey.Enter)
        {
            var valdSak = ryggsack[index];
            if (valdSak.Namn.ToLower().Contains("tofflor"))
            {
                s.AktivtSkodon = "Tofflor";
                Tolk.Output("Du tar på dig tofflorna.");
                tittar = false;
                System.Threading.Thread.Sleep(500);
            }
            else Tolk.Output($"Kan inte använda {valdSak.Namn}.");
        }
        else if (k.Key == ConsoleKey.Escape || k.Key == ConsoleKey.I)
        {
            Tolk.Output("Stänger ryggsäcken.");
            tittar = false;
        }
    }
}