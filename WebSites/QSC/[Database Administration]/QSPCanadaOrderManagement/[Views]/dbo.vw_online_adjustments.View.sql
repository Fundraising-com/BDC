USE [QSPCanadaOrderManagement]
GO
/****** Object:  View [dbo].[vw_online_adjustments]    Script Date: 06/07/2017 09:18:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create view [dbo].[vw_online_adjustments] as
select campaign_id, max(date_created) as lastdate, sum(adjustment_amount) as total_adjustment  from qspcanadafinance..adjustment where adjustment_type_id=49028 group by campaign_id
GO
