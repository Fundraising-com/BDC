USE [esubs_global_v2]
GO
/****** Object:  StoredProcedure [dbo].[es_get_promotion_self_registered]    Script Date: 02/14/2014 13:06:25 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- EXEC [dbo].[es_get_promotion_self_registered] 0

CREATE   procedure [dbo].[es_get_promotion_self_registered]
	@partner_id int
as

	declare @promotion_id int

	select  top 1
		@promotion_id =  p.promotion_id 
	from 	
		efrcommon.dbo.promotion p WITH (NOLOCK)
		inner join  efrcommon..partner_promotion pp WITH (NOLOCK) on p.promotion_id = pp.promotion_id
	where
		pp.partner_id = @partner_id
	and	p.Promotion_Type_Code = 'ON'


	if @promotion_id is null
	begin 
 		set @promotion_id = 3136
	end 

	select @promotion_id as promotion_id
GO
