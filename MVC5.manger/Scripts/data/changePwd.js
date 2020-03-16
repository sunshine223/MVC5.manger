var init ="";

$(function () {
    var namess = $("#names", parent.document);
    var changePwd = namess.text();
    var orldPwd = "";
    $("#uesrPwd").val("");
    $("#uesrPwd").attr('readonly', 'true');
    $.ajax({
        type: "post",
        data: { names: changePwd },
        dataType: "json",
        url: "/Data/SelectPwds",
        success: function (res) {
            $("#userName").val(res.userLog);
            $("#userID").val(res.userID);
            init=res.userPwd;
        }
    });
   
  
});

function onblurs() {
    $("#uesrPwd").removeAttr('readonly');
}

//清空密码框
function remove() {
  $("#uesrPwd").val("");
 $("#newPwd1").val("");
 $("#newPwd2").val("");
}

//提交数据
function btn_ok() {
    var Pwd = $("#uesrPwd").val();
    var newPwd1 = $("#newPwd1").val();
    var newPwd2 = $("#newPwd2").val();
    var form = $("#pwdss").serialize();
    if (init != Pwd) {
        alert("你输入的原密码不对！请重新输入");
        remove();
       return;
    } 
    if (newPwd1 != newPwd2) {
        alert("你2两次输入的密码不对！请重新输入");
        remove();
      return;
    }
    if (newPwd1==""|| newPwd2=="") {
        alert("请重输入密码");
        remove();
        return;
    }
    $.ajax({
        type: "post",
        data: form,
        dataType: "text",
        url: "/Data/ChangePwds",
        success: function (res) {
            alert("修改成功!");
            location.href = "/data/Index";
            return;
        },
        error: function () {
            alert("网络错误");
            return;
        }
    });
}

