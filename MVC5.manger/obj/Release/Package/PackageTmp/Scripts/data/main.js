 var ids= [];
$(function () {
    init();
  
});

function init() {
    var test = $("#names", parent.document)
    var userName = test.text();
    $.ajax({
        url: "/Data/Check",
        dataType: "json",
        type: "post",
        data: { eName: userName },
        success: function (res) {
            for (var i = 0; i < res.length; i++) {
                $("#userID").val(res[i].userID);
                $("#userName").val(res[i].userName);
                $("#userSex").val(res[i].userSex);
                $("#userPhone").val(res[i].userPhone);
                $("#usermingzu").val(res[i].usermingzu);
                $("#userAderrs").val(res[i].userAderrs);
                $("#userType").val(res[i].userType);
                $("#img").attr("src", res[i].userPhot);
                ids.push(res[i].userID);
            }

        },
        error: function () {
            alert("服务器繁忙，请稍后再试。")
        }
    });

}


function updown() {
    $("#myModal").modal("show");
}
function saveMenu() {
    var formData = new FormData();
    formData.append('file', $('#file')[0].files[0]);
    formData.append('userID',ids);
    $.ajax({
        url: '/Data/UpdetImg',
        type: 'POST',
        cache: false,
        data: formData,
        processData: false,
        contentType: false,
        success: function (res) {
        if (res.res == true) {
            alert("上传成功！");
            $("#myModal").modal("hide");
            $("#file").val("");
            $("#img").attr("src", res.userPhot);
             init();
        }
        },
        error: function (res) {
            if (res.res == false) {
                alert("上传失败！");
            }
        }
    });
}
