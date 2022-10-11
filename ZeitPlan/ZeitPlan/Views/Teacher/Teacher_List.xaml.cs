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
    public partial class Teacher_List : ContentPage
    {
        public Teacher_List()
        {
            InitializeComponent();
            LoadData();
        }

        async void LoadData()
        {
            DataList.ItemsSource = (await App.firebaseDatabase.Child("Teacher").OnceAsync<TBL_TEACHER>()).Select(x => new TBL_TEACHER
            {
                TEACHER_ID = x.Object.TEACHER_ID,
                TEACHER_NAME = x.Object.TEACHER_NAME,
                TEACHER_EMAIL = x.Object.TEACHER_EMAIL,
                TEACHER_PASSWORD=x.Object.TEACHER_PASSWORD,
                TEACHER_PHNO=x.Object.TEACHER_PHNO,
                TEACHER_ADDRESS=x.Object.TEACHER_ADDRESS,
                Image = x.Object.Image,
                DEPARTMENT_FID=x.Object.DEPARTMENT_FID,


            }).ToList();
           

        }

        private void ToolbarItem_Clicked(object sender, EventArgs e)
        {

        }
    }
}