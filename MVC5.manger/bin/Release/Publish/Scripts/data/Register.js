$(function () {
    initValidate();
});

function initValidate() {
    $("#registeForm").bootstrapValidator({
        excluded: [':disabled'],
        message: 'This value is not valid',
        feedbackIcons: {
            valid: 'glyphicon glyphicon-ok',
            invalid: 'glyphicon glyphicon-remove',
            validating: 'glyphicon glyphicon-refresh'
        },
        fields: {
            userLog: {
                message: '登录名验证失败',
                validators: {
                    notEmpty: {
                        message: '登录名不能为空'
                    },
                    regexp: {
                        regexp: /^[a-zA-Z][a-zA-Z0-9_]{3,12}$/,
                        message: '以字母开头，只能包含大写、小写、数字和下划线,3位以上'
                    }
                }
            },
            userName: {
                message: '用户名验证失败',
                validators: {
                    notEmpty: {
                        message: '用户名不能为空'
                    }
                }
            },
            userPwd: {
                message: '密码验证失败',
                validators: {
                    notEmpty: {
                        message: '密码不能为空'
                    }
                }
            }
           
        }
    });
}



//注册事件
function register() {
    initValidate();
    var parmas = $("#registeForm").serialize();
    //让表单进行校验
    $("#registeForm").data("bootstrapValidator").validate();
    //获取校验结果
    var flag = $('#registeForm').data("bootstrapValidator").isValid();
    //判断
    if (flag == false) {
        return;
    }
   
        $.ajax({
            type: "post",
            data: parmas,
            dataType: "json",
            url: "/Register/Checks",
            success: function (res) {
                if (res == false) {
                    alert("提示用户名重复");
                    $(':input', '#registeForm').val("");
                    return;
                } else {
                    alert("注册成功");
                    location.href = "/home/Index";
                }
            },
            error: function () {
               alert("服务器繁忙，请稍后再试。");
            }
        });
    
}
function cancel() {
    location.href = "/Main/Index";
}