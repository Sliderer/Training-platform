using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;

namespace Training_platfomt
{
    class TextBoxesCheker
    {
        public static bool CheckUser(User user)
        {
            if (!ThowError(user.login, 3, 20, "Длина логина должна быть больше 3 и меньше 20!")) return false;
            if (!ThowError(user.name, 3, 20, "Длина имени должна быть больше 3 и меньше 20!")) return false;
            if (!ThowError(user.surname, 0, 50, "Заполните фамилию!")) return false;
            if (!ThowError(user.password, 8, 50, "Длина пароля должна быть больше 8 и меньше 50")) return false;

            if (!EmailSender.SendWelcomEmail(user.email))
            {
                MessageBox.Show("Такой почты не существует!");
                return false;
            }

            return true;
        }

        private static bool ThowError(string stringToCheck, int minLength, int maxLength, string errorText)
        {
            if (stringToCheck.Length >= minLength && stringToCheck.Length <= maxLength)
            {
                return true;
            }
            else
            {
                MessageBox.Show(errorText, "Warning");
                return false;
            }
        }
    }
}
