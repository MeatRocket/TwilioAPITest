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
        string s = "";
        string s1 = "";
        string s2 = "";

        string elieAc = "";
        string elieAuth = "";
        string elieNum = "";

        string elieTestAc = "";
        string elieTestAuth = "";
        string elieTestNum = "";

        Twilio.Base.ResourceSet<LocalResource>? result { get; set; } = null;
        AvailablePhoneNumberCountryResource availablePhoneNumberCountry { get; set; } = null;
        public string? Number { get; set; } = "+16267204252";
        [Parameter]
        public string Result { get; set; }

        public string Contents { get; set; }
        public void TwilioSMS()
        {
            Result = "";
            TwilioClient.Init(elieAc, elieAuth);

            var message = MessageResource.Create(
                body: "Kif Ken El Jumbo ?",
                from: new Twilio.Types.PhoneNumber(elieNum),
                to: new Twilio.Types.PhoneNumber("+96171237052")
            );


            Result = message.Body + " status: " + message.Status;
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