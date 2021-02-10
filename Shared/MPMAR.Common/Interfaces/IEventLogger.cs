using System;
using System.Collections.Generic;
using System.Text;

namespace MPMAR.Common.Interfaces
{
    public interface IEventLogger<T>
    {
        /// <summary>
        /// log information of event
        /// </summary>
        /// <param name="userName">user name for the one who did the event</param>
        /// <param name="activity">activity enum describe what the event do</param>
        /// <param name="pageName">page name that the event occurred in</param>
        /// <param name="objectName">object name that the event occurred on</param>
        public void LogInfoEvent(string userName, ActivityEnum activity, string pageName, string objectName);
    }
}
