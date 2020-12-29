USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[GetNbrActFor7NextDay_ByConsultantID2]    Script Date: 02/14/2014 13:08:41 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/****** Object:  Stored Procedure dbo.GetNbrActFor7NextDay_ByConsultantID2    Script Date: 2003-02-22 20:34:54 ******/




CREATE   procedure [dbo].[GetNbrActFor7NextDay_ByConsultantID2](@nConsultantID integer,@nFact integer)
/* Le résultat nous ramène selon le consultant le nombre d'activité
pour chacun des 7 prochain jour a partir de la date courante */
as
begin
 declare @day0_from datetime
 declare @day0_to datetime
 declare @day1_from datetime
 declare @day1_to datetime
 declare @day2_from datetime
 declare @day2_to datetime
 declare @day3_from datetime
 declare @day3_to datetime
 declare @day4_from datetime
 declare @day4_to datetime
 declare @day5_from datetime
 declare @day5_to datetime
 declare @day6_from datetime
 declare @day6_to datetime

 set @day0_from = convert(char(8),(DateAdd (day,@nFact,getdate())),112)
 set @day0_to = @day0_from + ' 23:59:59'
 set @day1_from  = convert(char(8),(DateAdd (day,(@nFact+1),getdate())),112) 
 set @day1_to = @day1_from + ' 23:59:59'
 set @day2_from = convert(char(8),(DateAdd (day,(@nFact+2),getdate())),112)
 set @day2_to = @day2_from + ' 23:59:59'
 set @day3_from = convert(char(8),(DateAdd (day,(@nFact+3),getdate())),112)
 set @day3_to = @day3_from + ' 23:59:59'
 set @day4_from = convert(char(8),(DateAdd (day,(@nFact+4),getdate())),112)
 set @day4_to = @day4_from + ' 23:59:59'
 set @day5_from = convert(char(8),(DateAdd (day,(@nFact+5),getdate())),112)
 set @day5_to = @day5_from + ' 23:59:59'
 set @day6_from = convert(char(8),(DateAdd (day,(@nFact+6),getdate())),112)
 set @day6_to = @day6_from + ' 23:59:59'

  select Consultant.Consultant_ID,
    Days0=(
	select Count(L.Consultant_ID) 
	from Lead_Activity 
	join Lead as L on Lead_Activity.Lead_Id = L.Lead_ID 
	where Lead_Activity.Lead_Activity_Date between @day0_from and @day0_to
		and L.Consultant_ID = @nConsultantID
		and Completed_Date is null),
    Days1=(
	select Count(L.Consultant_ID) 
	from Lead_Activity join Lead as L on Lead_Activity.Lead_Id = L.Lead_ID 
	where Lead_Activity.Lead_Activity_Date between @day1_from and @day1_to
		and L.Consultant_ID = @nConsultantID
		and Completed_Date is null),
    Days2=(
	select Count(L.Consultant_ID)
	from Lead_Activity join Lead as L on Lead_Activity.Lead_Id = L.Lead_ID 
	where Lead_Activity.Lead_Activity_Date between @day2_from and @day2_to
		and L.Consultant_ID = @nConsultantID
		and Completed_Date is null),
    Days3=(
	select Count(L.Consultant_ID)
	from Lead_Activity join Lead as L on Lead_Activity.Lead_Id = L.Lead_ID 
	where Lead_Activity.Lead_Activity_Date between @day3_from and @day3_to
		and L.Consultant_ID = @nConsultantID
		and Completed_Date is null),
    Days4=(
	select Count(L.Consultant_ID)
	from Lead_Activity join Lead as L on Lead_Activity.Lead_Id = L.Lead_ID
	where Lead_Activity.Lead_Activity_Date between @day4_from and @day4_to
		and L.Consultant_ID = @nConsultantID
		and Completed_Date is null),
    Days5=(
	select Count(L.Consultant_ID)
	from Lead_Activity join Lead as L on Lead_Activity.Lead_Id = L.Lead_ID 
	where Lead_Activity.Lead_Activity_Date between @day5_from and @day5_to
		and L.Consultant_ID = @nConsultantID
		and Completed_Date is null),
    Days6=(
	select Count(L.Consultant_ID)
	from Lead_Activity join Lead as L on Lead_Activity.Lead_Id = L.Lead_ID 
	where Lead_Activity.Lead_Activity_Date between @day6_from and @day6_to
		and L.Consultant_ID = @nConsultantID
		and Completed_Date is null) 
    from
    Consultant where
    Consultant.Consultant_ID = @nConsultantID
end
GO
