using MPMAR.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace MPMAR.Business.Interfaces
{
    public interface INewsTypesForNewsRepository
    {
        void AddNewsTypesForNews(List<NewsTypesForNews> NewsTypesForNews);

        void Delete(int id);
       
    }
}

