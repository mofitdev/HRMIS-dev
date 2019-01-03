<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="dashboard.aspx.cs" Inherits="HRWebApp.pg.dashboard" %>
<style type="text/css">
    .fc-header-title {
    margin: -18px 0 0 0;
    }
    .fc-event-time {
        display:none!important;
    }
    .fc-event-title {
    padding: 10px 0 2px 3px!important;
    }
</style>
<div class="row">
    <div class="col-xs-12 col-sm-7 col-md-7 col-lg-4">
        <h1 class="page-title txt-color-blueDark">
        <i class="fa-fw fa fa-home"></i>
        <span>> Хянах самбар</span>
        </h1>
    </div>
    <div class="col-xs-12 col-sm-5 col-md-5 col-lg-8">
		<ul id="sparks" class="">
			<li class="sparks-info font-md">
				Системд холбогдсон <label class="txt-color-blue font-lg">10</label>
			</li>
		</ul>
	</div>
</div>
<section id="widget-grid" class="">
    <div class="row">
        <div class="col-sm-6">
            <div class="well well-sm">
                <div class="row">
                    <div class="col-sm-3 profile-pic">
					    <img id="dashboardSection1Image" runat="server" src="../img/avatars/male.png">
                        <div id="dashboardSection1DivStafftype" runat="server" style="padding: 5px 0 0 5px;">
                                            
						</div>
                        <div style="margin-top: 10px;">
                            <a id="dashboardSection1btnStaffAnket" runat="server" href="javascript:void(0);" class="btn btn-primary btn-xs"> <i class="fa fa-files-o"></i> Миний анкет </a>
                        </div>
                        <div style="margin-top: 10px;">
                            <a id="dashboardSection1btnStaffAnketPrint" runat="server" href="javascript:void(0);" class="btn btn-xs bg-color-teal txt-color-white" data-target="#dashboardStaffAnketPrint" data-toggle="modal"> <i class="fa fa-print"></i> ТАХ маягт №1 Хэвлэх </a>
                        </div>
				    </div>
                    <div class="col-sm-5">
						<h1 id="dashboardSection1H1Names" runat="server" style="text-transform:uppercase;">
                                            
						</h1>
						<ul class="list-unstyled">
							<li>
								<p class="text-muted">
									<i class="fa fa-phone"></i>&nbsp;&nbsp;<span id="dashboardSection1SpanTels" runat="server" class="txt-color-darken">-</span>
								</p>
							</li>
							<li>
								<p class="text-muted">
									<i class="fa fa-envelope"></i>&nbsp;&nbsp;<a id="dashboardSection1SpanEmail" runat="server" href="#">-</a>
								</p>
							</li>
							<li>
								<p class="text-muted">
									<i class="fa fa-home"></i>&nbsp;&nbsp;<span id="dashboardSection1SpanAddress" runat="server" class="txt-color-darken">-</span>
								</p>
							</li>
						</ul>
                        <br />
                        <p class="font-md"><i>Нийт ажилласан</i></p>
                        <p id="dashboardSection1PWorkedTime" runat="server"></p>
					</div>
                    <div class="col-sm-4">
                        <div style="margin:5px 5px 0 0;">
                            <div style="display:block; overflow:hidden;">
                                <p class="text-right font-sm text-muted no-margin"><i>Ерөнхий мэдээлэл</i></p>
                                <div class="progress no-margin">
                                    <div id="dashboardSection1MainPercent" runat="server" class="progress-bar bg-color-greenLight" aria-valuetransitiongoal="0"></div>
                                </div>
                            </div>
                            <div style="display:block; overflow:hidden; margin-top:5px;">
                                <p class="text-right font-sm text-muted no-margin"><i>Гэр бүлийн мэдээлэл</i></p>
                                <div class="progress progress-micro" style="margin-bottom: 5px;">
									<div id="dashboardSection1Tab1T2Percent" runat="server" class="progress-bar progress-bar-primary" role="progressbar" style="width: 0%;"></div>
								</div>
                            </div>
                            <div style="display:block; overflow:hidden; margin-top:5px;">
                                <p class="text-right font-sm text-muted no-margin"><i>Боловсролын мэдээлэл</i></p>
                                <div class="progress progress-micro" style="margin-bottom: 5px;">
									<div id="dashboardSection1Tab1T3Percent" runat="server" class="progress-bar progress-bar-primary" role="progressbar" style="width: 0%;"></div>
								</div>
                            </div>
                            <div style="display:block; overflow:hidden; margin-top:5px;">
                                <p class="text-right font-sm text-muted no-margin"><i>Мэргэшил сургалтын мэдээлэл</i></p>
                                <div class="progress progress-micro" style="margin-bottom: 5px;">
									<div id="dashboardSection1Tab1T4Percent" runat="server" class="progress-bar progress-bar-primary" role="progressbar" style="width: 0%;"></div>
								</div>
                            </div>
                            <div style="display:block; overflow:hidden; margin-top:5px;">
                                <p class="text-right font-sm text-muted no-margin"><i>Зэрэг дэв, Цолын мэдээлэл</i></p>
                                <div class="progress progress-micro" style="margin-bottom: 5px;">
									<div id="dashboardSection1Tab1T5Percent" runat="server" class="progress-bar progress-bar-primary" role="progressbar" style="width: 0%;"></div>
								</div>
                            </div>
                            <div style="display:block; overflow:hidden; margin-top:5px;">
                                <p class="text-right font-sm text-muted no-margin"><i>Ур чадварын мэдээлэл</i></p>
                                <div class="progress progress-micro" style="margin-bottom: 5px;">
									<div id="dashboardSection1Tab1T6Percent" runat="server" class="progress-bar progress-bar-primary" role="progressbar" style="width: 0%;"></div>
								</div>
                            </div>
                            <div style="display:block; overflow:hidden; margin-top:5px;">
                                <p class="text-right font-sm text-muted no-margin"><i>Гадаад хэлний мэдээлэл</i></p>
                                <div class="progress progress-micro" style="margin-bottom: 5px;">
									<div id="dashboardSection1Tab1T7Percent" runat="server" class="progress-bar progress-bar-primary" role="progressbar" style="width: 0%;"></div>
								</div>
                            </div>
                            <div style="display:block; overflow:hidden; margin-top:5px;">
                                <p class="text-right font-sm text-muted no-margin"><i>Туршлагын мэдээлэл</i></p>
                                <div class="progress progress-micro" style="margin-bottom: 5px;">
									<div id="dashboardSection1Tab1T8Percent" runat="server" class="progress-bar progress-bar-primary" role="progressbar" style="width: 0%;"></div>
								</div>
                            </div>
                            <div style="display:block; overflow:hidden; margin-top:5px;">
                                <p class="text-right font-sm text-muted no-margin"><i>Бусад мэдээлэл</i></p>
                                <div class="progress progress-micro" style="margin-bottom: 5px;">
									<div id="dashboardSection1Tab1T9Percent" runat="server" class="progress-bar progress-bar-primary" role="progressbar" style="width: 0%;"></div>
								</div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="jarviswidget jarviswidget-sortable" data-widget-colorbutton="false" data-widget-togglebutton="false" data-widget-editbutton="false" data-widget-fullscreenbutton="false" data-widget-deletebutton="false">
                <header>
                    <span class="widget-icon"><i class="fa fa-calendar"></i></span>
                    <h2>Миний цагийн бүртгэл</h2>
                    <div class="widget-toolbar">  
                        <div class="btn-group options" style="margin-right:0px !important;">
                            <a id="dashboardSection1btnStaffWorkingtimeDescription" runat="server" class="btn btn-warning btn-xs" href="javascript:void(0);" data-target="#dashboardStaffWorkingtimeDescriptionModal" data-toggle="modal">Сарын дэлгэрэнгүй харах</a>
                        </div> 
                    </div>
                </header>
                <div>
                    <div id="loaderTab1" class="search-background">
                        <label>
                            <img width="64" height="" src="img/loading.gif"/>
                            <h2 style="width:100%; display:block; overflow:hidden; padding:20px 0 0 0; color:#fff; padding-top:0px; margin-top:0px;">Уншиж байна !</h2>
                        </label>
                    </div> 
                    <div class="widget-body no-padding">
                        <div class="widget-body-toolbar">
                            <div class="Colvis TableTools" style="right:125px; top:0px; z-index:5; margin-top:10px;">
                                Сар:
                            </div>
                            <div class="Colvis TableTools" style="width:45px;right:75px; top:0px; z-index:5; margin-top:5px;">
                                <select id="dashboardSection1WorkingtimeSelectMonth" name="dashboardSection1WorkingtimeSelectMonth" runat="server" class="form-control" style="padding:1px"><option value="1">01</option><option value="2">02</option><option value="3">03</option><option value="4">04</option><option value="5">05</option><option value="6">06</option><option value="7">07</option><option value="8">08</option><option value="9">09</option><option value="10">10</option><option value="11">11</option><option value="12">12</option></select>
                            </div>
                            <div class="Colvis TableTools" style="width:60px;right:6px; top:0px; z-index:5; margin-top:5px;">
                                <select id="dashboardSection1WorkingtimeSelectYear" name="dashboardSection1WorkingtimeSelectYear" runat="server" class="form-control" style="padding:1px"><option>2012</option><option>2013</option><option>2014</option><option>2015</option><option>2016</option><option>2017</option><option>2018</option><option>2019</option><option>2020</option></select>
                            </div>
                        </div>
                        <div id="workingtimeCalendar">

                        </div>
                    </div>
                </div> 
            </div>
        </div>
        <div class="col-sm-3">
            <div class="well well-sm">
                <h3 class="text-primary" style="margin: 10px 0;">Албан хаагч</h3>
                <div id="dashboardSection2RankTable" runat="server" class="row">
                    <table class="table">
                        <tbody>
                            <tr>
                                <th></th>
                                <th class="text-center">УТ</th>
                                <th class="text-center">ТТ</th>
                                <th class="text-center">ТҮ</th>
                                <th class="text-center">ТЗ</th>
                                <th class="text-center">ТӨ</th>
                                <th class="text-center">АА</th>
                                <th class="text-center">Нийт</th>
                            </tr>
                            <tr>
                                <td class="text-right">Бүгд</td>
                                <th class="text-center text-primary">0</th>
                                <th class="text-center text-primary">0</th>
                                <th class="text-center text-primary">0</th>
                                <th class="text-center text-primary">0</th>
                                <th class="text-center text-primary">0</th>
                                <th class="text-center text-primary">0</th>
                                <th class="text-center text-primary">0</th>
                            </tr>
                            <tr>
                                <td class="text-right">Эрэгтэй</td>
                                <th class="text-center text-primary">0</th>
                                <th class="text-center text-primary">0</th>
                                <th class="text-center text-primary">0</th>
                                <th class="text-center text-primary">0</th>
                                <th class="text-center text-primary">0</th>
                                <th class="text-center text-primary">0</th>
                                <th class="text-center text-primary">0</th>
                            </tr>
                            <tr>
                                <td class="text-right">Эмэгтэй</td>
                                <th class="text-center text-primary">0</th>
                                <th class="text-center text-primary">0</th>
                                <th class="text-center text-primary">0</th>
                                <th class="text-center text-primary">0</th>
                                <th class="text-center text-primary">0</th>
                                <th class="text-center text-primary">0</th>
                                <th class="text-center text-primary">0</th>
                            </tr>
                        </tbody>
                    </table>
                </div>
                <h3 class="text-primary" style="margin: 10px 0;">Нийт ажилчдын тоо</h3>
                <div class="row">
                    <div class="col-sm-3 text-center">
                        <i class="fa fa-group font-xl"></i> <span id="dashboardSection2GenderTotal" runat="server" class="font-lg">0</span>
                    </div>
                    <div class="col-sm-3 text-center">
                        <i class="fa fa-male font-xl"></i> <span id="dashboardSection2GenderMale" runat="server" class="font-lg">0</span>
                    </div>
                    <div class="col-sm-3 text-center">
                        <i class="fa fa-female font-xl"></i> <span id="dashboardSection2GenderFemale" runat="server" class="font-lg">0</span>
                    </div>
                    <div class="col-sm-3 text-center">
                        <span id="dashboardSection2GenderPie" runat="server" class="sparkline display-inline" data-sparkline-type="pie" data-sparkline-offset="100" data-sparkline-piesize="26px">0,0</span>
                    </div>
                </div>
                <h3 class="text-primary" style="margin: 10px 0;">Боловсрол <small class="pull-right" style="margin-top: 8px;"><a class="btn btn-link btn-xs font-sm text-muted" href="javascript:void(0);"><i>[Дэлгэрэнгүй харах...]</i></a></small></h3>
                <div id="dashboardSection2Edutplist" runat="server" class="row">
                    
                </div>
            </div>
            <div class="well well-sm">
                <h3 class="text-primary" style="margin: 10px 0;">Сүүлд томилогдсон</h3>
                <div id="dashboardSection2DivLastActionList" runat="server" class="chat-body no-padding profile-message">
					<i class="text-muted">Мэдээлэл олдсонгүй...</i>
				</div>
            </div>
        </div>
        <div class="col-sm-3">
            <div class="well well-sm">
                <h3 class="text-primary" style="margin: 10px 0;">Удирдлага</h3>
                <div id="dashboardSection3DivBossList" runat="server" class="chat-body no-padding profile-message">
					<i class="text-muted">Мэдээлэл олдсонгүй...</i>
				</div>
            </div>
            <div class="well well-sm">
                <h3 class="text-primary" style="margin: 10px 0;">Төрсөн өдөр</h3>
                <div id="dashboardSection3DivBdayList" runat="server"  class="chat-body no-padding profile-message">
					<i class="text-muted">Мэдээлэл олдсонгүй...</i>
				</div>
            </div>
        </div>
    </div>
    <div class="row">
        <div class="col-sm-6">
            
        </div>
        <div class="col-sm-3">
            
        </div>
        <div class="col-sm-3">
            
        </div>
    </div>
</section>
<div class="modal fade" id="dashboardStaffAnketPrint" tabindex="-1" role="dialog" aria-labelledby="remoteModalLabel" aria-hidden="true">  
	<div class="modal-dialog modal-lg" style="width:70%;">
        <div style="width:56%; display:block; height: 35px; margin:0 auto;">
            <div class="btn-group pull-right">
                <a href="javascript:void(0);" class="btn btn-default" rel="tooltip" data-placement="left" data-original-title="Word татах" onclick="exportWord('#dashboardStaffAnketPrintContent', 'Анкет')">
                    <i class="fa fa-file-word-o"></i>
                </a>
                <a href="javascript:void(0);" class="btn btn-default printBtn" rel="tooltip" data-placement="right" data-original-title="Хэвлэх" onclick="PrintElem('#dashboardStaffAnketPrintContent')">
                    <i class="fa fa-print"></i>
                </a>
            </div>
        </div>
		<div id="dashboardStaffAnketPrintContent" class="modal-content reports" style="width:56%;">
			
		</div>  
	</div>  
</div>
<div id="dashboardStaffWorkingtimeDescriptionModal" class="modal fade" tabindex="-1" role="dialog" aria-labelledby="remoteModalLabel" aria-hidden="true">
	<div class="modal-dialog modal-lg">
        <div style="width:56%; display:block; height: 35px; margin:0 auto;">
            <div class="btn-group pull-right">
                <a href="javascript:void(0);" class="btn btn-default" rel="tooltip" data-placement="left" data-original-title="Word татах" onclick="exportWord('#dashboardStaffWorkingtimeDescriptionModalContent', 'Ирц')">
                    <i class="fa fa-file-word-o"></i>
                </a>
                <a href="javascript:void(0);" class="btn btn-default printBtn" rel="tooltip" data-placement="right" data-original-title="Хэвлэх" onclick="PrintElem('#dashboardStaffWorkingtimeDescriptionModalContent')">
                    <i class="fa fa-print"></i>
                </a>
            </div>
        </div>
        <div id="dashboardStaffWorkingtimeDescriptionModalContent" class="modal-content reports" style="width:56%;">
			
		</div> 
    </div>
</div>
<script type="text/javascript">
    pageSetUp();
    var globalAjaxVar = null;
    var pagefunction = function () {
        if (getUrlVars()["t"] == 'f') smallBox($('#staffName').text(), 'Системд тавтай морил...', '#3276b1', 4000);
        $('.progress-bar').progressbar({
            display_text: 'fill'
        });

        $('#dashboardSection1btnStaffWorkingtimeDescription').attr('href', '../pg/dashboardStaffWorkingtimeDescriptionModal.aspx?yr=' + $('#dashboardSection1WorkingtimeSelectYear option:selected').val() + '&mnth=' + $('#dashboardSection1WorkingtimeSelectMonth option:selected').val() + '&stid=' + $('#indexUserId').html());

        dataBindworkingtumeTab1Calendar();
    }
    var pagedestroy = function () {
        if (globalAjaxVar != null) {
            globalAjaxVar.abort();
            globalAjaxVar = null;
        }
    }
    loadScript("js/plugin/bootstrap-progressbar/bootstrap-progressbar.min.js", pagefunction);

    function getUrlVars() {
        var vars = [], hash;
        var hashes = window.location.href.slice(window.location.href.indexOf('?') + 1).split('&');
        for (var i = 0; i < hashes.length; i++) {
            hash = hashes[i].split('=');
            vars.push(hash[0]);
            vars[hash[0]] = hash[1];
        }
        return vars;
    }
    function smallBox(vartitle, varcontent, varcolor, vartimeout) {
        $.smallBox({
            title: vartitle,
            content: varcontent,
            color: varcolor,
            iconSmall: "fa fa-check fa-2x fadeInRight animated",
            timeout: vartimeout
        });
    }
    function showLoader(el) {
        $('#' + el).css({ "display": "block", "background": "rgba(0, 0, 0, .3)" });
    }
    function hideLoader(el) {
        $('#' + el).css({ "display": "none" });
    };

    //CALENDAR
    var calendarSource = '';
    function dataBindworkingtumeTab1Calendar() {
        $('#workingtimeCalendar').html('');
        showLoader('loaderTab1');
        calendarSource = 'pg/dashboardStaffWorkingtimeCalendar.aspx?yr=' + $('#dashboardSection1WorkingtimeSelectYear option:selected').val() + '&mnth=' + $('#dashboardSection1WorkingtimeSelectMonth option:selected').val() + '&stid=' + $('#indexUserId').html();
        var hdr = {
            left: 'title',
            center: 'month,agendaWeek,agendaDay',
            right: 'prev,today,next'
        };
        var datas;
        var calendar = $('#workingtimeCalendar').fullCalendar({
            header: hdr,
            buttonText: {
                prev: '<i class="fa fa-chevron-left"></i>',
                next: '<i class="fa fa-chevron-right"></i>'
            },
            editable: false,
            selectable: false,
            selectHelper: true,
            droppable: false,
            weekends: true,
            draggable: false,
            year: $('#workingtimeTab1SelectYear option:selected').val(),
            month: $('#workingtimeTab1SelectMonth option:selected').val() - 1,
            date: 1,
            events: calendarSource,
            eventRender: function (event, element, icon) {
                element.find('.fc-event-title').append("<br/><span class='ultra-light'>" + event.description + "</span>");
                element.find('.fc-event-inner').append('<span class="air air-top-right font-xs" style="padding-right: 2px;"><i class="fa fa-clock-o"></i> '+event.tm+'</span>');
            },
            windowResize: function (event, ui) {
                $('#calendar').fullCalendar('render');
            },
            drop: function (date, allDay) { // this function is called when something is dropped
                var originalEventObject = $(this).data('eventObject');
                var copiedEventObject = $.extend({}, originalEventObject);
                copiedEventObject.start = date;
                copiedEventObject.allDay = allDay;
                $('#calendar').fullCalendar('renderEvent', copiedEventObject, true);
                if ($('#drop-remove').is(':checked')) {
                    $(this).remove();
                }
            },
            loading: function (bool) {
                if (bool) showLoader('loaderTab1');
                else hideLoader('loaderTab1');
            }
        });
        /* hide default buttons */
        $('.fc-header-right, .fc-header-center').hide();
        hideLoader('loaderTab1');
    }
    function reloadCalendar() {
        $('#workingtimeCalendar').fullCalendar('gotoDate', $('#dashboardSection1WorkingtimeSelectYear option:selected').val() + '-' + $('#dashboardSection1WorkingtimeSelectMonth option:selected').text() + '-01');
        $('#dashboardSection1btnStaffWorkingtimeDescription').attr('href', '../pg/dashboardStaffWorkingtimeDescriptionModal.aspx?yr=' + $('#dashboardSection1WorkingtimeSelectYear option:selected').val() + '&mnth=' + $('#dashboardSection1WorkingtimeSelectMonth option:selected').val() + '&stid=' + $('#indexUserId').html());
        var calendarNewSource = 'pg/dashboardStaffWorkingtimeCalendar.aspx?yr=' + $('#dashboardSection1WorkingtimeSelectYear option:selected').val() + '&mnth=' + $('#dashboardSection1WorkingtimeSelectMonth option:selected').val() + '&stid=' + $('#indexUserId').html();
        $('#workingtimeCalendar').fullCalendar('removeEventSource', calendarSource);
        $('#workingtimeCalendar').fullCalendar('refetchEvents');
        $('#workingtimeCalendar').fullCalendar('addEventSource', calendarNewSource);
        $('#workingtimeCalendar').fullCalendar('refetchEvents');
        calendarSource = calendarNewSource;
    }
    $("#dashboardSection1WorkingtimeSelectMonth, #dashboardSection1WorkingtimeSelectYear").change(function () {
        reloadCalendar();
    });
</script>
