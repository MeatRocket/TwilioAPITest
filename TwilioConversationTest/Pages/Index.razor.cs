using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using System.Net.Http;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Components.Routing;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.Web.Virtualization;
using Microsoft.JSInterop;
using TwilioConversationTest;
using TwilioConversationTest.Shared;
using TwilioConversationTest.Data;
using Twilio;
using Twilio.Rest.Api.V2010.Account;
using Twilio.Rest.Lookups.V1;
using Twilio.Rest.Api.V2010.Account.AvailablePhoneNumberCountry;
using Twilio.Rest.Api.V2010.Account.Message;
using Twilio.Types;

namespace TwilioConversationTest.Pages
{
    public partial class Index
    {
        string s = "AC852206e13664b0d688a35c899509ba4e";
        string s1 = "2b24e659c9817d3517064eb8c668bd43";
        string s2 = "+18787686783";



        string elieAc = "AC2915fe0e5baaf1e40c5beaefce66b1ec";
        string elieAuth = "828130918559f61b11adaf51a10ffa0f";
        string elieNum = "+18325725034";



        string elieTestAc = "ACb030db9222e95964ffb27ca39ea7f447";
        string elieTestAuth = "4e0c71bf50354a60b0ca7b7074e4d740";
        string elieTestNum = "+18325725034";

        Twilio.Base.ResourceSet<LocalResource>? result { get; set; } = null;
        AvailablePhoneNumberCountryResource availablePhoneNumberCountry { get; set; } = null;
        public string? Number { get; set; } = "+16267204252";

        // +15109440756
        // +18325725034
        public string? From { get; set; }
        public string? To { get; set; }
        public string? Message { get; set; }
        [Parameter]
        public string Result { get; set; }

        public string Contents { get; set; }
        public async Task TwilioSMSAsync()
        {
            Result = "Loading...";
            if (string.IsNullOrEmpty(From) || string.IsNullOrEmpty(To) || string.IsNullOrEmpty(Message))
            {
                Result = "Empty values!";
                return;
            }
            TwilioClient.Init(elieAc, elieAuth);

            var message = await MessageResource.CreateAsync(
                body: Message,
                from: new Twilio.Types.PhoneNumber(From),
                to: new Twilio.Types.PhoneNumber(To)
            );

            Result = "Message Send Successful";
            StateHasChanged();
            Console.WriteLine(message.Sid);
        }

        public void CheckTwilioNumber()
        {

            TwilioClient.Init(s, s1);

            var type = new List<string> {
            "carrier"
        };

            var phoneNumber = PhoneNumberResource.Fetch(
                type: type,
                pathPhoneNumber: new Twilio.Types.PhoneNumber(Number)
            );
            Result = phoneNumber.Carrier.ToString();
            Console.WriteLine(phoneNumber.Carrier);
        }

        public void ReadTwilioSMS()
        {
            Result = "";
            TwilioClient.Init(elieAc, elieAuth);

            var messages = MessageResource.Read(limit: 20);

            foreach (var record in messages)
                Result += record.Body + "<br />[" + record.Status + "] from:" + record.From + " | to: " + record.To + "<br />-------<br />";
        }

        public void GetAllAvailableTwilioNumbersInCountry()
        {
            Result = "";
            TwilioClient.Init(elieAc, elieAuth);

            var availablePhoneNumberCountry = AvailablePhoneNumberCountryResource.Fetch(
                pathCountryCode: "US"
            );
            Result = availablePhoneNumberCountry.CountryCode;
            Console.WriteLine(availablePhoneNumberCountry.CountryCode);
        }

        public void GetAllTwilioCountryPhoneNumbers()
        {
            Result = "";
            TwilioClient.Init(elieAc, elieAuth);

            result = LocalResource.Read(areaCode: 510, pathCountryCode: "US", limit: 20);
            string sdsd = "";
            //foreach (LocalResource? record in local)
            //{
            //    Result += record.FriendlyName;
            //    Console.WriteLine(record.FriendlyName);
            //}
        }

        public void GetAllTwilioTollFreePhoneNumbers()
        {
            Result = "";
            TwilioClient.Init(s, s1);

            var tollFree = TollFreeResource.Read(pathCountryCode: "US", limit: 20);

            foreach (var record in tollFree)
            {
                Result += record.FriendlyName;
                Console.WriteLine(record.FriendlyName);
            }
        }

        public void ProvisonPhoneNumber(string number)
        {
            TwilioClient.Init(elieTestAc, elieTestAuth);

            try
            {
                IncomingPhoneNumberResource incomingPhoneNumber = IncomingPhoneNumberResource.Create(
                phoneNumber: new Twilio.Types.PhoneNumber(number));
                Console.WriteLine("Success!");
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            string sr = "";

        }
    }


}