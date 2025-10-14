namespace AdventureQuest
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // the method for the whole login process
            Login loginMsg = new Login();
            loginMsg.LoginMessage();

            MainMenu menuChoices = new MainMenu();
            menuChoices.Menu();

        }


    }
}