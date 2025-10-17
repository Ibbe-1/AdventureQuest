using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace AdventureQuest
{
    internal class QuestManagment
    {

        // we make an enum for our quest status
        public enum Status
        {
            NotStarted,
            InProgress,
            Completed
        }
        
        public class QuestTemplate
        {
            // template for all our quests
            public string QuestName { get; set; }
            public string QuestDescription { get; set; }
            public DateTime QuestDueDate { get; set; }
            public string QuestPriority { get; set; }
            public Status QuestStatus { get; set; }
        }


        public void QuestMenu()
        {
            Console.Clear();
            // for our quest menu 
            Console.ForegroundColor = ConsoleColor.DarkBlue;
            Console.WriteLine("You approach the quest board in the tavern. What would you like to do?");
            Console.WriteLine("1. Add quests.");
            Console.WriteLine("2. Update quests");
            Console.WriteLine("3. Complete quest.");
            Console.WriteLine("4. None of these interest me. leave.");

            switch (Console.ReadLine())
            {
                case "1":
                    AddQuest();
                    break;
                case "2":
                    UpdateQuest();
                    break;
                case "3":
                    CompleteQuest();
                    break;
                case "4":
                    Console.Clear();
                    Console.WriteLine("You leave the quest board and head back to the tavern.");
                    MainMenu ReturnToMain = new MainMenu(this);
                    ReturnToMain.Menu();
                    break;
                default:
                    Console.WriteLine("Invalid choice. Please select a valid option.");
                    QuestMenu();
                    break;
            }
        }

        public List<QuestTemplate> quests = new List<QuestTemplate>();
        public QuestTemplate selectedQuest;

        // method to display quest details
        public void DisplayQuest(QuestTemplate quest)
        {
            switch (quest.QuestStatus)
            {
                case Status.NotStarted:
                    Console.ForegroundColor = ConsoleColor.Red;
                    break;
                case Status.InProgress:
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    break;
                case Status.Completed:
                    Console.ForegroundColor = ConsoleColor.Green;
                    break;
            }
            // this is how we utilize our get set template
            Console.WriteLine($"Quest: {quest.QuestName}");
            Console.WriteLine($"Description: {quest.QuestDescription}");
            Console.WriteLine($"Due Date: {quest.QuestDueDate:MMMM dd, yyyy}");
            Console.WriteLine($"Priority: {quest.QuestPriority}");
            Console.WriteLine($"Status: {quest.QuestStatus}");

            
        }

        public void AddQuest()
        {
            if (quests.Count == 0)
            {
                quests.AddRange(new List<QuestTemplate>
         {
                 new QuestTemplate
            {
                QuestName = "Help old lady to find her cat",
                QuestDescription = "An old lady needs assistance recovering her cat from a nearby tree.",
                QuestDueDate = DateTime.Now.AddDays(1),
                QuestPriority = "Low",
                QuestStatus = Status.NotStarted
            },
            new QuestTemplate
            {
                QuestName = "Hunt Goblins",
                QuestDescription = "A nearby village is being terrorized by goblins. They need a hero to help.",
                QuestDueDate = DateTime.Now.AddDays(3),
                QuestPriority = "Medium",
                QuestStatus = Status.NotStarted
            },
            new QuestTemplate
            {
                QuestName = "Find Lost Treasure",
                QuestDescription = "There's newly discovered ruins with hidden treasures. Get on it before the others.",
                QuestDueDate = DateTime.Now.AddDays(7),
                QuestPriority = "High",
                QuestStatus = Status.NotStarted
            }
            });
            }

            // we sort them into lists and make a small quest board
            // we display here the quest board with all the quests. We start from 0 up to 3 with different width
            Console.WriteLine("Quest Board:");
            Console.WriteLine("-----------------------------------------------------------");
            Console.WriteLine("{0,-30} {1,-12} {2,-8} {3,-12}", "Quest Name", "Due Date", "Priority", "Status");
            Console.WriteLine("-----------------------------------------------------------");

            // we do the same foreach quest variable
            foreach (var quest in quests)
            {
                // so we change colours depending on status of quest
                switch (quest.QuestStatus)
                {
                    case Status.NotStarted:
                        Console.ForegroundColor = ConsoleColor.Red;
                        break;
                    case Status.InProgress:
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        break;
                    case Status.Completed:
                        Console.ForegroundColor = ConsoleColor.Green;
                        break;
                }


                Console.WriteLine("{0,-30} {1,-12} {2,-8} {3,-12}",
                quest.QuestName,
                quest.QuestDueDate.ToShortDateString(),
                quest.QuestPriority,
                quest.QuestStatus);
            }

            Console.WriteLine("-----------------------------------------------------------");



            // dont forget to make later a remove for selected quests 
            // make later a way to track completed quests
            // make it into switch
            // make get set for quests

            // we ask the user to pick a quest by typing in the name or if they want to leave.
            Console.WriteLine("Tell the guest keeper the name of the quest you want to accept or type 'leave' to leave.");
            string questChoice = Console.ReadLine()?.Trim().ToLower();

            if (questChoice == "leave")
            {
                Console.Clear();
                Console.WriteLine("You leave the quest board and head back to the tavern.");
                MainMenu ReturnToMain = new MainMenu(this);
                ReturnToMain.Menu();
            }

            // we make a bool method to match the quest name with input
            bool MatchingQuest(QuestTemplate quest, string input)
            {
                return quest.QuestName.ToLower().Contains(input);
            }
            // we make a loop to go through the quests and see if there is a match

            selectedQuest = quests.FirstOrDefault(q => q.QuestName.ToLower().Contains(questChoice));
            
            if (selectedQuest != null)
            {
                selectedQuest.QuestStatus = Status.InProgress;

                DisplayQuest(selectedQuest);

                Console.WriteLine("The quest keeper nods approvingly and wishes you luck on your journey.");
                Console.WriteLine("Press any key to continue.");
                Console.ReadKey();
                QuestMenu();
            }
            // if there is no match you will be redirected to questmenu again.
            else
            {
                Console.WriteLine("The quest keeper responds that there is no quest with that name.");
                Console.WriteLine("Try telling him again.");
                AddQuest();
            }
        }
        // new method to see info about quest and due date before quest ends

        ///////
        ///
        public void UpdateQuest()
        {
            // if there is no quests then we cant proceed. Only picks quests that are in progress
            var updatableQuests = quests.Where(q => q.QuestStatus == Status.InProgress).ToList();

            if (updatableQuests.Count == 0)
            {
                Console.WriteLine("No quests available to update.");
                Console.ReadKey();
                QuestMenu();
                return;
            }

            Console.WriteLine("You look at the piece of paper the quest keeper gave you. You use a pen to modify the contents. Type the name of the quest");
            // we display our available quests here.
            updatableQuests.ForEach(q => Console.WriteLine($"- {q.QuestName}"));

            string QuestUpdateInput = Console.ReadLine()?.Trim().ToLower();
            var questToUpdate = updatableQuests.FirstOrDefault(q => q.QuestName.ToLower().Contains(QuestUpdateInput));

            // if there is no match you will be redirected to questmenu again.
            if (questToUpdate == null)
            {
                Console.WriteLine("No quest found with that name.");
                Console.ReadKey();
                QuestMenu();
                return;
            }
                Console.WriteLine("1. Try pushing for extended deadline?");
                Console.WriteLine("2. Change priority level?");
                Console.WriteLine("3. Status?");
            // string so that the user can choose between what they want updated.
            string updatechoice = Console.ReadLine();
            switch (updatechoice)
            {
                case "1":
                    Console.WriteLine("Enter number of extra days to extend:");
                    if (int.TryParse(Console.ReadLine(), out int days))
                        questToUpdate.QuestDueDate = questToUpdate.QuestDueDate.AddDays(days);
                    QuestMenu();
                    break;

                case "2":
                    Console.WriteLine("Enter new priority (Low, Medium, High):");
                    questToUpdate.QuestPriority = Console.ReadLine();
                    QuestMenu();
                    break;

                case "3":
                    Console.WriteLine("Enter new status (NotStarted, InProgress, Completed):");
                    if (Enum.TryParse(Console.ReadLine(), out Status newStatus))
                        questToUpdate.QuestStatus = newStatus;
                    QuestMenu();
                    break;

                default:
                    Console.WriteLine("Invalid choice.");
                    break;
                    }
                DisplayQuest(questToUpdate);
        }

        public void CompleteQuest()
        {   // we can only complete quests that are in progress and are selected.
            if (selectedQuest != null && selectedQuest.QuestStatus == Status.InProgress)
            {
                Console.WriteLine($"You approach the guest keeper to hand in your quest.");
                selectedQuest.QuestStatus = Status.Completed;
                DisplayQuest(selectedQuest);

                Console.WriteLine("The quest keeper congratulates you and gives your reward. Quest completed!");
                Console.WriteLine("Return back to quest board.");
                Console.WriteLine("Press any key to continue.");
                Console.ReadKey();
                QuestMenu();
            }

        }
    }
}


