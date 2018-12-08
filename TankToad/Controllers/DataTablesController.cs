using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using TankToad.Models;
using DataTableAjax;
using LinqKit;
using System.Linq.Expressions;
using System.Linq.Dynamic;
using System.Data.Entity;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Diagnostics;
using System.Dynamic;
using System.Globalization;

using System.Data.Objects;
using DataTablesParser;

namespace TankToad.Controllers
{
    public class MainTable
    {
        public int DeviceId { get; set; }
        public string DeviceName { get; set; }
        public int? WaterLevel { get; set; }
        public int? BatteryLevel { get; set; }
        public DateTime? Timestamp { get; set; }
        public string LowLevel { get; set; }
        public string Zeros { get; set; }
        public string GraceWindowExceeded { get; set; }
        public string LowBattery { get; set; }
        public string MissingReport { get; set; }
    }

    public class DataSelect
    {
        public int Id { get; set; }
        public int? DeviceAttributesId { get; set; }
        public string DeviceAttributesName { get; set; }
        public int? SMSId { get; set; }
        public DateTime? Timestamp { get; set; }
        public int WaterLevel { get; set; }
        public int BatteryLevel { get; set; }
    }

    public class DiagnosticsSelect
    {
        public int Id { get; set; }
        public int? DeviceAttributesId { get; set; }
        public string DeviceAttributesName { get; set; }
        public int? SMSId { get; set; }
        public DateTime ReportTime { get; set; }
        public int UptimeCount { get; set; }
        public int TimeDifferenceFromDesiredLocalReportTime { get; set; }
        public string LowLevel { get; set; }
        public string Zeros { get; set; }
        public string GraceWindowExceeded { get; set; }
        public string LowBattery { get; set; }
    }

    [Authorize(Roles = "admin")]
    public class DataTablesController : Controller
    {
        public Expression<Func<T, bool>> InitDateRange<T>(string minDateStr, string maxDateStr, Expression<Func<T, DateTime?>> Exp)
        {
            DateTime? minDate, maxDate;
            if (!string.IsNullOrEmpty(minDateStr))
            {
                minDate = DateTime.Parse(minDateStr);
            }
            else
                minDate = null;

            if (!string.IsNullOrEmpty(maxDateStr))
            {
                maxDate = DateTime.Parse(maxDateStr);
                maxDate = maxDate.Value.AddDays(1).AddMilliseconds(-1);
            }
            else
                maxDate = null;

            var predicateMain = PredicateBuilder.New<T>(true);
            if (minDate != null)
            {
                var predicateMin = Expression.Lambda<Func<T, bool>>(
                               Expression.GreaterThan(
                                   Exp.Body,
                                   Expression.Constant(minDate, typeof(DateTime?))
                               ), Exp.Parameters);

                predicateMain = predicateMain.And(predicateMin);
            }
            if (maxDate != null)
            {
                var predicateMax = Expression.Lambda<Func<T, bool>>(
                       Expression.LessThan(
                           Exp.Body,
                           Expression.Constant(maxDate, typeof(DateTime?))
                       ), Exp.Parameters);

                predicateMain = predicateMain.And(predicateMax);
            }

            return predicateMain;
        }

        TankToadContext db = new TankToadContext();

        public JsonResult Datas(DataTableAjaxPostModel model, string minDateStr = null, string maxDateStr = null)
        {
            var predicateMain = InitDateRange<Data>(minDateStr, maxDateStr, d => d.Timestamp);
            var fullCount = db.Datas.Count();

            var dbSet = db.Datas
                    .Where(predicateMain)
                    .Include(d => d.DeviceAttributes)
                    .Select(d => new DataSelect
                    {
                        Id = d.Id,
                        DeviceAttributesId = d.DeviceAttributesId,
                        DeviceAttributesName = d.DeviceAttributes.Name,
                        BatteryLevel = d.BatteryLevel,
                        SMSId = d.SMSId,
                        Timestamp = d.Timestamp,
                        WaterLevel = d.WaterLevel
                    });
            var res = new DataTablesParser<DataSelect>(Request, dbSet)
                      .Parse();

            res.recordsTotal = fullCount;

            return Json(res);
        }
        public JsonResult DatasByDevice(int DeviceId, DataTableAjaxPostModel model, string minDateStr = null, string maxDateStr = null)
        {
            var predicateMain = InitDateRange<Data>(minDateStr, maxDateStr, d => d.Timestamp);
            
            var dbSet0 = db.Datas
                .Where(d => d.DeviceAttributesId == DeviceId);

            var fullCount = dbSet0.Count();

            var dbSet = dbSet0
                .Where(predicateMain)
                .Include(d => d.DeviceAttributes)
                .Select(d => new DataSelect
                {
                    Id = d.Id,
                    BatteryLevel = d.BatteryLevel,
                    SMSId = d.SMSId,
                    Timestamp = d.Timestamp,
                    WaterLevel = d.WaterLevel
                });

            var res = new DataTablesParser<DataSelect>(Request, dbSet)
                      .Parse();

            res.recordsTotal = fullCount;

            return Json(res);
        }

        public JsonResult Diagnostic(DataTableAjaxPostModel model, string minDateStr = null, string maxDateStr = null)
        {
            var predicateMain = InitDateRange<Diagnostics>(minDateStr, maxDateStr, d => d.ReportTime);
            var fullCount = db.Diagnostics.Count();

            var dbSet = db.Diagnostics
                            .Where(predicateMain)
                            .Include(d => d.DeviceAttributes)
                            .Select(d => new DiagnosticsSelect
                            {
                                Id = d.Id,
                                DeviceAttributesId = d.DeviceAttributesId,
                                DeviceAttributesName = d.DeviceAttributes.Name,//
                                TimeDifferenceFromDesiredLocalReportTime = d.TimeDifferenceFromDesiredLocalReportTime,
                                GraceWindowExceeded = d.GraceWindowExceeded,
                                LowBattery = d.LowBattery,
                                LowLevel = d.LowLevel,
                                SMSId = d.SMSId,
                                UptimeCount = d.UptimeCount,
                                Zeros = d.Zeros,
                                ReportTime = d.ReportTime
                            });

            var res = new DataTablesParser<DiagnosticsSelect>(Request, dbSet)
                      .Parse();
            res.recordsTotal = fullCount;

            return Json(res);
        }
        public JsonResult DiagnosticByDevice(int DeviceId, DataTableAjaxPostModel model, string minDateStr = null, string maxDateStr = null)
        {
            var predicateMain = InitDateRange<Diagnostics>(minDateStr, maxDateStr, d => d.ReportTime);

            var dbSet0 = db.Diagnostics
                .Where(d => d.DeviceAttributesId == DeviceId);

            var fullCount = dbSet0.Count();

            var dbSet = dbSet0
                .Where(predicateMain)
                .Include(d => d.DeviceAttributes)
                .Select(d => new DiagnosticsSelect
                {
                    Id = d.Id,
                    GraceWindowExceeded = d.GraceWindowExceeded,
                    LowBattery = d.LowBattery,
                    LowLevel = d.LowLevel,
                    SMSId = d.SMSId,
                    TimeDifferenceFromDesiredLocalReportTime = d.TimeDifferenceFromDesiredLocalReportTime,
                    UptimeCount = d.UptimeCount,
                    Zeros = d.Zeros,
                    ReportTime = d.ReportTime
                });

            var res = new DataTablesParser<DiagnosticsSelect>(Request, dbSet)
                        .Parse();

            res.recordsTotal = fullCount;

            return Json(res);
        }

        public JsonResult DeviceAttributes(DataTableAjaxPostModel model)
        {
            var dbSet = db.DeviceAttributes;

            var res = new DataTablesParser<DeviceAttributes>(Request, dbSet)
                        .Parse();

            return Json(res);
        }

        public JsonResult DeviceAttributesLogs(DataTableAjaxPostModel model, string minDateStr = null, string maxDateStr = null)
        {
            var predicateMain = InitDateRange<DeviceAttributesLog>(minDateStr, maxDateStr, d => d.UpdateDate);
            var fullCount = db.DeviceAttributesLogs.Count();

            var dbSet = db.DeviceAttributesLogs
                          .Where(predicateMain);

            var res = new DataTablesParser<DeviceAttributesLog>(Request, dbSet)
                        .Parse();

            res.recordsTotal = fullCount;

            return Json(res);
        }
        public JsonResult DeviceAttributesLogsByDevice(int DeviceId, DataTableAjaxPostModel model, string minDateStr = null, string maxDateStr = null)
        {
            var predicateMain = InitDateRange<DeviceAttributesLog>(minDateStr, maxDateStr, d => d.UpdateDate);

            var dbSet0 = db.DeviceAttributesLogs
                            .Where(d => d.DeviceAttributesId == DeviceId);

            var fullCount = dbSet0.Count();

            var dbSet = dbSet0.Where(predicateMain);

            var res = new DataTablesParser<DeviceAttributesLog>(Request, dbSet)
                        .Parse();

            res.recordsTotal = fullCount;

            return Json(res);
        }

        public JsonResult SMS(DataTableAjaxPostModel model, string minDateStr = null, string maxDateStr = null)
        {
            var predicateMain = InitDateRange<SMS>(minDateStr, maxDateStr, d => d.DateReceiving);
            var fullCount = db.SMS.Count();

            var dbSet = db.SMS
                          .Where(predicateMain);

            var res = new DataTablesParser<SMS>(Request, dbSet)
                        .Parse();

            res.recordsTotal = fullCount;

            return Json(res);
        }
        public JsonResult SMSByDevice(int DeviceId, DataTableAjaxPostModel model, string minDateStr = null, string maxDateStr = null)
        {
            var predicateMain = InitDateRange<SMS>(minDateStr, maxDateStr, d => d.DateReceiving);
            var device = db.DeviceAttributes
                            .Where(d => d.Id == DeviceId)
                            .FirstOrDefault();

            var dbSet0 = db.SMS
                .Where(d => d.From == device.CellNumber);

            var fullCount = dbSet0.Count();
            var dbSet = dbSet0.Where(predicateMain);

            var res = new DataTablesParser<SMS>(Request, dbSet)
                        .Parse();

            res.recordsTotal = fullCount;

            return Json(res);
        }

        public JsonResult Main(DataTableAjaxPostModel model, bool selectProblemDevice =false)
        {
            var maxT= DateTime.UtcNow.AddHours(-48);

            var devices = db.DeviceAttributes
                            .Where(d=>d.Status== "active")
                            .Select(d => new
                            {
                                d.Id,
                                d.CurrentLocationTimeZone,
                                d.CellNumber,
                                d.Name
                            })
                            .ToList();

            var dataBD = db.Datas
                            .Where(d => d.Timestamp >= maxT)
                            .GroupBy(d => d.DeviceAttributesId)
                            .Select(g=> new
                            {
                                key= g.Key,
                                data =  g.OrderByDescending(d => d.Timestamp)
                                        .Select(d => new
                                        {
                                            d.DeviceAttributesId,
                                            d.Timestamp,
                                            d.WaterLevel,
                                            d.BatteryLevel
                                        })
                                        .FirstOrDefault()
                            })
                            .ToList();

            var diagnosticBD = db.Diagnostics
                            .Where(d => d.ReportTime >= maxT)
                            .Select(d => new
                            {
                                d.DeviceAttributesId,
                                d.ReportTime,
                                d.GraceWindowExceeded,
                                d.LowBattery,
                                d.LowLevel,
                                d.Zeros
                            })
                            .ToList();

            var smsMaxT = DateTime.UtcNow.AddHours(-36);
            var smsBD = db.SMS
                    .Where(d => d.DateReceiving >= smsMaxT)
                    .GroupBy(s=>s.From)
                    .Select(g => new {
                        g.Key
                    })
                    .ToList();

            List<MainTable> mainTableList= new List<MainTable>();
            var DTnow = DateTime.UtcNow.AddHours(-24);

            foreach (var device in devices) {
                int.TryParse(device.CurrentLocationTimeZone, out int timeZone);
                var deviceMaxT = DTnow.AddHours(timeZone);

                var dataObj = dataBD
                    .Where(d => d.data.Timestamp >= deviceMaxT && d.key == device.Id)
                    .FirstOrDefault();
                var data = dataObj?.data;

                var diagnostic = diagnosticBD
                    .Where(d => d.ReportTime >= deviceMaxT && d.DeviceAttributesId == device.Id)
                    .OrderByDescending(d => d.ReportTime)
                    .ToList();

                string alertGraceWindowExceeded = "",
                    alertLowBattery = "",
                    alertLowLevel = "",
                    alertZeros = "";

                foreach (var diagns in diagnostic)
                {
                    if (!string.IsNullOrWhiteSpace(diagns.GraceWindowExceeded))
                        alertGraceWindowExceeded = diagns.GraceWindowExceeded;

                    if (!string.IsNullOrWhiteSpace(diagns.LowBattery))
                        alertLowBattery = diagns.LowBattery;

                    if (!string.IsNullOrWhiteSpace(diagns.LowLevel))
                        alertLowLevel = diagns.LowLevel;

                    if (!string.IsNullOrWhiteSpace(diagns.Zeros))
                        alertZeros = diagns.Zeros;
                }

                var smsExist = smsBD.Exists(d => d.Key == device.CellNumber);

                var mainTable = new MainTable()
                {
                    DeviceId = device.Id,
                    DeviceName = device.Name,
                    MissingReport = smsExist ? "" : "!",
                    GraceWindowExceeded = alertGraceWindowExceeded,
                    LowBattery = alertLowBattery,
                    LowLevel = alertLowLevel,
                    Zeros = alertZeros
                };

                if (data != null)
                {
                    mainTable.BatteryLevel = data.BatteryLevel;
                    mainTable.Timestamp = data.Timestamp;
                    mainTable.WaterLevel = data.WaterLevel;
                }

                if (selectProblemDevice)
                {
                    if (!(String.IsNullOrWhiteSpace(alertGraceWindowExceeded) &&
                        String.IsNullOrWhiteSpace(alertLowBattery) &&
                        String.IsNullOrWhiteSpace(alertLowLevel) &&
                        String.IsNullOrWhiteSpace(alertZeros) &&
                        String.IsNullOrWhiteSpace(mainTable.MissingReport)))

                            mainTableList.Add(mainTable);
                }
                else
                    mainTableList.Add(mainTable);    
            }

            var res = new DataTablesParser<MainTable>(Request, mainTableList.AsQueryable())
                     .Parse();

            return Json(res);
        }

        public JsonResult SMSstat(string minDateStr = null, string maxDateStr = null)
        {
            var predicateMain = InitDateRange<SMS>(minDateStr, maxDateStr, d => d.DateReceiving);

            var dbSet = db.SMS
                    .Where(predicateMain)
                    .GroupBy(x => new { x.DateReceiving.Year, x.DateReceiving.Month, x.DateReceiving.Day })
                    .Select(g => new
                    {
                        count = g.Count(),
                        date = g.Key
                    })
                    .ToList();

            var minDT = dbSet.Min(d => new DateTime(d.date.Year, d.date.Month, d.date.Day));
            var maxDT = dbSet.Max(d => new DateTime(d.date.Year, d.date.Month, d.date.Day));

            var newList = new List<dynamic>();
            for(var i= minDT; i <= maxDT; i = i.AddDays(1))
            {
                var elem = dbSet.Where(d => 
                                        d.date.Day == i.Day
                                        &&  d.date.Month == i.Month
                                        &&d.date.Year == i.Year
                                    ).FirstOrDefault();
                if (elem != null)
                    newList.Add(elem);
                else
                    newList.Add(new {
                        count = 0,
                        date = new { i.Year, i.Month, i.Day }
                    });
            }

            return Json(newList, JsonRequestBehavior.AllowGet);
        }
    }
}