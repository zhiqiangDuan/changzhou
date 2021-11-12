using System;
using System.Data;
using System.Text.RegularExpressions;
using System.Xml;
using System.IO;
using System.Collections;
using System.Data.SQLite;

namespace testDemo2
{
    /// <summary>
    /// SQLiteHelper is a utility class similar to "SQLHelper" in MS
    /// Data Access Application Block and follows similar pattern.
    /// </summary>
    public class SQLiteHelper
    {

        SQLiteConnection m_dbConnection;
        
        //创建一个空的数据库
        public bool createNewDatabase(string name)
        {
            bool ret = false;
            if (!isDBExist(name))
            {
                SQLiteConnection.CreateFile(name);
                ret = true;
            }
            else
                ret = false;
            connectToDatabase(name);
            return ret;
                
        }
        //创建一个连接到指定数据库
        public void connectToDatabase(string name)
        {
            m_dbConnection = new SQLiteConnection("Data Source=" + name + ";Version=3;");
            m_dbConnection.Open();
        }

        //在指定数据库中创建一个table
        public void createTable(string sql)
        {
            SQLiteCommand command = new SQLiteCommand(sql, m_dbConnection);
            command.ExecuteNonQuery();
        }

        //插入一些数据
        public void fillTable(string sql)
        {
            SQLiteCommand command = new SQLiteCommand(sql, m_dbConnection);
            command.ExecuteNonQuery();

        }

        //使用sql查询语句，并显示结果
        public DataTable queryTable(string sql)
        {
            SQLiteCommand command = new SQLiteCommand(sql, m_dbConnection);
            SQLiteDataReader reader = command.ExecuteReader();
            DataTable table = ConvertDataReaderToDataTable(reader);
            return table;
        }

        //判断db是否存在
        private bool isDBExist(string path)
        {
            bool ret = true;
            //判断数据库是否存在
            if (!File.Exists(path))
                ret = false;
            return ret;
        }

        private static DataTable ConvertDataReaderToDataTable(SQLiteDataReader dataReader)
        {
            ///定义DataTable  
            DataTable datatable = new DataTable();

            try
            {    ///动态添加表的数据列  
                for (int i = 0; i < dataReader.FieldCount; i++)
                {
                    DataColumn myDataColumn = new DataColumn();
                    myDataColumn.DataType = dataReader.GetFieldType(i);
                    myDataColumn.ColumnName = dataReader.GetName(i);
                    datatable.Columns.Add(myDataColumn);
                }

                ///添加表的数据  
                while (dataReader.Read())
                {
                    DataRow myDataRow = datatable.NewRow();
                    for (int i = 0; i < dataReader.FieldCount; i++)
                    {
                        myDataRow[i] = dataReader[i].ToString();
                    }
                    datatable.Rows.Add(myDataRow);
                    myDataRow = null;
                }
                ///关闭数据读取器  
                dataReader.Close();
                return datatable;
            }
            catch (Exception ex)
            {
                ///抛出类型转换错误  
                //SystemError.CreateErrorLog(ex.Message);  
                throw new Exception(ex.Message, ex);
            }
        }

    }
}