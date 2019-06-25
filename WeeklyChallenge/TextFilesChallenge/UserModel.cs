using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextFilesChallenge
{
    public class UserModel
    {
        
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }
        public bool IsAlive { get; set; }

        public string DisplayText
        {
            get
            {
                string aliveStatus = "is dead";

                if (IsAlive == true)
                {
                    aliveStatus = "is alive";
                }

                return $"{ FirstName} { LastName } is { Age } and { aliveStatus }";
            }
        }

        public void AssignDataValues(string[] dataValues, string[] fieldNames)
        {

            for (int jindex = 0; jindex < dataValues.Length; jindex++)
            {
                string valueToCheck = dataValues[jindex];
                string fieldName = fieldNames[jindex];
                ConvertValue(fieldName, valueToCheck);
            }
        }

        //convert and set value form
        private void ConvertValue(string fieldName, string fieldValue)
        {
            System.Reflection.PropertyInfo propertyInfo = this.GetType().GetProperty(fieldName);

            var converter = System.ComponentModel.TypeDescriptor.GetConverter(propertyInfo.PropertyType);

            //from string to boolean special case
            if (propertyInfo.PropertyType == typeof(bool))
            {
                bool success = int.TryParse(fieldValue, out int number);
                if (success)
                {
                    bool booleanValue = (number == 1) ? true : false;
                    propertyInfo.SetValue(this, booleanValue, null);
                }
            }
            else
            {
                var result = converter.ConvertFrom(fieldValue);
                propertyInfo.SetValue(this, result, null);
            }
        }
    }
}
