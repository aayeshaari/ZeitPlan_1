using Firebase.Database.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ZeitPlan.Views.Admin
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Create_Time_Table : ContentPage
    {
        public Create_Time_Table()
        {
            InitializeComponent();
            LoadData();
        }
        async void LoadData()
        {

            var firebaseList = (await App.firebaseDatabase.Child("TBL_CLASS").OnceAsync<TBL_CLASS>()).Select(x => new TBL_CLASS
            {

                CLASS_NAME = x.Object.CLASS_NAME,

            }).ToList();
            var refinedList = firebaseList.Select(x => x.CLASS_NAME).ToList();
            ddlClass.ItemsSource = refinedList;

            //COURSE_FID
            var firebaseList1 = (await App.firebaseDatabase.Child("TBL_COURSE").OnceAsync<TBL_COURSE>()).Select(x => new TBL_COURSE
            {

                COURSE_NAME = x.Object.COURSE_NAME,

            }).ToList();
            var refinedList1 = firebaseList1.Select(x => x.COURSE_NAME).ToList();
            ddlCourse.ItemsSource = refinedList1;

            //TEACHER_fid
            var firebaseList2 = (await App.firebaseDatabase.Child("TBL_TEACHER").OnceAsync<TBL_TEACHER>()).Select(x => new TBL_TEACHER
            {

                TEACHER_NAME = x.Object.TEACHER_NAME,

            }).ToList();
            var refinedList2 = firebaseList2.Select(x => x.TEACHER_NAME).ToList();
            ddlTeacher.ItemsSource = refinedList2;
            //sLOT_FID
            var firebaseList3 = (await App.firebaseDatabase.Child("TBL_SLOT").OnceAsync<TBL_SLOT>()).Select(x => new TBL_SLOT
            {

                SLOT_NAME = x.Object.SLOT_NAME,

            }).ToList();
            var refinedList3 = firebaseList3.Select(x => x.SLOT_NAME).ToList();
            ddlSlot.ItemsSource = refinedList3;
            //ROOM_FID
            var firebaseList4 = (await App.firebaseDatabase.Child("TBL_ROOM").OnceAsync<TBL_ROOM>()).Select(x => new TBL_ROOM
            {

                ROOM_NO = x.Object.ROOM_NO,

            }).ToList();
            var refinedList4 = firebaseList4.Select(x => x.ROOM_NO).ToList();
            ddlRoom.ItemsSource = refinedList4;

        }
        //private async void btnCreate_Clicked(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        //if (string.IsNullOrEmpty(txtCClass.Text) || string.IsNullOrEmpty(txtCCourse.Text) || string.IsNullOrEmpty(txtCTeacher.Text) || string.IsNullOrEmpty(txtCSlot.Text) || string.IsNullOrEmpty(txtCRoom.Text))
        //        //{
        //        //    await DisplayAlert("Error", "please fill all the required fields try again", "ok");
        //        //    return;
        //        //}
        //        if (ddlClass.SelectedItem == null)
        //        {
        //            await DisplayAlert("Error", "please select Class and try again", "ok");
        //            return;
        //        }
        //        if (ddlCourse.SelectedItem == null)
        //        {
        //            await DisplayAlert("Error", "please select Course and try again", "ok");
        //            return;
        //        }
        //        if (ddlTeacher.SelectedItem == null)
        //        {
        //            await DisplayAlert("Error", "please select Teacher and try again", "ok");
        //            return;
        //        }
        //        if (ddlSlot.SelectedItem == null)
        //        {
        //            await DisplayAlert("Error", "please select Slot and try again", "ok");
        //            return;
        //        }
        //        if (ddlRoom.SelectedItem == null)
        //        {
        //            await DisplayAlert("Error", "please select Room and try again", "ok");
        //            return;
        //        }

        //        //var check = (await App.firebaseDatabase.Child("TBL_CLASS").OnceAsync<TBL_CLASS>()).FirstOrDefault();

        //        //if (check != null)
        //        //{
        //        //    await DisplayAlert("Error", check.Object.CLASS_NAME + "This Class is already Picked .", "ok");
        //        //    return;
        //        //}
        //        //var check1 = (await App.firebaseDatabase.Child("TBL_COURSE").OnceAsync<TBL_COURSE>()).FirstOrDefault();

        //        //if (check1 != null)
        //        //{
        //        //    await DisplayAlert("Error", check1.Object.COURSE_NAME + "This COURSE is already Picked .", "ok");
        //        //    return;
        //        //}
        //        //var check2 = (await App.firebaseDatabase.Child("TBL_TEACHER").OnceAsync<TBL_TEACHER>()).FirstOrDefault();
        //        //if (check2 != null)
        //        //{
        //        //    await DisplayAlert("Error", check2.Object.TEACHER_EMAIL + "This Teacher is already Picked .", "ok");
        //        //    return;
        //        //}
        //        //var check3 = (await App.firebaseDatabase.Child("TBL_SLOT").OnceAsync<TBL_SLOT>()).FirstOrDefault();

        //        //if (check3 != null)
        //        //{
        //        //    await DisplayAlert("Error", check3.Object.SLOT_ID + "This SLOT is already Picked .", "ok");
        //        //    return;
        //        //}
        //        //var check4 = (await App.firebaseDatabase.Child("TBL_ROOM").OnceAsync<TBL_ROOM>()).FirstOrDefault();

        //        //if (check4 != null)
        //        //{
        //        //    await DisplayAlert("Error", check4.Object.ROOM_NO + "This ROOM is already Picked .", "ok");
        //        //    return;
        //        //}
        //        LoadingInd.IsRunning = true;
        //        int LastID, NewID = 1;

        //        var LastRecord = (await App.firebaseDatabase.Child("TBL_TIMETABLE").OnceAsync<TBL_TIMETABLE>()).FirstOrDefault();
        //        if (LastRecord != null)
        //        {
        //            LastID = (await App.firebaseDatabase.Child("TBL_TIMETABLE").OnceAsync<TBL_TIMETABLE>()).Max(a => a.Object.TIMETABLE_ID);
        //            NewID = ++LastID;
        //        }
        //        List<TBL_CLASS> cl = (await App.firebaseDatabase.Child("TBL_CLASS").OnceAsync<TBL_CLASS>()).Select(x => new TBL_CLASS
        //        {
        //            CLASS_NAME = x.Object.CLASS_NAME,
        //            //SESSION = x.Object.SESSION,
        //            //SECTION = x.Object.SECTION,
        //            //SHIFT = x.Object.SHIFT,

        //        }).ToList();
        //        int selected = cl[ddlClass.SelectedIndex].CLASS_ID;
        //        //COURSE_fid
        //        List<TBL_COURSE> cs = (await App.firebaseDatabase.Child("TBL_COURSE").OnceAsync<TBL_COURSE>()).Select(x => new TBL_COURSE
        //        {
        //            //COURSE_ID = x.Object.COURSE_ID,
        //            COURSE_NAME = x.Object.COURSE_NAME

        //        }).ToList();
        //        int selected1 = cs[ddlCourse.SelectedIndex].COURSE_ID;
        //        //TEACHER_FID
        //        List<TBL_TEACHER> te = (await App.firebaseDatabase.Child("TBL_TEACHER").OnceAsync<TBL_TEACHER>()).Select(x => new TBL_TEACHER
        //        {
        //            //TEACHER_ID = x.Object.TEACHER_ID,
        //            TEACHER_NAME = x.Object.TEACHER_NAME,

        //        }).ToList();
        //        int selected2 = te[ddlTeacher.SelectedIndex].TEACHER_ID;
        //        //ROOM_FID
        //        List<TBL_ROOM> ro = (await App.firebaseDatabase.Child("TBL_ROOM").OnceAsync<TBL_ROOM>()).Select(x => new TBL_ROOM
        //        {
        //            ROOM_NO = x.Object.ROOM_NO,


        //        }).ToList();
        //        int selected3 = ro[ddlRoom.SelectedIndex].ROOM_ID;
        //        //SLOT_FID
        //        List<TBL_SLOT> st = (await App.firebaseDatabase.Child("TBL_SLOT").OnceAsync<TBL_SLOT>()).Select(x => new TBL_SLOT
        //        {
        //            SLOT_START_TIME = x.Object.SLOT_START_TIME,
        //            SLOT_END_TIME = x.Object.SLOT_END_TIME,

        //        }).ToList();
        //        int selected4 = st[ddlSlot.SelectedIndex].SLOT_ID;

        //        var Class = (await App.firebaseDatabase.Child("TBL_CLASS").OnceAsync<TBL_CLASS>()).FirstOrDefault(x => x.Object.CLASS_NAME == ddlClass.SelectedItem.ToString());
        //        var Course = (await App.firebaseDatabase.Child("TBL_COURSE").OnceAsync<TBL_COURSE>()).FirstOrDefault(x => x.Object.COURSE_ID == item.Object.COURSE_FID);
        //        var Teacher = (await App.firebaseDatabase.Child("TBL_TEACHER").OnceAsync<TBL_TEACHER>()).FirstOrDefault(x => x.Object.TEACHER_ID == item.Object.TEACHER_FID);
        //        var Slot = (await App.firebaseDatabase.Child("TBL_SLOT").OnceAsync<TBL_SLOT>()).FirstOrDefault(x => x.Object.SLOT_ID == item.Object.SLOT_FID);
        //        var Room = (await App.firebaseDatabase.Child("TBL_ROOM").OnceAsync<TBL_ROOM>()).FirstOrDefault(x => x.Object.ROOM_ID == item.Object.ROOM_FID);



        //        TBL_TIMETABLE tt = new TBL_TIMETABLE()
        //        {
        //            TIMETABLE_ID = NewID,

        //            CLASS_FID = Class.Object.CLASS_ID,
        //            COURSE_FID = selected1,
        //            TEACHER_FID = selected2,
        //            ROOM_FID = selected3,
        //            SLOT_FID = selected4,

        //        };

        //        await App.firebaseDatabase.Child("TBL_TIMETABLE").PostAsync(tt);
        //        LoadingInd.IsRunning = false;
        //        await DisplayAlert("Success", "TIMETABLE Created  ", "Ok");

        //    }
        //    catch (Exception ex)
        //    {
        //        LoadingInd.IsRunning = false;
        //        await DisplayAlert("Error", "Something went wrong Please try again later.\nError:" + ex.Message, "ok");
        //        return;
        //    }
        //}
        private async void btnCreate2_Clicked(object sender, EventArgs e)
        {
            try
            {
                //areej work
                
                    //if (string.IsNullOrEmpty(txtDate.Text) || string.IsNullOrEmpty(txtDay.Text))
                    //{
                    //    await DisplayAlert("Error", "please fill all the required fields try again", "ok");
                    //    return;
                    //}

                    var check = (await App.firebaseDatabase.Child("TBL_TimeTbale").OnceAsync<TBL_TIMETABLE>()).FirstOrDefault();

                if (check != null)
                {
                    await DisplayAlert("Error", check.Object.TIMETABLE_ID + "This Time Table is already added .", "ok");
                    return;
                }

                if (ddlClass.SelectedItem == null)
                {
                    await DisplayAlert("Error", "please select Class and try again", "ok");
                    return;
                }
                if (ddlCourse.SelectedItem == null)
                {
                    await DisplayAlert("Error", "please select Course and try again", "ok");
                    return;
                }
                if (ddlTeacher.SelectedItem == null)
                {
                    await DisplayAlert("Error", "please select Teacher and try again", "ok");
                    return;
                }
                if (ddlSlot.SelectedItem == null)
                {
                    await DisplayAlert("Error", "please select Slot and try again", "ok");
                    return;
                }
                if (ddlRoom.SelectedItem == null)
                {
                    await DisplayAlert("Error", "please select Room and try again", "ok");
                    return;
                }

                LoadingInd.IsRunning = true;
                int LastID, NewID = 1;

                var LastRecord = (await App.firebaseDatabase.Child("TBL_TIMETABLE").OnceAsync<TBL_TIMETABLE>()).FirstOrDefault();
                if (LastRecord != null)
                {
                    LastID = (await App.firebaseDatabase.Child("TBL_TIMETABLE").OnceAsync<TBL_TIMETABLE>()).Max(a => a.Object.TIMETABLE_ID);
                    NewID = ++LastID;
                }

                var Class = (await App.firebaseDatabase.Child("TBL_CLASS").OnceAsync<TBL_CLASS>()).FirstOrDefault(x => x.Object.CLASS_NAME == ddlClass.SelectedItem.ToString());
                var Course = (await App.firebaseDatabase.Child("TBL_COURSE").OnceAsync<TBL_COURSE>()).FirstOrDefault(x => x.Object.COURSE_NAME == ddlCourse.SelectedItem.ToString());
                var Teacher = (await App.firebaseDatabase.Child("TBL_TEACHER").OnceAsync<TBL_TEACHER>()).FirstOrDefault(x => x.Object.TEACHER_NAME == ddlTeacher.SelectedItem.ToString());
                var Slot = (await App.firebaseDatabase.Child("TBL_SLOT").OnceAsync<TBL_SLOT>()).FirstOrDefault(x => x.Object.SLOT_NAME == ddlSlot.SelectedItem.ToString());
                var Room = (await App.firebaseDatabase.Child("TBL_ROOM").OnceAsync<TBL_ROOM>()).FirstOrDefault(x => x.Object.ROOM_NO == ddlRoom.SelectedItem.ToString());
                


                TBL_TIMETABLE tt = new TBL_TIMETABLE()
                {
                    TIMETABLE_ID = NewID,

                    CLASS_FID = Class.Object.CLASS_ID,
                    COURSE_FID = Course.Object.COURSE_ID,
                    TEACHER_FID = Teacher.Object.TEACHER_ID,
                    ROOM_FID = Room.Object.ROOM_ID,
                    SLOT_FID = Slot.Object.SLOT_ID,
                    DATE=date.Date,
                    DAY=txtDay.Text,

                };

                await App.firebaseDatabase.Child("TBL_TIMETABLE").PostAsync(tt);
                LoadingInd.IsRunning = false;
                await DisplayAlert("Success", "TIMETABLE Created  ", "Ok");

            }
            catch (Exception ex)
            {
                LoadingInd.IsRunning = false;
                await DisplayAlert("Error", "Something went wrong Please try again later.\nError:" + ex.Message, "ok");
                return;
            }
        }
    }
}

