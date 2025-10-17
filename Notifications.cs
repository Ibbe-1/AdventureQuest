using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Twilio;
using Twilio.Rest.Api.V2010.Account;
using Twilio.Types;


namespace AdventureQuest
{
    internal class Notifications
    {
        // need to declare the variables here so that it carries over our chosen quests from questmanagment.
        private QuestManagment questManager;

        public Notifications(QuestManagment questManager)
        {
            this.questManager = questManager;
        }
        List<QuestManagment.QuestTemplate> quests;



        public void NotificationMenu()
        {
            Console.WriteLine("The mail instructs you about how to use the notification system.");
            Console.WriteLine("Are you interested in checking your notifications?");
            Console.WriteLine("1. Yes, I'm interested.");
            Console.WriteLine("2. No, send the pigeon on its way.");
            string NotificationChoice = Console.ReadLine();

            switch (NotificationChoice)
            {
                case "1":
                    Console.WriteLine("You proceed to look through the mail the pigeon brought you.");
                    Console.WriteLine("Press a key to continue...");
                    Console.ReadLine();
                    Console.Clear();
                    NotificationSystem(questManager.quests);
                    break;  

                case "2":
                    Console.WriteLine("The pigeon flutters away as you proceed on with your duties.");
                    Console.WriteLine("Press a key to continue...");
                    Console.ReadLine();
                    Console.Clear();
                    MainMenu mainMenu = new MainMenu(questManager);
                    mainMenu.Menu(); 
                    break;

                default:
                    Console.WriteLine("Invalid choice, please select 1 or 2.");
                    NotificationMenu(); 
                    break;
            }
        }
        public void NotificationSystem(List<QuestManagment.QuestTemplate> activeQuests)
        {
            this.quests = activeQuests;
            // we start off by putting in all our information at the start and letting twilio initialize.
            Console.WriteLine("Welcome hero to the notification system. Please input your phone number.");
            string UserPhoneNumber = Console.ReadLine();
            string AuthenticatorTwilioPhone = Environment.GetEnvironmentVariable("TWILIO_PHONENUMBER");
            string accountSID = Environment.GetEnvironmentVariable("TWILIO_SID");
            string authToken = Environment.GetEnvironmentVariable("TWILIO_AUTHTOKEN");

            // we use a quest notify system by searching through which quests have the status in progress
            var questToNotify = questManager.quests.FirstOrDefault(q => q.QuestStatus == QuestManagment.Status.InProgress);

            // if something is wrong with twilio or user did not put in phone number it will put user in a loop that sends them back to notificationsmenu.
            if (string.IsNullOrEmpty(UserPhoneNumber) ||
            string.IsNullOrEmpty(AuthenticatorTwilioPhone) ||
            string.IsNullOrEmpty(accountSID) ||
            string.IsNullOrEmpty(authToken))
            {
                Console.WriteLine("Something went wrong with verifying your phone number or twilio. ");
                Console.WriteLine("Press a key to continue..");
                Console.ReadLine();
                Console.Clear();
                NotificationMenu();
                return;
            }
            // initialize twilio client
            TwilioClient.Init(accountSID, authToken);

            // if there is no quests it will send you back to notifications menu
            if (quests == null || !quests.Any(q => q.QuestStatus == QuestManagment.Status.InProgress))
            {
                Console.WriteLine("You have no quests in progress to notify about.");
                Console.WriteLine("Press any key to return to the main menu...");
                Console.ReadKey();
                Console.Clear();

                NotificationMenu();
                return;
            }

            // we create a new variable called inprogressquests which check the list of quest with the status in progress.
            var inProgressQuests = quests
            .Where(q => q.QuestStatus == QuestManagment.Status.InProgress)
            .ToList();

            // checking for quests that are inprogress and if the timespan is under 24h then it will notify the user through their phone
            foreach (var quest in inProgressQuests)
            {
                TimeSpan timeLeft = quest.QuestDueDate - DateTime.Now;

                if (timeLeft.TotalHours <= 24 && timeLeft.TotalHours > 0)
                {
                    {
                        string messageBody = $"Reminder: The quest '{quest.QuestName}' is due on {quest.QuestDueDate:MMMM dd, yyyy}. Time is running out!";

                        var message = MessageResource.Create(
                            body: messageBody,
                            from: new PhoneNumber(AuthenticatorTwilioPhone),
                            to: new PhoneNumber(UserPhoneNumber)
                           
                           
                        );
                    }
                    // once notification has been sent it will redirect the user to notificationsmenu
                    Console.WriteLine("A notification has been sent to your phone.");
                    Console.WriteLine("Press any key to continue.");
                    Console.ReadLine();
                    Console.Clear();
                    NotificationMenu();
                }
            }
        }
    }
}

