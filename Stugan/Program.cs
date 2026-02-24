using DavyKager;
using Stugan;
using static System.Console;

// Snygg avslutning för JAWS vid Alt+F4 eller stängning
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
var garden = new Rum("Gårdsplanen", "Du står på en grusad gårdsplan utanför stugan i Ljungsbro. Dörren till hallen är öppen mot norr.");

var hallen = new Rum("Hallen", "Du är i hallen. Till vänster finns köket. Söderut ser du gårdsplanen.");
var sax = new Spelsak("Sax", "En rostig skräddarsax som ser ut att kunna klippa igenom det mesta.");
hallen.Saker.Add(sax);

garden.Koppla("UpArrow", hallen);
hallen.Koppla("DownArrow", garden);

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
// Här skriver vi bara instruktionerna på skärmen (tyst)
WriteLine("(Piltangenter: Gå | I: Ryggsäck | T: Ta upp | Alt+F4: Avsluta)");
WriteLine("----------------------------------");

// Här pratar vi bara EN gång
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
            Prata($"Du plockar upp {sakAttTa.Namn}.");
        }
        else
        {
            Prata("Här finns inget att plocka upp.");
        }
    }
    else if (knapp.Key == ConsoleKey.Escape)
    {
        // Tyst
    }
    else
    {
        string riktning = knapp.Key.ToString();
        if (nuvarandeRum.Utgangar.ContainsKey(riktning))
        {
            nuvarandeRum = nuvarandeRum.Utgangar[riktning];
            VisaRum(nuvarandeRum);
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