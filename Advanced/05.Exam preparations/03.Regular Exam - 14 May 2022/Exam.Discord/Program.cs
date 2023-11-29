using System;

namespace Exam.Discord
{
    class Program
    {
        static void Main(string[] args)
        {
            var discord = new Discord();
            discord.SendMessage(new Message("1", "Test", 1, "test"));
            discord.SendMessage(new Message("2", "Testdas", 2, "test"));
            discord.SendMessage(new Message("3", "Test1", 3, "test1"));

            discord.ReactToMessage("1", "bravo");
            discord.ReactToMessage("3", "smiah");

            discord.DeleteMessage("3");
            var result = discord.GetAllMessagesOrderedByCountOfReactionsThenByTimestampThenByLengthOfContent();
            Console.WriteLine(result);
        }
    }
}
