using System;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using Telegram.BotAPI;
using Telegram.BotAPI.AvailableMethods;
using Telegram.BotAPI.AvailableTypes;
using Telegram.BotAPI.GettingUpdates;
using Telegram.BotAPI.UpdatingMessages;
using telegramBot;


[DataContract]
public class UserLog
{
    [DataMember]
    public string Times { get; set; }

    [DataMember]
    public required string UserName { get; set; }

    [DataMember]
    public long UserId { get; set; }

    [DataMember]
    public string Message_Text { get; set; }

}
class Program
{

    public static long InputChatId = 0;

    static void Main()
    {
         
         int messID = 0;
        string ID = null;


        string token = "8061818030:AAHxPjLwvLtOsTxRb4gi0xATfq1j8geDXJo";

        var bot = new TelegramBotClient(token);
        Console.WriteLine("Бот запущен!");

        int offset = 0;

        while (true)
        {
            try
            {

                var updates = bot.GetUpdates(offset);

                foreach (var update in updates)
                {
                    offset = update.UpdateId + 1;

                    if (update?.Message?.From?.Id != null)
                    {
                        ID = update.Message.From.Id.ToString();
                    }

                    string File_logi = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Logi");
                    string file = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Logi", $"{ID}.json");

                    if (update.CallbackQuery != null)
                    {
                        MenuBar.Click_Button(update.CallbackQuery, bot,messID);
                        continue;
                    }


                    if (update.Message?.Text != null)
                    {
                            UserLog userLog = new UserLog
                            {
                                Times = DateTime.Now.ToString("dd.MM.yyyy HH:mm:ss"),
                                UserId = update.Message.From.Id,
                                UserName = update.Message.From.Username ?? "Неизвестно",
                                Message_Text = update.Message.Text
                            };

                             Logi.Record(userLog, File_logi, file);
                       
                            string userMessage = update.Message.Text.ToLower();

                            if (userMessage == "/start"|| userMessage == "/menu")
                            {
                                MenuBar.ShowMenu(bot, update.Message.Chat.Id);
                            
                            }
                        else if (update.Message?.Text != null && Program.InputChatId == update.Message.Chat.Id)
                        {
                            search.Sear(update.Message.Text,bot, update.CallbackQuery);
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"Ошибка: {e.Message}");
            }
        }
    }
}