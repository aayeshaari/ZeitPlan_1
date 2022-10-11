using ZeitPlan.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using ZeitPlan.Views.Admin;
using ZeitPlan.Views.User;
using ZeitPlan.Views.Teacher;
using ZeitPlan.Views.Student;

namespace ZeitPlan.LoginSystem
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Login : ContentPage
    {
        public string type;
        public Login(string t)
        {
            type = t;
            InitializeComponent();
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {

            if (string.IsNullOrEmpty(txtEmail.Text) || string.IsNullOrEmpty(txtPassword.Text))
            {
                await DisplayAlert("Error", "please fill all the required fields try again", "ok");
                return;
            }
           
            try
            {
                LoadingInd.IsRunning = true;
               
                //App.db.CreateTable<users>();
                //var check = App.db.Table<users>().FirstOrDefault(x => x.Email == txtEmail.Text && x.Password == txtPassword.Text);
               

                if (type == "Admin")
                {
                    var check = (await App.firebaseDatabase.Child("Users").OnceAsync<users>()).FirstOrDefault(x => x.Object.Email == txtEmail.Text && x.Object.Password == txtPassword.Text);
                    if (check == null)
                    {
                        LoadingInd.IsRunning = false;
                        await DisplayAlert("Error", "Email or password incorrect", "ok");
                        return;
                    }
                    App.Current.MainPage = new AdminSideBar();
                }
                if(type=="Teacher")
                {
                    var check = (await App.firebaseDatabase.Child("TBL_TEACHER").OnceAsync<TBL_TEACHER>()).FirstOrDefault(x => x.Object.TEACHER_EMAIL == txtEmail.Text && x.Object.TEACHER_PASSWORD == txtPassword.Text);
                    if (check == null)
                    {
                        LoadingInd.IsRunning = false;
                        await DisplayAlert("Error", "Email or password incorrect", "ok");
                        return;
                    }
                    App.Current.MainPage = new TeacherSideBar();
                }
                if (type == "Student")
                {
                    var check = (await App.firebaseDatabase.Child("TBL_STUDENT").OnceAsync<TBL_STUDENT>()).FirstOrDefault(x => x.Object.STUDENT_EMAIL == txtEmail.Text && x.Object.STUDENT_PASSWORD == txtPassword.Text);
                    if (check == null)
                    {
                        LoadingInd.IsRunning = false;
                        await DisplayAlert("Error", "Email or password incorrect", "ok");
                        return;
                    }

                    App.Current.MainPage = new StudentSideBar();
                }

            }
            catch (Exception ex)
            {
                LoadingInd.IsRunning = false;
                await DisplayAlert("Error", "Something went wrong Please try again later.\nError:" + ex.Message, "ok");
                return;
            }


        }
        private async void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            if(type == "Teacher")
            {
                await Navigation.PushAsync(new Views.Teacher.Register());
            }
            if (type == "Student")
            {
                await Navigation.PushAsync(new Views.Student.Register());
            }
            
        }


    }
}