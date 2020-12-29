USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_get_req_request_by_id]    Script Date: 02/14/2014 13:05:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate get by id stored proc for Req_Request
CREATE PROCEDURE [dbo].[efrcrm_get_req_request_by_id] @Request_Id int AS
begin

select Request_Id, Language_Id, Request_Type_ID, Project_Type_ID, Priority_Id, Project_Name, Summary_Description, Request_Date, Decision_Required_Date, Impact_Description, Mis_Impact_Description, Decision_Description, Decision_Id, Decision_Date, Project_Sheduled_Start_Date, Project_Sheduled_End_Date, Project_Start_Date, Project_End_Date, Manager_ID, Employee_Id, MIS_ID from Req_Request where Request_Id=@Request_Id

end
GO
