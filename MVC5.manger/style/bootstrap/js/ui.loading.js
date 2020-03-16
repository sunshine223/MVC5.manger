/**
* 只需要导入js和css
* 并在加载完成之后初始化即可 如下:
*
* jQuery(function($) {
*   $(document).ui_loading();
* })
* 参数目前支持
*
* overlay:true/false  boolean 是否开启全屏遮挡效果,默认true
*
* opacity:0~1.0 number 全屏遮挡透明度,当为0是关闭全屏遮挡效果 默认0.2
*
* supportIframe:true/false boolean 是否支持iframe加载效果,默认true
*
* message String 提示性问题 默认值:'数据加载中，请稍等...'
*
* jQuery(function($) {
*   $(document).ui_loading({
*       overlay:true,
*       opacity:0.2,
*       supportIframe:true,
*       message:''
*   });
* })
*
*
* 若要iframe加载效果也生效,除了supportIframe:true外,每次更改iframe src属性时,需要手动触发 beforeload事件
* 例如:Iframe.trigger('beforeload');
*
*/
(function ($) {
    $.ui_loading = function () { }
    $.extend($.ui_loading, {
        settings: {
            overlay: true,
            opacity: 0.2,
            supportIframe: true,
            message: '加载中，请稍等...'
        }
    });

    $.fn.ui_loading = function (settings) {
        if ($(this).length == 0) return
        init(settings)
    }

    function init(settings) {
        if ($.ui_loading.settings.inited) return true
        else $.ui_loading.settings.inited = true

        if (settings) $.extend($.ui_loading.settings, settings)
        $('<div id="ui_loading_progressBar" class="ui_loading_progressBar" style="display: none; ">' + $.ui_loading.settings.message + '...</div>').appendTo('body');
        $("body").append('<div id="ui_loading_overlay" class="ui_loading_hide"></div>');

        var $loading = $("#ui_loading_progressBar").hide();
        $(document).ajaxStart(function () {
            showOverlay();
            showLoading();
        }).ajaxStop(function () {
            hideOverlay();
            hideLoading();
        });
        if ($.ui_loading.settings.supportIframe) {
            $('iframe').each(function () {
                $(this).bind('beforeload', function () {
                    showOverlay(this);
                    showLoading(this);
                });
                $(this).load(function () {
                    hideOverlay();
                    hideLoading();
                });
            });
        }
    }

    function skipOverlay() {
        return $.ui_loading.settings.overlay == false || $.ui_loading.settings.opacity === null
    }
    function isVisible(obj) {
        if (obj == null) return false;
        return obj.offsetWidth > 0 && obj.offsetHeight > 0;
    }
    function showLoading(iframe) {
        if (iframe) {
            var pIframe = iframe.getBoundingClientRect();
            var left = parseInt((pIframe.left).toFixed(0)) + (pIframe.right - pIframe.left) / 2 + $(document).scrollLeft();
            var top = parseInt((pIframe.top).toFixed(0)) + (pIframe.bottom - pIframe.top) / 2 + $(document).scrollTop();
            //$("#ui_loading_progressBar").css({ "left": left, "top": top })
            $("#ui_loading_progressBar").css({ "left": "50%", "top": "50%" })
		.show();
        } else {
            $("#ui_loading_progressBar").css({ "left": "50%", "top": "50%" })
		.show();
        }
    }
    function hideLoading() {
        $("#ui_loading_progressBar").hide();
    }
    function showOverlay(iframe) {
        if (skipOverlay()) return
        if ($('#ui_loading_overlay').length == 0)
            $("body").append('<div id="ui_loading_overlay" class="ui_loading_hide"></div>');

        var $overlay = $("#ui_loading_overlay");
        if (iframe) {
            if (!isVisible(iframe)) return;
            if (isNaN(iframe.height)) {
                $overlay.height(iframe.height);
            } else {
                $overlay.height(iframe.height + "px");
            }
            if (isNaN(iframe.width)) {
                $overlay.width(iframe.width)
            } else {
                $overlay.width(iframe.width + "px");
            }
            var pIframe = iframe.getBoundingClientRect();
            $overlay.css("left", pIframe.left + $(document).scrollLeft()).css("top", pIframe.top + $(document).scrollTop());
        } else {
            $overlay.height("100%").width("100%").css("top", 0).css("left", 0);
        }
        $('#ui_loading_overlay').hide().addClass("ui_loading_overlayBG")
      .css('opacity', $.ui_loading.settings.opacity)
      .fadeIn(200)
        return false
    }

    function hideOverlay() {
        if (skipOverlay()) return
        $('#ui_loading_overlay').fadeOut(200, function () {
            $("#ui_loading_overlay").removeClass("ui_loading_overlayBG")
            $("#ui_loading_overlay").addClass("ui_loading_hide")
            $("#ui_loading_overlay").remove()
        })
        return false
    }
})(jQuery)
$(function () {
	/*if(!document.getElementById("login-bg")) 
	{ */
		setInterval("resizeMainContainer()", 500);
		$(".layout-main").width($(window).width() -100);
		$(document).ui_loading({
			overlay: true,
		    opacity: 0.2,
		    supportIframe: true
		});
		$(".body-iframe").trigger('beforeload');
	/*} */
});
	/// 自动计算高度
	function resizeMainContainer() {
		$(".layout-main").height($(window).height()-80);
		$(".body-iframe").height($(".layout-main").height()-80);
		$(".layout-main").width($(window).width()-100);
	}