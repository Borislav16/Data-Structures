using System.Collections.Generic;

namespace Exam.Discord
{
    public class Message
    {
        public Message(string id, string content, int timestamp, string channel)
        {
            Id = id;
            Content = content;
            Timestamp = timestamp;
            Channel = channel;
        }

        public string Id { get; set; }

        public string Content { get;set; }

        public int Timestamp { get; set; }

        public string Channel { get; set; }

        public List<string> Reactions { get; set; } = new List<string>();
    }
}
