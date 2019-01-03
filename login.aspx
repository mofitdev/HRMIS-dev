<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="login.aspx.cs" Inherits="HRWebApp.login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml" lang="en-us" id="extr-page">
<head runat="server">
    <meta charset="utf-8">
		<title> Хүний нөөцийн удирдлагын мэдээллийн систем</title>
		<meta name="description" content="">
		<meta name="author" content="">
		<meta name="viewport" content="width=device-width, initial-scale=1.0, maximum-scale=1.0, user-scalable=no">		
		<!-- #CSS Links -->
		<!-- Basic Styles -->
		<link rel="stylesheet" type="text/css" media="screen" href="css/bootstrap.min.css">
		<link rel="stylesheet" type="text/css" media="screen" href="css/font-awesome.min.css">
		<!-- SmartAdmin Styles : Caution! DO NOT change the order -->
		<link rel="stylesheet" type="text/css" media="screen" href="css/smartadmin-production-plugins.min.css">
		<link rel="stylesheet" type="text/css" media="screen" href="css/smartadmin-production.min.css">
		<link rel="stylesheet" type="text/css" media="screen" href="css/smartadmin-skins.min.css">
		<!-- SmartAdmin RTL Support -->
		<link rel="stylesheet" type="text/css" media="screen" href="css/smartadmin-rtl.min.css"> 
		<link rel="stylesheet" type="text/css" media="screen" href="css/demo.min.css">
		<!-- #FAVICONS -->
		<link rel="shortcut icon" href="img/favicon/favicon.ico" type="image/x-icon">
		<link rel="icon" href="img/favicon/favicon.ico" type="image/x-icon">

		<!-- #GOOGLE FONT -->
		<link rel="stylesheet" href="http://fonts.googleapis.com/css?family=Open+Sans:400italic,700italic,300,400,700">

		<!-- #APP SCREEN / ICONS -->
		<!-- Specifying a Webpage Icon for Web Clip 
			 Ref: https://developer.apple.com/library/ios/documentation/AppleApplications/Reference/SafariWebContent/ConfiguringWebApplications/ConfiguringWebApplications.html -->
		<link rel="apple-touch-icon" href="img/splash/sptouch-icon-iphone.png">
		<link rel="apple-touch-icon" sizes="76x76" href="img/splash/touch-icon-ipad.png">
		<link rel="apple-touch-icon" sizes="120x120" href="img/splash/touch-icon-iphone-retina.png">
		<link rel="apple-touch-icon" sizes="152x152" href="img/splash/touch-icon-ipad-retina.png">
		
		<!-- iOS web-app metas : hides Safari UI Components and Changes Status Bar Appearance -->
		<meta name="apple-mobile-web-app-capable" content="yes">
		<meta name="apple-mobile-web-app-status-bar-style" content="black">
		
		<!-- Startup image for web apps -->
		<link rel="apple-touch-startup-image" href="img/splash/ipad-landscape.png" media="screen and (min-device-width: 481px) and (max-device-width: 1024px) and (orientation:landscape)">
		<link rel="apple-touch-startup-image" href="img/splash/ipad-portrait.png" media="screen and (min-device-width: 481px) and (max-device-width: 1024px) and (orientation:portrait)">
		<link rel="apple-touch-startup-image" href="img/splash/iphone.png" media="screen and (max-device-width: 320px)">
</head>
<body class="animated fadeInDown">
    <header id="header">
		<div id="logo-group">
			<span id="logo"> <img src="img/logo.png" alt="SmartAdmin"> </span>
		</div>
		<span id="extr-page-header-space"> <span class="hidden-mobile hidden-xs">Санхүүгийн мэдээлэл технологийн хэлтэс. Утас: +51-260663, +51-266156
	</header>
	<div id="main" role="main">
		<!-- MAIN CONTENT -->
		<div id="content" class="container">
			<div class="row">
				<div class="col-xs-12 col-sm-12 col-md-7 col-lg-8 hidden-xs hidden-sm">
					<h1 class="txt-color-red login-header-big">HRMIS</h1>
					<div class="hero">
						<div class="pull-left login-desc-box-l">
							<h4 class="paragraph-header">Хүний нөөцийн удирдлагын мэдээллийн систем</h4>
							<div class="login-app-icons">
								<a href="javascript:void(0);" class="btn btn-danger btn-sm">Гарын авлага</a>
								<a href="javascript:void(0);" class="btn btn-danger btn-sm">Нийтлэг асуулт хариулт</a>
							</div>
						</div>							
						<img src="http://www.evernexthr.com/Images/slider/graphiclogo.png" class="pull-right display-image" alt="" style="width:350px; margin-top: -20px;">
					</div>
					<div class="row">
						<div class="col-xs-12 col-sm-12 col-md-6 col-lg-6">
							<h5 class="about-heading">Програмд яаж нэвтрэх вэ?</h5>
							<p>
								Сангийн яамны өөрийн домайн нэр болон нууц үгээ хийж системд нэвтрэнэ.<br />Жишээлбэл Нэвтрэх домайн нэр: mof9999999
							</p>
						</div>
						<div class="col-xs-12 col-sm-12 col-md-6 col-lg-6">
							<h5 class="about-heading">Өргөн боломжит модулиуд!</h5>
							<p>
								- Газар нэгжийн архив<br />- Ажилтаны хөдөлгөөний архив<br />- Анкетан мэдээллээ өөрөө засварлах<br />- Чөлөөний хүсэлт, Амралтын хүсэлт гэт мэт... 
							</p>
						</div>
					</div>
				</div>
				<div class="col-xs-12 col-sm-12 col-md-5 col-lg-4">
					<div class="well no-padding">
						<form id="login-form" class="smart-form client-form">
							<header>
								Нэвтрэх
							</header>
							<fieldset>									
								<section>
									<label class="label">Нэвтрэх нэр</label>
									<label class="input"> <i class="icon-append fa fa-user"></i>
										<input type="text" id="loginUsername" name="loginUsername">
										<b class="tooltip tooltip-top-right"><i class="fa fa-user txt-color-teal"></i> Нэвтрэх домайн нэр оруулна уу</b>
									</label>
								</section>
								<section>
									<label class="label">Нууц үг</label>
									<label class="input">
                                        <i class="icon-append fa fa-lock"></i>
										<input type="password" id="loginPassword" name="loginPassword">
										<b class="tooltip tooltip-top-right"><i class="fa fa-lock txt-color-teal"></i> Нууц үг оруулна уу</b>
									</label>
								</section>
                                <section id="errSection" style="margin: 5px 0 0 0;"></section>
							</fieldset>
							<footer>
								<button id="login-btn" type="submit" class="btn btn-primary">
									Нэвтрэх
								</button>
							</footer>
						</form>
					</div>					
				</div>
			</div>
		</div>
	</div>
    <!--================================================== -->	

		<!-- PACE LOADER - turn this on if you want ajax loading to show (caution: uses lots of memory on iDevices)-->
		<script src="js/plugin/pace/pace.min.js"></script>

	    <!-- Link to Google CDN's jQuery + jQueryUI; fall back to local -->
	    <script src="//ajax.googleapis.com/ajax/libs/jquery/2.1.1/jquery.min.js"></script>
		<script> if (!window.jQuery) { document.write('<script src="js/libs/jquery-2.1.1.min.js"><\/script>');} </script>

	    <script src="//ajax.googleapis.com/ajax/libs/jqueryui/1.10.3/jquery-ui.min.js"></script>
		<script> if (!window.jQuery.ui) { document.write('<script src="js/libs/jquery-ui-1.10.3.min.js"><\/script>');} </script>

		<!-- IMPORTANT: APP CONFIG -->
		<script src="js/app.config.js"></script>

		<!-- JS TOUCH : include this plugin for mobile drag / drop touch events 		
		<script src="js/plugin/jquery-touch/jquery.ui.touch-punch.min.js"></script> -->

		<!-- BOOTSTRAP JS -->		
		<script src="js/bootstrap/bootstrap.min.js"></script>

		<!-- JQUERY VALIDATE -->
		<script src="js/plugin/jquery-validate/jquery.validate.min.js"></script>
		
		<!-- JQUERY MASKED INPUT -->
		<script src="js/plugin/masked-input/jquery.maskedinput.min.js"></script>
		
		<!--[if IE 8]>
			
			<h1>Your browser is out of date, please update your browser by going to www.microsoft.com/download</h1>
			
		<![endif]-->

		<!-- MAIN APP JS FILE -->
		<script src="js/app.min.js"></script>

		<script type="text/javascript">
			runAllForms();

			$(function() {
				$("#login-form").validate({
				    rules: {
				        loginUsername: {
				            required: true
				        },
				        loginPassword: {
				            required: true
				        }
				    },
				    messages: {
				        loginUsername: {
				            required: 'Нэвтрэх домайн нэр оруулна уу'
				        },
				        loginPassword: {
				            required: 'Нууц үг оруулна уу'
				        }
				    },
					errorPlacement : function(error, element) {
						error.insertAfter(element.parent());
					},
					submitHandler: function (form) {
					    $("#login-btn").html('<i class="fa fa-refresh fa-spin"></i> Нэвтрэх');
					    $("#login-btn").prop('disabled', true);
					    $.ajax({
					        type: "POST",
					        url: "../ws.aspx/CheckLogin",
					        data: '{username:"' + $.trim($('#loginUsername').val()) + '", pass:"' + $.trim($('#loginPassword').val()) + '"}',
					        contentType: "application/json; charset=utf-8",
					        dataType: "json",
					        success: function (msg) {
					            $('#errSection').html('');
					            $("#login-btn").html('Нэвтрэх');
					            $("#login-btn").prop('disabled', false);
					            if (window.location.hash != '') window.location = '../' + window.location.hash;
					            else window.location = '../#pg/dashboard.aspx?t=f';
					        },
					        failure: function (response) {
					            alert(response.d);
					        },
					        error: function (xhr, status, error) {
					            var err = eval("(" + xhr.responseText + ")");
					            $('#errSection').html('<div class="alert alert-danger fade in" style="margin:0; padding:5px;"><button class="close" data-dismiss="alert">×</button>' + err.Message + '</div>');
					            $("#login-btn").html('Нэвтрэх');
					            $("#login-btn").prop('disabled', false);
					        }
					    });
					}
				});
			});
		</script>

		<!-- Your GOOGLE ANALYTICS CODE Below -->
		<script type="text/javascript">
		
		  var _gaq = _gaq || [];
		  _gaq.push(['_setAccount', 'UA-43548732-3']);
		  _gaq.push(['_trackPageview']);
		
		  (function() {
		    var ga = document.createElement('script'); ga.type = 'text/javascript'; ga.async = true;
		    ga.src = ('https:' == document.location.protocol ? 'https://ssl' : 'http://www') + '.google-analytics.com/ga.js';
		    var s = document.getElementsByTagName('script')[0]; s.parentNode.insertBefore(ga, s);
		  })();
		
		</script>
</body>
</html>
