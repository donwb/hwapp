-- Tables
create table mapuser
(
	id int identity(1,1) primary key,
	email text not null
)

create table actions
(
	id int identity(1,1) primary key,
	action text NOT NULL,
	points int NOT NULL
)

create table useractions
(
	id int identity(1,1) primary key,
	userid int NOT NULL,
	actionsid int NOT NULL,
 	actiondate datetime	NOT NULL
)

-- FKs
ALTER TABLE useractions
ADD CONSTRAINT FK_UserActions_Mapid FOREIGN KEY (userid)
    REFERENCES mapuser(id);
    
ALTER TABLE useractions
ADD CONSTRAINT FK_UserActions_ActionID FOREIGN KEY (actionsid)
    REFERENCES actions(id);

CREATE VIEW vwActivity AS
select mu.email, a.[action], a.points from useractions
	JOIN mapuser as mu
	ON useractions.Userid	= mu.id
	JOIN actions as a
	ON useractions.actionsid	= a.id;
	
