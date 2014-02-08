<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EventEdit.aspx.cs" Inherits="DefenseTraining.Web.EventEdit" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="Styles/jquery-ui.css" rel="stylesheet" type="text/css" />
    <link href="Styles/Site.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        input[type=text]
        {
            border: 1px solid #aaaaaa;
        }
        .text-box
        {
            width: 90%;
            margin-bottom: 5px;
        }
        .date-picker
        {
            width: 100px;
            margin-bottom: 5px;
        }
        .table
        {
            border-collapse: collapse;
            width: 100%;
        }
        .table th
        {
            background-image: url("Styles/images/ui-bg_highlight-soft_75_cccccc_1x100.png");
            background-repeat: repeat-x;
            border-width: 1px;
            padding: 3px 7px 4px 7px;
            border-style: solid;
            border-color: Silver;
            font-weight: normal;
            text-align: left;
            height: 20px;
        }
        .table td
        {
            border-width: 1px;
            padding: 3px 7px 4px 7px;
            border-style: solid;
            border-color: Silver;
        }
    </style>
    <script src="Scripts/jquery-1.9.1.js" type="text/javascript"></script>
    <script src="Scripts/jquery-ui.js" type="text/javascript"></script>
    <script src="Scripts/json2.js" type="text/javascript"></script>
    <script src="Scripts/jquery.tmpl.min.js" type="text/javascript"></script>
    <script src="Scripts/common.js" type="text/javascript"></script>
    <script type="text/javascript">
        var _event = new Object();
        var _nominees = new Array();
        var _reminders = new Array();
        var _param = "";
        var _url = "";

        $(document).ready(function () {
            $("#divDialog").dialog({
                autoOpen: false,
                modal: true
            });
            $("td > span > input").width(65);
            $("#divNominee").hide();
            $("#tabs").tabs();
            loadEventTypes();
            loadGenres();
            loadSpecialities();
            loadCountries();
            loadResponsibilities();
            loadRequiredDocs();
            loadRanks();
            loadUnits();
            loadBranches();
            loadAlotments();
            allowOnlyNumeric();
            $(".date-picker").datepicker({ dateFormat: "dd M yy" });
            var queryString = decodeURI(window.location.search.substring(1));
            var parms = queryString.split('&');
            var id = parseInt(parms[0].substring(parms[0].indexOf("=") + 1));
            if (id != 0) {
                loadEventData(id);
            }
        });

        function loadRequiredDocs() {
            $.ajax({
                type: "POST",
                url: "RequiredDocList.aspx/GetRequiredDocs",
                data: '{}',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                async: false,
                success: function (data) {
                    if (data.d.IsSuccessful) {
                        $("#divRequiredDocs > div").empty();
                        $("#tmplData").tmpl(data.d.Data).appendTo("#divRequiredDocs > div");
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

        function loadAlotments() {
            var alotments = [
                { Type: "Army", TypeVal: 1, Persons: 0 },
                { Type: "Navy", TypeVal: 2, Persons: 0 },
                { Type: "Airforce", TypeVal: 3, Persons: 0 },
                { Type: "DGFI", TypeVal: 4, Persons: 0 },
                { Type: "DSCSC", TypeVal: 5, Persons: 0 },
                { Type: "NDC", TypeVal: 6, Persons: 0 },
                { Type: "MIST", TypeVal: 7, Persons: 0 },
                { Type: "DGMS", TypeVal: 8, Persons: 0 },
                { Type: "BUP", TypeVal: 9, Persons: 0 },
                { Type: "Others", TypeVal: 10, Persons: 0 }
            ];
            $("#divInitAlotment > div").empty();
            $("#tmplAlotment").tmpl(alotments).appendTo("#divInitAlotment > div");
            $("#divReAlotment > div").empty();
            $("#tmplAlotment").tmpl(alotments).appendTo("#divReAlotment > div");
        }

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
                        options.append($("<option />").val(0).text("Please Select"));
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
                        options.append($("<option />").val(0).text("Please Select"));
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
                        options.append($("<option />").val(0).text("Please Select"));
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
                        options.append($("<option />").val(0).text("Please Select"));
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

        function loadResponsibilities() {
            $.ajax({
                type: "POST",
                url: "ResponsibilityList.aspx/GetResponsibilities",
                data: '{}',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                async: false,
                success: function (data) {
                    if (data.d.IsSuccessful) {
                        $("#divHostResponsibilities > div").empty();
                        $("#tmplData").tmpl(data.d.Data).appendTo("#divHostResponsibilities > div");
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
                        options.append($("<option />").val(0).text("Please Select"));
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

        function loadUnits() {
            $.ajax({
                type: "POST",
                url: "UnitList.aspx/GetUnits",
                data: '{}',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                async: false,
                success: function (data) {
                    if (data.d.IsSuccessful) {
                        var options = $("#cmbUnits");
                        options.append($("<option />").val(0).text("Please Select"));
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

        function loadBranches() {
            $.ajax({
                type: "POST",
                url: "BranchList.aspx/GetBranches",
                data: '{}',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                async: false,
                success: function (data) {
                    if (data.d.IsSuccessful) {
                        var options = $("#cmbBranches");
                        options.append($("<option />").val(0).text("Please Select"));
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

        function bindNominees() {
            $("#tblNominees > tbody").empty();
            $("#tmplNominee").tmpl(_nominees).appendTo("#tblNominees > tbody");
        }

        function bindReminders() {
            $("#tblReminders > tbody").empty();
            $("#tmplReminder").tmpl(_reminders).appendTo("#tblReminders > tbody");
        }

        function loadEventData(id) {
            $.ajax({
                type: "POST",
                url: "EventEdit.aspx/GetEvent",
                data: '{ "id":' + JSON.stringify(id) + '}',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                async: false,
                success: function (data) {
                    if (data.d.IsSuccessful) {
                        _event = data.d.Data;
                        _nominees = _event.Nominees;
                        _reminders = _event.Reminders;
                        populateEventData();
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

        function populateEventData() {
            $("#txtEventName").val(_event.Name);
            $("#cmbEventTypes").val(_event.Type.Id);
            $("#divEventTypes > span > input").val(_event.Type.Name);
            $("#cmbGenres").val(_event.Genre.Id);
            $("#divGenres > span > input").val(_event.Genre.Name);
            $("#cmbSpecialities").val(_event.Speciality.Id);
            $("#divSpecialities > span > input").val(_event.Speciality.Name);
            $("#cmbCountries").val(_event.Country.Id);
            $("#divCountries > span > input").val(_event.Country.Name);
            $("#txtCity").val(_event.City);
            $("#txtInstitute").val(_event.Institute);
            $("#txtStartsOn").val(_event.StartsOn);
            $("#txtEndsOn").val(_event.EndsOn);
            $("#txtRanks").val(_event.Ranks);
            $("#txtVacancies").val(_event.Vacancies);
            displayResponsibilities();
            $("#txtAcceptanceOn").val(_event.AcceptanceOn);
            $("#txtNominationOn").val(_event.NominationOn);
            $("#txtDocForwardOn").val(_event.DocForwardOn);
            displayInitAlotment();
            displayReAlotment();
            displayRequiredDocs();
            bindNominees();
            bindReminders();
        }

        function displayResponsibilities() {
            $("#divHostResponsibilities :checkbox").each(function () {
                for (var i = 0; i < _event.Responsibilities.length; i++) {
                    var existing = _event.Responsibilities[i].Id;
                    if (existing == parseInt($(this).attr("dataId"), 10)) {
                        $(this).attr("checked", "checked");
                    }
                }
            });
        }

        function displayRequiredDocs() {
            $("#divRequiredDocs :checkbox").each(function () {
                for (var i = 0; i < _event.RequiredDocs.length; i++) {
                    var existing = _event.RequiredDocs[i].Id;
                    if (existing == parseInt($(this).attr("dataId"), 10)) {
                        $(this).attr("checked", "checked");
                    }
                }
            });
        }

        function displayInitAlotment() {
            $("#divInitAlotment > div > div").each(function () {
                for (var i = 0; i < _event.InitAlotment.length; i++) {
                    var alotment = _event.InitAlotment[i];
                    if (alotment.Type == $(this).find("input:checkbox").attr("typeVal")) {
                        $(this).find("input:checkbox").attr("checked", "checked");
                        $(this).find("input:text").val(alotment.Quota);
                    }
                }
            });
        }

        function displayReAlotment() {
            $("#divReAlotment > div > div").each(function () {
                for (var i = 0; i < _event.ReAlotment.length; i++) {
                    var alotment = _event.ReAlotment[i];
                    if (alotment.Type == $(this).find("input:checkbox").attr("typeVal")) {
                        $(this).find("input:checkbox").attr("checked", "checked");
                        $(this).find("input:text").val(alotment.Quota);
                    }
                }
            });
        }

        function getFormData() {
            _event.Name = $.trim($("#txtEventName").val());
            _event.Type = new Object();
            _event.Type.Id = parseInt($("#cmbEventTypes").val(), 10);
            _event.Genre = new Object();
            _event.Genre.Id = parseInt($("#cmbGenres").val(), 10);
            _event.Speciality = new Object();
            _event.Speciality.Id = parseInt($("#cmbSpecialities").val(), 10);
            _event.Country = new Object();
            _event.Country.Id = parseInt($("#cmbCountries").val(), 10);
            _event.City = $.trim($("#txtCity").val());
            _event.Institute = $.trim($("#txtInstitute").val());
            _event.StartsOn = $.trim($("#txtStartsOn").val());
            _event.EndsOn = $.trim($("#txtEndsOn").val());
            _event.Ranks = $.trim($("#txtRanks").val());
            _event.Vacancies = parseInt($.trim($("#txtVacancies").val()), 10);
            _event.Responsibilities = getResponsibilities();
            _event.RequiredDocs = getRequiredDocs();
            _event.AcceptanceOn = $.trim($("#txtAcceptanceOn").val());
            _event.NominationOn = $.trim($("#txtNominationOn").val());
            _event.DocForwardOn = $.trim($("#txtDocForwardOn").val());
            _event.InitAlotment = getInitAlotment();
            _event.ReAlotment = getReAlotment();
            _event.Nominees = _nominees;
            _event.Reminders = _reminders;
        }

        function getInitAlotment() {
            var alotments = new Array();
            $("#divInitAlotment > div > div").each(function () {
                if ($(this).find("input:checkbox").is(":checked")) {
                    var alotment = new Object();
                    alotment.Type = $.trim($(this).find("input:checkbox").val());
                    alotment.Quota = parseInt($.trim($(this).find("input:text").val()), 10);
                    alotments.push(alotment);
                }
            });
            return alotments;
        }

        function getReAlotment() {
            var alotments = new Array();
            $("#divReAlotment > div > div").each(function () {
                if ($(this).find("input:checkbox").is(":checked")) {
                    var alotment = new Object();
                    alotment.Type = $.trim($(this).find("input:checkbox").val());
                    alotment.Quota = $.trim($(this).find("input:text").val());
                    alotments.push(alotment);
                }
            });
            return alotments;
        }

        function isValidData(data) {
            var msg = "";
            if (data.Type.Id == 0) {
                msg += "Please select an event type\n";
            }
            if (data.Genre.Id == 0) {
                msg += "Please select a event genre\n";
            }
            if (data.Speciality.Id == 0) {
                msg += "Please select a speciality\n";
            }
            if (data.Name == "") {
                msg += "Please enter event name\n";
            }
            if (data.Country.Id == 0) {
                msg += "Please select a country\n";
            }
            if (data.StartsOn == "") {
                msg += "Please enter when starts\n";
            }
            if (data.EndsOn == "") {
                msg += "Please enter when ends\n";
            }
            if (data.AcceptanceOn == "") {
                msg += "Please enter acceptance by\n";
            }
            if (data.NominationOn == "") {
                msg += "Please enter nomination by\n";
            }
            if (data.DocForwardOn == "") {
                msg += "Please enter doc. forward by\n";
            }
            var alotMsg = validateAlotment(data.InitAlotment);
            if (alotMsg != "") {
                msg += "Consists following invalid alotment data:\n";
                msg += alotMsg;
            }
            alotMsg = validateAlotment(data.ReAlotment);
            if (validateAlotment(data.ReAlotment) != "") {
                msg += "Consists following invalid re-alotment data:\n";
                msg += alotMsg;
            }
            if (msg != "") {
                alert(msg);
                return false;
            }
            return true;
        }

        function validateAlotment(alotments) {
            var msg = "";
            for (var i = 0; i < alotments.length; i++) {
                if (isNaN(alotments[i].Quota) || alotments[i].Quota == 0) {
                    msg += "\t" + alotments[i].Type + ": has invalid persons\n";
                }
            }
            return msg;
        }

        function getResponsibilities() {
            var responsibilities = new Array();
            $("#divHostResponsibilities :checkbox:checked").each(function () {
                var responsibility = new Object();
                responsibility.Id = $(this).attr("dataId");
                responsibilities.push(responsibility);
            });
            return responsibilities;
        }

        function getRequiredDocs() {
            var requiredDocs = new Array();
            $("#divRequiredDocs :checkbox:checked").each(function () {
                var requiredDoc = new Object();
                requiredDoc.Id = $(this).attr("dataId");
                requiredDocs.push(requiredDoc);
            });
            return requiredDocs;
        }

        function saveData() {
            getFormData();
            if (isValidData(_event)) {
                $.ajax({
                    type: "POST",
                    url: "EventEdit.aspx/SaveEvent",
                    data: '{ "evnt":' + JSON.stringify(_event) + '}',
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    async: true,
                    success: function (data) {
                        if (data.d.IsSuccessful) {
                            window.parent.doSearch();
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

        function removeNominee(element) {
            var personalNo = $(element).attr("personalNo");
            for (var i = 0; i < _nominees.length; i++) {
                if (_nominees[i].PersonalNo == personalNo) {
                    _nominees.splice(i, 1);
                    break;
                }
            }
            bindNominees();
        }

        function removeReminder(element) {
            var remindFor = $(element).attr("remindFor");
            for (var i = 0; i < _reminders.length; i++) {
                if (_reminders[i].RemindFor == remindFor) {
                    _reminders.splice(i, 1);
                    break;
                }
            }
            bindReminders();
        }

        function addNominee() {
            if (isValidNominee()) {
                var nominee = new Object();
                nominee.PersonalNo = $.trim($("#txtPersonalNo").val());
                nominee.Rank = new Object();
                nominee.Rank.Id = $("#cmbRanks").val();
                nominee.Rank.Name = $("#cmbRanks option:selected").text();
                nominee.Name = $.trim($("#txtNomineeName").val());
                nominee.Unit = new Object();
                nominee.Unit.Id = $("#cmbUnits").val();
                nominee.Unit.Name = $("#cmbUnits option:selected").text();
                nominee.Branch = new Object();
                nominee.Branch.Id = $("#cmbBranches").val();
                nominee.Branch.Name = $("#cmbBranches option:selected").text();
                _nominees.push(nominee);
                bindNominees();
                hideNomineeDiv();
            }
        }

        function addReminder() {
            if (isValidReminder()) {
                var reminder = new Object();
                reminder.RemindFor = $.trim($("#txtRemindFor").val());
                reminder.RemindOn = $.trim($("#txtRemindOn").val());
                reminder.Dismissed = false;
                _reminders.push(reminder);
                bindReminders();
            }
        }

        function showNomineeDiv() {
            $("#divShowNominee").hide();
            $("#divNominee").show();
        }

        function hideNomineeDiv() {
            $("#divNominee").hide();
            $("#divShowNominee").show();
            $("#txtPersonalNo").val("");
            $("#cmbRanks").val("0");
            $("#txtNomineeName").val("");
        }

        function isValidNominee() {
            var msg = "";
            if ($("#txtPersonalNo").val() == "") {
                msg += "Please enter personal number\n";
            }
            else {
                if (isDuplicatePersonalNo($("#txtPersonalNo").val())) {
                    msg += "This trainee has already been added\n";
                }
            }
            if ($("#cmbRanks").val() == "0") {
                msg += "Please select a rank\n";
            }
            if ($("#txtNomineeName").val() == "") {
                msg += "Please enter nominee name\n";
            }
            if (msg != "") {
                alert(msg);
                return false;
            }
            return true;
        }

        function isValidReminder() {
            var msg = "";
            if ($.trim($("#txtRemindFor").val()) == "") {
                msg += "Please enter remind for\n";
            }
            else {
                if (isDuplicateReminder($.trim($("#txtRemindFor").val()))) {
                    msg += "This reminder already exist\n";
                }
            }
            if ($.trim($("#txtRemindOn").val()) == "") {
                msg += "Please select a date\n";
            }
            if (msg != "") {
                alert(msg);
                return false;
            }
            return true;
        }

        function isDuplicatePersonalNo(personalNo) {
            for (var i = 0; i < _nominees.length; i++) {
                if (_nominees[i].PersonalNo == personalNo) {
                    return true;
                }
            }
        }

        function isDuplicateReminder(remindFor) {
            for (var i = 0; i < _reminders.length; i++) {
                if (_reminders[i].RemindFor == remindFor) {
                    return true;
                }
            }
        }

        function openDialog(param, url, title) {
            $("#txtName").val("");
            _param = param;
            _url = url;
            $(".ui-dialog-title").text(title);
            $("#divDialog").dialog("open");
        }

        function saveMetadata() {
            var metadata = new Object();
            metadata.Name = $.trim($("#txtName").val());
            if (metadata.Name == "") {
                alert("Please enter name\n");
                $("#txtName").focus();
                return;
            }
            $.ajax({
                type: "POST",
                url: _url,
                data: '{' + _param + ':' + JSON.stringify(metadata) + '}',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (data) {
                    if (data.d.IsSuccessful) {
                        populateCombo(data.d.Data);
                        $("#divDialog").dialog("close");
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

        function populateCombo(metadata) {
            if (_param == "evtType") {
                loadEventTypes();
                $("#cmbEventTypes").val(metadata.Id);
                $("#divEventTypes > span > input").val(metadata.Name);
            }
            if (_param == "country") {
                loadCountries();
                $("#cmbCountries").val(metadata.Id);
                $("#divCountries > span > input").val(metadata.Name);
            }
            if (_param == "genre") {
                loadGenres();
                $("#cmbGenres").val(metadata.Id);
                $("#divGenres > span > input").val(metadata.Name);
            }
            if (_param == "speciality") {
                loadSpecialities();
                $("#cmbSpecialities").val(metadata.Id);
                $("#divSpecialities > span > input").val(metadata.Name);
            }
            if (_param == "rank") {
                loadRanks();
                $("#cmbRanks").val(metadata.Id);
                $("#tdRanks > span > input").val(metadata.Name);
            }
            if (_param == "unit") {
                loadUnits();
                $("#cmbUnits").val(metadata.Id);
                $("#tdUnits > span > input").val(metadata.Name);
            }
            if (_param == "branch") {
                loadBranches();
                $("#cmbBranches").val(metadata.Id);
                $("#tdBranches > span > input").val(metadata.Name);
            }
            if (_param == "responsibility") {
                loadResponsibilities();
                if (_event.Responsibilities == null) {
                    _event.Responsibilities = new Array();
                }
                _event.Responsibilities.push(metadata);
                displayResponsibilities();
            }
            if (_param == "requiredDoc") {
                loadRequiredDocs();
                if (_event.RequiredDocs == null) {
                    _event.RequiredDocs = new Array();
                }
                _event.RequiredDocs.push(metadata);
                displayRequiredDocs();
            }
        }
    </script>
    <script id="tmplData" type="text/html">
        <div>
            <input type="checkbox" dataId="${Id}" value="${Name}">${Name}</input>
        </div>
    </script>
    <script id="Script1" type="text/html">
        <div>
            <input type="checkbox" dataId="${Id}" value="${Name}">${Name}</input>
        </div>
    </script>
    <script id="tmplAlotment" type="text/html">
        <div>
            <input type="checkbox" value="${Type}" typeVal="${TypeVal}">${Type}</input>
            <input type="text" class="numeric-only" style="width: 50px; text-align: right;"/>
        </div>
    </script>
    <script id="tmplNominee" type="text/html">
        <tr>
            <td>
                ${PersonalNo}
            </td>
            <td rankId="${Rank.Id}">
                ${Rank.Name}
            </td>
            <td unitId="${Unit.Id}">
                ${Unit.Name}
            </td>
            <td >
                ${Name}
            </td>
            <td>
                ${Branch.Name}
            </td>
            <td style="text-align: center;">
                <img src="Styles/Images/DeleteIcon.jpg" alt="Remove" style="cursor: pointer" personalNo="${PersonalNo}" title="Remove" onclick="removeNominee(this);"/>
            </td>
        </tr>
    </script>
    <script id="tmplReminder" type="text/html">
        <tr>
            <td>
                ${RemindFor}
            </td>
            <td >
                ${RemindOn}
            </td>
            <td style="text-align: center;">
                <img src="Styles/Images/DeleteIcon.jpg" alt="Remove" style="cursor: pointer" remindFor="${RemindFor}" title="Remove" onclick="removeReminder(this);"/>
            </td>
        </tr>
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <input type="button" value="Save" onclick="saveData();" style="margin-left: 2px;
        margin-top: 2px;" />
    <div id="tabs" style="border-width: 0;">
        <ul>
            <li><a href="#tabs-1">General</a></li>
            <li><a href="#tabs-2">Allotment</a></li>
            <li><a href="#tabs-3">Nominees</a></li>
            <li><a href="#tabs-4">Setup Reminder</a></li>
        </ul>
        <div id="tabs-1">
            <div style="float: left; width: 48%;">
                <div>
                    Event Type <span style="color: Red;">*</span></div>
                <div id="divEventTypes">
                    <select id="cmbEventTypes" class="combobox">
                    </select>
                    <img alt="Add new" title="Add new event type" src="Styles/images/add-new.png" style="width: 18px;
                        height: 23px; float: right; margin-right: 35px; cursor: pointer;" onclick="openDialog('evtType', 'EventTypeEdit.aspx/SaveEventType', 'New Event Type');" />
                </div>
                <div>
                    Event Genre <span style="color: Red;">*</span></div>
                <div id="divGenres">
                    <select id="cmbGenres" class="combobox">
                    </select>
                    <img alt="Add new" title="Add new genge" src="Styles/images/add-new.png" style="width: 18px;
                        height: 23px; float: right; margin-right: 35px; cursor: pointer;" onclick="openDialog('genre', 'GenreEdit.aspx/SaveGenre', 'New Genre');" />
                </div>
                <div>
                    Speciality <span style="color: Red;">*</span></div>
                <div id="divSpecialities">
                    <select id="cmbSpecialities" class="combobox">
                    </select>
                    <img alt="Add new" title="Add new speciality" src="Styles/images/add-new.png" style="width: 18px;
                        height: 23px; float: right; margin-right: 35px; cursor: pointer;" onclick="openDialog('speciality', 'SpecialityEdit.aspx/SaveSpeciality', 'New Speciality');" />
                </div>
                <div>
                    What <span style="color: Red;">*</span></div>
                <div>
                    <input type="text" id="txtEventName" class="text-box" />
                </div>
                <div style="border-bottom: 1px solid silver">
                    Where</div>
                <div>
                    Country <span style="color: Red;">*</span>
                </div>
                <div id="divCountries">
                    <select id="cmbCountries" class="combobox">
                    </select>
                    <img alt="Add new" title="Add new country" src="Styles/images/add-new.png" style="width: 18px;
                        height: 23px; float: right; margin-right: 35px; cursor: pointer;" onclick="openDialog('country', 'CountryEdit.aspx/SaveCountry', 'New Country');" />
                </div>
                <div>
                    City</div>
                <div>
                    <input type="text" id="txtCity" class="text-box" />
                </div>
                <div>
                    Institute</div>
                <div>
                    <input type="text" id="txtInstitute" class="text-box" />
                </div>
            </div>
            <div style="float: right; width: 48%;">
                <div style="border-bottom: 1px solid silver">
                    When</div>
                <div>
                    From <span style="color: Red;">*</span>
                </div>
                <div>
                    <input type="text" id="txtStartsOn" class="date-picker" />
                </div>
                <div>
                    To <span style="color: Red;">*</span>
                </div>
                <div>
                    <input type="text" id="txtEndsOn" class="date-picker" />
                </div>
                <div>
                    Who
                </div>
                <div>
                    <input type="text" id="txtRanks" class="text-box" />
                </div>
                <div>
                    No of Vacancies
                </div>
                <div>
                    <input type="text" class="numeric-only" id="txtVacancies" />
                </div>
                <div>
                    Host Country Responsibilities
                    <img alt="Add new" title="Add new required doc" src="Styles/images/add-new.png" style="width: 18px;
                        height: 18px; margin-left: 5px; margin-top: 2px; cursor: pointer;" onclick="openDialog('responsibility', 'ResponsibilityEdit.aspx/SaveResponsibility', 'New Responsibility');" />
                </div>
                <div id="divHostResponsibilities" style="height: 90px; overflow: auto; border: 1px solid silver;">
                    <div>
                    </div>
                </div>
            </div>
        </div>
        <div id="tabs-2">
            <div style="float: left; width: 48%;">
                <div>
                    Initial Alotment
                </div>
                <div id="divInitAlotment" style="height: 150px; border: 1px solid silver; overflow: auto;">
                    <div>
                    </div>
                </div>
                <div>
                    Acceptance By <span style="color: Red">*</span>
                </div>
                <div>
                    <input type="text" id="txtAcceptanceOn" class="date-picker" />
                </div>
                <div>
                    Nomination By <span style="color: Red">*</span>
                </div>
                <div>
                    <input type="text" id="txtNominationOn" class="date-picker" />
                </div>
                <div>
                    Documents Forward By <span style="color: Red">*</span>
                </div>
                <div>
                    <input type="text" id="txtDocForwardOn" class="date-picker" />
                </div>
            </div>
            <div style="float: right; width: 48%;">
                <div>
                    Re Alotment
                </div>
                <div id="divReAlotment" style="height: 150px; border: 1px solid silver; overflow: auto;">
                    <div>
                    </div>
                </div>
                <div>
                    Required Documents
                    <img alt="Add new" title="Add new required doc" src="Styles/images/add-new.png" style="width: 18px;
                        height: 18px; margin-left: 5px; margin-top: 2px; cursor: pointer;" onclick="openDialog('requiredDoc', 'RequiredDocEdit.aspx/SaveRequiredDoc', 'New Required Document');" />
                </div>
                <div id="divRequiredDocs" style="height: 120px; border: 1px solid silver; overflow: auto;">
                    <div>
                    </div>
                </div>
            </div>
        </div>
        <div id="tabs-3">
            <div id="divNominee" style="width: 100%;">
                <fieldset>
                    <table style="margin-bottom: 7px;" class="table">
                        <thead>
                            <tr>
                                <th style="width: 13%;">
                                    Personal No
                                </th>
                                <th style="width: 19%;">
                                    Rank
                                </th>
                                <th style="width: 19%;">
                                    Unit
                                </th>
                                <th style="width: 30%;">
                                    Name
                                </th>
                                <th style="width: 19%;">
                                    Branch
                                </th>
                            </tr>
                        </thead>
                        <tbody>
                            <tr>
                                <td>
                                    <input type="text" id="txtPersonalNo" style="width: 100%; margin: 0 0 0 0;" />
                                </td>
                                <td id="tdRanks">
                                    <select id="cmbRanks" style="width: 100%; margin: 0 0 0 0;" class="combobox">
                                    </select>
                                    <img alt="Add new" title="Add new rank" src="Styles/images/add-new.png" style="width: 18px;
                                        height: 23px; float: right; cursor: pointer;" onclick="openDialog('rank', 'RankEdit.aspx/SaveRank', 'New Rank');" />
                                </td>
                                <td id="tdUnits">
                                    <select id="cmbUnits" style="width: 100%; margin: 0 0 0 0;" class="combobox">
                                    </select>
                                    <img alt="Add new" title="Add new unit" src="Styles/images/add-new.png" style="width: 18px;
                                        height: 23px; float: right; cursor: pointer;" onclick="openDialog('unit', 'UnitEdit.aspx/SaveUnit', 'New Unit');" />
                                </td>
                                <td>
                                    <input type="text" id="txtNomineeName" style="width: 100%; margin: 0 0 0 0;" />
                                </td>
                                <td id="tdBranches">
                                    <select id="cmbBranches" style="width: 100%; margin: 0 0 0 0;" class="combobox">
                                    </select>
                                    <img alt="Add new" title="Add new branch" src="Styles/images/add-new.png" style="width: 18px;
                                        height: 23px; float: right; cursor: pointer;" onclick="openDialog('branch', 'BranchEdit.aspx/SaveBranch', 'New Branch');" />
                                </td>
                            </tr>
                        </tbody>
                    </table>
                    <div style="float: right; margin-left: 10px;">
                        <input type="button" value="Cancel" onclick="hideNomineeDiv();" />
                    </div>
                    <div style="float: right;">
                        <input type="button" value="OK" onclick="addNominee();" />
                    </div>
                </fieldset>
            </div>
            <div id="divShowNominee" style="width: 100%;">
                <fieldset>
                    <div style="float: right;">
                        <input type="button" value="Add Nominee" onclick="showNomineeDiv();" />
                    </div>
                </fieldset>
            </div>
            <fieldset>
                <table id="tblNominees" class="table" cellpadding="0" cellspacing="0">
                    <thead>
                        <tr>
                            <th width="10%">
                                Personal No
                            </th>
                            <th width="18%">
                                Rank
                            </th>
                            <th width="18%">
                                Unit
                            </th>
                            <th width="31%">
                                Name
                            </th>
                            <th width="18%">
                                Branch
                            </th>
                            <th width="5%">
                                Remove
                            </th>
                        </tr>
                    </thead>
                    <tbody>
                    </tbody>
                </table>
            </fieldset>
        </div>
        <div id="tabs-4">
            <fieldset>
                <legend>Add Reminder</legend>Remind For
                <input type="text" id="txtRemindFor" style="width: 400px;" />
                Remind On
                <input type="text" id="txtRemindOn" class="date-picker" />
                <input type="button" value="Add" onclick="addReminder();" />
            </fieldset>
            <fieldset>
                <table id="tblReminders" class="table">
                    <thead>
                        <tr>
                            <th width="70%">
                                Remind For
                            </th>
                            <th width="20%">
                                Remind On
                            </th>
                            <th width="10%">
                                Remove
                            </th>
                        </tr>
                    </thead>
                    <tbody>
                    </tbody>
                </table>
            </fieldset>
        </div>
        <div id="divDialog">
            <fieldset>
                <label for="txtName">
                    Name
                </label>
                <input type="text" id="txtName" style="width: 250px;" class="text ui-widget-content ui-corner-all" />
                <div style="margin-top: 15px; text-align: center;">
                    <input type="button" value="Save" onclick="saveMetadata();" />
                </div>
            </fieldset>
        </div>
    </div>
    </form>
</body>
</html>
