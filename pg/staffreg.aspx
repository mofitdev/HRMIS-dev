<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="staffreg.aspx.cs" Inherits="HRWebApp.pg.staffreg" %>
<style>
    .stafflistimage {
        width: 25px;
        border-radius: 0;
    }
</style>
<div class="col-xs-12 col-sm-7 col-md-7 col-lg-4">
    <h1 class="page-title txt-color-blueDark">
    <i class="fa-fw fa fa-home"></i>
    <span>> Ажилтаны бүртгэл</span>
    </h1>
</div>
<section id="widget-grid">
    <div id="pIsRole" runat="server" class="row" style="display:none;">
        <div class="col-xs-12 col-sm-12 col-md-12 col-lg-12" >
            ТАНЬД ХАНДАХ ЭРХ БАЙХГҮЙ БАЙНА!
        </div>
    </div>
    <div id="pMainDiv" runat="server" class="row">
        <article class="col-xs-12 col-sm-12 col-md-12 col-lg-12">
            <div class="jarviswidget" data-widget-sortable="false" data-widget-colorbutton="false" data-widget-togglebutton="false" data-widget-editbutton="false" data-widget-fullscreenbutton="false" data-widget-deletebutton="false">
                <header>
                    <span class="widget-icon"> 
                        <i class="fa fa-table"></i> 
                    </span>
                    <h2 id="pTab1H2Title" runat="server">Ажилтаны бүртгэл</h2>
                </header>
                <div>
                    <div class="Colvis TableTools" style="right:75px; top:4px; z-index:5; margin-top:7px;"><label>Илэрц: </label></div>
                    <div class="Colvis TableTools" style="width:62px; right:120px; top:8px; z-index:5; margin-top:1px;">
                        <button id="pTab1AddBtn" runat="server" class="btn btn-primary btn-xs" type="button" data-target="#pTab1Modal" data-toggle="modal" onclick="showAddEditTab1(this,'нэмэх')"><i class="fa fa-plus"></i> Нэмэх</button>
                    </div>
                    <div class="Colvis TableTools" style="width:155px; right:195px; top:5px; z-index:5; margin-top:1px;">
                        <select id="pTab1SelectType" name="pTab1SelectType" runat="server" class="form-control" style="padding: 1px;">
							<option value="">- Бүгд -</option>
						</select>
                    </div>
                    <div class="Colvis TableTools" style="right:355px; top:4px; z-index:5; margin-top:7px;"><label>Төлөв:</label></div>
                    <div class="Colvis TableTools" style="width:80px; right:400px; top:5px; z-index:5; margin-top:1px;">
                        <select id="pTab1SelectHeltes" name="pTab1SelectHeltes" runat="server" class="form-control" style="padding: 1px;" disabled="disabled">
							<option value="">- Бүгд -</option>
						</select>
                    </div>
                    <div class="Colvis TableTools" style="right:490px; top:4px; z-index:5; margin-top:7px;"><label>Хэлтэс:</label></div>
                    <div class="Colvis TableTools" style="width:80px; right:545px; top:5px; z-index:5; margin-top:1px;">
                        <select id="pTab1SelectGazar" name="pTab1SelectGazar" runat="server" class="form-control" style="padding: 1px;">
							<option value="">- Бүгд -</option>
						</select>
                    </div>
                    <div class="Colvis TableTools" style="right:635px; top:4px; z-index:5; margin-top:7px;"><label>Газар:</label></div>
                    <div class="Colvis TableTools" style="width:150px; right:685px; top:5px; z-index:5; margin-top:1px;">
                        <select id="pTab1SelectPos" name="pTab1SelectPos" runat="server" class="form-control" style="padding: 1px;">
							<option value="">- Бүгд -</option>
						</select>
                    </div>
                    <div class="Colvis TableTools" style="right:845px; top:4px; z-index:5; margin-top:7px;"><label>Албан тушаал:</label></div>
                    <div id="loaderTab1" class="search-background">
                        <img width="64" height="" src="img/loading.gif"/>
                        <h2 style="width:100%; display:block; overflow:hidden; padding:20px 0 0 0; color: #fff; padding-top:0px; margin-top:0px;">Уншиж байна !</h2>
                    </div>
                    <div id="divBindTab1Table" class="widget-body no-padding">
                    </div>
                </div>
            </div>
        </article>
    </div>
</section>
<div id="pTab1Modal" class="modal fade" tabindex="-1" role="dialog" aria-labelledby="remoteModalLabel" aria-hidden="true">
	<div class="modal-dialog modal-lg">
        <div class="modal-content">
            <form id="pTab1ModalForm">
                <div class="modal-header">
			        <button type="button" class="close" data-dismiss="modal" aria-hidden="true">×</button>
			        <h4 class="modal-title">Ажилтаны бүртгэл&nbsp;<span id="pTab1ModalHeaderLabel"></span></h4>
		        </div>
		        <div class="modal-body">
                    <div id="pTab1ID" class="hide"></div>
                    <fieldset>
                        <legend>Албан тушаал</legend>
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
                                    <input id="pTab1ModalInputDomainname" name="pTab1ModalInputDomainname" type="text" class="form-control" placeholder="Домайн нэр" />
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
                                    <input id="pTab1ModalInputSigndate" name="pTab1ModalInputSigndate" type="text" class="form-control" placeholder="Томилогдсон огноо" />
                                </div>
                                <div class="col-md-2">
                                    <label class="control-label">Тушаалын огноо</label>
                                    <input id="pTab1ModalInputTushaaldate" name="pTab1ModalInputTushaaldate" type="text" class="form-control" placeholder="Тушаалын огноо" />
                                </div>
                                <div class="col-md-2">
                                    <label class="control-label">Тушаалын дугаар</label>
                                    <input id="pTab1ModalInputTushaalno" name="pTab1ModalInputTushaalno" type="text" class="form-control" placeholder="Тушаалын дугаар" />
                                </div>
                                <div class="col-md-6">
                                    <label class="control-label">Нэмэлт тайлбар</label>
                                    <input id="pTab1ModalInputDescription" name="pTab1ModalInputDescription" type="text" class="form-control" placeholder="Нэмэлт тайлбар" />
                                </div>
                            </div>
                        </div>
                    </fieldset>
                    <div class="row">
                        <div class="col-md-4">
                            <fieldset>
                                <legend>Хувийн мэдээлэл</legend>
                                <div class="form-group">
                                    <label class="control-label">Ургийн овог</label>
                                    <input id="pTab1ModalInputMName" name="pTab1ModalInputMName" type="text" class="form-control" placeholder="Ургийн овог" />
                                </div>
                                <div class="form-group">
                                    <label class="control-label">*Эцэг(эх)-н нэр</label>
                                    <input id="pTab1ModalInputLName" name="pTab1ModalInputLName" type="text" class="form-control" placeholder="Эцэг(эх)-н нэр" />
                                </div>
                                <div class="form-group">
                                    <label class="control-label">*Өөрийн нэр</label>
                                    <input id="pTab1ModalInputFName" name="pTab1ModalInputFName" type="text" class="form-control" placeholder="Өөрийн нэр" />
                                </div>
                                <div class="form-group">
                                    <label class="control-label">*Хүйс</label>
                                    <div class="radio no-margin">
                                        <label class="radio radio-inline no-margin">
										    <input id="pTab1ModalSelectGenderMale" type="radio" class="radiobox" name="pTab1ModalSelectGender" value="1">
										    <span>Эрэгтэй</span>
									    </label>
									    <label class="radio radio-inline">
										    <input id="pTab1ModalSelectGenderFemale" type="radio" class="radiobox" name="pTab1ModalSelectGender" value="0">
										    <span>Эмэгтэй</span>  
									    </label>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label class="control-label">Төрсөн огноо</label>
									<input id="pTab1ModalInputBdate" name="pTab1ModalInputBdate" type="text" class="form-control" placeholder="Төрсөн огноо" />
                                </div>
                                <div class="form-group">
                                    <label class="control-label">Боловсрол</label>
									<select id="pTab1ModalSelectEdutp" name="pTab1ModalSelectEdutp" runat="server" class="form-control" style="padding: 5px;">
							            <option value="">- Сонго -</option>
						            </select>
                                </div>
                                <div class="form-group">
                                    <label class="control-label">Мэргэжлийн ангилал</label>
									<select id="pTab1ModalSelectOcctp" name="pTab1ModalSelectOcctp" runat="server" class="form-control" style="padding: 5px;">
							            <option value="">- Сонго -</option>
						            </select>
                                </div>
                                <div class="form-group">
                                    <label class="control-label">Мэргэжлийн нэр</label>
									<input id="pTab1ModalInputOccname" name="pTab1ModalInputOccname" type="text" class="form-control" placeholder="Мэргэжлийн нэр" />
                                </div>
                            </fieldset>
                            <fieldset>
                                <legend>Төрсөн газар, Үндэс угсаа</legend>
                                <div class="form-group">
                                    <label class="control-label">Төрсөн аймаг, хот</label>
									<select id="pTab1ModalSelectBcity" name="pTab1ModalSelectBcity" runat="server" class="form-control" style="padding: 5px;">
							            <option value="">- Сонго -</option>
						            </select>
                                </div>
                                <div class="form-group">
                                    <label class="control-label">Төрсөн сум, дүүрэг</label>
									<select id="pTab1ModalSelectBdist" name="pTab1ModalSelectBdist" runat="server" class="form-control" style="padding: 5px;" disabled="disabled">
							            <option value="">- Сонго -</option>
						            </select>
                                </div>
                                <div class="form-group">
                                    <label class="control-label">Үндэс угсаа</label>
									<select id="pTab1ModalSelectNat" name="pTab1ModalSelectNat" runat="server" class="form-control" style="padding: 5px;">
							            <option value="">- Сонго -</option>
						            </select>
                                </div>
                                <div class="form-group">
                                    <label class="control-label">Нийгмийн гарал</label>
									<select id="pTab1ModalSelectSocpos" name="pTab1ModalSelectSocpos" runat="server" class="form-control" style="padding: 5px;">
							            <option value="">- Сонго -</option>
						            </select>
                                </div>
                            </fieldset>
                        </div>
                        <div class="col-md-4">
                            <fieldset>
                                <legend>Зураг</legend>
                                <div class="form-group friends-list">
                                    <img id="pTab1ModalImgStaffImage" src="../img/avatars/male.png" alt="friend-1" style="height:100px; width:auto;">
                                </div>
                                <div class="form-group">
                                    <input id="pTab1ModalInputImageUpload" type="file" class="btn btn-default">
                                </div>
                                <div class="form-group">
                                    <label class="control-label">Хурууний хээний код</label>
                                    <label class="control-label pull-right"><a id="pTab1ModalBtnNewFingerid" href="javascript:void(0);" class="btn btn-link btn-xs font-sm"><i>Шинэ код авах</i></a></label>
                                    <input id="pTab1ModalInputFingerid" name="pTab1ModalInputFingerid" type="text" class="form-control" placeholder="Хурууний хээний код" />
                                </div>
                            </fieldset>
                            <fieldset>
                                <legend>Оршин суугаа хаяг</legend>
                                <div class="form-group">
                                    <label class="control-label">Аймаг, хот</label>
                                    <select id="pTab1ModalSelectAddresscity" name="pTab1ModalSelectAddresscity" runat="server" class="form-control" style="padding: 5px;">
							            <option value="">- Сонго -</option>
						            </select>
                                </div>
                                <div class="form-group">
                                    <label class="control-label">Сум, дүүрэг</label>
                                    <select id="pTab1ModalSelectAddressdist" name="pTab1ModalSelectAddressdist" runat="server" class="form-control" style="padding: 5px;" disabled="disabled">
							            <option value="">- Сонго -</option>
						            </select>
                                </div>
                                <div class="form-group">
                                    <label class="control-label">Гэрийн хаяг</label>
                                    <textarea id="pTab1ModalInputAddressname" name="pTab1ModalInputAddressname" class="form-control" placeholder="Гэрийн хаяг" cols="2"></textarea>
                                </div>
                                <div class="form-group">
                                    <label class="control-label">Утас</label>
                                    <input id="pTab1ModalInputTel" name="pTab1ModalInputTel" type="text" class="form-control" placeholder="Гар утас" />
                                </div>
                                <div class="form-group">
                                    <label class="control-label">Утас 2</label>
                                    <input id="pTab1ModalInputTel2" name="pTab1ModalInputTel2" type="text" class="form-control" placeholder="Гар утас 2" />
                                </div>
                                <div class="form-group">
                                    <label class="control-label">Имэйл</label>
                                    <input id="pTab1ModalInputEmail" name="pTab1ModalInputEmail" type="email" class="form-control" placeholder="Имэйл" />
                                </div>
                            </fieldset>
                        </div>
                        <div class="col-md-4">
                            <fieldset>
                                <legend>Бичиг баримтууд</legend>
                                <div class="form-group">
                                    <label class="control-label">*Регистрийн дугаар</label>
                                    <input id="pTab1ModalInputRegno" name="pTab1ModalInputRegno" type="text" class="form-control" placeholder="Регистрийн дугаар" />
                                </div>
                                <div class="form-group">
                                    <label class="control-label">ИҮ-н дугаар</label>
                                    <input id="pTab1ModalInputCitno" name="pTab1ModalInputCitno" type="text" class="form-control" placeholder="ИҮ-н дугаар" />
                                </div>
                                <div class="form-group">
                                    <label class="control-label">НДД-н дугаар</label>
                                    <input id="pTab1ModalInputSocno" name="pTab1ModalInputSocno" type="text" class="form-control" placeholder="НДД-н дугаар" />
                                </div>
                                <div class="form-group">
                                    <label class="control-label">ЭМДД-н дугаар</label>
                                    <input id="pTab1ModalInputHealno" name="pTab1ModalInputHealno" type="text" class="form-control" placeholder="ЭМДД-н дугаар" />
                                </div>
                            </fieldset>
                            <fieldset>
                                <legend>Холбоо барих ойр дотны хүн</legend>
                                <div class="form-group">
                                    <label class="control-label">Нэр</label>
                                    <input id="pTab1ModalInputRelName" name="pTab1ModalInputRelName" type="text" class="form-control" placeholder="Нэр" />
                                </div>
                                <div class="form-group">
                                    <label class="control-label">Таны юу болох</label>
                                    <select id="pTab1ModalSelectRelRelation" name="pTab1ModalSelectRelRelation" runat="server" class="form-control" style="padding: 5px;">
							            <option value="">- Сонго -</option>
						            </select>
                                </div>
                                <div class="form-group">
                                    <label class="control-label">Гэрийн хаяг</label>
                                    <textarea id="pTab1ModalInputRelAddress" name="pTab1ModalInputRelAddress" class="form-control" placeholder="Гэрийн хаяг" cols="2"></textarea>
                                </div>
                                <div class="form-group">
                                    <label class="control-label">Утас</label>
                                    <input id="pTab1ModalInputRelTel" name="pTab1ModalInputRelTel" type="text" class="form-control" placeholder="Гар утас" />
                                </div>
                                <div class="form-group">
                                    <label class="control-label">Утас 2</label>
                                    <input id="pTab1ModalInputRelTel2" name="pTab1ModalInputRelTel2" type="text" class="form-control" placeholder="Гар утас 2" />
                                </div>
                                <div class="form-group">
                                    <label class="control-label">Имэйл</label>
                                    <input id="pTab1ModalInputRelEmail" name="pTab1ModalInputRelEmail" type="email" class="form-control" placeholder="Имэйл" />
                                </div>
                            </fieldset>
                        </div>
                    </div>
		        </div>
		        <div class="modal-footer">
			        <button type="button" class="btn btn-default" data-dismiss="modal">Болих</button>
			        <button type="submit" class="btn btn-primary"><span class="glyphicon glyphicon-floppy-disk"></span> Хадгалах</button>
		        </div>
            </form>
        </div>
	</div>
</div>
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
<script type="text/javascript">
    pageSetUp();
    var globalAjaxVar = null;
    var pagefunction = function () {
        var userGazarId = $('#indexUserGazarId').text();
        var userHeltesId = $('#indexUserHeltesId').text();
        var userPosId = $('#indexUserPosId').text();
        globalAjaxVar = $.ajax({
            type: "POST",
            url: "ws.aspx/gazarListForDDL",
            data: '{}',
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (msg) {
                $("#pTab1SelectGazar").html(msg.d);
                globalAjaxVar = $.ajax({
                    type: "POST",
                    url: "ws.aspx/checkRoles",
                    data: '{stid:"' + $('#indexUserId').text() + '", roles:"1,7"}',
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (msg) {
                        if (msg.d == '0') {
                            $('#pTab1SelectGazar').val(userGazarId);
                            $('#pTab1SelectGazar').prop('disabled', true);
                            globalAjaxVar = $.ajax({
                                type: "POST",
                                url: "ws.aspx/heltesListForDDL",
                                data: '{gazarId:"' + $("#pTab1SelectGazar option:selected").val() + '"}',
                                contentType: "application/json; charset=utf-8",
                                dataType: "json",
                                success: function (msg) {
                                    $("#pTab1SelectHeltes").html(msg.d);
                                    if (userPosId != '2010201') {
                                        $('#pTab1SelectHeltes').val(userHeltesId);
                                        $('#pTab1SelectHeltes').prop('disabled', true);
                                        dataBindTab1Datatable();
                                    }
                                    else dataBindTab1Datatable();
                                },
                                failure: function (response) {
                                    alert('FAILURE: ' + response.d);
                                },
                                error: function (xhr, status, error) {
                                    var err = eval("(" + xhr.responseText + ")");
                                    if (err.Message == 'SessionDied') window.location = '../login';
                                    else window.location = '../#pg/error500.aspx';
                                }
                            });
                        }
                        else dataBindTab1Datatable();
                    },
                    failure: function (response) {
                        alert('FAILURE: ' + response.d);
                    },
                    error: function (xhr, status, error) {
                        var err = eval("(" + xhr.responseText + ")");
                        if (err.Message == 'SessionDied') window.location = '../login';
                        else window.location = '../#pg/error500.aspx';
                    }
                });
            },
            failure: function (response) {
                alert('FAILURE: ' + response.d);
            },
            error: function (xhr, status, error) {
                var err = eval("(" + xhr.responseText + ")");
                if (err.Message == 'SessionDied') window.location = '../login';
                else window.location = '../#pg/error500.aspx';
            }
        });
        globalAjaxVar = $.ajax({
            type: "POST",
            url: "ws.aspx/staffreg_staffregTab1ModalBranchForDDL",
            data: '{}',
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (msg) {
                $("#pTab1ModalSelectBranch").html(msg.d);
            },
            failure: function (response) {
                alert('FAILURE: ' + response.d);
            },
            error: function (xhr, status, error) {
                var err = eval("(" + xhr.responseText + ")");
                if (err.Message == 'SessionDied') window.location = '../login';
                else window.location = '../#pg/error500.aspx';
            }
        });
        globalAjaxVar = $.ajax({
            type: "POST",
            url: "ws.aspx/staffreg_staffregTab1ModalRankForDDL",
            data: '{}',
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (msg) {
                $("#pTab1ModalSelectRank").html(msg.d);
            },
            failure: function (response) {
                alert('FAILURE: ' + response.d);
            },
            error: function (xhr, status, error) {
                var err = eval("(" + xhr.responseText + ")");
                if (err.Message == 'SessionDied') window.location = '../login';
                else window.location = '../#pg/error500.aspx';
            }
        });
    };
    var pagedestroy = function () {
        if (globalAjaxVar != null) {
            globalAjaxVar.abort();
            globalAjaxVar = null;
        }
    }
    pagefunction();

    //таб1
    $("#pTab1SelectPos, #pTab1SelectHeltes, #pTab1SelectType").change(function () {
        dataBindTab1Datatable();
    });
    $("#pTab1SelectGazar").change(function () {
        if ($("#pTab1SelectGazar option:selected").val() == "") {
            $("#pTab1SelectHeltes").html("<option selected value=\"\">- Бүгд -</option>");
            $("#pTab1SelectHeltes").prop("disabled", true);
            dataBindTab1Datatable();
        }
        else {
            globalAjaxVar = $.ajax({
                type: "POST",
                url: "ws.aspx/heltesListForDDL",
                data: '{gazarId:"' + $("#pTab1SelectGazar option:selected").val() + '"}',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (msg) {
                    $("#pTab1SelectHeltes").html(msg.d);
                    $("#pTab1SelectHeltes").prop("disabled", false);
                    dataBindTab1Datatable();
                },
                failure: function (response) {
                    alert('FAILURE: ' + response.d);
                },
                error: function (xhr, status, error) {
                    var err = eval("(" + xhr.responseText + ")");
                    if (err.Message == 'SessionDied') window.location = '../login';
                    else window.location = '../#pg/error500.aspx';
                }
            });
        }
    });
    function dataBindTab1Datatable() {
        var valIsMove = '0';
        if ($.trim($('#pTab1H2Title').html()) == "Бүрэлдэхүүн хөдөлгөөн") valIsMove = '1';
        showLoader('loaderTab1');
        globalAjaxVar = $.ajax({
            type: "POST",
            url: "ws.aspx/staffreg_staffregTab1Datatable",
            data: '{pos:"' + $("#pTab1SelectPos option:selected").val() + '", gazar:"' + $("#pTab1SelectGazar option:selected").val() + '", heltes:"' + $("#pTab1SelectHeltes option:selected").val() + '", tp:"' + $("#pTab1SelectType option:selected").val() + '", currstaffid:"' + $.trim($('#indexUserId').text()) + '", ismove:"' + valIsMove + '"}',
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (msg) {
                $("#divBindTab1Table").html(msg.d);
                hideLoader('loaderTab1');
            },
            failure: function (response) {
                alert('FAILURE: ' + response.d);
            },
            error: function (xhr, status, error) {
                var err = eval("(" + xhr.responseText + ")");
                if (err.Message == 'SessionDied') window.location = '../login';
                else window.location = '../#pg/error500.aspx';
            }
        });
    }
    $("#pTab1ModalSelectBcity").change(function () {
        if ($("#pTab1ModalSelectBcity option:selected").val() == "") {
            $("#pTab1ModalSelectBdist").html("<option selected value=\"\">- Сонго -</option>");
            $("#pTab1ModalSelectBdist").prop("disabled", true);
        }
        else {
            globalAjaxVar = $.ajax({
                type: "POST",
                url: "ws.aspx/staffreg_staffregTab1ModalDistForDDL",
                data: '{city:"' + $("#pTab1ModalSelectBcity option:selected").val() + '"}',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (msg) {
                    $("#pTab1ModalSelectBdist").html(msg.d);
                    $("#pTab1ModalSelectBdist").prop("disabled", false);
                },
                failure: function (response) {
                    alert('FAILURE: ' + response.d);
                },
                error: function (xhr, status, error) {
                    var err = eval("(" + xhr.responseText + ")");
                    if (err.Message == 'SessionDied') window.location = '../login';
                    else window.location = '../#pg/error500.aspx';
                }
            });
        }
    });
    $("#pTab1ModalSelectAddresscity").change(function () {
        if ($("#pTab1ModalSelectAddresscity option:selected").val() == "") {
            $("#pTab1ModalSelectAddressdist").html("<option selected value=\"\">- Сонго -</option>");
            $("#pTab1ModalSelectAddressdist").prop("disabled", true);
        }
        else {
            globalAjaxVar = $.ajax({
                type: "POST",
                url: "ws.aspx/staffreg_staffregTab1ModalDistForDDL",
                data: '{city:"' + $("#pTab1ModalSelectAddresscity option:selected").val() + '"}',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (msg) {
                    $("#pTab1ModalSelectAddressdist").html(msg.d);
                    $("#pTab1ModalSelectAddressdist").prop("disabled", false);
                },
                failure: function (response) {
                    alert('FAILURE: ' + response.d);
                },
                error: function (xhr, status, error) {
                    var err = eval("(" + xhr.responseText + ")");
                    if (err.Message == 'SessionDied') window.location = '../login';
                    else window.location = '../#pg/error500.aspx';
                }
            });
        }
    });
    function showAddEditTab1(el, isinsupt) {
        $('#pTab1ModalHeaderLabel').text(isinsupt);
        if (isinsupt == 'нэмэх') {
            $('#pTab1ID').text('');
            $('#pTab1ModalSelectBranch').val('');
            $('#pTab1ModalInputDomainname').val('');
            globalAjaxVar = $.ajax({
                type: "POST",
                url: "ws.aspx/staffreg_staffregTab1ModalMoveForDDL",
                data: '{tp:"1"}',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (msg) {
                    $("#pTab1ModalSelectMove").html(msg.d);
                    $('#pTab1ModalSelectMove').val('');
                },
                failure: function (response) {
                    alert('FAILURE: ' + response.d);
                },
                error: function (xhr, status, error) {
                    var err = eval("(" + xhr.responseText + ")");
                    if (err.Message == 'SessionDied') window.location = '../login';
                    else window.location = '../#pg/error500.aspx';
                }
            });
            $('#pTab1ModalSelectPostype').val('');
            $('#pTab1ModalSelectPos').val('');
            $('#pTab1ModalSelectRank').val('');
            $('#pTab1ModalInputSigndate').val('');
            $('#pTab1ModalInputMName').val('');
            $('#pTab1ModalInputLName').val('');
            $('#pTab1ModalInputFName').val('');
            $('#pTab1ModalInputBdate').val('');
            $('#pTab1ModalSelectBcity').val('');
            $("#pTab1ModalSelectBdist").html("<option selected value=\"\">- Сонго -</option>");
            $("#pTab1ModalSelectBdist").prop("disabled", true);
            $('#pTab1ModalSelectNat').val('');
            $('#pTab1ModalSelectEdutp').val('');
            $('#pTab1ModalSelectSocpos').val('');
            $('#pTab1ModalSelectOcctp').val('');
            $('#pTab1ModalInputOccname').val('');
            $('input[name = "pTab1ModalSelectGender"]').prop('checked', false);
            $('#pTab1ModalImgStaffImage').attr('src','../img/avatars/male.png');
            $('#pTab1ModalInputImageUpload').val('');
            $('#pTab1ModalInputFingerid').val('');
            $('#pTab1ModalInputRegno').val('');
            $('#pTab1ModalInputCitno').val('');
            $('#pTab1ModalInputSocno').val('');
            $('#pTab1ModalInputHealno').val('');
            $('#pTab1ModalSelectAddresscity').val('');
            $("#pTab1ModalSelectAddressdist").html("<option selected value=\"\">- Сонго -</option>");
            $("#pTab1ModalSelectAddressdist").prop("disabled", true);
            $('#pTab1ModalInputAddressname').val('');
            $('#pTab1ModalInputTel').val('');
            $('#pTab1ModalInputTel2').val('');
            $('#pTab1ModalInputEmail').val('');
            $('#MacID').val('');
            $('#pTab1ModalInputRelName').val('');
            $('#pTab1ModalSelectRelRelation').val('');
            $('#pTab1ModalInputRelAddress').val('');
            $('#pTab1ModalInputRelTel').val('');
            $('#pTab1ModalInputRelTel2').val('');
            $('#pTab1ModalInputRelEmail').val('');
        }
        else {
            $('#pTab1ID').text($(el).closest('tr').find('td:eq(0)').text());
            $('#pTab1ModalInputDomainname').val($(el).closest('tr').find('td:eq(3)').text());
            $('#pTab1ModalInputLName').val($(el).closest('tr').find('td:eq(5)').text());
            $('#pTab1ModalInputFName').val($(el).closest('tr').find('td:eq(6)').text());
            $('input[name = "pTab1ModalSelectGender"]').prop('checked', false);
            if ($(el).closest('tr').find('td:eq(8)').text() == 'Эр') $('#pTab1ModalSelectGenderMale').prop('checked', true);
            else $('#pTab1ModalSelectGenderFemale').prop('checked', true);
            $('#pTab1ModalImgStaffImage').attr('src', $(el).closest('tr').find('td:eq(1)').find('img').attr('src'));
            $('#pTab1ModalInputImageUpload').val('');
            $('#pTab1ModalInputFingerid').val($(el).closest('tr').find('td:eq(11)').text());
            $('#pTab1ModalInputRegno').val($(el).closest('tr').find('td:eq(2)').text());

            globalAjaxVar = $.ajax({
                type: "POST",
                url: "ws.aspx/staffreg_staffregTab1ModalMoveForDDL",
                data: '{tp:""}',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (msg) {
                    $("#pTab1ModalSelectMove").html(msg.d);
                    globalAjaxVar = $.ajax({
                        type: "POST",
                        url: "ws.aspx/WSOracleExecuteScalar",
                        data: '{qry:"SELECT b.BRANCH_ID||\'~\'||b.MOVE_ID||\'~\'||b.POSTYPE_ID||\'~\'||b.POS_ID||\'~\'||b.RANK_ID||\'~\'||b.DT||\'~\'||b.TUSHAALDATE||\'~\'||b.TUSHAALNO||\'~\'||b.DESCRIPTION||\'~\'||a.MNAME||\'~\'||a.BDATE||\'~\'||a.BCITY_ID||\'~\'||a.BDIST_ID||\'~\'||a.NAT_ID||\'~\'||a.EDUTP_ID||\'~\'||a.SOCPOS_ID||\'~\'||a.OCCTYPE_ID||\'~\'||a.OCCNAME||\'~\'||a.CITNO||\'~\'||a.SOCNO||\'~\'||a.HEALNO||\'~\'||a.ADDRCITY_ID||\'~\'||a.ADDRDIST_ID||\'~\'||a.ADDRESSNAME||\'~\'||a.TEL||\'~\'||a.TEL2||\'~\'||a.EMAIL||\'~\'||a.RELNAME||\'~\'||a.RELATION_ID||\'~\'||a.RELADDRESSNAME||\'~\'||RELTEL||\'~\'||a.RELTEL2||\'~\'||a.RELEMAIL FROM ST_STAFFS a INNER JOIN ST_STBR b ON a.ID=b.STAFFS_ID AND b.ISACTIVE=1 WHERE a.ID=' + $(el).closest('tr').find('td:eq(0)').text() + '"}',
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        success: function (msg) {
                            $('#pTab1ModalSelectBranch').val(msg.d.split('~')[0]);
                            $('#pTab1ModalSelectMove').val(msg.d.split('~')[1]);
                            $('#pTab1ModalSelectPostype').val(msg.d.split('~')[2]);
                            $('#pTab1ModalSelectPos').val(msg.d.split('~')[3]);
                            $('#pTab1ModalSelectRank').val(msg.d.split('~')[4]);
                            $('#pTab1ModalInputSigndate').val(msg.d.split('~')[5]);
                            $('#pTab1ModalInputTushaaldate').val(msg.d.split('~')[6]);
                            $('#pTab1ModalInputTushaalno').val(msg.d.split('~')[7]);
                            $('#pTab1ModalInputDescription').val(msg.d.split('~')[8]);
                            $('#pTab1ModalInputMName').val(msg.d.split('~')[9]);
                            $('#pTab1ModalInputBdate').val(msg.d.split('~')[10]);
                            $('#pTab1ModalSelectBcity').val(msg.d.split('~')[11]);
                            var valBdist = msg.d.split('~')[12];
                            if ($("#pTab1ModalSelectBcity option:selected").val() == "") {
                                $("#pTab1ModalSelectBdist").html("<option selected value=\"\">- Сонго -</option>");
                                $("#pTab1ModalSelectBdist").prop("disabled", true);
                            }
                            else {
                                globalAjaxVar = $.ajax({
                                    type: "POST",
                                    url: "ws.aspx/staffreg_staffregTab1ModalDistForDDL",
                                    data: '{city:"' + $("#pTab1ModalSelectBcity option:selected").val() + '"}',
                                    contentType: "application/json; charset=utf-8",
                                    dataType: "json",
                                    success: function (msg) {
                                        $("#pTab1ModalSelectBdist").html(msg.d);
                                        $("#pTab1ModalSelectBdist").prop("disabled", false);
                                        $('#pTab1ModalSelectBdist').val(valBdist);
                                    },
                                    failure: function (response) {
                                        alert('FAILURE: ' + response.d);
                                    },
                                    error: function (xhr, status, error) {
                                        var err = eval("(" + xhr.responseText + ")");
                                        if (err.Message == 'SessionDied') window.location = '../login';
                                        else window.location = '../#pg/error500.aspx';
                                    }
                                });
                            }
                            $('#pTab1ModalSelectNat').val(msg.d.split('~')[13]);
                            $('#pTab1ModalSelectEdutp').val(msg.d.split('~')[14]);
                            $('#pTab1ModalSelectSocpos').val(msg.d.split('~')[15]);
                            $('#pTab1ModalSelectOcctp').val(msg.d.split('~')[16]);
                            $('#pTab1ModalInputOccname').val(msg.d.split('~')[17]);
                            $('#pTab1ModalInputCitno').val(msg.d.split('~')[18]);
                            $('#pTab1ModalInputSocno').val(msg.d.split('~')[19]);
                            $('#pTab1ModalInputHealno').val(msg.d.split('~')[20]);
                            $('#pTab1ModalSelectAddresscity').val(msg.d.split('~')[21]);
                            var valAddressdist = msg.d.split('~')[22];
                            if ($("#pTab1ModalSelectAddresscity option:selected").val() == "") {
                                $("#pTab1ModalSelectAddressdist").html("<option selected value=\"\">- Сонго -</option>");
                                $("#pTab1ModalSelectAddressdist").prop("disabled", true);
                            }
                            else {
                                globalAjaxVar = $.ajax({
                                    type: "POST",
                                    url: "ws.aspx/staffreg_staffregTab1ModalDistForDDL",
                                    data: '{city:"' + $("#pTab1ModalSelectAddresscity option:selected").val() + '"}',
                                    contentType: "application/json; charset=utf-8",
                                    dataType: "json",
                                    success: function (msg) {
                                        $("#pTab1ModalSelectAddressdist").html(msg.d);
                                        $("#pTab1ModalSelectAddressdist").prop("disabled", false);
                                        $('#pTab1ModalSelectAddressdist').val(valAddressdist);
                                    },
                                    failure: function (response) {
                                        alert('FAILURE: ' + response.d);
                                    },
                                    error: function (xhr, status, error) {
                                        var err = eval("(" + xhr.responseText + ")");
                                        if (err.Message == 'SessionDied') window.location = '../login';
                                        else window.location = '../#pg/error500.aspx';
                                    }
                                });
                            }
                            $('#pTab1ModalInputAddressname').val(msg.d.split('~')[23]);
                            $('#pTab1ModalInputTel').val(msg.d.split('~')[24]);
                            $('#pTab1ModalInputTel2').val(msg.d.split('~')[25]);
                            $('#pTab1ModalInputEmail').val(msg.d.split('~')[26]);
                            $('#pTab1ModalInputRelName').val(msg.d.split('~')[27]);
                            $('#pTab1ModalSelectRelRelation').val(msg.d.split('~')[28]);
                            $('#pTab1ModalInputRelAddress').val(msg.d.split('~')[29]);
                            $('#pTab1ModalInputRelTel').val(msg.d.split('~')[30]);
                            $('#pTab1ModalInputRelTel2').val(msg.d.split('~')[31]);
                            $('#pTab1ModalInputRelEmail').val(msg.d.split('~')[32]);
                        },
                        failure: function (response) {
                            alert('FAILURE: ' + response.d);
                        },
                        error: function (xhr, status, error) {
                            var err = eval("(" + xhr.responseText + ")");
                            if (err.Message == 'SessionDied' || $.trim(err.Message) == 'Invalid operation. The connection is closed.') window.location = '../login';
                            else window.location = '../#pg/error500.aspx';
                        }
                    });
                },
                failure: function (response) {
                    alert('FAILURE: ' + response.d);
                },
                error: function (xhr, status, error) {
                    var err = eval("(" + xhr.responseText + ")");
                    if (err.Message == 'SessionDied') window.location = '../login';
                    else window.location = '../#pg/error500.aspx';
                }
            });
        }
    }
    $("#pTab1ModalInputImageUpload").change(function () {
        var errMsg = '';
        var uploadfiles = $(this).get(0);
        var uploadedfiles = uploadfiles.files;
        var fromdata = new FormData();
        if (uploadedfiles[0].size > 2097152) {
            errMsg += 'Файлын хэмжээ 2MB -аас ихгүй байна!\n';
            $(this).val('');
        }
        if (errMsg == '') {
            valE = uploadedfiles[0].name.substr((uploadedfiles[0].name.lastIndexOf('.') + 1));
            if (valE != 'jpeg' && valE != 'jpg' && valE != 'png') {
                errMsg += 'Файлын төрөл зөвшөөрөгдөөгүй төрөл байна. /jpeg, jpg, png/\n';
                $(this).val('');
            }
            else fromdata.append(uploadedfiles[0].name, uploadedfiles[0]);
        }
        if (errMsg == '') {
            showImage(this);
        }
        else alert(errMsg);
    });
    function showImage(input) {
        if (input.files && input.files[0]) {
            var reader = new FileReader();
            reader.onload = function (e) {
                $('#pTab1ModalImgStaffImage').attr('src', e.target.result);
            }
            reader.readAsDataURL(input.files[0]);
        }
    }
    $('#pTab1ModalBtnNewFingerid').click(function () {
        globalAjaxVar = $.ajax({
            type: "POST",
            url: "ws.aspx/WSOracleExecuteScalar",
            data: '{qry:"SELECT MAX(TO_NUMBER(NVL(FINGERID,0)))+1 FROM ST_STAFFS"}',
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (msg) {
                $('#pTab1ModalInputFingerid').val(msg.d);
            },
            failure: function (response) {
                alert('FAILURE: ' + response.d);
            },
            error: function (xhr, status, error) {
                var err = eval("(" + xhr.responseText + ")");
                if (err.Message == 'SessionDied') window.location = '../login';
                else window.location = '../#pg/error500.aspx';
            }
        }); 
    });
    $('#pTab1ModalForm').bootstrapValidator({
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
            },
            pTab1ModalInputMName: {
                group: '.form-group',
                validators: {
                    stringLength: {
                        max: 50,
                        message: 'Уртдаа 50 тэмдэгт авна'
                    }
                }
            },
            pTab1ModalInputLName: {
                group: '.form-group',
                validators: {
                    notEmpty: {
                        message: 'Оруулна уу'
                    },
                    stringLength: {
                        max: 50,
                        message: 'Уртдаа 50 тэмдэгт авна'
                    }
                }
            },
            pTab1ModalInputFName: {
                group: '.form-group',
                validators: {
                    notEmpty: {
                        message: 'Оруулна уу'
                    },
                    stringLength: {
                        max: 50,
                        message: 'Уртдаа 50 тэмдэгт авна'
                    }
                }
            },
            pTab1ModalSelectGender: {
                group: '.form-group',
                validators: {
                    notEmpty: {
                        message: 'Сонгоно уу'
                    }
                }
            },
            pTab1ModalInputDate: {
                group: '.form-group',
                validators: {
                    date: {
                        format: 'YYYY-MM-DD',
                        message: 'Огноо буруу орсон байна. /Жил-Сар-Өдөр/'
                    }
                }
            },
            pTab1ModalInputOccname: {
                group: '.form-group',
                validators: {
                    stringLength: {
                        max: 75,
                        message: 'Уртдаа 75 тэмдэгт авна'
                    }
                }
            },
            pTab1ModalInputRegno: {
                group: '.form-group',
                validators: {
                    notEmpty: {
                        message: 'Оруулна уу'
                    },
                    stringLength: {
                        min: 10,
                        max: 10,
                        message: 'Зөвхөн 10 тэмдэгт авна'
                    }
                }
            },
            pTab1ModalInputCitno: {
                group: '.form-group',
                validators: {
                    stringLength: {
                        max: 9,
                        message: 'Уртдаа 9 тэмдэгт авна'
                    }
                }
            },
            pTab1ModalInputSocno: {
                group: '.form-group',
                validators: {
                    stringLength: {
                        max: 10,
                        message: 'Уртдаа 10 тэмдэгт авна'
                    }
                }
            },
            pTab1ModalInputHealno: {
                group: '.form-group',
                validators: {
                    stringLength: {
                        max: 8,
                        message: 'Уртдаа 8 тэмдэгт авна'
                    }
                }
            },
            pTab1ModalInputAddressname: {
                group: '.form-group',
                validators: {
                    stringLength: {
                        max: 200,
                        message: 'Уртдаа 200 тэмдэгт авна'
                    }
                }
            },
            pTab1ModalInputTel: {
                group: '.form-group',
                validators: {
                    stringLength: {
                        max: 8,
                        message: 'Уртдаа 8 тэмдэгт авна'
                    },
                    numeric: {
                        message: 'Зөвхөн тоон тэмдэгт авна'
                    }
                }
            },
            pTab1ModalInputTel2: {
                group: '.form-group',
                validators: {
                    stringLength: {
                        max: 8,
                        message: 'Уртдаа 8 тэмдэгт авна'
                    },
                    numeric: {
                        message: 'Зөвхөн тоон тэмдэгт авна'
                    }
                }
            },
            pTab1ModalInputEmail: {
                group: '.form-group',
                validators: {
                    emailAddress: {
                        message: 'И-мэйл зөв оруулна уу'
                    }
                }
            },
            pTab1ModalInputRelName: {
                group: '.form-group',
                validators: {
                    stringLength: {
                        max: 50,
                        message: 'Уртдаа 50 тэмдэгт авна'
                    }
                }
            },
            pTab1ModalInputRelAddress: {
                group: '.form-group',
                validators: {
                    stringLength: {
                        max: 200,
                        message: 'Уртдаа 200 тэмдэгт авна'
                    }
                }
            },
            pTab1ModalInputRelTel: {
                group: '.form-group',
                validators: {
                    stringLength: {
                        max: 8,
                        message: 'Уртдаа 8 тэмдэгт авна'
                    },
                    numeric: {
                        message: 'Зөвхөн тоон тэмдэгт авна'
                    }
                }
            },
            pTab1ModalInputRelTel2: {
                group: '.form-group',
                validators: {
                    stringLength: {
                        max: 8,
                        message: 'Уртдаа 8 тэмдэгт авна'
                    },
                    numeric: {
                        message: 'Зөвхөн тоон тэмдэгт авна'
                    }
                }
            },
            pTab1ModalInputRelEmail: {
                group: '.form-group',
                validators: {
                    emailAddress: {
                        message: 'И-мэйл зөв оруулна уу'
                    }
                }
            },
            pTab1ModalInputFingerid: {
                group: '.form-group',
                validators: {
                    stringLength: {
                        max: 8,
                        message: 'Уртдаа 8 тэмдэгт авна'
                    },
                    numeric: {
                        message: 'Зөвхөн тоон тэмдэгт авна'
                    }
                }
            }
        },
        onSuccess: function (e, data) {
            e.preventDefault();
            var isUpdate = "";
            if ($("#pTab1ModalHeaderLabel").html() == "засах") isUpdate = " AND a.ID!=" + $("#pTab1ID").text();
            globalAjaxVar = $.ajax({
                type: "POST",
                url: "ws.aspx/WSOracleExecuteScalar",
                data: '{qry:"SELECT COUNT(1) FROM ST_STAFFS a WHERE a.REGNO=N\'' + $.trim($('#pTab1ModalInputRegno').val()) + '\'' + isUpdate + '"}',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (msg) {
                    if (msg.d != '0') alert($.trim($('#pTab1ModalInputRegno').val()) + ' регистерийн дугаартай хэрэглэгч бүртгэгдсэн байна!');
                    else {
                        globalAjaxVar = $.ajax({
                            type: "POST",
                            url: "ws.aspx/WSOracleExecuteScalar",
                            data: '{qry:"SELECT COUNT(1) FROM ST_STAFFS a INNER JOIN ST_STBR b ON a.ID=b.STAFFS_ID AND b.ISACTIVE=1 INNER JOIN STN_MOVE c ON b.MOVE_ID=c.ID WHERE c.ACTIVE=1 AND a.DOMAIN_USER=\'' + $.trim($('#pTab1ModalInputDomainname').val()) + '\'' + isUpdate + '"}',
                            contentType: "application/json; charset=utf-8",
                            dataType: "json",
                            success: function (msg) {
                                if (msg.d != '0') alert($.trim($('#pTab1ModalInputDomainname').val()) + ' домайн нэртэй идэвхтэй хэрэглэгч бүртгэгдсэн байна!');
                                else {
                                    var valIsMarried = '0', valIsDavy = '0', valImage = '';
                                    if ($('#pTab1ModalInputImageUpload').val() != '') valImage = $.trim($('#pTab1ModalInputRegno').val()) + "." + $('#pTab1ModalInputImageUpload').val().split('\\').pop().split('.')[$('#pTab1ModalInputImageUpload').val().split('\\').pop().split('.').length - 1];
                                    else {
                                        if ($('#pTab1ModalImgStaffImage').attr('src') == '../img/avatars/male.png') valImage = '';
                                        else valImage = $('#pTab1ModalImgStaffImage').attr('src').split('/').pop();
                                    }
                                    if ($('#pTab1ModalHeaderLabel').text() == 'нэмэх') {
                                        globalAjaxVar = $.ajax({
                                            type: "POST",
                                            url: "ws.aspx/STAFF_INSERT",
                                            data: '{P_MNAME:"' + replaceDisplayToDatabase($.trim($('#pTab1ModalInputMName').val())).toUpperCase() + '", P_LNAME:"' + replaceDisplayToDatabase($.trim($('#pTab1ModalInputLName').val())).toUpperCase() + '", P_FNAME:"' + replaceDisplayToDatabase($.trim($('#pTab1ModalInputFName').val())).toUpperCase() + '", P_BDATE:"' + $.trim($('#pTab1ModalInputBdate').val()) + '", P_BCITY_ID:"' + $('#pTab1ModalSelectBcity option:selected').val() + '", P_BDIST_ID:"' + $('#pTab1ModalSelectBdist option:selected').val() + '", P_NAT_ID:"' + $('#pTab1ModalSelectNat option:selected').val() + '", P_EDUTP_ID:"' + $('#pTab1ModalSelectEdutp option:selected').val() + '", P_SOCPOS_ID:"' + $('#pTab1ModalSelectSocpos option:selected').val() + '", P_OCCTYPE_ID:"' + $('#pTab1ModalSelectOcctp option:selected').val() + '", P_OCCNAME:"' + replaceDisplayToDatabase($.trim($('#pTab1ModalInputOccname').val())) + '", P_GENDER:"' + $('input[name = "pTab1ModalSelectGender"]:checked').val() + '", P_REGNO:"' + $.trim($('#pTab1ModalInputRegno').val()).toUpperCase() + '", P_CITNO:"' + $.trim($('#pTab1ModalInputCitno').val()).toUpperCase() + '", P_SOCNO:"' + $.trim($('#pTab1ModalInputSocno').val()).toUpperCase() + '", P_HEALNO:"' + $.trim($('#pTab1ModalInputHealno').val()).toUpperCase() + '", P_ADDRCITY_ID:"' + $('#pTab1ModalSelectAddresscity option:selected').val() + '", P_ADDRDIST_ID:"' + $('#pTab1ModalSelectAddressdist option:selected').val() + '", P_ADDRESSNAME:"' + replaceDisplayToDatabase($.trim($('#pTab1ModalInputAddressname').val())) + '", P_TEL:"' + $.trim($('#pTab1ModalInputTel').val()) + '", P_TEL2:"' + $.trim($('#pTab1ModalInputTel2').val()) + '", P_EMAIL:"' + $.trim($('#pTab1ModalInputEmail').val()) + '", P_IMAGE:"' + valImage + '", P_DT:"' + $.trim($('#pTab1ModalInputSigndate').val()) + '", P_BRANCH_ID:"' + $('#pTab1ModalSelectBranch option:selected').val() + '", P_POSTYPE_ID:"' + $('#pTab1ModalSelectPostype option:selected').val() + '", P_POS_ID:"' + $('#pTab1ModalSelectPos option:selected').val() + '", P_RANK_ID:"' + $('#pTab1ModalSelectRank option:selected').val() + '", P_TUSHAALDATE:"' + $.trim($('#pTab1ModalInputTushaaldate').val()) + '", P_TUSHAALNO:"' + replaceDisplayToDatabase($.trim($('#pTab1ModalInputTushaalno').val())) + '", P_MOVE_ID:"' + $('#pTab1ModalSelectMove option:selected').val() + '", P_DESCRIPTION:"' + replaceDisplayToDatabase($.trim($('#pTab1ModalInputDescription').val())) + '", P_STAFFID:"' + $.trim($('#indexUserId').html()) + '", P_DOMAIN_USER:"' + $.trim($('#pTab1ModalInputDomainname').val()) + '", P_RELNAME:"' + replaceDisplayToDatabase($.trim($('#pTab1ModalInputRelName').val())) + '", P_RELATION_ID:"' + $('#pTab1ModalSelectRelRelation option:selected').val() + '", P_RELADDRESSNAME:"' + replaceDisplayToDatabase($.trim($('#pTab1ModalInputRelAddress').val())) + '", P_RELTEL:"' + $.trim($('#pTab1ModalInputRelTel').val()) + '", P_RELTEL2:"' + $.trim($('#pTab1ModalInputRelTel2').val()) + '", P_RELEMAIL:"' + $.trim($('#pTab1ModalInputRelEmail').val()) + '", P_FINGERID:"' + $.trim($('#pTab1ModalInputFingerid').val()) + '"}',
                                            contentType: "application/json; charset=utf-8",
                                            dataType: "json",
                                            success: function () {
                                                if ($.trim($('#pTab1ModalInputImageUpload').val()) != '') {
                                                    var uploadfiles = $("#pTab1ModalInputImageUpload").get(0);
                                                    var uploadedfiles = uploadfiles.files;
                                                    var fromdata = new FormData();
                                                    globalAjaxVar = $.ajax({
                                                        type: "POST",
                                                        url: "pg/UploadFile.ashx?folder=staffs&filename=" + valImage,
                                                        data: fromdata,
                                                        contentType: false,
                                                        processData: false,
                                                        success: function () {
                                                            smallBox('Ажилтаны зураг', 'Амжилттай хадгалагдлаа', '#659265', 2000);
                                                        },
                                                        failure: function (response) {
                                                            alert('FAILURE: ' + response.d);
                                                        },
                                                        error: function (xhr, status, error) {
                                                            var err = eval("(" + xhr.responseText + ")");
                                                            if (err.Message == 'SessionDied') window.location = '../login';
                                                            else alert('Зураг хадгалахад алдаа гарлаа: ' + err.Message);
                                                        }
                                                    });
                                                }
                                                dataBindTab1Datatable();
                                                $('#pTab1Modal').modal('hide');
                                                smallBox('Ажилтан', 'Амжилттай хадгалагдлаа', '#659265', 4000);
                                            },
                                            failure: function (response) {
                                                alert('FAILURE: ' + response.d);
                                            },
                                            error: function (xhr, status, error) {
                                                var err = eval("(" + xhr.responseText + ")");
                                                if (err.Message == 'SessionDied') window.location = '../login';
                                                else window.location = '../#pg/error500.aspx';
                                            }
                                        });
                                    }
                                    else {
                                        globalAjaxVar = $.ajax({
                                            type: "POST",
                                            url: "ws.aspx/STAFF_UPDATE",
                                            data: '{P_ID:"' + $('#pTab1ID').text() + '", P_MNAME:"' + replaceDisplayToDatabase($.trim($('#pTab1ModalInputMName').val())).toUpperCase() + '", P_LNAME:"' + replaceDisplayToDatabase($.trim($('#pTab1ModalInputLName').val())).toUpperCase() + '", P_FNAME:"' + replaceDisplayToDatabase($.trim($('#pTab1ModalInputFName').val())).toUpperCase() + '", P_BDATE:"' + $.trim($('#pTab1ModalInputBdate').val()) + '", P_BCITY_ID:"' + $('#pTab1ModalSelectBcity option:selected').val() + '", P_BDIST_ID:"' + $('#pTab1ModalSelectBdist option:selected').val() + '", P_NAT_ID:"' + $('#pTab1ModalSelectNat option:selected').val() + '", P_EDUTP_ID:"' + $('#pTab1ModalSelectEdutp option:selected').val() + '", P_SOCPOS_ID:"' + $('#pTab1ModalSelectSocpos option:selected').val() + '", P_OCCTYPE_ID:"' + $('#pTab1ModalSelectOcctp option:selected').val() + '", P_OCCNAME:"' + replaceDisplayToDatabase($.trim($('#pTab1ModalInputOccname').val())) + '", P_GENDER:"' + $('input[name = "pTab1ModalSelectGender"]:checked').val() + '", P_REGNO:"' + $.trim($('#pTab1ModalInputRegno').val()).toUpperCase() + '", P_CITNO:"' + $.trim($('#pTab1ModalInputCitno').val()).toUpperCase() + '", P_SOCNO:"' + $.trim($('#pTab1ModalInputSocno').val()).toUpperCase() + '", P_HEALNO:"' + $.trim($('#pTab1ModalInputHealno').val()).toUpperCase() + '", P_ADDRCITY_ID:"' + $('#pTab1ModalSelectAddresscity option:selected').val() + '", P_ADDRDIST_ID:"' + $('#pTab1ModalSelectAddressdist option:selected').val() + '", P_ADDRESSNAME:"' + replaceDisplayToDatabase($.trim($('#pTab1ModalInputAddressname').val())) + '", P_TEL:"' + $.trim($('#pTab1ModalInputTel').val()) + '", P_TEL2:"' + $.trim($('#pTab1ModalInputTel2').val()) + '", P_EMAIL:"' + $.trim($('#pTab1ModalInputEmail').val()) + '", P_IMAGE:"' + valImage + '", P_DT:"' + $.trim($('#pTab1ModalInputSigndate').val()) + '", P_BRANCH_ID:"' + $('#pTab1ModalSelectBranch option:selected').val() + '", P_POSTYPE_ID:"' + $('#pTab1ModalSelectPostype option:selected').val() + '", P_POS_ID:"' + $('#pTab1ModalSelectPos option:selected').val() + '", P_RANK_ID:"' + $('#pTab1ModalSelectRank option:selected').val() + '", P_TUSHAALDATE:"' + $.trim($('#pTab1ModalInputTushaaldate').val()) + '", P_TUSHAALNO:"' + replaceDisplayToDatabase($.trim($('#pTab1ModalInputTushaalno').val())) + '", P_MOVE_ID:"' + $('#pTab1ModalSelectMove option:selected').val() + '", P_DESCRIPTION:"' + replaceDisplayToDatabase($.trim($('#pTab1ModalInputDescription').val())) + '", P_STAFFID:"' + $.trim($('#indexUserId').html()) + '", P_DOMAIN_USER:"' + $.trim($('#pTab1ModalInputDomainname').val()) + '", P_RELNAME:"' + replaceDisplayToDatabase($.trim($('#pTab1ModalInputRelName').val())) + '", P_RELATION_ID:"' + $('#pTab1ModalSelectRelRelation option:selected').val() + '", P_RELADDRESSNAME:"' + replaceDisplayToDatabase($.trim($('#pTab1ModalInputRelAddress').val())) + '", P_RELTEL:"' + $.trim($('#pTab1ModalInputRelTel').val()) + '", P_RELTEL2:"' + $.trim($('#pTab1ModalInputRelTel2').val()) + '", P_RELEMAIL:"' + $.trim($('#pTab1ModalInputRelEmail').val()) + '", P_FINGERID:"' + $.trim($('#pTab1ModalInputFingerid').val()) + '"}',
                                            contentType: "application/json; charset=utf-8",
                                            dataType: "json",
                                            success: function () {
                                                if ($.trim($('#pTab1ModalInputImageUpload').val()) != '') {
                                                    var uploadfiles = $("#pTab1ModalInputImageUpload").get(0);
                                                    var uploadedfiles = uploadfiles.files;
                                                    var fromdata = new FormData();
                                                    globalAjaxVar = $.ajax({
                                                        type: "POST",
                                                        url: "pg/UploadFile.ashx?folder=staffs&filename=" + valImage,
                                                        data: fromdata,
                                                        contentType: false,
                                                        processData: false,
                                                        success: function () {
                                                            smallBox('Ажилтаны зураг', 'Амжилттай хадгалагдлаа', '#659265', 2000);
                                                        },
                                                        failure: function (response) {
                                                            alert('FAILURE: ' + response.d);
                                                        },
                                                        error: function (xhr, status, error) {
                                                            var err = eval("(" + xhr.responseText + ")");
                                                            if (err.Message == 'SessionDied') window.location = '../login';
                                                            else alert('Зураг хадгалахад алдаа гарлаа: ' + err.Message);
                                                        }
                                                    });
                                                }
                                                dataBindTab1Datatable();
                                                $('#pTab1Modal').modal('hide');
                                                smallBox('Ажилтан', 'Амжилттай хадгалагдлаа', '#659265', 4000);
                                            },
                                            failure: function (response) {
                                                alert('FAILURE: ' + response.d);
                                            },
                                            error: function (xhr, status, error) {
                                                var err = eval("(" + xhr.responseText + ")");
                                                if (err.Message == 'SessionDied') window.location = '../login';
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
                                if (err.Message == 'SessionDied') window.location = '../login';
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
                    if (err.Message == 'SessionDied') window.location = '../login';
                    else window.location = '../#pg/error500.aspx';
                }
            });            
        }
    });

    $('#pTab1ModalInputBdate, #pTab1ModalInputSigndate, #pTab1ModalInputTushaaldate').datepicker({
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