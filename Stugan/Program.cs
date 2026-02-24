using DavyKager;
using static System.Console;

// 1. Försök starta JAWS-kopplingen
Tolk.Load();

if (Tolk.IsLoaded())
{
    // 2. Om det lyckas, tala om det både i konsolen och via JAWS
    WriteLine("Succé! Tolk hittade din skärmläsare.");
    Tolk.Output("Tolk är laddad och redo för stugan!");

    WriteLine("Tryck på en tangent för att testa en rumsbeskrivning...");
    ReadKey(true);

    // 3. Testa att skicka en text som JAWS ska läsa upp direkt
    Tolk.Output("Du står utanför stugan i Ljungsbro. Det doftar Eriksberg Karaktär från verandan.", true);
}
else
{
    // 4. Om något saknas (t.ex. en DLL-fil inte hittades i bin-mappen)
    WriteLine("Kunde inte ladda Tolk.");
    WriteLine("Kontrollera att Tolk.dll och jfwapi.dll ligger i din bin-mapp.");
}

WriteLine("\nTryck på valfri tangent för att avsluta testet.");
ReadKey(true);

// 5. Viktigt: Stäng ner kopplingen ordentligt
Tolk.Unload();