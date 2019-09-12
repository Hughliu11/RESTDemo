using RestSharp;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Dapper;
using System.Configuration;
using SimpleJson;
using EBAP.RestTimer.Models;
using Newtonsoft.Json;
using EBAP.RestTimer.Models.CLCKD;
using System.IO;
using EBAP.RestTimer.Models.ZYTJD;

namespace EBAP.RestTimer
{
    public partial class MainForm : Form
    {
        string EBAPClient = ConfigurationManager.ConnectionStrings["EBAPClient"].ConnectionString;
        string EBAPLogging = ConfigurationManager.ConnectionStrings["EBAPLogging"].ConnectionString;
        
        //timer状态
        bool timer1Status = false;
        bool timer2Status = false;
        bool timer3Status = false;
        bool timer4Status = false;


        //接口地址
        string timer1ServerAddress = "";
        string timer2ServerAddress="";
        string timer3ServerAddress = "";
        string timer4ServerAddress = "";
        //本地日志路径
        string logPath = "";
        public MainForm()
        {
            InitializeComponent();

            timer1.Enabled = bool.Parse(ConfigurationManager.AppSettings["timer1Enabled"]);
            timer1.Interval = int.Parse(ConfigurationManager.AppSettings["timer1Interval"]);

            timer2.Enabled = bool.Parse(ConfigurationManager.AppSettings["timer2Enabled"]);
            timer2.Interval = int.Parse(ConfigurationManager.AppSettings["timer2Interval"]);

            timer3.Enabled = bool.Parse(ConfigurationManager.AppSettings["timer3Enabled"]);
            timer3.Interval = int.Parse(ConfigurationManager.AppSettings["timer3Interval"]);

            timer4.Enabled = bool.Parse(ConfigurationManager.AppSettings["timer4Enabled"]);
            timer4.Interval = int.Parse(ConfigurationManager.AppSettings["timer4Interval"]);

            timer1ServerAddress = ConfigurationManager.AppSettings["timer1ServerAddress"];
            timer2ServerAddress = ConfigurationManager.AppSettings["timer2ServerAddress"];
            timer3ServerAddress = ConfigurationManager.AppSettings["timer3ServerAddress"];
            timer4ServerAddress = ConfigurationManager.AppSettings["timer4ServerAddress"];
            logPath = ConfigurationManager.AppSettings["logPath"];
        }

        private void Msg(string msg)
        {
            txtMsg.AppendText($"{msg}\n");
        }

        /// <summary>
        /// 材料出库同步
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void timer1_Tick(object sender, EventArgs e)
        {
            TimerManerger("timer1", "in");

            var tJson = "";
            try
            {                
                using (var conn = new SqlConnection(EBAPClient)) {

                    CLCKDHeaddataEntity headData = new CLCKDHeaddataEntity();
                    List<CLCKDBodydataEntity> bodyDataList = new List<CLCKDBodydataEntity>();

                    using (var flag = conn.QueryMultiple("GetCLCKDInterfaceInfo", commandType: CommandType.StoredProcedure)) {

                        if (!flag.IsConsumed)
                        {
                            var hh = flag.Read<CLCKDHeaddataEntity>();
                            var bb = flag.Read<CLCKDBodydataEntity>();

                            foreach (CLCKDHeaddataEntity head in hh)
                            {
                                headData = head;
                            }

                            foreach (CLCKDBodydataEntity body in bb)
                            {
                                bodyDataList.Add(body);
                            }

                            if (hh.Count() > 0 & bb.Count() > 0) {
                                CLCKDBilldataEntity billData = new CLCKDBilldataEntity(headData, bodyDataList);
                                CLCKDEntity clckd = new CLCKDEntity("m4d", billData);

                                //对象转Json
                                 tJson = JsonConvert.SerializeObject(clckd);

                                //发送请求
                                var client = new RestClient(timer1ServerAddress);
                                //var client = new RestClient("http://localhost:59095/api/values");
                                var request = new RestRequest()
                                {
                                    Method = Method.POST
                                };
                                request.AddHeader("header", "application/x-www-form-urlencoded");
                                request.AddParameter("reqdata", tJson);

                                IRestResponse response = client.Execute(request);

                                if (response.ErrorMessage == null)
                                {
                                    string log = "timer1_材料出库_已发送数据:" + System.DateTime.Now.ToString() + "\n";
                                    Msg(log);
                                    WriteLogToText("CLCKD", log);
                                    WriteLogToText("CLCKD", "SendData:" + tJson);
                                    
                                    CLCKDResponseEntity respEntity = JsonConvert.DeserializeObject<CLCKDResponseEntity>(response.Content);

                                    if (respEntity == null)
                                    {
                                        string log2 = "timer1_材料出库_返回消息处理:" + System.DateTime.Now.ToString() + ":\n" + "U8返回信息不合规范";
                                        Msg(log2);
                                        WriteLogToText("CLCKD", log2);
                                    }
                                    else
                                    {
                                        string result = respEntity.result;
                                        string oldbillcode = respEntity.oldbillcode;
                                        string info = respEntity.info;
                                        if (result.Equals("SUCCESS"))
                                        {
                                            //添加 同步标记
                                            string sql = " update  T_ProductionBatch set  IsSynced=1,SyncTime1=GETDATE()   where PlanNo=@PlanNo ";
                                            string sql1 = " update  T_OptimizeBoard set  IsSynced=1,SyncTime1=GETDATE()   where PlanNo=@PlanNo ";

                                            conn.Execute(sql, new { PlanNo = oldbillcode });
                                            conn.Execute(sql1, new { PlanNo = oldbillcode });


                                            string deletesql = "delete  from C_InterfaceErrorLog where InterfaceTpye='材料出库单' and ReceiptNo=@ReceiptNo";
                                            conn.Execute(deletesql, new { ReceiptNo = oldbillcode });

                                        }
                                        else if (result.Equals("FAILED"))
                                        {
                                            string sql = "insert into C_InterfaceErrorLog values(@InterfaceTpye,@ReceiptNo,@ErrorMessage,GETDATE()) ";
                                            conn.Execute(sql, new { InterfaceTpye = "材料出库单", ReceiptNo = oldbillcode, ErrorMessage = info });
                                        }

                                        string log3 = "timer1_材料出库_返回消息处理:" + System.DateTime.Now.ToString() + ":\n" + response.Content;
                                        Msg(log3);
                                        WriteLogToText("CLCKD", log3);
                                    }
                                }
                                else
                                {
                                    string log2 = "timer1_材料出库:" + System.DateTime.Now.ToString() + response.ErrorMessage;
                                    Msg(log2);
                                    WriteLogToText("CLCKD", log2);
                                }
                            }
                            else
                            {
                                string log1 = "timer1_材料出库:" + System.DateTime.Now.ToString() + "无请求数据!!";
                                Msg(log1);
                                WriteLogToText("CLCKD", log1);
                            }
                        }                       
                    }
                }
            }
            catch (Exception ex)
            {
                string log3="timer1_材料出库:" + System.DateTime.Now.ToString() + "\n" + ex.Message;
                Msg(log3);
                WriteLogToText("CLCKD", log3);
                MessageBox.Show(ex.Message);
            }
            finally
            {
                (sender as Timer).Enabled = true;
                TimerManerger("timer1", "out");
            }
        }
        /// <summary>
        /// 产成品入库同步
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void timer2_Tick(object sender, EventArgs e)
        {
            TimerManerger("timer2", "in");
            try
            {
                using (var conn = new SqlConnection(EBAPClient))
                {
                    
                    CCPRKBodydataEntity bodydata = new CCPRKBodydataEntity();
                    CCPRKHeaddataEntity headdata = new CCPRKHeaddataEntity();
                    //测试执行存储过程
                    var rrr = conn.Query<CCPRKHeaddataEntity, CCPRKBodydataEntity, Boolean>("GetCCPRKInterfaceInfo", (h, b) =>
                    {
                        headdata = h;
                        bodydata = b;
                        return true;
                    }, commandType: CommandType.StoredProcedure, splitOn: "OrderId");

                    if (rrr.Count() > 0)
                    {
                        CCPRKBilldataEntity billdata = new CCPRKBilldataEntity(headdata, new List<CCPRKBodydataEntity>() { bodydata });
                       
                        CCPRKEntity ccpck = new CCPRKEntity("m46", billdata);
                        var sJson = JsonConvert.SerializeObject(ccpck);

                        //发送请求
                        //正式
                        var client = new RestClient(timer2ServerAddress);


                        var request = new RestRequest()
                        {
                            Method = Method.POST
                        };

                        request.AddHeader("header", "application/x-www-form-urlencoded");
                        request.AddParameter("reqdata", sJson);

                        IRestResponse response = client.Execute(request);

                        if (response.ErrorMessage == null)
                        {
                            string log3 = "timer2_产成品入库_已发送数据:" + System.DateTime.Now.ToString() + "\n";
                            Msg(log3);
                            WriteLogToText("CCPRK", log3);
                            WriteLogToText("CCPRK", "SendData:" + sJson);

                            CCPRKResponseEntity respEntity = JsonConvert.DeserializeObject<CCPRKResponseEntity>(response.Content);
                            string result = respEntity.result;
                            string oldbillcode = respEntity.oldbillcode;
                            string info = respEntity.info;
                            if (result.Equals("SUCCESS"))
                            {
                                string sql = "update T_BOM_Order set IsSynced=1,SyncTime1=GETDATE() where OrderId=@OrderId ";
                                string deletesql = "delete  from C_InterfaceErrorLog where InterfaceTpye='产成品入库' and ReceiptNo=@ReceiptNo";
                                conn.Execute(deletesql, new { ReceiptNo = oldbillcode });
                                conn.Execute(sql, new { OrderId = oldbillcode });

                            }
                            else if (result.Equals("FAILED"))
                            {
                                string sql = "insert into C_InterfaceErrorLog values(@InterfaceTpye,@ReceiptNo,@ErrorMessage,GETDATE()) ";
                                conn.Execute(sql, new { InterfaceTpye = "产成品入库", ReceiptNo = oldbillcode, ErrorMessage = info });
                            }

                            string log1 = "timer2_产成品入库_返回消息处理:" + System.DateTime.Now.ToString() + ":\n" + response.Content;
                            Msg(log1);
                            WriteLogToText("CCPRK", log1);
                        }
                        else
                            Msg(response.ErrorMessage);
                            WriteLogToText("CCPRK", response.ErrorMessage);

                    }
                    else
                    {
                        string log2 = "timer2_产成品入库:" + System.DateTime.Now.ToString() + "\n" + "无数据需发送";
                        Msg(log2);
                        WriteLogToText("CCPRK", log2);
                    }
                }                                    
            }
            catch (Exception ex)
            {
                string log3 = "timer2_产成品入库:" + System.DateTime.Now.ToString() + ":\n" + ex.Message;
                Msg(log3);
                WriteLogToText("CCPRK", log3);
                MessageBox.Show(ex.Message);
            }
            finally
            {
                (sender as Timer).Enabled = true;
                TimerManerger("timer2", "out");
            }
        }
        /// <summary>
        /// 作业统计单同步
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void timer3_Tick(object sender, EventArgs e)
        {          
            TimerManerger("timer3", "in");

            try
            {
                using (var conn = new SqlConnection(EBAPClient))
                {
                    ZYTJDHeaddataEntity headData = new ZYTJDHeaddataEntity();
                    List<ZYTJDBodydataEntity> bodyDataList = new List<ZYTJDBodydataEntity>();

                    using (var flag = conn.QueryMultiple("GetZYTJDInterfaceInfo", commandType: CommandType.StoredProcedure))
                    {
                        if (!flag.IsConsumed)
                        {
                            var hh = flag.Read<ZYTJDHeaddataEntity>();
                           
                            foreach (ZYTJDHeaddataEntity head in hh)
                            {                               
                                    headData = head;                                
                            }

                            var bb = flag.Read<ZYTJDBodydataEntity>();
                            foreach (ZYTJDBodydataEntity body in bb)
                            {
                                bodyDataList.Add(body);
                            }

                            if (hh.Count() > 0 & bb.Count() > 0)
                            {

                                ZYTJDBilldataEntity billData = new ZYTJDBilldataEntity(headData, bodyDataList);
                                ZYTJDEntity zytjd = new ZYTJDEntity("ZYTJ", billData);

                                var jsonStr = JsonConvert.SerializeObject(zytjd);
                                //发送请求
                               var client = new RestClient(timer3ServerAddress);
                                var request = new RestRequest()
                                {
                                    Method = Method.POST
                                };
                                request.AddHeader("header", "application/x-www-form-urlencoded");
                                request.AddParameter("reqdata", jsonStr);

                                IRestResponse response = client.Execute(request);
                                if (response.ErrorMessage == null)
                                {
                                    string log = "timer3_作业统计_已发送数据:" + System.DateTime.Now.ToString() + "\n";
                                    Msg(log);
                                    WriteLogToText("ZYTJD", log );
                                    WriteLogToText("ZYTJD", "SendData:" + jsonStr);

                                    ZYTJDResponseEntity respEntity = JsonConvert.DeserializeObject<ZYTJDResponseEntity>(response.Content);
                                   
                                    if(respEntity == null)
                                        {
                                        string log1 = "timer3_作业统计_返回消息处理:" + System.DateTime.Now.ToString() + ":\n" + "U8返回信息不合规范";
                                        Msg(log1);
                                        WriteLogToText("ZYTJD", log1);
                                    }
                                    else
                                    {
                                        string result = respEntity.result;
                                        string oldbillcode = respEntity.oldbillcode;
                                        string info = respEntity.info;

                                        if (result.Equals("SUCCESS"))
                                        {
                                            //添加 同步标记
                                            string sql = " update  T_Bom_Order set  IsSynced=1,SyncTime2=GETDATE()   where OrderId=@OrderId ";
                                            conn.Execute(sql, new { OrderId = oldbillcode });

                                            string deletesql = "delete  from C_InterfaceErrorLog where InterfaceTpye='作业统计单' and ReceiptNo=@ReceiptNo";
                                            conn.Execute(deletesql, new { ReceiptNo = oldbillcode });

                                        }
                                        else if (result.Equals("FAILED"))
                                        {
                                            string sql = "insert into C_InterfaceErrorLog values(@InterfaceTpye,@ReceiptNo,@ErrorMessage,GETDATE()) ";
                                            conn.Execute(sql, new { InterfaceTpye = "作业统计单", ReceiptNo = oldbillcode, ErrorMessage = info });
                                        }

                                        string log1 = "timer3_作业统计_返回消息处理:" + System.DateTime.Now.ToString() + ":\n" + response.Content;
                                        Msg(log1);
                                        WriteLogToText("ZYTJD", log1);
                                    }                                    
                                }
                                else
                                {
                                    string log2 = "timer3_作业统计:" + System.DateTime.Now.ToString() + response.ErrorMessage;
                                    Msg(log2);
                                    WriteLogToText("ZYTJD", log2);
                                }
                            }
                            else {
                                string log3 = "timer3_作业统计:" + System.DateTime.Now.ToString() + "无请求数据!!";
                                Msg(log3);
                                WriteLogToText("ZYTJD", log3);
                            }
                        }
                    }                                                      
                }
            }
                    catch (Exception ex)
            {
                string log4 = "timer3_作业统计:" + System.DateTime.Now.ToString() + ":\n" + ex.Message;
                Msg(log4);
                WriteLogToText("ZYTJD", log4);
                MessageBox.Show(ex.Message);
            }
            finally
            {
                (sender as Timer).Enabled = true;
                TimerManerger("timer3", "out");
            }
        }

        /// <summary>
        /// 产成品出库同步
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void timer4_Tick(object sender, EventArgs e)
        {
            TimerManerger("timer4", "in");

            try
            {
                Msg("timer4_产成品出库:" + System.DateTime.Now.ToString());

            }
            catch (Exception ex)
            {
                string log4 = "timer4_产成品出库:" + System.DateTime.Now.ToString() + ":\n" + ex.Message;
                Msg(log4);
                WriteLogToText("CCPCK", log4);
                MessageBox.Show(ex.Message);
            }
            finally
            {
                (sender as Timer).Enabled = true;
                TimerManerger("timer4", "out");
            }

        }

        private void btnStartAll_ButtonClick(object sender, EventArgs e)
        {
            timer1.Enabled = true;
            timer2.Enabled = true;
            timer3.Enabled = true;

            timer1Status = true;
            timer2Status = true;
            timer3Status = true;
        }

        private void btnStopAll_ButtonClick(object sender, EventArgs e)
        {
            timer1.Enabled = false;
            timer2.Enabled = false;
            timer3.Enabled = false;

            timer1Status = false;
            timer2Status = false;
            timer3Status = false;
        }

        private void btnStartTimer1_Click(object sender, EventArgs e)
        {
            timer1.Enabled = true;

            timer1Status = true;
        }

        private void btnStartTimer2_Click(object sender, EventArgs e)
        {
            timer2.Enabled = true;

            timer2Status = true;
        }

        private void btnStartTimer3_Click(object sender, EventArgs e)
        {
            timer3.Enabled = true;

            timer3Status = true;
        }

        private void btnStartTimer4_Click(object sender, EventArgs e)
        {
            timer4.Enabled = true;

            timer4Status = true;
        }




        private void btnStopTimer1_Click(object sender, EventArgs e)
        {
            timer1.Enabled = false;

            timer1Status = false;
            TimerManerger("timer1", "out");
        }

        private void btnStopTimer2_Click(object sender, EventArgs e)
        {
            timer2.Enabled = false;

            timer2Status = false;
            TimerManerger("timer2", "out");
        }
     
        private void btnStopTimer3_Click(object sender, EventArgs e)
        {
            timer3.Enabled = false;

            timer3Status = false;
            TimerManerger("timer3", "out");
        }
        private void btnStopTimer4_Click(object sender, EventArgs e)
        {
            timer4.Enabled = false;

            timer4Status = false;
            TimerManerger("timer4", "out");
        }


        //Timer管理     
        public void TimerManerger(string timer, string action)
        {
            if (action.Equals("in"))
            {
                timer1.Enabled = false;
                timer2.Enabled = false;
                timer3.Enabled = false;
                timer4.Enabled = false;
            }
            if (timer.Equals("timer1") & action.Equals("out"))
            {
                if (timer2Status == true)
                {
                    timer1.Enabled = false;

                    timer2.Enabled = true;
                    timer3.Enabled = false;
                    timer4.Enabled = false;
                }
                else if (timer3Status == true)
                {
                    timer1.Enabled = false;
                    timer2.Enabled = false;
                    timer3.Enabled = true;
                    timer4.Enabled = false;

                }
                else if (timer4Status == true)
                {
                    timer1.Enabled = false;
                    timer2.Enabled = false;
                    timer3.Enabled = false;
                    timer4.Enabled = true;
                }
                else if (timer1Status == true)
                {
                    timer4.Enabled = false;
                    timer3.Enabled = false;
                    timer2.Enabled = false;
                    timer1.Enabled = true;

                }

            }
            if (timer.Equals("timer2") & action.Equals("out"))
            {
                if (timer3Status == true)
                {
                    timer1.Enabled = false;
                    timer2.Enabled = false;
                    timer3.Enabled = true;
                    timer4.Enabled = false;
                }
                else if (timer4Status == true)
                {
                    timer1.Enabled = false;
                    timer2.Enabled = false;
                    timer3.Enabled = false;
                    timer4.Enabled = true;

                }
                else if (timer1Status == true)
                {
                    timer4.Enabled = false;
                    timer3.Enabled = false;
                    timer2.Enabled = false;
                    timer1.Enabled = true;

                }
                else if (timer2Status == true)
                {
                    timer4.Enabled = false;
                    timer3.Enabled = false;
                    timer1.Enabled = false;
                    timer2.Enabled = true;

                }
            }
            if (timer.Equals("timer3") & action.Equals("out"))
            {
                if (timer4Status == true)
                {
                    timer4.Enabled = true;
                    timer3.Enabled = false;
                    timer2.Enabled = false;
                    timer1.Enabled = false;
                }
                else if (timer1Status == true)
                {
                    timer4.Enabled = false;
                    timer3.Enabled = false;
                    timer2.Enabled = false;
                    timer1.Enabled = true;
                }
                else if (timer2Status == true)
                {
                    timer4.Enabled = false;
                    timer3.Enabled = false;
                    timer1.Enabled = false;
                    timer2.Enabled = true;

                }
                else if (timer3Status == true)
                {
                    timer4.Enabled = false;
                    timer2.Enabled = false;
                    timer1.Enabled = false;
                    timer3.Enabled = true;

                }
            }
            if (timer.Equals("timer4") & action.Equals("out"))
            {
                if (timer1Status == true)
                {
                    timer4.Enabled = false;
                    timer3.Enabled = false;
                    timer2.Enabled = false;
                    timer1.Enabled = true;
                }
                else if (timer2Status == true)
                {
                    timer4.Enabled = false;
                    timer3.Enabled = false;
                    timer1.Enabled = false;
                    timer2.Enabled = true;

                }
                else if (timer3Status == true)
                {
                    timer4.Enabled = false;
                    timer3.Enabled = true;
                    timer1.Enabled = false;
                    timer2.Enabled = false;

                }
                else if (timer4Status == true)
                {
                    timer2.Enabled = false;
                    timer1.Enabled = false;
                    timer3.Enabled = false;
                    timer4.Enabled = true;

                }
            }

        }

        //保存日志文件到本地,每日创建新文件(名称以当前时间 年月日)
        public void WriteLogToText(String type,String info) {
            
            FileStream fstream = null;
            System.DateTime currentTime = new System.DateTime();
            currentTime = System.DateTime.Now;
            string strYMD = currentTime.ToString("d");
            string datename = "";

            if (type.Equals("CLCKD"))
            {
                datename = logPath + "\\" + type + "_" + currentTime.Year.ToString() + currentTime.Month.ToString() + currentTime.Day.ToString() + ".txt";
            }
            else if (type.Equals("CCPRK")) {
                datename= logPath + "\\" + type +"_"+ currentTime.Year.ToString() + currentTime.Month.ToString() + currentTime.Day.ToString() + ".txt";
            }
            else if(type.Equals("ZYTJD"))
            {
                datename = logPath + "\\" + type + "_" + currentTime.Year.ToString() + currentTime.Month.ToString() + currentTime.Day.ToString() + ".txt";
            }
             

            if (File.Exists(datename))
            {
                fstream = new FileStream(datename, FileMode.Append);
            }
            else
            {
                fstream = new FileStream(datename, FileMode.Create);
            }
            StreamWriter swriter = new StreamWriter(fstream, Encoding.Default);
            swriter.WriteLine("\n" + info);
            swriter.Close();
        }

        
    }
}
