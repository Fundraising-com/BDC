USE [eFundweb]
GO
/****** Object:  StoredProcedure [dbo].[efr_get_lead_from_qsp]    Script Date: 02/14/2014 13:04:32 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE PROCEDURE [dbo].[efr_get_lead_from_qsp]

AS
BEGIN
SELECT qsp.[LeadGen_Id]
      ,qsp.[First_Name]
      ,qsp.[Last_Name]
      ,qsp.[Title]
      ,qsp.[Organization]
      ,qsp.[Address1]
      ,qsp.[Address2]
      ,qsp.[City]
      ,qsp.[State]
		,CASE qsp.[State]
			WHEN 'ON' THEN 'CA'
			WHEN 'BC' THEN 'CA'
			WHEN 'AB' THEN 'CA'
			WHEN 'QC' THEN 'CA'
			WHEN 'MA' THEN 'CA'
			WHEN 'SK' THEN 'CA'
			WHEN 'PE' THEN 'CA'
			WHEN 'NL' THEN 'CA'
			WHEN 'NS' THEN 'CA'
			WHEN 'YK' THEN 'CA'
			WHEN 'NW' THEN 'CA'
			WHEN 'NB' THEN 'CA'
			ELSE 'US'
		End as [Country]
	  ,qsp.[Zip]
      ,qsp.[Phone_Day]
      ,qsp.[Phone_Evening]
      ,qsp.[Fax]
      ,qsp.[EMail]
      ,qsp.[Goal_Of_Fundraisers]
      ,qsp.[No_Of_Fundraisers]
      ,qsp.[No_Of_Years]
      ,qsp.[Time_Period]
      ,qsp.[Message_To_Rep]
      ,qsp.[Comment]
      ,qsp.[Status]
      ,qsp.[Date_Entered]
      ,qsp.[Origin]
      ,qsp.[InternalTrackingId]
      ,qsp.[ExternalTrackingId]
      ,qsp.[Create_Date]
      ,qsp.[Modify_Date]
      ,qsp.[Modified_By]
      ,qsp.[Deleted_TF]
FROM         QSPEcommerce.dbo.LeadGen_Info qsp LEFT OUTER JOIN
         dbo.leadgen_lead ll ON ll.LeadGen_id = qsp.LeadGen_Id
WHERE     (qsp.Create_Date > '2007-11-12 15:12:00') AND (ll.LeadGen_id IS NULL)

END
GO
