using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace TextFilesChallenge
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        public MainWindow()
        {
            InitializeComponent();
            //assigns the list and sets display data provided from function.
            UserDataListBox.ItemsSource = SetUpUserDataList();
            UserDataListBox.DisplayMemberPath = "DisplayText";

        }


        private ObservableCollection<UserModel> SetUpUserDataList()
        {

            string fileToLoad = "StandardDataSet.csv";
            ParseDataSetFile(fileToLoad);

            return ParseDataSetFile(fileToLoad);
        }

        private ObservableCollection<UserModel> ParseDataSetFile(string filename = null)
        {

            ObservableCollection<UserModel> userDataList = new ObservableCollection<UserModel>();

            Assembly assembly = Assembly.GetExecutingAssembly();
            string resourceName = assembly.GetManifestResourceNames()
                .Single(str => str.EndsWith(filename));

            using (Stream stream = assembly.GetManifestResourceStream(resourceName))
            using (StreamReader reader = new StreamReader(stream))
            {
                string[] fieldNames = reader.ReadLine().Split(',');

                string[] data = reader.ReadToEnd().Split('\n').ToArray();

                if(string.IsNullOrWhiteSpace(data[data.Length-1]))
                {
                    data = data.Take(data.Length - 1).ToArray();
                }

                for (int index =0; index < data.Length; index++)
                {

                    string values = data[index];

                    string[] dataValues = values.Split(',');

                    UserModel newUserData = new UserModel();

                    for (int jindex = 0; jindex < dataValues.Length; jindex++)
                    {
                        Debug.WriteLine($"{fieldNames[jindex]} {dataValues[jindex]} ");

                        PropertyInfo propertyInfo = newUserData.GetType().GetProperty(fieldNames[jindex]);

                       var converter = TypeDescriptor.GetConverter(propertyInfo.PropertyType);

                    //from string to boolean special case
                     if (propertyInfo.PropertyType == typeof(bool))
                        {
                            bool booleanValue = false;
                            int number = int.Parse(dataValues[jindex]);
                            if (number == 1)
                            {
                                booleanValue = true;
                            }

                            newUserData.GetType().GetProperty(fieldNames[jindex]).SetValue(newUserData, booleanValue, null);
                        }
                       else
                        {
                            var result = converter.ConvertFrom(dataValues[jindex]);
                            newUserData.GetType().GetProperty(fieldNames[jindex]).SetValue(newUserData, result, null);

                        }

                    }
                    userDataList.Add(newUserData);
                }

                return userDataList;

            }

        }

        private void AddUserButton_Click(object sender, RoutedEventArgs e)
        {
            AddUser();
        }

        private void AddUser()
        {

        }

        private void SaveListButton_Click(object sender, RoutedEventArgs e)
        {
            SaveList();
        }

        private void SaveList()
        {
         
        }

        private void UserDataListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void FirstNameTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void LastNameTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void AgeTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void IsAliveCheckBox_Click(object sender, RoutedEventArgs e)
        {
    
        }
    }

}
