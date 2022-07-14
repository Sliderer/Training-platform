using System;
using System.Collections.Generic;
using System.Text;

namespace Training_platfomt
{
    class User
    {
        internal int id { get; set; }
        internal string name { get; set; }
        internal string surname { get; set; }
        internal string login { get; set; }
        internal string email { get; set; }
        internal string password { get; set; }

        public User(int id, string name, string surname, string login, string email, string password)
        {
            this.id = id;
            this.name = name;
            this.surname = surname;
            this.login = login;
            this.email = email;
            this.password = password;
        }

        public User(string name, string surname, string login, string email, string password)
        {
            this.name = name;
            this.surname = surname;
            this.login = login;
            this.email = email;
            this.password = password;
        }
    }
}
