using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using Twilio.Rest.Api.V2010.Account;

namespace TankToad.Models
{
    public class SMS
    {
        public int Id { get; set; }
        public DateTime DateReceiving { get; set; }

        //
        // Summary:
        //     The status of this message
        public string Status { get; set; }
        //
        // Summary:
        //     The date the message was sent
        public DateTime? DateSent { get; set; }
        //
        // Summary:
        //     The date this resource was last updated
        public DateTime? DateUpdated { get; set; }
        //
        // Summary:
        //     The date this resource was created
        public DateTime? DateCreated { get; set; }
        //
        // Summary:
        //     The text body of the message. Up to 1600 characters long.
        public string Body { get; set; }
        //
        // Summary:
        //     The version of the Twilio API used to process the message.
        public string ApiVersion { get; set; }
        //
        // Summary:
        //     The unique sid that identifies this account
        public string AccountSid { get; set; }
        //
        // Summary:
        //     Human readable description of the ErrorCode
        public string ErrorMessage { get; set; }
        //
        // Summary:
        //     The phone number that initiated the message
        public string From { get; set; }
        //
        // Summary:
        //     The unique id of the Messaging Service used with the message.
        public string MessagingServiceSid { get; set; }
        //
        // Summary:
        //     Number of media files associated with the message
        public string NumMedia { get; set; }
        //
        // Summary:
        //     Indicates number of messages used to delivery the body
        public string NumSegments { get; set; }
        //
        // Summary:
        //     The amount billed for the message
        public decimal? Price { get; set; }
        //
        // Summary:
        //     The currency in which Price is measured
        public string PriceUnit { get; set; }
        //
        // Summary:
        //     A string that uniquely identifies this message
        public string Sid { get; set; }
        //
        // Summary:
        //     The error code associated with the message
        public int? ErrorCode { get; set; }
        //
        // Summary:
        //     The URI for any subresources
        //public Dictionary<string, string> SubresourceUris { get; set; }
        //
        // Summary:
        //     The phone number that received the message
        public string To { get; set; }
        //
        // Summary:
        //     The URI for this resource
        public string Uri { get; set; }
        //
        // Summary:
        //     The direction of the message
        public string Direction { get; set; }

        public string ParseStatus { get; set; }
        public SMS()
        {
            DateReceiving = DateTime.UtcNow;
        }
    }
}