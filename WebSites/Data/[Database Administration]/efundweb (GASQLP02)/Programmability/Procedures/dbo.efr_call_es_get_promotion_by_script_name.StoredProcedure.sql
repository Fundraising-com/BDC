USE [eFundweb]
GO
/****** Object:  StoredProcedure [dbo].[efr_call_es_get_promotion_by_script_name]    Script Date: 02/14/2014 13:04:30 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE   procedure [dbo].[efr_call_es_get_promotion_by_script_name]
	@script_name varchar(100)
as

select *
from efundraisingprod..promotion
where script_name = @script_name
  and is_active = 1
GO
