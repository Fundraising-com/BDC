USE [eFundstore]
GO
/****** Object:  StoredProcedure [dbo].[wfq_TestMonthLouis]    Script Date: 02/14/2014 13:06:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[wfq_TestMonthLouis]
AS
	SELECT month_id, month_name FROM wfq_MonthLouis
GO
