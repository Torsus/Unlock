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
           // String Sql;
            if (Datacontainer.connectsource == "Data Source=Klingen-su-db,62468; Initial Catalog = Klingen;")
            {
                Datacontainer.SQLSearch = "SELECT ROW_NUMBER() OVER(ORDER BY[Index] ) AS RowNumber,[Index],Patient,[Analysis Number],AnswerDate,Comments FROM[Klingen].[dbo].[Analysis Answer] WHERE AnswerDate >= '" + theDate1 +"'AND Answerdate <= '" + theDate2 +"' AND Comments is not null";

            }
            else
            {
                Datacontainer.SQLSearch = "SELECT ROW_NUMBER() OVER(ORDER BY[Index] ) AS RowNumber,[Index],Patient,[Analysis Number],AnswerDate,Comments FROM[Klingen_test].[dbo].[Analysis Answer] WHERE AnswerDate >= '" + theDate1 + "'AND Answerdate <= '" + theDate2 + "' AND Comments is not null";

            }
            Datacontainer.command = new SqlCommand(Datacontainer.SQLSearch, Datacontainer.cnn);
            Datacontainer.command.CommandType = CommandType.Text;
            Datacontainer.reader = Datacontainer.command.ExecuteReader();
            int radnummer;
            radnummer = 4;
            ListViewItem item;
            this.listView1.Columns.Add("Key", 50, HorizontalAlignment.Left);
            this.listView1.Columns.Add("Co1 1", 50, HorizontalAlignment.Left);
            this.listView1.Columns.Add("Col 2", 50, HorizontalAlignment.Left);
            checkedListBox1.Items.Clear();
            int indexvarde;
            indexvarde = 0;
            while (Datacontainer.reader.Read())
            {

                Datacontainer.Index = (int)Datacontainer.reader.GetValue(1);
                Datacontainer.Indexarray[indexvarde] = (int)Datacontainer.reader.GetValue(1);
                indexvarde++;
                String varde1;
                String varde2;
                String varde3;
                String varde4;
                String varde5;
                String concat;
                varde1 = Datacontainer.reader.GetValue(1).ToString();
                varde2 = Datacontainer.reader.GetValue(2).ToString();
                varde3 = Datacontainer.reader.GetValue(3).ToString();
                varde4 = Datacontainer.reader.GetValue(4).ToString();
                varde5 = Datacontainer.reader.GetValue(5).ToString();
                //   listView1.Items.Add(dummy);



                var listViewItem = new ListViewItem(varde1);
                listView1.Items.Add(listViewItem);
               
                concat = varde1 + " " + varde2 + " " + varde3 + " " + varde4 + "" + varde5;
                checkedListBox1.Items.Add(concat);
                radnummer++;

            }




            Datacontainer.reader.Close();
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

        private void button4_Click(object sender, EventArgs e)
        {
            int antal;
            antal = checkedListBox1.Items.Count;
            //SqlDataReader reader;
            for (int i = 0; i < antal; i++)
            {
                if (checkedListBox1.GetItemCheckState(i) == CheckState.Checked)
                {
                    String SQL2;
                    if (Datacontainer.connectsource == "Data Source=Klingen-su-db,62468; Initial Catalog = Klingen;")
                    {
                          SQL2 = "update [Klingen].[dbo].[Analysis Answer] set Comments = null where [Index] = "+Datacontainer.Indexarray[i]+"";
                    }
                    else
                    {
                        SQL2 = "update [Klingen_test].[dbo].[Analysis Answer] set Comments = null where [Index] = "+Datacontainer.Indexarray[i]+"";

                    }
                    Datacontainer.command = new SqlCommand(SQL2, Datacontainer.cnn);
                    Datacontainer.command.CommandType = CommandType.Text;
                    Datacontainer.reader = Datacontainer.command.ExecuteReader();
                   
                }
                Datacontainer.reader.Close();
            }

            ///Kvittens
          //  Datacontainer.reader.Close();
            string message = "Upplåsning på valda svar utfört!";
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
            ///Läs in in den uppdaterade listan
            checkedListBox1.Items.Clear();
            Datacontainer.command = new SqlCommand(Datacontainer.SQLSearch, Datacontainer.cnn);
            Datacontainer.command.CommandType = CommandType.Text;
            Datacontainer.reader2 = Datacontainer.command.ExecuteReader();
            ///Inläst igen, lägg nu in det i tabellen igen
            /// 
            int indexvarde;
            indexvarde = 0;
            while (Datacontainer.reader2.Read())
            {

                Datacontainer.Index = (int)Datacontainer.reader2.GetValue(1);
                Datacontainer.Indexarray[indexvarde] = (int)Datacontainer.reader2.GetValue(1);
                indexvarde++;
                String varde1;
                String varde2;
                String varde3;
                String varde4;
                String varde5;
                String concat;
                varde1 = Datacontainer.reader2.GetValue(1).ToString();
                varde2 = Datacontainer.reader2.GetValue(2).ToString();
                varde3 = Datacontainer.reader2.GetValue(3).ToString();
                varde4 = Datacontainer.reader2.GetValue(4).ToString();
                varde5 = Datacontainer.reader2.GetValue(5).ToString();
                //   listView1.Items.Add(dummy);



                //   var listViewItem = new ListViewItem(varde1);
                //  listView1.Items.Add(listViewItem);

                concat = varde1 + " " + varde2 + " " + varde3 + " " + varde4 + " " + varde5;
                checkedListBox1.Items.Add(concat);
                // radnummer++;

            }
            Datacontainer.reader2.Close();
           
           
           

            //}

        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void label11_Click(object sender, EventArgs e)
        {

        }
    }
}
