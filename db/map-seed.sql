
INSERT INTO mapuser (email) VALUES ('don.browning@turner.com')
INSERT INTO mapuser (email) VALUES ('mickey.yawn@turner.com')


INSERT INTO actions (action, points) VALUES ('Take a Quiz in Cloud Academy', 1);
INSERT INTO actions (action, points) VALUES ('Attend cloud architecture office Hours', 1);
INSERT INTO actions (action, points) VALUES ('Take a Course in Cloud Academy or some related training site', 2);
INSERT INTO actions (action, points) VALUES ('Get a Certification in Cloud Academy', 3);

INSERT INTO useractions (userid, actionsid, actiondate) VALUES (1, 1, GETDATE());
INSERT INTO useractions (userid, actionsid, actiondate) VALUES (1, 2, GETDATE());
INSERT INTO useractions (userid, actionsid, actiondate) VALUES (1, 4, GETDATE());
INSERT INTO useractions (userid, actionsid, actiondate) VALUES (2, 2, GETDATE());
INSERT INTO useractions (userid, actionsid, actiondate) VALUES (2, 3, GETDATE());

select * from actions;