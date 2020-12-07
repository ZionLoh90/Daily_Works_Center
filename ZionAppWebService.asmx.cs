using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.DirectoryServices;
using System.IO;
using System.Linq;
using System.Management;
using System.Management.Automation;
using System.Management.Automation.Runspaces;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Script.Services;
using System.Web.Services;

namespace NewInstallationMgt
{
    /// <summary>
    /// Summary description for ZionAppWebService
    /// Description:Autotask API
    /// Author:Zion Loh
    /// Date:2020/5/6
    /// </summary>
    [WebService(Namespace = "ZionAppWebService", Name = "AutoTask")] //标题
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [System.Web.Script.Services.ScriptService]
    public class ZionAppWebService : System.Web.Services.WebService
    {
        [WebMethod(Description = "查询Case分配信息(SQL)")]
        public string GetAssignedToByTicketNumber(string TicketNumber)
        {
            string results = "";
            try
            {
                Conn con = new Conn();
                string strsql0 = "select AssignedTo,URL,AssignedDate from AutoTicket where Number = '" + TicketNumber.Trim() + "'";
                //string constr = con.ConnServiceNow().ToString();
                //SqlDataAdapter da = new SqlDataAdapter(strsql, new SqlConnection(constr));
                DataSet ds0 = con.ExeSQL(strsql0, con.ConnServiceNow());
                if (ds0.Tables[0].Rows.Count > 0)
                {
                    for (int i = 0; i < ds0.Tables[0].Rows.Count; i++)
                    {
                        results = ds0.Tables[0].Rows[i]["AssignedTo"].ToString().Trim() + ";" + ds0.Tables[0].Rows[i]["AssignedDate"].ToString().Trim() + ";" + ds0.Tables[0].Rows[i]["URL"].ToString().Trim();
                    }
                    //return results.Substring(1, results.Length - 1);
                    return results.Replace("xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\"", "");
                }
                else
                {
                    string strsql1 = "select AssignTo,RunTime,URL from AutoAssignBTCase where Number = '" + TicketNumber.Trim() + "'";
                    DataSet ds1 = con.ExeSQL(strsql1, con.ConnServiceNow());
                    if (ds1.Tables[0].Rows.Count > 0)
                    {
                        for (int i = 0; i < ds1.Tables[0].Rows.Count; i++)
                        {
                            results = ds1.Tables[0].Rows[i]["AssignTo"].ToString().Trim() + ";" + ds1.Tables[0].Rows[i]["RunTime"].ToString().Trim() + ";" + ds1.Tables[0].Rows[i]["URL"].ToString().Trim();
                        }
                        //return results.Substring(1, results.Length - 1);
                        return results.Replace("xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\"", "");
                    }
                    else
                    {
                        return results.Replace("xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\"", "") + string.Format("Case Number:{0}", TicketNumber) + " no record in database!";
                    }
                }

            }
            catch (Exception)
            {
                throw;
            }
        }

        [WebMethod(Description = "根据电脑名查询设备信息(SQL)")]
        public string GetInfoByHostName(string HostName)
        {
            string results = "";
            Conn con = new Conn();
            try
            {
                string strsql = "select IP,HostName,UserName,Model,BIOSVersion,SerialNumber,MAC,OSVersion,InstallTime,TotalMem,ModifyTime,UpdateBy,UpdatePC from CMDBPRD where HostName = '" + HostName.Trim() + "'";
                DataSet ds = con.ExeSQL(strsql, con.ConnCMDB());
                //results = @"Format(""IP"",""HostName"",""Model"",""BIOSVersion"",""SerialNumber"",""MAC"",""OSVersion"",""InstallTime"",""TotalMem"",""ModifyTime,UpdateBy"")" + "&#x000A;"; //XML换行 &#x000A;
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    results = "IP:" + ds.Tables[0].Rows[i]["IP"].ToString().Trim() + ";HostName:" + ds.Tables[0].Rows[i]["HostName"].ToString().Trim() + ";UserName:" + ds.Tables[0].Rows[i]["UserName"].ToString().Trim() + ";Model:" + ds.Tables[0].Rows[i]["Model"].ToString().Trim() + ";BIOS:" + ds.Tables[0].Rows[i]["BIOSVersion"].ToString().Trim() + ";SerialNumber:" + ds.Tables[0].Rows[i]["SerialNumber"].ToString().Trim() + ";MAC:" + ds.Tables[0].Rows[i]["MAC"].ToString().Trim().Replace(@"""", "") + ";OSVersion:" + ds.Tables[0].Rows[i]["OSVersion"].ToString().Trim() + ";InstallDate:" + ds.Tables[0].Rows[i]["InstallTime"].ToString().Trim() + ";Memory:" + ds.Tables[0].Rows[i]["TotalMem"].ToString().Trim() + ";ModifyDate:" + ds.Tables[0].Rows[i]["ModifyTime"].ToString().Trim() + ";UpdateBy:" + ds.Tables[0].Rows[i]["UpdateBy"].ToString().Trim() + ";UpdatePC:" + ds.Tables[0].Rows[i]["UpdatePC"].ToString().Trim();
                }

            }
            catch (Exception)
            {
                throw;
            }
            finally
            {

            }
            return results.Replace("xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\"", "");
        }

        [WebMethod(Description = "解锁NTID")]
        public string UnlockUserByNTID(string NTID)
        {
            string results = "";
            Process p = null;
            if (string.IsNullOrEmpty(NTID))
            {
                results = "NTID parameters missing!";
            }
            try
            {
                p = new Process();
                p.StartInfo.FileName = "cmd.exe";
                p.StartInfo.UseShellExecute = false;
                p.StartInfo.RedirectStandardInput = true;
                p.StartInfo.RedirectStandardOutput = true;
                p.StartInfo.RedirectStandardError = true;
                p.StartInfo.CreateNoWindow = true;
                p.StartInfo.WorkingDirectory = "c:\\";
                //string command2 = "powershell.exe ;cls;import-module activedirectory;powershell unlock-adaccount " + NTID;
                string command2 = "powershell;cls;Unlock-ADAccount -Identity '" + NTID.Trim() + "'";
                p.StartInfo.Arguments = " /c " + command2;
                p.Start();
                results = p.StandardOutput.ReadToEnd();
                //string psstr = "Unlock-ADAccount '" + NTID.Trim() + "'";
                //RunScript(psstr);

                //授权成功，记录数据
                try
                {
                    Conn con = new Conn();
                    string strsql0 = "Insert into MainAPI_Action_Record values('UnlockUserByNTID','NTID=" + NTID + "','http://" + Environment.MachineName + "/autotask/ZionAppWebService.asmx/UnlockUserByNTID?NTID=" + NTID + "','" + DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss") + "')";
                    con.ExeNoQuery(strsql0, con.ConnAutoJob());

                }
                catch (Exception)
                {
                    throw;
                }
            }
            catch (Exception)
            {
                results = new Exception().ToString();
            }
            finally
            {
                p.Close();
                GC.Collect();
            }
            if (string.IsNullOrEmpty(results))
            {
                return results.Replace("xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\"", "") + string.Format("Unlocked {0} success!", NTID);
            }
            else
            {
                return results.Replace("xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\"", "") + string.Format("Unlocked {0} failed!", NTID);
            }

        }

        [WebMethod(Description = "查询NTID信息")]
        public void GetUserInfoByNTID(string NTID)
        {
            //string command2 = "net user /domain " + NTID.Trim() + "&powershell.exe ;cls;import-module activedirectory;Invoke-Command -ScriptBlock{Get-ADUser -Identity " + NTID.Trim() + " -Properties 'DisplayName','extensionAttribute14','SamAccountName','EmployeeID','EmailAddress','Created','extensionAttribute13','Department','Title','Manager','serialNumber','roomNumber','POBox','MobilePhone','telephoneNumber','DistinguishedName','logonCount','LastLogonDate','LogonWorkstations','LockedOut','lockoutTime','LastBadPasswordAttempt','PasswordLastSet','whenChanged','PasswordNeverExpires','objectSid','Company','JabilExt15','PostalCode','StreetAddress','targetAddress','msTSExpireDate','enabled','Division','info','MemberOf','Description'}";
            //string command2 = "powershell.exe ;cls;import-module activedirectory;Invoke-Command -ScriptBlock{Get-ADUser -Identity " + NTID.Trim() + @" -Properties 'DisplayName','EmailAddress','Department','Created','LockedOut','lockoutTime','Enabled','msTSExpireDate','PasswordLastSet','LastBadPasswordAttempt','LastLogonDate','LogonCount','Company','PostalCode','serialNumber','JabilExt15','extensionAttribute14','MobilePhone','telephoneNumber','Title'}";
            //string psstr = "net user /domain " + NTID.Trim() + " ;Invoke-Command -ScriptBlock{Get-ADUser -Identity " + NTID.Trim() + " -Properties 'DisplayName','extensionAttribute14','SamAccountName','EmployeeID','EmailAddress','Created','extensionAttribute13','Department','Title','Manager','serialNumber','roomNumber','POBox','MobilePhone','telephoneNumber','DistinguishedName','logonCount','LastLogonDate','LogonWorkstations','LockedOut','lockoutTime','LastBadPasswordAttempt','PasswordLastSet','whenChanged','PasswordNeverExpires','objectSid','Company','JabilExt15','PostalCode','StreetAddress','targetAddress','msTSExpireDate','enabled','Division','info','MemberOf','Description' }|ConvertTo-Json";
            string psstr = "Invoke-Command -ScriptBlock{Get-ADUser -Identity  " + NTID.Trim() + " -Properties  'DisplayName','Name','extensionAttribute14','SamAccountName','EmployeeID','EmployeeNumber','JabilExt2','EmailAddress','Created','extensionAttribute13','Country','City','Division','JabilExt15','JabilExt16','Company','Department','Title','Manager','serialNumber','roomNumber','POBox','HomePhone','mobile','MobilePhone','telephoneNumber','DistinguishedName','logonCount','LastLogonDate','LogonWorkstations','LockedOut','lockoutTime','LastBadPasswordAttempt','PasswordLastSet','whenChanged','Modified','PasswordNeverExpires','objectSid','Company','JabilExt15','PostalCode','StreetAddress','targetAddress','msTSExpireDate','enabled','Division','info','MemberOf','Description','msDS-AuthenticatedAtDC' |ConvertTo-Json}";
            HttpContext.Current.Response.Write(HttpContext.Current.Request["callback"] + "({'Results':" + RunScript(psstr) + "})");
        }

        [WebMethod(Description = "根据用户名(英文名)查询用户信息")]
        public void GetUserInfoByUserName(string UserFullName)
        {
            //string command2 = @"powershell;Get-ADObject -LDAPFilter:'(anr=" + UserName + @")' -Properties:allowedChildClassesEffective,allowedChildClasses,lastKnownParent,sAMAccountType,systemFlags,userAccountControl,displayName,description,whenChanged,location,managedBy,memberOf,primaryGroupID,objectSid,msDS-User-Account-Control-Computed,sAMAccountName,lastLogonTimestamp,lastLogoff,mail,accountExpires,msDS-PhoneticCompanyName,msDS-PhoneticDepartment,msDS-PhoneticDisplayName,msDS-PhoneticFirstName,msDS-PhoneticLastName,pwdLastSet,operatingSystem,operatingSystemServicePack,operatingSystemVersion,telephoneNumber,physicalDeliveryOfficeName,department,company,manager,dNSHostName,groupType,c,l,employeeID,givenName,sn,title,st,postalCode,managedBy,userPrincipalName,isDeleted,msDS-PasswordSettingsPrecedence -ResultPageSize:'100' -ResultSetSize:'20201' -SearchScope:'Subtree' -Server:'172.26.160.69:3268'";
            string psstr = @"Get-ADUser " + UserFullName + " -Properties:allowedChildClassesEffective,allowedChildClasses,lastKnownParent,sAMAccountType,systemFlags,userAccountControl,displayName,description,whenChanged,location,managedBy,memberOf,primaryGroupID,objectSid,msDS-User-Account-Control-Computed,sAMAccountName,lastLogonTimestamp,lastLogoff,mail,accountExpires,msDS-PhoneticCompanyName,msDS-PhoneticDepartment,msDS-PhoneticDisplayName,msDS-PhoneticFirstName,msDS-PhoneticLastName,pwdLastSet,operatingSystem,operatingSystemServicePack,operatingSystemVersion,telephoneNumber,physicalDeliveryOfficeName,department,company,manager,dNSHostName,groupType,c,l,employeeID,givenName,sn,title,st,postalCode,managedBy,userPrincipalName,isDeleted,msDS-PasswordSettingsPrecedence | ConvertTo-Json";
            HttpContext.Current.Response.Write(HttpContext.Current.Request["callback"] + "({'Results':" + RunScript(psstr) + "})");
        }

        [WebMethod(Description = "查询AD域内电脑信息(SCCM)")]
        public void GetADComputerInfoByHostName(string HostName)
        {
            //string command2 = @"powershell ""get-wmiobject -query {select * from SMS_R_SYSTEM WHERE Name = '" + HostName.Trim() + "'" + @"} -computername ALFSCCM01 -namespace ROOT\SMS\site_J01"" ";
            string psstr = @"get-wmiobject -query {select * from SMS_R_SYSTEM WHERE Name = '" + HostName + @"'} -computername ALFSCCM01 -namespace ROOT\SMS\site_J01 |ConvertTo-Json";
            HttpContext.Current.Response.Write(HttpContext.Current.Request["callback"] + "({'Results':" + RunScript(psstr) + "})");
        }

        [WebMethod(Description = "查询目标(在线)主机登录用户名/序列号MAC地址")]
        public void GetLogonUserByHostName(string HostName)
        {
            //string command2 = @"powershell ""get-wmiobject -computername " + HostName.Trim() + @" win32_computersystem | format-list username""";
            string psstr = "Invoke-Command -scriptblock {Get-wmiobject -computername " + HostName + " -class win32_computersystem | Select-Object  -ExpandProperty username;get-wmiobject -computername " + HostName + " -class win32_bios| Select-Object  SerialNumber; getmac /s " + HostName + " /FO CSV |Select-Object -Skip 1 |ConvertFrom-Csv -Header MAC, Transport} | ConvertTo-Json";
            HttpContext.Current.Response.Write(HttpContext.Current.Request["callback"] + "({'Results':" + RunScript(psstr) + "})");
        }

        //[WebMethod(Description = "查询目标(在线)主机序列号和MAC地址")]
        //public void GetMACByHostName(string HostName)
        //{
        //    //string command2 = @"powershell ""get-wmiobject -computername " + HostName + @" -class win32_bios|findstr SerialNumber;getmac /s """ + @"" + HostName + @" |findstr /v ""\"" Physical Address";
        //    string psstr = "Invoke-Command -scriptblock {get-wmiobject -computername " + HostName + " -class win32_bios|findstr SerialNumber; getmac /s " + HostName + " /FO CSV |Select-Object -Skip 1 |ConvertFrom-Csv -Header MAC, Transport} | ConvertTo-Json";
        //    HttpContext.Current.Response.Write(HttpContext.Current.Request["callback"] + "({'Results':" + RunScript(psstr) + "})");
        //}

        [WebMethod(Description = "返回目标(在线)主机性能参数")]
        public void GetRemoteServerByHostName(string HostName)
        {
            GetRemoteServer(HostName);
        }
        #region  获取远程主机性能
        private const string PROPERTY_CAPACITY = "Capacity";
        private const string PROPERTY_AVAILABLE_BYTES = "AvailableBytes";
        private const string PROPERTY_PROCESS_ID = "ProcessId";
        private const string PROPERTY_PROCESS_NAME = "Name";
        private const string PROPERTY_WORKING_SET_SIZE = "WorkingSetSize";
        public void GetRemoteServer(string hostname)
        {
            ServerInfo serv = new ServerInfo();
            ConnectionOptions Conn = new ConnectionOptions();
            //设定用于WMI连接操作的用户名   
            Conn.Username = "SVCGZH_HUA";
            //设定用户的口令
            Conn.Password = "Jabil@!!2018aa18meet";
            Conn.EnablePrivileges = true; //获取最高的权限;
            Conn.Impersonation = ImpersonationLevel.Impersonate;
            Conn.Authentication = AuthenticationLevel.PacketPrivacy;//加密数据流
                                                                    // options.Authority = "ntdlmdomain:DOMAIN";
                                                                    // options.Authority = "kerberose:DOMAIN";
            ManagementScope test = new ManagementScope("\\\\" + hostname + "\\root\\cimv2", Conn);
            test.Connect();
            if (test.IsConnected)
            {
                //Console.WriteLine("Connect to remote host: ["+hostname+"] success!");
                serv.ConnectionInfo += "Connect to remote host: [" + hostname + "] success!";
            }
            else
            {
                //Console.WriteLine("Connect to remote host: [" + hostname + "] failed!");
                serv.ConnectionInfo += "Connect to remote host: [" + hostname + "] failed!";
            }
            //获取分辨率  SELECT * FROM CIM_VideoControllerResolution
            //获取DeviceID/Camera  Select * from Win32_PnPEntity
            //获取IP SELECT * FROM Win32_NetworkAdapterConfiguration  获取IP O["IPAddress"]
            //获取CPU和内存信息
            var query01 = new SelectQuery("SELECT * FROM Win32_ComputerSystemProduct");
            var query0 = new SelectQuery("SELECT * FROM Win32_OperatingSystem");
            var query1 = new SelectQuery("SELECT * FROM Win32_PhysicalMemory");
            var query2 = new SelectQuery("SELECT * FROM Win32_PerfRawData_PerfOS_Memory");
            var query3 = new SelectQuery("SELECT * FROM Win32_Processor");
            var query4 = new SelectQuery("SELECT * FROM Win32_Product");
            var query5 = new SelectQuery("select * from Win32_LogicalDisk where DriveType=3");
            var query6 = new SelectQuery("select * from Win32_Service");

            var searcher01 = new ManagementObjectSearcher(test, query01);
            var searcher0 = new ManagementObjectSearcher(test, query0);
            var searcher1 = new ManagementObjectSearcher(test, query1);
            var searcher2 = new ManagementObjectSearcher(test, query2);
            var searcher3 = new ManagementObjectSearcher(test, query3);
            var searcher4 = new ManagementObjectSearcher(test, query4);
            var searcher5 = new ManagementObjectSearcher(test, query5);
            var searcher6 = new ManagementObjectSearcher(test, query6);

            foreach (var o in searcher01.Get())
            {
                try
                {
                    //Console.WriteLine("Type:" + o["Name"].ToString() + "\r\n" + "SerialNumber:" + o["IdentifyingNumber"].ToString() + "\r\n" + "Version:" + o["Version"]);
                    serv.Type += "Type:" + o["Name"].ToString() + "<br />SerialNumber:" + o["IdentifyingNumber"].ToString() + "<br />Version:" + o["Version"];
                }
                catch (Exception ex)
                {
                    //Console.WriteLine(ex.Message);
                    throw;
                }
            }
            foreach (var o in searcher0.Get())
            {
                //Console.WriteLine("HostName: " + o["csname"]);
                //Console.WriteLine("OS: " + o["Caption"]);
                //Console.WriteLine("Version: " + o["Version"]);
                //Console.WriteLine("Windows DIR: " + o["WindowsDirectory"]);
                //Console.WriteLine("Vendor: " + o["Manufacturer"]);
                serv.HostInfo += "HostName: " + o["csname"];
                serv.HostInfo += "OS: " + o["Caption"];
                serv.HostInfo += "Version: " + o["Version"];
                serv.HostInfo += "Windows Path: " + o["WindowsDirectory"];
                serv.HostInfo += "Vendor: " + o["Manufacturer"];
            }
            var capacity = 0.0;
            var free = 0.0;
            var cpu = 0.0;
            // 2048
            foreach (var o in searcher1.Get())
            {
                capacity += (double)Convert.ToUInt64(o[PROPERTY_CAPACITY]) / (double)(1024 * 1024 * 1024);
                //Console.WriteLine("Memory Count: " + Math.Round(capacity, 2) + "GB");//Math.Round(capacity,2);//保留小数点后两位
                serv.MemoryInfo += "TotalMemory: " + Math.Round(capacity, 2) + "GB<br />";
            }
            foreach (var o in searcher2.Get())
            {
                free += (double)Convert.ToUInt64(o[PROPERTY_AVAILABLE_BYTES]) / (double)(1024 * 1024 * 1024);
                //Console.WriteLine("Memory Available: " + Math.Round(free, 2) + "GB");
                serv.MemoryInfo += "MemoryAvailable: " + Math.Round(free, 2) + "GB<br />";
            }
            var mempercent = Math.Round(((capacity - free) / capacity) * 100, 2);
            serv.MemoryInfo += "MemoryUsage:" + mempercent + "%<br />";
            foreach (var o in searcher3.Get())
            {
                if (o["LoadPercentage"] != null)
                {
                    cpu = long.Parse(o["LoadPercentage"].ToString());
                    //Console.WriteLine("CPUID:" + o["DeviceID"]);
                    //Console.WriteLine("CPU Usage: " + cpu + "%");
                    serv.CPUInfo += "CPUID:" + o["DeviceID"] + "<br />";
                    serv.CPUInfo += "CPUUsage: " + cpu + "%" + "<br />";
                }
                else
                {
                    cpu = 0;
                    //Console.WriteLine("CPUID:" + o["DeviceID"]);
                    //Console.WriteLine("CPU利用率: " + cpu);
                    serv.CPUInfo += "CPUID:" + o["DeviceID"] + "<br />";
                    serv.CPUInfo += "CPUUsage: " + cpu + "<br />";
                }
            }
            //foreach (var o in searcher4.Get())
            //{
            //    //Console.WriteLine(o.GetText(TextFormat.Mof) + "\r\n");
            //    Console.WriteLine("程序名:"+o["Name"].ToString()+"\r\n"+"版本:"+o["Version"]+"\r\n"+"产品ID:"+o["IdentifyingNumber"]+"\r\n");
            //}
            foreach (var o in searcher5.Get())
            {
                try
                {
                    var Size = Convert.ToDouble(o["Size"]) / 1024 / 1024 / 1024;//GB
                    double freesize = Convert.ToInt64(o["FreeSpace"]) / 1024 / 1024 / 1024;//GB
                                                                                           //Math.Round(Size, 2);//保留小数点后两位
                    string DiskName = "DiskName:" + o["Name"].ToString().Trim() + "<br />";
                    //string DiskID = "磁盘ID:" + o["VolumeName"].ToString().Trim() + "\r\n";
                    string DiskSize = "TotalDiskSpace:" + Math.Round(Size, 2).ToString().Trim() + "GB" + "<br />";
                    string FreeDisk = "DiskAvailable:" + Math.Round(freesize, 2).ToString().Trim() + "GB" + "<br />";
                    //string DiskDrive = "磁盘类型:" + o["Serialnumber"].ToString().Trim() + "\r\n";
                    //string DiskSn = "磁盘序列号:" + o["Serialnumber"].ToString().Trim() + "\r\n";
                    var percent = Math.Round(((Size - freesize) / Size) * 100, 2);
                    string Diskpercent = "DiskUsage:" + percent.ToString().Trim() + "%" + "<br />";
                    //Console.WriteLine(DiskName + DiskSize + FreeDisk + Diskpercent);
                    serv.DiskInfo += DiskName + DiskSize + FreeDisk + Diskpercent;
                }
                catch (Exception)
                {
                    throw;
                }

            }
            //foreach (var o in searcher6.Get())
            //{
            //    v += "Monitor:" + o["MonitorType"].ToString().Trim() + Environment.NewLine + o["MonitorManufacturer"].ToString().Trim() + Environment.NewLine + o["ScreenHeight"].ToString().Trim() + "*" + o["ScreenWidth"].ToString().Trim() + Environment.NewLine;
            //}
            GC.Collect();
            List<ServerInfo> list = new List<ServerInfo>();
            list.Add(serv);
            //return ToJSON(list);
            HttpContext.Current.Response.Write(HttpContext.Current.Request["callback"] + "({'Results':" + ToJSON(list) + "})");
            //return v + Environment.NewLine;
        }
        public class ServerInfo
        {
            public string ConnectionInfo { get; set; }
            public string Type { get; set; }
            public string HostInfo { get; set; }
            public string MemoryInfo { get; set; }
            public string CPUInfo { get; set; }
            public string DiskInfo { get; set; }
        }
        #endregion

        [WebMethod(Description = "验证目标(在线)主机是否可以远程连接")]
        public string ValidateRemoteServerByHostName(string HostName)
        {
            return remoteConnectValidate(HostName);
        }
        #region 验证远程主机是否可以访问
        /// <summary>  
        /// 验证是否能连接到远程计算机    
        /// </summary>  
        /// <param name="host">主机名或ip</param>  
        /// <param name="userName">用户名</param>  
        /// <param name="password">密码</param>  
        /// <returns></returns>  
        public string remoteConnectValidate(string hostname)
        {
            string v = "";
            ConnectionOptions connectionOptions = new ConnectionOptions();
            string username = "SVCGZH_HUA";
            string password = "Jabil@!!2018aa18meet";
            connectionOptions.Username = username;
            connectionOptions.Password = password;
            ManagementScope managementScope = new ManagementScope("\\\\" + hostname + "\\root\\cimv2", connectionOptions);
            Ping ping = new Ping();
            int timeout = 800;
            //string data = "ping test data";
            //byte buf = Encoding.ASCII.GetBytes(data);
            PingReply result = ping.Send(hostname, timeout);
            if (result.Status == IPStatus.Success)
            {
                try
                {
                    managementScope.Connect();
                    v += hostname + " remote connected succeed!";
                }
                catch (Exception)
                {
                    throw;
                }
            }
            else
            {
                v += "Hostname/IP address is not Pingable or invalid!";
            }
            return v;
        }
        #endregion

        [WebMethod(Description = "添加AD用户到临时管理员组")]
        public void AddTempAdminByHostName(string HostName, string NTID)
        {
            //Task.Factory.StartNew(() =>
            //{
                //添加权限
                string addAdmin = "Invoke-Command -ComputerName " + HostName + " -ScriptBlock {Add-LocalGroupMember -Group Administrators -Member " + NTID + ";Get-LocalGroupMember -Name Administrators |Select-Object -Property Name| findstr " + NTID + " } |ConvertTo-Json";
                //添加完成获取组成员是否有该用户，有的话就证明添加成功
                HttpContext.Current.Response.Write(HttpContext.Current.Request["callback"] + "({'Results':" + RunScript(addAdmin) + "})");
            //}).Wait();
            //string command1 = @"powershell.exe ""Invoke-Command -ComputerName " + HostName.Trim() + @" -ScriptBlock {Add-LocalGroupMember -Group Administrators -Member " + NTID.Trim() + @" ; Get-LocalGroupMember -Name Administrators | findstr """ + NTID.Trim() + @"""}";
            //查询
            //string command2 = @"powershell.exe ""Invoke-Command -ComputerName " + HostName.Trim() + @" -ScriptBlock {Get-LocalGroupMember -Name Administrators | findstr " + NTID.Trim() + @"}";

            //授权成功，记录数据
            try
            {
                Conn con = new Conn();
                string strsql0 = "Insert into MainAPI_Action_Record values('AddTempAdminByHostName_'" + HttpContext.Current.Request.UserHostName + "_" + HttpContext.Current.Request.LogonUserIdentity + ",'HostName=" + HostName + "&NTID=" + NTID + ",'http://" + Environment.MachineName + "/autotask/ZionAppWebService.asmx/AddTempAdminByHostName?HostName=" + HostName + "&NTID=" + NTID + "','" + DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss") + "')";
                con.ExeNoQuery(strsql0, con.ConnAutoJob());
            }
            catch (Exception)
            {
                throw;
            }

        }

        [WebMethod(Description = "查询目标主机管理员组")]
        public void GetAdminGroupByHostName(string HostName)
        {
            //string command1 = @"powershell.exe ""Invoke-Command -ComputerName " + HostName.Trim() + @" -ScriptBlock {Get-LocalGroupMember -Name Administrators | Format-List Name}";
            string psstr = @"Invoke-Command -ComputerName '" + HostName.Trim() + "' -ScriptBlock {Get-LocalGroupMember -Name Administrators |Select-Object -Property Name}|ConvertTo-Json";
            HttpContext.Current.Response.Write(HttpContext.Current.Request["callback"] + "({'Results':" + RunScript(psstr) + "})");
        }

        [WebMethod(Description = "查询黄埔用户信息(HR API)")]
        public string SearchHuangpuUsersByNTID(string SAP,string NTID,string Email)
        {
            string results = "";
            //Process p = null;
            try
            {
                string RemoteUrl = "http://cnhuam0appprd01/WebAPIHR/api/Employee/Info?";
                string Data = string.Format("SAPNo={0}&Ntid={1}&EMail={2}", SAP.Trim(), NTID, Email.Trim());
                string URL = RemoteUrl + Data;
                results = GeData(URL, "UTF-8");//调用Get API方法
                return results.Replace(@"""\""", "").Replace(",", "\r\n").Replace("{", "").Replace(@"[{""""", "").Replace(".", "").Replace(@"{""Records"":", "").Replace(@"}],""Result"":0,""ErrorMsg"":""success"",""ID"":0,""Remark"":null,""Count"":0}", "").Replace("[{", "").Replace("[]", "").Replace("}", "").Replace("]", "").Replace(@"""Records"":[", "");
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                GC.Collect();
            }
        }
        #region API-Get/Post
        /// <summary>
        /// GET提交
        /// </summary>
        /// <param name="strUrl">远程服务器路径</param>
        /// <param name="charset">字符编码</param>
        /// <returns></returns>
        public static string GeData(string strUrl, string charset)
        {
            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(strUrl);
                request.Method = "GET";
                request.Timeout = 30000;
                HttpWebResponse response = request.GetResponse() as HttpWebResponse;
                Stream streamReceive = response.GetResponseStream();
                Encoding encoding = Encoding.Default;
                if (!string.IsNullOrEmpty(charset) && Encoding.GetEncoding(charset) != Encoding.Default)
                {
                    encoding = Encoding.GetEncoding(charset);
                }
                StreamReader streamReader = new StreamReader(streamReceive, encoding);
                return streamReader.ReadToEnd();
            }
            catch (Exception ex)
            {
                return DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss") + Environment.NewLine + ex.Message;
            }
        }

        /// <summary>
        /// POST提交数据接收字符json
        /// </summary>
        /// <param name="url">远程服务器路径</param>
        /// <param name="postData">提交数据</param>
        /// <returns>接收数据</returns>
        public static string PostData(string url, byte[] postData)
        {
            try
            {
                HttpWebRequest myRequest = (HttpWebRequest)WebRequest.Create(url);
                myRequest.Method = "POST";
                myRequest.ContentType = "application/x-www-form-urlencoded";
                myRequest.ContentLength = postData.Length;
                Stream newStream = myRequest.GetRequestStream();
                // Send the data. 
                newStream.Write(postData, 0, postData.Length);
                newStream.Close();
                // Get response 
                HttpWebResponse myResponse = (HttpWebResponse)myRequest.GetResponse();
                StreamReader reader = new StreamReader(myResponse.GetResponseStream(), Encoding.UTF8);
                return reader.ReadToEnd();
            }
            catch (Exception ex)
            {
                return DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss") + Environment.NewLine + ex.Message;
            }
        }
        #endregion

        [WebMethod(Description = "查询AD组内成员")]
        public void SearchADGroupByGroupName(string FullGroupName)
        {
            //string command1 = "powershell.exe " + @" ""Get-ADGroupMember -Identity "" " + FullGroupName + @" "" |Select-Object name"" ";
            string psstr = @"Get-ADGroupMember -Identity '" + FullGroupName + "'|Select-Object -Property Name |ConvertTo-Json";
            HttpContext.Current.Response.Write(HttpContext.Current.Request["callback"] + "({'Results':" + RunScript(psstr) + "})");
        }

        [WebMethod(Description = "录入设备信息（新装电脑--SQL）")]
        public void UpdatePCInfo(string IP,string HostName,string UserName,string Domain,string Model,string BIOSVersion,string SerialNumber, string MAC,string Description,string OSVersion,DateTime InstallTime,DateTime BootOn,string TotalMemory,string AvaliableMemory,DateTime ModifyTime,string UpdateBy,string UpdatePC)
        {
            int results = 0;
            string result = "";
            bool flag = false;
            try
            {
                Conn con = new Conn();
                string condition = "select * from CMDBPRD where HostName='" + HostName + "' and SerialNumber='" + SerialNumber + "' and MAC='" + MAC + "'"; //判断是否存在记录--通过电脑名和序列号以及MAC地址
                string strsql0 = @"Insert into CMDBPRD values('" + IP+ "','" + HostName + "','" + UserName + "','" + Domain + "','" + Model + "','" + BIOSVersion + "','" + SerialNumber + "','" + MAC + "','" + Description + "','" + OSVersion + "','" + InstallTime + "','" + BootOn + "','" + TotalMemory + "','" + AvaliableMemory + "','" + ModifyTime + "','" + UpdateBy + "','" + UpdatePC + "')";
                DataSet ds = con.ExeSQL(condition, con.ConnCMDB());
                if (ds.Tables[0].Rows.Count > 0)
                {
                    flag = false;
                    result = "SQL has been existed record,please do not repeated!";
                }
                else
                {
                    results = con.ExeNoQuery(strsql0, con.ConnCMDB());
                    if (results > 0)
                    {
                        flag = true;
                        result = "Insert to SQL success!Total:" + results;
                    }
                    else
                    {
                        flag = false;
                        result = "Insert to SQL failed!";
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
            HttpContext.Current.Response.Write(HttpContext.Current.Request["callback"] + "({'Results':" + ToJSON(flag) + "})");
        }

        [WebMethod(Description = "SCCM健康状态检查")]
        public void SCCMCheck(string HostName)
        {
            //string results = "";
            //Process p = null;
            //p = new Process();
            //p.StartInfo.FileName = "cmd.exe";
            //p.StartInfo.UseShellExecute = false;
            //p.StartInfo.RedirectStandardInput = true;
            //p.StartInfo.RedirectStandardOutput = true;
            //p.StartInfo.RedirectStandardError = true;
            //p.StartInfo.CreateNoWindow = true;
            //p.StartInfo.WorkingDirectory = "c:\\";
            //string outPut = null;
            //string cmd1 = @"powershell ""get-wmiobject -query {select * from SMS_R_SYSTEM WHERE Name = '" + HostName + @"'} -computername ALFSCCM01 -namespace ROOT\SMS\site_J01 |format-list Name,IPAddresses,MACAddresses,LastLogonUserDomain,LastLogonUserName,OperatingSystemNameandVersion,BuildExt,CreationDate,IsAssignedToUser,SystemGroupName,ADSiteName,DistinguishedName,SID,ClientVersion,PFE_ScriptVer,PFE_LastDate,PFE_CFreeSpace,PFE_BITSStatus,PFE_CCMStatus,PFE_DCOM,PFE_DCOMProtocols,PFE_LanternAppCI,PFE_PolicyPlatformLAStatus,PFE_PolicyPlatformProcessorStatus,PFE_StaleLogs,PFE_WMIReadRepositor,PFE_WMIStatus,PFE_WMIWriteRepository,PFE_WUAStatus,PFE_RebootPending "" ";                    //查询
            //                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                           //string command2 = @"powershell.exe ""Invoke-Command -ComputerName " + HostName.Trim() + @" -ScriptBlock {Get-LocalGroupMember -Name Administrators | findstr " + NTID.Trim() + @"}";
            //p.StartInfo.Arguments = " /C " + cmd1;
            //p.Start();
            //results = p.StandardOutput.ReadToEnd();
            string psstr = @"get-wmiobject -query {select * from SMS_R_SYSTEM WHERE Name = '" + HostName + @"'} -computername ALFSCCM01 -namespace ROOT\SMS\site_J01 |Select-Object -Property Name,IPAddresses,MACAddresses,LastLogonUserDomain,LastLogonUserName,OperatingSystemNameandVersion,BuildExt,CreationDate,IsAssignedToUser,SystemGroupName,ADSiteName,DistinguishedName,SID,ClientVersion,PFE_ScriptVer,PFE_LastDate,PFE_CFreeSpace,PFE_BITSStatus,PFE_CCMStatus,PFE_DCOM,PFE_DCOMProtocols,PFE_LanternAppCI,PFE_PolicyPlatformLAStatus,PFE_PolicyPlatformProcessorStatus,PFE_StaleLogs,PFE_WMIReadRepositor,PFE_WMIStatus,PFE_WMIWriteRepository,PFE_WUAStatus,PFE_RebootPending|ConvertTo-Json";
            HttpContext.Current.Response.Write(HttpContext.Current.Request["callback"] + "({'Results':" + RunScript(psstr) + "})");
        }

        [WebMethod(Description = "货币金额转换成大写中文")]
        public string MoneyConventerTool_CN(string Money)
        {
            return MoneyConventer.GetCnString(Money);
            
        }
        [WebMethod(Description = "货币金额转换成大写英文")]
        public string MoneyConventerTool_EN(string Money)
        {
            return MoneyConventer.GetEnString(Money);

        }

        [WebMethod(Description = "姓名转拼音")]
        public string PinYinConventer(string Name)
        {
            return PinYinHelper.ConvertToAllSpell(Name);
        }
        [WebMethod(Description = "提取姓名拼音的首字母")]
        public string PinYinConventer_F(string Name)
        {
            return PinYinHelper.GetFirstSpell(Name);
        }

        //[WebMethod(Description = "扫描主机开放端口")]
        //public string PortScanTool(string IP,int MinPort,int MaxPort)
        //{
        //    string results = "";
        //    IPAddress ip = IPAddress.Parse(IP);
        //    for (int i = MinPort; i < MaxPort; i++) //1024
        //    {
        //        try
        //        {
        //            IPEndPoint point = new IPEndPoint(ip, i);
        //            Socket sock = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        //            sock.Connect(point);
        //            //results = string.Format("连接端口{0}成功!", point);
        //            //Console.WriteLine("连接端口{0}成功!", point);
        //            results = "Opened port on destination host:" + Environment.NewLine;
        //            results += IP + string.Format("({0})", point) + Environment.NewLine;
        //        }
        //        catch (SocketException e)
        //        {
        //            if (e.ErrorCode != 10061)
        //            {
        //                Console.WriteLine(e.Message);
        //            }
        //            results = string.Format("连接端口{0}失败!", i);
        //        }

        //    }
        //    return results;
        //}

        [WebMethod(Description = "【获取打印服务器打印机列表(PrinterStatus Code:0--Normal;2--Error;130--Error, Offline;128--Offline)】")]
        public string GetPrintersFromServer(string ServerName)
        {
            //DataSet ds = new DataSet("PrintersList_" + ServerName);
            //DataTable da = new DataTable("PrintersList_" + ServerName);
            string results = "";
            string servern = "";
            string printers = "";
            string printern = "";
            string ipa = "";
            string drivern = "";
            string jobc = "";
            string keepp = "";
            string sha = "";
            string shan = "";
            string id = "";
            string upt = "";
            try
            {
                Conn con = new Conn();
                string strsql0 = "select ServerName,PrinterStatus,PrinterName,IPAddress,DriverName,JobCount,KeepPrintedJobs,Shared,ShareName,ID as RunSpaceId,UpdateTime from PrinterServer where ServerName='" + ServerName + "' "; //判断是否存在记录--通过电脑名和序列号以及MAC地址
             //   cmd.Parameters.AddRange(
             //     new SqlParameter[]{
             //     new  SqlParameter("@ServerName",SqlDbType.VarChar){Value=ServerName},
             //});

                DataSet ds0 = con.ExeSQL(strsql0, con.ConnServiceNow());
                if (ds0.Tables[0].Rows.Count > 0)
                {
                    for (int i = 0; i < ds0.Tables[0].Rows.Count; i++)
                    {
                        results += "ServerName:" + ds0.Tables[0].Rows[i]["ServerName"].ToString() + ";PrinterStatus:" + ds0.Tables[0].Rows[i]["PrinterStatus"].ToString() + ";PrinterName" + ds0.Tables[0].Rows[i]["PrinterName"].ToString() + ";IPAddress:" + ds0.Tables[0].Rows[i]["IPAddress"].ToString() + ";DriverName:" + ds0.Tables[0].Rows[i]["DriverName"].ToString() + ";JobCount:" + ds0.Tables[0].Rows[i]["JobCount"].ToString() + ";KeepPrintedJobs:" + ds0.Tables[0].Rows[i]["KeepPrintedJobs"].ToString() + ";Shared:" + ds0.Tables[0].Rows[i]["Shared"].ToString() + ";ShareName:" + ds0.Tables[0].Rows[i]["ShareName"].ToString() + ";RunSpaceId:" + ds0.Tables[0].Rows[i]["RunSpaceId"].ToString() + ";UpdateTime:" + ds0.Tables[0].Rows[i]["UpdateTime"].ToString() ;
                        servern = ds0.Tables[0].Rows[i]["ServerName"].ToString();
                        printers = ds0.Tables[0].Rows[i]["PrinterStatus"].ToString();
                        printers = ds0.Tables[0].Rows[i]["PrinterName"].ToString();
                        ipa = ds0.Tables[0].Rows[i]["IPAddress"].ToString();
                        drivern = ds0.Tables[0].Rows[i]["DriverName"].ToString();
                        jobc = ds0.Tables[0].Rows[i]["JobCount"].ToString();
                    }
                    //return results.Substring(1, results.Length - 1);                  
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                //cmd.Dispose();
            }
            return results;
        }


        #region 获取AD用户信息
        //获取域用户信息方法
        private UserInfo GetUserInfo(string domainUserName)//domainUserName
        {
            try
            {
                if (string.IsNullOrEmpty(domainUserName))
                {
                    return null;
                }

                var userArr = domainUserName.Split('\\');
                var domain = userArr[0];
                var loginName = userArr[1];

                var entry = new DirectoryEntry(string.Concat("LDAP://", domain));
                var search = new DirectorySearcher(entry);
                search.Filter = string.Format("(SAMAccountName={0})", loginName);
                search.PropertiesToLoad.Add("SAMAccountName");
                search.PropertiesToLoad.Add("givenName");
                search.PropertiesToLoad.Add("cn");
                search.PropertiesToLoad.Add("mail");
                search.PropertiesToLoad.Add("Manager");
                search.PropertiesToLoad.Add("serialNumber");
                search.PropertiesToLoad.Add("Company");
                search.PropertiesToLoad.Add("Department");
                search.PropertiesToLoad.Add("Title");

                var result = search.FindOne();
                if (result != null)
                {
                    var info = new UserInfo();
                    info.SAMAccountName = result.Properties["SAMAccountName"][0].ToString();
                    info.GivenName = result.Properties["givenName"][0].ToString();
                    info.CN = result.Properties["cn"][0].ToString();
                    info.Email = result.Properties["mail"][0].ToString();
                    info.Manager = result.Properties["Manager"][0].ToString();
                    info.serialNumber = result.Properties["serialNumber"][0].ToString();//Cost Center    
                    info.Company = result.Properties["Company"][0].ToString();
                    info.Department = result.Properties["Department"][0].ToString();
                    info.Department = result.Properties["Title"][0].ToString();
                    return info;
                }
            }
            catch
            {

            }

            return null;
        }

        public sealed class UserInfo
        {
            public string SAMAccountName;
            public string GivenName;
            public string CN;
            public string Email;
            public string Manager;
            public string serialNumber; //Cost Center
            public string Company;
            public string Department;
            public string Title;
        }
        #endregion

        //https://www.cnblogs.com/bingzisky/archive/2011/10/12/2208972.html
        //[ScriptMethod(UseHttpGet = true, ResponseFormat = ResponseFormat.Json)]
        [WebMethod(Description = @"查询AD黄埔用户信息(用户名，邮箱，主管,成本中心，厂区，职位),按照指定格式输入NTID（1290656）")]
        public void GetADUser(string NTID)
        {
            string results = "";
            string domainname = Environment.UserDomainName + "\\" + NTID;
            var user2 = GetUserInfo(domainname);
            //results = string.Format("UserName:{0};Email:{1};Manager:{2};CostCenter:{3};Company:{4};Title:{5}", user2.CN, user2.Email, user2.Manager.Replace("CN=", "").Replace(",OU=Users,OU=Huangpu,OU=RegionAsia,DC=corp,DC=JABIL,DC=ORG", ""), user2.serialNumber.Replace("{", "").Replace("}", ""), user2.Company, user2.Department);
            List<UserModle> list = new List<UserModle>();
            UserModle use = new UserModle
            {
                UserName = user2.CN,
                Email = user2.Email,
                Manager = user2.Manager.Substring(0, user2.Manager.IndexOf(',')).Replace("CN=", ""),
                CostCenter = user2.serialNumber.Replace("{", "").Replace("}", ""),
                Company = user2.Company,
                Title = user2.Department,
            };
            list.Add(use);
            //return ToJSON(list);
            HttpContext.Current.Response.Write(HttpContext.Current.Request["callback"] + "({'Results':" + ToJSON(list) + "})");
        }
        //对数据序列化，返回JSON格式 
        public string ToJSON(object obj)
        {
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            return serializer.Serialize(obj);
        }
        public class UserModle
        {
            public string UserName { get; set; }
            public string Email { get; set; }
            public string Manager { get; set; }
            public string CostCenter { get; set; }
            public string Company { get; set; }
            public string Title { get; set; }
        }

        /*--PDA上测试只能执行一次，资源无法释放，需要重启应用程序
        [WebMethod(Description = "WINCE设备信息录入接口")]
        public string UpdateWINCEInfo(string HostName, string IP, string MAC, string Time)
        {
            int results = 0;
            string result = "";
            try
            {
                Conn con = new Conn();
                string condition = "select * from WinCE where HostName='" + HostName + "' and MAC='" + MAC + "' "; //判断是否存在记录--通过PDA名以及MAC地址
                string strsql0 = @"Insert into WinCE values('" + HostName + "','" + IP + "','" + MAC + "','" + Time + "','" + DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss") + "')";
                string strsql1 = @"Update WinCE set ClientTime='" + Time + "' , IP='"+IP+"',ServerTime = '"+DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss") +"' where HostName='" + HostName + "' and MAC='" + MAC + "' ";
                DataSet ds = con.ExeSQL(condition, con.ConnServiceNow());
                if (ds.Tables[0].Rows.Count > 0)
                {
                    result = "SQL has been existed this PDA record!Updating...";
                    results = con.ExeNoQuery(strsql1, con.ConnServiceNow());
                    if (results > 0)
                    {
                        result = "Update Info success!Total:" + results;
                    }
                    else
                    {
                        result = "Update Info failed!";
                    }
                }
                else
                {
                    results = con.ExeNoQuery(strsql0, con.ConnServiceNow());
                    if (results > 0)
                    {
                        result = "Insert to SQL success!Total:" + results;
                    }
                    else
                    {
                        result = "Insert to SQL failed!";
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
            return result;
        }

        [WebMethod(Description = "条码扫描测试接口")]
        public string BarcodeTest(string Barcode,string ClientTime,string ClientIP,string ClientMAC)
        {
            int results = 0;
            string result = "";
            Conn con = new Conn();
            try
            {
                string condition = "select * from WinCE_BarcodeTest where ClientMAC='" + ClientMAC + "' and ClientIP ='" + ClientIP + "' and Barcode = '" + Barcode + "'  and Day('" + DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss") + "') = Day(GetDate()) "; //判断是否存在记录--通过PDA名以及MAC地址
                string search = "Select * from WinCE_BarcodeTest where ClientMAC='" + ClientMAC + "' ";
                string strsql0 = @"Insert into WinCE_BarcodeTest values('" + Barcode + "','" + ClientTime + "','" + ClientIP + "','" + ClientMAC + "','" + DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss") + "' )";
                string strsql1 = "Update WinCE_BarcodeTest set Barcode = '" + Barcode + "' , ClientTime='" + ClientTime + "',ServerTime = '" + DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss") + "' , ClientIP = '" + ClientIP + "' where ClientMAC = '" + ClientMAC + "' ";
                DataSet ds = con.ExeSQL(search, con.ConnServiceNow());
                if (ds.Tables[0].Rows.Count > 0)
                {
                    result = "SQL has been existed this barcode test record!Updating...";
                    results = con.ExeNoQuery(strsql1, con.ConnServiceNow());
                    if (results > 0)
                    {
                        DataSet ds1 = con.ExeSQL(condition, con.ConnServiceNow());
                        if (ds1.Tables[0].Rows.Count > 0)
                        {
                            //for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                            //{
                            string clientreturn = ds.Tables[0].Rows[0]["ClientTime"].ToString().Trim();
                            string serverreturn = ds.Tables[0].Rows[0]["ServerTime"].ToString().Trim();
                            //}                            
                            //DateTime date0 = DateTime.Parse(clientreturn);
                            //DateTime date1 = DateTime.Parse(serverreturn);
                            ////TimeSpan timeSpan = date1 - date0;
                            //TimeSpan ts = date1.Subtract(date0).Duration();
                            ////double minutes = ts.Minutes;
                            //double sec = date1.Second - date0.Second;
                            //double millisec = date1.Millisecond - date0.Millisecond;
                            ////double seconds = ts.Seconds;
                            ////double milliseconds = ts.Milliseconds;
                            //string tempstr = "";
                            //tempstr = string.Format("Total time:{0} seconds {1} milliseconds.", sec, millisec);
                            result = string.Format("ClientTime:{0}\nServerTime{1}\nServer return time:{2}", clientreturn, serverreturn, DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss"));
                        }
                        ds.Dispose();
                    }
                    else
                    {
                        result = "Update Info failed!";
                    }
                }
                else
                {
                    results = con.ExeNoQuery(strsql0, con.ConnServiceNow());
                    if (results > 0)
                    {
                        DataSet ds1 = con.ExeSQL(condition, con.ConnServiceNow());
                        if (ds1.Tables[0].Rows.Count > 0)
                        {
                            //for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                            //{
                            string clientreturn = ds.Tables[0].Rows[0]["ClientTime"].ToString().Trim();
                            string serverreturn = ds.Tables[0].Rows[0]["ServerTime"].ToString().Trim();
                            //}                            
                            //DateTime date0 = DateTime.Parse(clientreturn);
                            //DateTime date1 = DateTime.Parse(serverreturn);
                            //TimeSpan timeSpan = date1 - date0;
                            ////TimeSpan ts = date1.Subtract(date0).Duration();
                            ////double minutes = ts.Minutes;
                            //double sec = date1.Second - date0.Second;
                            //double millisec = date1.Millisecond - date0.Millisecond;
                            ////double seconds = ts.Seconds;
                            ////double milliseconds = ts.Milliseconds;
                            //string tempstr = "";
                            //tempstr = string.Format("Total time:{0} seconds {1} milliseconds.", sec, millisec);
                            result = string.Format("ClientTime:{0}\nServerTime{1}\nServer return time:{2}", clientreturn, serverreturn, DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss"));
                        }
                        ds.Dispose();
                    }
                    else
                    {
                        result = "Upload Test Barcode To Server Failed!";
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                con.ConnServiceNow().Dispose();
            }
            return result;
        }
        */

        #region 执行powershell语句方法
        //执行powershell语句方法
        public static string InvokeSystemPS(string cmd)//提交方法，将命令传入，打开与powershell交互的工作流，提交命令，并获得返回值
        {
            string kk = "";
            try
            {
                List<string> ps = new List<string>();
                //开启计算机的安全设置，允许执行可能会用到
                //开启最高的执行权限
                //Unrestricted——允许所有的script运行
                //ps.Add("Set-ExecutionPolicy RemoteSigned");
                //ps.Add("Set-ExecutionPolicy -ExecutionPolicy Unrestricted");
                ps.Add(cmd);
                Runspace runspace = RunspaceFactory.CreateRunspace();
                runspace.Open();
                Pipeline pipeline = runspace.CreatePipeline();
                foreach (var scr in ps)
                {
                    pipeline.Commands.AddScript(scr);
                }
                var test = pipeline.Invoke();//Execute the ps script
                foreach (var item in test)
                {
                    //Type typename = item.ImmediateBaseObject.GetType();//获得通道中返回的最原始基类对象
                    string member = test[0].Members.ElementAt(3).Value.ToString();//返回对象中的第四个属性，保留活动标识，AddressState
                    string aa = member.Substring(0, member.Length);//分隔保留活动标识字符串
                                                                  //var A = Convert.ChangeType(item.ImmediateBaseObject, typename);//返回指定类型的对象

                    //Console.WriteLine(typename);//打印返回基类对象信息
                    Console.WriteLine(aa);//打印保留活动标识字符串
                                         //Console.WriteLine(item.ToString());//打印从通道流中的返回值信息
                                         //}
                    foreach (var a in test)
                    {
                        kk = a.ToString() + kk;
                        Console.WriteLine(kk);//打印返回信息
                    }
                    runspace.Close();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return kk;
        }
        #endregion

        public static string PsCommandRun(string cmd)
        {
            string results = "";
            Runspace runspace = RunspaceFactory.CreateRunspace();
            runspace.Open();
            using (PowerShell ps = PowerShell.Create())
            {
                //ps.AddCommand("Import-Module").AddParameter("Name", "RemoteDesktop");
                //ps.AddScript("Import-Module").AddParameter("Name", "RemoteDesktop");
                //ps.Invoke();
                //if (ps.Streams.Error.Count > 0)
                //    //Console.WriteLine(ps.Streams.Error[0].Exception);
                //    results = ps.Streams.Error[0].Exception.ToString();
                //ps.Commands.Clear();
                ps.Runspace = runspace;
                ps.AddCommand(cmd);
                results = ps.Invoke().ToString();
                if (ps.Streams.Error.Count > 0)
                    //Console.WriteLine(ps.Streams.Error[0].Exception);
                    results = ps.Streams.Error[0].Exception.ToString();
                // Do something with result ... 
            }
            runspace.Close();
            return results;
        }

        [WebMethod(Description = "根据关键词查询建Case链接")]
        public void GetCaseURLByKeyWord(string KeyWord)
        {
            string results = "";
            Conn con = null;
            try
            {
                con = new Conn();
                string strsql = "select Description as Chinese,KeyWord as English,URL from CaseKnowledgeBase where KeyWord like '%" + KeyWord.Trim() + "%' or Description like '%" + KeyWord.Trim() + "%'";
                DataSet ds = con.ExeSQL(strsql, con.ConnRegister());
                if (ds.Tables[0].Rows.Count > 0)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        results += "中文简介:" + ds.Tables[0].Rows[i]["Chinese"].ToString().Trim() + ";英文简介:" + ds.Tables[0].Rows[i]["English"].ToString().Trim() + ";链接地址:" + ds.Tables[0].Rows[i]["URL"].ToString().Trim() + "\n";
                    }
                }
                else
                {
                    results = "未找到匹配结果！";
                }

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {

            }
            HttpContext.Current.Response.Write(HttpContext.Current.Request["callback"] + "({'Results':" + ToJSON(results) + "})");//.Replace("xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\"", "");
        }

        [WebMethod(Description = "黄埔用户权限添加(添加用户到组--黄埔OU)")]
        public void AddUser2Group(string NTID, string GroupName)
        {
            string results = "";
            string pwd = ConfigurationManager.AppSettings.Get("ADPwd");
            bool flag = true;
            try
            {
                string script = @"Invoke-Command -ScriptBlock{$Username = 'q1_1290656';$Password = '" + pwd + "';$Pass = ConvertTo-SecureString $Password -AsPlainText -Force;$Cred = New-Object System.Management.Automation.PSCredential -ArgumentList $Username,$Pass;Add-ADGroupMember -Identity '" + GroupName + "' -Members '" + NTID.Trim() + "' -Credential $Cred}";
                results = RunScript(script);

                flag = true;
                //授权成功，记录数据
                try
                {
                    Conn con = new Conn();
                    string strsql0 = "Insert into MainAPI_Action_Record values('AddUser2Group_'" + HttpContext.Current.Request.UserHostName + "_" + HttpContext.Current.Request.LogonUserIdentity + " ,'NTID=" + NTID + "&GroupName=" + GroupName + "','http://" + Environment.MachineName + "/autotask/ZionAppWebService.asmx/AddUser2Group?NTID=" + NTID + "&GroupName=" + GroupName + "','" + DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss") + "')";
                    con.ExeNoQuery(strsql0, con.ConnAutoJob());
                }
                catch (Exception e)
                {
                    throw new Exception(e.Message + e.StackTrace);
                }
            }
            catch (Exception e)
            {
                flag = false;
            }
            finally
            {
                GC.Collect();
            }
            HttpContext.Current.Response.Write(HttpContext.Current.Request["callback"] + "({'Results':" + ToJSON(flag) + "})");
        }

        //C# 调用Powershell
        private static string RunScript(string scriptText)
        {
            // create Powershell runspace
            Runspace runspace = RunspaceFactory.CreateRunspace();
            // open it
            runspace.Open();
            // create a pipeline and feed it the script text
            Pipeline pipeline = runspace.CreatePipeline();
            pipeline.Commands.AddScript(scriptText);
            pipeline.Commands.Add("Out-String");

            // execute the script
            Collection<PSObject> results = pipeline.Invoke();
            // close the runspace
            runspace.Close();

            // convert the script result into a single string
            StringBuilder stringBuilder = new StringBuilder();
            foreach (PSObject obj in results)
            {
                stringBuilder.AppendLine(obj.ToString());
            }
            return stringBuilder.ToString();
        }

        public void RunScript(List<string> scripts)
        {
            try
            {
                Runspace runspace = RunspaceFactory.CreateRunspace();
                runspace.Open();
                Pipeline pipeline = runspace.CreatePipeline();
                foreach (var scr in scripts)
                {
                    pipeline.Commands.AddScript(scr);
                }
                //返回结果   
                var results = pipeline.Invoke();
                runspace.Close();

            }
            catch (Exception e)
            {
                //Console.WriteLine(DateTime.Now.ToString() + "日志记录:执行ps命令异常：" + e.Message);
                throw new Exception(e.Message);
            }
        }


        [WebMethod(Description = "黄埔用户权限删除(从组--黄埔OU中删除用户)")]
        public void RemoveUserFromGroup(string NTID, string GroupName)
        {
            string results = "";
            bool verify = true;
            try
            {
                string pwd = ConfigurationManager.AppSettings.Get("ADPwd");
                string script = @"Invoke-Command -ScriptBlock{$Username = 'q1_1290656';$Password = '" + pwd + "';$Pass = ConvertTo-SecureString $Password -AsPlainText -Force;$Cred = New-Object System.Management.Automation.PSCredential -ArgumentList $Username,$Pass;Remove-ADGroupMember -Identity '" + GroupName + "' -Members '" + NTID.Trim() + "' -Credential $Cred -Confirm:$false}";
                results = RunScript(script);
                verify = true;
                //授权成功，记录数据
                try
                {
                    Conn con = new Conn();
                    string strsql0 = "Insert into MainAPI_Action_Record values('RemoveUserFromGroup_'" + HttpContext.Current.Request.UserHostName + "_" + HttpContext.Current.Request.LogonUserIdentity + " ,'NTID=" + NTID + "&GroupName=" + GroupName + "','http://" + Environment.MachineName + ",'NTID=" + NTID + "&GroupName=" + GroupName + "','http://" + Environment.MachineName + "/autotask/ZionAppWebService.asmx/RemoveUserFromGrou?NTID=" + NTID + "&GroupName=" + GroupName + "','" + DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss") + "')";
                    con.ExeNoQuery(strsql0, con.ConnAutoJob());
                }
                catch (Exception)
                {
                    throw;
                }
            }
            catch (Exception e)
            {
                verify = false;
            }
            finally
            {
                //p.Close();
                GC.Collect();
            }
            HttpContext.Current.Response.Write(HttpContext.Current.Request["callback"] + "({'Results':" + ToJSON(verify) + "})");
        }

        //[WebMethod(Description = "黄埔用户权限添加(从组--黄埔OU中添加用户)")]
        //public bool AddUserFromGroup(string NTID, string GroupName)
        //{
        //    string results = "";
        //    bool verify = true;
        //    try
        //    {
        //        string pwd = ConfigurationManager.AppSettings.Get("ADPwd");
        //        string script = @"Invoke-Command -ScriptBlock{$Username = 'q1_1290656';$Password = '" + pwd + "';$Pass = ConvertTo-SecureString $Password -AsPlainText -Force;$Cred = New-Object System.Management.Automation.PSCredential -ArgumentList $Username,$Pass;Add-ADGroupMember -Identity '" + GroupName + "' -Members '" + NTID.Trim() + "' -Credential $Cred}";
        //        results = RunScript(script);
        //        if (string.IsNullOrEmpty(results))
        //        {
        //            verify = true;
        //            //授权成功，记录数据
        //            try
        //            {
        //                Conn con = new Conn();
        //                string strsql0 = "Insert into MainAPI_Action_Record values('AddUserFromGroup_'" + HttpContext.Current.Request.UserHostName + "_" + HttpContext.Current.Request.LogonUserIdentity + " ,'NTID=" + NTID + "&GroupName=" + GroupName + "','http://" + Environment.MachineName + ",'NTID=" + NTID + "&GroupName=" + GroupName + "','http://" + Environment.MachineName + "/autotask/ZionAppWebService.asmx/RemoveUserFromGrou?NTID=" + NTID + "&GroupName=" + GroupName + "','" + DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss") + "')";
        //                con.ExeNoQuery(strsql0, con.ConnAutoJob());
        //            }
        //            catch (Exception)
        //            {
        //                throw;
        //            }
        //        }
        //    }
        //    catch (Exception e)
        //    {
        //        verify = false;
        //    }
        //    finally
        //    {
        //        //p.Close();
        //        GC.Collect();
        //    }
        //    return verify;
        //}

        [WebMethod(Description = "黄埔电脑管理员权限添加(添加电脑到组(HUA Default OU Policy Deny))")]
        public void AddPC2Admin(string PCName)
        {
            string results = "";
            bool flag = true;
            try
            {
                string pwd = ConfigurationManager.AppSettings.Get("ADPwd");
                string script = @"Invoke-Command -ScriptBlock{$Username = 'q1_1290656';$Password = '" + pwd + @"';$Pass = ConvertTo-SecureString $Password -AsPlainText -Force;$Cred = New-Object System.Management.Automation.PSCredential -ArgumentList $Username,$Pass;
$dc = Get-ADDomainController -Discover -Service PrimaryDC;$ou = Get-ADComputer " + PCName + " -Properties DistinguishedName;Add-ADPrincipalGroupMembership -Identity:$ou.DistinguishedName -MemberOf:'CN=HUA Default OU Policy Deny, OU=Groups, OU=Huangpu, OU=RegionAsia, DC=corp, DC=JABIL, DC=ORG' -Server:$dc.Name -Credential $Cred }";
                results = RunScript(script);
                flag = true;
                //授权成功，记录数据
                try
                {
                    Conn con = new Conn();
                    string strsql0 = "Insert into MainAPI_Action_Record values('AddPC2Admin_'" + HttpContext.Current.Request.UserHostName + "_" + HttpContext.Current.Request.LogonUserIdentity + " ,'http://" + Environment.MachineName + ",'PCName=" + PCName + "','http://" + Environment.MachineName + "/autotask/ZionAppWebService.asmx/AddPC2Admin?PCName=" + PCName + "','" + DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss") + "')";
                    con.ExeNoQuery(strsql0, con.ConnAutoJob());
                }
                catch (Exception)
                {
                    throw;
                }
            }
            catch (Exception e)
            {
                flag = false;
            }
            finally
            {
                GC.Collect();
            }
            HttpContext.Current.Response.Write(HttpContext.Current.Request["callback"] + "({'Results':" + ToJSON(flag) + "})");
        }


        [WebMethod(Description = "今日普通Case分配情况(按分配人员统计)")]
        public void AssignedTicketTodayByAssignedTo()
        {
            string strsql0 = "select Count(AssignedTo) as Count,AssignedTo from AutoTicket WITH(NOLOCK) where AssignedDate>=convert(varchar(10),Getdate(),120) and AssignedDate<convert(varchar(10),dateadd(d,1,Getdate()),120) group by AssignedTo";
            try
            {
                DataSet ds0 = AutoTicketLib.dataset(strsql0);
                //Dictionary<string, string> lt = new Dictionary<string, string>();
                ArrayList arraylist = new ArrayList();
                if (ds0.Tables[0].Rows.Count > 0)
                {
                    //for (int i = 0; i < ds0.Tables[0].Rows.Count; i++)
                    //{
                    //    arraylist.Add(ds0.Tables[0].Rows[i]);
                    //}
                    HttpContext.Current.Response.Write(DatasetToJson(ds0));
                }
            }
            catch (Exception ex0)
            {
                throw new Exception(ex0.Message);
            }
        }

        #region DataSet/DataTable To Json
        /// <summary>
        /// table转json
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static string DataTableToJson(DataTable dt)
        {
            StringBuilder jsonBuilder = new StringBuilder();
            jsonBuilder.Append("{\"Name\":\"" + dt.TableName + "\",\"Rows");
            jsonBuilder.Append("\":[");
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                jsonBuilder.Append("{");
                for (int j = 0; j < dt.Columns.Count; j++)
                {
                    jsonBuilder.Append("\"");
                    jsonBuilder.Append(dt.Columns[j].ColumnName);
                    jsonBuilder.Append("\":\"");
                    jsonBuilder.Append(dt.Rows[i][j].ToString().Replace("\"", "\\\""));
                    jsonBuilder.Append("\",");
                }
                jsonBuilder.Remove(jsonBuilder.Length - 1, 1);
                jsonBuilder.Append("},");
            }
            jsonBuilder.Remove(jsonBuilder.Length - 1, 1);
            jsonBuilder.Append("]");
            jsonBuilder.Append("}");
            return jsonBuilder.ToString();
        }


        /// <summary>
        /// dataset转Json
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        public static string DatasetToJson(System.Data.DataSet ds)
        {
            StringBuilder json = new StringBuilder();
            json.Append("{\"Tables\":");
            json.Append("[");
            foreach (System.Data.DataTable dt in ds.Tables)
            {
                json.Append(DataTableToJson(dt));
                json.Append(",");
            }
            json.Remove(json.Length - 1, 1);
            json.Append("]");
            json.Append("}");
            return json.ToString();
        }
        #endregion

        [WebMethod(Description = "今日普通Case分配情况(按Case类型和AssignedTo统计--Incident)")]
        public void AssignedTicketTodayByINC()
        {
            //string strsql0 = "select Count(TaskType) as Count,TaskType from AutoTicket where AssignedDate>=convert(varchar(10),Getdate(),120) and AssignedDate<convert(varchar(10),dateadd(d,1,Getdate()),120) group by TaskType";
            string strsql0 = "select Count(AssignedTo) as Count,AssignedTo from AutoTicket WITH(NOLOCK) where AssignedDate >=convert(varchar(10),Getdate(),120) and AssignedDate<convert(varchar(10),dateadd(d,1,Getdate()),120) and TaskType = 'Incident' group by AssignedTo";
            try
            {
                DataSet ds0 = AutoTicketLib.dataset(strsql0);
                //Dictionary<string, string> lt = new Dictionary<string, string>();
                ArrayList arraylist = new ArrayList();
                if (ds0.Tables[0].Rows.Count > 0)
                {
                    //for (int i = 0; i < ds0.Tables[0].Rows.Count; i++)
                    //{
                    //    arraylist.Add(ds0.Tables[0].Rows[i]);
                    //}
                    HttpContext.Current.Response.Write(DatasetToJson(ds0));
                }
            }
            catch (Exception ex0)
            {
                throw new Exception(ex0.Message);
            }
        }
        [WebMethod(Description = "今日普通Case分配情况(按Case类型和AssignedTo统计--SR)")]
        public void AssignedTicketTodayBySR()
        {
            //string strsql0 = "select Count(TaskType) as Count,TaskType from AutoTicket where AssignedDate>=convert(varchar(10),Getdate(),120) and AssignedDate<convert(varchar(10),dateadd(d,1,Getdate()),120) group by TaskType";
            string strsql0 = "select Count(AssignedTo) as Count,AssignedTo from AutoTicket WITH(NOLOCK) where AssignedDate >=convert(varchar(10),Getdate(),120) and AssignedDate<convert(varchar(10),dateadd(d,1,Getdate()),120) and TaskType = 'ServiceRequest' group by AssignedTo";
            try
            {
                DataSet ds0 = AutoTicketLib.dataset(strsql0);
                //Dictionary<string, string> lt = new Dictionary<string, string>();
                ArrayList arraylist = new ArrayList();
                if (ds0.Tables[0].Rows.Count > 0)
                {
                    //for (int i = 0; i < ds0.Tables[0].Rows.Count; i++)
                    //{
                    //    arraylist.Add(ds0.Tables[0].Rows[i]);
                    //}
                    HttpContext.Current.Response.Write(DatasetToJson(ds0));
                }
            }
            catch (Exception ex0)
            {
                throw new Exception(ex0.Message);
            }
        }
        [WebMethod(Description = "BT Case年度分配情况(按AssignedTo统计)")]
        public void AssignedBTCaseYearByAssignedTo()
        {
            //string strsql0 = "select Count(TaskType) as Count,TaskType from AutoTicket where AssignedDate>=convert(varchar(10),Getdate(),120) and AssignedDate<convert(varchar(10),dateadd(d,1,Getdate()),120) group by TaskType";
            string strsql0 = "select Count(Number),AssignTo from AutoAssignBTCase WITH(NOLOCK) where YEAR(RunTime) = YEAR(GETDATE()) group by AssignTo";
            try
            {
                DataSet ds0 = AutoTicketLib.dataset(strsql0);
                //Dictionary<string, string> lt = new Dictionary<string, string>();
                ArrayList arraylist = new ArrayList();
                if (ds0.Tables[0].Rows.Count > 0)
                {
                    //for (int i = 0; i < ds0.Tables[0].Rows.Count; i++)
                    //{
                    //    arraylist.Add(ds0.Tables[0].Rows[i]);
                    //}
                    HttpContext.Current.Response.Write(DatasetToJson(ds0));
                }
            }
            catch (Exception ex0)
            {
                throw new Exception(ex0.Message);
            }
        }
        [WebMethod(Description = "今日普通Case最新情况(按Case最后分配时间统计)")]
        public void AssignedTicketTodayByLastAssignedDate()
        {
            string strsql0 = "select max(AssignedDate) as AssignedDate,TaskType from AutoTicket WITH(NOLOCK) group by TaskType";
            try
            {
                DataSet ds0 = AutoTicketLib.dataset(strsql0);
                //Dictionary<string, string> lt = new Dictionary<string, string>();
                ArrayList arraylist = new ArrayList();
                if (ds0.Tables[0].Rows.Count > 0)
                {
                    //for (int i = 0; i < ds0.Tables[0].Rows.Count; i++)
                    //{
                    //    arraylist.Add(ds0.Tables[0].Rows[i]);
                    //}
                    HttpContext.Current.Response.Write(DatasetToJson(ds0));
                }
            }
            catch (Exception ex0)
            {
                throw new Exception(ex0.Message);
            }
        }
        [WebMethod(Description = "Case年度分配情况(按月份统计)")]
        public void AssignedTicketYearByAssignedTo()
        {
            string strsql0 = @"select Count(Number) as Count,TaskType,MONTH(AssignedDate) as month_mm 
from AutoTicket WITH(NOLOCK)
where YEAR(AssignedDate) = YEAR(GETDATE())
group by TaskType,MONTH(AssignedDate)
order by MONTH(AssignedDate)";
            try
            {
                DataSet ds0 = AutoTicketLib.dataset(strsql0);
                //Dictionary<string, string> lt = new Dictionary<string, string>();
                ArrayList arraylist = new ArrayList();
                if (ds0.Tables[0].Rows.Count > 0)
                {
                    //for (int i = 0; i < ds0.Tables[0].Rows.Count; i++)
                    //{
                    //    arraylist.Add(ds0.Tables[0].Rows[i]);
                    //}
                    HttpContext.Current.Response.Write(DatasetToJson(ds0));
                }
            }
            catch (Exception ex0)
            {
                throw new Exception(ex0.Message);
            }
        }
        [WebMethod(Description = "MoveOUJob(总计)")]
        public void MoveOUTotal()
        {
            string strsql0 = @"select count(ComputerName) as Count from MoveOU WITH(NOLOCK)";
            try
            {
                DataSet ds0 = AutoTicketLib.dataset(strsql0);
                //Dictionary<string, string> lt = new Dictionary<string, string>();
                ArrayList arraylist = new ArrayList();
                if (ds0.Tables[0].Rows.Count > 0)
                {
                    //for (int i = 0; i < ds0.Tables[0].Rows.Count; i++)
                    //{
                    //    arraylist.Add(ds0.Tables[0].Rows[i]);
                    //}
                    HttpContext.Current.Response.Write(DatasetToJson(ds0));
                }
            }
            catch (Exception ex0)
            {
                throw new Exception(ex0.Message);
            }
        }
    }

}
