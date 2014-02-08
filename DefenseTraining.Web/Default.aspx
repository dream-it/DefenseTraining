<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeBehind="Default.aspx.cs" Inherits="DefenseTraining.Web._Default" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
    Dashboard
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <link href="Styles/jquery.ui.theme.css" rel="stylesheet" type="text/css" />
    <link href="Styles/list-page.css" rel="stylesheet" type="text/css" />
    <link href="Styles/style.css" media="screen" rel="stylesheet" type="text/css" />
    <script type="text/javascript">
        $(document).ready(function () {
            doSearch();
        });

        setInterval("doSearch()", 60 * 1000);

        function doSearch() {
            loadReminders();
        }

        function loadReminders() {
            $.ajax({
                type: "POST",
                url: "Default.aspx/GetReminders",
                data: '{}',
                contentType: "application/json; charset=utf-8",
                async: false,
                dataType: "json",
                success: function (data) {
                    if (data.d.IsSuccessful) {
                        var reminders = data.d.Data;
                        populateReminders(reminders);
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

        function populateReminders(reminders) {
            $("#reminderContainer > tbody").empty();
            $("#tmplReminder").tmpl(reminders).appendTo("#reminderContainer > tbody");
        }

        function openWindow(id) {
            var wTitle = "Edit Event [" + id + "]";
            $.window({
                showModal: true,
                title: wTitle,
                url: "EventEdit.aspx?Id=" + id,
                height: 440,
                width: 570,
                resizable: false,
                maximizable: false,
                bookmarkable: false,
                showRoundCorner: true
            });
        }

        function dismissData(eventId, remindFor) {
            if (confirm("Are you sure you want to dismiss this reminder?") == true) {
                $.ajax({
                    type: "POST",
                    url: "Default.aspx/DismissReminder",
                    data: JSON.stringify({ eventId: eventId, remindFor: remindFor }),
                    contentType: "application/json; charset=utf-8",
                    async: false,
                    dataType: "json",
                    success: function (data) {
                        if (data.d.IsSuccessful) {
                            loadReminders();
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
        }     
    </script>
    <script id="tmplReminder" type="text/html">
        <tr>
            <td>
                ${EventName}
            </td>
            <td>
                ${RemindFor}
            </td>
            <td>
                ${RemindOn}
            </td>
            <td style="text-align: center;">
                <img src="Styles/Images/DeleteIcon.jpg" alt="Delete" style="cursor: pointer" title="Delete" onclick="dismissData(${EventId}, '${RemindFor}');"/>
            </td>
        </tr>
    </script>
    <div style="min-height: 400px;">
        <fieldset>
            <legend>Action Pending</legend>
            <input type="button" style="margin-bottom: 5px; width: 150px;" value="Preview Pending Actions" onclick="openReminderViewer('');" />
            <input type="button" style="margin-bottom: 5px; width: 170px;" value="Preview Allotment Statement" onclick="openReminderViewer('alotmentStatement');" />
            <table class="itemTable" id="reminderContainer">
                <thead>
                    <tr>
                        <th style="width: 35%;">
                            Event Name
                        </th>
                        <th style="width: 50%;">
                            Remind For
                        </th>
                        <th style="width: 10%;">
                            Remind On
                        </th>
                        <th style="text-align: center; width: 5%;">
                            Dismiss
                        </th>
                    </tr>
                </thead>
                <tbody>
                </tbody>
            </table>
        </fieldset>
    </div>
</asp:Content>
