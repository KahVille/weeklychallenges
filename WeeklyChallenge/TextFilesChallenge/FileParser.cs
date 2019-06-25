using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextFilesChallenge
{
    class FileParser
    {

        private string[] fieldNames;

        // returns touple with field names, list of users and list of user values
        public (string[] fieldNames, List<string[]> data) ParseDataSetFile (string filename = null)
        {


            List<string> fileData = File.ReadAllLines(filename).ToList();

            fieldNames = fileData[0].Split(',');
            List<string[]> data = new List<string[]>();

            foreach (var item in fileData.Skip(1))
            {

                string[] dataValues = item.Split(',');
                data.Add(dataValues);

            }
            return (fieldNames: fieldNames, data: data);
        }

        public void SaveDataFile(string fileName, List<UserModel> userDataList)
        {

            //get file location
            // write to list
            // format data
            //write file to disk

            List<string> fileoutput = new List<string>();
            char[] unwantedChars = { ',' };

            string dataFieldNames = "";
            foreach (var dataField in fieldNames)
            {
                dataFieldNames += $"{dataField},";
            }

            string dataFields = dataFieldNames.TrimEnd(unwantedChars);

            fileoutput.Add(dataFields);

            foreach (UserModel user in userDataList)
            {

                string userDataString = "";
                for (int index = 0; index < fieldNames.Length; index++)
                {

                    if (user.GetType().GetProperty(fieldNames[index]).PropertyType == typeof(bool))
                    {
                        int numberFormat = (user.IsAlive == false) ? 0 : 1;
                        userDataString += $"{numberFormat},";

                    }
                    else
                    {
                        userDataString += $"{user.GetType().GetProperty(fieldNames[index]).GetValue(user)},";
                    }
                }
                string userData = userDataString.TrimEnd(unwantedChars);
                fileoutput.Add(userData);
            }
            File.WriteAllLines(fileName, fileoutput);
        }

    }
}
