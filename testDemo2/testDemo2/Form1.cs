using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SQLite;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace testDemo2
{
    public partial class Form1 : Form
    {

        const string dbNameUser = "user";
        public Form1()
        {
            InitializeComponent();

            UserDb user = new UserDb();
            user.createUserDB();
            DataTable table = user.queryUserByName("admin");
            dataGridView1.DataSource = table;
        }

      

    }
}
