USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[report_failed_sales]    Script Date: 3/14/2017 3:16:58 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER PROCEDURE [dbo].[report_failed_sales]
AS
BEGIN
	DECLARE @rows int
		SELECT @rows = COUNT(S.sales_id)
		FROM
		sale S (NOLOCK)
		JOIN AR_Status ARS (NOLOCK) ON S.ar_status_id = ARS.AR_Status_ID
		JOIN Sales_Status SS (NOLOCK) ON S.sales_status_id = SS.Sales_Status_ID
		JOIN payment_method PM (NOLOCK) ON S.payment_method_id = PM.payment_method_id
		JOIN client C (NOLOCK) ON S.client_id = C.client_id AND S.client_sequence_code = C.client_sequence_code
		WHERE
		S.sales_status_id IN (1,6)
		AND S.payment_method_id IN (2,3,8,9,15)
		AND S.ar_status_id IN (21)

	IF (@rows < 1)
		BEGIN
			RAISERROR ('No Records Found',16,1)
		END
	ELSE
		BEGIN
		SELECT
		S.sales_id AS [Sale Id]
		,S.sales_date AS [Date]
		,S.total_amount AS [Total Amount]
		,ARS.Description AS [AR Status]
		,SS.Description AS [Sale Status]
		,PM.description AS [Payment Method]
		,C.lead_id AS [Lead Id]
		,C.first_name AS [Client First Name]
		,C.last_name AS [Client Last Name]
		,C.email AS [Client Email]
		,C.client_id AS [Client Id]
		FROM
		sale S (NOLOCK)
		JOIN AR_Status ARS (NOLOCK) ON S.ar_status_id = ARS.AR_Status_ID
		JOIN Sales_Status SS (NOLOCK) ON S.sales_status_id = SS.Sales_Status_ID
		JOIN payment_method PM (NOLOCK) ON S.payment_method_id = PM.payment_method_id
		JOIN client C (NOLOCK) ON S.client_id = C.client_id AND S.client_sequence_code = C.client_sequence_code
		WHERE
		S.sales_status_id IN (1,6)
		AND S.payment_method_id IN (2,3,8,9,15)
		AND S.ar_status_id IN (21)
	END
END 