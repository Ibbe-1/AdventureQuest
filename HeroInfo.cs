using AdventureQuest;

internal class HeroInfo
{
    // we save all of this here so that the program does not forget our choices from questmanager or the login username.
    private QuestManagment questManager;
    private Login login;
    private static string heroClass = "Warrior"; // Default class 
    private static bool hasChosenClass = true;

    public HeroInfo(QuestManagment questManager, Login login)
    {
        this.questManager = questManager;
        this.login = login;
    }

    // our display hero info
    public void DisplayHeroInfo()
    {
        Console.Clear();
        // will display the name from user register
        Console.WriteLine($"Hero name: {login.UserRegister}");
        // will display class, default right now is warrior.
        Console.WriteLine($"Hero class: {heroClass}");
        // everytime a new quest has been chosen, it will display it here.
        Console.WriteLine($"Current active quests: {questManager.quests.Count(q => q.QuestStatus == QuestManagment.Status.InProgress)}");

        Console.WriteLine("Options:");
        Console.WriteLine("1. Change class");
        Console.WriteLine("2. Return to menu");
        // to pick which class or to leave
        string classInput = Console.ReadLine()?.Trim();

        switch (classInput)
        {
            case "1":
                ChangeClass();
                break;

            case "2":
            case "leave":
                MainMenu menuChoices = new MainMenu(questManager, login);
                menuChoices.Menu();
                break;

            default:
                Console.WriteLine("Invalid input. Try again.");
                DisplayHeroInfo();
                break;
        }
    }

    private void ChangeClass()
    {
        bool validChoice = false;

        while (!validChoice)
        {
            Console.WriteLine("Pick a new class:");
            Console.WriteLine("1. Warrior");
            Console.WriteLine("2. Mage");
            Console.WriteLine("3. Archer");

            string classChoice = Console.ReadLine()?.Trim();

            switch (classChoice)
            {
                case "1":
                    heroClass = "Warrior";
                    validChoice = true;
                    break;

                case "2":
                    heroClass = "Mage";
                    validChoice = true;
                    break;

                case "3":
                    heroClass = "Archer";
                    validChoice = true;
                    break;

                default:
                    Console.WriteLine("Invalid choice. Please try again.");
                    break;
            }
        }

        Console.WriteLine($"You have chosen the path of the {heroClass}!");
        DisplayHeroInfo();
    }
}
