using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventureQuest
{
    internal class QuestManagment
    {

        public void QuestMenu()
        {
            Console.ForegroundColor = ConsoleColor.DarkBlue;
            Console.WriteLine("You approach the quest board. You see several quests available:");
            Console.WriteLine("1. Rescue cat for old lady");
            Console.WriteLine("2. Defeat goblins that are haunting the woods");
            Console.WriteLine("3. Delve into a newly discovered dungeon");
            Console.WriteLine("4. Exit to Main Menu");
            string questChoice = Console.ReadLine();

            // dont forget to make later a remove for selected quests 
            // make later a way to track completed quests
            // make it into switch
            // make get set for quests

            switch (questChoice)
            {
                case "1":
                    Console.WriteLine("You've decided to help the old lady");
                    break;

                case "2":
                    Console.WriteLine("You feel a tinge of courage as you decide to hunt goblins");
                    break;

                case "3":
                    Console.WriteLine("Your mind races at the excitement of newly discovered treasures.");
                    break;

                case "4":
                    Console.WriteLine("Returning to main menu...");
                    MainMenu menuChoices = new MainMenu();
                    menuChoices.Menu();
                    break;

                default:
                    Console.WriteLine("Invalid choice. Please select a valid option.");
                    QuestMenu(); // Redisplay the quest menu
                    break;
            }

            // new method to see info about quest and due date before quest ends

            ///////


        }
    }

}

