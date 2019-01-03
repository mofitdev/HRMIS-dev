﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ptab2t2.aspx.cs" Inherits="HRWebApp.pg.profileforms.ptab2t2" %>
<div class="row" style="padding-top:10px;">
    <div class="col-sm-5">
        <div class="well well-sm">
            <fieldset>
                <legend>Одоогийн албан тушаал</legend>
				<div class="row" style="margin-bottom: 5px;">
                    <div class="col-md-4 text-right">
                        *Харьяалагдах байгууллага
                    </div>
                    <div class="col-md-8">
                        <strong id="ptab2t2Section1DivOrg"></strong>
                    </div>
                </div>
				<div class="row" style="margin-bottom: 5px;">
                    <div class="col-md-4 text-right">
                        *Харьяалагдах нэгж
                    </div>
                    <div class="col-md-8">
                        <strong id="ptab2t2Section1DivBranch"></strong>
                    </div>
                </div>
				<div class="row" style="margin-bottom: 5px;">
                    <div class="col-md-4 text-right">
                        *Домайн нэр
                    </div>
                    <div class="col-md-8">
                        <strong id="ptab2t2Section1DivDomainname"></strong>
                    </div>
                </div>
				<div class="row" style="margin-bottom: 5px;">
                    <div class="col-md-4 text-right">
                        *Хөдөлгөөн
                    </div>
                    <div class="col-md-8">
                        <strong id="ptab2t2Section1DivMove"></strong>
                    </div>
                </div>
				<div class="row" style="margin-bottom: 5px;">
                    <div class="col-md-4 text-right">
                        *Албан тушаал төрөл
                    </div>
                    <div class="col-md-8">
                        <strong id="ptab2t2Section1DivPostype"></strong>
                    </div>
                </div>
				<div class="row" style="margin-bottom: 5px;">
                    <div class="col-md-4 text-right">
                        *Албан тушаал
                    </div>
                    <div class="col-md-8">
                        <strong id="ptab2t2Section1DivPos"></strong>
                    </div>
                </div>
				<div class="row" style="margin-bottom: 5px;">
                    <div class="col-md-4 text-right">
                        Албаны ангилал зэрэглэл
                    </div>
                    <div class="col-md-8">
                        <strong id="ptab2t2Section1DivRank"></strong>
                    </div>
                </div>
				<div class="row" style="margin-bottom: 5px;">
                    <div class="col-md-4 text-right">
                        *Томилогдсон огноо
                    </div>
                    <div class="col-md-8">
                        <strong id="ptab2t2Section1DivSigndate"></strong>
                    </div>
                </div>
				<div class="row" style="margin-bottom: 5px;">
                    <div class="col-md-4 text-right">
                        Тушаалын огноо
                    </div>
                    <div class="col-md-8">
                        <strong id="ptab2t2Section1DivTushaaldate"></strong>
                    </div>
                </div>
				<div class="row" style="margin-bottom: 5px;">
                    <div class="col-md-4 text-right">
                        Тушаалын дугаар
                    </div>
                    <div class="col-md-8">
                        <strong id="ptab2t2Section1DivTushaalno"></strong>
                    </div>
                </div>
				<div class="row" style="margin-bottom: 10px;">
                    <div class="col-md-4 text-right">
                        Нэмэлт тайлбар
                    </div>
                    <div class="col-md-8">
                        <strong id="ptab2t2Section1DivDescription"></strong>
                    </div>
                </div>
            </fieldset>
        </div>
    </div>
    <div class="col-sm-7">
        <div class="well well-sm">
            <form id="ptab2t2Section1Form" method="post">
                <fieldset>
                    <legend>Дотоод шилжилт хөдөлгөөн</legend>
					<div class="form-group">
                        <div class="row">
                            <div class="col-md-3">
                                <label class="control-label">*Харьяалагдах байгууллага</label>
                                <select id="pTab1ModalSelectOrg" name="pTab1ModalSelectOrg" runat="server" class="form-control" style="padding: 5px;">
							        <option value="">- Сонго -</option>
						        </select>
                            </div>
                            <div class="col-md-6">
                                <label class="control-label">*Харьяалагдах нэгж</label>
                                <select id="pTab1ModalSelectBranch" name="pTab1ModalSelectBranch" runat="server" class="form-control" style="padding: 5px;">
							        <option value="">- Сонго -</option>
						        </select>
                            </div>
                            <div class="col-md-3">
                                <label class="control-label">*Домайн нэр</label>
                                <input id="pTab1ModalInputDomainname" name="pTab1ModalInputDomainname" runat="server" type="text" class="form-control" placeholder="Домайн нэр" />
                            </div>
                        </div>
                    </div>
                </fieldset>
                <fieldset>
					<div class="form-group">
                        <div class="row">
                            <div class="col-md-3">
                                <label class="control-label">*Хөдөлгөөн</label>
                                <select id="pTab1ModalSelectMove" name="pTab1ModalSelectMove" runat="server" class="form-control" style="padding: 5px;">
							        <option value="">- Сонго -</option>
						        </select>
                            </div>
                            <div class="col-md-3">
                                <label class="control-label">*Албан тушаал төрөл</label>
                                <select id="pTab1ModalSelectPostype" name="pTab1ModalSelectPostype" runat="server" class="form-control" style="padding: 5px;">
							        <option value="">- Сонго -</option>
						        </select>
                            </div>
                            <div class="col-md-3">
                                <label class="control-label">*Албан тушаал</label>
                                <select id="pTab1ModalSelectPos" name="pTab1ModalSelectPos" runat="server" class="form-control" style="padding: 5px;">
							        <option value="">- Сонго -</option>
						        </select>
                            </div>
                            <div class="col-md-3">
                                <label class="control-label">Албаны ангилал зэрэглэл</label>
                                <select id="pTab1ModalSelectRank" name="pTab1ModalSelectRank" runat="server" class="form-control" style="padding: 5px;">
							        <option value="">- Сонго -</option>
						        </select>
                            </div>
                        </div>
                    </div>
					<div class="form-group">
                        <div class="row">
                            <div class="col-md-2">
                                <label class="control-label">*Томил/огноо</label>
                                <input id="pTab1ModalInputSigndate" name="pTab1ModalInputSigndate" runat="server" type="text" class="form-control" placeholder="Томилогдсон огноо" />
                            </div>
                            <div class="col-md-2">
                                <label class="control-label">Тушаалын огноо</label>
                                <input id="pTab1ModalInputTushaaldate" name="pTab1ModalInputTushaaldate" runat="server" type="text" class="form-control" placeholder="Тушаалын огноо" />
                            </div>
                            <div class="col-md-2">
                                <label class="control-label">Тушаалын дугаар</label>
                                <input id="pTab1ModalInputTushaalno" name="pTab1ModalInputTushaalno" runat="server" type="text" class="form-control" placeholder="Тушаалын дугаар" />
                            </div>
                            <div class="col-md-6">
                                <label class="control-label">Нэмэлт тайлбар</label>
                                <input id="pTab1ModalInputDescription" name="pTab1ModalInputDescription" runat="server" type="text" class="form-control" placeholder="Нэмэлт тайлбар" />
                            </div>
                        </div>
                    </div>
                </fieldset>
                <div class="form-group">
                    <div class="row">
                        <div class="col-md-12 text-right">
                            <button id="ptab2t2Section1ButtonSave" runat="server" type="submit" class="btn btn-primary"><span class="glyphicon glyphicon-floppy-disk"></span> Хадгалах</button>
                        </div>
                    </div>
                </div>
            </form>
        </div>
    </div>
</div>
<script type="text/javascript">
    globalAjaxVar = $.ajax({
        type: "POST",
        url: "ws.aspx/staffreg_staffregTab1ModalBranchForDDL",
        data: '{}',
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (msg) {
            $("#pTab1ModalSelectBranch").html('<option value="">- Сонго -</option>' + msg.d);
            globalAjaxVar = $.ajax({
                type: "POST",
                url: "ws.aspx/staffreg_staffregTab1ModalRankForDDL",
                data: '{}',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (msg) {
                    $("#pTab1ModalSelectRank").html(msg.d);
                    globalAjaxVar = $.ajax({
                        type: "POST",
                        url: "ws.aspx/staffreg_staffregTab1ModalMoveForDDL",
                        data: '{tp:"5"}',
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        success: function (msg) {
                            $("#pTab1ModalSelectMove").html(msg.d);
                            globalAjaxVar = $.ajax({
                                type: "POST",
                                url: "ws.aspx/WSOracleExecuteScalar",
                                data: '{qry:"SELECT i.NAME||\'~\'||CASE WHEN c.ID=c.FATHER_ID THEN c.INITNAME ELSE j.INITNAME||\' - \'||c.INITNAME END||\'~\'||a.DOMAIN_USER||\'~\'||d.NAME||\'~\'||f.NAME||\'~\'||g.NAME||\'~\'||NVL(h.NAME,\'-\')||\'~\'||b.DT||\'~\'||NVL(b.TUSHAALDATE,\'-\')||\'~\'||NVL(b.TUSHAALNO,\'-\')||\'~\'||NVL(b.DESCRIPTION,\'-\') FROM ST_STAFFS a INNER JOIN ST_STBR b ON a.ID=b.STAFFS_ID AND b.ISACTIVE=1 INNER JOIN ST_BRANCH c ON b.BRANCH_ID=c.ID INNER JOIN ST_BRANCH j ON c.FATHER_ID=j.ID INNER JOIN ST_ORG i ON c.ORG_ID=i.ID INNER JOIN STN_MOVE d ON b.MOVE_ID=d.ID INNER JOIN STN_POSTYPE f ON b.POSTYPE_ID=f.ID INNER JOIN STN_POS g ON b.POS_ID=g.ID LEFT JOIN ST_RANK h ON b.RANK_ID=h.ID WHERE a.ID=' + <%=Request.QueryString["id"]%> + '"}',
                                contentType: "application/json; charset=utf-8",
                                dataType: "json",
                                success: function (msg) {
                                    $('#ptab2t2Section1DivOrg').html(msg.d.split('~')[0]);
                                    $('#ptab2t2Section1DivBranch').html(msg.d.split('~')[1]);
                                    $('#ptab2t2Section1DivDomainname').html(msg.d.split('~')[2]);
                                    $('#ptab2t2Section1DivMove').html(msg.d.split('~')[3]);
                                    $('#ptab2t2Section1DivPostype').html(msg.d.split('~')[4]);
                                    $('#ptab2t2Section1DivPos').html(msg.d.split('~')[5]);
                                    $('#ptab2t2Section1DivRank').html(msg.d.split('~')[6]);
                                    $('#ptab2t2Section1DivSigndate').html(msg.d.split('~')[7]);
                                    $('#ptab2t2Section1DivTushaaldate').html(msg.d.split('~')[8]);
                                    $('#ptab2t2Section1DivTushaalno').html(msg.d.split('~')[9]);
                                    $('#ptab2t2Section1DivDescription').html(msg.d.split('~')[10]);
                                },
                                failure: function (response) {
                                    alert('FAILURE: ' + response.d);
                                },
                                error: function (xhr, status, error) {
                                    var err = eval("(" + xhr.responseText + ")");
                                    if (err.Message == 'SessionDied' || $.trim(err.Message) == 'Invalid operation. The connection is closed.') window.location = '../login.html';
                                    else window.location = '../#pg/error500.aspx';
                                }
                            });
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

    $('#ptab2t2Section1Form').bootstrapValidator({
        fields: {
            pTab1ModalSelectOrg: {
                group: '.col-md-3',
                validators: {
                    notEmpty: {
                        message: 'Сонгоно уу'
                    }
                }
            },
            pTab1ModalSelectBranch: {
                group: '.col-md-6',
                validators: {
                    notEmpty: {
                        message: 'Сонгоно уу'
                    }
                }
            },
            pTab1ModalInputDomainname: {
                group: '.col-md-3',
                validators: {
                    notEmpty: {
                        message: 'Оруулна уу'
                    },
                    stringLength: {
                        max: 10,
                        message: 'Уртдаа 10 тэмдэгт авна'
                    }
                }
            },
            pTab1ModalSelectMove: {
                group: '.col-md-3',
                validators: {
                    notEmpty: {
                        message: 'Сонгоно уу'
                    }
                }
            },
            pTab1ModalSelectPostype: {
                group: '.col-md-3',
                validators: {
                    notEmpty: {
                        message: 'Сонгоно уу'
                    }
                }
            },
            pTab1ModalSelectPos: {
                group: '.col-md-3',
                validators: {
                    notEmpty: {
                        message: 'Сонгоно уу'
                    }
                }
            },
            pTab1ModalInputSigndate: {
                group: '.col-md-2',
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
            pTab1ModalInputTushaaldate: {
                group: '.col-md-2',
                validators: {
                    date: {
                        format: 'YYYY-MM-DD',
                        message: 'Огноо буруу орсон байна. /Жил-Сар-Өдөр/'
                    }
                }
            },
            pTab1ModalInputTushaalno: {
                group: '.col-md-2',
                validators: {
                    stringLength: {
                        max: 10,
                        message: 'Уртдаа 10 тэмдэгт авна'
                    }
                }
            }
        },
        onSuccess: function (e, data) {
            e.preventDefault();
            globalAjaxVar = $.ajax({
                type: "POST",
                url: "ws.aspx/WSOracleExecuteScalar",
                data: '{qry:"SELECT COUNT(1) FROM ST_STAFFS a INNER JOIN ST_STBR b ON a.ID=b.STAFFS_ID AND b.ISACTIVE=1 INNER JOIN STN_MOVE c ON b.MOVE_ID=c.ID WHERE c.ACTIVE=1 AND a.DOMAIN_USER=\'' + $.trim($('#pTab1ModalInputDomainname').val()) + '\' AND a.ID!=' + <%=Request.QueryString["id"]%> +'"}',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (msg) {
                    if (msg.d != '0') alert($.trim($('#pTab1ModalInputDomainname').val()) + ' домайн нэртэй идэвхтэй хэрэглэгч бүртгэгдсэн байна!');
                    else {
                        globalAjaxVar = $.ajax({
                            type: "POST",
                            url: "ws.aspx/STAFF_MOVE",
                            data: '{P_STAFFS_ID:"' + <%=Request.QueryString["id"]%> + '", P_DT:"' + $.trim($('#pTab1ModalInputSigndate').val()) + '", P_BRANCH_ID:"' + $('#pTab1ModalSelectBranch option:selected').val() + '", P_POSTYPE_ID:"' + $('#pTab1ModalSelectPostype option:selected').val() + '", P_POS_ID:"' + $('#pTab1ModalSelectPos option:selected').val() + '", P_RANK_ID:"' + $('#pTab1ModalSelectRank option:selected').val() + '", P_TUSHAALDATE:"' + $.trim($('#pTab1ModalInputTushaaldate').val()) + '", P_TUSHAALNO:"' + replaceDisplayToDatabase($.trim($('#pTab1ModalInputTushaalno').val())) + '", P_MOVE_ID:"' + $('#pTab1ModalSelectMove option:selected').val() + '", P_DESCRIPTION:"' + replaceDisplayToDatabase($.trim($('#pTab1ModalInputDescription').val())) + '", P_STAFFID:"' + $.trim($('#indexUserId').html()) + '", P_DOMAIN_USER:"' + $.trim($('#pTab1ModalInputDomainname').val()) + '"}',
                            contentType: "application/json; charset=utf-8",
                            dataType: "json",
                            success: function () {
                                globalAjaxVar = $.ajax({
                                    type: "POST",
                                    url: "ws.aspx/WSOracleExecuteScalar",
                                    data: '{qry:"SELECT i.NAME||\'~\'||CASE WHEN c.ID=c.FATHER_ID THEN c.INITNAME ELSE j.INITNAME||\' - \'||c.INITNAME END||\'~\'||a.DOMAIN_USER||\'~\'||d.NAME||\'~\'||f.NAME||\'~\'||g.NAME||\'~\'||NVL(h.NAME,\'-\')||\'~\'||b.DT||\'~\'||NVL(b.TUSHAALDATE,\'-\')||\'~\'||NVL(b.TUSHAALNO,\'-\')||\'~\'||NVL(b.DESCRIPTION,\'-\') FROM ST_STAFFS a INNER JOIN ST_STBR b ON a.ID=b.STAFFS_ID AND b.ISACTIVE=1 INNER JOIN ST_BRANCH c ON b.BRANCH_ID=c.ID INNER JOIN ST_BRANCH j ON c.FATHER_ID=j.ID INNER JOIN ST_ORG i ON c.ORG_ID=i.ID INNER JOIN STN_MOVE d ON b.MOVE_ID=d.ID INNER JOIN STN_POSTYPE f ON b.POSTYPE_ID=f.ID INNER JOIN STN_POS g ON b.POS_ID=g.ID LEFT JOIN ST_RANK h ON b.RANK_ID=h.ID WHERE a.ID=' + <%=Request.QueryString["id"]%> + '"}',
                                    contentType: "application/json; charset=utf-8",
                                    dataType: "json",
                                    success: function (msg) {
                                        //bind section 1
                                        $('#ptab2t2Section1DivOrg').html(msg.d.split('~')[0]);
                                        $('#ptab2t2Section1DivBranch').html(msg.d.split('~')[1]);
                                        $('#ptab2t2Section1DivDomainname').html(msg.d.split('~')[2]);
                                        $('#ptab2t2Section1DivMove').html(msg.d.split('~')[3]);
                                        $('#ptab2t2Section1DivPostype').html(msg.d.split('~')[4]);
                                        $('#ptab2t2Section1DivPos').html(msg.d.split('~')[5]);
                                        $('#ptab2t2Section1DivRank').html(msg.d.split('~')[6]);
                                        $('#ptab2t2Section1DivSigndate').html(msg.d.split('~')[7]);
                                        $('#ptab2t2Section1DivTushaaldate').html(msg.d.split('~')[8]);
                                        $('#ptab2t2Section1DivTushaalno').html(msg.d.split('~')[9]);
                                        $('#ptab2t2Section1DivDescription').html(msg.d.split('~')[10]);
                                        //empty section 2
                                        $('#pTab1ModalSelectBranch').val('');
                                        $('#pTab1ModalInputDomainname').val('');
                                        $('#pTab1ModalSelectMove').val('');
                                        $('#pTab1ModalSelectPostype').val('');
                                        $('#pTab1ModalSelectPos').val('');
                                        $('#pTab1ModalSelectRank').val('');
                                        $('#pTab1ModalInputSigndate').val('');
                                        $('#pTab1ModalInputTushaaldate').val('');
                                        $('#pTab1ModalInputTushaalno').val('');
                                        $('#pTab1ModalInputDescription').val('');

                                        $('#profileMainH1Names').find('small').html(msg.d.split('~')[1] + ' | ' + msg.d.split('~')[5]);
                                        smallBox('Ажилтаны дотоод шилжилт хөдөлгөөн', 'Амжилттай хадгалагдлаа', '#659265', 4000);
                                    },
                                    failure: function (response) {
                                        alert('FAILURE: ' + response.d);
                                    },
                                    error: function (xhr, status, error) {
                                        var err = eval("(" + xhr.responseText + ")");
                                        if (err.Message == 'SessionDied' || $.trim(err.Message) == 'Invalid operation. The connection is closed.') window.location = '../login.html';
                                        else window.location = '../#pg/error500.aspx';
                                    }
                                });

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


    $('#pTab1ModalInputSigndate, #pTab1ModalInputTushaaldate').datepicker({
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
