USE [eFundweb]
GO
/****** Object:  StoredProcedure [dbo].[efr_insert_leadgen_lead]    Script Date: 02/14/2014 13:04:34 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[efr_insert_leadgen_lead]
@LeadGen_id int,
@lead_id int, 
@create_date datetime

AS

INSERT INTO [eFundweb].[dbo].[leadgen_lead]
           ([LeadGen_id]
           ,[lead_id]
           ,[create_date])
     VALUES
(@LeadGen_id, @lead_id, @create_date)
GO
