using OpenAI;
using OpenAI.Chat;
using OpenAI.Models;
using System;
using System.ClientModel;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AdventureQuest
{
    internal class TheGuildAdvisor
    {
        private QuestManagment questManager;

        public TheGuildAdvisor(QuestManagment questManager)
        {
            this.questManager = questManager;
        }

        private readonly ChatClient _chatClient;
        private readonly List<ChatMessage> _conversation;
        
        public async Task GuildAI()
        {
         
        }
    }
}
