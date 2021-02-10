using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using MPMAR.Business.Interfaces;
using MPMAR.Data;
using MPMAR.Data.Mappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MPMAR.Business.Services
{
    public class NewsTypesForNewsRepository : INewsTypesForNewsRepository
    {
        private readonly ApplicationDbContext _db;
      
       
        public NewsTypesForNewsRepository(ApplicationDbContext db)
        {
            _db = db;
        }
       
      
        public void AddNewsTypesForNews(List<NewsTypesForNews> NewsTypesForNewsList)
        {
            try
            {
                foreach(var NewsTypesForNews in NewsTypesForNewsList)
                { 
                _db.NewsTypesForNews.Add(NewsTypesForNews);
                _db.SaveChanges();
                }


            }
            catch (Exception ex)
            {
              
            }
        }
        public void Delete(int id)
        {
            try
            {
                var items = _db.NewsTypesForNews.Where(x => x.PageNewsId == id).ToList();

                foreach(var item in items)
                { 
                _db.NewsTypesForNews.Attach(item);
                _db.Entry(item).State = EntityState.Deleted;
                _db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
               
            }
        }

    }
}
