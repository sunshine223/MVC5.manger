
$(function () {
   
    init();
});

function init() {
    $('#tb').bootstrapTable("destroy");
    // 初始化表格,动态从服务器加载数据
    $("#tb").bootstrapTable({
        method: "post",
        contentType: "application/x-www-form-urlencoded",
        url: "/Empsalary/Init", // 获取数据的地址
        striped: true, // 表格显示条纹
        pagination: true, // 启动分页
        pageSize: 5, // 每页显示的记录数
        pageNumber: 1, // 当前第几页
        minimumCountColumns: 1, // 最少允许的列数
        clickToSelect: true, //是否启用点击选中行
        pageList: [5, 10, 25], // 记录数可选列表
        search: false, // 是否启用查询
        sidePagination: "server", // 表示服务端请求
        // 设置为undefined可以获取pageNumber，pageSize，searchText，sortName，sortOrder
        // 设置为limit可以获取limit, offset, search, sort, order
        queryParamsType: "undefined",
        queryParams: function queryParams(params) { // 设置查询参数
            var param = {
                curPage: params.pageNumber,// 当前第几页
                pageSize: params.pageSize,// 每页显示的记录数
                dName: $(".searchs").val() || ""
            };
            return param;
        },
        columns: [{
            checkbox: true
        }, {
            title: '序号',//标题  可不加  
            formatter: function (value, row, index) {
                return index + 1;
            }
        }, {
                field: 'ID',
            title: '部门ID'
            },
            {
                field: 'departmentName',
                title: '部门名称'
            },
            {
                field: 'CreateTime',
                title: '创建时间',
                formatter: function (row) {
                    return dateFormat(row);
                }
        }, {
            field: 'num',
            title: '部门人数',

        }, {
                field: 'states',
            title: '备注'
        }],
        onLoadError: function () { // 加载失败时执行
            console.log("请求失败！");
        },
        responseHandler: function (res) {
            return {
                "total": res.rows,// 总页数
                "rows": res.data || []
                // 数据
            };
        }
    });

}

//日期格式化
function dateFormat(dateStr) {
    if (!dateStr) {
        return;
    }
    var reg = /\d+/g;
    var dateNum = parseInt(dateStr.match(reg).join(""));
    var date = new Date(dateNum);
    var year = date.getFullYear();
    var month = date.getMonth() + 1;
    var day = date.getMonth();
    var hour = date.getHours();
    var min = date.getMinutes();
    var sec = date.getSeconds();
    return year + "-" + month + "-" + day + " " + hour + ":" + min + ":" + sec;
}



//查询
function search() {
    init();
}

//删除
function del() {
    var trs = $("#tb").find("tr");
    var ids = [];
    for (var i = 0; i < trs.length; i++) {
        if (trs[i].children[0].children[0].checked) {
            ids.push(trs[i].children[2].innerHTML.trim());
        }

    } if (ids.length != 1) {
        alert("请选择一条数据！");
        return;
    }
    $.ajax({
        url: "/Empsalary/Dele_Department",
        dataType: "json",
        type: "post",
        data: { id: ids },
        success: function (res) {
            alert("删除成功！")
            init();
            return;
        },
        error: function () {
            alert("删除失败！")
            init();
            return;
        }
    });
}
//添加
function add() {
    //打开模态框
    $("#myModal").modal("show");
    $("#hides").attr("style", "display: block");
    $("#titels").text("添加部门信息");
}
//修改
function updt() {
    var trs = $("#tb").find("tr");
    var ids = [];
    for (var i = 0; i < trs.length; i++) {
        if (trs[i].children[0].children[0].checked) {
            ids.push(trs[i].children[2].innerHTML.trim());
        }

    } if (ids.length != 1) {
        alert("请选择一条数据！");
        return;
    }
    $.ajax({
        url: "/Empsalary/Depart_select",
        dataType: "json",
        type: "post",
        data: { id: ids },
        success: function (res) {
            $("#ID").val(res[0].ID);
            $("#departmentName").val(res[0].departmentName);
            $("#CreateTime").val(res[0].CreateTime);
            $("#num").val(res[0].num);
            $("#states").val(res[0].states);
        }
    });
    //打开模态框
    $("#hides").attr("style", "display: none");
  
    $("#ID").val("用户ID自增，无需填写");
    $("#titels").text("修改部门信息");
    $("#myModal").modal("show");
}

function saveMenu() {
    var Url = "";
    var parmas ="";
    var departmentName = $("#ID").val();
    if (departmentName == "")
    {
        parmas = $("#menuForm").serialize();
        Url = "/Empsalary/Add_Department";
    }
    else
    {
        parmas = $("#menuForm").serialize();
        Url = "/Empsalary/Update_Department";
    }
 
    $.ajax({
        url:Url,
        dataType: "json",
        type: "post",
        data: parmas,
        success: function (res) {
            $(':input', '#menuForm').val("");
            alert("操作成功！");
         
            cancel();
        },
        error: function () {
            alert("操作失败！");
            return;
        }
    });
}

function upload() {
    $("#myModal1").modal("show");
}
//上传文件
function uploadExcel(){
  
    var file = $("#uploadDeptExcel")[0].files[0];
    var formData = new FormData();
    formData.append("file", file)
    $.ajax({
        url: '/Empsalary/UploadDept',
        dataType: 'json',
        type: 'POST',
        async: false,
        data: formData,
        processData: false, // 使数据不做处理 
        contentType: false, // 不要设置Content-Type请求头 
        success: function (data) {
            if (data.status == 'True') {
                alert("上传成功！");
            }
        },
        error: function (response) {
            console.log(response);
        }
    });
}

//取消
function cancel() {
    //隐藏模态框
    $("#myModal1").modal("hide");
    $(':input', '#menuForm').val("");
    init();
}
