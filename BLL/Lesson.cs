using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class Lesson
    {

        public static List<COM.Slide> GetAllSlide()
        {
            try
            {
                using (var ent = DB.Entity)
                {
                    return ent.Slides.ToList();
                }
            }
            catch (Exception e)
            {
                new System.Threading.Thread(delegate () { Log.DoLog(COM.Action.GetAllSlide, "GetAll", -100, e.Message); });
                return null;
            }
        }

        public static List<COM.Lesson> GetAllLesson()
        {
            try
            {
                using (var ent = DB.Entity)
                {
                    return ent.Lessons.ToList();
                }
            }
            catch (Exception e)
            {
                new System.Threading.Thread(delegate () { Log.DoLog(COM.Action.GetAllLesson, "GetAll", -100, e.Message); });
                return null;
            }
        }

        //public static COM.Lesson GetLessonByID(int LID)
        //{
        //    try
        //    {
        //        using (var ent = DB.Entity)
        //        {
        //            return ent.Lessons.Where(L => L.LID == LID).SingleOrDefault();
        //        }
        //    }
        //    catch (Exception e)
        //    {
        //        Log.DoLog(COM.Action.GetLessonByID, LID.ToString(), -100, e.Message);
        //        return null;
        //    }
        //}

        public static COM.Lesson GetLessonByNumbers(int LevelNumber,int UnitNumber, int LessonNumber)
        {
            try
            {
                using (var ent = DB.Entity)
                {
                    return ent.Lessons.Where(L => L.Level == LevelNumber && L.UnitNumber == UnitNumber && L.LessonNumber == LessonNumber).SingleOrDefault();
                }
            }
            catch (Exception e)
            {
                Log.DoLog(COM.Action.GetLessonByNumbers, LevelNumber.ToString() + " " + UnitNumber.ToString() +" "+ LessonNumber, -100, e.Message);
                return null;
            }
        }

        public static List<COM.Slide> GetSlideByLesson(int LevelNumber, int UnitNumber, int LessonNumber)
        {
            try
            {
                using (var ent = DB.Entity)
                {
                    return ent.Slides.Where(L => L.LevelNumber == LevelNumber && L.UnitNumber == UnitNumber && L.LessonNumber == LessonNumber).ToList();
                }
            }
            catch (Exception e)
            {
                Log.DoLog(COM.Action.GetSlideByLesson, LevelNumber.ToString() + " " + UnitNumber.ToString() + " " + LessonNumber.ToString(), -100, e.Message);
                return null;
            }
        }

        public static COM.Slide GetSlideByNumbers(int LevelNumber, int UnitNumber, int LessonNumber, int SlideNumber)
        {
            try
            {
                using (var ent = DB.Entity)
                {
                    return ent.Slides.Where(L => L.LevelNumber == LevelNumber && L.UnitNumber == UnitNumber && L.LessonNumber == LessonNumber && L.SlideNumber == SlideNumber).SingleOrDefault();
                }
            }
            catch (Exception e)
            {
                Log.DoLog(COM.Action.GetSlideByNumbers, LevelNumber.ToString() + " " + UnitNumber.ToString() + " " + LessonNumber.ToString() +" "+ SlideNumber.ToString(), -100, e.Message);
                return null;
            }
        }
        //public static List<COM.Slide> GetSlideByLesson(int ChapterNumber, int LessonNumber)
        //{
        //    try
        //    {
        //        using (var ent = DB.Entity)
        //        {
        //            return ent.Slides.Where(L => L.ChapterNumber == ChapterNumber && L.LessonNumber == LessonNumber).ToList();
        //        }
        //    }
        //    catch (Exception e)
        //    {
        //        Log.DoLog(COM.Action.GetSlideByLesson, ChapterNumber.ToString() + " " + LessonNumber.ToString(), -100, e.Message);
        //        return null;
        //    }
        //}
        public static COM.Slide GetSlideByID(int SID)
        {
            try
            {
                using (var ent = DB.Entity)
                {
                    return ent.Slides.Where(L => L.SID == SID).SingleOrDefault();
                }
            }
            catch (Exception e)
            {
                Log.DoLog(COM.Action.GetSlideByID, SID.ToString(), -100, e.Message);
                return null;
            }
        }


        public static bool UpdateSlide(COM.Slide mSlide)
        {
            try
            {
                using (var ent = DB.Entity)
                {
                    ent.Slides.Attach(mSlide);
                    var Entry = ent.Entry(mSlide);
                    Entry.Property(ex => ex.Context).IsModified = true;
                    Entry.Property(ex => ex.SlideName).IsModified = true;
                    ent.SaveChanges();
                    return true;
                }
            }
            catch (Exception e)
            {
                Log.DoLog(COM.Action.UpdateSlide, mSlide.SID.ToString(), -100, e.Message);
                return false;
            }
        }

        //public static int AddLesson(COM.Lesson mLesson)
        //{
        //    try
        //    {
        //        using (var ent = DB.Entity)
        //        {
        //            ent.Lessons.Add(mLesson);
        //            ent.SaveChanges();

        //            return mLesson.LID;
        //        }
        //    }
        //    catch (Exception e)
        //    {
        //        new System.Threading.Thread(delegate () { Log.DoLog(COM.Action.AddLessonToDB, "AddLesson", -100, e.Message); });
        //        return -100;
        //    }
        //}
        public static int AddSlide(COM.Slide mSlide)
        {
            try
            {
                using (var ent = DB.Entity)
                {
                    ent.Slides.Add(mSlide);
                    ent.SaveChanges();

                    return mSlide.SID;
                }
            }
            catch (Exception e)
            {
                new System.Threading.Thread(delegate () { Log.DoLog(COM.Action.AddSlideToDB, "AddSlide", -100, e.Message); });
                return -100;
            }
        }

    }
}
