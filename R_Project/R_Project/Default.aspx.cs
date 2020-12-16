using RDotNet;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Runtime.ExceptionServices;
using System.Runtime.InteropServices;
using System.Security;



namespace R_Project
{
    public partial class _Default : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            var scriptFilePath = "C:/dev/R/test_script.R";
            var riskCsvPath = "C:/dev/R/test_data.csv";
            var valueAtRisk = "25750000000";
            //ExecuteScriptFile(scriptFilePath, riskCsvPath, valueAtRisk);
        }

        //static void RTest()
        //{
        //    var envPath = Environment.GetEnvironmentVariable("PATH");
        //    var rBinPath = System.Environment.Is64BitProcess ? @"C:\Program Files\R\R-3.0.1\bin\x64" : @"C:\Program Files\R\R-3.0.1\bin\i386";
        //    Environment.SetEnvironmentVariable("PATH", envPath + Path.PathSeparator + rBinPath);

        //    using (var engine = REngine.CreateInstance("RDotNet"))
        //    {
        //        engine.Initialize();

        //        using (var fs = File.OpenRead(@"C:\R-scripts\r-test.R"))
        //        {
        //            engine.Evaluate(fs);
        //        }
        //    }
        //}
        //public static void ExecuteScriptFile(string scriptFilePath, string paramForScript1, string paramForScript2)
        //{
        //    using (var en = REngine.GetInstance())
        //    {
        //        var args_r = new string[2] { paramForScript1, paramForScript2 };
        //        var execution = "source('" + scriptFilePath + "')";
        //        en.SetCommandLineArguments(args_r);
        //        en.Evaluate(execution);
        //    }
        //}


        private static double EvaluateExpression(REngine engine, string expression)
        {
            var expressionVector = engine.CreateCharacterVector(new[] { expression });
            engine.SetSymbol("expr", expressionVector);

            //  WRONG -- Need to parse to expression before evaluation
            //var result = engine.Evaluate("eval(expr)");

            //  RIGHT way to do this!!!
            var result = engine.Evaluate("eval(parse(text=expr))");
            var ret = result.AsNumeric().First();

            return ret;
        }

        public static void SetupPath(string Rversion = "R-3.2.1")
        {
            var oldPath = System.Environment.GetEnvironmentVariable("PATH");
            var rPath = System.Environment.Is64BitProcess ?
                                   string.Format(@"C:\Program Files\R\R-2.13.0\bin\x64", Rversion) :
                                   string.Format(@"C:\Program Files\R\R-2.13.0\bin\i386", Rversion);

            if (!Directory.Exists(rPath))
                throw new DirectoryNotFoundException(
                  string.Format(" R.dll not found in : {0}", rPath));
            var newPath = string.Format("{0}{1}{2}", rPath,
                                         System.IO.Path.PathSeparator, oldPath);
            System.Environment.SetEnvironmentVariable("PATH", newPath);
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            try
            {
                SetupPath();
                REngine.SetEnvironmentVariables();

                REngine engine = REngine.GetInstance();
                // REngine requires explicit initialization.
                // You can set some parameters.
                engine.Initialize();
                 //string path = Server.MapPath("~/R Script/1.R");
                //string path = @"D:\R_Script\1.R";
                //engine.Evaluate("source('" + path + "')");
                engine.Evaluate("C<-sum(1,2,3,4)");
                //string a = EvaluateExpression(engine, "C").ToString();
                Label1.Text = EvaluateExpression(engine, "C").ToString();
            }
            catch(Exception ex)
            {
                string a = ex.Message.ToString();
            }
        }

        //protected void Button1_Click(object sender, EventArgs e)
        //{
        //    SetupPath();
        //    REngine.SetEnvironmentVariables();

        //    REngine engine = REngine.GetInstance();
        //    // REngine requires explicit initialization.
        //    // You can set some parameters.
        //    engine.Initialize();
        //    string path = Server.MapPath("~/R Script/rscript.r");
        //    engine.Evaluate("source('" + path + "')");
        //   string a = EvaluateExpression(engine, "C").ToString();
        //    //Label1.Text = EvaluateExpression(engine, "C").ToString();
        //}
    }
}
