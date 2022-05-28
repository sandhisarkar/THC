using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Windows.Forms;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.ComponentModel;
using WeifenLuo.WinFormsUI.Docking;
using System.Runtime.InteropServices;
using DockSample.Customization;
using System.IO;
using DockSample;
using NovaNet.Utils;
using NovaNet.wfe;
using System.Data;
using System.Data.Odbc;
using System.Collections;
using LItems;
//using AForge.Imaging;
//using AForge;
//using AForge.Imaging.Filters;
using TwainLib;
using Inlite.ClearImageNet;
//using System.Drawing.Bitmap;
//using System.Drawing.Graphics;
//using Graphics.DrawImage;
using iTextSharp;
using iTextSharp.text;
using iTextSharp.text.pdf;
using Microsoft;
using Microsoft.Office;
using Microsoft.Office.Interop.Excel;
using System.Linq;

namespace ImageHeaven
{
    public partial class frmUserCountReport : Form
    {
        OdbcConnection sqlCon = null;

        public string stDate;
        public string endDate;
        public string stage = string.Empty;
        public string userId = string.Empty;

        public Credentials crd = new Credentials();

        public frmUserCountReport()
        {
            InitializeComponent();
        }

        public frmUserCountReport(OdbcConnection prmCon)
        {
            InitializeComponent();
            sqlCon = prmCon;
        }

        private void populateUserType()
        {
            DataSet ds = new DataSet();
            System.Data.DataTable dt = new System.Data.DataTable();

            string sql = "select role_id, role_description from ac_role where role_id = 1 or role_id = 3 or role_id = 5 or role_id = 7 or role_id = 9 or role_id = 10";

            OdbcDataAdapter odap = new OdbcDataAdapter(sql, sqlCon);
            odap.Fill(dt);


            if (dt.Rows.Count > 0)
            {
                comboBox1.DataSource = dt;
                comboBox1.DisplayMember = "role_description";
                comboBox1.ValueMember = "role_id";
            }

        }

        private void populateAllUserType()
        {
            DataSet ds = new DataSet();
            System.Data.DataTable dt = new System.Data.DataTable();

            string sql = "select user_id, role_id from ac_user_role_map  ";

            OdbcDataAdapter odap = new OdbcDataAdapter(sql, sqlCon);
            odap.Fill(dt);


            if (dt.Rows.Count > 0)
            {
                deComboBox2.DataSource = dt;
                deComboBox2.DisplayMember = "user_id";
                deComboBox2.ValueMember = "role_id";
            }
            else
            {
                deComboBox2.DataSource = null;
            }
        }

        private void populateUserId()
        {
            DataSet ds = new DataSet();
            System.Data.DataTable dt = new System.Data.DataTable();

            string sql = "select user_id, role_id from ac_user_role_map a where role_id = '" + comboBox1.SelectedValue.ToString() + "'";

            OdbcDataAdapter odap = new OdbcDataAdapter(sql, sqlCon);
            odap.Fill(dt);


            if (dt.Rows.Count > 0)
            {
                deComboBox2.DataSource = dt;
                deComboBox2.DisplayMember = "user_id";
                deComboBox2.ValueMember = "role_id";
            }
            else
            {
                deComboBox2.DataSource = null;
            }
        }

        private void frmUserCountReport_Load(object sender, EventArgs e)
        {
            populateUserType();

            //populateAllUserType();

            stDate = DateTime.Now.ToString("yyyy-MM-dd");
            endDate = DateTime.Now.ToString("yyyy-MM-dd");

            dateTimePicker1.Text = stDate;
            dateTimePicker1.Format = DateTimePickerFormat.Custom;
            dateTimePicker1.CustomFormat = "yyyy-MM-dd";
            dateTimePicker1.Value = Convert.ToDateTime(stDate.ToString());

            dateTimePicker2.Text = endDate;
            dateTimePicker2.Format = DateTimePickerFormat.Custom;
            dateTimePicker2.CustomFormat = "yyyy-MM-dd";
            dateTimePicker2.Value = Convert.ToDateTime(endDate.ToString());

            grdStatus.DataSource = null;

            comboBox1.SelectedIndex = 0;
            comboBox1.Select();
        }

        private void comboBox1_Leave(object sender, EventArgs e)
        {
            populateUserId();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void FormatDataGridView()
        {
            //Format the Data Grid View
            //grdStatus.Columns[0].Visible = false;
            //grdStatus.Columns[1].Visible = false;
            //dtGrdVol.Columns[2].Visible = false;
            //Format Colors


            //Set Autosize on for all the columns
            for (int i = 0; i < grdStatus.Columns.Count; i++)
            {
                grdStatus.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            }


        }

        private void init()
        {
            System.Data.DataTable Dt = new System.Data.DataTable();
            Dt = _GetEntries(userId);

            System.Data.DataTable DtTemp = new System.Data.DataTable();
            DtTemp.Columns.Add("Entry User");
            DtTemp.Columns.Add("File Count");


            DtTemp.Rows.Add(userId, Dt.Rows.Count);
            grdStatus.DataSource = DtTemp;

            FormatDataGridView();

            this.grdStatus.Refresh();

        }
        public System.Data.DataTable _GetEntries(string userId)
        {
            System.Data.DataTable dt = new System.Data.DataTable();
            string sql = "select proj_code,bundle_key,filename from metadata_entry where " +
                         "((date_format(created_dttm, '%Y-%m-%d') >= '" + stDate + "' and date_format(created_dttm, '%Y-%m-%d') <= '" + endDate + "') or " +
                         "(date_format(modified_dttm, '%Y-%m-%d') >= '" + stDate + "' and date_format(modified_dttm, '%Y-%m-%d') <= '" + endDate + "')) " +
                         "and(created_by = '" + userId + "' or modified_by = '" + userId + "')";
            OdbcCommand cmd = new OdbcCommand(sql, sqlCon);
            OdbcDataAdapter odap = new OdbcDataAdapter(cmd);
            odap.Fill(dt);
            return dt;
        }

        private void initScan()
        {
            System.Data.DataTable Dt = new System.Data.DataTable();
            Dt = _GetEntriesScan(userId);

            System.Data.DataTable DtTemp = new System.Data.DataTable();
            DtTemp.Columns.Add("Scan User");
            DtTemp.Columns.Add("File Count");
            DtTemp.Columns.Add("Image Count");

            int imgCount = 0;
            for (int i = 0; i < Dt.Rows.Count; i++)
            {
                imgCount = imgCount + Convert.ToInt32(_GetImageCount(Dt.Rows[i][0].ToString(), Dt.Rows[i][1].ToString(), Dt.Rows[i][2].ToString()).Rows[0][0].ToString());
            }

            DtTemp.Rows.Add(userId, Dt.Rows.Count, imgCount);
            grdStatus.DataSource = DtTemp;

            FormatDataGridView();

            this.grdStatus.Refresh();
        }
        public System.Data.DataTable _GetEntriesScan(string userId)
        {
            System.Data.DataTable dt = new System.Data.DataTable();
            string sql = "select proj_key,batch_key,policy_number from transaction_log where " +
                         "date_format(scanned_dttm, '%Y-%m-%d') >= '" + stDate + "' and date_format(scanned_dttm, '%Y-%m-%d') <= '" + endDate + "' and scanned_user = '" + userId + "' ";
            OdbcCommand cmd = new OdbcCommand(sql, sqlCon);
            OdbcDataAdapter odap = new OdbcDataAdapter(cmd);
            odap.Fill(dt);
            return dt;
        }

        private void initQC()
        {
            System.Data.DataTable Dt = new System.Data.DataTable();
            Dt = _GetEntriesQC(userId);

            System.Data.DataTable DtTemp = new System.Data.DataTable();
            DtTemp.Columns.Add("QC User");
            DtTemp.Columns.Add("File Count");
            DtTemp.Columns.Add("Image Count");

            int imgCount = 0;
            for (int i = 0; i < Dt.Rows.Count; i++)
            {
                imgCount = imgCount + Convert.ToInt32(_GetImageCount(Dt.Rows[i][0].ToString(), Dt.Rows[i][1].ToString(), Dt.Rows[i][2].ToString()).Rows[0][0].ToString());
            }

            DtTemp.Rows.Add(userId, Dt.Rows.Count, imgCount);
            grdStatus.DataSource = DtTemp;


            FormatDataGridView();

            this.grdStatus.Refresh();

        }
        public System.Data.DataTable _GetEntriesQC(string userId)
        {
            System.Data.DataTable dt = new System.Data.DataTable();
            string sql = "select proj_key,batch_key,policy_number from transaction_log where " +
                         "date_format(qc_dttm, '%Y-%m-%d') >= '" + stDate + "' and date_format(qc_dttm, '%Y-%m-%d') <= '" + endDate + "' and qc_user = '" + userId + "' ";
            OdbcCommand cmd = new OdbcCommand(sql, sqlCon);
            OdbcDataAdapter odap = new OdbcDataAdapter(cmd);
            odap.Fill(dt);
            return dt;
        }

        private void initIndex()
        {
            System.Data.DataTable Dt = new System.Data.DataTable();
            Dt = _GetEntriesIndex(userId);

            System.Data.DataTable DtTemp = new System.Data.DataTable();
            DtTemp.Columns.Add("DOC Type Association User");
            DtTemp.Columns.Add("File Count");
            DtTemp.Columns.Add("Image Count");

            int imgCount = 0;
            for (int i = 0; i < Dt.Rows.Count; i++)
            {
                imgCount = imgCount + Convert.ToInt32(_GetImageCount(Dt.Rows[i][0].ToString(), Dt.Rows[i][1].ToString(), Dt.Rows[i][2].ToString()).Rows[0][0].ToString());
            }

            DtTemp.Rows.Add(userId, Dt.Rows.Count, imgCount);
            grdStatus.DataSource = DtTemp;


            FormatDataGridView();

            this.grdStatus.Refresh();

        }
        public System.Data.DataTable _GetEntriesIndex(string userId)
        {
            System.Data.DataTable dt = new System.Data.DataTable();
            string sql = "select proj_key,batch_key,policy_number from transaction_log where " +
                         "date_format(index_dttm, '%Y-%m-%d') >= '" + stDate + "' and date_format(index_dttm, '%Y-%m-%d') <= '" + endDate + "' and index_user = '" + userId + "' ";
            OdbcCommand cmd = new OdbcCommand(sql, sqlCon);
            OdbcDataAdapter odap = new OdbcDataAdapter(cmd);
            odap.Fill(dt);
            return dt;
        }

        private void initFqc()
        {
            System.Data.DataTable Dt = new System.Data.DataTable();
            Dt = _GetEntriesFqc(userId);

            System.Data.DataTable DtTemp = new System.Data.DataTable();
            DtTemp.Columns.Add("FQC User");
            DtTemp.Columns.Add("File Count");
            DtTemp.Columns.Add("Image Count");

            int imgCount = 0;
            for (int i = 0; i < Dt.Rows.Count; i++)
            {
                imgCount = imgCount + Convert.ToInt32(_GetImageCount(Dt.Rows[i][0].ToString(), Dt.Rows[i][1].ToString(), Dt.Rows[i][2].ToString()).Rows[0][0].ToString());
            }

            DtTemp.Rows.Add(userId, Dt.Rows.Count, imgCount);
            grdStatus.DataSource = DtTemp;


            FormatDataGridView();

            this.grdStatus.Refresh();

        }
        public System.Data.DataTable _GetEntriesFqc(string userId)
        {
            System.Data.DataTable dt = new System.Data.DataTable();
            string sql = "select proj_key,batch_key,policy_number from transaction_log where " +
                         "date_format(fqc_dttm, '%Y-%m-%d') >= '" + stDate + "' and date_format(fqc_dttm, '%Y-%m-%d') <= '" + endDate + "' and fqc_user = '" + userId + "' ";
            OdbcCommand cmd = new OdbcCommand(sql, sqlCon);
            OdbcDataAdapter odap = new OdbcDataAdapter(cmd);
            odap.Fill(dt);
            return dt;
        }

        public System.Data.DataTable _GetImageCount(string proj, string batch, string file)
        {
            System.Data.DataTable dt = new System.Data.DataTable();
            string sql = "select count(*) from image_master where proj_key = '" + proj + "' and batch_key = '" + batch + "' and policy_number = '" + file + "' and status <> 29";
            OdbcCommand cmd = new OdbcCommand(sql, sqlCon);
            OdbcDataAdapter odap = new OdbcDataAdapter(cmd);
            odap.Fill(dt);
            return dt;
        }

        private void initAudit()
        {
            System.Data.DataTable Dt = new System.Data.DataTable();
            Dt = _GetEntriesAudit(userId);

            System.Data.DataTable DtTemp = new System.Data.DataTable();
            DtTemp.Columns.Add("Audit User");
            DtTemp.Columns.Add("File Count");
            DtTemp.Columns.Add("Image Count");

            int imgCount = 0;
            for (int i = 0; i < Dt.Rows.Count; i++)
            {
                imgCount = imgCount + Convert.ToInt32(_GetImageCount(Dt.Rows[i][0].ToString(), Dt.Rows[i][1].ToString(), Dt.Rows[i][2].ToString()).Rows[0][0].ToString());
            }

            DtTemp.Rows.Add(userId, Dt.Rows.Count, imgCount);

            grdStatus.DataSource = DtTemp;


            FormatDataGridView();

            this.grdStatus.Refresh();

        }
        public System.Data.DataTable _GetEntriesAudit(string userId)
        {
            System.Data.DataTable dt = new System.Data.DataTable();
            string sql = "select proj_key,batch_key,policy_number from lic_qa_log where " +
                         "((date_format(created_dttm, '%Y-%m-%d') >= '" + stDate + "' and date_format(created_dttm, '%Y-%m-%d') <= '" + endDate + "') or " +
                         "(date_format(modified_dttm, '%Y-%m-%d') >= '" + stDate + "' and date_format(modified_dttm, '%Y-%m-%d') <= '" + endDate + "') ) " +
                         "and(created_by = '" + userId + "' or modified_by = '" + userId + "') and(qa_status = 0 or qa_status = 1 or qa_status = 2)";
            OdbcCommand cmd = new OdbcCommand(sql, sqlCon);
            OdbcDataAdapter odap = new OdbcDataAdapter(cmd);
            odap.Fill(dt);
            return dt;
        }

        private void deButton1_Click(object sender, EventArgs e)
        {
            grdStatus.DataSource = null;

            stage = comboBox1.Text;
            userId = deComboBox2.Text;

            stDate = dateTimePicker1.Text;
            endDate = dateTimePicker2.Text;

            if (stage == "Metadata Entry")
            {
                if (userId != null || userId != "")
                {
                    init();
                }
            }
            else if (stage == "Scan")
            {
                if (userId != null || userId != "")
                {
                    initScan();
                }
            }
            else if (stage == "QC")
            {
                if (userId != null || userId != "")
                {
                    initQC();
                }
            }
            else if (stage == "DOC Type Association")
            {
                if (userId != null || userId != "")
                {
                    initIndex();
                }
            }
            else if (stage == "Fqc")
            {
                if (userId != null || userId != "")
                {
                    initFqc();
                }
            }
            else if (stage == "Audit")
            {
                if (userId != null || userId != "")
                {
                    initAudit();
                }
            }
        }

        private void grdStatus_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                if (grdStatus.Rows.Count > 0)
                {
                    cmsDeeds.Show(Cursor.Position);
                }
            }
        }

        private void updateDeedToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (grdStatus.Rows.Count > 0)
            {
                Microsoft.Office.Interop.Excel._Application app = new Microsoft.Office.Interop.Excel.Application();
                Microsoft.Office.Interop.Excel._Workbook workbook = app.Workbooks.Add(Type.Missing);

                Microsoft.Office.Interop.Excel._Worksheet worksheet = null;

                app.Visible = false;

                worksheet = (Microsoft.Office.Interop.Excel._Worksheet)workbook.Sheets["Sheet1"];


                worksheet = (Microsoft.Office.Interop.Excel._Worksheet)workbook.ActiveSheet;

                worksheet.Name = "User Wise Report";

                worksheet.Cells[1, 2] = "User Wise Report";
                Range range44 = worksheet.get_Range("B1");
                range44.Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.YellowGreen);

                worksheet.Rows.AutoFit();
                worksheet.Columns.AutoFit();


                worksheet.Cells[3, 1] = "User Report for : " + stage;
                Range range43 = worksheet.get_Range("A3");
                range43.Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Yellow);
                worksheet.Rows.AutoFit();
                worksheet.Columns.AutoFit();

                worksheet.Cells[4, 1] = "Date From : " + stDate + " - " + endDate;
                Range range33 = worksheet.get_Range("A4");
                range33.Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Yellow);
                worksheet.Rows.AutoFit();
                worksheet.Columns.AutoFit();

                worksheet.Cells[5, 1] = "User Id : " + userId;
                Range range34 = worksheet.get_Range("A5");
                range34.Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Yellow);
                worksheet.Rows.AutoFit();
                worksheet.Columns.AutoFit();

                Range range = worksheet.get_Range("A3", "A5");
                range.Borders.Color = ColorTranslator.ToOle(Color.Black);

                if (stage == "Metadata Entry")
                {
                    Range range1 = worksheet.get_Range("A7", "B7");
                    range1.Borders.Color = ColorTranslator.ToOle(Color.Black);

                    for (int i = 1; i < grdStatus.Columns.Count + 1; i++)
                    {


                        Range range2 = worksheet.get_Range("A7", "B7");
                        range2.Borders.Color = ColorTranslator.ToOle(Color.Black);
                        range2.EntireRow.AutoFit();
                        range2.EntireColumn.AutoFit();
                        worksheet.Cells[7, i] = grdStatus.Columns[i - 1].HeaderText;
                    }
                }
                else
                {
                    Range range1 = worksheet.get_Range("A7", "C7");
                    range1.Borders.Color = ColorTranslator.ToOle(Color.Black);

                    for (int i = 1; i < grdStatus.Columns.Count + 1; i++)
                    {


                        Range range2 = worksheet.get_Range("A7", "C7");
                        range2.Borders.Color = ColorTranslator.ToOle(Color.Black);
                        range2.EntireRow.AutoFit();
                        range2.EntireColumn.AutoFit();
                        worksheet.Cells[7, i] = grdStatus.Columns[i - 1].HeaderText;
                    }
                }

                for (int i = 0; i < grdStatus.Rows.Count; i++)
                {
                    for (int j = 0; j < grdStatus.Columns.Count; j++)
                    {
                        Range range3 = worksheet.Cells;
                        //range3.Borders.Color = ColorTranslator.ToOle(Color.Black);
                        range3.EntireRow.AutoFit();
                        range3.EntireColumn.AutoFit();
                        worksheet.Cells[i + 8, j + 1] = grdStatus.Rows[i].Cells[j].Value.ToString();
                        worksheet.Cells[i + 8, j + 1].Borders.Color = ColorTranslator.ToOle(Color.Black);

                    }

                }

                string namexls = "User_Wise_Count_Report_" + userId + ".xls";
                string path = Path.GetDirectoryName(System.Windows.Forms.Application.ExecutablePath);
                sfdUAT.Filter = "Xls files (*.xls)|*.xls";
                sfdUAT.FilterIndex = 2;
                sfdUAT.RestoreDirectory = true;
                sfdUAT.FileName = namexls;
                sfdUAT.ShowDialog();

                workbook.SaveAs(sfdUAT.FileName, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Microsoft.Office.Interop.Excel.XlSaveAsAccessMode.xlExclusive, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);

                app.Quit();
            }
        }
    }
}
