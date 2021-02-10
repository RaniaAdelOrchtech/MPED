using Microsoft.AspNetCore.Hosting;
using MPMAR.Business.Interfaces;
using MPMAR.Data;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace MPMAR.Web.Admin.Helpers
{
    public class HTMLFileHelper
    {
        private readonly IWebHostEnvironment _IWebHostEnvironment;
        private readonly IPageRouteRepository _pageRouteRepository;
        private readonly IPageSectionVersionRepository _IPageSectionVersionRepository;
        public IPageRouteVersionRepository _pageRouteVersionRepository { get; }
        private readonly ISectionCardVersionRepository _ISectionCardVersionRepository;

        public HTMLFileHelper(IPageRouteRepository pageRouteRepository, IPageRouteVersionRepository pageRouteVersionRepository,
             IPageSectionVersionRepository PageSectionVersionRepository
            , ISectionCardVersionRepository SectionCardVersionRepository, IWebHostEnvironment WebHostEnvironment
            )
        {
            _pageRouteRepository = pageRouteRepository;
            _pageRouteVersionRepository = pageRouteVersionRepository;
            _IPageSectionVersionRepository = PageSectionVersionRepository;
            _ISectionCardVersionRepository = SectionCardVersionRepository;
            _IWebHostEnvironment = WebHostEnvironment;
        }
        /// <summary>
        /// create ar and en dynamic page for specefic page route
        /// </summary>
        /// <param name="id"></param>
        /// <param name="pageRoute"></param>
        public void ApplyPageChanges(int id, PageRoute pageRoute)
        {
            pageRoute.PageFilePathAr = Generatehtmlfile(pageRoute, "ar");
            pageRoute.PageFilePathEn = Generatehtmlfile(pageRoute, "en");

            _pageRouteVersionRepository.ApplyEditRequest(id, pageRoute.PageFilePathAr, pageRoute.PageFilePathEn);
            _pageRouteRepository.UpdatePageRoute(pageRoute);
        }
        /// <summary>
        /// generate html for dynamic page
        /// </summary>
        /// <param name="pageRoute"></param>
        /// <param name="language"></param>
        /// <returns></returns>
        private string Generatehtmlfile(PageRoute pageRoute, string language)
        {
            pageRoute = _pageRouteRepository.Get(pageRoute.Id);
            var pagename = pageRoute.EnName.Replace(' ', '_') + language + ".html";
            //path where the new html page will be generated in
            var relativePath = "\\PublishDynamicPages\\" + Guid.NewGuid() + pagename;
            string filePath = _IWebHostEnvironment.WebRootPath + relativePath;
            FileInfo generatedHtmlFile = new FileInfo(filePath);
            generatedHtmlFile.Directory.Create();

            //ApplyTemplateOnGeneratedHtmlFile
            FileInfo TemplateFile;

            //get the template
            TemplateFile = new FileInfo(_IWebHostEnvironment.WebRootPath + "/DynamicPageTemplate/DynamicPageTemplateAR.html");

            string Template = TemplateFile.OpenText().ReadToEnd();
            var TemplateParts = Template.Split("newsectionsplit");

            //list that will contain the generated section
            var sections = new List<string>();
            //get the sections that will be generated
            var pageSections = _IPageSectionVersionRepository.GetPageSections(pageRoute.Id).Where(x => x.IsActive && !x.IsDeleted).OrderBy(x => x.Order);
            int i = 0;
            foreach (var section in pageSections)
            {
                sections.Add(ApplyTemplateOnGeneratedHtmlFile(section.PageSectionTypeId, section, TemplateParts[section.PageSectionTypeId], language, i++));
            }

            StreamWriter writerInGeneratedHtmlFile = generatedHtmlFile.CreateText();

            //templateHeader marks to be replaced with the header values
            string PageTittle = "&lt;&lt;PageTittle&gt;&gt;";
            string pageNav = "&lt;&lt;PageNav&gt;&gt;";
            string PageHome = "&lt;&lt;PageHome&gt;&gt;";


            //write the header 
            if (language.ToLower() == "en")
            {
                writerInGeneratedHtmlFile.WriteLine(TemplateParts[0].Replace(PageTittle, pageRoute.EnName).Replace(pageNav, pageRoute.NavItem.EnName).Replace(PageHome, "Home"));

            }
            else
            {
                writerInGeneratedHtmlFile.WriteLine(TemplateParts[0].Replace(PageTittle, pageRoute.ArName).Replace(pageNav, pageRoute.NavItem.ArName).Replace(PageHome, "الرئيسية"));
            }

            //write sections
            foreach (var section in sections)
            {
                writerInGeneratedHtmlFile.WriteLine(section);
            }

            //templateFooterandCSS
            writerInGeneratedHtmlFile.WriteLine(TemplateParts[9]);
            writerInGeneratedHtmlFile.Close();
            return relativePath;
        }
        /// <summary>
        /// get the generated section in template
        /// </summary>
        /// <param name="PageSectionTypeId"></param>
        /// <param name="sectionOfPage">section content</param>
        /// <param name="Template"></param>
        /// <param name="language"></param>
        /// <param name="sectionIndex">index of the section in the page</param>
        /// <returns></returns>
        private string ApplyTemplateOnGeneratedHtmlFile(int PageSectionTypeId, PageSection sectionOfPage, string Template, string language, int sectionIndex)
        {
            //marks to be replaced with the real values
            string tittle = "&lt;&lt;tittle&gt;&gt;";
            string desc = "&lt;&lt;description&gt;&gt;";
            string Video = "&lt;&lt;Video&gt;&gt;";
            string image = "&lt;&lt;image&gt;&gt;";
            string CardTittle = "&lt;&lt;Cardtittle&gt;&gt;";
            string CardDesc = "&lt;&lt;Carddescription&gt;&gt;";
            string CardImage = "&lt;&lt;CardImage&gt;&gt;";
            string CardFile = "&lt;&lt;CardFile&gt;&gt;";
            string Download = "&lt;&lt;Download&gt;&gt;";
            var imageBaseURL = "#baseURL#";
            string imageAlign = "&lt;&lt;imageAlign&gt;&gt;";
            string margin = "&lt;&lt;margin&gt;&gt;";
            string marginClass = "&lt;&lt;marginClass&gt;&gt;";

            var text = "";

            if (sectionOfPage != null)
            {
                var templateFields = Template;


                if (PageSectionTypeId == 8)
                {
                    templateFields = Template.Split("cardssplit")[0];

                }
                templateFields = templateFields.Replace(margin, language == "en" ? "margin-left" : "margin-right").Replace(imageAlign, language == "en" ? "align-left" : "align-right");
                if (sectionIndex == 0)
                {
                    if ((language == "en" && string.IsNullOrEmpty(sectionOfPage.EnTitle)) || (language != "en" && string.IsNullOrEmpty(sectionOfPage.ArTitle)))
                    {
                        templateFields = templateFields.Replace(marginClass, "no-margin-all");
                    }
                    else
                    {
                        templateFields = templateFields.Replace(marginClass, "no-margin-top");
                    }

                }

                if (Template.Contains(tittle))
                    templateFields = templateFields.Replace(tittle, language == "en" ? sectionOfPage.EnTitle : sectionOfPage.ArTitle);
                if (Template.Contains(Video))
                    templateFields = templateFields.Replace(Video, sectionOfPage.Url.Replace("watch?v=", "embed/"));
                if (Template.Contains(image))
                    templateFields = templateFields.Replace(image, imageBaseURL + sectionOfPage.Url);
                if (Template.Contains(desc))
                {
                    if ((language == "en" && string.IsNullOrEmpty(sectionOfPage.EnDescription)) || (language != "en" && string.IsNullOrEmpty(sectionOfPage.ArDescription)))
                    {
                        templateFields = templateFields.Replace(desc, "");
                    }
                    else
                    {
                        templateFields = templateFields.Replace(desc, language == "en" ? sectionOfPage.EnDescription.Replace("<div>", "<p>").Replace("</div>", "</p>") : sectionOfPage.ArDescription.Replace("<div>", "<p>").Replace("</div>", "</p>"));
                    }
                }

                text += templateFields;
                //GetCardsOfSection8
                if (PageSectionTypeId == 8)
                {
                    //get cards of section from db
                    var CardsOfSection = _ISectionCardVersionRepository.GetSectionCards(sectionOfPage.Id).OrderBy(x => x.Order);
                    if (CardsOfSection.Count() > 0)
                    {
                        Template = Template.Replace(margin, language == "en" ? "margin-left" : "margin-right").Replace(imageAlign, language == "en" ? "align-left" : "align-right");

                        var Cards = Template.Split("cardssplit");  // to split cards from sections in template
                        var templateCards = Cards[1].Split("newcard");  // to split each card in template

                        text += templateCards[0];  //card head

                        var i = 1;
                        foreach (var CardOfSection in CardsOfSection)
                        {
                            var CardsVal = templateCards[i];
                            if (CardsVal.Contains(CardTittle))
                                CardsVal = CardsVal.Replace(CardTittle, language == "en" ? CardOfSection.EnTitle : CardOfSection.ArTitle);

                            if (CardsVal.Contains(CardDesc))
                            {
                                if ((language == "en" && string.IsNullOrEmpty(CardOfSection.EnDescription)) || (language != "en" && string.IsNullOrEmpty(CardOfSection.ArDescription)))
                                {
                                    templateFields = templateFields.Replace(desc, "");
                                }
                                else
                                {
                                    CardsVal = CardsVal.Replace(CardDesc, language == "en" ? CardOfSection.EnDescription.Replace("<p>", "").Replace("</p>", ""): CardOfSection.ArDescription.Replace("<p>", "").Replace("</p>", ""));
                                }

                            }
                            if (CardsVal.Contains(CardImage))
                            {
                                CardsVal = CardsVal.Replace(CardImage, imageBaseURL + CardOfSection.ImageUrl);
                            }
                            if (CardsVal.Contains(CardFile))
                            {
                                CardsVal = CardsVal.Replace(CardFile, imageBaseURL + CardOfSection.FileUrl);
                            }
                            CardsVal = CardsVal.Replace(Download, language == "en" ? "Download" : "تحميل");



                            text += CardsVal;
                            i++;
                            if (i > 3) //to handle three cards only in one line
                            {
                                i = 1;
                            }
                        }
                        text += templateCards[4]; //card footer
                    }
                }



            }
            return text;
        }

    }
}
