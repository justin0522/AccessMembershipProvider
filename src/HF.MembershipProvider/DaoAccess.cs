using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HF.MembershipProvider
{
    public class DaoAccess
    {
        private string conStr;
        public DaoAccess()
        {
            conStr = @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=|DataDirectory|Database.mdb";
        }
        private int ExecuteNonQuery(string query)
        {
            var con = new OleDbConnection(conStr);
            int rst = -1;
            OleDbCommand cmd = new OleDbCommand(query, con);
            try
            {
                con.Open();
                rst = cmd.ExecuteNonQuery();
            }
            finally
            {
                con.Close();
            }
            return rst;
        }
        public bool CreateUser(string username, string password, string email, string passwordQuestion, string passwordAnswer)
        {
            string query = string.Format("insert into UserInfo(Name,[Password],Email,PasswordQuestion,PasswordAnswer) values('{0}','{1}','{2}','{3}','{4}')",
                                        username, password, email, passwordAnswer, passwordQuestion);

            return ExecuteNonQuery(query) > 0;
        }
        public bool ChangePassword(string username, string oldPassword, string newPassword)
        {
            string query = string.Format("update UserInfo set [Password]='{0}' where Name='{1}' and [Password]='{2}'",
                                     newPassword, username, oldPassword);
            return ExecuteNonQuery(query) > 0;
        }
        public UserInfo GetUserByName(string username)
        {
            string query = string.Format("select * from UserInfo where Name='{0}'", username);
            OleDbDataAdapter ada = new OleDbDataAdapter(query, new OleDbConnection(conStr));
            DataTable dt = new DataTable();
            ada.Fill(dt);
            var userlist = dt.ConvertToList<UserInfo>();
            if (userlist.Count > 0)
                return userlist[0];
            return null;
        }
    }
}
