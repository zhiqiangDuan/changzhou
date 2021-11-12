using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace testDemo2
{
    class UserDb
    {
        const string dbname = "user.db";
        const string sqlCreateTable = "CREATE TABLE user (name VARCHAR(20),passwd CHAR(20),admin BOOLEAN);";
        const string userAdminName = "admin";
        const string userAdminPasswd = "123456";

        SQLiteHelper dbUser;
        public UserDb()
        {
            dbUser = new SQLiteHelper();
        }

        public void createUserDB()
        {
            if (dbUser.createNewDatabase(dbname))//如果新建DB则创建表。然后添加默认的admin管理员。
            {
                dbUser.createTable(sqlCreateTable);
                insertUser(userAdminName, userAdminPasswd,true);
            }
        }

        public void insertUser(string name ,string passwd,bool isAdmin)
        {
            string sqlInsert = "insert into user (name, passwd) values ('"+ name + "', '"+passwd+ "', '" + isAdmin.ToString() + "')";
            dbUser.fillTable(sqlInsert);
        }

        //查找指定人名是否存在
        public DataTable queryUserByName(string name)
        {
            string sql = "select * from user where name='"+ name + "'";
            DataTable table = dbUser.queryTable(sql);
            return table;
        }
    }
}
