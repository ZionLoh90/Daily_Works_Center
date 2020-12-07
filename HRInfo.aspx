<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="HRInfo.aspx.cs" Inherits="ITWorkShopHomePage.HRInfo" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>JABIL黄埔内部员工信息查询系统</title>
    <link href="Common/button.css" rel="stylesheet" />
    <script src="Common/jquery.min.js"></script>
    <style>
        .form {
            width: 94%;
            height: 100%;
            margin-left: 3%;
            margin-right: 3%;
            margin-top: 10px;
        }

        .title {
            text-align: center;
            font-style: italic;
        }

        .notice {
            text-align: center;
            color: red;
        }

        .select {
            /*margin-left: 12%;*/
            font-size: 12pt;
            text-align:center;
            width: 140px;
            height: 30px;
            text-align: center;
        }

        .input {
            font-size: 12pt;
            width: 200px;
            height: 30px;
            text-align: center;
        }

        #content_center {
            height: 60px;
            margin-left: 35%;
            /*margin-right: 20%;*/
        }

        #content_bottom {
            margin-top: 15px;
            width: 100%;
            height: 480px;
            overflow:auto;
        }

        .mytable {
            margin-top:10px;
            width: 100%;
            table-layout:auto;
            padding: 0;
            margin: 0;
        }

        caption {
            padding: 0 0 5px 0;
            width: 100%;
            font: italic 14px "Trebuchet MS", Verdana, Arial, Helvetica, sans-serif;
            text-align: right;
        }

        th {
            font: bold 11px "Trebuchet MS", Verdana, Arial, Helvetica, sans-serif;
            color: #4f6b72;
            border-right: 1px solid #C1DAD7;
            border-bottom: 1px solid #C1DAD7;
            border-top: 1px solid #C1DAD7;
            letter-spacing: 2px;
            text-transform: uppercase;
            text-align: left;
            padding: 6px 6px 6px 12px;
            background: #CCE8EA; /*#CAE8EA no-repeat;*/
            font-size:14px;
        }

            th.nobg {
                border-top: 0;
                border-left: 0;
                border-right: 1px solid #C1DAD7;
                background: none;
            }

        td {
            border-right: 1px solid #C1DAD7;
            border-bottom: 1px solid #C1DAD7;
            background: #fff;
            font-size: 12px;
            padding: 6px 6px 6px 12px;
            color: #4f6b72;
        }


            td.alt {
                background: #F5FAFA;
                color: #797268;
            }

        th.spec {
            border-left: 1px solid #C1DAD7;
            border-top: 0;
            background: #fff no-repeat;
            font: bold 10px "Trebuchet MS", Verdana, Arial, Helvetica, sans-serif;
        }

        th.specalt {
            border-left: 1px solid #C1DAD7;
            border-top: 0;
            background: #f5fafa no-repeat;
            font: bold 10px "Trebuchet MS", Verdana, Arial, Helvetica, sans-serif;
            color: #797268;
        }


    </style>
    <script lang="javascript" type="text/javascript">
        $(document).ready(function () {            
            $("#select").change(function () {
                var selectedIndex = $(this).get(0).selectedIndex;
                if (selectedIndex == 0) { document.getElementById("searchtxt").value = ""; document.getElementById("searchtxt").setAttribute("placeholder", "请输入卡号"); }
                if (selectedIndex == 1) { document.getElementById("searchtxt").value = ""; document.getElementById("searchtxt").setAttribute("placeholder", "请输入SAP工号"); }
                if (selectedIndex == 2) { document.getElementById("searchtxt").value = ""; document.getElementById("searchtxt").setAttribute("placeholder", "请输入NTID"); }
                if (selectedIndex == 3) { document.getElementById("searchtxt").value = ""; document.getElementById("searchtxt").setAttribute("placeholder", "请输入邮箱"); }
                if (selectedIndex == 4) { document.getElementById("searchtxt").value = ""; document.getElementById("searchtxt").setAttribute("placeholder", "请输入SAP工号(查询上级)"); }
            });
            $("#searchtxt").on('keypress', function (e) {
                if (event.keyCode == 13) {
                    event.cancelBubble = true;
                    event.returnValue = false;
                    $(this).parents("div").find("#searchbtn").click();
                }
            });
        });
    </script>
</head>
<body>
    <form id="form1" runat="server" class="form">
        <div id="content_top">
            <h1 class="title">JABIL黄埔内部员工信息查询系统</h1>
            <h3 class="notice">注：内部员工信息属于保密信息,请勿外泄！</h3>
        </div>
        <hr />
        <div id="content_center">
            <select id="select" class="select red mini">
		        <option value="CardNo" selected="selected">卡号</option>
                <option value="SAPNo">SAP工号</option>
                <option value="NTID">NTID</option>
                <option value="Email">邮箱</option>
                <option value="Leader">查询用户上级</option>
            </select>
            <input id="searchtxt" class="input" name="searchtxt" placeholder="请输入查询条件" />
            <input id="searchbtn" name="searchtxt" type="button" class="btn-gradient red mini" value="查询" />
        </div>
        <div id="content_bottom">
            <table id="table1" class="mytable">
                <tr>
                    <th>查询参数</th>
                    <th>NTID</th>
                    <th>SAP工号</th>
                    <th>旧工号</th>
                    <th>Workday工号</th>
                    <th>中文名</th>
                    <th>英文名</th>
                    <th>在职状态</th>
                    <th>邮箱</th>
                    <th>部门</th>
                    <th>成本中心</th>
                    <th>电话</th>
                </tr>
            </table>   
        </div>
        <script>
            var temp;
            var click = document.getElementById("searchbtn");
            click.onclick = function () {
                temp = document.getElementById("searchtxt").value;
                var input = temp.replace(/(^\s*)|(\s*$)/g, '');//去除空格;
                if (input == "" || input == undefined || input == null) {
                    alert("查询条件不能为空！");
                    $("#searchtxt").focus();
                    return;
                }
                var param = $("#select").val();
                switch (param) {
                    case "CardNo":
                        getuserbycard(input);
                        break;
                    case "SAPNo":
                        getuserbysap(input);
                        break;
                    case "NTID":
                        getuserbyntid(input);
                        break;
                    case "Email":
                        getuserbymail(input);
                        break;
                    case "Leader":
                        getleader(input);
                        break;
                }
                document.getElementById("searchtxt").value = "";
            }

            var _0x5b4a = ['<tr><td>', 'CostCenter', 'Records', '#select', 'append', 'text', 'ajax', 'EmploymentStatus', 'OldNo', 'EnglishName', 'option:selected', '</td><td>', 'http://cnhuam0appprd01/WebAPIHR/api/Employee/GetEmployeeInfo?SAPNo=&Ntid=', 'success', 'find', '查询出错:', 'NTID', 'WDID', 'ChineseName', 'Mobile', 'ErrorMsg', 'Get', 'Mail', 'EmployeeId', 'WorkcellDescription', '#table1']; var _0x31ef = function (_0x5b4a53, _0x31efe2) { _0x5b4a53 = _0x5b4a53 - 0x0; var _0x4d9087 = _0x5b4a[_0x5b4a53]; return _0x4d9087; }; function getuserbycard(_0x115407) { $['ajax']({ 'type': 'Get', 'url': 'http://cnhuam0appprd01/WebAPIHR/api/Employee/GetEmployeeInfoByCardNo?CardNo=' + _0x115407 + '', 'success': function (_0x2887c6, _0x248db1) { if (_0x248db1 == 'success') { var _0x148404; var _0x42e2f1 = $('#select')[_0x31ef('0xe')](_0x31ef('0xa')); var _0xed7031 = _0x42e2f1[_0x31ef('0x5')]() + '(' + temp + ')'; _0x148404 = _0x31ef('0x0') + _0xed7031 + '</td><td>' + _0x2887c6[_0x31ef('0x2')]['0']['NTID'] + _0x31ef('0xb') + _0x2887c6['Records']['0']['EmployeeId'] + _0x31ef('0xb') + _0x2887c6[_0x31ef('0x2')]['0'][_0x31ef('0x8')] + '</td><td>' + _0x2887c6[_0x31ef('0x2')]['0']['WDID'] + '</td><td>' + _0x2887c6[_0x31ef('0x2')]['0']['ChineseName'] + '</td><td>' + _0x2887c6['Records']['0']['EnglishName'] + '</td><td>' + _0x2887c6[_0x31ef('0x2')]['0']['EmploymentStatus'] + _0x31ef('0xb') + _0x2887c6['Records']['0']['Mail'] + _0x31ef('0xb') + _0x2887c6['Records']['0']['WorkcellDescription'] + '</td><td>' + _0x2887c6[_0x31ef('0x2')]['0']['CostCenter'] + _0x31ef('0xb') + _0x2887c6['Records']['0']['Mobile'] + '</td></tr>'; $('#table1')[_0x31ef('0x4')](_0x148404); } }, 'error': function (_0x273d4d) { alert(_0x31ef('0xf') + data['ErrorMsg']); }, 'complete': function () { } }); }; function getleader(_0x591f02) { $[_0x31ef('0x6')]({ 'type': _0x31ef('0x15'), 'url': 'http://cnhuam0appprd01/WebAPIHR/api/Employee/GetSupervisorInfo?SAPNo=' + _0x591f02 + '', 'success': function (_0x5a4115, _0x37d75f) { if (_0x37d75f == _0x31ef('0xd')) { var _0x564e8b; var _0x2c53db = $('#select')[_0x31ef('0xe')](_0x31ef('0xa')); var _0x19238e = _0x2c53db[_0x31ef('0x5')]() + '(' + temp + ')'; _0x564e8b = '<tr><td>' + _0x19238e + '</td><td>' + _0x5a4115[_0x31ef('0x2')]['0']['NTID'] + '</td><td>' + _0x5a4115[_0x31ef('0x2')]['0']['EmployeeId'] + '</td><td>' + _0x5a4115[_0x31ef('0x2')]['0']['OldNo'] + _0x31ef('0xb') + _0x5a4115['Records']['0']['WDID'] + _0x31ef('0xb') + _0x5a4115['Records']['0']['ChineseName'] + '</td><td>' + _0x5a4115[_0x31ef('0x2')]['0']['EnglishName'] + '</td><td>' + _0x5a4115['Records']['0']['EmploymentStatus'] + '</td><td>' + _0x5a4115[_0x31ef('0x2')]['0']['Mail'] + _0x31ef('0xb') + _0x5a4115[_0x31ef('0x2')]['0']['WorkcellDescription'] + _0x31ef('0xb') + _0x5a4115['Records']['0']['CostCenter'] + '</td><td>' + _0x5a4115['Records']['0'][_0x31ef('0x13')] + '</td></tr>'; $(_0x31ef('0x19'))['append'](_0x564e8b); } }, 'error': function (_0x49210a) { alert('查询出错:' + data['ErrorMsg']); }, 'complete': function () { } }); }; function getuserbysap(_0x17a898) { $['ajax']({ 'type': _0x31ef('0x15'), 'url': 'http://cnhuam0appprd01/WebAPIHR/api/Employee/GetEmployeeInfo?SAPNo=' + _0x17a898 + '&Ntid=&EMail=', 'success': function (_0x11533c, _0x24f278) { if (_0x24f278 == _0x31ef('0xd')) { var _0x59b321; var _0x5bdbeb = $(_0x31ef('0x3'))['find']('option:selected'); var _0x320def = _0x5bdbeb[_0x31ef('0x5')]() + '(' + temp + ')'; _0x59b321 = _0x31ef('0x0') + _0x320def + _0x31ef('0xb') + _0x11533c['Records']['0'][_0x31ef('0x10')] + _0x31ef('0xb') + _0x11533c['Records']['0']['EmployeeId'] + _0x31ef('0xb') + _0x11533c['Records']['0'][_0x31ef('0x8')] + _0x31ef('0xb') + _0x11533c['Records']['0'][_0x31ef('0x11')] + '</td><td>' + _0x11533c[_0x31ef('0x2')]['0']['ChineseName'] + _0x31ef('0xb') + _0x11533c[_0x31ef('0x2')]['0'][_0x31ef('0x9')] + '</td><td>' + _0x11533c['Records']['0']['EmploymentStatus'] + '</td><td>' + _0x11533c['Records']['0']['Mail'] + '</td><td>' + _0x11533c[_0x31ef('0x2')]['0']['WorkcellDescription'] + _0x31ef('0xb') + _0x11533c[_0x31ef('0x2')]['0'][_0x31ef('0x1')] + '</td><td>' + _0x11533c[_0x31ef('0x2')]['0'][_0x31ef('0x13')] + '</td></tr>'; $(_0x31ef('0x19'))[_0x31ef('0x4')](_0x59b321); } }, 'error': function (_0x447590) { alert(_0x31ef('0xf') + data['ErrorMsg']); }, 'complete': function () { } }); }; function getuserbyntid(_0x57bb93) { $[_0x31ef('0x6')]({ 'type': 'Get', 'url': _0x31ef('0xc') + _0x57bb93 + '&EMail=', 'success': function (_0x26ec2d, _0x629bda) { if (_0x629bda == _0x31ef('0xd')) { var _0x2d901c; var _0x4032e2 = $(_0x31ef('0x3'))[_0x31ef('0xe')]('option:selected'); var _0x440938 = _0x4032e2['text']() + '(' + temp + ')'; _0x2d901c = _0x31ef('0x0') + _0x440938 + _0x31ef('0xb') + _0x26ec2d[_0x31ef('0x2')]['0'][_0x31ef('0x10')] + _0x31ef('0xb') + _0x26ec2d['Records']['0']['EmployeeId'] + _0x31ef('0xb') + _0x26ec2d[_0x31ef('0x2')]['0'][_0x31ef('0x8')] + _0x31ef('0xb') + _0x26ec2d[_0x31ef('0x2')]['0'][_0x31ef('0x11')] + _0x31ef('0xb') + _0x26ec2d['Records']['0'][_0x31ef('0x12')] + _0x31ef('0xb') + _0x26ec2d['Records']['0']['EnglishName'] + _0x31ef('0xb') + _0x26ec2d[_0x31ef('0x2')]['0'][_0x31ef('0x7')] + '</td><td>' + _0x26ec2d['Records']['0'][_0x31ef('0x16')] + _0x31ef('0xb') + _0x26ec2d[_0x31ef('0x2')]['0'][_0x31ef('0x18')] + '</td><td>' + _0x26ec2d[_0x31ef('0x2')]['0']['CostCenter'] + _0x31ef('0xb') + _0x26ec2d[_0x31ef('0x2')]['0'][_0x31ef('0x13')] + '</td></tr>'; $('#table1')['append'](_0x2d901c); } }, 'error': function (_0x4e7109) { alert('查询出错:' + data['ErrorMsg']); }, 'complete': function () { } }); }; function getuserbymail(_0x40bdee) { $['ajax']({ 'type': 'Get', 'url': 'http://cnhuam0appprd01/WebAPIHR/api/Employee/GetEmployeeInfo?SAPNo=&Ntid=&EMail=' + _0x40bdee + '', 'success': function (_0x5af752, _0x4a24ca) { if (_0x4a24ca == 'success') { var _0x318dfc; var _0xa52c92 = $(_0x31ef('0x3'))['find']('option:selected'); var _0x38320a = _0xa52c92[_0x31ef('0x5')]() + '(' + temp + ')'; _0x318dfc = _0x31ef('0x0') + _0x38320a + _0x31ef('0xb') + _0x5af752['Records']['0'][_0x31ef('0x10')] + '</td><td>' + _0x5af752[_0x31ef('0x2')]['0'][_0x31ef('0x17')] + '</td><td>' + _0x5af752['Records']['0']['OldNo'] + _0x31ef('0xb') + _0x5af752[_0x31ef('0x2')]['0']['WDID'] + _0x31ef('0xb') + _0x5af752[_0x31ef('0x2')]['0'][_0x31ef('0x12')] + _0x31ef('0xb') + _0x5af752[_0x31ef('0x2')]['0'][_0x31ef('0x9')] + _0x31ef('0xb') + _0x5af752['Records']['0'][_0x31ef('0x7')] + '</td><td>' + _0x5af752[_0x31ef('0x2')]['0']['Mail'] + '</td><td>' + _0x5af752[_0x31ef('0x2')]['0']['WorkcellDescription'] + '</td><td>' + _0x5af752[_0x31ef('0x2')]['0']['CostCenter'] + '</td><td>' + _0x5af752['Records']['0']['Mobile'] + '</td></tr>'; $('#table1')['append'](_0x318dfc); } }, 'error': function (_0x44482a) { alert('查询出错:' + data[_0x31ef('0x14')]); }, 'complete': function () { } }); };

        </script>
    </form>
</body>


</html>
