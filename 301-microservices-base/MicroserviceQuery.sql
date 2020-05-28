select * from tblRestaurant where ID =3;
select * from tblRestaurantDetails where tblRestaurantID = 3;
select * from tblOffer where tblRestaurantID = 3;
select * from tblMenu where ID=310;
select * from tblCuisine where ID = 703;

select * from tblFoodOrder;    
select * from tblFoodOrderMapping;
select * from tblFoodCart; 
select * from tblFoodCartMapping;
select * from tblOrderPayment;

select * from  [dbo].[tblOrderStatus];
select * from [dbo].[tblPaymentType];
select * from [dbo].[tblPaymentStatus];
--delete from  [dbo].[tblPaymentStatus];

insert into [dbo].[tblOrderStatus](Status, UserCreated, UserModified,RecordTimeStamp,RecordTimeStampCreated) values('Initiated',123456,0,getDate(),getDate());
insert into [dbo].[tblOrderStatus](Status, UserCreated, UserModified,RecordTimeStamp,RecordTimeStampCreated) values('Processing',123456,0,getDate(),getDate());
insert into [dbo].[tblOrderStatus](Status, UserCreated, UserModified,RecordTimeStamp,RecordTimeStampCreated) values('Success',123456,0,getDate(),getDate());
insert into [dbo].[tblOrderStatus](Status, UserCreated, UserModified,RecordTimeStamp,RecordTimeStampCreated) values('Failure',123456,0,getDate(),getDate());

insert into [dbo].[tblPaymentType](Type, UserCreated, UserModified,RecordTimeStamp,RecordTimeStampCreated) values('Cash',123456,0,getDate(),getDate());
insert into [dbo].[tblPaymentType](Type, UserCreated, UserModified,RecordTimeStamp,RecordTimeStampCreated) values('Debit',123456,0,getDate(),getDate());
insert into [dbo].[tblPaymentType](Type, UserCreated, UserModified,RecordTimeStamp,RecordTimeStampCreated) values('Credit',123456,0,getDate(),getDate());
insert into [dbo].[tblPaymentType](Type, UserCreated, UserModified,RecordTimeStamp,RecordTimeStampCreated) values('NoPayment',123456,0,getDate(),getDate());

insert into [dbo].[tblPaymentStatus]( Status, UserCreated,UserModified,RecordTimeStamp,RecordTimeStampCreated) values('Success',123456,0,getDate(),getDate());
insert into [dbo].[tblPaymentStatus]( Status, UserCreated,UserModified,RecordTimeStamp,RecordTimeStampCreated) values('Failed',123456,0,getDate(),getDate());

DBCC CHECKIDENT ('[dbo].[tblPaymentStatus]', RESEED, 0);
DBCC CHECKIDENT ('[dbo].[tblPaymentStatus]');