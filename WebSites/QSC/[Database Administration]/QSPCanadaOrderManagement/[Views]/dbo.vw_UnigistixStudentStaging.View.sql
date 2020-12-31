USE [QSPCanadaOrderManagement]
GO
/****** Object:  View [dbo].[vw_UnigistixStudentStaging]    Script Date: 06/07/2017 09:18:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[vw_UnigistixStudentStaging] AS


			select  orderid, 
				s.instance as ParticipantInstance, 
				cast(s.Instance as varchar) + cast(cast(rand() * DATEPART(MS,getDate()) * 10000 as int) as varchar) as NewInstance,
				s.LastName as ParticipantLastName,
				s.FirstName as ParticipantFirstName,
				s.TeacherInstance,
				count(premium_rd.premiumtype) as qspremiumcount,
				count(premium_other.premiumtype)as otherpremiumcount  
			from
				student s, 
				customerorderheader coh,
				
				batch,		
				customerorderdetail cod left join
				(SELECT * FROM vw_Premiums WHERE  PremiumType=1) premium_rd on premium_rd.product_code = cod.productcode left join
 				(SELECT * FROM vw_Premiums WHERE  PremiumType=2) premium_other
					 on premium_other.product_code = cod.productcode
				where 
					coh.orderbatchdate=date and coh.orderbatchid=id
					and coh.instance= cod.customerorderheaderinstance
					and coh.studentinstance = s.instance

			group by orderid,
				s.instance,
				s.LastName,
				s.FirstName,
				s.TeacherInstance
GO
