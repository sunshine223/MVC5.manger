$.extend($.validator.messages, {
	required: "不能为空",
	remote: "请修正此字段",
	email: "请输入有效的电子邮件地址",
	url: "请输入有效的网址",
	date: "请输入有效的日期",
	dateISO: "请输入有效的日期 (YYYY-MM-DD)",
	number: "请输入有效的数字",
	digits: "只能输入数字",
	creditcard: "请输入有效的信用卡号码",
	equalTo: "你的输入不相同",
	extension: "请输入有效的后缀",
	maxlength: $.validator.format("最多可以输入 {0} 个字符"),
	minlength: $.validator.format("最少要输入 {0} 个字符"),
	rangelength: $.validator.format("请输入长度在 {0} 到 {1} 之间的字符串"),
	range: $.validator.format("请输入范围在 {0} 到 {1} 之间的数值"),
	max: $.validator.format("请输入不大于 {0} 的数值"),
	min: $.validator.format("请输入不小于 {0} 的数值"),
	specialCharFilter: "请不要输入特殊字符",
	checknumzimu:"请输入数字字母和下划线",
	checknumzh:"输入字母、数字、中文、下划线",
	minNumber:"输入的数字小数点位数不能大于两位"
});
function bindformvalidate(selector,rules,messages){
	return $(selector).validate({
		onfocusout: function(element) {
			$(element).valid(); 
		},
		rules:rules,
		messages:messages,
		errorPlacement: function (error, element) {
			if(""!=error.html()&&element.parent().parent().find("em").html()==""){
				element.parent().find("#err").html("<span style=\"width: 40%;color: red;\"><img src=\"../../../image/error.png\"/>"+error.html()+"</span>");
			}else if(""!=error.html()&&element.parent().parent().find("em").html()!=""){
				element.parent().find("#err").html("");
				element.parent().find("#err").html("<span style=\"width: 40%;color: red;\"><img src=\"../../../image/error.png\"/>"+error.html()+"</span>");
			}else if(""==error.html()&&element.parent().parent().find("em").html()==""){
				element.parent().find("#err").html("<span style=\"width: 40%;color: green;\"><img src=\"../../../image/right_1.png\"/>验证成功</span>");
			}else if(""==error.html()&&element.parent().parent().find("em").html()!=""){
				element.parent().find("#err").html("");
				element.parent().find("#err").html("<span style=\"width: 40%;color: green;\"><img src=\"../../../image/right_1.png\"/>验证成功</span>");
			}
        },
        success:function(label) {
        }
	});
}

function resetErr(element){
	
	element.parent().find("#err").html("<span id=\"err\" class=\"modal-error\"></span>");
}

/**
 * 特殊字符验证 
 */
$.validator.addMethod("specialCharFilter", function(value, element) {  
    var pattern = new RegExp("[`~!@#$^&*()=|{}':;,.<>/?~！@#￥……&*（）——|【】‘；：”“'。，、？%+ 　\"\\\\]");  
    var specialStr = "";  
    for(var i=0;i<value.length;i++){  
         specialStr += value.substr(i, 1).replace(pattern, '');  
    }  
      
    if( specialStr == value){  
        return true;  
    }  
      
    return false;  
});
	
$.validator.addMethod("checkpic", function(value, element) { 
	var filepath =value;
	var fileArr = filepath.split("\\");
	var fileTArr =fileArr[fileArr.length-1].toLowerCase().split(".");
	var filetype = fileTArr[fileTArr.length-1];
	if(filetype !="zip"&& filetype!="rar"){
	    return false;  
	}else{
	    return true;  
	}

},"上传文件格式不适合");
	

/**
 * 验证手机号
 */
$.validator.addMethod("checkphone",function(value,element,params){
	if(value.length<1){
	return false;
	}
	var reg = /^1[3|4|5|7|8][0-9]{9}$/;
	return flag = reg.test(value);
	},"请输入正确的手机号");

/**
 * 验证只能输入字母数字
 */
$.validator.addMethod("checknumzimu",function(value,element,params){
	if(value.length<1){
	return false;
	}
	var reg = /^[0-9a-zA-Z_-]*$/;
	return flag = reg.test(value);
	},"请输入字母、数字、下划线、横线");

/**
 * 验证只能输入字母数字中文下划线
 */
$.validator.addMethod("checknumzh",function(value,element,params){
	if(value.length<1){
	return false;
	}
	var reg = /^[\u4e00-\u9fa5_a-zA-Z0-9]+$/;
	return flag = reg.test(value);
	},"请输入输入字母、数字、中文、下划线");

/**
 * 验证數字字母組合
 */
$.validator.addMethod("numAndzimu",function(value,element,params){
	var regu = /\w*[0-9]+\w*$/;  
    var regu2 = /\w*[a-zA-Z_]+\w*$/;  
	
    if(!regu.test(value) || !regu2.test(value)){
    	return false;
    }else{
    	return true;
    }
    
	},"密码必须为字母、数字、下橫线组合");
/**
 * 自定义validate验证输入的数字小数点位数不能大于两位
 * */
jQuery.validator.addMethod("minNumber",function(value, element){
    var returnVal = true;
    inputZ=value;
    var ArrMen= inputZ.split(".");    //截取字符串
    if(ArrMen.length==2){
        if(ArrMen[1].length>2){    //判断小数点后面的字符串长度
            returnVal = false;
            return false;
        }
    }
    return returnVal;
},"小数点后最多为两位");


function formData(form,obj){
	//var obj = eval("("+jsonStr+")");
	var key,value,tagName,type,arr;
	for(x in obj){
		key = x;
		value = obj[x];
		$("#"+form+" [name='"+key+"'],[name='"+key+"[]']").each(function(){
			tagName = $(this)[0].tagName;
			type = $(this).attr('type');
			if(tagName=='INPUT'){
				if(type=='radio'){
					$(this).attr('checked',$(this).val()==value);
				}else if(type=='checkbox'){
					arr = value.split(',');
					for(var i =0;i<arr.length;i++){
						if($(this).val()==arr[i]){
							$(this).attr('checked',true);
							break;
						}
					}
				}else{
					$(this).val(value);
				}
			}else if(tagName=='SELECT' || tagName=='TEXTAREA'){
				$(this).val(value);
			}else if(tagName=='DIV' ){
				$(this).html(value);
			}
			
		});
	}
}
function divformData(form,obj){
	var key,value,tagName,type,arr;
	for(x in obj){
		key = x;
		value = obj[x];
		$("#"+form+" [name='"+key+"'],[name='"+key+"[]']").each(function(){
			tagName = $(this)[0].tagName;
			type = $(this).attr('type');
			if(tagName=='INPUT'){
				if(type=='radio'){
					$(this).attr('checked',$(this).val()==value);
				}else if(type=='checkbox'){
					arr = value.split(',');
					for(var i =0;i<arr.length;i++){
						if($(this).val()==arr[i]){
							$(this).attr('checked',true);
							break;
						}
					}
				}else{
					$(this).val(value);
				}
			}else if(tagName=='SELECT' || tagName=='TEXTAREA'){
				$(this).val(value);
			}
			
		});
	}
}

//清楚整个form的验证信息
function removeError(ele){
	$('#'+ele).find('.modal-error span').remove();
}
