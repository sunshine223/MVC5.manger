

function person() {
    var name = $("#person").text();
    var url = "/Data/Person";
    $(".larry-iframe").attr("src", url);
}
//退出
function Return() {
    $.ajax({
        url: "../main/ClearSession",
        data: {},
        dataType: "json",
        type: "post",
        success: function (res) {
            if (res.res == true) {
                //页面跳转
                location.href = "../main/Index";
            } else {
                alert("退出失败。");
            }
        },
        error: function () {

        }
    });
}

function tables() {
    $(".larry-iframe").attr("src", "/Data/Tables");
}

function home() {
    $(".larry-iframe").attr("src", "/Data/Index");
}
function updetPwd() {
    $(".larry-iframe").attr("src", "/Data/UpdetProsens");
}
function magazine() {
    $(".larry-iframe").attr("src", "/Data/Magazine");
}


function ChangePwd() {
    $(".larry-iframe").attr("src", "/Data/ChangePwd");
}
function Salary() {
    $(".larry-iframe").attr("src", "/Empsalary/Empsalary");
}
function bumen() {
    $(".larry-iframe").attr("src", "/Empsalary/Department");
}
function NewDept() {
    $(".larry-iframe").attr("src", "/NewDept/Index");
    
}

function training() {
    $(".larry-iframe").attr("src", "/Training/Index");
}

function Add_Training() {
    $(".larry-iframe").attr("src", "/Training/Add_trainings");
}

function user() {
    $(".larry-iframe").attr("src", "/User/Index");
}
