using OpenAI;
using OpenAI.Chat;
using OpenAI.Models;
using System;
using System.ClientModel;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace AdventureQuest
{
    internal class TheGuildAdvisor
    {
        private QuestManagment questManager;

        private Login login;

        public TheGuildAdvisor(QuestManagment questManager, Login login)
        {
            this.questManager = questManager;
            this.login = login;
        }

        private readonly ChatClient _chatClient;
        private readonly List<ChatMessage> _conversation;

        string apiKey = Environment.GetEnvironmentVariable("OPENAI_API_KEY");
        string Chatinput;

        // we have a simple system that connects your API key to chatgpt in the program. 
        public async Task GuildAI()
        {
            // our modelname
            var modelName = "gpt-4o";
            var client = new ChatClient(modelName, apiKey);

            // introductory message.
            Console.WriteLine("Guildmaster: Greetings, adventurer! What do you seek today? If you want to leave type 'leave'");
            Console.WriteLine();
           
            while (true)
            {
                // the you: and AI: will make it easier for the user to understand who is speaking.
                Console.Write("You: ");
                Chatinput = Console.ReadLine()?.Trim();

                // Exit immediately if user wants to leave
                if (string.IsNullOrWhiteSpace(Chatinput))
                {
                    MainMenu mainMenu = new MainMenu(questManager, login);
                    mainMenu.Menu();
                    Console.Clear();
                    break;
                }
                // if chatinput is to leave it exits back to menu.
                if (Chatinput.ToLower() == "leave")
                {
                    Console.WriteLine("Guildmaster: Farewell, brave one. May your quests be glorious!");
                    MainMenu mainMenu = new MainMenu(questManager, login);
                    mainMenu.Menu();
                    Console.Clear();
                    break;
                }

                // Only call AI if it's not leave
                var response = client.CompleteChat(Chatinput);
                Console.WriteLine($"AI: {response.Value.Content[0].Text}");
                Console.WriteLine("Hint!: once you're done type leave.");
            }

        }
    }
}