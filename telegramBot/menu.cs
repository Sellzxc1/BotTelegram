using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Telegram.BotAPI;
using Telegram.BotAPI.AvailableMethods;
using Telegram.BotAPI.AvailableTypes;
using Telegram.BotAPI.GettingUpdates;
using Telegram.BotAPI.UpdatingMessages;

namespace telegramBot
{
    public class MenuBar
    {
        public static void ShowMenu(TelegramBotClient bot, long chatId)
        {
            var _Menu = new InlineKeyboardMarkup(
                new[]
                {
                    new[]
                    {
                        new InlineKeyboardButton("поиск"){CallbackData="Search"}
                    },
                    new[]
                    {
                         new InlineKeyboardButton("мой аккаунт"){CallbackData="Button1"},
                          new InlineKeyboardButton("Поддержка"){CallbackData="Button1"}
                    },
                    new[]
                    {
                         new InlineKeyboardButton("партнерам"){CallbackData="Button1"},
                          new InlineKeyboardButton("Создать бота"){CallbackData="Button1"}
                    }
                }
            );
            bot.SendMessage(
               chatId,
               "меню",
               replyMarkup: _Menu
            );
        }


        public static void Click_Button(CallbackQuery query, TelegramBotClient bot, int messageId)
        {
            long chat = query.Message.Chat.Id;
            string data = query.Data;
            bot.AnswerCallbackQuery(
                 callbackQueryId: query.Id,
                    text: "" 
            );

            if (messageId!=0)
            {
                bot.DeleteMessage(query.Message.Chat.Id, messageId);
            }
            switch (data)
            {
                case "Search":
                    Program.InputChatId = chat;
                    bot.SendMessage(query.Message.Chat.Id, "Введите ID:");

                    break;
            }
        }
    }
}
