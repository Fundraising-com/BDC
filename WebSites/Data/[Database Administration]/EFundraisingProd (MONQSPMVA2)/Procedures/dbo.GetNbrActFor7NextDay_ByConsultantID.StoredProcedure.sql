USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[GetNbrActFor7NextDay_ByConsultantID]    Script Date: 02/14/2014 13:08:41 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/****** Object:  Stored Procedure dbo.GetNbrActFor7NextDay_ByConsultantID    Script Date: 2003-02-22 20:34:54 ******/

create procedure [dbo].[GetNbrActFor7NextDay_ByConsultantID](@nConsultantID integer)
/* Le résultat nous ramène selon le consultant le nombre d'activité
pour chacun des 7 prochain jour a partir de la date courante */
as
begin
  select Consultant.Consultant_ID,
    Days0=(
	select Count(L.Consultant_ID) 
	from Lead_Activity 
	join Lead as L on Lead_Activity.Lead_Id = L.Lead_ID 
	where convert(char(8), Lead_Activity.Lead_Activity_Date, 112) = convert(char(8), getdate(), 112)
		and L.Consultant_ID = Consultant.Consultant_ID 
		and Completed_Date is null),
    Days1=(
	select Count(L.Consultant_ID) 
	from Lead_Activity join Lead as L on Lead_Activity.Lead_Id = L.Lead_ID 
	where convert(char(8), Lead_Activity.Lead_Activity_Date, 112) = convert(char(8), (dateadd(day,1,getdate())), 112) 
		and L.Consultant_ID = Consultant.Consultant_ID 
		and Completed_Date is null),
    Days2=(
	select Count(L.Consultant_ID)
	from Lead_Activity join Lead as L on Lead_Activity.Lead_Id = L.Lead_ID 
	where convert(char(8), Lead_Activity.Lead_Activity_Date, 112) = convert(char(8), (dateadd(day,2,getdate())), 112) 
		and L.Consultant_ID = Consultant.Consultant_ID 
		and Completed_Date is null),
    Days3=(
	select Count(L.Consultant_ID)
	from Lead_Activity join Lead as L on Lead_Activity.Lead_Id = L.Lead_ID 
	where convert(char(8), Lead_Activity.Lead_Activity_Date, 112) = convert(char(8), (dateadd(day,3,getdate())), 112)  
		and L.Consultant_ID = Consultant.Consultant_ID 
		and Completed_Date is null),
    Days4=(
	select Count(L.Consultant_ID)
	from Lead_Activity join Lead as L on Lead_Activity.Lead_Id = L.Lead_ID
	where convert(char(8), Lead_Activity.Lead_Activity_Date, 112) = convert(char(8), (dateadd(day,4,getdate())), 112) 
		and L.Consultant_ID = Consultant.Consultant_ID 
		and Completed_Date is null),
    Days5=(
	select Count(L.Consultant_ID)
	from Lead_Activity join Lead as L on Lead_Activity.Lead_Id = L.Lead_ID 
	where convert(char(8), Lead_Activity.Lead_Activity_Date, 112) = convert(char(8), (dateadd(day,5,getdate())), 112)  
		and L.Consultant_ID = Consultant.Consultant_ID 
		and Completed_Date is null),
    Days6=(
	select Count(L.Consultant_ID)
	from Lead_Activity join Lead as L on Lead_Activity.Lead_Id = L.Lead_ID 
	where convert(char(8), Lead_Activity.Lead_Activity_Date, 112) = convert(char(8), (dateadd(day,6,getdate())), 112)  
		and L.Consultant_ID = Consultant.Consultant_ID 
		and Completed_Date is null) 
    from
    Consultant where
    Consultant.Consultant_ID = @nConsultantID
end
GO
