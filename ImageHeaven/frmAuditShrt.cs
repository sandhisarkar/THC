using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ImageHeaven
{
    public partial class frmAuditShrt : Form
    {
        string[] list;
        public frmAuditShrt()
        {
            InitializeComponent();
        }
        public frmAuditShrt(string[] listCons, string textName)
        {
            InitializeComponent();
            list = listCons;
            if (textName == "Petitioner")
            {
                this.Text = "Total Petitioner Names " + list.Length;
            }
            else if (textName == "Respondant")
            {
                this.Text = "Total Respondant Names " + list.Length;
            }
            if (textName == "Petitioner Counsel")
            {
                this.Text = "Total Petitioner Counsel Names " + list.Length;
            }
            else if (textName == "Respondant Counsel")
            {
                this.Text = "Total Respondant Counsel Names " + list.Length;
            }
        }
        private void frmAuditShrt_Load(object sender, EventArgs e)
        {
            lstDeeds.Items.Clear();
            for (int i = 0; i < list.Length; i++)
            {

                string name = list[i].Trim().ToString();

                string[] row = { name };
                var listItem = new ListViewItem(row);

                lstDeeds.Items.Add(listItem);
            }
        }
    }
}
