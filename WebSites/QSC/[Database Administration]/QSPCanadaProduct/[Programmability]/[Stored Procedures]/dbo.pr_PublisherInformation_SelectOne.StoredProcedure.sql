USE [QSPCanadaProduct]
GO
/****** Object:  StoredProcedure [dbo].[pr_PublisherInformation_SelectOne]    Script Date: 06/07/2017 09:17:58 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[pr_PublisherInformation_SelectOne]

@iPublisherID int,
@zUMC varchar(4)

 AS

DECLARE @count int

select 	@count = count(*)
	
from 	publishers p,
	publisher_contacts pc
where 	p.pub_nbr=@iPublisherID
	and p.pub_nbr=pc.pub_nbr
	--and product_code = @zUMC

if @count =0
begin

select 	p.pub_nbr,
	p.pub_name,
	p.pub_addr_1,
	coalesce(p.pub_addr_2, '') as pub_addr_2,
	p.pub_city,
	p.pub_state,
	p.pub_zip,
	p.pub_countrycode,
	pc.pcontact_fname,
	pc.pcontact_lname,
	coalesce(pc.PContact_Title, '') as Position,
	coalesce(pc.PContact_Email, '') as email,
	coalesce(wp.PhoneNumber, '') as workphone,
	coalesce(fp.PhoneNumber, '') as faxnbr
	
from 	publishers p LEFT OUTER JOIN
	publisher_contacts pc ON p.pub_nbr=pc.pub_nbr LEFT OUTER JOIN
	phone wp ON pc.PhoneListID = wp.PhoneListID AND wp.Type = 1 LEFT OUTER JOIN
	phone fp ON pc.PhoneListID = fp.PhoneListID AND fp.Type = 3
	
where 	p.pub_nbr=@iPublisherID
	--and product_code = @zUMC
order by pc.pcontact_instance desc
end
else
begin
select 	top 1 p.pub_nbr,
	p.pub_name,
	p.pub_addr_1,
	coalesce(p.pub_addr_2, '') as pub_addr_2,
	p.pub_city,
	p.pub_state,
	p.pub_zip,
	p.pub_countrycode,
	pc.pcontact_fname,
	pc.pcontact_lname,
	coalesce(pc.PContact_Title, '') as Position,
	coalesce(pc.PContact_Email, '') as email,
	coalesce(wp.PhoneNumber, '') as workphone,
	coalesce(fp.PhoneNumber, '') as faxnbr
	
from 	publishers p,
	publisher_contacts pc LEFT OUTER JOIN
	phone wp ON pc.PhoneListID = wp.PhoneListID AND wp.Type = 1 LEFT OUTER JOIN
	phone fp ON pc.PhoneListID = fp.PhoneListID AND fp.Type = 3
where 	p.pub_nbr=@iPublisherID
	and p.pub_nbr=pc.pub_nbr
	--and product_code = @zUMC
order by pc.pcontact_instance desc
end
GO
