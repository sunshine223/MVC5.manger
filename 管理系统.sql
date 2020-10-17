create database manger
go
use manger
go
create table userChecke(
userID int primary key identity(1,1),
userName varchar(50),
userLog  varchar(50),
userPwd  varchar(50),
userType varchar(50)
)
go
insert into userChecke values('张三','admin','123456','主管')
insert into userChecke values('李四','哗啦啦Happy','123456','经理')
insert into userChecke values('浅若夏沫','sunshine','123456','部长')
go
select *from userChecke

go
create table department(
ID int primary key identity(1,1),
departmentName varchar(50),
CreateTime  date,
num  int,
states varchar(50)
)
go 
insert into department values('人力资源部','2013-12-03','12','负责人事工作')
insert into department values('C#部','2013-12-03','23','负责编写程序')
insert into department values('基础部','2013-12-03','12','负责编写基础程序')
insert into department values('PHP部','2013-12-03','14','PHP网页')
insert into department values('ASP部','2013-12-03','13','ASP网页')
insert into department values('H5部','2013-12-03','14','H5网页')
insert into department values('ASP.NET部','2013-12-03','13','ASP.NET网页')
SELECT *from department
go
create table userinfo(
userID int primary key identity(1,1),
userSex  char(2),
userPhone int,
userNum varchar(100),
usermingzu  varchar(100),
userAderrs varchar(100),
userPhot varchar(100),
)
go
insert into userinfo values('男','123456789','500233103620131023','汉族','重庆','/style/img/tx.png')
insert into userinfo values('女','123456789','500233103620131023','汉族','南京','/style/img/tx.png')
insert into userinfo values('男','123456789','500233103620131023','汉族','中国','/style/img/tx.png')
insert into userinfo values('女','123456789','500233103620131023','汉族','上海','/style/img/tx.png')
insert into userinfo values('男','123456789','500233103620131023','汉族','广州','/style/img/tx.png')
go

select *from userinfo
go
create table staffInfo
(
	empId int primary key,
	empName char(20) not null,
	empPosition char(20) not null,
	empHrId int null,
	empInDate date not null,
	empSalary decimal(10,2) not null,
	empBonus decimal(10,2) null,
	deptId int 
);
insert into staffInfo values(7369, '任盈盈', '职员', 7902, '1980-12-17', 800, NULL, 20);
insert into staffInfo values(7499, '杨逍', '销售人员', 7698, '1981-2-20', 1600, 300, 30);
insert into staffInfo values(7521, '范遥', '销售人员', 7698, '1981-2-22', 1250, 500, 30);
insert into staffInfo values(7566, '任我行', '经理', 7839, '1981-4-2', 2975, NULL, 20);
insert into staffInfo values(7654, '金毛狮王', '销售人员', 7698, '1981-9-28', 1250, 1400, 30);
insert into staffInfo values(7698, '张无忌', '经理', 7839, '1981-5-1', 2850, NULL, 30);
insert into staffInfo values(7782, '苏荃', '经理', 7839, '1981-6-9', 2450, NULL, 10);
insert into staffInfo values(7788, '东方不败', '分析员', 7566, '1982-12-9', 3000, NULL, 20);
insert into staffInfo values(7839, '韦小宝', '总裁', NULL, '1981-11-17', 5000, NULL, 10);
insert into staffInfo values(7844, '紫衫龙王', '销售人员', 7698, '1981-9-8', 1500, 0, 30);
insert into staffInfo values(7876, '向问天', '职员', 7788, '1983-1-12', 1100, NULL, 20);
insert into staffInfo values(7900, '小昭', '职员', 7698, '1981-12-3', 950, NULL, 30);
insert into staffInfo values(7902, '令狐冲', '分析员', 7566, '1981-12-3', 3000, NULL, 20);
insert into staffInfo values(7934, '双儿', '职员', 7782, '1982-1-23', 1300, NULL, 10);
insert into staffInfo values(7933, '石中玉', '销售人员', 7782, '1984-12-23', 2850, NULL, 10);


select * from staffInfo

go
create  table customer(
customerid varchar(50) primary key,
companyname varchar(50),
connectname varchar(50),
addresss varchar(50),
zipcode varchar(50),
telephone int,)
go
insert into customer values('c01','东南商贸','张先生','西湖路275号','215000','501206')
insert into customer values('c02','西多商贸','王小姐','扬子西路182号','225000','501206')
insert into customer values('c03','大恒贸易','陈先生','淮海中路210号','225000','501206')
insert into customer values('c04','海达商贸','李先生','通江北路316号','215000','501206')
select *from customer
go
create  table seller(
saleid varchar(3),
salename varchar(50),
sex   char(2) check(sex='男'or sex='女'),
brithday datetime,
hiredate datetime,
addresss varchar(50),
telephone varchar(43),)
go
insert into seller values('s01','王强','男','1975-12-08','2002-02-01','蓝色港湾42-12','0519-85150900')
insert into seller values('s02','付芳芳','女','1975-12-08','2008-08-17','燕阳花园53-4','0519-85150900')
insert into seller values('s03','李芳','女','1983-08-30','2008-08-17','富都小区252-16','0519-85150900')
insert into seller values('s04','胡宝林','男','1991-09-19','2014-04-17','燕兴小区79-42','0519-85150900')
insert into seller values('s05','吴韵','男','1979-12-08','2008-11-15','浮城花园42-12','0519-85150900')
insert into seller values('s06','陆海成','男','1990-03-22','2014-04-11','都市雅居3-2','0519-85150900')
insert into seller values('s07','刘洋','男','1988-12-06','2012-12-03','顺元八村59-6','0519-85150900')
insert into seller values('s08','吴永佳','男','1985-07-10','2012-10-23','顺元三村42-12','0519-85150900')
select *from  seller

go
create table Training(
id int primary key identity(1,1),
people varchar(100),
theme varchar(100),
Training_Time date,
place   varchar(100),
)
go
insert into Training values('总经理','员工法则','2019-10-10','会议室')
insert into Training values('总经理','管理方法','2019-9-10','会议室')
insert into Training values('部门经理','出勤管理','2019-8-10','会议室')
insert into Training values('财务经理','薪资调配','2019-7-10','会议室')
insert into Training values('人事经理','招贤纳士','2019-6-10','会议室')
insert into Training values('后勤经理','供给输出','2019-5-10','会议室')
insert into Training values('董事长','公司发展问题','2019-1-10','行政室')
go
select * from Training
go
create table test(
id int primary key ,
people varchar(100),
)
go
insert into test values('1','供给输出')
insert into test values('2','公司发展问题')
insert into test values('3','公司发展问题')
insert into test values('4','公司发展问题')
select * from test


CREATE TABLE Checks (
"id"  int primary key NOT NULL,
"name"  varchar(255),
"danhao"  varchar(255),
"style"  varchar(255),
"didian"  varchar(255),
"danjia"  varchar(255),
"shulu"  varchar(255),
)
go
insert into Checks values('1','巡逻船1号','MJSA31568433519','2020-3-20','重庆码头','123','155')
insert into Checks values('2','巡逻船2号','MJSA31568433519','2020-3-22','重庆码头','123','15')
insert into Checks values('3','巡逻船3号','MJSA31568433519','2020-3-22','重庆码头','123','143')
insert into Checks values('4','巡逻船4号','MJSA31568433519','2020-3-23','重庆码头','123','125')
insert into Checks values('5','巡逻船5号','MJSA31568433519','2020-3-24','重庆码头','123','96')
insert into Checks values('6','巡逻船6号','MJSA31568433519','2020-3-25','重庆码头','123','49')
insert into Checks values('7','巡逻船7号','MJSA31364233519','2020-3-22','重庆码头','123','15')
insert into Checks values('8','巡逻船8号','MJSA74138433519','2020-3-22','重庆码头','123','13')
insert into Checks values('9','巡逻船9号','MJSA74123633519','2020-3-23','重庆码头','123','125')
insert into Checks values('10','巡逻船10号','MJSA85230433519','2020-3-24','重庆码头','123','196')
insert into Checks values('11','巡逻船11号','MJSA7856433519','2020-3-25','重庆码头','123','149')
select * from Checks
go
CREATE TABLE Checking (
"id"  int primary key NOT NULL,
"name"  varchar(255),
"danhao"  varchar(255),
"style"  varchar(255),
"didian"  varchar(255),
"danjia"  varchar(255),
"shulu"  varchar(255),
)
go
insert into Checking values('1','巡逻船1号','MJSA31568433519','2020-3-20','重庆码头','长江游轮','0#柴油')
insert into Checking values('2','巡逻船2号','MJSA31568433519','2020-3-22','重庆码头','长江游轮','15#航空柴油')
insert into Checking values('3','巡逻船3号','MJSA31568433519','2020-3-22','重庆码头','长江游轮','0#柴油')
insert into Checking values('4','巡逻船4号','MJSA31568433519','2020-3-23','重庆码头','长江游轮','15#航空柴油')
insert into Checking values('5','巡逻船5号','MJSA31568433519','2020-3-24','重庆码头','长江游轮','0#柴油')
insert into Checking values('6','巡逻船6号','MJSA31568433519','2020-3-25','重庆码头','长江游轮','15#航空柴油')
insert into Checking values('7','巡逻船7号','MJSA31364233519','2020-3-22','重庆码头','长江游轮','0#柴油')
insert into Checking values('8','巡逻船8号','MJSA74138433519','2020-3-22','重庆码头','长江游轮','15#航空柴油')
insert into Checking values('9','巡逻船9号','MJSA74123633519','2020-3-23','重庆码头','长江游轮','0#柴油')
insert into Checking values('10','巡逻船10号','MJSA85230433519','2020-3-24','重庆码头','长江游轮','15#航空柴油')
insert into Checking values('11','巡逻船11号','MJSA7856433519','2020-3-25','重庆码头','长江游轮','0#柴油')
select * from Checking
go
create table mar(
Id int identity(1,1) primary key,
Cats varchar(500),
Name varchar(200),
moneys varchar(200))
go 
insert into mar values('修复系统BUg','李达','1200元') 
insert into mar values('修复系统BUg','李凯','1200元')
insert into mar values('修复系统BUg','王梅','1200元')