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
insert into userChecke values('����','admin','123456','����')
insert into userChecke values('����','������Happy','123456','����')
insert into userChecke values('ǳ����ĭ','sunshine','123456','����')
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
insert into department values('������Դ��','2013-12-03','12','�������¹���')
insert into department values('C#��','2013-12-03','23','�����д����')
insert into department values('������','2013-12-03','12','�����д��������')
insert into department values('PHP��','2013-12-03','14','PHP��ҳ')
insert into department values('ASP��','2013-12-03','13','ASP��ҳ')
insert into department values('H5��','2013-12-03','14','H5��ҳ')
insert into department values('ASP.NET��','2013-12-03','13','ASP.NET��ҳ')
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
insert into userinfo values('��','123456789','500233103620131023','����','����','/style/img/tx.png')
insert into userinfo values('Ů','123456789','500233103620131023','����','�Ͼ�','/style/img/tx.png')
insert into userinfo values('��','123456789','500233103620131023','����','�й�','/style/img/tx.png')
insert into userinfo values('Ů','123456789','500233103620131023','����','�Ϻ�','/style/img/tx.png')
insert into userinfo values('��','123456789','500233103620131023','����','����','/style/img/tx.png')
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
insert into staffInfo values(7369, '��ӯӯ', 'ְԱ', 7902, '1980-12-17', 800, NULL, 20);
insert into staffInfo values(7499, '����', '������Ա', 7698, '1981-2-20', 1600, 300, 30);
insert into staffInfo values(7521, '��ң', '������Ա', 7698, '1981-2-22', 1250, 500, 30);
insert into staffInfo values(7566, '������', '����', 7839, '1981-4-2', 2975, NULL, 20);
insert into staffInfo values(7654, '��ëʨ��', '������Ա', 7698, '1981-9-28', 1250, 1400, 30);
insert into staffInfo values(7698, '���޼�', '����', 7839, '1981-5-1', 2850, NULL, 30);
insert into staffInfo values(7782, '����', '����', 7839, '1981-6-9', 2450, NULL, 10);
insert into staffInfo values(7788, '��������', '����Ա', 7566, '1982-12-9', 3000, NULL, 20);
insert into staffInfo values(7839, 'ΤС��', '�ܲ�', NULL, '1981-11-17', 5000, NULL, 10);
insert into staffInfo values(7844, '��������', '������Ա', 7698, '1981-9-8', 1500, 0, 30);
insert into staffInfo values(7876, '������', 'ְԱ', 7788, '1983-1-12', 1100, NULL, 20);
insert into staffInfo values(7900, 'С��', 'ְԱ', 7698, '1981-12-3', 950, NULL, 30);
insert into staffInfo values(7902, '�����', '����Ա', 7566, '1981-12-3', 3000, NULL, 20);
insert into staffInfo values(7934, '˫��', 'ְԱ', 7782, '1982-1-23', 1300, NULL, 10);
insert into staffInfo values(7933, 'ʯ����', '������Ա', 7782, '1984-12-23', 2850, NULL, 10);


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
insert into customer values('c01','������ó','������','����·275��','215000','501206')
insert into customer values('c02','������ó','��С��','������·182��','225000','501206')
insert into customer values('c03','���ó��','������','������·210��','225000','501206')
insert into customer values('c04','������ó','������','ͨ����·316��','215000','501206')
select *from customer
go
create  table seller(
saleid varchar(3),
salename varchar(50),
sex   char(2) check(sex='��'or sex='Ů'),
brithday datetime,
hiredate datetime,
addresss varchar(50),
telephone varchar(43),)
go
insert into seller values('s01','��ǿ','��','1975-12-08','2002-02-01','��ɫ����42-12','0519-85150900')
insert into seller values('s02','������','Ů','1975-12-08','2008-08-17','������԰53-4','0519-85150900')
insert into seller values('s03','�','Ů','1983-08-30','2008-08-17','����С��252-16','0519-85150900')
insert into seller values('s04','������','��','1991-09-19','2014-04-17','����С��79-42','0519-85150900')
insert into seller values('s05','����','��','1979-12-08','2008-11-15','���ǻ�԰42-12','0519-85150900')
insert into seller values('s06','½����','��','1990-03-22','2014-04-11','�����ž�3-2','0519-85150900')
insert into seller values('s07','����','��','1988-12-06','2012-12-03','˳Ԫ�˴�59-6','0519-85150900')
insert into seller values('s08','������','��','1985-07-10','2012-10-23','˳Ԫ����42-12','0519-85150900')
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
insert into Training values('�ܾ���','Ա������','2019-10-10','������')
insert into Training values('�ܾ���','������','2019-9-10','������')
insert into Training values('���ž���','���ڹ���','2019-8-10','������')
insert into Training values('������','н�ʵ���','2019-7-10','������')
insert into Training values('���¾���','������ʿ','2019-6-10','������')
insert into Training values('���ھ���','�������','2019-5-10','������')
insert into Training values('���³�','��˾��չ����','2019-1-10','������')
go
select * from Training