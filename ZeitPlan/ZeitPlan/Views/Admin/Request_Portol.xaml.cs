using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text.RegularExpressions;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using ZeitPlan.Login_System;
using System.ComponentModel;
using Firebase.Database.Query;

namespace ZeitPlan.Views.Admin
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Request_Portol : ContentPage
    {
        //SmtpClient SmtpServer;
        public Request_Portol()
        {
            InitializeComponent();
            LoadData();
        }
        async void LoadData()
        {
            //Department Fid
            var firebaseList = (await App.firebaseDatabase.Child("TBL_DEPARTMENT").OnceAsync<TBL_DEPARTMENT>()).Select(x => new TBL_DEPARTMENT
            {

                DEPARTMENT_NAME = x.Object.DEPARTMENT_NAME,

            }).ToList();
            var refinedList = firebaseList.Select(x => x.DEPARTMENT_NAME).ToList();
            ddldept.ItemsSource = refinedList;

            //Student Fid
            var firebaseList1 = (await App.firebaseDatabase.Child("TBL_STUDENT").OnceAsync<TBL_STUDENT>()).Select(x => new TBL_STUDENT
            {

               STUDENT_NAME = x.Object.STUDENT_NAME,

            }).ToList();
            var refinedList1 = firebaseList1.Select(x => x.STUDENT_NAME).ToList();
            ddlStudent.ItemsSource = refinedList1;

        }
        private async void sendbtn_Clicked(object sender, EventArgs e)

        {
            try
            {

                if (ddldept.SelectedItem == null)
                {
                    await DisplayAlert("Error", "please select Degree and try again", "ok");
                    return;
                }

                if (ddltype.SelectedItem == null)
                {
                    await DisplayAlert("Error", "please select Degree and try again", "ok");
                    return;
                }
                LoadingInd.IsRunning = true;
                int LastID, NewID = 1;

                var LastRecord = (await App.firebaseDatabase.Child("TBL_REQUEST_PORTAL").OnceAsync<TBL_REQUEST_PORTAL>()).FirstOrDefault();
                if (LastRecord != null)
                {
                    LastID = (await App.firebaseDatabase.Child("TBL_REQUEST_PORTAL").OnceAsync<TBL_REQUEST_PORTAL>()).Max(a => a.Object.REQUEST_PORTAL_ID);
                    NewID = ++LastID;
                }
                var Department = (await App.firebaseDatabase.Child("TBL_DEPARTMENT").OnceAsync<TBL_DEPARTMENT>()).FirstOrDefault(x => x.Object.DEPARTMENT_NAME == ddldept.SelectedItem.ToString());
                var Student = (await App.firebaseDatabase.Child("TBL_STUDENT").OnceAsync<TBL_STUDENT>()).FirstOrDefault(x => x.Object.STUDENT_NAME == ddlStudent.SelectedItem.ToString());
                TBL_REQUEST_PORTAL t = new TBL_REQUEST_PORTAL()
                {
                    REQUEST_PORTAL_ID = NewID,
                    STUDENT_FID = Student.Object.STUDENT_ID,
                    DEPARTMENT_FID = Department.Object.DEPARTMENT_ID,
                    REQUEST_MESSAGE = Subject.Text,
                    TYPE = ddltype.SelectedItem.ToString(),
                };

                await App.firebaseDatabase.Child("TBL_REQUEST_PORTAL").PostAsync(t);
                LoadingInd.IsRunning = false;
                await DisplayAlert("Success", "Request Added  ", "Ok");
            }
            catch (Exception ex)
            {

                LoadingInd.IsRunning = false;
                await DisplayAlert("Error", "Something went wrong Please try again later.\nError:" + ex.Message, "ok");
                return;
            }
            //        try
            //        {
            //            SmtpServer = new SmtpClient("smtp.gmail.com");
            //            SmtpServer.Port = 587;
            //            SmtpServer.Host = "smtp.gmail.com";
            //            SmtpServer.EnableSsl = true;
            //            SmtpServer.UseDefaultCredentials = false;
            //            SmtpServer.Credentials = new System.Net.NetworkCredential("aayeshaarif52@gmail.com", GolobalVaribles.Password);
            //            SmtpServer.SendAsync(GolobalVaribles.FromEmail, email.Text, subject.Text, body.Text, "xyz123d");
            //            SmtpServer.SendCompleted += emailSendCompleted;
            //        }
            //        catch(Exception ex)
            //        {
            //            DisplayAlert("Faild", ex.Message, "ok");
            //        }
            //    }

            //    private void email_TextChanged(object sender, TextChangedEventArgs e)
            //    {
            //        sendbtn.IsEnabled=Regex.IsMatch(.Text, @"^([a-zA-Z0-9_\-\.]+)@([a-zA-Z0-9_\-\.]+)\.([a-zA-Z]{2,5})$");

            //    }
            //    private void emailSendCompleted(Object Sender,AsyncCompletedEventArgs e)
            //    {
            //        DisplayAlert("Message", "Mail Send Successfully", "ok");
            //    }
            //}

        }
    }
}