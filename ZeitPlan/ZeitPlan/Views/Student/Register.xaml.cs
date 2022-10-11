using Firebase.Database.Query;
using System;
using System.Linq;
using System.Text.RegularExpressions;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using ZeitPlan.LoginSystem;
using ZeitPlan.Models;

namespace ZeitPlan.Views.Student
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Register : ContentPage
    {
        public Register()
        {
            InitializeComponent();
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtPassword.Text) ||   string.IsNullOrEmpty(txtCPassword.Text) || string.IsNullOrEmpty(txtName.Text)|| string.IsNullOrEmpty(txtEmail.Text) )
                {
                    await DisplayAlert("Error", "please fill all the required fields try again", "ok");
                    return;
                }
                if (txtCPassword.Text != txtPassword.Text)
                {
                    await DisplayAlert("Error", "Password do not match", "ok");
                    return;
                }


                //App.db.CreateTable<users>();
                //var check = App.db.Table<users>().FirstOrDefault(x => x.Email == txtEmail.Text);
                //if (check != null)
                //{
                //    await DisplayAlert("Error", "This Email is already registered.", "ok");
                //    return;
                //}
                LoadingInd.IsRunning = true;
                int LastID, NewID = 1;

                var LastRecord = (await App.firebaseDatabase.Child("Student").OnceAsync<TBL_STUDENT>()).FirstOrDefault();
                if (LastRecord != null)
                {
                    LastID = (await App.firebaseDatabase.Child("Student").OnceAsync<TBL_STUDENT>()).Max(a => a.Object.STUDENT_ID);
                    NewID = ++LastID;
                }
                TBL_STUDENT u = new TBL_STUDENT()
                {
                    STUDENT_ID = NewID,
                    STUDENT_NAME = txtName.Text,
                    STUDENT_EMAIL = txtEmail.Text,
                    STUDENT_PASSWORD = txtPassword.Text
                   /*CLASS_FID = txtClassFID.Text*/,

                };
                
                await App.firebaseDatabase.Child("Student").PostAsync(u);
                LoadingInd.IsRunning = false;
                //App.db.Insert(u);
                await DisplayAlert("Success", "Account Registered", "Ok");
                await Navigation.PushAsync(new Login("Student"));
            }
            catch (Exception ex)
            {
                LoadingInd.IsRunning = false;
                await DisplayAlert("Error", "Something went wrong Please try again later.\nError:" + ex.Message, "ok");
                return;
            }
        }

        //private void txtPhone_TextChanged(object sender, TextChangedEventArgs e)
        //{
        //    if (e.NewTextValue.Length<11 && e.NewTextValue.Length>13)
        //    {
        //        PhoneNumbervalid.IsVisible = true;
        //        PhoneNumbervalid.Text = "InValid Phone!";
        //        PhoneNumbervalid.TextColor = Color.Red;
        //    }
        //    else
        //    {
        //        PhoneNumbervalid.Text = "Valid Phone";
        //        PhoneNumbervalid.TextColor = Color.Green;
        //    }
        //}

        //private void txtEmail_TextChanged(object sender, TextChangedEventArgs e)
        //{
        //    var EmailPattern = @"^([a-zA-Z0-9_\-\.]+)@([a-zA-Z0-9_\-\.]+)\.([a-zA-Z]{2,5})$";
        //    if (Regex.IsMatch(e.NewTextValue, EmailPattern))
        //    {
        //        EmailValid.IsVisible = true;
        //        EmailValid.Text = "Valid Email";
        //        EmailValid.TextColor = Color.Green;
        //    }
        //    else
        //    {
        //        EmailValid.Text = "InValid Email! Email must contain @ and .com";
        //        EmailValid.TextColor = Color.Red;
        //    }
        //}

        //private void txtPassword_TextChanged(object sender, TextChangedEventArgs e)
        //{
        //    if (e.NewTextValue.Length < 8)
        //    {
        //        PasswordValid.IsVisible = true;
        //        PasswordValid.Text = "Password must be at least 8 characters and dosn't contain underscore(_)";
        //        PasswordValid.TextColor = Color.Red;
        //    }
        //    var passwordPattern = @"^(?=.*[A-Za-z])(?=.*\d)(?=.*[$@$!%*#?&])";
        //    if (Regex.IsMatch(e.NewTextValue, passwordPattern))
        //    {

        //        PasswordValid.Text = "Strong Password";
        //        PasswordValid.TextColor = Color.Green;
        //    }
        //    else
        //    {
        //        PasswordValid.Text = "Weak Password! Password must  contain Special_characters(@$!%*#?&),uppercase_letters,Lowercase_letters,Number(s)";
        //        PasswordValid.TextColor = Color.Red;
        //    }
        //}

        private async  void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Login(""));
        }
    }
}