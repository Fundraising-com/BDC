USE [esubs_global_v2]
GO
/****** Object:  StoredProcedure [dbo].[es_rpt_search_event_2]    Script Date: 02/14/2014 13:06:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Because of the performance issue, this stored procedure is built dynamically according to the parameters passsed to it BUT
-- it is therefore vulnerable of SQL INJECTION ATTACK. To minimize the attack,  the parameters must be checked (To eliminate the SQL injection) before passing them to this stored procedure.

--Created by Dat Nghiem on May-07-2006.

CREATE PROCEDURE [dbo].[es_rpt_search_event_2] (
@SearchEventName nvarchar(100),  
@SearchEventID int,
@MemberEACriteria nvarchar(100),
@MemberMNCriteria  nvarchar(100),
@EventLID int,
@EventGN nvarchar(100),
@EventCN int,
@DateFirstDay datetime,
@DateSecondDay datetime,
@SelectTop varchar(20)
) AS
BEGIN


-- Main SQL statement.
DECLARE @SQLStatement VARCHAR(8000)

	IF (@SelectTop IS NULL OR Ltrim(@SelectTop) = '')
	set @SQLStatement = 
		'SELECT TOP 5000
				e.event_id as EventID,
				e.event_name as EventName, 
				e.active as EventActive,
				m.email_address as SponsorEmailAddress,
				Isnull(m.first_name,'''') + Isnull(m.middle_name,'''') + Isnull(m.last_name,'''') as SponsorName,
				par.partner_name as PartnerName,
				g.lead_id as LeadID,
				p.subdivision_code,
				sub.subdivision_name_1 as PaymentState,
				g.group_name,
				e.create_date,
				e.start_date,
				e.end_date,
				g.group_id,
				g.external_group_id
			FROM event e
				INNER JOIN event_group eg
					on e.event_id = eg.event_id
				INNER JOIN [group] g
					on eg.group_id = g.group_id
				INNER JOIN member_hierarchy mh
					on mh.member_hierarchy_id = g.sponsor_id
				INNER JOIN member m
					on mh.member_id = m.member_id
				INNER JOIN partner par ON par.partner_id = g.partner_id 
				LEFT OUTER JOIN payment_info pi on g.group_id = pi.group_id
				LEFT OUTER JOIN payment p on pi.payment_info_id = p.payment_info_id
				LEFT OUTER JOIN subdivision sub on sub.subdivision_code = p.subdivision_code						
			WHERE (1=1)  '
	ELSE
	BEGIN
	set @SQLStatement = 
		'SELECT TOP ' + @SelectTop + ' e.event_id as EventID,
				e.event_name as EventName, 
				e.active as EventActive,
				m.email_address as SponsorEmailAddress,
				Isnull(m.first_name,'''') + Isnull(m.middle_name,'''') + Isnull(m.last_name,'''') as SponsorName,
				par.partner_name as PartnerName,
				g.lead_id as LeadID,
				p.subdivision_code,
				sub.subdivision_name_1 as PaymentState,
				g.group_name,
				e.create_date,
				e.start_date,
				e.end_date,
				g.group_id,
				g.external_group_id
			FROM event e
				INNER JOIN event_group eg
					on e.event_id = eg.event_id
				INNER JOIN [group] g
					on eg.group_id = g.group_id
				INNER JOIN member_hierarchy mh
					on mh.member_hierarchy_id = g.sponsor_id
				INNER JOIN member m
					on mh.member_id = m.member_id
				INNER JOIN partner par ON par.partner_id = g.partner_id 
				LEFT OUTER JOIN payment_info pi on g.group_id = pi.group_id
				LEFT OUTER JOIN payment p on pi.payment_info_id = p.payment_info_id
				LEFT OUTER JOIN subdivision sub on sub.subdivision_code = p.subdivision_code						
			WHERE (1=1)  '
	END

		
	IF (@SearchEventName IS NOT NULL AND LTrim(@SearchEventName) <> '')
	BEGIN
		set @SQLStatement = @SQLStatement + '  AND (e.event_name  LIKE ''%' +  @SearchEventName + '%'')  ' 
	END
		
	IF (@SearchEventID IS NOT NULL)
		set @SQLStatement = @SQLStatement + '  AND (e.event_id='  + cast(@SearchEventID as varchar(38)) + ')  '

	IF (@EventLID IS NOT NULL)
		set @SQLStatement = @SQLStatement + '  AND (g.lead_id =' + cast(@EventLID as varchar(38)) + ') '
	IF (@EventGN IS NOT NULL) 
		set @SQLStatement = @SQLStatement + '  AND (g.group_name  LIKE ''%' +  @EventGN + '%'') ' 
	IF (@EventCN IS NOT NULL)
		set @SQLStatement = @SQLStatement + '  AND (p.cheque_number =' + cast(@EventCN as varchar(38)) + ') '

	
	IF (@DateFirstDay IS NOT NULL AND @DateSecondDay IS NOT NULL AND @DateFirstDay <= @DateSecondDay) -- The started day is between the first and second day.
	BEGIN
		--set @SQLStatement = @SQLStatement + ' AND (1=0) '
		set @SQLStatement = @SQLStatement + '  AND  e.start_date >= convert(datetime,''' + replace(convert(varchar(40), @DateFirstDay, 102),'.', '-') + ' 00:00:00' + ''',' + '20) ' 
		--set @SQLStatement = @SQLStatement + '  AND  e.start_date <= convert(datetime,''' + replace(convert(varchar(40), @DateSecondDay, 102),'.', '-') + ' 23:59:59' + ''',' + '20) ' 
	END
	



	IF ( @MemberEACriteria IS NOT NULL)
	BEGIN
	set @SQLStatement = @SQLStatement + '   AND e.event_id in (select distinct e.event_id 
			from event e
			inner join event_participation ep
			on e.event_id = ep.event_id
			inner join member_hierarchy mh
			on mh.member_hierarchy_id = ep.member_hierarchy_id
			inner join member m
			on mh.member_id = m.member_id where m.email_address LIKE ''%' + @MemberEACriteria + '%'')   '
	END
	
	IF (@MemberMNCriteria IS NOT NULL)
	BEGIN
	set @SQLStatement = @SQLStatement +  '  AND e.event_id in (select distinct e.event_id 
			from event e
			inner join event_participation ep
			on e.event_id = ep.event_id
			inner join member_hierarchy mh
			on mh.member_hierarchy_id = ep.member_hierarchy_id
			inner join member m
			on mh.member_id = m.member_id where ' + 'Isnull(m.first_name,'''') + Isnull(m.middle_name, '''') +  Isnull(m.last_name, '''') ' + 'LIKE ''%' + @MemberMNCriteria + '%'')  '
	END




EXEC(@SQLStatement)

END
GO
