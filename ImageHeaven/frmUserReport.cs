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
    public partial class frmUserReport : Form
    {
        OdbcConnection sqlCon = null;

        public string stDate;
        public string endDate;
        public string stage = string.Empty;
        public string userId = string.Empty;
        int i;
        int j;

        public Credentials crd = new Credentials();

        public frmUserReport()
        {
            InitializeComponent();
        }

        public frmUserReport(OdbcConnection prmCon)
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

        private void frmUserReport_Load(object sender, EventArgs e)
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

        private void deComboBox1_Leave(object sender, EventArgs e)
        {

            populateUserId();
        }

        private void deComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            populateUserType();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            //populateUserType();
        }

        private void comboBox1_Leave(object sender, EventArgs e)
        {
            populateUserId();
        }

        private void deComboBox2_SelectedIndexChanged(object sender, EventArgs e)
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

            grdStatus.DataSource = Dt;

            FormatDataGridView();

            this.grdStatus.Refresh();

        }
        public System.Data.DataTable _GetEntries(string userId)
        {
            System.Data.DataTable dt = new System.Data.DataTable();
            string sql = "select distinct date_format(a.created_dttm,'%Y-%m-%d') as 'Entry Date',a.created_by as 'Entry User',b.bundle_name as 'Bundle Name',a.filename as 'Filename' from metadata_entry a,bundle_master b " +
                         "where date_format(a.created_dttm,'%Y-%m-%d') >= '" + stDate + "' and date_format(a.created_dttm,'%Y-%m-%d') <= '" + endDate + "' and " +
                         " a.proj_code = b.proj_code and a.bundle_key = b.bundle_key and a.created_by = '" + userId + "'";
            OdbcCommand cmd = new OdbcCommand(sql, sqlCon);
            OdbcDataAdapter odap = new OdbcDataAdapter(cmd);
            odap.Fill(dt);
            return dt;
        }

        private void initScan()
        {
            System.Data.DataTable Dt = new System.Data.DataTable();
            Dt = _GetEntriesScan(userId);

            grdStatus.DataSource = Dt;

            FormatDataGridView();

            this.grdStatus.Refresh();
        }
        public System.Data.DataTable _GetEntriesScan(string userId)
        {
            System.Data.DataTable dt = new System.Data.DataTable();
            string sql = "select distinct date_format(a.scanned_dttm,'%Y-%m-%d') as 'Scanned Date',a.scanned_user as 'Scanned User',b.bundle_name as 'Bundle Name',a.policy_number as 'Filename' from transaction_log a, bundle_master b " +
                         " where date_format(a.scanned_dttm,'%Y-%m-%d') >= '" + stDate + "' and date_format(a.scanned_dttm,'%Y-%m-%d') <= '" + endDate + "' and " +
                         " a.proj_key = b.proj_code and a.batch_key = b.bundle_key and a.scanned_user = '" + userId + "'";
            OdbcCommand cmd = new OdbcCommand(sql, sqlCon);
            OdbcDataAdapter odap = new OdbcDataAdapter(cmd);
            odap.Fill(dt);
            return dt;
        }

        private void initQC()
        {
            System.Data.DataTable Dt = new System.Data.DataTable();
            Dt = _GetEntriesQC(userId);

            grdStatus.DataSource = Dt;


            FormatDataGridView();

            this.grdStatus.Refresh();

        }
        public System.Data.DataTable _GetEntriesQC(string userId)
        {
            System.Data.DataTable dt = new System.Data.DataTable();
            string sql = "select distinct date_format(a.qc_dttm,'%Y-%m-%d') as 'QC Date',a.qc_user as 'QC User',b.bundle_name as 'Bundle Name',a.policy_number as 'Filename' from transaction_log a, bundle_master b " +
                         " where date_format(a.qc_dttm,'%Y-%m-%d') >= '" + stDate + "' and date_format(a.qc_dttm,'%Y-%m-%d') <= '" + endDate + "' and " +
                          " a.proj_key = b.proj_code and a.batch_key = b.bundle_key and a.qc_user = '" + userId + "'";
            OdbcCommand cmd = new OdbcCommand(sql, sqlCon);
            OdbcDataAdapter odap = new OdbcDataAdapter(cmd);
            odap.Fill(dt);
            return dt;
        }

        private void initIndex()
        {
            System.Data.DataTable Dt = new System.Data.DataTable();
            Dt = _GetEntriesIndex(userId);


            grdStatus.DataSource = Dt;


            FormatDataGridView();

            this.grdStatus.Refresh();

        }
        public System.Data.DataTable _GetEntriesIndex(string userId)
        {
            System.Data.DataTable dt = new System.Data.DataTable();
            string sql = "select distinct date_format(a.index_dttm,'%Y-%m-%d') as 'DOC Type Association Date',a.index_user as 'DOC Type Association User',b.bundle_name as 'Bundle Name',a.policy_number as 'Filename' from transaction_log a, bundle_master b " +
                         " where date_format(a.index_dttm,'%Y-%m-%d') >= '" + stDate + "' and date_format(a.index_dttm,'%Y-%m-%d') <= '" + endDate + "' and " +
                         " a.proj_key = b.proj_code and a.batch_key = b.bundle_key and a.index_user = '" + userId + "'";
            OdbcCommand cmd = new OdbcCommand(sql, sqlCon);
            OdbcDataAdapter odap = new OdbcDataAdapter(cmd);
            odap.Fill(dt);
            return dt;
        }

        private void initFqc()
        {
            System.Data.DataTable Dt = new System.Data.DataTable();
            Dt = _GetEntriesFqc(userId);


            grdStatus.DataSource = Dt;


            FormatDataGridView();

            this.grdStatus.Refresh();

        }
        public System.Data.DataTable _GetEntriesFqc(string userId)
        {
            System.Data.DataTable dt = new System.Data.DataTable();
            string sql = "select distinct date_format(a.fqc_dttm,'%Y-%m-%d') as 'Fqc Date',a.fqc_user as 'Fqc User',b.bundle_name as 'Bundle Name',a.policy_number  as 'Filename' from transaction_log a, bundle_master b " +
                         " where date_format(a.fqc_dttm,'%Y-%m-%d') >= '" + stDate + "' and date_format(a.fqc_dttm,'%Y-%m-%d') <= '" + endDate + "' and " +
                         " a.proj_key = b.proj_code and a.batch_key = b.bundle_key and a.fqc_user = '" + userId + "'";
            OdbcCommand cmd = new OdbcCommand(sql, sqlCon);
            OdbcDataAdapter odap = new OdbcDataAdapter(cmd);
            odap.Fill(dt);
            return dt;
        }

        private void initAudit()
        {
            //DataSet ds = new DataSet();
            System.Data.DataTable Dt = new System.Data.DataTable();
            Dt = _GetEntriesAudit(userId);

            // Dt.Columns.Add("Image Count");
            //Dt.Columns.Add("Image Count");
            //ds.Tables.Add(Dt);
            //int imagecount = 0;
            // for(i= 0; i< Dt.Rows.Count; i++)
            //{
            //    //Dt.Rows[i][6] = _GetImageCount(Dt.Rows[i][0].ToString(), Dt.Rows[i][1].ToString(), Dt.Rows[i][5].ToString());
            //   imagecount = Convert.ToInt32(_GetAuditImage(Dt.Rows[i][3].ToString()));
            //    Dt.Rows[i][5] = imagecount;
            //}
            //grdStatus.DataSource = ds.Tables[0];

            System.Data.DataTable DtTemp = new System.Data.DataTable();
            DtTemp.Columns.Add("Audit Date");
            // DtTemp.Columns.Add("Audit User");
            DtTemp.Columns.Add("Bundle Name");
            DtTemp.Columns.Add("FileName");
            DtTemp.Columns.Add("Image Count");

            int imgCount = 0;
            for (int i = 0; i < Dt.Rows.Count; i++)
            {
                // Dt.Rows[i][5] = _GetImageCount(Dt.Rows[i][0].ToString(), Dt.Rows[i][1].ToString(),Dt.Rows[i][4].ToString());
                // Dt.Rows[i][5] = _GetImageCount(Dt.Rows[i][0].ToString(), Dt.Rows[i][1].ToString(), Dt.Rows[i][5].ToString());
                imgCount = Convert.ToInt32(_GetImageCount(Dt.Rows[i][0].ToString(), Dt.Rows[i][1].ToString(), Dt.Rows[i][5].ToString()).Rows[0][0].ToString());

                DtTemp.Rows.Add(Dt.Rows[i][2].ToString(), Dt.Rows[i][4].ToString(), Dt.Rows[i][5].ToString(), imgCount);
            }


            // DtTemp.Rows.Add( imgCount);
            // DtTemp.Rows.Add(Dt.Rows[i][2].ToString(), Dt.Rows[i][4].ToString(), Dt.Rows[i][5].ToString(), imgCount);


            grdStatus.DataSource = DtTemp;


            FormatDataGridView();

            this.grdStatus.Refresh();

        }
        public System.Data.DataTable _GetEntriesAudit(string userId)
        {
            System.Data.DataTable dt = new System.Data.DataTable();
            string sql = "select distinct b.proj_code,b.bundle_key, date_format(a.created_dttm,'%Y-%m-%d') as 'Audit Date',a.created_by as 'Audit User',b.bundle_name as 'Bundle Name',a.policy_number  as 'Filename' from lic_qa_log a, bundle_master b " +
                         " where (date_format(a.created_dttm,'%Y-%m-%d') >= '" + stDate + "' and date_format(a.created_dttm,'%Y-%m-%d') <= '" + endDate + "') and (a.qa_status = 0 or a.qa_status = 1 or a.qa_status = 2) and " +
                         " a.proj_key = b.proj_code and a.batch_key = b.bundle_key and a.created_by = '" + userId + "'";
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
        public System.Data.DataTable _GetAuditImage(string filename)
        {
            System.Data.DataTable dt = new System.Data.DataTable();
            string sql = "SELECT COUNT(*) FROM image_master WHERE policy_number ='" + filename + "'";
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

                if (comboBox1.Text.Trim() == "Audit")

                {
                    worksheet.Name = "User Wise Report";

                    worksheet.Cells[1, 3] = "User Wise Report";
                    Range range44 = worksheet.get_Range("C1");
                    range44.Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.YellowGreen);

                    worksheet.Rows.AutoFit();
                    worksheet.Columns.AutoFit();

                    //  worksheet.Cells[2, 2] = "Customer Acceptance Certificate";
                    // Range range4 = worksheet.get_Range("B2");
                    // range4.Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.White);
                    // range45.Style.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;

                    //worksheet.Rows.AutoFit();
                    //worksheet.Columns.AutoFit();

                    // worksheet.Cells[3, 2] = "Digitization of Court Records of High court of Tripura";
                    // Range range5 = worksheet.get_Range("B3");
                    //range5.Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.White);

                    //worksheet.Rows.AutoFit();
                    //worksheet.Columns.AutoFit();

                    worksheet.Cells[3, 2] = "User Report for : " + stage;
                    Range range43 = worksheet.get_Range("B3");
                    range43.Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Yellow);
                    worksheet.Rows.AutoFit();
                    worksheet.Columns.AutoFit();

                    worksheet.Cells[4, 2] = "Period : " + stDate + " - " + endDate;
                    Range range6 = worksheet.get_Range("B4");
                    range6.Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Yellow);
                    worksheet.Rows.AutoFit();
                    worksheet.Columns.AutoFit();

                    worksheet.Cells[5, 2] = "User Id : " + userId;
                    Range range7 = worksheet.get_Range("B5");
                    range7.Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Yellow);
                    worksheet.Rows.AutoFit();
                    worksheet.Columns.AutoFit();

                    Range range = worksheet.get_Range("B3", "B5");
                    range.Borders.Color = ColorTranslator.ToOle(Color.Black);


                    Range range1 = worksheet.get_Range("B7", "E7");
                    range1.Borders.Color = ColorTranslator.ToOle(Color.Black);
                    //range1.Borders.Color = ColorTranslator.ToOle(Color.Black);


                    for (int i = 1; i < grdStatus.Columns.Count + 1; i++)
                    {


                        Range range2 = worksheet.get_Range("B7", "E7");
                        range2.Borders.Color = ColorTranslator.ToOle(Color.Black);
                        range2.EntireRow.AutoFit();
                        range2.EntireColumn.AutoFit();
                        worksheet.Cells[7, i + 1] = grdStatus.Columns[i - 1].HeaderText;
                        worksheet.Cells[7, i + 1].EntireRow.Font.Bold = true;
                    }
                    worksheet.Cells[9 + grdStatus.Rows.Count, 3] = "Total";
                    worksheet.Cells[9 + grdStatus.Rows.Count, 3].Font.Bold = true;

                    int imgcount = 0;
                    int filecount = 0;

                    for (int i = 0; i < grdStatus.Rows.Count; i++)
                    {
                        for (int j = 0; j < grdStatus.Columns.Count; j++)
                        {
                            Range range3 = worksheet.Cells;
                            //range3.Borders.Color = ColorTranslator.ToOle(Color.Black);
                            range3.EntireRow.AutoFit();
                            range3.EntireColumn.AutoFit();
                            worksheet.Cells[i + 8, j + 2] = grdStatus.Rows[i].Cells[j].Value.ToString();
                            worksheet.Cells[i + 8, j + 2].Borders.Color = ColorTranslator.ToOle(Color.Black);

                        }
                        filecount = grdStatus.Rows.Count;
                        imgcount = imgcount + Convert.ToInt32(grdStatus.Rows[i].Cells[3].Value.ToString());
                        // filecount = filecount + Convert.ToInt32(grdStatus.Rows[i].Cells[2].Value.ToString());


                    }
                    worksheet.Cells[9 + grdStatus.Rows.Count, 4] = filecount.ToString();
                    worksheet.Cells[9 + grdStatus.Rows.Count, 5] = imgcount.ToString();
                    worksheet.Cells[9 + grdStatus.Rows.Count, 3].Borders.Color = ColorTranslator.ToOle(Color.Black);
                    worksheet.Cells[9 + grdStatus.Rows.Count, 4].Borders.Color = ColorTranslator.ToOle(Color.Black);
                    worksheet.Cells[9 + grdStatus.Rows.Count, 5].Borders.Color = ColorTranslator.ToOle(Color.Black);

                    //  worksheet.Cells[11 + grdStatus.Rows.Count, 2] = "The above files have been checked by THC officials and found satisfactory";



                    // worksheet.Cells[15 + grdStatus.Rows.Count, 2] = "Checked & Verified";
                    // worksheet.Cells[17 + grdStatus.Rows.Count, 2] = "Signature of High Court Of Tripura";
                    // worksheet.Cells[17 + grdStatus.Rows.Count, 2].EntireRow.Font.Bold = true;
                    // worksheet.Cells[15 + grdStatus.Rows.Count, 7] = "Checked & Verified";
                    // worksheet.Cells[17 + grdStatus.Rows.Count, 5] = "Signature of NTPL Supervisor";
                    // worksheet.Cells[17 + grdStatus.Rows.Count, 5].EntireRow.Font.Bold = true;


                    string namexls = "User_Wise_Report_" + userId + ".xls";
                    string path = Path.GetDirectoryName(System.Windows.Forms.Application.ExecutablePath);
                    sfdUAT.Filter = "Xls files (*.xls)|*.xls";
                    sfdUAT.FilterIndex = 2;
                    sfdUAT.RestoreDirectory = true;
                    sfdUAT.FileName = namexls;
                    sfdUAT.ShowDialog();

                    workbook.SaveAs(sfdUAT.FileName, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Microsoft.Office.Interop.Excel.XlSaveAsAccessMode.xlExclusive, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);

                    app.Quit();
                }
                else
                {
                    //Microsoft.Office.Interop.Excel._Application app = new Microsoft.Office.Interop.Excel.Application();
                    //Microsoft.Office.Interop.Excel._Workbook workbook = app.Workbooks.Add(Type.Missing);

                    //Microsoft.Office.Interop.Excel._Worksheet worksheet = null;

                    //app.Visible = false;

                    //worksheet = (Microsoft.Office.Interop.Excel._Worksheet)workbook.Sheets["Sheet1"];


                    //worksheet = (Microsoft.Office.Interop.Excel._Worksheet)workbook.ActiveSheet;

                    worksheet.Name = "User Wise Report";

                    worksheet.Cells[1, 3] = "User Wise Report";
                    Range range44 = worksheet.get_Range("C1");
                    range44.Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.YellowGreen);

                    worksheet.Rows.AutoFit();
                    worksheet.Columns.AutoFit();

                    worksheet.Cells[3, 1] = "User Report for : " + stage;
                    Range range43 = worksheet.get_Range("A3");
                    range43.Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Yellow);
                    worksheet.Rows.AutoFit();
                    worksheet.Columns.AutoFit();

                    worksheet.Cells[4, 1] = "Date from : " + stDate + "-" + endDate;
                    Range range8 = worksheet.get_Range("A4");
                    range8.Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Yellow);
                    worksheet.Rows.AutoFit();
                    worksheet.Columns.AutoFit();

                    worksheet.Cells[5, 1] = "User Id :" + userId;
                    Range range34 = worksheet.get_Range("A5");
                    range34.Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Yellow);
                    worksheet.Rows.AutoFit();
                    worksheet.Columns.AutoFit();

                    Range range = worksheet.get_Range("A3", "A5");
                    range.Borders.Color = ColorTranslator.ToOle(Color.Black);

                    Range range1 = worksheet.get_Range("A7", "D7");
                    range1.Borders.Color = ColorTranslator.ToOle(Color.Black);

                    for (i = 1; i < grdStatus.Columns.Count + 1; i++)
                    {
                        Range range2 = worksheet.get_Range("A7", "D7");
                        range2.Borders.Color = ColorTranslator.ToOle(Color.Black);
                        range2.EntireRow.AutoFit();
                        range2.EntireRow.AutoFit();
                        worksheet.Cells[7, i] = grdStatus.Columns[i - 1].HeaderText;

                    }
                    worksheet.Cells[9 + grdStatus.Rows.Count, 3] = "Total";
                    worksheet.Cells[9 + grdStatus.Rows.Count, 3].Font.Bold = true;
                    worksheet.Cells[9 + grdStatus.Rows.Count, 3].Borders.Color = ColorTranslator.ToOle(Color.Black);


                    //int imgcount = 0;
                    int filecount = 0;
                    for (i = 0; i < grdStatus.Rows.Count; i++)
                    {
                        for (j = 0; j < grdStatus.Columns.Count; j++)
                        {
                            Range range3 = worksheet.Cells;
                            range3.EntireRow.AutoFit();
                            range3.EntireColumn.AutoFit();
                            worksheet.Cells[i + 8, j + 1] = grdStatus.Rows[i].Cells[j].Value.ToString();
                            worksheet.Cells[i + 8, j + 1].Borders.Color = ColorTranslator.ToOle(Color.Black);
                        }
                        filecount = grdStatus.Rows.Count;
                        //imgcount = imgcount + Convert.ToInt32(grdStatus.Rows[i].Cells[3].Value.ToString());
                    }
                    worksheet.Cells[9 + grdStatus.Rows.Count, 4] = filecount.ToString();
                    // worksheet.Cells[9 + grdStatus.Rows.Count, 5] = imgcount.ToString();
                    // worksheet.Cells[9 + grdStatus.Rows.Count, 3].Borders.Color = ColorTranslator.ToOle(Color.Black);
                    worksheet.Cells[9 + grdStatus.Rows.Count, 4].Borders.Color = ColorTranslator.ToOle(Color.Black);
                    // worksheet.Cells[9 + grdStatus.Rows.Count, 5].Borders.Color = ColorTranslator.ToOle(Color.Black);

                    string namexls = "User_Wise_Report_" + userId + ".xls";
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
}
