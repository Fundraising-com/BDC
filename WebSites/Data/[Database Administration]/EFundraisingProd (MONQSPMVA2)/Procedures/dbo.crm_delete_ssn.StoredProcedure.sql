USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[crm_delete_ssn]    Script Date: 02/14/2014 13:03:09 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[crm_delete_ssn]
           
as


declare @time datetime
set @time = dateadd(DD,-1,getdate())

update credit_check_request set ssn = '' where request_date < @time
GO
