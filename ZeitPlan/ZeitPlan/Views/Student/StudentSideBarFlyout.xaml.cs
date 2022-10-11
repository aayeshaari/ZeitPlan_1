using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using ZeitPlan.Views.Admin;

namespace ZeitPlan.Views.Student
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class StudentSideBarFlyout : ContentPage
    {
        public ListView ListView;

        public StudentSideBarFlyout()
        {
            InitializeComponent();

            BindingContext = new StudentSideBarFlyoutViewModel();
            ListView = MenuItemsListView;
        }

        class StudentSideBarFlyoutViewModel : INotifyPropertyChanged
        {
            public ObservableCollection<StudentSideBarFlyoutMenuItem> MenuItems { get; set; }

            public StudentSideBarFlyoutViewModel()
            {
                MenuItems = new ObservableCollection<StudentSideBarFlyoutMenuItem>(new[]
                {
                    new StudentSideBarFlyoutMenuItem { Id = 0, Icon="ic_home.png"  , Title = "Home" ,TargetType=typeof(StudentHome) },
                    new StudentSideBarFlyoutMenuItem { Id = 0, Icon="ic_profile"  , Title = "Profile" ,TargetType=typeof(Profile) },
                    new StudentSideBarFlyoutMenuItem { Id = 1, Icon="ic_table.png", Title = "Time Table",TargetType=typeof(Mange_TimeTable) },
                    //new StudentSideBarFlyoutMenuItem { Id = 2, Icon="ic_prog.png", Title = "Student Progress" },
                    //new StudentSideBarFlyoutMenuItem { Id = 3, Icon="ic_diary.png", Title = "Teacher Diary" },
                    new StudentSideBarFlyoutMenuItem { Id = 4, Icon="ic_list", Title = " Student List", TargetType=typeof(Student_List) },
                    new StudentSideBarFlyoutMenuItem { Id = 5, Icon="ic_email", Title = "Request portal", TargetType=typeof(Request_Portol) },
                                    new StudentSideBarFlyoutMenuItem { Id = 4, Icon="ic_phone", Title = "Contact Us", TargetType=typeof(Contact) },
                    new StudentSideBarFlyoutMenuItem { Id = 5, Icon="ic_group", Title = "About Us", TargetType=typeof(About) },
                });
            }

            #region INotifyPropertyChanged Implementation
            public event PropertyChangedEventHandler PropertyChanged;
            void OnPropertyChanged([CallerMemberName] string propertyName = "")
            {
                if (PropertyChanged == null)
                    return;

                PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }
            #endregion
        }
    }
}