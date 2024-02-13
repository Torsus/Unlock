using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Unlock
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            Datacontainer.connectsource = "Data Source=Klingen-su-db,62468; Initial Catalog = Klingen;";
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Datacontainer.anvandarnamn = textBox1.Text;
            Datacontainer.losen = textBox2.Text;
            Datacontainer.connectionString = @Datacontainer.connectsource + "User ID=" + textBox1.Text + ";Password=" + textBox2.Text + "";
            Datacontainer.cnn = new SqlConnection(Datacontainer.connectionString);
            Datacontainer.cnn.Open();
            string message = "Connection Open  !";
            string title = "";
            MessageBoxButtons buttons = MessageBoxButtons.OK;
            DialogResult result = MessageBox.Show(message, title, buttons);
            if (result == DialogResult.OK)
            {
                button2.Enabled = true;
            }
            else
            {
                // Do something
            }

        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            Datacontainer.connectsource = "Data Source=Klingen-su-db,62468; Initial Catalog = Klingen;";
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            Datacontainer.connectsource = "Data Source=Klingen-test-su-db,62468; Initial Catalog = Klingen_Test;";
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string theDate1 = dateTimePicker1.Value.ToString("yyyy-MM-dd");
            string theDate2 = dateTimePicker2.Value.ToString("yyyy-MM-dd");
            String Sql;
            if (Datacontainer.connectsource == "Data Source=Klingen-su-db,62468; Initial Catalog = Klingen;")
            {
                Sql = "SELECT ROW_NUMBER() OVER(ORDER BY[Index] Desc) AS RowNumber,[Index],Patient,[Analysis Number] FROM[Klingen].[dbo].[Analysis Answer] WHERE AnswerDate > theDate1";

            }
            else
            {
                Sql = "SELECT ROW_NUMBER() OVER(ORDER BY[Index] Desc) AS RowNumber,[Index],Patient,[Analysis Number] FROM[Klingen_test].[dbo].[Analysis Answer] WHERE AnswerDate > theDate1";

            }
            Datacontainer.command = new SqlCommand(Sql, Datacontainer.cnn);
            Datacontainer.command.CommandType = CommandType.Text;
            SqlDataReader reader = Datacontainer.command.ExecuteReader();
            int radnummer;
            radnummer = 4;
            while (reader.Read())
            {

                Datacontainer.Index = (int)reader.GetValue(1);
                //Datacontainer.personnummer = (String)reader.GetValue(2);
                ////Datacontainer.Familyname = (String)reader.GetValue(3);
                //if (reader.GetValue(3) != DBNull.Value)
                //{
                //    Datacontainer.Familyname = (String)reader.GetValue(3);
                //}
                //else
                //{
                //    Datacontainer.Familyname = "";
                //}
                //if (reader.GetValue(4) != DBNull.Value)
                //{
                //    Datacontainer.fornamn = (String)reader.GetValue(4);
                //}
                //else
                //{
                //    Datacontainer.fornamn = "";
                //}
                ////För nu över till excel!
              
                radnummer++;

            }

            reader.Close();
        }
    }
}
