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

        // to remember the username of the user
        private Login login;
        // to remember our class and name of hero
        private HeroInfo hero;
        // this is my old constructor that i have to  preserve so that the program doesn't break.
        public MainMenu(QuestManagment questManager)
        {
            this.questManager = questManager;
        }

        // new constructor for so that our login variables get saved. 
        public MainMenu(QuestManagment questManager, Login login)
        {
            this.questManager = questManager;
            this.login = login;
            this.hero = new HeroInfo(questManager, login);
        }

        public async Task Menu()
        {

            // Menu for quests or exiting program
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("You sit down at the tavern. You inspect the choices before you.");
            Console.WriteLine("1. Quest Managment");
            Console.WriteLine("2. Notifications");
            Console.WriteLine("3. Guild advisor");
            Console.WriteLine("4. Hero Page");
            Console.WriteLine("5. Exit.");

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
                    Notifications notifierSystem = new Notifications(questManager, login);
                    notifierSystem.NotificationMenu();
                    break;

                case "3":
                    Console.WriteLine("You approach the guild advisor.");
                    TheGuildAdvisor guildAdvisor = new TheGuildAdvisor(questManager, login);
                    await guildAdvisor.GuildAI();
                    break;


                case "4":
                    // have to rewrite this section due to issues with old constructor leading to program being null sometimes. 
                    if (login == null)
                    {
                        Console.WriteLine("Cannot access Hero Page: login information is missing.");
                        Console.WriteLine("Press any key to return to menu...");
                        Console.ReadKey();
                        Menu();
                        break; // return to menu without crashing
                    }

                    if (hero == null)
                    {
                        hero = new HeroInfo(questManager, login);
                    }

                    hero.DisplayHeroInfo();
                    break;

                case "5":
                    Console.WriteLine("You leave the town and rest in a nearby village.");
                    Environment.Exit(0);
                    break;

                default:
                    Console.WriteLine("Invalid choice. Please select a valid option.");
                    break;
            }


        }
    }
}
