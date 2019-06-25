using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Linq;

namespace TextFilesChallenge
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly ObservableCollection<UserModel> userDataList = null;
        private readonly string fileToLoad = "AdvancedDataSet.csv";
        FileParser fileParser = new FileParser();

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

            var filedata = fileParser.ParseDataSetFile(fileName);
            ObservableCollection<UserModel> userList = new ObservableCollection<UserModel>();


            foreach (var data in filedata.data)
            {
                UserModel newUserData = new UserModel();
                newUserData.AssignDataValues(data, filedata.fieldNames);
                userList.Add(newUserData);
            }
            return userList;
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
                    IsAlive = (bool)(IsAliveCheckBox.IsChecked) ? true : false
                });

        }

        private void SaveListButton_Click(object sender, RoutedEventArgs e)
        {
            SaveList(fileToLoad);
        }

        private void SaveList(string fileName = null)
        {

            fileParser.SaveDataFile(fileName, userDataList.ToList());
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
