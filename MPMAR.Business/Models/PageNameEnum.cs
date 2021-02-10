using System;
using System.Collections.Generic;
using System.Text;

namespace MPMAR.Business.Models
{
    public enum PageNameEnum
    {
        FormerMinistries=1,
        MinistryVision = 2,
        MinistrySpeech = 3,
        News = 4,
        EgyptVision = 5,
        Analytics = 6,
        DynamicPage = 7,
        PhotoArchive=8
    }

    public  class PageNameEnumPerController
    {
       public PageNameEnumPerController()
        {
            PageNameEnumPerControllerDictionary = new Dictionary<string, PageNameEnum>();

            PageNameEnumPerControllerDictionary.Add("FormerMinistries",PageNameEnum.FormerMinistries);
            PageNameEnumPerControllerDictionary.Add("MinistryVision", PageNameEnum.MinistryVision);
            PageNameEnumPerControllerDictionary.Add("MinistrySpeech", PageNameEnum.MinistrySpeech);
            PageNameEnumPerControllerDictionary.Add("News", PageNameEnum.News);
            PageNameEnumPerControllerDictionary.Add("EgyptVision", PageNameEnum.EgyptVision);
            PageNameEnumPerControllerDictionary.Add("Analytics", PageNameEnum.Analytics);
            PageNameEnumPerControllerDictionary.Add("DynamicPage", PageNameEnum.DynamicPage);
            PageNameEnumPerControllerDictionary.Add("PhotoArchive", PageNameEnum.PhotoArchive);
        }
        public Dictionary<string, PageNameEnum> PageNameEnumPerControllerDictionary;
    }
}
