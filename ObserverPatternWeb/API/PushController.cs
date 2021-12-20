using System;
using System.Collections.Generic;
using System.Net.Http;
using Microsoft.AspNetCore.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using System.Linq;
using Dapper;
using Microsoft.Extensions.Configuration;
using ObserverPattern.Base;
using ObserverPattern.Base.Enum;
using ObserverPattern.Services.TableData;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ObserverPatternWeb.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class PushController : SseControllerBase
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private static IDataServiceBase _dataServicesBase;
        private static IDataServiceBase _efDataServicesBase;
        private int msgIndex = -1;
        private int taskIndex =-1;
        private string efQueueIndex = string.Empty;
        private int count = 0;
        
        
        public PushController(IHttpContextAccessor httpContextAccessor, IDataServiceBase dataServicesBase)
        {
            _efDataServicesBase = new DataServiceBase("Data Source=192.168.0.15;Initial Catalog=DSCSYS;User ID=sa;Password=cmsSA!769; MultipleActiveResultSets=true");
            _dataServicesBase = dataServicesBase;
            _httpContextAccessor = httpContextAccessor;
        }

        // GET api/<Push>/Account
        [HttpGet("{account}")]
        public async Task<IEnumerable<dynamic>> GetData(string account)
        {
            //var items = _dataServicesBase.GetTableData("SummaryLog",  20);
            string cmd = @"use DSCSYS
                            select Top 20  EF001,EF002,EF003,EF004,EF006,EF007,EF008,EF009,EF010,EF011,
                            D.MB002 as EF001C,A.MB002 as EF002C,MF002 as EF004C
                            from EFJOBQUE EFJOBQUE
                            Left Join   DSCMB D on EF001=D.MB001
                            Left Join   ADMMB A on EF002=A.MB001
                            Left Join TW_TEST..ADMMF AS ADMMF on EF004=MF001
                            where  ( EFJOBQUE.EF002= N'PURI05' )  
                            order by EFJOBQUE.EF008 desc ";
            var items = _efDataServicesBase.GetTableDataByScript(cmd);
            return await items;
        }

        [HttpGet, HttpPost]
        public async Task ServerSentEvents()
        {
            string _event = string.Empty;
            var response = _httpContextAccessor.HttpContext.Response;
            response.Headers.Add("Content-Type", "text/event-stream");
            response.StatusCode = 200;
            try
            {
                for (var i = 0; true; ++i)
                {
                    _event = Events.EventType.None.ToString();
                    var data = ServerSentEventData($@" {count}. 無動作", "", _event);

                    string cmd = @"use DSCSYS
                                    select Top 1  EF001,EF002,EF003,EF004,EF006,EF007,EF008,EF009,EF010,EF011,
                                    D.MB002 as EF001C,A.MB002 as EF002C,MF002 as EF004C
                                    from EFJOBQUE EFJOBQUE
                                    Left Join   DSCMB D on EF001=D.MB001
                                    Left Join   ADMMB A on EF002=A.MB001
                                    Left Join TW_TEST..ADMMF AS ADMMF on EF004=MF001
                                    where  ( EFJOBQUE.EF002= N'PURI05' )  
                                    order by EFJOBQUE.EF008 desc";
                    var lastData = (IDictionary<string, object>)_efDataServicesBase.GetTableDataByScript(cmd).Result.FirstOrDefault();
                    if (lastData != null)
                        if (efQueueIndex != Convert.ToDateTime(lastData["EF008"]).ToString("yyyyMMddHHmmss"))
                        {
                            count++;
                            _event = Events.EventType.NewMessage.ToString();
                            //if (efQueueIndex != string.Empty)
                                data = ServerSentEventData($@" {count}. 訊息更新", Convert.ToDateTime(lastData["EF008"]).ToString("yyyyMMddHHmmss"), _event);
                            efQueueIndex = Convert.ToDateTime(lastData["EF008"]).ToString("yyyyMMddHHmmss");
                        }


                    #region LogData
                    //var msg = (IDictionary<string, object>)_dataServicesBase.GetTableDataCount("SummaryLog").Result.FirstOrDefault();

                    //if (msg != null)
                    //    if (msgIndex != Convert.ToInt32(msg["ID"]))
                    //    {
                    //        count++;
                    //        _event = Events.EventType.NewMessage.ToString();
                    //        if (msgIndex != -1)
                    //            data = ServerSentEventData($@" {count}. 訊息更新", msg["ID"].ToString(), _event);
                    //        msgIndex = Convert.ToInt32(msg["ID"]);
                    //    }
                    #endregion


                    await response.WriteAsync(data);

                    response.Body.Flush();
                    await Task.Delay(5 * 1000);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
        private string ServerSentEventData(string data, string id, string _event = "message", long retry = 10000)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("retry:{0}\n", retry);
            sb.AppendFormat("event:{0}\n", _event);
            sb.AppendFormat("id:{0}\n", id);
            sb.AppendFormat("data:{0}\n\n", $@"[{_event}] {data}_{DateTime.Now:yyyy/MM/dd HH:mm:ss}");
            return sb.ToString();
        }
    }
}
