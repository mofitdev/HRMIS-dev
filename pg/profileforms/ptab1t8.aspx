﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ptab1t8.aspx.cs" Inherits="HRWebApp.pg.profileforms.ptab1t8" %>
<section id="widget-grid">
    <div class="row" style="padding-top:10px;">
        <article class="col-xs-12 col-sm-12 col-md-12 col-lg-12">
            <div class="jarviswidget" data-widget-sortable="false" data-widget-colorbutton="false" data-widget-togglebutton="false" data-widget-editbutton="false" data-widget-fullscreenbutton="false" data-widget-deletebutton="false">
                <header>
                    <span class="widget-icon"> 
                        <i class="fa fa-table"></i> 
                    </span>
                    <h2> Туршлагын талаарх мэдээлэл  <span class="label bg-color-orangeDark font-sm" style="margin: 6px 0 0 10px;">САНГИЙН ЯАМАНД ОРХООС ӨМНӨХ ТУРШЛАГЫН ТАЛААРХ МЭДЭЭЛЭЛ ОРУУЛНА!</span></h2>
                </header>
                <div>
                    <div class="Colvis TableTools" style="right:75px; top:4px; z-index:5; margin-top:7px;"><label>Илэрц: </label></div>
                    <div class="Colvis TableTools" style="width:62px; right:120px; top:8px; z-index:5; margin-top:1px;">
                        <button id="pTab1T8Section1AddBtn" class="btn btn-primary btn-xs" type="button" data-target="#pTab1T8Section1Modal" data-toggle="modal" onclick="showAddEditTab1T8Section1(this,'нэмэх')"><i class="fa fa-plus"></i> Нэмэх</button>
                    </div>
                    <div id="loaderTab1T8Section1" class="search-background">
                        <img width="64" height="" src="img/loading.gif"/>
                        <h2 style="width:100%; display:block; overflow:hidden; padding:20px 0 0 0; color: #fff; padding-top:0px; margin-top:0px;">Уншиж байна !</h2>
                    </div>
                    <div id="divBindTab1T8Section1Table" class="widget-body no-padding">
                    </div>
                </div>
            </div>
        </article>
    </div>
    <div class="row" style="padding-top:10px;">
        <article class="col-xs-12 col-sm-12 col-md-12 col-lg-6">
            <div class="well well-sm">
                <h4 class="text-primary" style="margin-bottom:5px;">Төрийн албан хаагч</h4>
                <table class="table">
                    <tbody>
                        <tr>
                            <td style="width:500px;">Төрийн албан хаагчийн "Төрийн албаны мэргэшлийн шалгалт" өгсөн эсэх:</td>
                            <td>
                                <label class="radio radio-inline no-margin" style="width:90px;">
							        <input id="ptab1t8IsgaveT1" name="ptab1t8Isgave" runat="server" type="radio" class="radiobox" value="1">
								    <span>Өгсөн</span> 
							    </label>
                                <label class="radio radio-inline" style="width:90px;">
							        <input id="ptab1t8IsgaveT0" name="ptab1t8Isgave" runat="server" type="radio" class="radiobox" value="0">
								    <span>Өгөөгүй</span> 
							    </label>
                            </td>
                        </tr>
                        <tr>
                            <td>Төрийн албан хаагчийн "Төрийн жинхэнэ тангараг" өгсөн эсэх:</td>
                            <td>
                                <label class="radio radio-inline no-margin" style="width:90px;">
							        <input id="ptab1t8IsswearT1" name="ptab1t8Isswear" runat="server" type="radio" class="radiobox" value="1">
								    <span>Өргөсөн</span> 
							    </label>
                                <label class="radio radio-inline" style="width:90px;">
							        <input id="ptab1t8IsswearT0" name="ptab1t8Isswear" runat="server" type="radio" class="radiobox" value="0">
								    <span>Өргөөгүй</span> 
							    </label>
                            </td>
                        </tr>
                        <tr>
                            <td>Төрийн албан хаагчийн "Төрийн жинхэнэ тангараг" өгсөн огноо:</td>
                            <td class="smart-form">
                                <label class="input" style="width:120px;"> 
                                    <i class="icon-append glyphicon glyphicon-floppy-disk savebtn"></i>
									<input id="ptab1t8Testdate" name="ptab1t8Testdate" type="text" runat="server" placeholder="Огноо" maxlength="10">
								</label>
                            </td>
                        </tr>
                    </tbody>
                </table>
            </div>
        </article>
    </div>
</section>
<div id="pTab1T8Section1Modal" class="modal fade" tabindex="-1" role="dialog" aria-labelledby="remoteModalLabel" aria-hidden="true">
	<div class="modal-dialog modal-md">
        <div class="modal-content">
            <form id="pTab1T8Section1ModalForm">
                <div class="modal-header">
			        <button type="button" class="close" data-dismiss="modal" aria-hidden="true">×</button>
			        <h4 class="modal-title">Туршлага&nbsp;<span id="pTab1T8Section1ModalHeaderLabel"></span></h4>
		        </div>
		        <div class="modal-body">
                    <div id="pTab1T8Section1ID" class="hide"></div>
                    <fieldset>
						<div class="form-group">
                            <div class="row">
                                <div class="col-md-12">
                                    <label class="control-label">*Ажилласан байгууллага</label>
                                    <input id="pTab1T8Section1ModalInputOrgname" name="pTab1T8Section1ModalInputOrgname" type="text" class="form-control" placeholder="Ажилласан байгууллага" />
                                </div>
                            </div>
                        </div>
                    </fieldset>
                    <fieldset>
						<div class="form-group">
                            <div class="row">
                                <div class="col-md-12">
                                    <label class="control-label">*Албан тушаал</label>
                                    <input id="pTab1T8Section1ModalInputTitle" name="pTab1T8Section1ModalInputTitle" type="text" class="form-control" placeholder="Албан тушаал" />
                                </div>
                            </div>
                        </div>
                    </fieldset>
                    <fieldset>
						<div class="form-group">
                            <div class="row">
                                <div class="col-md-6">
                                    <label class="control-label">*Ямар байгууллагад</label>
                                    <select id="pTab1T8Section1ModalSelectOrgtype" name="pTab1T8Section1ModalSelectOrgtype" runat="server" class="form-control" style="padding: 5px;">
							            <option value="">- Сонго -</option>
						            </select>
                                </div>
                                <div class="col-md-6">
                                    <label class="control-label">*Ажиллаж байгаа эсэх</label>
                                    <select id="pTab1T8Section1ModalSelectIsworking" name="pTab1T8Section1ModalSelectIsworking" runat="server" class="form-control" style="padding: 5px;" disabled="disabled">
							            <option value="">- Сонго -</option>
						            </select>
                                </div>
                            </div>
                        </div>
                    </fieldset>
                    <fieldset>
						<div class="form-group">
                            <div class="row">
                                <div class="col-md-4">
                                    <label class="control-label">*Орсон огноо</label>
                                    <input id="pTab1T8Section1ModalInputFromdate" name="pTab1T8Section1ModalInputFromdate" type="text" class="form-control" placeholder="Орсон огноо" />
                                </div>
                                <div class="col-md-4">
                                    <label class="control-label">*Гарсан огноо</label>
                                    <input id="pTab1T8Section1ModalInputTodate" name="pTab1T8Section1ModalInputTodate" type="text" class="form-control" placeholder="Гарсан огноо"/>
                                </div>
                                <div class="col-md-4">
                                    <label class="control-label">*НД төлж байсан эсэх</label>
                                    <div class="checkbox no-margin">
                                        <label class="checkbox-inline no-margin">
							                <input id="pTab1T8Section1ModalInputIsSocins" name="pTab1T8Section1ModalInputIsSocins" class="checkbox style-0" type="checkbox" value="1">
							                <span>Төлж байсан</span>
						                </label>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </fieldset>
                </div>
                <div class="modal-footer">
			        <button type="button" class="btn btn-default" data-dismiss="modal">Болих</button>
			        <button type="submit" class="btn btn-primary"><span class="glyphicon glyphicon-floppy-disk"></span> Хадгалах</button>
		        </div>
            </form>
        </div>
    </div>
</div>
<script type="text/javascript">
    dataBindTab1T8Section1Datatable();
    function dataBindTab1T8Section1Datatable() {
        showLoader('loaderTab1T8Section1');
        globalAjaxVar = $.ajax({
            type: "POST",
            url: "ws.aspx/profile_profileTab1T8Datatable1",
            data: '{staffid:"' +<%=Request.QueryString["id"]%> +'"}',
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (msg) {
                $("#divBindTab1T8Section1Table").html(msg.d);
                formTab1T8Percent();
                profileAjillasanJil();
                hideLoader('loaderTab1T8Section1');
            },
            failure: function (response) {
                alert('FAILURE: ' + response.d);
            },
            error: function (xhr, status, error) {
                var err = eval("(" + xhr.responseText + ")");
                if (err.Message == 'SessionDied') window.location = '../login.html';
                else window.location = '../#pg/error500.aspx';
            }
        });
    }
    function showAddEditTab1T8Section1(el, isinsupt) {
        $('#pTab1T8Section1ModalHeaderLabel').text(isinsupt);
        $('#pTab1T8Section1ID').html('');
        $('#pTab1T8Section1ModalInputOrgname').val('');
        $('#pTab1T8Section1ModalInputTitle').val('');
        $('#pTab1T8Section1ModalSelectOrgtype').val('');
        //$('#pTab1T8Section1ModalSelectIsworking').val('');
        $('#pTab1T8Section1ModalSelectIsworking').val('1');
        $('#pTab1T8Section1ModalInputFromdate').val('');
        $('#pTab1T8Section1ModalInputTodate').val('');
        if (isinsupt == 'засах') {
            $('#pTab1T8Section1ID').html($(el).closest('tr').find('td:eq(0)').text());
            $('#pTab1T8Section1ModalInputOrgname').val($(el).closest('tr').find('td:eq(1)').text());
            $('#pTab1T8Section1ModalInputTitle').val($(el).closest('tr').find('td:eq(2)').text());
            $('#pTab1T8Section1ModalSelectOrgtype').val($(el).closest('tr').find('td:eq(9)').text());
            //$('#pTab1T8Section1ModalSelectIsworking').val($(el).closest('tr').find('td:eq(10)').text());
            $('#pTab1T8Section1ModalInputFromdate').val($(el).closest('tr').find('td:eq(5)').text());
            $('#pTab1T8Section1ModalInputTodate').val($(el).closest('tr').find('td:eq(6)').text());
        }
    }
    function showDeleteTab1T8Section1(el) {
        $.SmartMessageBox({
            title: "Анхааруулга!",
            content: "Сонгосон мөр бичиглэлийг устгахдаа итгэлтэй байна уу?",
            buttons: '[Үгүй][Тийм]'
        }, function (ButtonPressed) {
            if (ButtonPressed === "Тийм") {
                globalAjaxVar = $.ajax({
                    type: "POST",
                    url: "ws.aspx/WSOracleExecuteNonQuery",
                    data: '{qry:"DELETE FROM ST_EXPHISTORY WHERE ID=' + $(el).closest('tr').find('td:eq(0)').text() + '"}',
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function () {
                        dataBindTab1T8Section1Datatable();
                        smallBox('Сонгосон мөр бичиглэл', 'Амжилттай устгагдлаа', '#659265', 4000);
                    },
                    failure: function (response) {
                        alert('FAILURE: ' + response.d);
                    },
                    error: function (xhr, status, error) {
                        var err = eval("(" + xhr.responseText + ")");
                        if (err.Message == 'SessionDied') window.location = '../login.html';
                        else if (err.Message == 'NullReferenceException') {
                            //alert('NullReferenceException');
                        }
                        else window.location = '../#pg/error500.aspx';
                    }
                });
            }
        });
    }
    $('#pTab1T8Section1ModalForm').bootstrapValidator({
        fields: {
            pTab1T8Section1ModalInputOrgname: {
                validators: {
                    notEmpty: {
                        message: 'Оруулна уу'
                    },
                    stringLength: {
                        max: 100,
                        message: 'Уртдаа 100 тэмдэгт авна'
                    }
                }
            },
            pTab1T8Section1ModalInputTitle: {
                validators: {
                    notEmpty: {
                        message: 'Оруулна уу'
                    },
                    stringLength: {
                        max: 100,
                        message: 'Уртдаа 100 тэмдэгт авна'
                    }
                }
            },
            pTab1T8Section1ModalSelectOrgtype: {
                validators: {
                    notEmpty: {
                        message: 'Сонгоно уу'
                    }
                }
            },
            pTab1T8Section1ModalSelectIsworking: {
                validators: {
                    notEmpty: {
                        message: 'Сонгоно уу'
                    }
                }
            },
            pTab1T8Section1ModalInputFromdate: {
                validators: {
                    notEmpty: {
                        message: 'Оруулна уу'
                    },
                    date: {
                        format: 'YYYY-MM-DD',
                        message: 'Огноо буруу орсон байна. /Жил-Сар-Өдөр/'
                    }
                }
            },
            pTab1T8Section1ModalInputTodate: {
                validators: {
                    notEmpty: {
                        message: 'Оруулна уу'
                    },
                    date: {
                        format: 'YYYY-MM-DD',
                        message: 'Огноо буруу орсон байна. /Жил-Сар-Өдөр/'
                    }
                }
            }
        },
        onSuccess: function (e, data) {
            e.preventDefault();
            globalAjaxVar = $.ajax({
                type: "POST",
                url: "ws.aspx/WSOracleExecuteScalar",
                data: '{qry:"SELECT ISOUT FROM STN_ISWORKING WHERE ID=' + $('#pTab1T8Section1ModalSelectIsworking option:selected').val() + '"}',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (msg) {
                    var errMsg = '';
                    if (msg.d == '1') {
                        if ($.trim($('#pTab1T8Section1ModalInputTodate').val()) == '') errMsg = 'Гарсан огноо заавал оруулна уу!';
                        else {
                            if (parseDate($.trim($('#pTab1T8Section1ModalInputTodate').val())) <= parseDate($.trim($('#pTab1T8Section1ModalInputFromdate').val()))) errMsg = 'Гарсан огноо орсон огнооноос их байна!';
                        }
                    }
                    if (errMsg != '') alert(errMsg);
                    else {
                        var valIsSocins = '0';
                        if ($('#pTab1T8Section1ModalInputIsSocins').is(':checked')) valIsSocins = '1';
                        if ($('#pTab1T8Section1ModalHeaderLabel').text() == 'нэмэх') {
                            globalAjaxVar = $.ajax({
                                type: "POST",
                                url: "ws.aspx/WSOracleExecuteNonQuery",
                                data: '{qry:"INSERT INTO ST_EXPHISTORY (ID, STAFFS_ID, ORGNAME, TITLE, ORGTYPE_ID, ISWORKING_ID, FROMDATE, TODATE, ISSOCINS, CREATED_STAFFID, CREATED_DATE) VALUES (TBLLASTID(\'ST_EXPHISTORY\'), ' +<%=Request.QueryString["id"]%> +', \'' + replaceDisplayToDatabase($.trim($('#pTab1T8Section1ModalInputOrgname').val())) + '\', \'' + replaceDisplayToDatabase($.trim($('#pTab1T8Section1ModalInputTitle').val())) + '\', ' + $('#pTab1T8Section1ModalSelectOrgtype option:selected').val() + ', ' + $('#pTab1T8Section1ModalSelectIsworking option:selected').val() + ', \'' + $.trim($('#pTab1T8Section1ModalInputFromdate').val()) + '\', ' + strQryIsNull('varchar', $.trim($('#pTab1T8Section1ModalInputTodate').val())) + ', ' + valIsSocins + ', ' + $('#indexUserId').text() + ', sysdate)"}',
                                contentType: "application/json; charset=utf-8",
                                dataType: "json",
                                success: function () {
                                    dataBindTab1T8Section1Datatable();
                                    $('#pTab1T8Section1Modal').modal('hide');
                                    smallBox('Туршлага', 'Амжилттай хадгалагдлаа', '#659265', 4000);
                                },
                                failure: function (response) {
                                    alert('FAILURE: ' + response.d);
                                },
                                error: function (xhr, status, error) {
                                    var err = eval("(" + xhr.responseText + ")");
                                    if (err.Message == 'SessionDied') window.location = '../login.html';
                                    else if (err.Message == 'NullReferenceException') {
                                        //alert('NullReferenceException');
                                    }
                                    else window.location = '../#pg/error500.aspx';
                                }
                            });
                        }
                        else {
                            globalAjaxVar = $.ajax({
                                type: "POST",
                                url: "ws.aspx/WSOracleExecuteNonQuery",
                                data: '{qry:"UPDATE ST_EXPHISTORY SET ORGNAME=\'' + replaceDisplayToDatabase($.trim($('#pTab1T8Section1ModalInputOrgname').val())) + '\', TITLE=\'' + replaceDisplayToDatabase($.trim($('#pTab1T8Section1ModalInputTitle').val())) + '\', ORGTYPE_ID=' + $('#pTab1T8Section1ModalSelectOrgtype option:selected').val() + ', ISWORKING_ID=' + $('#pTab1T8Section1ModalSelectIsworking option:selected').val() + ', FROMDATE=\'' + $.trim($('#pTab1T8Section1ModalInputFromdate').val()) + '\', TODATE=' + strQryIsNull('varchar', $.trim($('#pTab1T8Section1ModalInputTodate').val())) + ', ISSOCINS=' + valIsSocins + ', UPDATED_STAFFID=' + $('#indexUserId').text() + ', UPDATED_DATE=SYSDATE WHERE ID=' + $('#pTab1T8Section1ID').html() + '"}',
                                contentType: "application/json; charset=utf-8",
                                dataType: "json",
                                success: function () {
                                    dataBindTab1T8Section1Datatable();
                                    $('#pTab1T8Section1Modal').modal('hide');
                                    smallBox('Туршлага', 'Амжилттай засварлагдлаа', '#659265', 4000);
                                },
                                failure: function (response) {
                                    alert('FAILURE: ' + response.d);
                                },
                                error: function (xhr, status, error) {
                                    var err = eval("(" + xhr.responseText + ")");
                                    if (err.Message == 'SessionDied') window.location = '../login.html';
                                    else if (err.Message == 'NullReferenceException') {
                                        //alert('NullReferenceException');
                                    }
                                    else window.location = '../#pg/error500.aspx';
                                }
                            });
                        }
                    }
                },
                failure: function (response) {
                    alert('FAILURE: ' + response.d);
                },
                error: function (xhr, status, error) {
                    var err = eval("(" + xhr.responseText + ")");
                    if (err.Message == 'SessionDied') window.location = '../login.html';
                    else if (err.Message == 'NullReferenceException') {
                        //alert('NullReferenceException');
                    }
                    else window.location = '../#pg/error500.aspx';
                }
            });
        }
    });
    $("#pTab1T8Section1ModalSelectIsworking").change(function () {
        if ($("#pTab1T8Section1ModalSelectIsworking option:selected").val() == '') {
            $('#pTab1T8Section1ModalInputTodate').prop('disabled', false);
        }
        else {
            globalAjaxVar = $.ajax({
                type: "POST",
                url: "ws.aspx/WSOracleExecuteScalar",
                data: '{qry:"SELECT ISOUT FROM STN_ISWORKING WHERE ID=' + $('#pTab1T8Section1ModalSelectIsworking option:selected').val() + '"}',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (msg) {
                    if (msg.d == '0') {
                        $('#pTab1T8Section1ModalInputTodate').val('');
                        $('#pTab1T8Section1ModalInputTodate').prop('disabled', true);
                    }
                    else $('#pTab1T8Section1ModalInputTodate').prop('disabled', false);
                },
                failure: function (response) {
                    alert('FAILURE: ' + response.d);
                },
                error: function (xhr, status, error) {
                    var err = eval("(" + xhr.responseText + ")");
                    if (err.Message == 'SessionDied') window.location = '../login.html';
                    else if (err.Message == 'NullReferenceException') {
                        //alert('NullReferenceException');
                    }
                    else window.location = '../#pg/error500.aspx';
                }
            });
        }
    });
    $('.radiobox').change(function () {
        var myEl = $(this);
        globalAjaxVar = $.ajax({
            type: "POST",
            url: "ws.aspx/WSOracleExecuteScalar",
            data: '{qry:"SELECT COUNT(1) FROM ST_STATES WHERE STAFFS_ID=' +<%=Request.QueryString["id"]%> +'"}',
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (msg) {
                if (msg.d == '0') {
                    globalAjaxVar = $.ajax({
                        type: "POST",
                        url: "ws.aspx/WSOracleExecuteNonQuery",
                        data: '{qry:"INSERT INTO ST_STATES (STAFFS_ID, ' + myEl.attr('name').split('ptab1t8')[1].toUpperCase() + ') VALUES (' +<%=Request.QueryString["id"]%> +', ' + myEl.attr('id').split('ptab1t8')[1].split('T')[1] + ')"}',
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        success: function () {
                            formTab1T8Percent();
                            smallBox('Мэдээлэл', 'Амжилттай хадгалагдлаа', '#659265', 4000);
                        },
                        failure: function (response) {
                            alert('FAILURE: ' + response.d);
                        },
                        error: function (xhr, status, error) {
                            var err = eval("(" + xhr.responseText + ")");
                            if (err.Message == 'SessionDied') window.location = '../login.html';
                            else if (err.Message == 'NullReferenceException') {
                                //alert('NullReferenceException');
                            }
                            else window.location = '../#pg/error500.aspx';
                        }
                    });
                }
                else {
                    globalAjaxVar = $.ajax({
                        type: "POST",
                        url: "ws.aspx/WSOracleExecuteNonQuery",
                        data: '{qry:"UPDATE ST_STATES SET ' + myEl.attr('name').split('ptab1t8')[1].toUpperCase() + '=' + myEl.attr('id').split('ptab1t8')[1].split('T')[1] + '  WHERE STAFFS_ID=' +<%=Request.QueryString["id"]%> +'"}',
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        success: function () {
                            formTab1T8Percent();
                            smallBox('Мэдээлэл', 'Амжилттай хадгалагдлаа', '#659265', 4000);
                        },
                        failure: function (response) {
                            alert('FAILURE: ' + response.d);
                        },
                        error: function (xhr, status, error) {
                            var err = eval("(" + xhr.responseText + ")");
                            if (err.Message == 'SessionDied') window.location = '../login.html';
                            else if (err.Message == 'NullReferenceException') {
                                //alert('NullReferenceException');
                            }
                            else window.location = '../#pg/error500.aspx';
                        }
                    });
                }
            },
            failure: function (response) {
                alert('FAILURE: ' + response.d);
            },
            error: function (xhr, status, error) {
                var err = eval("(" + xhr.responseText + ")");
                if (err.Message == 'SessionDied') window.location = '../login.html';
                else if (err.Message == 'NullReferenceException') {
                    //alert('NullReferenceException');
                }
                else window.location = '../#pg/error500.aspx';
            }
        });
    });
    $('.savebtn').click(function (e) {
        globalAjaxVar = $.ajax({
            type: "POST",
            url: "ws.aspx/WSOracleExecuteScalar",
            data: '{qry:"SELECT COUNT(1) FROM ST_STATES WHERE STAFFS_ID=' +<%=Request.QueryString["id"]%> +'"}',
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (msg) {
                if (msg.d == '0') {
                    globalAjaxVar = $.ajax({
                        type: "POST",
                        url: "ws.aspx/WSOracleExecuteNonQuery",
                        data: '{qry:"INSERT INTO ST_STATES (STAFFS_ID, TESTDATE) VALUES (' +<%=Request.QueryString["id"]%> +', \'' + $.trim($('#ptab1t8Testdate').val()) + '\')"}',
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        success: function () {
                            formTab1T8Percent();
                            smallBox('Мэдээлэл', 'Амжилттай хадгалагдлаа', '#659265', 4000);
                        },
                        failure: function (response) {
                            alert('FAILURE: ' + response.d);
                        },
                        error: function (xhr, status, error) {
                            var err = eval("(" + xhr.responseText + ")");
                            if (err.Message == 'SessionDied') window.location = '../login.html';
                            else if (err.Message == 'NullReferenceException') {
                                //alert('NullReferenceException');
                            }
                            else window.location = '../#pg/error500.aspx';
                        }
                    });
                }
                else {
                    globalAjaxVar = $.ajax({
                        type: "POST",
                        url: "ws.aspx/WSOracleExecuteNonQuery",
                        data: '{qry:"UPDATE ST_STATES SET TESTDATE=\'' + $.trim($('#ptab1t8Testdate').val()) + '\' WHERE STAFFS_ID=' +<%=Request.QueryString["id"]%> +'"}',
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        success: function () {
                            formTab1T8Percent();
                            smallBox('Мэдээлэл', 'Амжилттай хадгалагдлаа', '#659265', 4000);
                        },
                        failure: function (response) {
                            alert('FAILURE: ' + response.d);
                        },
                        error: function (xhr, status, error) {
                            var err = eval("(" + xhr.responseText + ")");
                            if (err.Message == 'SessionDied') window.location = '../login.html';
                            else if (err.Message == 'NullReferenceException') {
                                //alert('NullReferenceException');
                            }
                            else window.location = '../#pg/error500.aspx';
                        }
                    });
                }
            },
            failure: function (response) {
                alert('FAILURE: ' + response.d);
            },
            error: function (xhr, status, error) {
                var err = eval("(" + xhr.responseText + ")");
                if (err.Message == 'SessionDied') window.location = '../login.html';
                else if (err.Message == 'NullReferenceException') {
                    //alert('NullReferenceException');
                }
                else window.location = '../#pg/error500.aspx';
            }
        });
    });

    function parseDate(s) {
        var b = s.split(/\D/);
        return new Date(b[0], --b[1], b[2]);
    }
    $('#pTab1T8Section1ModalInputFromdate, #pTab1T8Section1ModalInputTodate, #ptab1t8Testdate').datepicker({
        changeMonth: true,
        changeYear: true,
        dateFormat: 'yy-mm-dd',
        prevText: '<i class="fa fa-chevron-left"></i>',
        nextText: '<i class="fa fa-chevron-right"></i>',
        monthNames: ["1-р сар", "2-р сар", "3-р сар", "4-р сар", "5-р сар", "6-р сар", "7-р сар", "8-р сар", "9-р сар", "10-р сар", "11-р сар", "12-р сар"],
        monthNamesShort: ["1-р сар", "2-р сар", "3-р сар", "4-р сар", "5-р сар", "6-р сар", "7-р сар", "8-р сар", "9-р сар", "10-р сар", "11-р сар", "12-р сар"],
        dayNamesMin: ['Ня', 'Да', 'Мя', 'Лх', 'Пү', 'Ба', 'Бя']
    });
</script>
