select * from fulfillment_house
where ful_name like 'CDS%'

select * from fulfillment_house
where ful_name like '%Faculty%' or ful_name like '%staff%'

select * from qspcanadaordermanagement..interfacelayout
where InterfaceLayoutID in (33006,33007)

begin tran t1
select * 
--update fh
--set InterfaceLayoutID = 33007
--update fh
--set IsCancelFileReqd = 'Y'
from fulfillment_house fh
where ful_Nbr = 232
commit tran t1

select * from product
where fulfill_house_Nbr = 232