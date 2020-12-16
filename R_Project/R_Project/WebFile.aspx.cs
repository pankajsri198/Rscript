using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using RDotNet;

using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Runtime.ExceptionServices;
using System.Runtime.InteropServices;
using System.Security;


using System.Diagnostics;
using System.IO;


namespace R_Project
{
    public partial class WebFile : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //var scriptFilePath = "C:/dev/R/test_script.R";
            //var riskCsvPath = "C:/dev/R/test_data.csv";
            //var valueAtRisk = "25750000000";
            //// ExecuteScriptFile(scriptFilePath, riskCsvPath, valueAtRisk);
            //var path = @"D:\R_Script\maxr.R";
            //ExecuteScriptFile(path, "", "");
        }

        public void ExecuteScriptFile(string scriptFilePath, string paramForScript1, string paramForScript2)
        {
            using (var en = REngine.GetInstance())
            {
                //var args_r = new string[2] { paramForScript1, paramForScript2 };
                var execution = "source('" + scriptFilePath + "')";
                // en.SetCommandLineArguments(args_r);
                en.Evaluate(execution);
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            var path = Server.MapPath("~/R_Script/maxr.r");  
            string result = null;
            //result = RScriptRunner.RunFromCmd(path + @"\maxr.r", @"C:\Program Files\R\R-2.13.0\bin\rscript.exe", "3");
            string Exepath = Server.MapPath("~/R/R-2.13.0/bin/rscript.exe");
            result = RScriptRunner.RunFromCmd(path , Exepath, "3");           
            lblOutput.Text = result.ToString();
        }



        public static string RunFromCmd(string rCodeFilePath, string rScriptExecutablePath, string args)
        {
            string file = rCodeFilePath;
            string result = string.Empty;

            try
            {

                var info = new ProcessStartInfo();
                info.FileName = rScriptExecutablePath;
                info.WorkingDirectory = Path.GetDirectoryName(rScriptExecutablePath);
                info.Arguments = rCodeFilePath + " " + args;

                info.RedirectStandardInput = false;
                info.RedirectStandardOutput = true;
                info.UseShellExecute = false;
                info.CreateNoWindow = true;

                using (var proc = new Process())
                {
                    proc.StartInfo = info;
                    proc.Start();
                    result = proc.StandardOutput.ReadToEnd();

                }

                return result;
            }
            catch (Exception ex)
            {
                throw new Exception("R Script failed: " + result, ex);
            }
        }
    }
}
