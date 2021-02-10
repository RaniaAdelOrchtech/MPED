using MPMAR.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace MPMAR.Business.Interfaces
{
    public interface ISocialMediaRepository
    {
        /// <summary>
        /// add new SocialMedia
        /// </summary>
        /// <param name="footerMenuItem"></param>
        /// <returns></returns>
        SocialMedia Add(SocialMedia footerMenuItem);
        /// <summary>
        /// update SocialMedia
        /// </summary>
        /// <param name="footerMenuItem"></param>
        /// <returns></returns>
        SocialMedia Update(SocialMedia footerMenuItem);
        /// <summary>
        /// get footer soicial media links 
        /// </summary>
        /// <returns></returns>
        IEnumerable<SocialMedia> GetFooterSocialMediaLink();
        /// <summary>
        /// delete SocialMedia
        /// </summary>
        /// <param name="id">SocialMedia id</param>
        /// <returns></returns>
        bool Delete(int id);
        /// <summary>
        /// get SocialMedia
        /// </summary>
        /// <param name="id">SocialMedia id</param>
        /// <returns></returns>
        SocialMedia Get(int id);
        /// <summary>
        /// get SocialMedia with no tracking
        /// </summary>
        /// <param name="id">SocialMedia id</param>
        /// <returns></returns>
        SocialMedia GetByIdWithNoTracking(int id);
        /// <summary>
        /// get SocialMedia
        /// </summary>
        /// <param name="id">SocialMedia id</param>
        /// <returns></returns>
        SocialMedia GetDetail(int id);
    }
}
