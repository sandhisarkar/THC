﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
//using System.Linq;
using System.Text;
using System.Windows.Forms;
using NovaNet.Utils;
using System.Data.Odbc;
using System.Reflection;
using System.Data.OleDb;
using System.Globalization;
using LItems;
using NovaNet;
using NovaNet.wfe;


namespace ImageHeaven
{
    public partial class frmMain : Form
    {

        static wItem wi;
        //NovaNet.Utils.dbCon dbcon;
        frmMain mainForm;
        OdbcConnection sqlCon = null;
        public Credentials crd;
        static int colorMode;
        dbCon dbcon;
        
        //
        NovaNet.Utils.GetProfile pData;
        NovaNet.Utils.ChangePassword pCPwd;
        NovaNet.Utils.Profile p;
        public static NovaNet.Utils.IntrRBAC rbc;
        private short logincounter;
        //
        OdbcTransaction txn;

        public static string projKey;
        public static string bundleKey;
        public static string projectName = null;
        public static string batchName = null;
        public static string boxNumber = null;
        public static string projectVal = null;
        public static string batchVal = null;

        public static string name;

        public static int height;
        public static int width;
        
        public frmMain(OdbcConnection pCon)
        {
            InitializeComponent();

            sqlCon = pCon;

            logincounter = 0;

            ImageHeaven.Program.Logout = false;
        }

        public frmMain()
        {
            //
            // The InitializeComponent() call is required for Windows Forms designer support.
            //
            AssemblyName assemName = Assembly.GetExecutingAssembly().GetName();
            this.Text = "B'Zer - Tripura High Court" + "           Version: " + assemName.ToString();
            InitializeComponent();
            
            logincounter = 0;
            //
            // TODO: Add constructor code after the InitializeComponent() call.
            //
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            height = pictureBox1.Height;
            width = pictureBox1.Width;

            int k;
            dbcon = new NovaNet.Utils.dbCon();
            try
            {
                string dllPaths = string.Empty;

                menuStrip1.Visible = false;
                toolStrip1.Visible = false;

                if (sqlCon.State == ConnectionState.Open)
                {
                    pData = getData;
                    pCPwd = getCPwd;
                    rbc = new NovaNet.Utils.RBAC(sqlCon, dbcon, pData, pCPwd);
                    //string test = sqlCon.Database;
                    GetChallenge gc = new GetChallenge(getData);




                    gc.ShowDialog(this);

                    crd = rbc.getCredentials(p);
                    AssemblyName assemName = Assembly.GetExecutingAssembly().GetName();
                    this.Text = "B'Zer - Tripura High Court" + "           Version: " + assemName.Version.ToString() + "    Database name: " + sqlCon.Database.ToString() + "    Logged in user: " + crd.userName;

                    name = crd.userName;
                    if (crd.role == ihConstants._ADMINISTRATOR_ROLE || crd.role == "Supervisor")
                    {
                        menuStrip1.Visible = true;
                        newToolStripMenuItem.Visible = true;
                        newToolStripMenuItem.Enabled = true;
                        toolsToolStripMenuItem.Visible = true;
                        transactionsToolStripMenuItem.Enabled = true;
                        transactionsToolStripMenuItem.Visible = true;
                        dataEntryToolStripMenuItem.Visible = true;
                        //imageImportToolStripMenuItem.Visible = true;
                        exportToolStripMenuItem.Visible = true;
                        newToolStripMenuItem.Visible = true;
                        toolsToolStripMenuItem.Visible = true;
                        newPasswordToolStripMenuItem.Visible = true;
                        newUserToolStripMenuItem.Visible = true;
                        onlineUsersToolStripMenuItem.Visible = true;
                        dataEntryToolStripMenuItem.Visible = true;
                        imageImportToolStripMenuItem.Visible = true;
                        exportToolStripMenuItem.Visible = true;
                        batchUploadToolStripMenuItem.Visible = true;
                        batchUploadToolStripMenuItem.Enabled = true;
                        toolStrip1.Visible = true;
                        toolStripButton1.Visible = true;
                        imageQualityControlToolStripMenuItem.Visible = true;
                        toolStripButton3.Visible = true;
                        indexingToolStripMenuItem.Visible = true;
                        toolStripButton2.Visible = true;
                        qualityControlFinalToolStripMenuItem.Visible = true;
                        toolStripButton4.Visible = true;
                        toolStripMenuItem1.Visible = true;
                        outwardSubmissionToolStripMenuItem.Visible = true;
                        //generateBarcodeToolStripMenuItem.Visible = false;
                        reportToolStripMenuItem.Visible = true;
                        reportToolStripMenuItem.Enabled = true;
                        dashboardToolStripMenuItem1.Visible = true;
                        dashboardToolStripMenuItem1.Enabled = true;
                        productionReportToolStripMenuItem.Visible = true;
                        productionReportToolStripMenuItem.Enabled = true;
                        siteReportToolStripMenuItem.Visible = true;
                        siteReportToolStripMenuItem.Enabled = true;
                        masterUpdateToolStripMenuItem.Visible = true;
                        masterUpdateToolStripMenuItem.Enabled = true;
                        judgeCreationToolStripMenuItem.Visible = true;
                        districtCreationToolStripMenuItem.Visible = true;

                        generateBarcodeToolStripMenuItem1.Visible = true;
                        caseFileCreationToolStripMenuItem.Visible = false;
                        caseFileCreationToolStripMenuItem.Enabled = false;

                        configurationToolStripMenuItem.Visible = true;


                        helpToolStripMenuItem.Visible = true;


                        aboutToolStripMenuItem.Visible = true;

                        auditToolStripMenuItem.Visible = true;

                        userWiseReportToolStripMenuItem.Visible = true;

                        userWiseCountReportToolStripMenuItem.Visible = false;
                        outwardReportToolStripMenuItem.Visible = true;
                        uATCertificateToolStripMenuItem.Visible = true;
                    }
                    else if (crd.role == "Metadata Entry")
                    {
                        menuStrip1.Visible = true;
                        newToolStripMenuItem.Enabled = true;
                        newToolStripMenuItem.Visible = true;
                        projectToolStripMenuItem.Enabled = false;
                        batchToolStripMenuItem.Enabled = false;
                        caseFileCreationToolStripMenuItem.Visible = false;
                        caseFileCreationToolStripMenuItem.Enabled = false;
                        masterUpdateToolStripMenuItem.Visible = false;
                        masterUpdateToolStripMenuItem.Enabled = false;
                        judgeCreationToolStripMenuItem.Visible = false;
                        districtCreationToolStripMenuItem.Visible = false;
                        generateBarcodeToolStripMenuItem1.Enabled = false;
                        exitToolStripMenuItem.Enabled = true;
                        exitToolStripMenuItem.Visible = true;
                        transactionsToolStripMenuItem.Enabled = true;
                        transactionsToolStripMenuItem.Visible = true;
                        dataEntryToolStripMenuItem.Visible = true;
                        batchUploadToolStripMenuItem.Enabled = false;
                        batchUploadToolStripMenuItem.Visible = false;
                        bundleScanToolStripMenuItem.Enabled = false;
                        bundleScanToolStripMenuItem.Visible = false;
                        imageImportToolStripMenuItem.Enabled = false;
                        imageImportToolStripMenuItem.Visible = false;
                        //imageImportToolStripMenuItem.ShortcutKeys = Keys.None;
                        imageQualityControlToolStripMenuItem.Enabled = false;
                        imageQualityControlToolStripMenuItem.Visible = false;
                        indexingToolStripMenuItem.Enabled = false;
                        indexingToolStripMenuItem.Visible = false;
                        qualityControlFinalToolStripMenuItem.Enabled = false;
                        qualityControlFinalToolStripMenuItem.Visible = false;
                        toolStripMenuItem1.Enabled = false;
                        toolStripMenuItem1.Visible = false;
                        exportToolStripMenuItem.Enabled = false;
                        exportToolStripMenuItem.Visible = false;
                        toolsToolStripMenuItem.Enabled = true;
                        toolsToolStripMenuItem.Visible = true;
                        outwardSubmissionToolStripMenuItem.Visible = false;
                        newPasswordToolStripMenuItem.Visible = true;
                        newPasswordToolStripMenuItem.Enabled = true;
                        newUserToolStripMenuItem.Visible = false;
                        newUserToolStripMenuItem.Enabled = false;
                        onlineUsersToolStripMenuItem.Visible = false;
                        onlineUsersToolStripMenuItem.Enabled = false;
                        reportToolStripMenuItem.Visible = true;
                        reportToolStripMenuItem.Enabled = true;
                        dashboardToolStripMenuItem1.Visible = false;
                        dashboardToolStripMenuItem1.Enabled = false;
                        productionReportToolStripMenuItem.Visible = true;
                        productionReportToolStripMenuItem.Enabled = true;
                        siteReportToolStripMenuItem.Visible = false;
                        siteReportToolStripMenuItem.Enabled = false;
                        logoutToolStripMenuItem.Visible = true;
                        toolStrip1.Visible = false;

                        configurationToolStripMenuItem.Visible = false;


                        helpToolStripMenuItem.Visible = true;


                        aboutToolStripMenuItem.Visible = true;

                        auditToolStripMenuItem.Visible = false;
                        userWiseReportToolStripMenuItem.Visible = false;

                        userWiseCountReportToolStripMenuItem.Visible = false;
                        outwardReportToolStripMenuItem.Visible = false;
                        uATCertificateToolStripMenuItem.Visible = false;

                    }
                    else if (crd.role == "Scan")
                    {
                        menuStrip1.Visible = true;
                        newToolStripMenuItem.Enabled = false;
                        newToolStripMenuItem.Visible = false;
                        projectToolStripMenuItem.Enabled = false;
                        batchToolStripMenuItem.Enabled = false;
                        caseFileCreationToolStripMenuItem.Visible = false;
                        caseFileCreationToolStripMenuItem.Enabled = false;

                        masterUpdateToolStripMenuItem.Visible = false;
                        masterUpdateToolStripMenuItem.Enabled = false;
                        judgeCreationToolStripMenuItem.Visible = false;
                        districtCreationToolStripMenuItem.Visible = false;


                        generateBarcodeToolStripMenuItem1.Enabled = false;
                        exitToolStripMenuItem.Enabled = false;
                        transactionsToolStripMenuItem.Enabled = true;
                        transactionsToolStripMenuItem.Visible = true;
                        dataEntryToolStripMenuItem.Visible = false;
                        batchUploadToolStripMenuItem.Enabled = false;
                        batchUploadToolStripMenuItem.Visible = false;
                        bundleScanToolStripMenuItem.Enabled = true;
                        bundleScanToolStripMenuItem.Visible = true;
                        imageImportToolStripMenuItem.Enabled = true;
                        imageImportToolStripMenuItem.Visible = true;
                        //imageImportToolStripMenuItem.ShortcutKeys = Keys.None;
                        imageQualityControlToolStripMenuItem.Enabled = false;
                        imageQualityControlToolStripMenuItem.Visible = false;
                        indexingToolStripMenuItem.Enabled = false;
                        indexingToolStripMenuItem.Visible = false;
                        qualityControlFinalToolStripMenuItem.Enabled = true;
                        qualityControlFinalToolStripMenuItem.Visible = true;
                        toolStripMenuItem1.Enabled = false;
                        toolStripMenuItem1.Visible = false;
                        exportToolStripMenuItem.Enabled = false;
                        exportToolStripMenuItem.Visible = false;
                        toolsToolStripMenuItem.Enabled = true;
                        toolsToolStripMenuItem.Visible = true;
                        outwardSubmissionToolStripMenuItem.Visible = false;

                        newPasswordToolStripMenuItem.Visible = true;
                        newPasswordToolStripMenuItem.Enabled = true;
                        newUserToolStripMenuItem.Visible = false;
                        newUserToolStripMenuItem.Enabled = false;
                        onlineUsersToolStripMenuItem.Visible = false;
                        onlineUsersToolStripMenuItem.Enabled = false;
                        reportToolStripMenuItem.Visible = true;
                        reportToolStripMenuItem.Enabled = true;
                        dashboardToolStripMenuItem1.Visible = false;
                        dashboardToolStripMenuItem1.Enabled = false;
                        productionReportToolStripMenuItem.Visible = true;
                        productionReportToolStripMenuItem.Enabled = true;
                        siteReportToolStripMenuItem.Visible = false;
                        siteReportToolStripMenuItem.Enabled = false;
                        logoutToolStripMenuItem.Visible = true;
                        toolStrip1.Visible = true;

                        toolStripButton1.Visible = true;
                        toolStripButton3.Visible = false;
                        toolStripButton2.Visible = false;
                        toolStripButton4.Visible = true;

                        configurationToolStripMenuItem.Visible = false;


                        helpToolStripMenuItem.Visible = true;


                        aboutToolStripMenuItem.Visible = true;

                        auditToolStripMenuItem.Visible = false;
                        userWiseReportToolStripMenuItem.Visible = false;

                        userWiseCountReportToolStripMenuItem.Visible = false;
                        outwardReportToolStripMenuItem.Visible = false;
                        uATCertificateToolStripMenuItem.Visible = false;


                    }
                    else if (crd.role == "QC")
                    {
                        menuStrip1.Visible = true;
                        newToolStripMenuItem.Enabled = false;
                        newToolStripMenuItem.Visible = false;
                        projectToolStripMenuItem.Enabled = false;
                        batchToolStripMenuItem.Enabled = false;
                        caseFileCreationToolStripMenuItem.Visible = false;
                        caseFileCreationToolStripMenuItem.Enabled = false;

                        masterUpdateToolStripMenuItem.Visible = false;
                        masterUpdateToolStripMenuItem.Enabled = false;
                        judgeCreationToolStripMenuItem.Visible = false;
                        districtCreationToolStripMenuItem.Visible = false;

                        generateBarcodeToolStripMenuItem1.Enabled = false;
                        exitToolStripMenuItem.Enabled = false;
                        transactionsToolStripMenuItem.Enabled = true;
                        transactionsToolStripMenuItem.Visible = true;
                        dataEntryToolStripMenuItem.Enabled = false;
                        dataEntryToolStripMenuItem.Visible = false;
                        batchUploadToolStripMenuItem.Enabled = false;
                        batchUploadToolStripMenuItem.Visible = false;
                        bundleScanToolStripMenuItem.Enabled = false;
                        bundleScanToolStripMenuItem.Visible = false;
                        imageImportToolStripMenuItem.Enabled = false;
                        imageImportToolStripMenuItem.Visible = false;
                        //imageImportToolStripMenuItem.ShortcutKeys = Keys.None;
                        imageQualityControlToolStripMenuItem.Enabled = true;
                        imageQualityControlToolStripMenuItem.Visible = true;
                        indexingToolStripMenuItem.Enabled = false;
                        indexingToolStripMenuItem.Visible = false;
                        qualityControlFinalToolStripMenuItem.Enabled = false;
                        qualityControlFinalToolStripMenuItem.Visible = false;
                        toolStripMenuItem1.Enabled = false;
                        toolStripMenuItem1.Visible = false;
                        exportToolStripMenuItem.Enabled = false;
                        exportToolStripMenuItem.Visible = false;
                        toolsToolStripMenuItem.Enabled = true;
                        toolsToolStripMenuItem.Visible = true;
                        outwardSubmissionToolStripMenuItem.Visible = false;

                        newPasswordToolStripMenuItem.Visible = true;
                        newPasswordToolStripMenuItem.Enabled = true;
                        newUserToolStripMenuItem.Visible = false;
                        newUserToolStripMenuItem.Enabled = false;
                        onlineUsersToolStripMenuItem.Visible = false;
                        onlineUsersToolStripMenuItem.Enabled = false;
                        reportToolStripMenuItem.Visible = true;
                        reportToolStripMenuItem.Enabled = true;
                        dashboardToolStripMenuItem1.Visible = false;
                        dashboardToolStripMenuItem1.Enabled = false;
                        productionReportToolStripMenuItem.Visible = true;
                        productionReportToolStripMenuItem.Enabled = true;
                        siteReportToolStripMenuItem.Visible = false;
                        siteReportToolStripMenuItem.Enabled = false;
                        logoutToolStripMenuItem.Visible = true;
                        toolStrip1.Visible = true;

                        toolStripButton1.Visible = false;
                        toolStripButton3.Visible = true;
                        toolStripButton2.Visible = false;
                        toolStripButton4.Visible = false;

                        configurationToolStripMenuItem.Visible = false;


                        helpToolStripMenuItem.Visible = true;


                        aboutToolStripMenuItem.Visible = true;

                        auditToolStripMenuItem.Visible = false;
                        userWiseReportToolStripMenuItem.Visible = false;

                        userWiseCountReportToolStripMenuItem.Visible = false;
                        outwardReportToolStripMenuItem.Visible = false;
                        uATCertificateToolStripMenuItem.Visible = false;


                    }
                    else if (crd.role == "DOC Type Association")
                    {
                        menuStrip1.Visible = true;
                        newToolStripMenuItem.Enabled = false;
                        newToolStripMenuItem.Visible = false;
                        projectToolStripMenuItem.Enabled = false;
                        batchToolStripMenuItem.Enabled = false;
                        caseFileCreationToolStripMenuItem.Visible = false;
                        caseFileCreationToolStripMenuItem.Enabled = false;

                        masterUpdateToolStripMenuItem.Visible = false;
                        masterUpdateToolStripMenuItem.Enabled = false;
                        judgeCreationToolStripMenuItem.Visible = false;
                        districtCreationToolStripMenuItem.Visible = false;

                        generateBarcodeToolStripMenuItem1.Enabled = false;
                        exitToolStripMenuItem.Enabled = false;
                        transactionsToolStripMenuItem.Enabled = true;
                        transactionsToolStripMenuItem.Visible = true;
                        dataEntryToolStripMenuItem.Enabled = false;
                        dataEntryToolStripMenuItem.Visible = false;
                        batchUploadToolStripMenuItem.Enabled = false;
                        batchUploadToolStripMenuItem.Visible = false;
                        bundleScanToolStripMenuItem.Enabled = false;
                        bundleScanToolStripMenuItem.Visible = false;
                        imageImportToolStripMenuItem.Enabled = false;
                        imageImportToolStripMenuItem.Visible = false;
                        //imageImportToolStripMenuItem.ShortcutKeys = Keys.None;
                        imageQualityControlToolStripMenuItem.Enabled = false;
                        imageQualityControlToolStripMenuItem.Visible = false;
                        indexingToolStripMenuItem.Enabled = true;
                        indexingToolStripMenuItem.Visible = true;
                        qualityControlFinalToolStripMenuItem.Enabled = true;
                        qualityControlFinalToolStripMenuItem.Visible = true;
                        toolStripMenuItem1.Enabled = false;
                        toolStripMenuItem1.Visible = false;
                        exportToolStripMenuItem.Enabled = false;
                        exportToolStripMenuItem.Visible = false;
                        toolsToolStripMenuItem.Enabled = true;
                        toolsToolStripMenuItem.Visible = true;
                        outwardSubmissionToolStripMenuItem.Visible = false;

                        newPasswordToolStripMenuItem.Visible = true;
                        newPasswordToolStripMenuItem.Enabled = true;
                        newUserToolStripMenuItem.Visible = false;
                        newUserToolStripMenuItem.Enabled = false;
                        onlineUsersToolStripMenuItem.Visible = false;
                        onlineUsersToolStripMenuItem.Enabled = false;
                        reportToolStripMenuItem.Visible = true;
                        reportToolStripMenuItem.Enabled = true;
                        dashboardToolStripMenuItem1.Visible = false;
                        dashboardToolStripMenuItem1.Enabled = false;
                        productionReportToolStripMenuItem.Visible = true;
                        productionReportToolStripMenuItem.Enabled = true;
                        siteReportToolStripMenuItem.Visible = false;
                        siteReportToolStripMenuItem.Enabled = false;
                        logoutToolStripMenuItem.Visible = true;
                        toolStrip1.Visible = true;

                        toolStripButton1.Visible = false;
                        toolStripButton3.Visible = false;
                        toolStripButton2.Visible = true;
                        toolStripButton4.Visible = true;

                        configurationToolStripMenuItem.Visible = false;


                        helpToolStripMenuItem.Visible = true;


                        aboutToolStripMenuItem.Visible = true;

                        auditToolStripMenuItem.Visible = false;
                        userWiseReportToolStripMenuItem.Visible = false;
                        userWiseCountReportToolStripMenuItem.Visible = false;
                        outwardReportToolStripMenuItem.Visible = false;
                        uATCertificateToolStripMenuItem.Visible = false;


                    }
                    else if (crd.role == "InventoryIn")
                    {
                        menuStrip1.Visible = true;
                        newToolStripMenuItem.Enabled = true;
                        newToolStripMenuItem.Visible = true;
                        projectToolStripMenuItem.Enabled = false;
                        batchToolStripMenuItem.Enabled = true;
                        batchToolStripMenuItem.Visible = true;
                        caseFileCreationToolStripMenuItem.Visible = false;
                        caseFileCreationToolStripMenuItem.Enabled = false;


                        masterUpdateToolStripMenuItem.Visible = false;
                        masterUpdateToolStripMenuItem.Enabled = false;
                        judgeCreationToolStripMenuItem.Visible = false;
                        districtCreationToolStripMenuItem.Visible = false;

                        generateBarcodeToolStripMenuItem1.Enabled = true;
                        generateBarcodeToolStripMenuItem1.Visible = true;
                        exitToolStripMenuItem.Enabled = true;
                        exitToolStripMenuItem.Visible = true;
                        transactionsToolStripMenuItem.Enabled = false;
                        transactionsToolStripMenuItem.Visible = false;
                        dataEntryToolStripMenuItem.Enabled = false;
                        dataEntryToolStripMenuItem.Visible = false;
                        batchUploadToolStripMenuItem.Enabled = false;
                        batchUploadToolStripMenuItem.Visible = false;
                        bundleScanToolStripMenuItem.Enabled = false;
                        bundleScanToolStripMenuItem.Visible = false;
                        imageImportToolStripMenuItem.Enabled = false;
                        imageImportToolStripMenuItem.Visible = false;
                        //imageImportToolStripMenuItem.ShortcutKeys = Keys.None;
                        imageQualityControlToolStripMenuItem.Enabled = false;
                        imageQualityControlToolStripMenuItem.Visible = false;
                        indexingToolStripMenuItem.Enabled = false;
                        indexingToolStripMenuItem.Visible = false;
                        qualityControlFinalToolStripMenuItem.Enabled = false;
                        qualityControlFinalToolStripMenuItem.Visible = false;
                        toolStripMenuItem1.Enabled = false;
                        toolStripMenuItem1.Visible = false;
                        exportToolStripMenuItem.Enabled = false;
                        exportToolStripMenuItem.Visible = false;
                        toolsToolStripMenuItem.Enabled = true;
                        toolsToolStripMenuItem.Visible = true;
                        outwardSubmissionToolStripMenuItem.Visible = false;

                        newPasswordToolStripMenuItem.Visible = true;
                        newPasswordToolStripMenuItem.Enabled = true;
                        newUserToolStripMenuItem.Visible = false;
                        newUserToolStripMenuItem.Enabled = false;
                        onlineUsersToolStripMenuItem.Visible = false;
                        onlineUsersToolStripMenuItem.Enabled = false;
                        reportToolStripMenuItem.Visible = false;
                        reportToolStripMenuItem.Enabled = false;
                        dashboardToolStripMenuItem1.Visible = false;
                        dashboardToolStripMenuItem1.Enabled = false;
                        productionReportToolStripMenuItem.Visible = false;
                        productionReportToolStripMenuItem.Enabled = false;
                        siteReportToolStripMenuItem.Visible = false;
                        siteReportToolStripMenuItem.Enabled = false;
                        logoutToolStripMenuItem.Visible = true;
                        toolStrip1.Visible = false;

                        configurationToolStripMenuItem.Visible = false;


                        helpToolStripMenuItem.Visible = true;


                        aboutToolStripMenuItem.Visible = true;

                        auditToolStripMenuItem.Visible = false;

                        userWiseReportToolStripMenuItem.Visible = false;
                        userWiseCountReportToolStripMenuItem.Visible = false;
                        outwardReportToolStripMenuItem.Visible = false;
                        uATCertificateToolStripMenuItem.Visible = false;


                    }
                    else if (crd.role == ihConstants._LIC_ROLE)
                    {
                        toolStrip1.Visible = false;
                        menuStrip1.Visible = true;
                        newToolStripMenuItem.Visible = false;
                        newToolStripMenuItem.Enabled = false;
                        transactionsToolStripMenuItem.Visible = false;
                        transactionsToolStripMenuItem.Enabled = false;
                        auditToolStripMenuItem.Visible = true;
                        masterUpdateToolStripMenuItem.Visible = false;
                        masterUpdateToolStripMenuItem.Enabled = false;
                        toolsToolStripMenuItem.Visible = true;
                        outwardSubmissionToolStripMenuItem.Visible = false;

                        configurationToolStripMenuItem.Visible = false;
                        newPasswordToolStripMenuItem.Visible = true;
                        newUserToolStripMenuItem.Visible = false;
                        onlineUsersToolStripMenuItem.Visible = false;
                        reportToolStripMenuItem.Visible = true;
                        dashboardToolStripMenuItem1.Visible = false;
                        productionReportToolStripMenuItem.Visible = true;
                        siteReportToolStripMenuItem.Visible = false;
                        userWiseReportToolStripMenuItem.Visible = false;
                        helpToolStripMenuItem.Visible = true;
                        logoutToolStripMenuItem.Visible = true;
                        userWiseCountReportToolStripMenuItem.Visible = false;
                        outwardReportToolStripMenuItem.Visible = false;
                        uATCertificateToolStripMenuItem.Visible = true;


                    }
                    else
                    {
                        //rbc.UnLockedUser(crd.created_by.ToString());
                        menuStrip1.Visible = true;
                        newToolStripMenuItem.Visible = false;
                        toolsToolStripMenuItem.Visible = true;
                        newPasswordToolStripMenuItem.Visible = true;
                        newUserToolStripMenuItem.Visible = false;
                        onlineUsersToolStripMenuItem.Visible = false;
                        dataEntryToolStripMenuItem.Visible = true;
                        imageImportToolStripMenuItem.Visible = true;
                        exportToolStripMenuItem.Visible = false;
                        batchUploadToolStripMenuItem.Visible = false;
                        toolStrip1.Visible = false;
                        outwardSubmissionToolStripMenuItem.Visible = false;

                        caseFileCreationToolStripMenuItem.Visible = false;
                        caseFileCreationToolStripMenuItem.Enabled = false;
                        reportToolStripMenuItem.Visible = false;
                        reportToolStripMenuItem.Enabled = false;
                        dashboardToolStripMenuItem1.Visible = false;
                        dashboardToolStripMenuItem1.Enabled = false;
                        productionReportToolStripMenuItem.Visible = false;
                        productionReportToolStripMenuItem.Enabled = false;
                        siteReportToolStripMenuItem.Visible = false;
                        siteReportToolStripMenuItem.Enabled = false;


                        masterUpdateToolStripMenuItem.Visible = false;
                        masterUpdateToolStripMenuItem.Enabled = false;
                        judgeCreationToolStripMenuItem.Visible = false;
                        districtCreationToolStripMenuItem.Visible = false;

                        configurationToolStripMenuItem.Visible = false;
                        userWiseReportToolStripMenuItem.Visible = false;


                        helpToolStripMenuItem.Visible = true;


                        aboutToolStripMenuItem.Visible = true;

                        auditToolStripMenuItem.Visible = true;
                        userWiseCountReportToolStripMenuItem.Visible = false;
                        outwardReportToolStripMenuItem.Visible = false;
                        uATCertificateToolStripMenuItem.Visible = false;


                    }
                }
            }
            catch (DBConnectionException dbex)
            {
                //MessageBox.Show(dbex.Message, "Image Heaven", MessageBoxButtons.OK, MessageBoxIcon.Error);
                string err = dbex.Message;
                this.Close();
            }
        }
        void getData(ref NovaNet.Utils.Profile prmp)
        {
            int i;
            p = prmp;
            for (i = 1; i <= 2; i++)
            {
                if (rbc.authenticate(p.UserId, p.Password) == false)
                {
                    if (logincounter == 2)
                    {
                        Application.Exit();
                    }
                    else
                    {
                        logincounter++;
                        GetChallenge ogc = new GetChallenge(getData);
                        ogc.ShowDialog(this);
                    }
                }
                else
                {
                    if (rbc.CheckUserIsLogged(p.UserId))
                    {

                        p = rbc.getProfile();
                        crd = rbc.getCredentials(p);
                        if (crd.role != ihConstants._ADMINISTRATOR_ROLE)
                        {
                            rbc.LockedUser(p.UserId, crd.created_dttm);
                        }
                        break;
                    }
                    else
                    {
                        p.UserId = null;
                        p.UserName = null;
                        GetChallenge ogc = new GetChallenge(getData);
                        AssemblyName assemName = Assembly.GetExecutingAssembly().GetName();
                        this.Text = "B'Zer - Tripura High Court" + "           Version: " + assemName.Version.ToString() + "    Database name: " + sqlCon.Database.ToString() + "    Logged in user: " + crd.userName;
                        ogc.ShowDialog(this);
                    }
                }
            }
        }
        void getCPwd(ref NovaNet.Utils.Profile prmpwd)
        {
            p = prmpwd;
            rbc.changePassword(p.UserId, p.UserName, p.Password);
        }

        private void projectToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                frmProject dispProject;
                wi = new wfeProject(sqlCon);
                dispProject = new frmProject(wi, sqlCon);
                dispProject.ShowDialog(this);
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            rbc.UnLockedUser(crd.created_by.ToString());
            this.Close();
        }

        private void dataEntryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //frmEntry frm = new frmEntry(sqlCon);
            //frm.ShowDialog();
        }

        private void batchToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                frmBatch dispProject;
                wi = new wfeBatch(sqlCon);
                dispProject = new frmBatch(wi, sqlCon, DataLayerDefs.Mode._Add);
                dispProject.ShowDialog(this);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void newPasswordToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PwdChange pwdCh = new PwdChange(ref p, getCPwd);
            pwdCh.ShowDialog(this);
        }

        private void newUserToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddNewUser nwUsr = new AddNewUser(getnwusrData, sqlCon);
            nwUsr.ShowDialog(this);
        }
        void getnwusrData(ref NovaNet.Utils.Profile prmp)
        {
            p = prmp;
            if (rbc.addUser(p.UserId, p.UserName, p.Role_des, p.Password) == false)
            {
                AddNewUser nwUsr = new AddNewUser(getnwusrData, sqlCon);
                nwUsr.ShowDialog(this);
            }
        }

        private void onlineUsersToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmLoggedUser loged = new frmLoggedUser(rbc, crd);
            loged.ShowDialog(this);
        }

        public string _GetFileCount(string proj_code, string bundle_key)
        {
            DataTable dt = new DataTable();
            string sql = "select COUNT(*) from case_file_master where proj_code = '" + proj_code + "' and bundle_key = '" + bundle_key + "' ";
            OdbcCommand cmd = new OdbcCommand(sql, sqlCon);
            OdbcDataAdapter odap = new OdbcDataAdapter(cmd);
            odap.Fill(dt);
            return dt.Rows[0][0].ToString();
        }

        public string _GetEntryCount(string proj_code, string bundle_key)
        {
            DataTable dt = new DataTable();
            string sql = "select COUNT(*) from metadata_entry where proj_code = '" + proj_code + "' and bundle_key = '" + bundle_key + "' ";
            OdbcCommand cmd = new OdbcCommand(sql, sqlCon);
            OdbcDataAdapter odap = new OdbcDataAdapter(cmd);
            odap.Fill(dt);
            return dt.Rows[0][0].ToString();
        }

        public DataTable _GetBundleStatus(string proj, string bundle)
        {
            DataTable dt = new DataTable();
            string sql = "select distinct status from bundle_master where proj_code = '" + proj + "' and bundle_key = '" + bundle + "' ";
            OdbcCommand cmd = new OdbcCommand(sql, sqlCon);
            OdbcDataAdapter odap = new OdbcDataAdapter(cmd);
            odap.Fill(dt);
            return dt;
        }

        private void dataEntryToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            //frmBatchSelect frm = new frmBatchSelect(sqlCon);
            //frm.ShowDialog(this);

            //frmFile fm = new frmFile(sqlCon);
            //fm.ShowDialog();

            NovaNet.wfe.eSTATES[] state = new NovaNet.wfe.eSTATES[2];
            //state[0] = NovaNet.wfe.eSTATES.POLICY_EXCEPTION;
            state[0] = NovaNet.wfe.eSTATES.METADATA_ENTRY;
            
            

            frmEntrySummary fm = new frmEntrySummary(sqlCon,crd,state);
            
            fm.ShowDialog(this);

            
            
            //if (projKey != null && bundleKey != null)
            //{
            //    Form activeChild = this.ActiveMdiChild;
            //    if (activeChild == null)
            //    {
            //        EntryForm frmEntry = new EntryForm();

            //        frmEntry.MdiParent = this;
                    
            //        frmEntry.Location = new Point(0, 24);
            //        frmEntry.Dock = DockStyle.Fill;
            //        frmEntry.Height = height;
            //        frmEntry.Width = width;
            //        frmEntry.Show();

            //    }
            //}
            
        }

        private void logoutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AssemblyName assemName = Assembly.GetExecutingAssembly().GetName();
            this.Text = "B'Zer - Tripura High Court" + "           Version: " + assemName.Version.ToString() + "    Database name: " + sqlCon.Database.ToString();
            sqlCon.Close();


            sqlCon.Open();
            
            menuStrip1.Visible = false;
            toolStrip1.Visible = false;
            rbc.UnLockedUser(crd.created_by.ToString());
            frmMain_Load(sender, e);

           


        }

        private void imageImportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmDataImport data = new frmDataImport(sqlCon, crd);
            data.ShowDialog(this);
        }

        private void exportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmExport export = new frmExport(sqlCon, crd);
            export.ShowDialog(this);
        }

        private void toolStripStatusLabel1_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("https://www.nevaehtech.com/");
        }

        private void batchUploadToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //frmBatchUpload frm = new frmBatchUpload(sqlCon);
            //frm.ShowDialog(this);


            frmBundleUpload frm = new frmBundleUpload(sqlCon,crd);
            frm.ShowDialog(this);
        }

        private void caseFileCreationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmBundleSummary frm = new frmBundleSummary(sqlCon,crd);
            frm.ShowDialog();
        }

        public void SetValues(wItem pBox, int prmMode)
        {
            wi = (wfeBox)pBox;
            colorMode = prmMode;
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            eSTATES[] state = new eSTATES[1];
            state[0] = eSTATES.POLICY_CREATED;
            frmBundleSelect box = new frmBundleSelect(state, sqlCon,txn,crd);
            box.chkPhotoScan.Visible = true;
            box.ShowDialog(this);

            projKey = frmBundleSelect.projKey;
            bundleKey = frmBundleSelect.bundleKey;
            if (projKey != null && bundleKey != null)
            {
                //status check
                if (_GetBundleStatus(projKey, bundleKey).Rows[0][0].ToString() == "1")
                {
                    //file count greater than one and file count and meta count matches
                    if (Convert.ToInt32(_GetFileCount(projKey, bundleKey).ToString()) > 0 && Convert.ToInt32(_GetFileCount(projKey, bundleKey).ToString()) == Convert.ToInt32(_GetEntryCount(projKey, bundleKey).ToString()))
                    {
                        aePolicyScan frmScan = new aePolicyScan(sqlCon, crd, colorMode);

                        frmScan.ShowDialog(this);
                    }
                }
            }
        }

        private void bundleScanToolStripMenuItem_Click(object sender, EventArgs e)
        {
            eSTATES[] state = new eSTATES[1];
            state[0] = eSTATES.POLICY_CREATED;
            frmBundleSelect box = new frmBundleSelect(state, sqlCon,txn,  crd);
            box.chkPhotoScan.Visible = true;
            box.ShowDialog(this);

            projKey = frmBundleSelect.projKey;
            bundleKey = frmBundleSelect.bundleKey;

            if(projKey != null && bundleKey != null)
            {
                //status check
                if(_GetBundleStatus(projKey,bundleKey).Rows[0][0].ToString() == "1")
                {
                    //file count greater than one and file count and meta count matches
                    if(Convert.ToInt32(_GetFileCount(projKey,bundleKey).ToString()) > 0 && Convert.ToInt32(_GetFileCount(projKey,bundleKey).ToString()) == Convert.ToInt32(_GetEntryCount(projKey,bundleKey).ToString()))
                    {
                        aePolicyScan frmScan = new aePolicyScan(sqlCon, crd, colorMode);
                        
                        frmScan.ShowDialog(this);
                    }
                }
            }
        }

        private void imageQualityControlToolStripMenuItem_Click(object sender, EventArgs e)
        {
            eSTATES[] state = new eSTATES[1];
            state[0] = eSTATES.POLICY_SCANNED;
            frmBundleSelect box = new frmBundleSelect(state, sqlCon,txn, crd);
            box.chkPhotoScan.Visible = false;
            box.ShowDialog(this);

            projKey = frmBundleSelect.projKey;
            bundleKey = frmBundleSelect.bundleKey;

            if (projKey != null && bundleKey != null)
            {
                //status check
                if (_GetBundleStatus(projKey, bundleKey).Rows[0][0].ToString() == "2")
                {
                    if (Convert.ToInt32(_GetFileCount(projKey, bundleKey).ToString()) > 0 && Convert.ToInt32(_GetFileCount(projKey, bundleKey).ToString()) == Convert.ToInt32(_GetEntryCount(projKey, bundleKey).ToString()))
                    {
                        aeImageQC frmQc = new aeImageQC(sqlCon, crd);
                        //frmQc.MdiParent = this;
                        //frmQc.Height = this.ClientRectangle.Height;
                        //frmQc.Width = this.ClientRectangle.Width;
                        frmQc.ShowDialog(this);
                    }
                }
            }
        }

        private void indexingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            eSTATES[] state = new eSTATES[2];
            state[0] = eSTATES.POLICY_QC;
            state[1] = eSTATES.POLICY_ON_HOLD;

            frmBundleSelect box = new frmBundleSelect(state, sqlCon,txn, crd);
            box.chkPhotoScan.Visible = false;
            box.ShowDialog(this);

            projKey = frmBundleSelect.projKey;
            bundleKey = frmBundleSelect.bundleKey;

            if (projKey != null && bundleKey != null)
            {
                //status check
                if (_GetBundleStatus(projKey, bundleKey).Rows[0][0].ToString() == "3" || _GetBundleStatus(projKey, bundleKey).Rows[0][0].ToString() == "2")
                {
                    if (Convert.ToInt32(_GetFileCount(projKey, bundleKey).ToString()) > 0 && Convert.ToInt32(_GetFileCount(projKey, bundleKey).ToString()) == Convert.ToInt32(_GetEntryCount(projKey, bundleKey).ToString()))
                    {
                        aeIndexing frmIndex = new aeIndexing(sqlCon, crd);

                        frmIndex.ShowDialog(this);
                    }
                }

            }


        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            eSTATES[] state = new eSTATES[1];
            state[0] = eSTATES.POLICY_SCANNED;
            frmBundleSelect box = new frmBundleSelect(state, sqlCon, txn, crd);
            box.chkPhotoScan.Visible = false;
            box.ShowDialog(this);

            projKey = frmBundleSelect.projKey;
            bundleKey = frmBundleSelect.bundleKey;

            if (projKey != null && bundleKey != null)
            {
                //status check
                if (_GetBundleStatus(projKey, bundleKey).Rows[0][0].ToString() == "2")
                {
                    if (Convert.ToInt32(_GetFileCount(projKey, bundleKey).ToString()) > 0 && Convert.ToInt32(_GetFileCount(projKey, bundleKey).ToString()) == Convert.ToInt32(_GetEntryCount(projKey, bundleKey).ToString()))
                    {
                        aeImageQC frmQc = new aeImageQC(sqlCon, crd);
                        
                        frmQc.ShowDialog(this);
                    }
                }
            }
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            eSTATES[] state = new eSTATES[2];
            state[0] = eSTATES.POLICY_QC;
            state[1] = eSTATES.POLICY_ON_HOLD;

            frmBundleSelect box = new frmBundleSelect(state, sqlCon,txn,  crd);
            box.chkPhotoScan.Visible = false;
            box.ShowDialog(this);

            projKey = frmBundleSelect.projKey;
            bundleKey = frmBundleSelect.bundleKey;

            if (projKey != null && bundleKey != null)
            {
                //status check
                if (_GetBundleStatus(projKey, bundleKey).Rows[0][0].ToString() == "3" || _GetBundleStatus(projKey, bundleKey).Rows[0][0].ToString() == "2")
                {
                    if (Convert.ToInt32(_GetFileCount(projKey, bundleKey).ToString()) > 0 && Convert.ToInt32(_GetFileCount(projKey, bundleKey).ToString()) == Convert.ToInt32(_GetEntryCount(projKey, bundleKey).ToString()))
                    {
                        aeIndexing frmIndex = new aeIndexing(sqlCon, crd);

                        frmIndex.ShowDialog(this);
                    }
                }
            }
        }

        private void qualityControlFinalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            NovaNet.wfe.eSTATES[] state = new NovaNet.wfe.eSTATES[2];
            //state[0] = NovaNet.wfe.eSTATES.POLICY_EXCEPTION;
            state[0] = NovaNet.wfe.eSTATES.POLICY_INDEXED;
            //state[2] = NovaNet.wfe.eSTATES.POLICY_FQC;
            state[1] = NovaNet.wfe.eSTATES.POLICY_ON_HOLD;
            //state[4] = NovaNet.wfe.eSTATES.POLICY_CHECKED;
            //state[5] = NovaNet.wfe.eSTATES.POLICY_NOT_INDEXED;
            //state[6] = NovaNet.wfe.eSTATES.POLICY_EXPORTED;
            //state[7] = NovaNet.wfe.eSTATES.POLICY_QC;
            //state[8] = NovaNet.wfe.eSTATES.POLICY_SUBMITTED;

            frmBundleSelect box = new frmBundleSelect(state, sqlCon, txn, crd);
            box.chkPhotoScan.Visible = false;
            box.ShowDialog(this);

            projKey = frmBundleSelect.projKey;
            bundleKey = frmBundleSelect.bundleKey;

            if (projKey != null && bundleKey != null)
            {
                //status check
                if (_GetBundleStatus(projKey, bundleKey).Rows[0][0].ToString() == "3" || _GetBundleStatus(projKey, bundleKey).Rows[0][0].ToString() == "2" || _GetBundleStatus(projKey, bundleKey).Rows[0][0].ToString() == "4" || _GetBundleStatus(projKey, bundleKey).Rows[0][0].ToString() == "5" || _GetBundleStatus(projKey, bundleKey).Rows[0][0].ToString() == "6" || _GetBundleStatus(projKey, bundleKey).Rows[0][0].ToString() == "7" || _GetBundleStatus(projKey, bundleKey).Rows[0][0].ToString() == "8" || _GetBundleStatus(projKey, bundleKey).Rows[0][0].ToString() == "30" || _GetBundleStatus(projKey, bundleKey).Rows[0][0].ToString() == "31" || _GetBundleStatus(projKey, bundleKey).Rows[0][0].ToString() == "37" || _GetBundleStatus(projKey, bundleKey).Rows[0][0].ToString() == "40" || _GetBundleStatus(projKey, bundleKey).Rows[0][0].ToString() == "77")
                {
                    if (Convert.ToInt32(_GetFileCount(projKey, bundleKey).ToString()) > 0 && Convert.ToInt32(_GetFileCount(projKey, bundleKey).ToString()) == Convert.ToInt32(_GetEntryCount(projKey, bundleKey).ToString()))
                    {
                        aeFQC frm = new aeFQC(sqlCon, crd);
                        frm.ShowDialog(this);
                    }
                }
                
            }
        }

        private void toolStripButton4_Click(object sender, EventArgs e)
        {
            NovaNet.wfe.eSTATES[] state = new NovaNet.wfe.eSTATES[2];
            //state[0] = NovaNet.wfe.eSTATES.POLICY_EXCEPTION;
            state[0] = NovaNet.wfe.eSTATES.POLICY_INDEXED;
            //state[2] = NovaNet.wfe.eSTATES.POLICY_FQC;
            state[1] = NovaNet.wfe.eSTATES.POLICY_ON_HOLD;
            //state[4] = NovaNet.wfe.eSTATES.POLICY_CHECKED;
            //state[5] = NovaNet.wfe.eSTATES.POLICY_NOT_INDEXED;
            //state[6] = NovaNet.wfe.eSTATES.POLICY_EXPORTED;
            //state[7] = NovaNet.wfe.eSTATES.POLICY_QC;
            //state[8] = NovaNet.wfe.eSTATES.POLICY_SUBMITTED;

            frmBundleSelect box = new frmBundleSelect(state, sqlCon,txn, crd);
            box.chkPhotoScan.Visible = false;
            box.ShowDialog(this);

            projKey = frmBundleSelect.projKey;
            bundleKey = frmBundleSelect.bundleKey;

            if (projKey != null && bundleKey != null)
            {
                //status check
                if (_GetBundleStatus(projKey, bundleKey).Rows[0][0].ToString() == "3" || _GetBundleStatus(projKey, bundleKey).Rows[0][0].ToString() == "2" || _GetBundleStatus(projKey, bundleKey).Rows[0][0].ToString() == "4" || _GetBundleStatus(projKey, bundleKey).Rows[0][0].ToString() == "5" || _GetBundleStatus(projKey, bundleKey).Rows[0][0].ToString() == "6" || _GetBundleStatus(projKey, bundleKey).Rows[0][0].ToString() == "7" || _GetBundleStatus(projKey, bundleKey).Rows[0][0].ToString() == "8" || _GetBundleStatus(projKey, bundleKey).Rows[0][0].ToString() == "30" || _GetBundleStatus(projKey, bundleKey).Rows[0][0].ToString() == "31" || _GetBundleStatus(projKey, bundleKey).Rows[0][0].ToString() == "37" || _GetBundleStatus(projKey, bundleKey).Rows[0][0].ToString() == "40" || _GetBundleStatus(projKey, bundleKey).Rows[0][0].ToString() == "77")
                {
                    if (Convert.ToInt32(_GetFileCount(projKey, bundleKey).ToString()) > 0 && Convert.ToInt32(_GetFileCount(projKey, bundleKey).ToString()) == Convert.ToInt32(_GetEntryCount(projKey, bundleKey).ToString()))
                    {
                        aeFQC frm = new aeFQC(sqlCon, crd);
                        frm.ShowDialog(this);
                    }
                }
            }
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            //frmSubmit frm = new frmSubmit(sqlCon, crd);
            //frm.ShowDialog(this);


            frmBundleSubmit frm = new frmBundleSubmit(sqlCon,crd);
            frm.ShowDialog(this);
        }

        private void dashboardToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
        }

        private void generateBarcodeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            eSTATES[] state = new eSTATES[1];
            state[0] = eSTATES.BARCODE_ENTRY;
            frmBundleSelect box = new frmBundleSelect(state, sqlCon, txn,crd);
            box.chkPhotoScan.Visible = true;
            box.ShowDialog(this);

            projKey = frmBundleSelect.projKey;
            bundleKey = frmBundleSelect.bundleKey;

            if (projKey != null && bundleKey != null)
            {
                //status check
                if (_GetBundleStatus(projKey, bundleKey).Rows[0][0].ToString() == "0" || _GetBundleStatus(projKey, bundleKey).Rows[0][0].ToString() == "1" || _GetBundleStatus(projKey, bundleKey).Rows[0][0].ToString() == "4" || _GetBundleStatus(projKey, bundleKey).Rows[0][0].ToString() == "5" || _GetBundleStatus(projKey, bundleKey).Rows[0][0].ToString() == "6" || _GetBundleStatus(projKey, bundleKey).Rows[0][0].ToString() == "7" || _GetBundleStatus(projKey, bundleKey).Rows[0][0].ToString() == "8" || _GetBundleStatus(projKey, bundleKey).Rows[0][0].ToString() == "30" || _GetBundleStatus(projKey, bundleKey).Rows[0][0].ToString() == "31" || _GetBundleStatus(projKey, bundleKey).Rows[0][0].ToString() == "37" || _GetBundleStatus(projKey, bundleKey).Rows[0][0].ToString() == "40" || _GetBundleStatus(projKey, bundleKey).Rows[0][0].ToString() == "77")
                {
                    //file count greater than one and file count and meta count matches
                    if (Convert.ToInt32(_GetFileCount(projKey, bundleKey).ToString()) > 0 )
                    {
                        frmBarcode frmB = new frmBarcode(sqlCon,crd);

                        frmB.ShowDialog(this);
                    }
                }
            }
        }

        private void generateBarcodeToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            
            frmBarcode frmB = new frmBarcode(sqlCon, crd);

            frmB.ShowDialog(this);

        }

        private void judgeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
        }

        private void dashboardToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            frmJobDistribution frmjob = new frmJobDistribution(sqlCon);
            frmjob.ShowDialog(this);
        }

        private void productionReportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmProduction frm = new frmProduction(sqlCon,crd);
            frm.ShowDialog(this);
        }

        private void siteReportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmSite frm = new frmSite(sqlCon);
            frm.ShowDialog(this);
        }

        private void judgeCreationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmJudge frm = new frmJudge(sqlCon, crd);
            frm.ShowDialog(this);
        }

        private void districtCreationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmDistrict frm = new frmDistrict(sqlCon, crd);
            frm.ShowDialog(this);
        }

        private void configurationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            aeConfiguration csvUploader = new aeConfiguration();
            mainForm = new frmMain();
            csvUploader.ShowDialog(mainForm);
        }

        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            frmPathConfig frm = new frmPathConfig(sqlCon);
            frm.ShowDialog(this);
        }

        private void scanDataTransferToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmDataTransfer frmData = new frmDataTransfer(sqlCon, crd);
            frmData.ShowDialog(this);
        }

        private void toolStripMenuItem3_Click(object sender, EventArgs e)
        {
            frmTriggerReport frm = new frmTriggerReport(sqlCon, crd);
            frm.ShowDialog(this);
        }

       
        private void shortcutKeysToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            About frm = new About();
            frm.ShowDialog(this);
        }

        private void auditToolStripMenuItem_Click(object sender, EventArgs e)
        {
            aeLicQa frm = new aeLicQa(sqlCon,crd);
            frm.ShowDialog(this);
        }

        private void userWiseReportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmUserReport frm = new frmUserReport(sqlCon);
            frm.ShowDialog(this);
        }

        private void outwardSubmissionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmOutwardSubmission frm = new frmOutwardSubmission(sqlCon, crd);
            frm.ShowDialog(this);
        }

        private void userWiseCountReportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmUserCountReport frm = new frmUserCountReport(sqlCon);
            frm.ShowDialog(this);
        }

        private void outwardReportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmOutwardReport frm = new frmOutwardReport(sqlCon);
            frm.ShowDialog(this);
        }

        private void uATCertificateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmUAT frm = new frmUAT(sqlCon);
            frm.ShowDialog(this);
        }
    }
}
