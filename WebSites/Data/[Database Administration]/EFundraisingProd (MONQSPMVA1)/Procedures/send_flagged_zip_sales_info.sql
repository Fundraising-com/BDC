USE [eFundraisingProd]
GO

/****** Object:  StoredProcedure [dbo].[send_flagged_zip_sales_info]    Script Date: 01/26/2016 07:34:28 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


-- =================================================
-- Author:		Drew Pettit
-- Create date: 2015-01-26
-- Description:	Send report with xp_smtp_sendmail
-- exec [send_flagged_zip_sales_info]
-- =================================================
CREATE PROCEDURE [dbo].[send_flagged_zip_sales_info]
AS
BEGIN

	declare @report varchar(8000)
	declare @source_id int
	declare @project_id int
	declare @subject varchar(100)
	declare @to_name varchar(100)
	declare @to_email varchar(100)
	DECLARE @lead_id int
	DECLARE @organization VARCHAR(100)
	DECLARE @sales_id int
	DECLARE @scratch_book_id int
	DECLARE @description varchar(100)
	DECLARE @sales_amount decimal (7,2)
	DECLARE @sales_date datetime
	declare @rc int

	set @report = 'The following list of sales have occurred in an area which has been flagged as a GAO sales rep area in the past 7 days<br><br><br>'
	
DECLARE c CURSOR FOR 
	select l.lead_id, ISNULL(l.organization,'NULL'),s.sales_id,s.sales_date, sb.scratch_book_id,sb.description,si.sales_amount
	from sale s
	join sales_item si on si.sales_id = s.sales_id
	join scratch_book sb on sb.scratch_book_id = si.scratch_book_id
	join lead l on l.lead_id = s.lead_id
	where l.zip_code in 
	('33056','60153','60408','60416','60419','60424','60444','60447','60450','60460','60518','60531','60549','60551','60563','60953','60957','60959','60966','60968','60970','61201','61231','61232','61234','61238','61240','61241','61244','61254','61256','61259','61263','61264','61265','61273','61275','61279','61281','61282','61284','61301','61315','61317','61319','61320','61322','61325','61326','61327','61329','61330','61335','61337','61338','61341','61342','61348','61350','61354','61356','61360','61362','61364','61368','61369','61370','61373','61374','61375','61376','61377','61379','61413','61414','61418','61421','61422','61434','61443','61460','61462','61467','61480','61483','61486','61490','61491','61501','61517','61523','61525','61529','61530','61531','61533','61534','61537','61540','61546','61548','61550','61552','61554','61559','61564','61565','61568','61570','61571','61603','61604','61605','61606','61607','61610','61611','61614','61615','61616','61729','61733','61734','61738','61739','61740','61741','61742','61743','61744','61755','61759','61760','61764','61769','62022','62031','62037','62052','62301','62305','62320','62339','62347','62351','62360','62376','62601','62631','62638','62644','62650','62664','62692')
	and s.sales_date > GETDATE()-7

OPEN c;
FETCH NEXT FROM c INTO @lead_id, @organization, @sales_id, @sales_date, @scratch_book_id, @description, @sales_amount
WHILE(@@FETCH_STATUS = 0)
BEGIN
	
	--,@to = 'Pettit, Drew - QSP <drew_pettit@qsp.com>; Alcindor, Marc - QSP <marc_alcindor@qsp.com>; Cote, Melissa - QSP <melissa_cote@qsp.com>'

	SET @report =  @report 
			+ '<table><tr><td colspan=2>' 
			+ '================ SALE GENERATED IN GA REP ZIP CODE ================================</td><td></tr>' 
			+ '<tr><td>Lead ID:</td><td>' + CAST(@lead_id as varchar(10)) + '</td></tr>'
			+ '<tr><td>Lead Name:</td><td>' + @organization + '</td></tr>'
			+ '<tr><td>Sale ID:</td><td>' + CAST(@sales_id as varchar(10)) + '</td></tr>' 
			+ '<tr><td>Sale Date:</td><td>' + CAST(@sales_date as varchar(20)) + '</td></tr>'
			+ '<tr><td>Scratch Book ID:</td><td>' + CAST(@scratch_book_id as varchar(10)) + '</td></tr>'
			+ '<tr><td>Scratch Book item:</td><td>' + @description + '</td></tr>'
			+ '<tr><td>Sales Amount:</td><td>' + CAST(@sales_amount as VARCHAR(10)) + '</td></tr>'
			+ '<tr><td></td></tr></table>'
		
	FETCH NEXT FROM c INTO @lead_id, @organization, @sales_id, @sales_date, @scratch_book_id, @description, @sales_amount

END

CLOSE c;
DEALLOCATE c;

	EXEC @rc = msdb.dbo.sp_send_dbmail  
      @profile_name = NULL
     --, @recipients = 'jason.farrell@fundraising.com;Javier.Arellano@fundraising.com'
     --, @recipients = 'BDCDevelopers@fundraising.com;xavier.desaunettes@fundraising.com;marc.alcindor@fundraising.com;dpettit@gafundraising.com;sadday.Zivec@fundraising.com;jason.farrell@fundraising.com;Javier.Arellano@fundraising.com'
     
      , @recipients = 'BDCDevelopers@fundraising.com;bdc@gafundraising.com'
      --, @recipients = 'dpettit@gafundraising.com'
      , @subject = '[FUNDRAISING] Flagged Sales Report (Past 7 Days) - GA Rep Zip Codes'
      , @body = @report
      , @body_format = 'HTML'
      , @from_address = 'efrreporting@fundraising.com'
      , @reply_to = 'dpettit@gafundraising.com'
	
		
	--print @report 
	if (@@error <> 0 or @rc <> 0)
		raiserror(N'Sending message using xp_smtp_sendmail failed', 16, 1)

	
	return @rc


END

GO


