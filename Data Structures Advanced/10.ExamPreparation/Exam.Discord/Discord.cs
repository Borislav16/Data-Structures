using System;
using System.Collections.Generic;
using System.Linq;

namespace Exam.Discord
{
    public class Discord : IDiscord
    {
        private Dictionary<string, Message> discord;
        private Dictionary<string, List<Message>> channelMessages;

        public Discord()
        {
            discord = new Dictionary<string, Message>();
            channelMessages = new Dictionary<string, List<Message>>();
        }

        public int Count
            => discord.Count;

        public bool Contains(Message message)
            => discord.ContainsKey(message.Id);

        public void SendMessage(Message message)
        {
            discord.Add(message.Id, message);

            if (!channelMessages.ContainsKey(message.Channel))
            {
                channelMessages.Add(message.Channel, new List<Message>());
            }

            channelMessages[message.Channel].Add(message);
        }

        public void DeleteMessage(string messageId)
        {
            if (!discord.ContainsKey(messageId))
            {
                throw new ArgumentException();
            }

            var messageToDeleteInChannel =  channelMessages[discord[messageId].Channel]
                .FirstOrDefault(m => m.Id == messageId);

            if (messageToDeleteInChannel == null)
            {
                throw new ArgumentException();
            }
            else
            {
                channelMessages[discord[messageId].Channel].Remove(messageToDeleteInChannel);
            }

            discord.Remove(messageId);
        }

        public Message GetMessage(string messageId)
        {
            if (!discord.ContainsKey(messageId))
            {
                throw new ArgumentException();
            }

            return discord[messageId];
        }

        public IEnumerable<Message> GetAllMessagesOrderedByCountOfReactionsThenByTimestampThenByLengthOfContent()
            => discord.Values
                .OrderByDescending(m => m.Reactions.Count)
                .ThenBy(m => m.Timestamp)
                .ThenBy(m => m.Content.Length);

        public IEnumerable<Message> GetChannelMessages(string channel)
        {
            if (!channelMessages.ContainsKey(channel))
            {
                throw new ArgumentException();
            }

            return channelMessages[channel];
        }

        public IEnumerable<Message> GetMessageInTimeRange(int lowerBound, int upperBound)
            => discord.Values
                .Where(m => m.Timestamp >= lowerBound && m.Timestamp <= upperBound)
                .OrderByDescending(m => m.Channel);

        public IEnumerable<Message> GetMessagesByReactions(List<string> reactions)
            => discord.Values
                .Where(m => m.Reactions.Intersect(reactions).Any())
                .OrderByDescending(m => m.Reactions.Count)
                .ThenByDescending(m => m.Timestamp);

        public IEnumerable<Message> GetTop3MostReactedMessages()
            => discord.Values
                .OrderByDescending(m => m.Reactions.Count)
                .Take(3);

        public void ReactToMessage(string messageId, string reaction)
        {
            if (!discord.ContainsKey(messageId))
            {
                throw new ArgumentException();
            }

            discord[messageId].Reactions.Add(reaction);
        }
    }
}