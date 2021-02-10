using MPMAR.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace MPMAR.Business.Interfaces
{
   public interface ISocialMediaVersionRepository
    {
        /// <summary>
        /// add new SocialMediaVersion
        /// </summary>
        /// <param name="footerMenuItem"></param>
        /// <returns></returns>
        SocialMediaVersion Add(SocialMediaVersion footerMenuItem);
        /// <summary>
        /// update SocialMediaVersion
        /// </summary>
        /// <param name="footerMenuItem"></param>
        /// <returns></returns>
        SocialMediaVersion Update(SocialMediaVersion footerMenuItem);
        /// <summary>
        /// get FooterSocialMediaLink 
        /// </summary>
        /// <returns></returns>
        IEnumerable<SocialMediaVersion> GetFooterSocialMediaLink();
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        bool Delete(int id);
        /// <summary>
        /// get SocialMediaVersion 
        /// </summary>
        /// <param name="id">SocialMediaVersion id</param>
        /// <returns></returns>
        SocialMediaVersion GetById(int id);
        /// <summary>
        /// get SocialMediaVersion 
        /// </summary>
        /// <param name="id">SocialMediaVersion id</param>
        /// <returns></returns>
        SocialMediaVersion GetDetail(int id);
        /// <summary>
        /// get list of SocialMediaVersion
        /// </summary>
        /// <returns></returns>
        IEnumerable<SocialMediaVersion> GetAll();
        /// <summary>
        /// get list of all drafts SocialMediaVersion
        /// </summary>
        /// <returns></returns>
        IEnumerable<SocialMediaVersion>  GetAllDrafts();
        /// <summary>
        /// get list of all submited SocialMediaVersion
        /// </summary>
        /// <returns></returns>
        IEnumerable<SocialMediaVersion> GetAllSubmitted();
        /// <summary>
        /// get SocialMediaVersion by SocialMedia id
        /// </summary>
        /// <param name="socialId">SocialMedia id</param>
        /// <returns></returns>
        SocialMediaVersion GetBySocialId(int socialId);
    }
}
