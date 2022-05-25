using System;
using System.Data.OleDb;
using System.Diagnostics;
using System.IO;
using System.Net.NetworkInformation;
using System.Threading;
using System.Windows.Forms;

namespace CC_Update
{
    public partial class MES_Update : Form
    {
        string Server_IP = "192.168.30.103";
        private string sExcuteFile = "";
        private string sShowData = "";
        private string sConnnection = EncryptionQRCode.Encryption.DatabaseConnection("QRCode_Database.bmp");
        private static bool Finish_Download = false;
        private static bool First = true;
        public MES_Update()
        {
            InitializeComponent();
        }

        private void pnlLoading_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                // Release the mouse capture started by the mouse down.
                pnlLoading.Capture = false;

                // Create and send a WM_NCLBUTTONDOWN message.
                const int WM_NCLBUTTONDOWN = 0x00A1;
                const int HTCAPTION = 2;
                Message msg =
                    Message.Create(this.Handle, WM_NCLBUTTONDOWN,
                        new IntPtr(HTCAPTION), IntPtr.Zero);
                this.DefWndProc(ref msg);
            }
        }
        public bool Ping_Server(string Server_IP)
        {
            bool bOK = false;
            System.Net.NetworkInformation.Ping p = new System.Net.NetworkInformation.Ping();
            try
            {
                PingReply r = p.Send(Server_IP);
                if (r.Status == IPStatus.Success)
                    bOK = true;
                else
                    bOK = false;
            }
            catch (Exception exMsg)
            {
                if (exMsg != null)
                {
                    bOK = false;
                }
            }

            return bOK;
        }
        #region //Error log//
        public void WriteErrorLog(string sErrorMessage)
        {
            FileInfo fiLog = new FileInfo(Application.StartupPath + @"\UpdateLog.txt");
            StreamWriter swMsg = null;
            string sLine = null;
            try
            {
                if (fiLog.Exists)
                {
                    swMsg = fiLog.AppendText();
                }
                else
                {
                    swMsg = fiLog.CreateText();
                    sLine = sLine + DateTime.Now.ToString() + " Create new file" + Environment.NewLine;
                    swMsg.Write(sLine);
                }
                sLine = sLine + DateTime.Now.ToString() + " " + sErrorMessage + Environment.NewLine;
                swMsg.Write(sLine);
                swMsg.Flush();
                swMsg.Close();
            }
            catch (Exception exError)
            {
                if (exError != null)
                {
                    swMsg.Flush();
                    swMsg.Close();
                }
            }
        }
        #endregion

        #region //Check file//
        private bool CheckFile(string FileName)
        {
            bool bKeep = false;
            //if (FileName == "Shoe_Material_Update.exe") bKeep = true;
            if (FileName == "QRCode_Database.bmp") bKeep = true;
            //if (FileName == "EncryptionQRCode.dll") bKeep = true;
            if (FileName == "zxing.dll") bKeep = true;
            if (FileName == "RFID.ini") bKeep = true;
            //if (FileName == "Shoe_Material_Update.pdb") bKeep = true;
            if (!bKeep)
            {
                OleDbConnection odcConnect = new OleDbConnection(sConnnection);
                string sSQL = "SELECT FileName FROM File_Download_New WHERE FileName = ?";
                OleDbCommand odcCommand = new OleDbCommand(sSQL, odcConnect);
                odcCommand.Parameters.Add("FileName", OleDbType.VarChar).Value = FileName;
                odcConnect.Open();
                OleDbDataReader odrReader = odcCommand.ExecuteReader();
                try
                {
                    while (odrReader.Read())
                    {
                        bKeep = true;
                    }
                }
                catch (Exception exMsg)
                {
                    if (exMsg != null)
                    {
                        WriteErrorLog("CheckFile " + exMsg.ToString());
                        if (odrReader != null) odrReader.Dispose();

                    }
                }
                finally
                {
                    if (odrReader != null) odrReader.Dispose();
                }
            }
            return bKeep;
        }
        #endregion

        #region //Set excute file//
        private string ExcuteFile()
        {
            string sData = "";
            OleDbConnection odcConnect = new OleDbConnection(sConnnection);
            string sSQL = "SELECT FileName FROM File_Download_New WHERE ExcuteFile = 1";
            OleDbCommand odcCommand = new OleDbCommand(sSQL, odcConnect);

            try
            {
                odcConnect.Open();
                OleDbDataReader odrReader = odcCommand.ExecuteReader();
                try
                {
                    while (odrReader.Read())
                    {
                        sData = odrReader["FileName"].ToString().Trim();
                    }
                }
                catch (Exception exMsg)
                {
                    if (exMsg != null)
                    {
                        WriteErrorLog("CCDownload.DownloadFile " + exMsg.ToString());
                        if (odrReader != null) odrReader.Dispose();

                    }
                }
                finally
                {
                    if (odrReader != null) odrReader.Dispose();
                }
            }
            catch (Exception exMsg)
            {
                if (exMsg != null)
                {

                    if (odcCommand != null) odcCommand.Dispose();
                    if (odcConnect != null) odcConnect.Dispose();
                }
            }
            finally
            {
                if (odcCommand != null) odcCommand.Dispose();
                if (odcConnect != null) odcConnect.Dispose();
            }

            return sData;
        }
        #endregion

        #region //Download file//
        private void Get_DownloadMax()
        {
            First = false;
            int iCountFile = 0;
            string sPath = "", sFileName = "";
            bool bFileExists = false;
            string[] sFileList = new string[1000];

            foreach (string FileName in System.IO.Directory.GetFileSystemEntries(System.Windows.Forms.Application.StartupPath + @"\\SM_Download", "*.*"))
            {
                if (!CheckFile(FileName.Remove(0, System.Windows.Forms.Application.StartupPath.Length + 14))) File.Delete(FileName);
            }

            OleDbConnection odcConnect = new OleDbConnection(sConnnection);
            string sSQL = "SELECT FileName, ExcuteFile, Enforce, UpdateTime FROM File_Download_New WHERE UpdateStartDate <= ? AND UpdateDutyDate >= ? ORDER BY UpdateTime";
            //string sSQL = "SELECT LEN(FileStream) AS FileStream, FileName, ExcuteFile, Enforce, UpdateTime FROM File_Download_New WHERE UpdateStartDate <= ? AND UpdateDutyDate >= ? AND FileName = 'Lacty_HRS.exe'";
            OleDbCommand odcCommand = new OleDbCommand(sSQL, odcConnect);
            odcCommand.Parameters.Add("UpdateStartDate", OleDbType.VarChar).Value = DateTime.Now.ToString("yyyyMMdd");
            odcCommand.Parameters.Add("UpdateDutyDate", OleDbType.VarChar).Value = DateTime.Now.ToString("yyyyMMdd");

            try
            {
                odcConnect.Open();
                OleDbDataReader odrReader = odcCommand.ExecuteReader();
                try
                {
                    while (odrReader.Read())
                    {
                        sPath = System.Windows.Forms.Application.StartupPath + @"\\SM_Download\\" + odrReader["FileName"].ToString().Trim();
                        if (odrReader["ExcuteFile"].ToString().Trim() == "1")
                            sExcuteFile = odrReader["FileName"].ToString().Trim();
                        Application.DoEvents();
                        System.GC.Collect();
                        System.IO.FileInfo fiImg = new System.IO.FileInfo(sPath);
                        sFileName = odrReader["FileName"].ToString().Trim();
                        bFileExists = fiImg.Exists;
                        if (odrReader["Enforce"].ToString().Trim() == "1")
                        {
                            fiImg.Delete();
                            bFileExists = false;
                        }

                        if (bFileExists && (fiImg.LastWriteTime < StrToDate(odrReader["UpdateTime"].ToString().Trim())))
                        {
                            fiImg.Delete();
                            bFileExists = false;

                        }

                        if (!bFileExists)
                        {

                            sFileList[iCountFile] = odrReader["UpdateTime"].ToString().Trim();
                            iCountFile = iCountFile + 1;
                        }
                    }
                }
                catch (Exception exMsg)
                {
                    if (exMsg != null)
                    {
                        WriteErrorLog("CCDownload.DownloadFile " + exMsg.ToString());
                        if (odrReader != null) odrReader.Dispose();
                    }
                }
                finally
                {
                    if (odrReader != null) odrReader.Dispose();
                }
            }
            catch (Exception exMsg)
            {
                if (exMsg != null)
                {

                    if (odcCommand != null) odcCommand.Dispose();
                    if (odcConnect != null) odcConnect.Dispose();
                }
            }
            finally
            {
                if (odcCommand != null) odcCommand.Dispose();
                if (odcConnect != null) odcConnect.Dispose();
            }

            for (int i = 0; i < iCountFile; i++)
            {

                DownloadFile(sFileList[i]);
            }
            Finish_Download = true;
        }

        private void DownloadFile(string UpdateTime)
        {
            OleDbConnection odcConnect = new OleDbConnection(sConnnection);
            string sPath = "";
            bool bFileExists = false;

            string sSQL = "SELECT FileStream, FileName, ExcuteFile, Enforce, LEN(FileStream) AS LEN_FileStream, UpdateTime  FROM File_Download_New WHERE UpdateTime >= ?";
            OleDbCommand odcCommand = new OleDbCommand(sSQL, odcConnect);
            odcCommand.Parameters.Add("UpdateTime", OleDbType.VarChar).Value = UpdateTime;
            try
            {
                odcConnect.Open();
                OleDbDataReader odrReader = odcCommand.ExecuteReader();

                while (odrReader.Read())
                {
                    Application.DoEvents();
                    if (odrReader["ExcuteFile"].ToString().Trim() == "1")
                        sExcuteFile = odrReader["FileName"].ToString().Trim();
                    System.GC.Collect();
                    sPath = System.Windows.Forms.Application.StartupPath + @"\\SM_Download\\" + odrReader["FileName"].ToString().Trim();
                    //sPath = @"C:\ERP\" + odrReader["FileName"].ToString().Trim();
                    System.IO.FileInfo fiImg = new System.IO.FileInfo(sPath);
                    bFileExists = fiImg.Exists;

                    if (!bFileExists)
                    {
                        Application.DoEvents();
                        //lblFileDownload.Text = odrReader["FileName"].ToString().Trim() + "-->" + "Download";
                        //lblFileDownload.Refresh();
                        Byte[] byBlob = null;
                        byBlob = new Byte[(odrReader.GetBytes(0, 0, null, 0, int.MaxValue))];
                        odrReader.GetBytes(0, 0, byBlob, 0, byBlob.Length);
                        FileStream fsImg = null;
                        fsImg = new FileStream(sPath, FileMode.Create, FileAccess.Write);
                        fsImg.Write(byBlob, 0, byBlob.Length);
                        fsImg.Close();
                       sShowData= odrReader["FileName"].ToString().Trim();
                        
                        //prbDownload.Value += Convert.ToInt32(odrReader["LEN_FileStream"].ToString().Trim());
                    }
                }
            }
            catch (Exception exMsg)
            {
                if (exMsg != null)
                {
                    WriteErrorLog("CCDownload.DownloadFile " + exMsg.ToString());
                    if (odcCommand != null) odcCommand.Dispose();
                    if (odcConnect != null) odcConnect.Dispose();
                }
            }
            finally
            {
                if (odcCommand != null) odcCommand.Dispose();
                if (odcConnect != null) odcConnect.Dispose();
            }

        }

        #endregion

        #region //Kill process
        private void Kill_Process()
        {
            string sStr = "";
            OleDbConnection odcConnect = new OleDbConnection(sConnnection);
            string sSQL = "SELECT FileName  FROM File_Background";
            OleDbCommand odcCommand = new OleDbCommand(sSQL, odcConnect);
            try
            {
                odcConnect.Open();
                OleDbDataReader odrReader = odcCommand.ExecuteReader();
                try
                {
                    while (odrReader.Read())
                    {
                        Application.DoEvents();
                        sStr = odrReader["FileName"].ToString().Trim();
                        Process[] MyProcess = Process.GetProcessesByName(sStr);
                        if (MyProcess.Length > 0) MyProcess[MyProcess.Length - 1].Kill();

                    }
                }
                catch (Exception exMsg)
                {
                    if (exMsg != null)
                    {
                        WriteErrorLog("CCDownload.DownloadFile " + exMsg.ToString());
                        if (odrReader != null) odrReader.Dispose();

                    }
                }
                finally
                {
                    if (odrReader != null) odrReader.Dispose();
                }
            }
            catch (Exception exMsg)
            {
                if (exMsg != null)
                {
                    WriteErrorLog("CCDownload.DownloadFile " + exMsg.ToString());
                    if (odcCommand != null) odcCommand.Dispose();
                    if (odcConnect != null) odcConnect.Dispose();
                }
            }
            finally
            {
                if (odcCommand != null) odcCommand.Dispose();
                if (odcConnect != null) odcConnect.Dispose();
            }
        }
        #endregion

        #region //String change to DateTime//
        //2011.11.24 Merlin Create
        //2012.11.21 Merlin 增加一筆2012/11/21 15:02:40:111
        public DateTime StrToDate(string sDate)
        {
            DateTime dtDateTime = DateTime.Now;
            if (sDate.Length == 8) sDate = sDate.Substring(0, 4) + "-" + sDate.Substring(4, 2) + "-" + sDate.Substring(6, 2) + " 00:00:00";
            if (sDate.Length == 10) sDate = sDate.Substring(0, 4) + "-" + sDate.Substring(4, 2) + "-" + sDate.Substring(6, 2) + " " + sDate.Substring(8, 2) + ":00:00";
            if (sDate.Length == 12) sDate = sDate.Substring(0, 4) + "-" + sDate.Substring(4, 2) + "-" + sDate.Substring(6, 2) + " " + sDate.Substring(8, 2) + ":" + sDate.Substring(10, 2) + ":00";
            if (sDate.Length == 14) sDate = sDate.Substring(0, 4) + "-" + sDate.Substring(4, 2) + "-" + sDate.Substring(6, 2) + " " + sDate.Substring(8, 2) + ":" + sDate.Substring(10, 2) + ":" + sDate.Substring(12, 2);
            if (sDate.Length == 17) sDate = sDate.Substring(0, 4) + "-" + sDate.Substring(4, 2) + "-" + sDate.Substring(6, 2) + " " + sDate.Substring(8, 2) + ":" + sDate.Substring(10, 2) + ":" + sDate.Substring(12, 2) + "." + sDate.Substring(14, 3);
            return dtDateTime = Convert.ToDateTime(sDate);
        }
        #endregion
        private void DownloadProgram()
        {
            if (First)
            {
                Thread tDownload = new Thread(new ThreadStart(Get_DownloadMax));
                if (Directory.Exists(System.Windows.Forms.Application.StartupPath + @"\\SM_Download\\"))
                {
                    //Kill_Process();
                    //Get_DownloadMax();
                    tDownload.Start();
                }
                else
                {
                    System.IO.Directory.CreateDirectory(System.Windows.Forms.Application.StartupPath + @"\\SM_Download\\");
                    //Kill_Process();
                    //Get_DownloadMax();
                    tDownload.Start();
                }
            }
            if (Finish_Download)
            {
                sExcuteFile = ExcuteFile();
                Process.Start(System.Windows.Forms.Application.StartupPath + @"\\SM_Download\\" + sExcuteFile);
                //if(File.Exists("message.vbs")) Process.Start("message.vbs");
                //Thread.Sleep(2000);
                this.Close();
                Environment.Exit(Environment.ExitCode);//保證一定離開程式
            }
        }
        private void timUpdate_Tick(object sender, EventArgs e)
        {
            timUpdate.Enabled = false;
            DownloadProgram();
            if (!Finish_Download) timUpdate.Enabled = true;
            if (Ping_Server(Server_IP))
            {
                ptxwifi.Image = Properties.Resources.wifi32;
            }
            else
            {
                ptxwifi.Image = Properties.Resources.nowifi_32;
            }
        }

        private void pnlLoading_Paint(object sender, PaintEventArgs e)
        {
            //this.SetStyle(ControlStyles.UserPaint, true);
            //this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            //this.SetStyle(ControlStyles.DoubleBuffer, true);
            //Font fFont = new Font("Calibri Bold", 14F);
            //SolidBrush bBrush = new SolidBrush(Color.FromArgb(1, 118, 248));
            //e.Graphics.DrawString(sShowData, fFont, bBrush, 20, 20);

            label1.Text = sShowData;
        }

      
    }
}
