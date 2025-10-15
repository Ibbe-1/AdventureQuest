using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventureQuest
{
    internal class MainMenu
    {
        public void Menu()
        {
            // Menu for quests or exiting program
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("You sit down at the tavern. You inspect the choices before you.");
            Console.WriteLine("1. Quest Managment");
            Console.WriteLine("2. Notifications");
            Console.WriteLine("3. Exit.");

            // the readline to choose
            string MenuChoice = Console.ReadLine();
            // small menu to pick between options after login

            switch (MenuChoice)
            {
                case "1":
                    QuestManagment questPicker = new QuestManagment();
                    questPicker.QuestMenu();
                    break;

                case "2":
                    // this is where we will add notifications with twilio
                    Console.WriteLine("You see a pigeon arriving with some mail.");
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
