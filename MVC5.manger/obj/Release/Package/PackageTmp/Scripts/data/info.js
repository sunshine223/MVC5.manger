
$(function () {
    search();
});


//员工信息
function search() {
    //初始化表格，先销毁数据
    $('#tbs').bootstrapTable("destroy");
       // 初始化表格,动态从服务器加载数据
    $("#tbs").bootstrapTable({
        method: "post",
        contentType: "application/x-www-form-urlencoded",
        url: "/Data/Table_check", // 获取数据的地址
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
                dName: $(".searchs").val() || "" };
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
                field: 'empId',
                title: '用户ID'
            }, {
                field: 'empName',
                title: '用户名'
            }, {
                field: 'empPosition',
                title: '职位',
               
            }, {
                field: 'empHrId',
                title: 'empHrId'
            }, {
                field: 'empSalary',
                title: '工资'
            }, {
                field: 'empBonus',
                title: '奖金'
            }, {
                field: 'deptId',
                title: 'deptId'
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
