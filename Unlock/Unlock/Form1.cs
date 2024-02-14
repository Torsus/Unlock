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
                Sql = "SELECT ROW_NUMBER() OVER(ORDER BY[Index] Desc) AS RowNumber,[Index],Patient,[Analysis Number] FROM[Klingen].[dbo].[Analysis Answer] WHERE AnswerDate > " + theDate1 +" AND Answer is Not null";

            }
            else
            {
                Sql = "SELECT ROW_NUMBER() OVER(ORDER BY[Index] Desc) AS RowNumber,[Index],Patient,[Analysis Number] FROM[Klingen_test].[dbo].[Analysis Answer] WHERE AnswerDate > " + theDate1 + " AND Answer is Not null";

            }
            Datacontainer.command = new SqlCommand(Sql, Datacontainer.cnn);
            Datacontainer.command.CommandType = CommandType.Text;
            SqlDataReader reader = Datacontainer.command.ExecuteReader();
            int radnummer;
            radnummer = 4;
            ListViewItem item;
            this.listView1.Columns.Add("Key", 50, HorizontalAlignment.Left);
            this.listView1.Columns.Add("Co1 1", 50, HorizontalAlignment.Left);
            this.listView1.Columns.Add("Col 2", 50, HorizontalAlignment.Left);

            while (reader.Read())
            {

                Datacontainer.Index = (int)reader.GetValue(1);
                String dummy;
                dummy = reader.GetValue(1).ToString();
             //   listView1.Items.Add(dummy);



                var listViewItem = new ListViewItem(dummy);
                listView1.Items.Add(listViewItem);
                //  listView1.Items.Insert(radnummer-4, listViewItem);


                //ListViewItem row1 = new ListViewItem();
                //row1.Text = "Row 1";
                //row1.SubItems.Add(dummy);
                //row1.SubItems.Add("dummy");
                //listView1.Items.Add(row1);


                //   listView1.Items.Insert(radnummer-4,dummy);
                // listView1.Items.Add(new ListViewItem(dummy));
                // lvwAddItem(listView1, dummy);
                //listView1.Items.Insert(0, "\n");
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
                checkedListBox1.Items.Add(dummy);
                radnummer++;

            }




            reader.Close();
        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void dateTimePicker2_ValueChanged(object sender, EventArgs e)
        {

        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
