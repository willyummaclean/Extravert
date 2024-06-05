// See https://aka.ms/new-console-template for more information
using System.Diagnostics.Metrics;
using System.Dynamic;

List<Plant> plants = new List<Plant>()
{
    new Plant()
    {
        Species = "Football",
        LightNeeds = 1,
        AskingPrice = 12.67M,
        City = "Wichita",
        ZIP = 20101,
        Sold = false,
        AvailableUntil = new DateTime(2024, 12, 31)
    },
    new Plant()
    {
        Species = "Hockey Stick",
        LightNeeds = 2,
        AskingPrice = 43.12M,
        City = "NY",
        ZIP = 20093,
        Sold = false,
        AvailableUntil = new DateTime(2024, 12, 31)
    },
    new Plant()
    {
        Species = "Golf Club",
        LightNeeds = 1,
        AskingPrice = 23.11M,
        City = "LA",
        ZIP = 20002,
        Sold = false,
        AvailableUntil = new DateTime(2023, 12, 31)
    },
    new Plant()
    {
        Species = "Decoy",
        LightNeeds = 5,
        AskingPrice = 9.09M,
        City = "Provo",
        ZIP = 20014,
        Sold = false,
        AvailableUntil = new DateTime(2023, 12, 31)
    },
     new Plant()
    {
        Species = "Shoes",
        LightNeeds = 3,
        AskingPrice = 12.22M,
        City = "Boston",
        ZIP = 19825,
        Sold = false,
        AvailableUntil = new DateTime(2024, 12, 31)
    },
    new Plant()
    {
        Species = "Tennis Racket",
        LightNeeds = 4,
        AskingPrice = 67.54M,
        City = "Austin",
        ZIP = 20067,
        Sold = true,
        AvailableUntil = new DateTime(2024, 12, 31)
    }
};
Plant plantOfTheDay;
Random random = new Random();
do
{
    int randomIndex = random.Next(plants.Count);
    plantOfTheDay = plants[randomIndex];
} while (plantOfTheDay.Sold);


string greeting = @"Welcome to ExtraVert
Your one-stop shop for used plants";
Console.WriteLine(greeting);
MainMenu();

void MainMenu()
{
    string choice = null;
    while (choice != "0")
    {
        Console.WriteLine(@"Choose an option:
                        0. Exit
                        1. Display all plants
                        2. Post a plant to be adopted
                        3. Adopt a plant
                        4. Delist a plant
                        5. Plant of the Day
                        6. Search By Light Needs
                        7. View App Stats");
        choice = Console.ReadLine();
        if (choice == "0")
        {
            string exitChoice = null;
            while (exitChoice != "0")
            {
                Console.WriteLine(@"Choose an option:
                        0. Exit
                        1. Return to Menu");
                exitChoice = Console.ReadLine();
                if (exitChoice == "0")
                {
                    Console.WriteLine("Goodbye!");
                }
                else if (exitChoice == "1")
                {
                    MainMenu();
                }
            }
        }
        else if (choice == "1")
        {
            DisplayAllPlants();
        }
        else if (choice == "2")
        {
            PostAPlant();
        }
        else if (choice == "3")
        {
            AdoptAPlant();
        }
        else if (choice == "4")
        {
            DelistAPlant();
        }
        else if (choice == "5")
        {
            PlantOfTheDay();
        }
        else if (choice == "6")
        {
            SearchByLightNeeds();
        }
        else if (choice == "7")
        {
            ViewAppStats();
        }
        else
        {
            Console.WriteLine("Make a better Choice");
            Console.WriteLine("Press Any Key to Continue");
            Console.ReadKey();
            Console.Clear();
            MainMenu();

        }
    }
};

void DisplayAllPlants()
{
    for (int i = 0; i < plants.Count; i++)
    {
        Console.WriteLine($"{i + 1}. {plants[i].Species} in {plants[i].City} {(plants[i].Sold ? "was sold for" : "is available for")} {plants[i].AskingPrice} {(plants[i].Sold ? "" : $"until {plants[i].AvailableUntil}")}");

    }
}


void PostAPlant()
{
    Console.WriteLine("What type of Plant are we talking about?");
    string species = Console.ReadLine();
    Console.WriteLine("Where exactly is the heckin thing?");
    string city = Console.ReadLine();
    Console.WriteLine("ZIP code?");
    string zip = Console.ReadLine();
    Console.WriteLine("On a scale from 1-5 how much light does it need?");
    string light = Console.ReadLine();
    Console.WriteLine("How much you looking for?");
    string price = Console.ReadLine();
    Console.WriteLine("Please submit the last date this plant is available in the format year,month,day i.e.  2024,7,15.");

    // string availableUntil = "";
    // try
    // {
    //     availableUntil = Console.ReadLine();
    // }
    // catch (FormatException)
    // {
    //     Console.WriteLine("Invalid date.");
    //     MainMenu();
    // }
    // catch (Exception ex)
    // {
    //     Console.WriteLine(ex);
    //     Console.WriteLine("Do Better!");
    //     MainMenu();
    // }

    DateTime availableUntil;
    while (true)
    {
        string dateInput = Console.ReadLine();
        try
        {
            availableUntil = DateTime.ParseExact(dateInput, "yyyy,M,d", null);
            break; // Exit the loop if the date is valid
        }
        catch (FormatException)
        {
            Console.WriteLine("Invalid date format. Please enter the date in the format year,month,day (e.g., 2024,7,15).");
        }
    }

    plants.Add(
    new Plant()
    {
        Species = species,
        LightNeeds = Convert.ToInt32(light),
        AskingPrice = Convert.ToDecimal(price),
        City = city,
        ZIP = Convert.ToInt32(zip),
        Sold = false,
        AvailableUntil = Convert.ToDateTime(availableUntil)

    }
    );
    Console.WriteLine("Your plant has been added.");
}

void AdoptAPlant()
{
    List<Plant> adoptablePlants = new List<Plant>();

    foreach (Plant item in plants)
    {

        if (!item.Sold && item.AvailableUntil >= DateTime.Now)
        {
            adoptablePlants.Add(item);
        }
    }


    Console.WriteLine("Please Select a Plant");
    for (int i = 0; i < adoptablePlants.Count; i++)
    {
        Console.WriteLine($"{i + 1}. {adoptablePlants[i].Species} in {adoptablePlants[i].City} {(adoptablePlants[i].Sold ? "was sold for" : "is available for")} {adoptablePlants[i].AskingPrice}");

    }
    string selectPlant = Console.ReadLine();
    adoptablePlants[Convert.ToInt32(selectPlant) - 1].Sold = true;
}

void DelistAPlant()
{
    Console.WriteLine("Please Select a Plant");
    for (int i = 0; i < plants.Count; i++)
    {
        Console.WriteLine($"{i + 1}. {plants[i].Species} in {plants[i].City} {(plants[i].Sold ? "was sold for" : "is available for")} {plants[i].AskingPrice}");

    }
    string selectPlant = Console.ReadLine();
    int plantIndex = Convert.ToInt32(selectPlant) - 1;
    plants.RemoveAt(plantIndex);

}

void PlantOfTheDay()
{
    Console.WriteLine($"The Plant of the Day is: {plantOfTheDay.Species} in {plantOfTheDay.City}. It's light needs are {plantOfTheDay.LightNeeds} out of 5. It  is available for {plantOfTheDay.AskingPrice}");
}

void SearchByLightNeeds()
{
    Console.WriteLine("On a scale from 1-5, what is the maximum amount of light can provide for a plant?");
    int lightChoice = Convert.ToInt32(Console.ReadLine());

    if (lightChoice <= 0 || lightChoice > 5)
    {
        Console.WriteLine("Make a better choice");
        Console.WriteLine("Press Any Key to Continue");
        Console.ReadKey();
        Console.Clear();
    }

    List<Plant> lightPlants = new List<Plant>();

    foreach (Plant item in plants)
    {

        if (item.LightNeeds <= lightChoice)
        {
            lightPlants.Add(item);
        }
    }

    if (lightPlants.Count == 0)
    {

        Console.WriteLine("There aren't any plants that meet your requirements. :''(");

    }
    else
    {
        Console.WriteLine("These plants meet your requirements:");

        for (int i = 0; i < lightPlants.Count; i++)
        {
            Console.WriteLine($"{i + 1}. {lightPlants[i].Species} in {lightPlants[i].City} {(lightPlants[i].Sold ? "was sold for" : "is available for")} {lightPlants[i].AskingPrice}");

        }
    }
}

void ViewAppStats()
{
    List<Plant> adoptablePlants = new List<Plant>();

    foreach (Plant item in plants)
    {

        if (!item.Sold && item.AvailableUntil >= DateTime.Now)
        {
            adoptablePlants.Add(item);
        }
    }


    Console.WriteLine($"There are {adoptablePlants.Count} adoptable plants.");

    List<Plant> availablePlants = new List<Plant>();

    foreach (Plant item in plants)
    {

        if (item.Sold)
        {
            availablePlants.Add(item);
        }
    }

    double percentAdopted = Convert.ToDouble(availablePlants.Count) / Convert.ToDouble(plants.Count);

    Console.WriteLine($"{percentAdopted * 100}% of the plants have already been adopted.");

    double lightTotal = new double();
    foreach (Plant item in plants)
    {
        lightTotal += Convert.ToDouble(item.LightNeeds);
    }
    double lightAverage = lightTotal / Convert.ToDouble(plants.Count());

    Console.WriteLine($"The average light need of our plants is {lightAverage} out of 5");

    Plant highestLightNeed = plants[0];
    for (int i = 0; i < plants.Count; i++)

    {

        if (plants[i].LightNeeds > highestLightNeed.LightNeeds)
        {
            highestLightNeed = plants[i];
        }

    }
    Console.WriteLine($"The plant with the highest light needed is {PlantDetails(highestLightNeed)}");

    Plant lowestPrice = plants[0];
    for (int i = 0; i < plants.Count; i++)

    {

        if (plants[i].AskingPrice < lowestPrice.AskingPrice)
        {
            lowestPrice = plants[i];
        }

    }
    Console.WriteLine($"The lowest priced plant is {PlantDetails(lowestPrice)}");
}

string PlantDetails(Plant plant)
{
    string plantString = plant.Species;

    return plantString;

}



