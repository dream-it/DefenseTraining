<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="EventList.aspx.cs" Inherits="DefenseTraining.Web.EventList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    Events
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <link href="Styles/jquery.ui.theme.css" rel="stylesheet" type="text/css" />
    <link href="Styles/list-page.css" rel="stylesheet" type="text/css" />
    <link href="Styles/style.css" media="screen" rel="stylesheet" type="text/css" />
    <script src="Scripts/jquery.paginate.js" type="text/javascript"></script>
    <style type="text/css">
        .demo
        {
            text-align: center;
            background-image: url("Styles/images/ui-bg_highlight-soft_75_cccccc_1x100.png");
            background-repeat: repeat-x;
            padding: 3px 5px 3px 5px;
            border: 1px solid silver;
            margin-bottom: 1px;
        }
    </style>
    <script type="text/javascript">
        var _totPages = 1;
        var _startIndex = 1;
        var _displayCount = 25;
        $(document).ready(function () {
            $("#divTable").hide();
            $("#divNoSearch").text("You didn't make any search yet.");
            $("#divNoSearch").show();
            $("#txtDateFrom").datepicker({ dateFormat: "dd M yy" });
            $("#txtDateTo").datepicker({ dateFormat: "dd M yy" });
            allowOnlyNumeric();
            loadEventTypes();
            loadGenres();
            loadSpecialities();
            loadCountries();
            loadRanks();
        });

        function loadEventTypes() {
            $.ajax({
                type: "POST",
                url: "EventTypeList.aspx/GetEventTypes",
                data: '{}',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                async: false,
                success: function (data) {
                    if (data.d.IsSuccessful) {
                        var options = $("#cmbEventTypes");
                        options.append($("<option />").val(0).text("All"));
                        $.each(data.d.Data, function () {
                            options.append($("<option />").val(this.Id).text(this.Name));
                        });
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

        function loadGenres() {
            $.ajax({
                type: "POST",
                url: "GenreList.aspx/GetGenres",
                data: '{}',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                async: false,
                success: function (data) {
                    if (data.d.IsSuccessful) {
                        var options = $("#cmbGenres");
                        options.append($("<option />").val(0).text("All"));
                        $.each(data.d.Data, function () {
                            options.append($("<option />").val(this.Id).text(this.Name));
                        });
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

        function loadSpecialities() {
            $.ajax({
                type: "POST",
                url: "SpecialityList.aspx/GetSpecialities",
                data: '{}',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                async: false,
                success: function (data) {
                    if (data.d.IsSuccessful) {
                        var options = $("#cmbSpecialities");
                        options.append($("<option />").val(0).text("All"));
                        $.each(data.d.Data, function () {
                            options.append($("<option />").val(this.Id).text(this.Name));
                        });
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

        function loadCountries() {
            $.ajax({
                type: "POST",
                url: "CountryList.aspx/GetCountries",
                data: '{}',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                async: false,
                success: function (data) {
                    if (data.d.IsSuccessful) {
                        var options = $("#cmbCountries");
                        options.append($("<option />").val(0).text("All"));
                        $.each(data.d.Data, function () {
                            options.append($("<option />").val(this.Id).text(this.Name));
                        });
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

        function loadRanks() {
            $.ajax({
                type: "POST",
                url: "RankList.aspx/GetRanks",
                data: '{}',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                async: false,
                success: function (data) {
                    if (data.d.IsSuccessful) {
                        var options = $("#cmbRanks");
                        options.append($("<option />").val(0).text("All"));
                        $.each(data.d.Data, function () {
                            options.append($("<option />").val(this.Id).text(this.Name));
                        });
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

        function openWindow(id) {
            var wTitle = "Edit Event [" + id + "]";
            if (id === 0) {
                wTitle = "New Event";
            }
            $.window({
                showModal: true,
                title: wTitle,
                url: "EventEdit.aspx?Id=" + id,
                height: 470,
                width: 770,
                resizable: false,
                maximizable: false,
                bookmarkable: false,
                showRoundCorner: true
            });
        }

        function doSearch() {
            var filter = createFilter();
            $.ajax({
                type: "POST",
                url: "EventList.aspx/GetEvents",
                data: JSON.stringify({ filter: filter }),
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                async: false,
                success: function (data) {
                    if (data.d.IsSuccessful) {
                        if (data.d.Data.length > 0) {
                            _totPages = Math.ceil(data.d.TotalCount / _displayCount);
                            $("#divTable").show();
                            $("#divNoSearch").hide();
                            $("#demo2").paginate({
                                count: _totPages,
                                start: _startIndex,
                                display: _displayCount,
                                border: false,
                                text_color: '#888',
                                background_color: '#EEE',
                                text_hover_color: 'black',
                                background_hover_color: '#CFCFCF',
                                onChange: function (page) {
                                    _startIndex = page;
                                    doSearch();
                                }
                            });
                        }
                        else {
                            $("#divTable").hide();
                            $("#divNoSearch").text("No data found on the search criteria.");
                            $("#divNoSearch").show();
                        }
                        $("#dataContainer > tbody").empty();
                        $("#tmplData").tmpl(data.d.Data).appendTo("#dataContainer > tbody");
                    }
                    else {
                        alert(data.d.ErrorMessage);
                    }
                },
                error: function (msg) {
                    alert(msg.Error);
                }
            });
        }

        function createFilter() {
            var filter = new Object();
            if ($.trim($("#txtEventId").val()) == "") {
                filter.Id = 0;
            }
            else {
                filter.Id = parseInt($.trim($("#txtEventId").val()), 10);
            }
            filter.Name = $.trim($("#txtEventName").val());
            filter.EventTypeId = parseInt($("#cmbEventTypes").val(), 10);
            filter.GenreId = parseInt($("#cmbGenres").val(), 10);
            filter.SpecialityId = parseInt($("#cmbSpecialities").val(), 10);
            filter.CountryId = parseInt($("#cmbCountries").val(), 10);
            filter.RankId = parseInt($("#cmbRanks").val(), 10);
            filter.DateFrom = $.trim($("#txtDateFrom").val());
            filter.DateTo = $.trim($("#txtDateTo").val());
            filter.PersonalNo = $.trim($("#txtPersonalNo").val());
            filter.StartIndex = _startIndex;
            filter.DisplayCount = _displayCount;
            return filter;
        }

        function deleteData(id) {
            if (confirm("Are you sure you want to delete this record?") == true) {
                $.ajax({
                    type: "POST",
                    url: "EventList.aspx/DeleteEvent",
                    data: '{"id":' + id + '}',
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    async: false,
                    success: function (data) {
                        if (data.d.IsSuccessful) {
                            doSearch();
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

        function clearFilter() {
            $("#txtEventId").val("");
            $("#txtEventName").val("");
            $("#cmbEventTypes").val(0);
            $("#cmbGenres").val(0);
            $("#cmbSpecialities").val(0);
            $("#cmbCountries").val(0);
            $("#cmbRanks").val(0);
            $("#txtDateFrom").val("");
            $("#txtDateTo").val("");
            $("#txtPersonalNo").val("");
        }        
    </script>
    <script id="tmplData" type="text/html">
        <tr>
            <td>
                ${Type.Name}
            </td>
            <td>
                ${Genre.Name}
            </td>
            <td>
                ${Speciality.Name}
            </td>
            <td>
                ${Name}
            </td>
            <td>
                ${CommSparatedNominees}
            </td>
            <td>
                ${Country.Name}
            </td>
            <td>
                ${StartsOn} - ${EndsOn}
            </td>
            <td style="text-align: center;">
                <img src="Styles/Images/report-icon.png" width="18px" height="16px" alt="Show report" style="cursor: pointer;" title="Show report" onclick="openTrainingReportViewer(${Id}, 'Event');"/>
            </td>
            <td style="text-align: center;">
                <img src="Styles/Images/report-icon.png" width="18px" height="16px" alt="Show report" style="cursor: pointer;" title="Show report" onclick="openTrainingReportViewer(${Id}, 'GO');"/>
            </td>
            <td style="text-align: center;">
                <img src="Styles/Images/EditIcon.jpg" alt="Edit" style="cursor: pointer" title="Edit" onclick="openWindow(${Id});"/>
            </td>
            <td style="text-align: center;">
                <img src="Styles/Images/DeleteIcon.jpg" alt="Delete" style="cursor: pointer" title="Delete" onclick="deleteData(${Id});"/>
            </td>
        </tr>
    </script>
    <div>
        <div style="min-width: 410px;">
            <input type="button" value="Add New" onclick="openWindow(0)" style="float: left;" />
            <input type="button" value="Show Report" onclick="openTrainingReportViewer()" style="margin-left: 10px;" />
            <input type="button" value="Search" onclick="doSearch();" style="float: right;" />
            <input type="button" value="Clear Filter" onclick="clearFilter();" style="float: right;
                margin-right: 10px;" />
            <div class="searchPanel" style="margin-top: 4px;">
                <table border="0" cellspacing="0" cellpadding="0">
                    <colgroup>
                        <col width="8%" />
                        <col width="17%" />
                        <col width="8%" />
                        <col width="17%" />
                        <col width="8%" />
                        <col width="17%" />
                        <col width="8%" />
                        <col width="17%" />
                    </colgroup>
                    <tr>
                        <td class="Header ItemPaddingStyle" style="padding-left: 10px;">
                            EventId ID
                        </td>
                        <td class="ItemPaddingStyle">
                            <input type="text" id="txtEventId" class="numeric-only" />
                        </td>
                        <td class="Header ItemPaddingStyle" style="padding-left: 10px;">
                            Event Name
                        </td>                        
                        <td class="ItemPaddingStyle">
                            <input type="text" id="txtEventName" />
                        </td>
                        <td class="Header ItemPaddingStyle" style="padding-left: 10px;">
                            Personal No
                        </td>
                        <td class="ItemPaddingStyle">
                            <input type="text" id="txtPersonalNo" />
                        </td>
                        <td class="Header ItemPaddingStyle" style="padding-left: 10px;">
                            Rank
                        </td>
                        <td class="ItemPaddingStyle">
                            <select id="cmbRanks">
                            </select>
                        </td>
                    </tr>
                    <tr>
                        <td class="InputSpecial ItemPaddingStyle" style="padding-left: 10px;">
                            Event Type
                        </td>
                        <td class="ItemPaddingStyle">
                            <select id="cmbEventTypes">
                            </select>
                        </td>
                        <td class="Header ItemPaddingStyle" style="padding-left: 10px;">
                            Date From
                        </td>
                        <td class="ItemPaddingStyle">
                            <input type="text" id="txtDateFrom" />
                        </td>
                        <td class="Header ItemPaddingStyle" style="padding-left: 10px;">
                            Date To
                        </td>
                        <td class="ItemPaddingStyle">
                            <input type="text" id="txtDateTo" />
                        </td>
                        <td class="Header ItemPaddingStyle" style="padding-left: 10px;">
                            Event Genre
                        </td>
                        <td class="ItemPaddingStyle">
                            <select id="cmbGenres">
                            </select>
                        </td>
                    </tr>
                    <tr>
                        <td class="Header ItemPaddingStyle" style="padding-left: 10px;">
                            Speciality
                        </td>
                        <td class="ItemPaddingStyle">
                            <select id="cmbSpecialities">
                            </select>
                        </td>
                        <td class="Header ItemPaddingStyle" style="padding-left: 10px;">
                            Country
                        </td>
                        <td class="ItemPaddingStyle">
                            <select id="cmbCountries">
                            </select>
                        </td>
                    </tr>
                </table>
            </div>
            <div id="divNoSearch" style="color: Red;">
            </div>
            <div id="divTable">
                <div class="demo">
                    <div id="demo2">
                    </div>
                </div>
                <table class="itemTable" id="dataContainer">
                    <thead>
                        <tr>
                            <th>
                                Event Type
                            </th>
                            <th>
                                Genre
                            </th>
                            <th>
                                Speciality
                            </th>
                            <th>
                                What
                            </th>
                            <th>
                                Nominees
                            </th>
                            <th>
                                Where
                            </th>
                            <th>
                                When
                            </th>
                            <th style="text-align: center;">
                                Report
                            </th>
                            <th style="text-align: center;">
                                GO
                            </th>
                            <th style="text-align: center;">
                                Edit
                            </th>
                            <th style="text-align: center;">
                                Delete
                            </th>
                        </tr>
                    </thead>
                    <tbody>
                    </tbody>
                </table>
            </div>
        </div>
    </div>
</asp:Content>
