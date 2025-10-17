using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventureQuest
{
    internal class MainMenu
    {
        // need to declare our variables here so that it carries over our quests from questmanagment.
        private QuestManagment questManager;

        public MainMenu(QuestManagment questManager)
        {
            this.questManager = questManager;
        }
        public void Menu()
        {

            // Menu for quests or exiting program
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("You sit down at the tavern. You inspect the choices before you.");
            Console.WriteLine("1. Quest Managment");
            Console.WriteLine("2. Notifications");
            Console.WriteLine("3. Guild advisor");
            Console.WriteLine("4. Exit.");

            // the readline to choose
            string MenuChoice = Console.ReadLine();
            // small menu to pick between options after login


            switch (MenuChoice)
            {
                case "1":
                    questManager.QuestMenu();
                    break;

                case "2":
                    // this is where we will add notifications with twilio
                    Console.WriteLine("You see a pigeon arriving with some mail.");
                    Notifications notifierSystem = new Notifications(questManager);
                    notifierSystem.NotificationMenu();
                    break;

                case "3":
                    Console.WriteLine("You exit the tavern and head back to town.");
                    Environment.Exit(0);
                    break;

                default:
                    Console.WriteLine("Invalid choice. Please select a valid option.");
                    break;
            }


        }
    }
}
