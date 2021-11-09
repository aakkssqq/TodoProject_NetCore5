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
        private int msgIndex = -1;
        private int taskIndex =-1;
        private int count = 0;
        
        
        public PushController(IHttpContextAccessor httpContextAccessor, IDataServiceBase dataServicesBase)
        {
            _dataServicesBase = dataServicesBase;
            _httpContextAccessor = httpContextAccessor;
        }

        // GET api/<Push>/Account
        [HttpGet("{account}")]
        public async Task<IEnumerable<dynamic>> GetData(string account)
        {
            var items = _dataServicesBase.GetTableData("YZAppISOLog",  20);
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

                    var msg = (IDictionary<string, object>)_dataServicesBase.GetTableDataCount("YZAppISOLog").Result.FirstOrDefault();
                    var task = (IDictionary<string, object>)_dataServicesBase.GetTaskCount().Result.FirstOrDefault();

                    if (msg != null)
                        if (msgIndex != Convert.ToInt32(msg["ID"]))
                        {
                            count++;
                            _event = Events.EventType.NewMessage.ToString();
                            if (msgIndex != -1)
                                data = ServerSentEventData($@" {count}. 訊息更新", msg["ID"].ToString(), _event);
                            msgIndex = Convert.ToInt32(msg["ID"]);
                        }

                    if (task != null)
                        if (taskIndex != Convert.ToInt32(task["TaskID"]))
                        {
                            count++;
                            _event = Events.EventType.NewTasks.ToString();
                            if (taskIndex != -1)
                                data = ServerSentEventData($@" {count}. {task["SerialNum"]}任務更新", task["TaskID"].ToString(), _event);
                            taskIndex = Convert.ToInt32(task["TaskID"]);
                        }

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
