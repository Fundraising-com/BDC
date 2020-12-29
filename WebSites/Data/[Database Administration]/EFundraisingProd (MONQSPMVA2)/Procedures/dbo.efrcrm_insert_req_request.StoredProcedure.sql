USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_insert_req_request]    Script Date: 02/14/2014 13:07:19 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate insert stored proc for Req_Request
CREATE PROCEDURE [dbo].[efrcrm_insert_req_request] @Request_Id int OUTPUT, @Language_Id int, @Request_Type_ID int, @Project_Type_ID int, @Priority_Id int, @Project_Name varchar(60), @Summary_Description text, @Request_Date smalldatetime, @Decision_Required_Date datetime, @Impact_Description text, @Mis_Impact_Description text, @Decision_Description text, @Decision_Id int, @Decision_Date smalldatetime, @Project_Sheduled_Start_Date smalldatetime, @Project_Sheduled_End_Date smalldatetime, @Project_Start_Date smalldatetime, @Project_End_Date smalldatetime, @Manager_ID int, @Employee_Id int, @MIS_ID int AS
begin

insert into Req_Request(Language_Id, Request_Type_ID, Project_Type_ID, Priority_Id, Project_Name, Summary_Description, Request_Date, Decision_Required_Date, Impact_Description, Mis_Impact_Description, Decision_Description, Decision_Id, Decision_Date, Project_Sheduled_Start_Date, Project_Sheduled_End_Date, Project_Start_Date, Project_End_Date, Manager_ID, Employee_Id, MIS_ID) values(@Language_Id, @Request_Type_ID, @Project_Type_ID, @Priority_Id, @Project_Name, @Summary_Description, @Request_Date, @Decision_Required_Date, @Impact_Description, @Mis_Impact_Description, @Decision_Description, @Decision_Id, @Decision_Date, @Project_Sheduled_Start_Date, @Project_Sheduled_End_Date, @Project_Start_Date, @Project_End_Date, @Manager_ID, @Employee_Id, @MIS_ID)

select @Request_Id = SCOPE_IDENTITY()

end
GO
