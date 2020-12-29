USE [eFundweb]
GO
/****** Object:  StoredProcedure [dbo].[check_leads]    Script Date: 02/14/2014 13:04:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE  PROC [dbo].[check_leads]
   @warn as int=15,
   @crit as int=21
AS
BEGIN
   /*
      Written by Joey Marks
      December 3, 2003

    This procedure checks the Temp_Leads table for leads that
    are more than the @warn or @crit threshhold of minutes old.
    The output is meant to be parsed by the check_mssql_cmd.sh
    Nagios plugin and expects a strict output format.
    The output can contain any text of no more than a few lines long
    and must be followed by a single line containing the string:
    RESULT=x. x being either 0, 1, 2, or 3. 0 means OK, 1 means 
    the warning threshold has been reached, 2 that the critical
    threshold has been reached and 3 for any error.

   */

   DECLARE @int_leads int
   DECLARE @int_max_lead_age int
   DECLARE @result tinyint

   SET @result=0

   SELECT @int_leads=ISNULL(count(*),0), @int_max_lead_age=ISNULL(DATEDIFF(mi, MIN(Lead_Entry_Date), getdate()),0)
      FROM Temp_Lead
      WHERE IsNew=1

   PRINT CONVERT(varchar(5),@int_leads)+' outstanding leads. Oldest lead is '+
         CONVERT(varchar(5),@int_max_lead_age)+' minutes old'
   
   IF @int_max_lead_age>=@warn
   BEGIN
      IF @int_max_lead_age>=@crit
      BEGIN
         SET @result=2
      END
      ELSE
      BEGIN
         SET @result=1
      END
   END

   PRINT 'RESULT='+CONVERT(varchar(3),@result)
   RETURN(@result)
END
GO
