using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace COM
{
    public class Common
    {
    }

    public enum Action
    {
        AddUser,
        GetUserByTell,
        AddLesson,
        AddLessonToDB,
        AddSlide,
        AddSlideToDB,
        GetAllLesson,
        GetAllSlide,
        GetLessonByID,
        GetSlideByID,
        GetLessonByNumbers,
        GetSlideByNumbers,
        GetSlideByLesson,
        UpdateSlide,
    }


    public class MegaLesson
    {
        public int LevelNumber { get; set; }
        public int UnitNumber { get; set; }
        public int LessonNumber { get; set; }
        public int SlideNumber { get; set; }
        public string SlideName { get; set; }
        public string Context { get; set; }
    }
}
