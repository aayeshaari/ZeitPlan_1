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
    public partial class Manage_Teacher : ContentPage
    {
        public Manage_Teacher()
        {
            InitializeComponent();
        }
        protected async override void OnAppearing()
        {
            base.OnAppearing();
            try
            {
                LoadingInd.IsRunning = true;
                LoadData();
                LoadingInd.IsRunning = false;
            }
            catch (Exception ex)
            {
                LoadingInd.IsRunning = false;
                await DisplayAlert("Error", "Something went wrong, please  try again later" + ex.Message, "ok");
                return;
            }
        }

        async void LoadData()
        {
            DataList.ItemsSource = (await App.firebaseDatabase.Child("TBL_TEACHER").OnceAsync<TBL_TEACHER>()).Select(x => new TBL_TEACHER
            {
                TEACHER_ID = x.Object.TEACHER_ID,
                TEACHER_NAME = x.Object.TEACHER_NAME,
                TEACHER_ADDRESS = x.Object.TEACHER_ADDRESS,
                TEACHER_PASSWORD= x.Object.TEACHER_PASSWORD,
                TEACHER_EMAIL=x.Object.TEACHER_EMAIL,
                TEACHER_PHNO=x.Object.TEACHER_PHNO,
                DEPARTMENT_FID=x.Object.DEPARTMENT_FID,
                //TIMETABLE_FID=x.Object.TIMETABLE_FID,
                Image = x.Object.Image,


            }).ToList();
        }

            private async void DataList_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            var selected = e.Item as TBL_TEACHER;

            var item = (await App.firebaseDatabase.Child("TBL_TEACHER").OnceAsync<TBL_TEACHER>()).FirstOrDefault(a => a.Object.TEACHER_ID == selected.TEACHER_ID);

            var choice = await DisplayActionSheet("Options", "Close", "Delete", "View", "Edit", "FAvoriate", "Archived");
            if (choice == "View")
            {
                //await DisplayAlert("Detail", "" +
                //    "\nCat ID:" + item.Object.CatId +
                //    " \nName:" + item.Object.Name +
                //    "\nEmail:" + item.Object.Email +
                //    "\nPassword:  *******" +
                //    "\nPhone:" + item.Object.Phone +
                //   "", "ok");
                await Navigation.PushAsync(new Teacher_Detail(selected));
               
            }
            if (choice == "Delete")
            {
                var q = DisplayAlert("Confirmation", "Are you sure you want to delete" + item.Object.TEACHER_ID, "Yes", "No");
                if (await q)
                {
                    await App.firebaseDatabase.Child("TBL_TEACHER").Child(item.Key).DeleteAsync();
                    LoadData();
                    await DisplayAlert("Confirmation", item.Object.TEACHER_ID+ "Deleted permanently", "ok");
                }
            }
            if (choice == "Edit")
            { }
        }
    }
}