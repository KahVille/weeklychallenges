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
        private readonly ObservableCollection<UserModel> userDataList = null;
        private string[] fieldNames;
        private readonly string fileToLoad = "StandardDataSet.csv";

        public MainWindow()
        {
            InitializeComponent();


            userDataList = SetUpUserDataList(fileToLoad);
            //assigns the list and sets display data provided from function.
            UserDataListBox.ItemsSource = userDataList;
            UserDataListBox.DisplayMemberPath = "DisplayText";

        }

        private ObservableCollection<UserModel> SetUpUserDataList(string fileName = null)
        {
            return ParseDataSetFile(fileName);
        }

        private ObservableCollection<UserModel> ParseDataSetFile(string filename = null)
        {

            ObservableCollection<UserModel> userList = new ObservableCollection<UserModel>();

            List<string> fileData = File.ReadAllLines(filename).ToList();
            fieldNames = fileData[0].Split(',');


            foreach (var item in fileData.Skip(1))
            {
                string[] dataValues = item.Split(',');

                UserModel newUserData = new UserModel();

                for (int jindex = 0; jindex < dataValues.Length; jindex++)
                {
                    PropertyInfo propertyInfo = newUserData.GetType().GetProperty(fieldNames[jindex]);
                    var converter = TypeDescriptor.GetConverter(propertyInfo.PropertyType);
                    string valueToCheck = dataValues[jindex];

                    SetDataValue(propertyInfo, newUserData, valueToCheck);
                }
                userList.Add(newUserData);

            }
            return userList;
        }

        private void SetDataValue(PropertyInfo propertyInfo, UserModel newUserData, string dataValue)
        {
            var converter = TypeDescriptor.GetConverter(propertyInfo.PropertyType);

            //from string to boolean special case
            if (propertyInfo.PropertyType == typeof(bool))
            {
                int number = int.Parse(dataValue);
                bool booleanValue = (number == 1) ? true: false;

                propertyInfo.SetValue(newUserData, booleanValue, null);
            }
            else
            {
                var result = converter.ConvertFrom(dataValue);
                propertyInfo.SetValue(newUserData, result, null);
            }
        }

        private void AddUserButton_Click(object sender, RoutedEventArgs e)
        {
            AddUser();
        }

        private void AddUser()
        {

            userDataList.Add(
                new UserModel()
                {
                    FirstName = FirstNameTextBox.Text,
                    LastName = LastNameTextBox.Text,
                    Age = Convert.ToInt32(AgeTextBox.Text),
                    IsAlive = IsAliveCheckBox.IsEnabled
                });

        }

        private void SaveListButton_Click(object sender, RoutedEventArgs e)
        {
            SaveList(fileToLoad);
        }

        private void SaveList(string fileName = null)
        {
           
            //get file location, resource name
            // open write stream
            // format data for the stream
            // write to stream
            //write file to disk

                //resource.CopyTo(file);
                string userDataString = "";
                foreach (UserModel user in userDataList)
                    {
                    
                    //    //PropertyInfo propertyInfo = user.GetType().GetProperty(fieldNames[0]);

                    //    //List<string> propertyWrteOrder = new List<string>();
                    for (int index = 0; index < fieldNames.Length; index++)
                        {
                            if (index == fieldNames.Length - 1)
                            {
                                userDataString += $"{user.GetType().GetProperty(fieldNames[index]).GetValue(user)}";
                            }
                            else
                            {
                                userDataString += $"{user.GetType().GetProperty(fieldNames[index]).GetValue(user)},";
                            }
                        }
                    userDataString += '\n';
                }
            //File.WriteAllText(fileName, fieldNameString + '\n' + userDataString);
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
