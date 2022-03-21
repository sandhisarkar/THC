﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using NovaNet;
using NovaNet.Utils;

namespace CHC
{
    internal sealed class Program
    {
        public static NovaNet.Utils.exLog.Logger exMailLog = new NovaNet.Utils.exLog.emailLogger("./errLog.log", NovaNet.Utils.exLog.LogLevel.Dev, Constants._MAIL_TO, Constants._MAIL_FROM, Constants._SMTP);
        public static NovaNet.Utils.exLog.Logger exTxtLog = new NovaNet.Utils.exLog.txtLogger("./errLog.log", NovaNet.Utils.exLog.LogLevel.Dev);
        /// <summary>
        /// Program entry point.
        /// </summary>
        [STAThread]
        private static void Main(string[] args)
        {

            try
            {

                System.Diagnostics.Process.GetCurrentProcess().PriorityClass = System.Diagnostics.ProcessPriorityClass.RealTime;
                exMailLog.SetNextLogger(exTxtLog);
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                //ImageHeaven.Program.IHMain(args);

                Start(args);
            }
            catch (Exception ex)
            {
                exMailLog.Log(ex);
            }
        }

        public static void Start(string[] args)   // <-- must be marked public!
        {


            do
            {
                
                ImageHeaven.Program.IHMain(args);

            }

            while (ImageHeaven.Program.LogOut() == true);

        }
    }
}
