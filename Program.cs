namespace AdventureQuest
{
    internal class Program
    {
        static void Main(string[] args)
        {
            QuestManagment questManager = new QuestManagment();

            // the method for the whole login process
            Login loginMsg = new Login();
            loginMsg.LoginMessage();

            MainMenu menuChoices = new MainMenu(questManager);
            menuChoices.Menu();

        }


    }
}