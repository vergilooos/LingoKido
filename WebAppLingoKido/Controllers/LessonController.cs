using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Script.Serialization;

namespace WebAppLingoKido.Controllers
{
    public class LessonController : ApiController
    {

        [AcceptVerbs("Get")]
        public string GetServerStatus(int Ping)
        {
            return "Pong" + Ping.ToString();
        }
        [AcceptVerbs("Get")]
        public List<COM.Lesson> GetListAllLesson()
        {
            return BLL.Lesson.GetAllLesson();
        }
        [AcceptVerbs("Get")]
        public List<COM.Slide> GetAllSlide()
        {
            return BLL.Lesson.GetAllSlide();
        }
        [AcceptVerbs("Get")]
        public List<COM.Slide> GetAllSlideOfLesson(int Level, int Unit,int Lesson)
        {
            return BLL.Lesson.GetSlideByLesson(Level,Unit ,Lesson);
        }
        [AcceptVerbs("Get")]
        public COM.Slide GetSlideByNumbers(int Level, int Unit,int Lesson, int Slide)
        {
            return BLL.Lesson.GetSlideByNumbers(Level, Unit,Lesson, Slide);
        }
        [AcceptVerbs("Get")]
        public COM.Slide GetSlideBySID(int SID)
        {
            return BLL.Lesson.GetSlideByID(SID);
        }

        [AcceptVerbs("Post")]
        public async Task<int> AddSilde()
        {
            try
            {
                if (!Request.Content.IsMimeMultipartContent())
                {
                    new System.Threading.Thread(delegate () { BLL.Log.DoLog(COM.Action.AddSlide, "", -101, "MimeMultipartContent"); }).Start();
                    return -101;
                }

                var filesReadToProvider = await Request.Content.ReadAsMultipartAsync();

                COM.MegaLesson megaLesson = new COM.MegaLesson();

                COM.Slide PubSlide = new COM.Slide();

                foreach (var itemContent in filesReadToProvider.Contents)
                {
                    if (itemContent.Headers.ContentDisposition.Name == "Object" || itemContent.Headers.ContentDisposition.Name == "\"Object\"")
                    {
                        var jsonString = await itemContent.ReadAsStringAsync();
                        var serializer = new JavaScriptSerializer();
                        megaLesson = serializer.Deserialize<COM.MegaLesson>(jsonString);

                        //COM.Lesson lesson = BLL.Lesson.GetLessonByNumbers(megaLesson.ChapterNumber, megaLesson.LessonNumber);
                        //if (lesson == null)
                        //{
                        //    lesson = new COM.Lesson()
                        //    {
                        //        ChapterName = megaLesson.ChapterName,
                        //        ChapterNumber = megaLesson.ChapterNumber,
                        //        LessonName = megaLesson.LessonName,
                        //        LessonNumber = megaLesson.LessonNumber,
                        //    };
                        //    lesson.LID = BLL.Lesson.AddLesson(lesson);
                        //    if (lesson.LID < 0)
                        //    {
                        //        new System.Threading.Thread(delegate () { BLL.Log.DoLog(COM.Action.AddSlide, "", -102, "cant Add Lesson to DB"); }).Start();
                        //        return -102;
                        //    }
                        //}

                        COM.Slide slide = BLL.Lesson.GetSlideByNumbers(megaLesson.LevelNumber,megaLesson.UnitNumber, megaLesson.LessonNumber, megaLesson.SlideNumber);
                        if (slide == null)
                        {
                            slide = new COM.Slide()
                            {
                                LevelNumber = megaLesson.LevelNumber,
                                UnitNumber = megaLesson.UnitNumber,
                                SlideNumber = megaLesson.SlideNumber,
                                LessonNumber = megaLesson.LessonNumber,
                                Context = megaLesson.Context,
                                SlideName = megaLesson.SlideName
                            };

                            slide.SID = BLL.Lesson.AddSlide(slide);
                            if (slide.SID < 0)
                            {
                                new System.Threading.Thread(delegate () { BLL.Log.DoLog(COM.Action.AddSlide, "", -103, "cant Add Slide to DB"); }).Start();
                                return -103;
                            }
                        }
                        else
                        {
                            slide.Context = megaLesson.Context;
                            slide.SlideName = megaLesson.SlideName;
                            bool ResUpdate = BLL.Lesson.UpdateSlide(slide);
                            if (!ResUpdate)
                            {
                                new System.Threading.Thread(delegate () { BLL.Log.DoLog(COM.Action.AddSlide, "", -104, "cant Update Slide in DB"); }).Start();
                                return -104;
                            }
                        }

                        PubSlide = slide;
                    }
                    else if (itemContent.Headers.ContentDisposition.Name == "File" || itemContent.Headers.ContentDisposition.Name == "\"File\"")
                    {
                        var fileBytes = await itemContent.ReadAsByteArrayAsync();
                        string FileName = itemContent.Headers.ContentDisposition.FileName;

                        string NameNewFile = System.Web.Hosting.HostingEnvironment.MapPath("~/LingoKidoResource/Picture/" +
                            PubSlide.LevelNumber + "/" + PubSlide.UnitNumber + "/"+ PubSlide.LessonNumber + "/" + PubSlide.SlideNumber);

                        if (!Directory.Exists(NameNewFile))
                            Directory.CreateDirectory(NameNewFile);

                        FileName = FileName.Replace("\"", string.Empty);
                        NameNewFile = NameNewFile + "/" + FileName;

                        if (File.Exists(NameNewFile))
                        {
                            File.Delete(NameNewFile);
                        }
                        File.WriteAllBytes(NameNewFile, fileBytes);
                    }
                    else
                    {
                        new System.Threading.Thread(delegate () { BLL.Log.DoLog(COM.Action.AddSlide, "", -105, "Ye Chize ezafi too req boode"); }).Start();
                        return -105;
                    }
                }

                new System.Threading.Thread(delegate () { BLL.Log.DoLog(COM.Action.AddSlide, PubSlide.SID.ToString(), PubSlide.SID, ""); }).Start();
                return PubSlide.SID;//OK Return
            }
            catch (Exception ee)
            {
                new System.Threading.Thread(delegate () { BLL.Log.DoLog(COM.Action.AddSlide, "", -100, ee.Message); }).Start();
                return -100;
            }
        }

    }
}
