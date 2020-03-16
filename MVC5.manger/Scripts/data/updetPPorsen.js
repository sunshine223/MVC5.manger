$(function () {
    var test = $("#names", parent.document);
    var namess = test.text();
    $.ajax({
        url: "/Data/UpdetProsen",
        dataType: "json",
        type: "post",
        data: { names: namess },
        success: function (res) {
            for (var i = 0; i < res.length; i++) {
            $("#userID").val(res[i].userID);
            $("#userLog").val(res[i].userLog);
            $("#userName").val(res[i].userName);
            $("#userSex").val(res[i].userSex);
            $("#userPhone").val(res[i].userPhone);
            $("#userType").val(res[i].userType);
            $("#userAderrs").val(res[i].userAderrs);
        }
        }
    });

});


//修改
function btn_ok() {
    var params = $("#updetPerson").serialize();
  
    $.ajax({
        type: "post",
        data: params,
        dataType: "text",
        url: "/Data/UpdetSave",
        success: function (res) {
            alert("修改成功!");
            location.href = "/data/Person";
            return;
        },
        error: function () {
            alert("网络错误");
            return;
        }
    });
}

//取消
function btn_cancel() {
    //打开用户信息
    location.href = "/Data/Index";
    return;
}