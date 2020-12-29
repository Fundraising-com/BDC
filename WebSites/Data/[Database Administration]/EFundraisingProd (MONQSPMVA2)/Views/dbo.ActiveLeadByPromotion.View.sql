USE [eFundraisingProd]
GO
/****** Object:  View [dbo].[ActiveLeadByPromotion]    Script Date: 02/14/2014 13:01:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
/****** Object:  View dbo.ActiveLeadByPromotion    Script Date: 2003-02-22 20:34:16 ******/


/****** Object:  View dbo.ActiveLeadByPromotion    Script Date: 2/11/2003 12:27:44 PM ******/

CREATE view [dbo].[ActiveLeadByPromotion] /* creator. */ /* view column name,... */
  as 
	select Count(Lead.Lead_ID) as CountOfLead_ID,Promotion.Promotion_ID 
	from(Lead join Promotion on Lead.Promotion_ID = Promotion.Promotion_ID) 
		join Partner on Promotion.Partner_ID = Partner.Partner_ID 
	where (((Partner.Partner_ID) = 8))
--fA-41,B-15
    group by Promotion.Promotion_ID
GO
