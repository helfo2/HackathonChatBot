using System;
using System.Collections.Generic;
using System.Linq;

namespace FleetChatBotServer.ChatBot.Configuration
{
    public class ChatBotPredicates
    {
        public KeyValuePair<string, int> this[string predicateKey]
        {
            get
            {
                var predicate = ToDictionary().FirstOrDefault(x => predicateKey.Contains(x.Key));
                if (predicate.Equals(default))
                {
                    throw new Exception($"Predicated {predicateKey} not found.");
                }
                return predicate;
            }
        }

        public List<ChatbotPredicate> Equipment { get; set; }

        private Dictionary<string, int> ToDictionary()
        {
            return Equipment.ToDictionary(x => x.Name, x => x.Key);
        }
    }
}
