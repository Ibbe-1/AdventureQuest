using OpenAI.Chat;

namespace AdventureQuest
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // for our login
            Login loginMsg = new Login();
            loginMsg.LoginMessage();

            // for the quest manager
            QuestManagment questManager = new QuestManagment(loginMsg);

            // this is our main menu that contains our program.
            MainMenu menuChoices = new MainMenu(questManager, loginMsg);
            menuChoices.Menu();


        }


    }
}