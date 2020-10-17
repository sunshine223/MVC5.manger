



$(function () {
    init();
});


function init() {
    $('#tb').bootstrapTable("destroy");
    // 初始化表格,动态从服务器加载数据
    $("#tb").bootstrapTable({
        method: "post",
        contentType: "application/x-www-form-urlencoded",
        url: "/maize/Init", // 获取数据的地址
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
                field: 'Id',
            title: '编号'
        },
        {
            field: 'Cats',
            title: '维护内容'
        },
        {
            field: 'Name',
            title: '维护人',

        }, {
            field: 'moneys',
            title: '维护金额',

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
        url: "/maize/Del_User",
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
