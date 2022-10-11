using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using ZeitPlan.Models;

namespace ZeitPlan.Views.Teacher
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TeacherProfile : ContentPage
    {
        public TeacherProfile(TBL_TEACHER u)
        {
            InitializeComponent();
            lblTeacherid.Text = u.TEACHER_ID.ToString();
            lblName.Text = u.TEACHER_NAME;
            lblEmail.Text = u.TEACHER_EMAIL;
            lblPassword.Text = "*******";
            lblPhone.Text = u.TEACHER_PHNO;
            lblAdress.Text = u.TEACHER_ADDRESS;

        }
    }
}