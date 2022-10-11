using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using ZeitPlan.Models;

namespace ZeitPlan.Views.Student
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Student_List : ContentPage
    {
        public Student_List()
        {
            InitializeComponent();
            LoadData();
        }
        async void LoadData()
        {
            DataList.ItemsSource = (await App.firebaseDatabase.Child("Student").OnceAsync<TBL_STUDENT>()).Select(x => new TBL_STUDENT
            {
                STUDENT_ID = x.Object.STUDENT_ID,
                STUDENT_NAME = x.Object.STUDENT_NAME,
               STUDENT_EMAIL = x.Object.STUDENT_EMAIL,
               STUDENT_PASSWORD=x.Object.STUDENT_PASSWORD,
               CLASS_FID=x.Object.CLASS_FID,


            }).ToList();
           

        }

        private void ToolbarItem_Clicked(object sender, EventArgs e)
        {

        }
    }
}