

//删除
function del() {
    var trs = $("#tbs").find("tr");
    inits = [];
    for (var i = 0; i < trs.length; i++) {
        if (trs[i].children[0].children[0].checked) {
            inits.push(trs[i].children[2].innerHTML.trim());
        }
    }
    if (inits.length != 1) {
        alert("请选择一条且只能选择一条数据");
        return;
    }
    $.ajax({
        type: "post",
        data: { empId: inits.join(",") },
        dataType: "text",
        url: "/Data/Del",
        success: function (res) {
            alert("删除成功");
            search();
        },
        error: function () {
            alert("网络错误");
            return;
        }
    });
}
//打开添加界面
function add() {
    location.href = "/NewDept/Index";
}

//取消
function cancel() {
    //打开用户信息
    location.href = "/Data/Tables";
    return;
}
////打开修改界面
function updt() {
    var trs = $("#tbs").find("tr");
    ids = [];
    for (var i = 0; i < trs.length; i++) {
        if (trs[i].children[0].children[0].checked) {
            ids.push(trs[i].children[2].innerHTML.trim());
        }
    }
    if (ids.length != 1) {
        alert("请选择一条且只能选择一条数据");
        return;
    }
    $("#myModal").modal("show");
    $.ajax({
        type: "post",
        data: { id: ids },
        dataType: "json",
        url: "/Data/SelectDEL",
        success: function (res) {
            var time = res[0].empInDate;
            var reg = /\d+/g;
            var longtime = time.match(reg).join("");
            var newtime = new Date(parseInt(longtime)).toLocaleString();
            $("#empId").val(res[0].empId);
            $("#empName").val(res[0].empName);
            $("#empPosition").val(res[0].empPosition);
            $("#empHrId").val(res[0].empHrId);
            $("#empInDate").val(newtime);
            $("#empSalary").val(res[0].empSalary);
            $("#empBonus").val(res[0].empBonus);
            $("#deptId").val(res[0].deptId);
        }
    });
}

function saveMenu() {

    $.ajax({
        type: "post",
        data: $("#menuForm").serialize(),
        dataType: "text",
        url: "/Data/Updet",
        success: function (res) {
            alert("修改成功!");
            location.href = "/data/Tables";
            return;
        },
        error: function () {
            alert("网络错误");
            return;
        }
    });
}
