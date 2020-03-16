

$(function () {
    init();
});


function init() {
    $('#tb').bootstrapTable("destroy");
    // 初始化表格,动态从服务器加载数据
    $("#tb").bootstrapTable({
        method: "post",
        contentType: "application/x-www-form-urlencoded",
        url: "/Training/Init", // 获取数据的地址
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
                curPage: params.pageNumber,
                pageSize: params.pageSize,
                name: $(".searchs").val() || ""
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
            field: 'id',
            title: '编号'
        },
        {
            field: 'people',
            title: '培训人'
        },
        {
            field: 'Training_Time',
            title: '培训时间',
            formatter: function (row) {
                return dateFormat(row);
            }
        }, {
            field: 'theme',
            title: '培训类容',

        }, {
            field: 'place',
            title: '培训地点'
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
        url: "/Training/Del_Training",
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

function btn_ok() {
    $.ajax({
        url: "/Training/Add_Training",
        dataType: "json",
        type: "post",
        data: $("#training").serialize(),
        success: function (res) {

            alert("添加成功！")
            location.href = "../Training/Index";
            return;
        },
        error: function () {
            alert("添加失败！")
            return;
        }
    });
}