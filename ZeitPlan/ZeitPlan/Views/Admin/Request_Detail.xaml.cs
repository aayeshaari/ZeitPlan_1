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
    public partial class Request_Detail : ContentPage
    {
        public Request_Detail(TBL_REQUEST_PORTAL r)
        {
            InitializeComponent();
            LoadData(r);


        }
        async void LoadData(TBL_REQUEST_PORTAL r)
        {
            try
            {





                var Department = (await App.firebaseDatabase.Child("TBL_DEPARTMENT").OnceAsync<TBL_DEPARTMENT>()).FirstOrDefault(x => x.Object.DEPARTMENT_ID ==r.DEPARTMENT_FID);
                var Student = (await App.firebaseDatabase.Child("TBL_STUDENT").OnceAsync<TBL_STUDENT>()).FirstOrDefault(x => x.Object.STUDENT_ID == r.STUDENT_FID);

                RequestID.Text = r.REQUEST_PORTAL_ID.ToString();
                StudentName.Text = Student.Object.STUDENT_NAME;
                DepartmentName.Text = Department.Object.DEPARTMENT_NAME;
                Type.Text = r.TYPE;
                Message.Text = r.REQUEST_MESSAGE;
                Status.Text = r.STATUS;


            }
            catch (Exception ex)
            {
                LoadingInd.IsRunning = false;
                await DisplayAlert("Error", "Something went wrong Please try again later.\nError:" + ex.Message, "ok");
                return;

            }
        }
    }
}