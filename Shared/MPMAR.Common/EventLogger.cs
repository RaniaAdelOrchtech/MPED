using Microsoft.Extensions.Logging;
using MPMAR.Common.Interfaces;
using NLog;
using System;
using System.Collections.Generic;
using System.Text;

namespace MPMAR.Common
{
    public class EventLogger<T>: IEventLogger<T>
    {
        private readonly ILogger<T> _logger;
        public EventLogger(ILogger<T> logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// log information of event
        /// </summary>
        /// <param name="userName">user name for the one who did the event</param>
        /// <param name="activity">activity enum describe what the event do</param>
        /// <param name="pageName">page name that the event occurred in</param>
        /// <param name="objectName">object name that the event occurred on</param>
        public void LogInfoEvent(string userName,ActivityEnum activity,string pageName ,string objectName)
        {
            string msg = "";
            switch(activity)
            {
                case ActivityEnum.Add:
                    msg = "has added new";
                    break;
                case ActivityEnum.Update:
                    msg = "has updated";
                    break;
                case ActivityEnum.Delete:
                    msg = "has deleted";
                    break;
                case ActivityEnum.Approve:
                    msg = "has approved";
                    break;
                case ActivityEnum.Reject:
                    msg = "has rejected";
                    break;
                case ActivityEnum.Submitted:
                    msg = "has submitted";
                    break;
                default:break;
            }
            string objectMsg = "";
            if(!string.IsNullOrEmpty(objectName))
            {
                objectMsg = $"with name '{objectName}'";
            }
            _logger.LogInformation($"User '{userName}'  {msg} {pageName} {objectMsg}");
        }
        public void LogWarningEvent(string userName, string message)
        {
            //_logger
        }
    }
}
