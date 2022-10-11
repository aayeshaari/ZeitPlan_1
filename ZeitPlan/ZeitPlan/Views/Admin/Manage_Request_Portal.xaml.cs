using Firebase.Database.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using ZeitPlan.View_Model;

namespace ZeitPlan.Views.Admin
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Manage_Request_Portal : ContentPage
    {
        public Manage_Request_Portal()
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
            DataList.ItemsSource = (await App.firebaseDatabase.Child("TBL_REQUEST_PORTAL").OnceAsync<TBL_REQUEST_PORTAL>()).Select(x => new TBL_REQUEST_PORTAL
            {
               REQUEST_PORTAL_ID=x.Object.REQUEST_PORTAL_ID,
               STUDENT_FID=x.Object.STUDENT_FID,
               DEPARTMENT_FID=x.Object.DEPARTMENT_FID,
               TYPE=x.Object.TYPE,
               REQUEST_MESSAGE=x.Object.REQUEST_MESSAGE,
               STATUS=x.Object.STATUS,
               


            }).ToList();
        }

        private async void DataList_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            var selected = e.Item as TBL_REQUEST_PORTAL;

            var item = (await App.firebaseDatabase.Child(" TBL_REQUEST_PORTAL").OnceAsync<TBL_REQUEST_PORTAL>()).FirstOrDefault(a => a.Object.REQUEST_PORTAL_ID == selected.REQUEST_PORTAL_ID);

            var choice = await DisplayActionSheet("Options", "Close", "Delete", "View", "Edit", "FAvoriate", "Archived");
            if (choice == "View")
            {

                await Navigation.PushAsync(new Request_Detail(selected));
               
            }
            if (choice == "Delete")
            {
                var q = DisplayAlert("Confirmation", "Are you sure you want to delete" + item.Object.REQUEST_PORTAL_ID, "Yes", "No");
                if (await q)
                {
                    await App.firebaseDatabase.Child("TBL_REQUEST_PORTAL").Child(item.Key).DeleteAsync();
                    LoadData();
                    await DisplayAlert("Confirmation", item.Object.REQUEST_PORTAL_ID + "Deleted permanently", "ok");
                }
                if (choice == "Edit")
                { }
            }
        }
    }
}