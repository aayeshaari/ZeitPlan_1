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

namespace ZeitPlan.Views.Teacher
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TeacherSideBarFlyout : ContentPage
    {
        public ListView ListView;

        public TeacherSideBarFlyout()
        {
            InitializeComponent();

            BindingContext = new TeacherSideBarFlyoutViewModel();
            ListView = MenuItemsListView;
        }

        class TeacherSideBarFlyoutViewModel : INotifyPropertyChanged
        {
            public ObservableCollection<TeacherSideBarFlyoutMenuItem> MenuItems { get; set; }

            public TeacherSideBarFlyoutViewModel()
            {
                MenuItems = new ObservableCollection<TeacherSideBarFlyoutMenuItem>(new[]
                {
                    new TeacherSideBarFlyoutMenuItem { Id = 0, Icon="ic_home"  , Title = "Home" ,TargetType=typeof(TeacherHome) },
                    new TeacherSideBarFlyoutMenuItem { Id = 1, Icon="ic_table", Title = "View table",TargetType=typeof(ViewTable) },

                    new TeacherSideBarFlyoutMenuItem { Id = 4, Icon="ic_list", Title = "Teacher List", TargetType=typeof(Teacher_List) },
                    new TeacherSideBarFlyoutMenuItem { Id = 5, Icon="ic_email", Title = "Request Portol", TargetType=typeof(Request_Portol) },
                    //                    new TeacherSideBarFlyoutMenuItem { Id = 2, Icon="ic_prog", Title = "Student Progress" },
                    //new TeacherSideBarFlyoutMenuItem { Id = 3, Icon="ic_mangtime", Title = "Manage Student Progress" },
                    //new TeacherSideBarFlyoutMenuItem { Id = 3, Icon="ic_diary", Title = "Daily Diary" },
                                       
                    //new TeacherSideBarFlyoutMenuItem { Id = 3, Icon="ic_mangiary", Title = "Manage Diary" },

                     new TeacherSideBarFlyoutMenuItem { Id = 8, Icon="ic_phone", Title = "Contact Us", TargetType=typeof(Contact) },
                      new TeacherSideBarFlyoutMenuItem { Id = 9, Icon="ic_group", Title = "About Us", TargetType=typeof(About) },
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