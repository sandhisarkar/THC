﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using NovaNet.wfe;
using NovaNet.Utils;
using System.Data.Odbc;
using System.Net;
using LItems;
using System.IO;
using System.Collections;

namespace ImageHeaven
{
    public partial class frmBundleSelect : Form
    {
        NovaNet.Utils.dbCon dbcon;
        OdbcConnection sqlCon = null;
        eSTATES[] state;
        public static string projKey;
        public static string bundleKey;
        //Transaction
        OdbcTransaction txn;
        //The credentials with which the user has logged on...
        Credentials crd = new Credentials();
        //The control object which will act as the key to retrieving records

        public frmBundleSelect()
        {
            InitializeComponent();
        }

        public frmBundleSelect(eSTATES[] prmState, OdbcConnection prmCon,OdbcTransaction pTxn, Credentials prmCrd)
		{
			//
			// The InitializeComponent() call is required for Windows Forms designer support.
			//
			InitializeComponent();
            sqlCon = prmCon;
			this.Text = "B'Zer - Tripura High Court - Fetch Docket";
			state=prmState;

            crd = prmCrd;
            
            txn = pTxn;
            //
            // TODO: Add constructor code after the InitializeComponent() call.
            //
        }

       

        private void frmBundleSelect_Load(object sender, EventArgs e)
        {
            populateProject();

        }
        private void populateProject()
        {
            
                DataSet ds = new DataSet();
                DataTable dt = new DataTable();

                string sql = "select proj_key, proj_code from project_master  ";
                OdbcCommand cmd = new OdbcCommand(sql, sqlCon, txn);
                OdbcDataAdapter odap = new OdbcDataAdapter(cmd);
                odap.Fill(dt);


                if (dt.Rows.Count > 0)
                {
                    cmbProject.DataSource = dt;
                    cmbProject.DisplayMember = "proj_code";
                    cmbProject.ValueMember = "proj_key";

                    populateBundle();
                }
                else
                {
                    cmbProject.DataSource = null;
                    // cmbProject.Text = "";
                    MessageBox.Show("Add one project first...");
                    this.Close();
                }
            

        }

        private void populateBundle()
        {
            if (state[0] == eSTATES.METADATA_ENTRY)
            {
                DataSet ds = new DataSet();
                DataTable dt = new DataTable();

                string sql = "select a.bundle_key, a.bundle_code from bundle_master a, project_master b where a.proj_code = b.proj_key and a.proj_code = '" + cmbProject.SelectedValue.ToString() + "'";
                OdbcCommand cmd = new OdbcCommand(sql, sqlCon, txn);
                OdbcDataAdapter odap = new OdbcDataAdapter(cmd);
                odap.Fill(dt);


                if (dt.Rows.Count > 0)
                {
                    cmbBundle.DataSource = dt;
                    cmbBundle.DisplayMember = "bundle_code";
                    cmbBundle.ValueMember = "bundle_key";
                }
                else
                {

                    cmbBundle.Text = string.Empty;
                    cmbBundle.DataSource = null;
                    cmbBundle.DisplayMember = "";
                    cmbBundle.ValueMember = "";
                    cmbProject.Select();

                }
            }
            if (state[0] == eSTATES.BARCODE_ENTRY)
            {
                DataSet ds = new DataSet();
                DataTable dt = new DataTable();

                string sql = "select a.bundle_key, a.bundle_code from bundle_master a, project_master b where a.proj_code = b.proj_key and a.proj_code = '" + cmbProject.SelectedValue.ToString() + "'";
                OdbcCommand cmd = new OdbcCommand(sql, sqlCon, txn);
                OdbcDataAdapter odap = new OdbcDataAdapter(cmd);
                odap.Fill(dt);


                if (dt.Rows.Count > 0)
                {
                    cmbBundle.DataSource = dt;
                    cmbBundle.DisplayMember = "bundle_code";
                    cmbBundle.ValueMember = "bundle_key";
                }
                else
                {

                    cmbBundle.Text = string.Empty;
                    cmbBundle.DataSource = null;
                    cmbBundle.DisplayMember = "";
                    cmbBundle.ValueMember = "";
                    cmbProject.Select();

                }
            }
            if (state[0] == eSTATES.POLICY_CREATED)
            {
                DataSet ds = new DataSet();
                DataTable dt = new DataTable();

                string sql = "select a.bundle_key, a.bundle_code from bundle_master a, project_master b where a.proj_code = b.proj_key and a.proj_code = '" + cmbProject.SelectedValue.ToString() + "' and a.status = '1'";
                OdbcCommand cmd = new OdbcCommand(sql, sqlCon, txn);
                OdbcDataAdapter odap = new OdbcDataAdapter(cmd);
                odap.Fill(dt);


                if (dt.Rows.Count > 0)
                {
                    cmbBundle.DataSource = dt;
                    cmbBundle.DisplayMember = "bundle_code";
                    cmbBundle.ValueMember = "bundle_key";
                }
                else
                {

                    cmbBundle.Text = string.Empty;
                    cmbBundle.DataSource = null;
                    cmbBundle.DisplayMember = "";
                    cmbBundle.ValueMember = "";
                    cmbProject.Select();

                }
            }
            if (state[0] == eSTATES.POLICY_SCANNED)
            {
                DataSet ds = new DataSet();
                DataTable dt = new DataTable();

                string sql = "select a.bundle_key, a.bundle_code from bundle_master a, project_master b where a.proj_code = b.proj_key and a.proj_code = '" + cmbProject.SelectedValue.ToString() + "' and a.status = '2'";
                OdbcCommand cmd = new OdbcCommand(sql, sqlCon, txn);
                OdbcDataAdapter odap = new OdbcDataAdapter(cmd);
                odap.Fill(dt);


                if (dt.Rows.Count > 0)
                {
                    cmbBundle.DataSource = dt;
                    cmbBundle.DisplayMember = "bundle_code";
                    cmbBundle.ValueMember = "bundle_key";
                }
                else
                {

                    cmbBundle.Text = string.Empty;
                    cmbBundle.DataSource = null;
                    cmbBundle.DisplayMember = "";
                    cmbBundle.ValueMember = "";
                    cmbProject.Select();

                }
            }
            if(state[0] == eSTATES.POLICY_QC || state[0] == eSTATES.POLICY_ON_HOLD)
            {
                DataSet ds = new DataSet();
                DataTable dt = new DataTable();

                string sql = "select a.bundle_key, a.bundle_code from bundle_master a, project_master b where a.proj_code = b.proj_key and a.proj_code = '" + cmbProject.SelectedValue.ToString() + "' and a.status = '3'";
                OdbcCommand cmd = new OdbcCommand(sql, sqlCon, txn);
                OdbcDataAdapter odap = new OdbcDataAdapter(cmd);
                odap.Fill(dt);


                if (dt.Rows.Count > 0)
                {
                    cmbBundle.DataSource = dt;
                    cmbBundle.DisplayMember = "bundle_code";
                    cmbBundle.ValueMember = "bundle_key";
                }
                else
                {
                    DataTable dt1 = new DataTable();
                    string sql1 = "select distinct a.bundle_key, a.bundle_code from bundle_master a, case_file_master b where a.proj_code = b.proj_code and a.bundle_key = b.bundle_key  and a.proj_code = '" + cmbProject.SelectedValue.ToString() + "' and b.status = '3'";
                    OdbcCommand cmd1 = new OdbcCommand(sql1, sqlCon, txn);
                    OdbcDataAdapter odap1 = new OdbcDataAdapter(cmd1);
                    odap1.Fill(dt1);

                    if (dt1.Rows.Count > 0)
                    {
                        cmbBundle.DataSource = dt1;
                        cmbBundle.DisplayMember = "bundle_code";
                        cmbBundle.ValueMember = "bundle_key";
                    }
                    else
                    {
                        cmbBundle.Text = string.Empty;
                        cmbBundle.DataSource = null;
                        cmbBundle.DisplayMember = "";
                        cmbBundle.ValueMember = "";
                        cmbProject.Select();
                    }
                    

                }
            }
            if(state[0] == eSTATES.POLICY_INDEXED || state[0]== eSTATES.POLICY_ON_HOLD)
            {
                DataSet ds = new DataSet();
                DataTable dt = new DataTable();

                string sql = "select distinct a.bundle_key, a.bundle_code from bundle_master a, project_master b, case_file_master c  where a.proj_code = b.proj_key and a.proj_code = '" + cmbProject.SelectedValue.ToString() + "' and c.proj_code = a.proj_code and c.bundle_key = a.bundle_key and (a.status = '4' or a.status = '5' or a.status = '6' or a.status = '7' or a.status = '8' or a.status = '30' or a.status = '31' or a.status = '37' or a.status = '40' or a.status = '77' or c.status = '4' or c.status = '5' or c.status = '30' or c.status = '31' or c.status = '37' or c.status = '40' or c.status = '77')";
                OdbcCommand cmd = new OdbcCommand(sql, sqlCon, txn);
                OdbcDataAdapter odap = new OdbcDataAdapter(cmd);
                odap.Fill(dt);


                if (dt.Rows.Count > 0)
                {
                    cmbBundle.DataSource = dt;
                    cmbBundle.DisplayMember = "bundle_code";
                    cmbBundle.ValueMember = "bundle_key";
                }
                else
                {
                    DataTable dt1 = new DataTable();
                    string sql1 = "select distinct a.bundle_key, a.bundle_code from bundle_master a, case_file_master b where a.proj_code = b.proj_code and a.bundle_key = b.bundle_key and a.proj_code = '" + cmbProject.SelectedValue.ToString() + "' and (b.status = '4' or b.status = '5' or b.status = '30' or b.status = '31' or b.status = '37' or b.status = '40' or b.status = '77')";
                    OdbcCommand cmd1 = new OdbcCommand(sql1, sqlCon, txn);
                    OdbcDataAdapter odap1 = new OdbcDataAdapter(cmd1);
                    odap1.Fill(dt1);

                    if (dt1.Rows.Count > 0)
                    {
                        cmbBundle.DataSource = dt1;
                        cmbBundle.DisplayMember = "bundle_code";
                        cmbBundle.ValueMember = "bundle_key";
                    }

                    cmbBundle.Text = string.Empty;
                    cmbBundle.DataSource = null;
                    cmbBundle.DisplayMember = "";
                    cmbBundle.ValueMember = "";
                    cmbProject.Select();
                }
            }
        }

        private void cmbProject_Leave(object sender, EventArgs e)
        {
            populateBundle();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            projKey = null;
            bundleKey = null;
            this.Close();
        }



        private void btnOK_Click(object sender, EventArgs e)
        {
            frmMain frm = new frmMain();

            if (state[0] == eSTATES.METADATA_ENTRY)
            {


                if ((cmbProject.Text.ToString() != string.Empty) && (cmbBundle.Text.ToString() != string.Empty))
                {

                    projKey = Convert.ToString(cmbProject.SelectedValue.ToString());
                    bundleKey = Convert.ToString(cmbBundle.SelectedValue.ToString());
                    this.Close();

                }
                else
                {
                    MessageBox.Show(this, "Please select Project and proper Bundle from the dropdown", "B'Zer - Tripura High Court - Selection Error !", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }
            if (state[0] == eSTATES.BARCODE_ENTRY)
            {


                if ((cmbProject.Text.ToString() != string.Empty) && (cmbBundle.Text.ToString() != string.Empty))
                {

                    projKey = Convert.ToString(cmbProject.SelectedValue.ToString());
                    bundleKey = Convert.ToString(cmbBundle.SelectedValue.ToString());
                    this.Close();

                }
                else
                {
                    MessageBox.Show(this, "Please select Project and proper Bundle from the dropdown", "B'Zer - Tripura High Court - Selection Error !", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }
            if (state[0] == eSTATES.POLICY_CREATED)
            {
                if ((cmbProject.Text.ToString() != string.Empty) && (cmbBundle.Text.ToString() != string.Empty))
                {

                    projKey = Convert.ToString(cmbProject.SelectedValue.ToString());
                    bundleKey = Convert.ToString(cmbBundle.SelectedValue.ToString());
                    this.Close();

                }
                else
                {
                    MessageBox.Show(this, "Please select Project and proper Bundle from the dropdown", "B'Zer - Tripura High Court - Selection Error !", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

            }

            if (state[0] == eSTATES.POLICY_SCANNED)
            {
                if ((cmbProject.Text.ToString() != string.Empty) && (cmbBundle.Text.ToString() != string.Empty))
                {

                    projKey = Convert.ToString(cmbProject.SelectedValue.ToString());
                    bundleKey = Convert.ToString(cmbBundle.SelectedValue.ToString());
                    this.Close();

                }
                else
                {
                    MessageBox.Show(this, "Please select Project and proper Bundle from the dropdown", "B'Zer - Tripura High Court - Selection Error !", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }

            if (state[0] == eSTATES.POLICY_QC || state[0] == eSTATES.POLICY_ON_HOLD)
            {
                if ((cmbProject.Text.ToString() != string.Empty) && (cmbBundle.Text.ToString() != string.Empty))
                {

                    projKey = Convert.ToString(cmbProject.SelectedValue.ToString());
                    bundleKey = Convert.ToString(cmbBundle.SelectedValue.ToString());
                    this.Close();

                }
                else
                {
                    MessageBox.Show(this, "Please select Project and proper Bundle from the dropdown", "B'Zer - Tripura High Court - Selection Error !", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }
            if (state[0] == eSTATES.POLICY_INDEXED || state[0] == eSTATES.POLICY_ON_HOLD)
            {
                if ((cmbProject.Text.ToString() != string.Empty) && (cmbBundle.Text.ToString() != string.Empty))
                {

                    projKey = Convert.ToString(cmbProject.SelectedValue.ToString());
                    bundleKey = Convert.ToString(cmbBundle.SelectedValue.ToString());
                    this.Close();

                }
                else
                {
                    MessageBox.Show(this, "Please select Project and proper Bundle from the dropdown", "B'Zer - Tripura High Court - Selection Error !", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }
        }
    }
}
