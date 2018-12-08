using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Twilio.TwiML;
using Twilio.AspNet.Mvc;
using Twilio.Security;
using System.Net;

using TankToad.Models;
using System.Text.RegularExpressions;
using TankToad.Class;

namespace TankToad.Controllers
{
   
    public class SMSController : TwilioController
    {
        private TankToadContext db = new TankToadContext();
        // GET: SMS
        public ActionResult Index()
        {
            return View();
        }
        public TwiMLResult PostSMS()
        {
            var messagingResponse = new MessagingResponse();
            var form = Request.Form;

            //set AccountSid
            if (form["AccountSid"] != "**********************************")
            {
                messagingResponse.Message("ERROR. Wrong Account");
                return TwiML(messagingResponse);
            }

            SMS sms = new SMS()
            {
                AccountSid = form["AccountSid"],
                ApiVersion = form["ApiVersion"],
                Body = form["Body"],
                DateCreated = form["DateCreated"] != null ? DateTime.Parse(form["DateCreated"]) : (DateTime?)null,
                DateSent = form["DateSent"] != null ? DateTime.Parse(form["DateSent"]) : (DateTime?)null,
                DateUpdated = form["DateUpdated"] != null ? DateTime.Parse(form["DateUpdated"]) : (DateTime?)null,
                Direction = form["Direction"],
                ErrorCode = form["ErrorCode"] != null ? int.Parse(form["ErrorCode"]) : (int?)null,
                ErrorMessage = form["ErrorMessage"],
                From = form["From"],
                MessagingServiceSid = form["MessagingServiceSid"],
                NumMedia = form["NumMedia"],
                NumSegments = form["NumSegments"],
                Price = form["Price"] != null ? decimal.Parse(form["Price"]) : (decimal?)null,
                PriceUnit = form["PriceUnit"],
                Sid = form["Sid"],
                Status = form["Status"],
                //SubresourceUris = null,
                To = form["To"],
                Uri = form["Uri"]
            };
            db.SMS.Add(sms);
            db.SaveChanges();

            string bodyStr = form["Body"];
            Regex DATregex = new Regex(@"^DAT:");
            bool ifDAT = DATregex.Matches(bodyStr).Count > 0;
            Regex RPTregex = new Regex(@"^RPT:");
            bool ifRPT = RPTregex.Matches(bodyStr).Count > 0;
            if (!(ifRPT || ifDAT))
            {
                messagingResponse.Message("ERROR. Wrong type sms");
                return TwiML(messagingResponse);
            }

            try
            {
                SMSparse(sms.Id);
                //messagingResponse.Message("ALL OK. Parsed");
            }
            catch (Exception ex)
            {
                messagingResponse.Message($"ERROR Parsing. [{ex.Message}]");
                return TwiML(messagingResponse);
            }

            return TwiML(messagingResponse);
        }

        [HttpGet]
        public void SMSparse(int Id)
        {
            TankToadContext tdParse = new TankToadContext();

            var sms = tdParse.SMS.Find(Id);
            var datas = tdParse.Datas.Where(d => d.SMSId == sms.Id);
            SMSparse sparse = new SMSparse(sms);
            if (sparse.ErrorList.Count != 0)
                sms.Status = sparse.ErrorList[0];
            else
            {    
                if (datas != null && sparse.Data.Count!=0)
                {
                    sms.Status = "OK";
                    tdParse.Datas.RemoveRange(datas);
                    tdParse.Datas.AddRange(sparse.Data);
                    var diagnostic = tdParse.Diagnostics.Where(d => d.SMSId == sms.Id).FirstOrDefault();
                    if (diagnostic != null)
                        tdParse.Diagnostics.Remove(diagnostic);
                    tdParse.Diagnostics.Add(sparse.Diagnostics);
                }
                
                if (sparse.EditedDevice != null)
                {
                    sms.Status = "OK";
                    DeviceController deviceController = new DeviceController();
                    deviceController.PutDeviceAttributes(sparse.EditedDevice.Id, sparse.EditedDevice);
                }

            }
            tdParse.SaveChanges();
        }
    }
}