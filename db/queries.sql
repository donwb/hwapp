select * from mapuser;

select * from actions;

select * from useractions;

select mu.email, a.[action], a.points from useractions
	JOIN mapuser as mu
	ON useractions.Userid	= mu.id
	JOIN actions as a
	ON useractions.actionsid	= a.id;
	
select * from vwActivity where email LIKE 'don.browning@turner.com';

	
