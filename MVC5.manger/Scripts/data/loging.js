function loging() {
    var name = $("#name").val();
    var pwd = $("#pwd").val();
    $.ajax({
        url: "/Home/Check",
        type: "post",
        data: { name: name, pwd: pwd },
        dataType: "json",
        success: function (res) {
            if (res.res == true) {
                alert("登录成功");
                location.href = "/Contents/Index";
            }
            else {
                alert("请输入正确的账号和密码！");
            }
        },
        error: function () {
            alert("请求错误！");
        }
    });
}

function register() {
    location.href = "/Register/Index";
}