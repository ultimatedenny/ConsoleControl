using ReaLTaiizor.Forms;
using ReaLTaiizor.Manager;
using System.Windows.Forms;
using ReaLTaiizor.Controls;
using System.Data;
using System.Data.SqlClient;
using System;
using System.Diagnostics;
using System.Configuration;
using System.Timers;
using System.Text.RegularExpressions;
using System.IO;
using System.Text;
using System.Drawing;
using System.Reflection;
using System.Runtime.InteropServices;


namespace ConsoleControlSample
{
    public partial class Form1 : PoisonForm
    {
        private readonly MaterialSkinManager MM;
        DataTable DT = new DataTable();
        DataTable RES_QUEUE = new DataTable();
        DataTable RES_PROCESS = new DataTable();
        DataTable COMMAND = new DataTable();
        public static string SERVICES_ID = "";
        private static System.Timers.Timer timer;

        public Form1()
        {
            InitializeComponent();
            
            Assembly assembly = Assembly.GetExecutingAssembly();
            AssemblyTitleAttribute titleAttribute = (AssemblyTitleAttribute)Attribute.GetCustomAttribute(assembly, typeof(AssemblyTitleAttribute));
            AssemblyDescriptionAttribute descriptionAttribute = (AssemblyDescriptionAttribute)Attribute.GetCustomAttribute(assembly, typeof(AssemblyDescriptionAttribute));
            AssemblyCompanyAttribute companyAttribute = (AssemblyCompanyAttribute)Attribute.GetCustomAttribute(assembly, typeof(AssemblyCompanyAttribute));
            AssemblyProductAttribute productAttribute = (AssemblyProductAttribute)Attribute.GetCustomAttribute(assembly, typeof(AssemblyProductAttribute));
            AssemblyCopyrightAttribute copyrightAttribute = (AssemblyCopyrightAttribute)Attribute.GetCustomAttribute(assembly, typeof(AssemblyCopyrightAttribute));
            AssemblyTrademarkAttribute trademarkAttribute = (AssemblyTrademarkAttribute)Attribute.GetCustomAttribute(assembly, typeof(AssemblyTrademarkAttribute));

            // Get Version Information
            Version assemblyVersion = assembly.GetName().Version;
            AssemblyFileVersionAttribute fileVersionAttribute = (AssemblyFileVersionAttribute)Attribute.GetCustomAttribute(assembly, typeof(AssemblyFileVersionAttribute));
            GuidAttribute guidAttribute = (GuidAttribute)Attribute.GetCustomAttribute(assembly, typeof(GuidAttribute));
            SERVICES_ID = $"{guidAttribute?.Value.ToUpper()}";
            poisonLabel2.Text = productAttribute?.Product + "\n" + assemblyVersion;
            poisonLabel5.Text =
            $"Title: {titleAttribute?.Title}\n" +
            $"Description: {descriptionAttribute?.Description}\n" +
            $"Company: {companyAttribute?.Company}\n" +
            $"Product: {productAttribute?.Product}\n" +
            $"Copyright: {DateTime.Now:yyyy} {copyrightAttribute?.Copyright} DENI ANDREAN\n" +
            $"Trademark: {trademarkAttribute?.Trademark}\n" +
            $"Assembly Version:  {assemblyVersion}\n" +
            $"File Version: {fileVersionAttribute?.Version}\n" +
            $"GUID / Services ID: {guidAttribute?.Value.ToUpper()}";

            poisonLabel1.Text = "Status: Online, Last Update:" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
           
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            LogToDatabase(1, $"Agent started");
            timer = new System.Timers.Timer(5000);
            timer.Elapsed += OnTimerElapsed;
            timer.AutoReset = true;
            timer.Start();
        }

        //  The NotifyIcon object

        //this.notifyIcon1.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIcon1.Icon")));

        private void TrayMinimizerForm_Resize(object sender, EventArgs e)
        {
            notifyIcon1.BalloonTipTitle = "Minimize to Tray App";
            notifyIcon1.BalloonTipText = "You have successfully minimized your form.";
            if (FormWindowState.Minimized == this.WindowState)
            {
                notifyIcon1.Visible = true;
                notifyIcon1.ShowBalloonTip(500);
                this.Hide();
            }
            else if (FormWindowState.Normal == this.WindowState)
            {
                notifyIcon1.Visible = false;
            }
        } 

        private void notifyIcon1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            this.Show();
            this.WindowState = FormWindowState.Normal;
        }

        public void GET_QUEUE_LIST()
        {
            RES_QUEUE = GET_QUEUE_LIST_SERVICES();
            Invoke((MethodInvoker)delegate {
                poisonLabel3.Text = "Queue List (" + RES_QUEUE.Rows.Count.ToString() + ")";
            });
            flowLayoutPanel1.Invoke(new Action(() =>
            {
                flowLayoutPanel1.Controls.Clear();
                foreach (DataRow row in RES_QUEUE.Rows)
                {
                    string Schedule = row["IdSchedule"].ToString();
                    string IdJob = row["IdJob"].ToString();
                    string idRpa = row["IdRpa"].ToString();
                    string status = row["Status"].ToString();
                    string TaskName = row["TaskName"].ToString();
                    string ScheduledOn = row["ScheduledOn"].ToString();

                    PoisonButton PB = new PoisonButton()
                    {
                        Text = $@"[{status}]
{TaskName}
{IdJob}
{Schedule} - {ScheduledOn}
",
                        BackColor = Color.WhiteSmoke,
                        ForeColor = Color.DimGray,
                        TextAlign = ContentAlignment.TopLeft,
                        Width = 435,
                        Height = 65,
                        Anchor = AnchorStyles.Left | AnchorStyles.Top | AnchorStyles.Right,
                    };
                    flowLayoutPanel1.Controls.Add(PB);
                }
            }));
            if(RES_QUEUE.Rows.Count > 0)
            {
                GET_PROCESS_LIST();
            }
        }

        public void GET_PROCESS_LIST()
        {
            RES_PROCESS = ADD_PROCESS_LIST_SERVICES();
            flowLayoutPanel2.Invoke(new Action(() =>
            {
                flowLayoutPanel2.Controls.Clear();
                foreach (DataRow row in RES_PROCESS.Rows)
                {
                    string ID = row["ID"].ToString();
                    string IDRPA = row["IDRPA"].ToString();
                    string IDJOB = row["IDJOB"].ToString();
                    string IDSCHEDULE = row["IDSCHEDULE"].ToString();
                    string IDTASK = row["IDTASK"].ToString();
                    string DEPARTMENT = row["DEPARTMENT"].ToString();
                    string PLANT = row["PLANT"].ToString();
                    string STARTRUN = row["STARTRUN"].ToString();
                    string ENDRUN = row["ENDRUN"].ToString();
                    string MESSAGE = row["MESSAGE"].ToString();
                    string STATUS = row["STATUS"].ToString();

                    PoisonButton PC = new PoisonButton()
                    {
                        Text = $@"[{STATUS}]
{IDTASK}
{IDJOB}
{STARTRUN} - {ENDRUN}
",
                        BackColor = Color.WhiteSmoke,
                        ForeColor = Color.DimGray,
                        TextAlign = ContentAlignment.TopLeft,
                        Width = 435,
                        Height = 65,
                        Anchor = AnchorStyles.Left | AnchorStyles.Top | AnchorStyles.Right,
                    };
                    flowLayoutPanel2.Controls.Add(PC);
                }
            }));
            RUN_PROCESS();
        }

        object[] valuesArray;
        string RPA_JOB_ID = "";
        string JOB_ID = "";
        string QUEUE_ID = "";
        string pattern = @"\((.*?)\)";

        public void RUN_PROCESS()
        {
            if(RES_QUEUE.Rows.Count> 0)
            {
                for (int i = 0; i < RES_QUEUE.Rows.Count; i++)
                {
                    timer.Stop();
                    DataRow ROW1 = RES_QUEUE.Rows[i];
                    valuesArray = ROW1.ItemArray;
                    string ID = valuesArray[0].ToString();
                    string IDSCHEDULE = valuesArray[1].ToString();
                    string IDJOB = valuesArray[2].ToString();
                    string IDRPA = valuesArray[3].ToString();
                    string TASKNAME = valuesArray[4].ToString();
                    string SCHEDULEON = valuesArray[5].ToString();
                    string STATUS = valuesArray[6].ToString();
                    string CREATEDATE = valuesArray[7].ToString();
                    string CREATEBY = valuesArray[8].ToString();

                    if (Convert.ToDateTime(SCHEDULEON) <= DateTime.Now)
                    {
                        if (RES_PROCESS.Rows.Count > 0)
                        {
                            for (int j = 0; j < RES_PROCESS.Rows.Count; j++)
                            {
                                timer.Stop();
                                DataRow firstRow = RES_PROCESS.Rows[j];
                                valuesArray = firstRow.ItemArray;
                                RPA_JOB_ID = valuesArray[0].ToString();
                                JOB_ID = "";
                                QUEUE_ID = "";
                                pattern = @"\((.*?)\)";
                                Match JOB_M = Regex.Match(valuesArray[2].ToString(), pattern);
                                Match QUEUE_M = Regex.Match(valuesArray[4].ToString(), pattern);
                                if (JOB_M.Success)
                                {
                                    JOB_ID = JOB_M.Groups[1].Value;
                                }
                                if (QUEUE_M.Success)
                                {
                                    QUEUE_ID = QUEUE_M.Groups[1].Value;
                                }
                                COMMAND = GET_COMMAND(RPA_JOB_ID, JOB_ID, QUEUE_ID);
                                string batchFilePath = @"C:\BATFOLDER\GeneratedBatchFile.bat";
                                CREATE_BAT_FILE(COMMAND, batchFilePath);
                                EXECUTE_BATCH_FILE(batchFilePath);
                                if (File.Exists(batchFilePath))
                                {
                                    File.Delete(batchFilePath);
                                }
                            }
                            
                        }
                    }
                    else
                    {
                        flowLayoutPanel2.Invoke(new Action(() =>
                        {
                            foreach (Control control in flowLayoutPanel2.Controls)
                            {
                                control.Dispose();
                            }
                            flowLayoutPanel2.Controls.Clear();
                        }));
                    }
                    timer.Start();
                }
            }
            //if (RES_PROCESS.Rows.Count > 0)
            //{
            //    timer.Stop();
            //    DataRow firstRow = RES_PROCESS.Rows[0];
            //    valuesArray = firstRow.ItemArray;
            //    RPA_JOB_ID = valuesArray[0].ToString();
            //    JOB_ID = "";
            //    QUEUE_ID = "";
            //    pattern = @"\((.*?)\)";
            //    Match JOB_M = Regex.Match(valuesArray[2].ToString(), pattern);
            //    Match QUEUE_M = Regex.Match(valuesArray[4].ToString(), pattern);
            //    if (JOB_M.Success)
            //    {
            //        JOB_ID = JOB_M.Groups[1].Value;
            //    }
            //    if (QUEUE_M.Success)
            //    {
            //        QUEUE_ID = QUEUE_M.Groups[1].Value;
            //    }
            //    COMMAND = GET_COMMAND(RPA_JOB_ID, JOB_ID, QUEUE_ID);
            //    string batchFilePath = @"C:\BATFOLDER\GeneratedBatchFile.bat";
            //    CREATE_BAT_FILE(COMMAND, batchFilePath);
            //    EXECUTE_BATCH_FILE(batchFilePath);
            //    //timer.Start();
            //}
            //else
            //{

            //}
        }

        public void CREATE_BAT_FILE(DataTable dataTable, string batchFilePath)
        {
            try
            {
                StringBuilder batchContent = new StringBuilder();
                foreach (DataRow row in dataTable.Rows)
                {
                    string columnValue = row["COMMAND"].ToString();
                    batchContent.AppendLine($"{columnValue}");
                }
                File.WriteAllText(batchFilePath, batchContent.ToString());
                LogToDatabase(1, $"CREATE_BAT_FILE: SUCCESS");
            }
            catch (Exception ex)
            {
                LogToDatabase(1, $"CREATE_BAT_FILE: {ex.Message}");
            }
        }

        public void EXECUTE_BATCH_FILE(string batchFilePath)
        {
            StreamReader outputReader = null;
            StreamReader errorReader = null;
            try
            {
                ProcessStartInfo processStartInfo =
                new ProcessStartInfo(batchFilePath, "")
                {
                    ErrorDialog = false,
                    UseShellExecute = false,
                    RedirectStandardError = true,
                    RedirectStandardInput = true,
                    RedirectStandardOutput = true,
                    CreateNoWindow = false
                };
                Process process = new Process
                {
                    StartInfo = processStartInfo
                };
                bool processStarted = process.Start();
                if (processStarted)
                {
                    outputReader = process.StandardOutput;
                    errorReader = process.StandardError;
                    var Output = "";
                    var Error = "";
                    process.WaitForExit();
                    Output = outputReader.ReadToEnd();
                    Error = errorReader.ReadToEnd();

                    string displayText = "OUTPUT" + Environment.NewLine + "=" + Environment.NewLine;
                    displayText += "ERROR" + Environment.NewLine + "=" + Environment.NewLine;
                    displayText += Error;

                    if (string.IsNullOrEmpty(Error))
                    {
                        POST_FINISH_TASK(RPA_JOB_ID, JOB_ID, QUEUE_ID, "-", "COMPLETED");
                        LogToDatabase(1, $"EXECUTE_BATCH_FILE: {displayText.ToUpper()}");
                    }
                    else
                    {
                        POST_FINISH_TASK(RPA_JOB_ID, JOB_ID, QUEUE_ID, Error, "TERMINATED");
                        LogToDatabase(0, $"EXECUTE_BATCH_FILE: {displayText.ToUpper()}");
                    }
                }
            }
            catch (Exception ex)
            {
                LogToDatabase(1, $"EXECUTE_BATCH_FILE: {ex.Message}");
            }
            finally
            {
                if (outputReader != null)
                {
                    outputReader.Close();
                }
                if (errorReader != null)
                {
                    errorReader.Close();
                }
            }
        }
        
        static void SetupEventLog()
        {
            //EventLog eventLog1 = new EventLog();
            //if (!EventLog.SourceExists("SBMRPALogSource"))
            //{
            //    EventLog.CreateEventSource("SBMRPALogSource", "SBMRPALog");
            //}
            //eventLog1.Source = "SBMRPALogSource";
            //eventLog1.Log = "SBMRPALog";
        }

        static void LogToDatabase(int type, string message)
        {
            try
            {
                string connectionString = ConfigurationManager.AppSettings["DBConn"];
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    string query = @"INSERT INTO DB_RPA_SERVICES.DBO.RPA_SERVICES_LOG 
                                (ID, MACHINE, TIME, TYPE, MESSAGE) 
                                VALUES 
                                (@ID, @Machine, GETDATE(), @Type, @Message)";
                    string types = "Error";
                    if (type == 1)
                    {
                        types = "Information";
                    }
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@ID", SERVICES_ID);
                        command.Parameters.AddWithValue("@Machine", Environment.MachineName);
                        command.Parameters.AddWithValue("@Type", types);
                        command.Parameters.AddWithValue("@Message", message);

                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                LogToLocal(0, ex.Message.ToString());
            }
        }

        static void LogToLocal(int type, string message)
        {
            //EventLog eventLog1 = new EventLog();
            //SetupEventLog();
            //if (type == 0)
            //{
            //    eventLog1.WriteEntry(message, EventLogEntryType.Error);
            //}
            //else if (type == 1)
            //{
            //    eventLog1.WriteEntry(message, EventLogEntryType.Information);
            //}
            //else
            //{
            //    eventLog1.WriteEntry(message, EventLogEntryType.Error);
            //}
        }

        public void UpdateStatus(string STATUS)
        {
            try
            {
                string connectionString = ConfigurationManager.AppSettings["DBConn"];

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    using (SqlTransaction transaction = connection.BeginTransaction())
                    {
                        try
                        {
                            using (SqlCommand command = new SqlCommand("UPDATE_SERVICES_STATUS", connection, transaction))
                            {
                                command.CommandType = CommandType.StoredProcedure;
                                command.Parameters.AddWithValue("@NAME", SERVICES_ID);
                                command.Parameters.AddWithValue("@MACHINE", Environment.MachineName);
                                command.Parameters.AddWithValue("@STATUS", STATUS);
                                command.ExecuteNonQuery();
                            }

                            transaction.Commit();

                            UpdateStatusLabel("Status: Online, Last Update:" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                        }
                        catch (Exception ex)
                        {
                            transaction.Rollback();
                            LogToDatabase(1, "Error during status update: " + ex.Message);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                LogToDatabase(1, "Error connecting to the database: " + ex.Message);
            }
        }

        private void UpdateStatusLabel(string status)
        {
            if (poisonLabel1.InvokeRequired)
            {
                poisonLabel1.Invoke(new Action(() => { poisonLabel1.Text = status; }));
            }
            else
            {
                poisonLabel1.Text = status;
            }
        }


        public DataTable GET_QUEUE_LIST_SERVICES()
        {
            DataTable resultTable = new DataTable();
            try
            {
                string connectionString = ConfigurationManager.AppSettings["DBConn"];
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    using (SqlTransaction transaction = connection.BeginTransaction())
                    {
                        try
                        {
                            using (SqlCommand command = new SqlCommand("GET_QUEUE_LIST_SERVICES", connection, transaction))
                            {
                                command.CommandType = CommandType.StoredProcedure;
                                command.Parameters.AddWithValue("@SERVICESID", SERVICES_ID);
                                using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                                {
                                    adapter.Fill(resultTable);
                                }
                            }
                            transaction.Commit();
                        }
                        catch (Exception ex)
                        {
                            transaction.Rollback();
                            LogToDatabase(1, "Error during row-by-row insertion: " + ex.Message);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                LogToDatabase(1, "Error connecting to the database: " + ex.Message);
            }
            return resultTable;
        }


        public DataTable ADD_PROCESS_LIST_SERVICES()
        {
            DataTable resultTable = new DataTable();
            try
            {
                string connectionString = ConfigurationManager.AppSettings["DBConn"];
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    using (SqlTransaction transaction = connection.BeginTransaction())
                    {
                        try
                        {
                            using (SqlCommand command = new SqlCommand("ADD_PROCESS_LIST_SERVICES", connection, transaction))
                            {
                                command.CommandType = CommandType.StoredProcedure;
                                command.Parameters.AddWithValue("@SERVICESID", SERVICES_ID);
                                using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                                {
                                    adapter.Fill(resultTable);
                                }
                            }
                            transaction.Commit();
                        }
                        catch (Exception ex)
                        {
                            transaction.Rollback();
                            LogToDatabase(1, "Error during row-by-row insertion: " + ex.Message);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                LogToDatabase(1, $"ADD_PROCESS_HOUR: {ex.Message}");
            }
            return resultTable;
        }

        public DataTable GET_COMMAND(string rpaJobId, string jobId, string queueId)
        {
            DataTable resultTable = new DataTable();
            try
            {
                string connectionString = ConfigurationManager.AppSettings["DBConn"];
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    using (SqlTransaction transaction = connection.BeginTransaction())
                    {
                        try
                        {
                            using (SqlCommand command = new SqlCommand("GET_COMMAND", connection, transaction))
                            {
                                command.CommandType = CommandType.StoredProcedure;
                                command.Parameters.AddWithValue("@RPA_JOB_ID", rpaJobId);
                                command.Parameters.AddWithValue("@JOB_ID", jobId);
                                command.Parameters.AddWithValue("@QUEUE_ID", queueId);
                                command.Parameters.AddWithValue("@SERVICESID", SERVICES_ID);
                                using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                                {
                                    adapter.Fill(resultTable);
                                }
                            }
                            transaction.Commit();
                        }
                        catch (Exception ex)
                        {
                            transaction.Rollback();
                            LogToDatabase(1, "Error during command retrieval: " + ex.Message);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                LogToDatabase(1, $"GET_COMMAND: {ex.Message}");
            }
            return resultTable;
        }

        public void POST_FINISH_TASK(string rpaJobId, string jobId, string queueId, string MESSAGE, string STATUS)
        {
            try
            {
                string connectionString = ConfigurationManager.AppSettings["DBConn"];
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    using (SqlTransaction transaction = connection.BeginTransaction())
                    {
                        try
                        {
                            using (SqlCommand command = new SqlCommand("POST_FINISH_TASK", connection, transaction))
                            {
                                command.CommandType = CommandType.StoredProcedure;
                                command.Parameters.AddWithValue("@RPA_JOB_ID", rpaJobId);
                                command.Parameters.AddWithValue("@JOB_ID", jobId);
                                command.Parameters.AddWithValue("@QUEUE_ID", queueId);
                                command.Parameters.AddWithValue("@SERVICESID", SERVICES_ID);
                                command.Parameters.AddWithValue("@MESSAGE", MESSAGE);
                                command.Parameters.AddWithValue("@STATUS", STATUS);
                                command.ExecuteNonQuery();
                            }
                            transaction.Commit();
                        }
                        catch (Exception ex)
                        {
                            transaction.Rollback();
                            LogToDatabase(1, "Error during command retrieval: " + ex.Message);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                LogToDatabase(1, $"POST_FINISH_TASK: {ex.Message}");
            }
        }

        private void OnTimerElapsed(object sender, ElapsedEventArgs e)
        {
            UpdateStatus("Online");
            GET_QUEUE_LIST();
            //GET_PROCESS_LIST();
            //RUN_PROCESS();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            LogToDatabase(1, $"Agent stopped");
        }
        private void notifyIcon1_MouseDoubleClick_1(object sender, MouseEventArgs e)
        {
            Show();
            WindowState = FormWindowState.Normal;
            string Title = "Background Mode: Disabled";
            string Body = "Application now running normal usage of CPU and Memory";
            CREATENOTIF(Title, Body, 1000);
        }
        
        private void Form1_Resize(object sender, EventArgs e)
        {
            if (WindowState == FormWindowState.Minimized)
            {
                Hide();
                string Title = "Background Mode: Enabled";
                string Body = "Application now running under very low usage of CPU and Memory";
                CREATENOTIF(Title, Body, 1000);
            }
            else if (FormWindowState.Normal == WindowState)
            {
                notifyIcon1.Visible = false;
            }
        }

        public void CREATENOTIF(string Title, string Body, int Time)
        {
            notifyIcon1.Visible = true;
            notifyIcon1.BalloonTipTitle = Title;
            notifyIcon1.BalloonTipText = Body;
            notifyIcon1.Text = "System Tray App";
            notifyIcon1.ShowBalloonTip(Time);
        }
    }
}