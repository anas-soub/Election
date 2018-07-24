using ElectionDb;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Election
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                ElectionDb.ElectionDb db = new ElectionDb.ElectionDb();
                db.FilterElctorByName("انس");
                db.FilterByGender("2");
                db.FilterByElectorByGovernate("الكرك");
                db.SortByAge(false);
                var results = db.GetResults();
                dataGridView1.DataSource = results;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void dataGridView1_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            Person p = dataGridView1.Rows[e.RowIndex].DataBoundItem as Person;
            pictureBox1.Image = p.GetImage();

        }
    }
}
