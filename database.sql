create database care;
use care;
drop database care;
create table Elder(
	eId char(10) not null,
    eName char(50) not null,
    eGender char(10),
    eBirth date not null,
    eIdCard char(10) not null,
    ePhone char(10),
    eAddress char(50),
    eHeight int,
    eWeight int,
    #rName char(10) not null,
    #rPhone int not null,
    eCreateFile date not null,
    
    #fId char(10),
    #hId char(10),
    pId char(10),#房號
    cId char(10),
    primary key(eId),
    foreign key(cId)references Carer(cId),
    unique key(eIdCard,ePhone)
);
ALTER TABLE Elder ADD foreign key (pId)references Place(pId) on update cascade;

create table Carer(
	cId char(10) not null,
	cName char(50) not null,
	cGender char(10),
	cBirth date not null,
	cIdCard char(10) not null,
	cPhone char(10) not null,
	cAddress char(50) not null,
	cSalary int not null, #要刪掉
	#eId char(10),

	primary key(cId),
	#foreign key(eId) references Elder(eId)on update cascade,
	unique key(cIdCard,cPhone)
);

create table Place(
pId char(10) not null,
pName char(20) not null,
pSize int,

primary key(pId),
unique key(pName)
);

create table Hospital(
	hId char(10) not null,
	hName char(50) not null, #院名
	hTime datetime not null,
	hPlace char(50) not null,
	hDepartment char(50) not null,
	hDrName char(50) not null,
	eId char(10) not null,

	primary key(hId),
	foreign key(eId) references Elder(eId)on update cascade on delete cascade
);


create table Relative(
	rName char(50) not null,
	rPhone char(10) not null,
	rGender char(10),
	rIdCard char(10) not null,
	rAddress char(50) not null,
	rJob char(50),
	rWorkPlace char(50),

	eId char(10) not null,
	primary key(rName,rPhone),
	foreign key(eId)references Elder(eId)on update cascade on delete cascade
);

create table Fall(
	fId int auto_increment primary key,#自己新增主鍵
	fTime datetime not null,
	fWhy char(50) not null, #原因
	fCount int not null default 0,#跌倒次數，預設為0
	
	eId char(10) not null,
	hId char(10),
	pId char(10) not null,
	#primary key(fId),

	foreign key(eId)references Elder(eId)on update cascade,
	foreign key(hId)references Hospital(hId)on update cascade,
	foreign key(pId)references Place(pId)on update cascade
);
create table Equipment(
	EqId char(10) not null,
	EqModel char(50) not null,#設備型號
	EqRepair date not null,#檢修日期

	pId char(10),
	primary key(EqId),
	foreign key(pId)references Place(pId)on update cascade
);

CREATE TABLE CarerLogin(
	cAccount char(20) not null,
    cPassword char(30) not null,
    cId char(10) not null,
    cEmail char(30) not null,
    
    foreign key(cId)references Carer(cId)
);


CREATE TABLE HomeLogin(
	homeUserName char(50) not null,
	homeRealName char(50),
    homePhone char(10),
    homeGender char(10),
	homeAddress char(50),
    homeEmail char(30) not null,
    homePassword char(30) not null,
    
    primary key(homeUserName)
);

CREATE TABLE HomeElder(
	heName char(50) not null,
    heGender char(10),
    heBirth date not null,
    heIdCard char(10) not null,
    hePhone char(10),
    heAddress char(50),
    heHeight int,
    heWeight int,
    
    homeUserName  char(50) not null, 
    primary key(heName),
    foreign key(homeUserName)references HomeLogin(homeUserName)
);
CREATE TABLE HomePlace(
	hPlace char(10) not null,
    primary key(hPlace)
);
CREATE TABLE HomeElderFall(
    hfId int auto_increment primary key,
    hfTime datetime not null,
    hfWhy char(50) not null,
    hfCount int not null default 0,
    
    heName char(10),
    homeUserName char(50) not null,
    hPlace char(10) not null,
    foreign key(heName) references HomeElder(heName) on update cascade,
    foreign key(homeUserName) references HomeLogin(homeUserName) on update cascade,
    foreign key(hPlace) references HomePlace(hPlace) on update cascade
);

table Carer;
table Elder;
table Relative;
table Place;
table CarerLogin;
table Fall;
table Hospital;
UPDATE Place SET pName = '大廳' WHERE pName = 'A101';
UPDATE Place SET pName = '交誼廳' WHERE pName = '玄關';

table HomeLogin;
table HomeElder;
table HomePlace;
table HomeElderFall;
DELETE FROM HomeLogin

INSERT INTO HomeElder VALUES ('阿公', 'male', '1940-05-15', 'A123456789', '0912345678', '台北市中山區', 165, 70, '貴');
INSERT INTO HomePlace (hPlace) VALUES ('客廳');
INSERT INTO HomePlace (hPlace) VALUES ('廚房');
INSERT INTO HomePlace (hPlace) VALUES ('浴室');
INSERT INTO HomeElderFall (hfTime, hfWhy, homeUserName, hPlace)
VALUES ('2024-09-01 10:00:00', '滑倒', '貴', '客廳');

INSERT INTO HomeElderFall (hfTime, hfWhy, homeUserName, hPlace)
VALUES ('2024-09-02 12:30:00', '絆倒', '貴', '廚房');

INSERT INTO HomeElderFall (hfTime, hfWhy, homeUserName, hPlace)
VALUES ('2024-09-03 08:15:00', '滑倒', '貴', '浴室');

INSERT INTO HomeElderFall (hfTime, hfWhy, homeUserName, hPlace)
VALUES ('2024-09-05 12:30:00', '前倒', 'wei', '玄關');


#創建順序 房間=管理員>長者>家屬>跌倒>紀錄
#居家管理員>居家老人>居家老人跌倒

insert into Place value('P01','A101','100');
insert into Place value('P02','A102','100');

insert into Carer value("C01","管理員","female","1999-11-11","E123456789","0987654332","taiwan",25000);
insert into Carer value("C02","管理員2","male","1993-2-11","E14556789","0982444332","taiwan",25000);

insert into Elder value('M01','阿公','male','1950-01-01','A123456789',0912345678,'taiwan',173,64,'2020-11-11','P01','C01');
insert into Elder value('M02','阿嬤','female','1950-01-01','A123456783',0912345673,'taiwan',170,60,'2020-11-11','P02','C01');
insert into Elder value('M03','阿嬤嬤','female','1940-03-31','A123411113',0912333333,'taiwan',160,61,'2020-11-11','P02','C01');
insert into Elder value('M04','阿公公','male','1933-10-31','F123421113',0912213333,'taiwan',178,71,'2020-11-11','P01','C02');