﻿namespace ImageHeaven
{
    partial class frmEntrySummary
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmEntrySummary));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            this.panel1 = new System.Windows.Forms.Panel();
            this.deLabel1 = new nControls.deLabel();
            this.cmdnew = new nControls.deButton();
            this.cmdReset = new nControls.deButton();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.dtGrdVol = new nControls.deDataGridView();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.cmdSearch = new nControls.deButton();
            this.textBox1 = new nControls.deTextBox();
            this.textBox2 = new nControls.deTextBox();
            this.deLabel3 = new nControls.deLabel();
            this.deLabel2 = new nControls.deLabel();
            this.cmsDeeds = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.updateDeedToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.deleteDeedToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.panel1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtGrdVol)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.cmsDeeds.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.deLabel1);
            this.panel1.Controls.Add(this.cmdnew);
            this.panel1.Controls.Add(this.cmdReset);
            this.panel1.Controls.Add(this.groupBox2);
            this.panel1.Controls.Add(this.groupBox1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(578, 568);
            this.panel1.TabIndex = 0;
            // 
            // deLabel1
            // 
            this.deLabel1.AutoSize = true;
            this.deLabel1.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.deLabel1.Location = new System.Drawing.Point(140, 536);
            this.deLabel1.Name = "deLabel1";
            this.deLabel1.Size = new System.Drawing.Size(0, 15);
            this.deLabel1.TabIndex = 8;
            // 
            // cmdnew
            // 
            this.cmdnew.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("cmdnew.BackgroundImage")));
            this.cmdnew.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.cmdnew.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.cmdnew.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmdnew.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdnew.Location = new System.Drawing.Point(455, 527);
            this.cmdnew.Name = "cmdnew";
            this.cmdnew.Size = new System.Drawing.Size(115, 33);
            this.cmdnew.TabIndex = 6;
            this.cmdnew.Text = "&New Entry";
            this.cmdnew.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.cmdnew.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.cmdnew.UseCompatibleTextRendering = true;
            this.cmdnew.UseVisualStyleBackColor = true;
            this.cmdnew.Click += new System.EventHandler(this.cmdnew_Click);
            // 
            // cmdReset
            // 
            this.cmdReset.BackColor = System.Drawing.SystemColors.Control;
            this.cmdReset.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("cmdReset.BackgroundImage")));
            this.cmdReset.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.cmdReset.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.cmdReset.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmdReset.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdReset.Location = new System.Drawing.Point(8, 527);
            this.cmdReset.Name = "cmdReset";
            this.cmdReset.Size = new System.Drawing.Size(96, 33);
            this.cmdReset.TabIndex = 5;
            this.cmdReset.Text = "&Reset";
            this.cmdReset.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.cmdReset.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.cmdReset.UseCompatibleTextRendering = true;
            this.cmdReset.UseVisualStyleBackColor = false;
            this.cmdReset.Click += new System.EventHandler(this.cmdReset_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.dtGrdVol);
            this.groupBox2.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox2.Location = new System.Drawing.Point(3, 58);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(573, 464);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Entry Details ";
            // 
            // dtGrdVol
            // 
            this.dtGrdVol.AllowUserToAddRows = false;
            this.dtGrdVol.AllowUserToDeleteRows = false;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.GradientActiveCaption;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.Color.White;
            this.dtGrdVol.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dtGrdVol.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dtGrdVol.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.dtGrdVol.BackgroundColor = System.Drawing.SystemColors.ControlDark;
            this.dtGrdVol.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dtGrdVol.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.ControlDark;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlDark;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.WhiteSmoke;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dtGrdVol.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dtGrdVol.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.GradientActiveCaption;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dtGrdVol.DefaultCellStyle = dataGridViewCellStyle3;
            this.dtGrdVol.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dtGrdVol.Location = new System.Drawing.Point(3, 25);
            this.dtGrdVol.MultiSelect = false;
            this.dtGrdVol.Name = "dtGrdVol";
            this.dtGrdVol.ReadOnly = true;
            this.dtGrdVol.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.Color.White;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dtGrdVol.RowHeadersDefaultCellStyle = dataGridViewCellStyle4;
            this.dtGrdVol.RowHeadersVisible = false;
            this.dtGrdVol.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.AutoSizeToAllHeaders;
            this.dtGrdVol.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dtGrdVol.Size = new System.Drawing.Size(567, 436);
            this.dtGrdVol.StandardTab = true;
            this.dtGrdVol.TabIndex = 4;
            this.dtGrdVol.DoubleClick += new System.EventHandler(this.dtGrdVol_DoubleClick);
            this.dtGrdVol.KeyUp += new System.Windows.Forms.KeyEventHandler(this.dtGrdVol_KeyUp);
            this.dtGrdVol.MouseClick += new System.Windows.Forms.MouseEventHandler(this.dtGrdVol_MouseClick);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.cmdSearch);
            this.groupBox1.Controls.Add(this.textBox1);
            this.groupBox1.Controls.Add(this.textBox2);
            this.groupBox1.Controls.Add(this.deLabel3);
            this.groupBox1.Controls.Add(this.deLabel2);
            this.groupBox1.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(3, 2);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(571, 57);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Search Criteria";
            // 
            // cmdSearch
            // 
            this.cmdSearch.BackColor = System.Drawing.SystemColors.Control;
            this.cmdSearch.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("cmdSearch.BackgroundImage")));
            this.cmdSearch.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.cmdSearch.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.cmdSearch.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmdSearch.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdSearch.Location = new System.Drawing.Point(466, 18);
            this.cmdSearch.Name = "cmdSearch";
            this.cmdSearch.Size = new System.Drawing.Size(94, 31);
            this.cmdSearch.TabIndex = 3;
            this.cmdSearch.Text = "&Search";
            this.cmdSearch.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.cmdSearch.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.cmdSearch.UseCompatibleTextRendering = true;
            this.cmdSearch.UseVisualStyleBackColor = false;
            this.cmdSearch.Click += new System.EventHandler(this.cmdSearch_Click);
            // 
            // textBox1
            // 
            this.textBox1.BackColor = System.Drawing.Color.White;
            this.textBox1.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox1.ForeColor = System.Drawing.Color.Black;
            this.textBox1.Location = new System.Drawing.Point(115, 23);
            this.textBox1.Mandatory = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(131, 23);
            this.textBox1.TabIndex = 1;
            // 
            // textBox2
            // 
            this.textBox2.BackColor = System.Drawing.Color.White;
            this.textBox2.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox2.ForeColor = System.Drawing.Color.Black;
            this.textBox2.Location = new System.Drawing.Point(330, 23);
            this.textBox2.Mandatory = true;
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(108, 23);
            this.textBox2.TabIndex = 2;
            // 
            // deLabel3
            // 
            this.deLabel3.AutoSize = true;
            this.deLabel3.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.deLabel3.Location = new System.Drawing.Point(257, 27);
            this.deLabel3.Name = "deLabel3";
            this.deLabel3.Size = new System.Drawing.Size(69, 15);
            this.deLabel3.TabIndex = 16;
            this.deLabel3.Text = "Bundle No :";
            // 
            // deLabel2
            // 
            this.deLabel2.AutoSize = true;
            this.deLabel2.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.deLabel2.Location = new System.Drawing.Point(25, 27);
            this.deLabel2.Name = "deLabel2";
            this.deLabel2.Size = new System.Drawing.Size(87, 15);
            this.deLabel2.TabIndex = 15;
            this.deLabel2.Text = "Establishment :";
            // 
            // cmsDeeds
            // 
            this.cmsDeeds.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.updateDeedToolStripMenuItem,
            this.deleteDeedToolStripMenuItem});
            this.cmsDeeds.Name = "cmsDeeds";
            this.cmsDeeds.Size = new System.Drawing.Size(153, 48);
            // 
            // updateDeedToolStripMenuItem
            // 
            this.updateDeedToolStripMenuItem.Name = "updateDeedToolStripMenuItem";
            this.updateDeedToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.updateDeedToolStripMenuItem.Text = "&Update Bundle";
            this.updateDeedToolStripMenuItem.Click += new System.EventHandler(this.updateDeedToolStripMenuItem_Click);
            // 
            // deleteDeedToolStripMenuItem
            // 
            this.deleteDeedToolStripMenuItem.Name = "deleteDeedToolStripMenuItem";
            this.deleteDeedToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.deleteDeedToolStripMenuItem.Text = "&Delete Bundle";
            this.deleteDeedToolStripMenuItem.Click += new System.EventHandler(this.deleteDeedToolStripMenuItem_Click);
            // 
            // frmEntrySummary
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(578, 568);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmEntrySummary";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Entry Summary";
            this.Load += new System.EventHandler(this.frmEntrySummary_Load);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.frmEntrySummary_KeyUp);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dtGrdVol)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.cmsDeeds.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private nControls.deDataGridView dtGrdVol;
        private nControls.deTextBox textBox1;
        private nControls.deTextBox textBox2;
        private nControls.deLabel deLabel3;
        private nControls.deLabel deLabel2;
        private nControls.deButton cmdSearch;
        private nControls.deButton cmdnew;
        private nControls.deButton cmdReset;
        private nControls.deLabel deLabel1;
        private System.Windows.Forms.ContextMenuStrip cmsDeeds;
        private System.Windows.Forms.ToolStripMenuItem updateDeedToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem deleteDeedToolStripMenuItem;
    }
}