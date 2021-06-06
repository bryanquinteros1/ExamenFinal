using System;
using System.Linq;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Args;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;

namespace TelegramBotExamples
{
    class Program
    {
        
        private static readonly TelegramBotClient Bot = new TelegramBotClient("1756573715:AAHi4o_TYIQ-MJfKhOXeBRzyLZgISpn4CKs"); // La API de telegram 

        static void Main(string[] args)
        {
            //el llamado para los botones 
            Bot.OnMessage += BotOnMessageReceived;
            Bot.OnCallbackQuery += BotOnCallbackQueryReceived;
            Bot.OnReceiveError += BotOnReceiveError;

            


            Bot.StartReceiving();
            Console.WriteLine("YA SE PUEDE USAR EL BOT"); // mensaje que muestra en la consola indicando que ya se puede usar
            Console.ReadLine();
            Bot.StopReceiving();
        }




        private static async void BotOnMessageReceived(object sender, MessageEventArgs messageEventArgs)
        {
            var message = messageEventArgs.Message;

            if (message == null || message.Type != MessageType.Text) return;

            switch (message.Text.Split(' ').First()) // se usa un switch para facilitar las opciones con los botones
            {
                
                case "/Atajo1":

                    
                    await Bot.SendChatActionAsync(message.Chat.Id, ChatAction.Typing);

                    await Task.Delay(50);

                    var keyboardEjemplo1 = new InlineKeyboardMarkup(new[]
                    {
                    new []
                    {
                        InlineKeyboardButton.WithCallbackData( //boton para la imagen
                        text:"Imagen",
                        callbackData: "imagen"),
                       
                    },
                    new []
                    {
                       
                        InlineKeyboardButton.WithCallbackData( //boton para el numero del dueño del bot
                        text:"Numero",
                        callbackData: "Numero"),
                    },
                    new []
                    {
                        InlineKeyboardButton.WithCallbackData( //boton del gif 
                        text:"Gif",
                         callbackData: "Gif"),
                       
                    }
                });

                    await Bot.SendTextMessageAsync(
                    message.Chat.Id,
                    "Eligio el  Atajo1.\nSeleccione una Opcion", // despues de haber seleccionado muestra un mensaje 
                    replyMarkup: keyboardEjemplo1);
                    break;

                case "/Atajo2": // botones del atajo 2 

                    var keyboardEjemplo2 = new InlineKeyboardMarkup(new[]
                    {
                    new []
                    {
                       
                     InlineKeyboardButton.WithCallbackData( //boton donde muestra la respuesta que habiamos seleccionado
                     text:"Respuesta",
                     callbackData: "Respuesta"),


                    },
                    new []
                    {
                        InlineKeyboardButton.WithCallbackData( //boton para mostrar un video 
                     text:"Video",
                     callbackData: "video"),
                     
                    },
                    new []
                    {
                        
                    
                      InlineKeyboardButton.WithCallbackData( //boton para iniciar de nuevo 
                     text:"Iniciar de nuevo",
                     callbackData: "Iniciar"),
                    }
                });


                    await Bot.SendTextMessageAsync(
                        message.Chat.Id,
                        "Eligio el Atajo2.\nSeleccione una Opción",// de igual forma muestra un mensaje despues de haber seleccionado una opcion 
                        replyMarkup: keyboardEjemplo2);
                    break;

                

                default:
                    const string usage = @"
                Hola Bienvenido. Elija Algun Atajo. 
                /Atajo1 ♦
                /Atajo2 ♦ "; //es la inicializacion de los botones donde tenemos dos atajos para las demas opciones 




                    await Bot.SendTextMessageAsync(
                        message.Chat.Id,
                        text: usage,
                        replyMarkup: new ReplyKeyboardRemove());

                    break;
            }
        }

        private static async void BotOnCallbackQueryReceived(object sender, CallbackQueryEventArgs callbackQueryEventArgs)
        {
            var callbackQuery = callbackQueryEventArgs.CallbackQuery; //el llamado para las opciones seleccionadas se ejecuten

            switch (callbackQuery.Data)
            {
                



              

                case "imagen":
                    await Bot.SendPhotoAsync(
                        chatId: callbackQuery.Message.Chat.Id,
                        photo: "https://desarrollarinclusion.cilsa.org/wp-content/uploads/2017/06/binary-system-557601_960_720.jpg"
                
                        );
                    break;






                case "Gif":
                    await Bot.SendAnimationAsync(
                        chatId: callbackQuery.Message.Chat.Id,
                        animation: "https://media.giphy.com/media/fsoCk5kgOcYMM/giphy.gif"
                        );
                    break;





                case "video":
                    await Bot.SendVideoAsync(
                        chatId: callbackQuery.Message.Chat.Id,
                        video: "https://res.cloudinary.com/universidad-mariano-galvez/video/upload/v1591328002/Qu%C3%A9_son_los_algoritmos_-_V%C3%ADdeo_Animaci%C3%B3n_N_2_ur7y2u.mp4"
                        );
                    break;

              


               
                    // codigo para mostrar la respuesta que habiamos seleccionado 
                case "Respuesta":
                 await Bot.SendTextMessageAsync(
                 chatId: callbackQuery.Message.Chat.Id,
                 text: "ID: " + callbackQuery.Message.MessageId + " - " + callbackQuery.Message.Text,
                 replyToMessageId: callbackQuery.Message.MessageId);
                    break;




                case "Numero":
                  await Bot.SendContactAsync(
                  chatId: callbackQuery.Message.Chat.Id,
                  phoneNumber: "502-33185015",
                  firstName: "Dueño",
                  lastName: "Del Bot"
                        );
                    break;



                case "Iniciar":
                    await Bot.SendTextMessageAsync(
                    chatId: callbackQuery.Message.Chat.Id,
                    text: "Escribir Reiniciar",
                    replyMarkup: new ForceReplyMarkup());
                    break;

               
            }
        }

        private static void BotOnReceiveError(object sender, ReceiveErrorEventArgs receiveErrorEventArgs)
        {
            Console.WriteLine("Received error: {0} — {1}", // muestra de algun error que se haya dado 
                receiveErrorEventArgs.ApiRequestException.ErrorCode,
                receiveErrorEventArgs.ApiRequestException.Message);
        }
    }
}