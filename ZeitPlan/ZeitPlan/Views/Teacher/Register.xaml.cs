﻿using Firebase.Database.Query;
using Plugin.Media;
using Plugin.Media.Abstractions;
using System;
using System.Linq;
using System.Text.RegularExpressions;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using ZeitPlan.LoginSystem;
using ZeitPlan.Models;

namespace ZeitPlan.Views.Teacher
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Register : ContentPage
    {
        public static string PicPath = "image_picker.png";
        public Register()
        {
            InitializeComponent();
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtPassword.Text) || string.IsNullOrEmpty(txtPhone.Text) || string.IsNullOrEmpty(txtCPassword.Text) || string.IsNullOrEmpty(txtName.Text)|| string.IsNullOrEmpty(txtEmail.Text)|| string.IsNullOrEmpty(txtAddress.Text))
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

                var LastRecord = (await App.firebaseDatabase.Child("TBL_TEACHER").OnceAsync<TBL_TEACHER>()).FirstOrDefault();
                if (LastRecord != null)
                {
                    LastID = (await App.firebaseDatabase.Child("TBL_TEACHER").OnceAsync<TBL_TEACHER>()).Max(a => a.Object.TEACHER_ID);
                    NewID = ++LastID;
                }
               TBL_TEACHER t = new TBL_TEACHER()
                {
                   TEACHER_ID = NewID,
                   TEACHER_NAME = txtName.Text,
                   TEACHER_EMAIL = txtEmail.Text,
                   TEACHER_PASSWORD= txtPassword.Text,
                   TEACHER_PHNO = txtPhone.Text,
                    TEACHER_ADDRESS=txtAddress.Text,

                };
                
                await App.firebaseDatabase.Child("TBL_TEACHER").PostAsync(t);
                LoadingInd.IsRunning = false;
                //App.db.Insert(u);
                await DisplayAlert("Success", "Account Registered", "Ok");
                await Navigation.PushAsync(new Login("Teacher"));
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
        //    if(Regex.IsMatch(e.NewTextValue,EmailPattern))
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
        //    if (e.NewTextValue.Length<8)
        //    {
        //        PasswordValid.IsVisible = true;
        //        PasswordValid.Text = "Password must be at least 8 characters and dosn't contain underscore(_)";
        //        PasswordValid.TextColor = Color.Red;
        //    }
        //    var passwordPattern = @"^(?=.*[A-Za-z])(?=.*\d)(?=.*[$@$!%*#?&])";
        //    if (Regex.IsMatch(e.NewTextValue,passwordPattern))
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

        private async void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Login(""));
        }

        private async void TapGestureRecognizer_Tapped_1(object sender, EventArgs e)
        {
            try
            {
                var response = await DisplayActionSheet("Select Image Source", "Close", "", "From Gallery", "From Camera");
                if (response == "From Camera")
                {
                    await CrossMedia.Current.Initialize();
                    if (!CrossMedia.Current.IsTakePhotoSupported)
                    {
                        await DisplayAlert("Error", "Phone is not Take Photo Supported", "OK");
                        return;
                    }

                    var mediaOptions = new StoreCameraMediaOptions()
                    {
                        PhotoSize = PhotoSize.Medium
                    };

                    var SelectedImg = await CrossMedia.Current.TakePhotoAsync(mediaOptions);

                    if (SelectedImg == null)
                    {
                        await DisplayAlert("Error", "Error Picking Image...", "OK");
                        return;
                    }

                    PicPath = SelectedImg.Path;
                    PreviewPic.Source = SelectedImg.Path;


                }
                if (response == "From Gallery")
                {
                    await CrossMedia.Current.Initialize();
                    if (!CrossMedia.Current.IsPickPhotoSupported)
                    {
                        await DisplayAlert("Error", "Phone is not Pick Photo Supported", "OK");
                        return;
                    }

                    var mediaOptions = new PickMediaOptions()
                    {
                        PhotoSize = PhotoSize.Medium
                    };

                    var SelectedImg = await CrossMedia.Current.PickPhotoAsync(mediaOptions);

                    if (SelectedImg == null)
                    {
                        await DisplayAlert("Error", "Error Picking Image...", "OK");
                        return;
                    }

                    PicPath = SelectedImg.Path;
                    PreviewPic.Source = SelectedImg.Path;

                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Message", "Something Went wrong \n" + ex.Message, "OK");

            }


        
    }
    }
}