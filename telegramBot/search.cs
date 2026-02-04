using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Telegram.BotAPI;
using Telegram.BotAPI.AvailableMethods;
using Telegram.BotAPI.AvailableTypes;

namespace telegramBot
{
    [DataContract]
    public class TelegramPerson
    {
        public string nick { get; set; }
        public string adapterType { get; set; }
        public string adapterUserId { get; set; }
        public string name { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string email { get; set; }
        public string phone { get; set; }
        public string unsubscribed { get; set; }
        public string notes { get; set; }
        public string @ref { get; set; }
        public int id { get; set; }
        public int customerId { get; set; }
        public string spider_type { get; set; }
        public object spider_operator_id { get; set; }
        public long spider_last_message_at { get; set; }
        public long spider_created_at { get; set; }
        public string nick_userid_md5 { get; set; }

    }
    public class search
    {
        public static void Sear(string userID,TelegramBotClient bot, CallbackQuery query)
        {

            string json = System.IO.File.ReadAllText("Telegram.json");
            List<TelegramPerson> persons = JsonSerializer.Deserialize<List<TelegramPerson>>(json);

            foreach (var person in persons)
            {
                if (person.adapterUserId == userID)
                {
                    bot.SendMessage(query.Message.Chat.Id,
                        $"👤 Username: {person.nick ?? "(нет)"}\n" +
                        $"🆔 ID: {person.adapterUserId ?? "нет"}\n" +
                        $"📝 Имя: {person.name ?? "(нет)"}");
                    return;
                }
            }

            bot.SendMessage(query.Message.Chat.Id, "Пользователь не найден");
        }
    }
}
