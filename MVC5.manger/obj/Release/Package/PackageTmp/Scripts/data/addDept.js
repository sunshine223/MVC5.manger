

function add() {
    var parmas = $("#addForm").serialize();
    $.ajax({
        url: "/NewDept/Add_detpart",
        dataType: "json",
        type: "post",
        data: parmas,
        success: function (res) {
            if (res.res == true) {
                alert("添加成功");
                location.href = "/Data/Tables";
            }
        },
        error: function () {
            alert("添加失败！");
            return;
        }
    });
}

