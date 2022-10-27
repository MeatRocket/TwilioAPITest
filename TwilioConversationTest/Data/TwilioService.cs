using Twilio;
using Twilio.Rest.Api.V2010.Account;
using Twilio.Rest.Conversations.V1;

namespace TwilioConversationTest.Data
{
    public class TwilioService
    {
        public void TwilioMethod()
        {
            // Find your Account SID and Auth Token at twilio.com/console
            // and set the environment variables. See http://twil.io/secure
            string accountSid = Environment.GetEnvironmentVariable("AC852206e13664b0d688a35c899509ba4e");
            string authToken = Environment.GetEnvironmentVariable("2b24e659c9817d3517064eb8c668bd43");

            TwilioClient.Init(accountSid, authToken);

            var conversation = ConversationResource.Create(
                friendlyName: "Friendly Conversation"
            );

            Console.WriteLine(conversation.Sid);
        }

        public void TwilioSMS()
        {
            // Find your Account SID and Auth Token at twilio.com/console
            // and set the environment variables. See http://twil.io/secure
            string accountSid = Environment.GetEnvironmentVariable("AC852206e13664b0d688a35c899509ba4e");
            string authToken = Environment.GetEnvironmentVariable("2b24e659c9817d3517064eb8c668bd43");

            TwilioClient.Init(accountSid, authToken);

            var message = MessageResource.Create(
                body: "Hi there",
                from: new Twilio.Types.PhoneNumber("+15017122661"),
                to: new Twilio.Types.PhoneNumber("+15558675310")
            );

            Console.WriteLine(message.Sid);
        }
    }
}
