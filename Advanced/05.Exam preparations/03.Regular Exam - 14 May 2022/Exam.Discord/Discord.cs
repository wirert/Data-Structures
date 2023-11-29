using System;
using System.Collections.Generic;
using System.Linq;

namespace Exam.Discord
{
    public class Discord : IDiscord
    {
        private Dictionary<string, Message> messagesById =  new Dictionary<string, Message>();

        private Dictionary<string, HashSet<Message>> channelMessages = new Dictionary<string, HashSet<Message>>();

        public int Count => messagesById.Count;

        public bool Contains(Message message) => messagesById.ContainsKey(message.Id);

        public void DeleteMessage(string messageId)
        {
            if (!messagesById.ContainsKey(messageId))
            {
                throw new ArgumentException();
            }
            var message = messagesById[messageId];
            messagesById.Remove(messageId);
            channelMessages[message.Channel].Remove(message);

            if (channelMessages[message.Channel].Count == 0)
            {
                channelMessages.Remove(message.Channel);
            }
        }

        public IEnumerable<Message> GetAllMessagesOrderedByCountOfReactionsThenByTimestampThenByLengthOfContent()
            => messagesById.Values
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

        public Message GetMessage(string messageId)
        {
            if (!messagesById.ContainsKey(messageId))
            {
                throw new ArgumentException();
            }

            return messagesById[messageId];
        }

        public IEnumerable<Message> GetMessageInTimeRange(int lowerBound, int upperBound)
            => messagesById.Values
            .Where(m => m.Timestamp >= lowerBound && m.Timestamp <= upperBound)
            .OrderByDescending(m => channelMessages[m.Channel].Count);

        public IEnumerable<Message> GetMessagesByReactions(List<string> reactions)
            => messagesById.Values
                .Where(m =>  reactions.Except(m.Reactions).Count() == 0)
                .OrderByDescending(m => m.Reactions.Count)
                .ThenBy(m => m.Timestamp);

        public IEnumerable<Message> GetTop3MostReactedMessages()
            => messagesById.Values
            .OrderByDescending(m => m.Reactions.Count)
            .Take(3);

        public void ReactToMessage(string messageId, string reaction)
        {
            if (!messagesById.ContainsKey(messageId))
            {
                throw new ArgumentException();
            }

            messagesById[messageId].Reactions.Add(reaction);
        }

        public void SendMessage(Message message)
        {
            if (messagesById.ContainsKey(message.Id))
            {
                throw new ArgumentException();
            }

            messagesById.Add(message.Id, message);
            if (!channelMessages.ContainsKey(message.Channel))
            {
                channelMessages.Add(message.Channel, new HashSet<Message>());
            }

            channelMessages[message.Channel].Add(message);
        }
    }
}
