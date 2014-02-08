<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EventTypeEdit.aspx.cs"
    Inherits="DefenseTraining.Web.EventTypeEdit" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link rel="Stylesheet" href="Styles/Site.css" type="text/css" />
    <style type="text/css">
        input[type=text], select, textarea
        {
            width: 275px;
            margin-top: 2px;
            margin-bottom: 5px;
        }
        select
        {
            width: 279px;
        }
    </style>
    <script src="scripts/jquery.js" type="text/javascript"></script>
    <script type="text/javascript">
        var _eventType;

        $(document).ready(function () {
            var queryString = decodeURI(window.location.search.substring(1));
            populateEventCategories();
            loadEventType(queryString);
            populateFormData();
        });

        function populateEventCategories() {
            var categories = new Array();
            categories.push({ Id: 1, Name: "Course" });
            categories.push({ Id: 2, Name: "Seminar" });
            categories.push({ Id: 3, Name: "Visit" });
            categories.push({ Id: 4, Name: "SMEE" });
            categories.push({ Id: 5, Name: "Competition" });
            categories.push({ Id: 6, Name: "Excercise" });
            var options = $("#cmbEventCategories");
            options.append($("<option />").val(0).text("Please Select"));
            $.each(categories, function () {
                options.append($("<option />").val(this.Id).text(this.Name));
            });
        }

        function populateFormData() {
            $("#txtName").val(_eventType.Name);
            $("#cmbEventCategories").val(_eventType.Category);
        }

        function saveData() {
            _eventType.Name = $.trim($("#txtName").val());
            _eventType.Category = parseInt($("#cmbEventCategories").val(), 10);
            var msg = "";
            if (_eventType.Name == "") {
                msg += "Please enter name\n";
            }
            if (_eventType.Category == 0) {
                msg += "Please select a category\n";
            }
            if (msg != "") {
                alert(msg);
                return;
            }
            $.ajax({
                type: "POST",
                url: "EventTypeEdit.aspx/SaveEventType",
                data: '{"evtType":' + JSON.stringify(_eventType) + '}',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (data) {
                    if (data.d.IsSuccessful) {
                        _eventType = data.d.Data;
                        window.parent.populateData();
                    }
                    else {
                        alert(data.d.ErrorMessage);
                    }
                },
                error: function (msg) {
                    alert(msg);
                }
            });
        }

        function loadEventType(queryString) {
            var parms = queryString.split('&');
            var id = parms[0].substring(parms[0].indexOf("=") + 1);
            if (id == 0) {
                _eventType = new Object();
                return;
            }
            $.ajax({
                type: "POST",
                url: "EventTypeEdit.aspx/GetEventType",
                data: JSON.stringify({ id: id }),
                contentType: "application/json; charset=utf-8",
                async: false,
                dataType: "json",
                success: function (data) {
                    if (data.d.IsSuccessful) {
                        _eventType = data.d.Data;
                    }
                    else {
                        _eventType = new Object();
                        alert(data.d.ErrorMessage);
                    }
                },
                error: function (msg) {
                    alert(msg);
                }
            });
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div style="padding: 10px 20px;">
        <div>
            Name <span style="color: Red">*</span>
        </div>
        <div>
            <input type="text" id="txtName" />
        </div>
        <div>
            Category <span style="color: Red">*</span>
        </div>
        <div>
            <select id="cmbEventCategories">
            </select>
        </div>
        <div style="text-align: center; margin-top: 10px;">
            <input type="button" id="btnSave" value="Save" onclick="saveData()" />
        </div>
    </div>
    <asp:HiddenField ID="hdnMessage" runat="server" />
    </form>
</body>
</html>
