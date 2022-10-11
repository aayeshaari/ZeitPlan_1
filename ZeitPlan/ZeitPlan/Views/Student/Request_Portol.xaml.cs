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

namespace ZeitPlan.Views.Student
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Request_Portol : ContentPage
    {
        SmtpClient SmtpServer;
        public Request_Portol()
        {
            InitializeComponent();
        }
        private void Button_Clicked(object sender, EventArgs e)
       
        {
            try
            {
                SmtpServer = new SmtpClient("smtp.gmail.com");
                SmtpServer.Port = 587;
                SmtpServer.Host = "smtp.gmail.com";
                SmtpServer.EnableSsl = true;
                SmtpServer.UseDefaultCredentials = false;
                SmtpServer.Credentials = new System.Net.NetworkCredential("aayeshaarif52@gmail.com", GolobalVaribles.Password);
                SmtpServer.SendAsync(GolobalVaribles.FromEmail, email.Text, subject.Text, body.Text, "xyz123d");
                SmtpServer.SendCompleted += emailSendCompleted;
            }
            catch(Exception ex)
            {
                DisplayAlert("Faild", ex.Message, "ok");
            }
        }

        private void email_TextChanged(object sender, TextChangedEventArgs e)
        {
            sendemailbtn.IsEnabled=Regex.IsMatch(email.Text, @"^([a-zA-Z0-9_\-\.]+)@([a-zA-Z0-9_\-\.]+)\.([a-zA-Z]{2,5})$");

        }
        private void emailSendCompleted(Object Sender,AsyncCompletedEventArgs e)
        {
            DisplayAlert("Message", "Mail Send Successfully", "ok");
        }
        //for Radio Button Chosen
        private void RadiobtnC_CheckedChanged(object sender, CheckedChangedEventArgs e)
        {
            RadioButton radioButton = sender as RadioButton;
            lbllable.Text = $"You Have Chosen: {radioButton.Content}";
        }
        //for show radio button
    }
}