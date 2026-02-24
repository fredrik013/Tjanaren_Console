using DavyKager;
using Stugan;
using static System.Console;
using System.Linq;

// Snygg avslutning för JAWS vid stängning
AppDomain.CurrentDomain.ProcessExit += (s, e) =>
{
    Tolk.Unload();
};

// ==========================================
// 1. STARTA LÄSKAMRATEN
// ==========================================
Tolk.Load();

// ==========================================
// 2. SKAPA VÄRLDEN
// ==========================================
var garden = new Rum("Gårdsplanen", "Du står på en grusad gårdsplan utanför stugan i Ljungsbro. Norr ser du en inbjudande glasveranda.");

var verandan = new Rum("Verandan", "Du står på verandan.");
garden.Koppla("UpArrow", verandan);
verandan.Koppla("DownArrow", garden);

var hallen = new Rum("Hallen", "Du är i hallen. Till vänster finns köket och söderut ser du verandan. Vid väggen står en gammal skohylla.");
var sax = new Spelsak("Sax", "En rostig skräddarsax.");
hallen.Saker.Add(sax);

verandan.Koppla("UpArrow", hallen);
hallen.Koppla("DownArrow", verandan);

var koket = new Rum("Köket", "Ett hemtrevligt kök. Hallen ligger till höger.");
hallen.Koppla("LeftArrow", koket);
koket.Koppla("RightArrow", hallen);

// ==========================================
// 3. INITIALISERING
// ==========================================
Rum nuvarandeRum = garden;
List<Spelsak> ryggsack = new List<Spelsak>();
bool speletKors = true;

Clear();
WriteLine("(Piltangenter: Gå | I: Ryggsäck | T: Ta upp | U: Undersök | Alt+F4: Avsluta)");
WriteLine("----------------------------------");

// Prata ut startläget
Prata(nuvarandeRum.Beskrivning);

// ==========================================
// 4. SPEL-LOOPEN (MOTORN)
// ==========================================
while (speletKors)
{
    var knapp = ReadKey(true);

    if (knapp.Key == ConsoleKey.I)
    {
        HanteraRyggsack(ryggsack);
        VisaRum(nuvarandeRum);
    }
    else if (knapp.Key == ConsoleKey.T)
    {
        if (nuvarandeRum.Saker.Count > 0)
        {
            var sakAttTa = nuvarandeRum.Saker[0];
            ryggsack.Add(sakAttTa);
            nuvarandeRum.Saker.Remove(sakAttTa);

            Clear();
            // Vi pratar bara bekräftelsen för att undvika tjat
            Prata($"Du plockar upp {sakAttTa.Namn}.");

            // Resten skrivs tyst för markören
            WriteLine($"\nPlats: {nuvarandeRum.Namn}");
            foreach (var sak in nuvarandeRum.Saker)
            {
                WriteLine($"Kvar i rummet: {sak.Namn}.");
            }
        }
        else
        {
            Prata("Här finns inget att plocka upp.");
        }
    }
    else if (knapp.Key == ConsoleKey.U)
    {
        if (nuvarandeRum == hallen && !hallen.HarUndersokts)
        {
            var tofflor = new Spelsak("Tofflor", "Ett par varma, mjuka tofflor.");
            hallen.Saker.Add(tofflor);
            hallen.HarUndersokts = true;

            Clear();
            // Prata bara det nya fyndet
            Prata("Du letar igenom skohyllan och hittar ett par tofflor!");

            // Skriv rumsinfo tyst
            WriteLine($"\n{nuvarandeRum.Beskrivning}");
            foreach (var sak in nuvarandeRum.Saker)
            {
                WriteLine($"Här ser du: {sak.Namn}.");
            }
        }
        else if (nuvarandeRum.HarUndersokts)
        {
            Prata("Du ser inget mer av intresse här.");
        }
        else
        {
            Prata("Du ser inget särskilt när du tittar närmare.");
            nuvarandeRum.HarUndersokts = true;
        }
    }
    else if (knapp.Key == ConsoleKey.Escape)
    {
        // Tyst läge
    }
    else
    {
        string riktning = knapp.Key.ToString();
        if (nuvarandeRum.Utgangar.ContainsKey(riktning))
        {
            var nastaRum = nuvarandeRum.Utgangar[riktning];

            // PUSSEL-KONTROLL
            if (nuvarandeRum == hallen && nastaRum == koket)
            {
                bool harTofflor = ryggsack.Any(s => s.Namn.Equals("Tofflor", StringComparison.OrdinalIgnoreCase));

                if (!harTofflor)
                {
                    Prata("Stopp! Du kan inte gå in i köket med ytterskorna på. Det är nystädat!");
                }
                else
                {
                    nuvarandeRum = nastaRum;
                    VisaRum(nuvarandeRum);
                }
            }
            else
            {
                nuvarandeRum = nastaRum;
                VisaRum(nuvarandeRum);
            }
        }
        else
        {
            Prata("Det tar stopp där.");
        }
    }
}

// ==========================================
// 5. VERKTYGSLÅDAN
// ==========================================

void VisaRum(Rum rum)
{
    Clear();
    Prata(rum.Beskrivning);
    foreach (var sak in rum.Saker)
    {
        Prata($"Här ser du: {sak.Namn}.");
    }
}

void Prata(string text)
{
    WriteLine(text);
    Tolk.Output(text);
}

void HanteraRyggsack(List<Spelsak> ryggsack)
{
    if (ryggsack.Count == 0)
    {
        Prata("Ryggsäcken är tom.");
        return;
    }

    int index = 0;
    bool tittarIRyggsack = true;
    Prata($"I ryggsäcken: {ryggsack[index].Namn}.");

    while (tittarIRyggsack)
    {
        var k = ReadKey(true);
        if (k.Key == ConsoleKey.DownArrow)
        {
            if (index < ryggsack.Count - 1)
            {
                index++;
                Prata(ryggsack[index].Namn);
            }
            else
            {
                Prata($"Slut på listan. {ryggsack[index].Namn}");
            }
        }
        else if (k.Key == ConsoleKey.UpArrow)
        {
            if (index > 0)
            {
                index--;
                Prata(ryggsack[index].Namn);
            }
            else
            {
                Prata($"Början på listan. {ryggsack[index].Namn}");
            }
        }
        else if (k.Key == ConsoleKey.Escape || k.Key == ConsoleKey.I)
        {
            Prata("Du stänger ryggsäcken.");
            tittarIRyggsack = false;
        }
    }
}