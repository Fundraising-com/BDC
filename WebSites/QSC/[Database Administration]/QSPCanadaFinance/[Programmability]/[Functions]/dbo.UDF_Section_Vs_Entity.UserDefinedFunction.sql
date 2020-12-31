USE [QSPCanadaFinance]
GO
/****** Object:  UserDefinedFunction [dbo].[UDF_Section_Vs_Entity]    Script Date: 06/07/2017 09:17:40 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE FUNCTION  [dbo].[UDF_Section_Vs_Entity]  (	@InvoiceID 		Int,
							@SectionTypeID 	Int,
             							@EntityId                         Int
						  )
Returns Varchar  As  
----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
--   MS 6/14/2004 
--   Section_vs_Entity For Canada Finance System verify if Invoice pertains to a Section Type for a given Entity
---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
Begin
	
	Declare	@RowCount 	Int,
		@v_Exists 	Varchar(1)

	Set        @v_Exists = 'N'

    	Select 	@RowCount  =  Count(*)
    	From 	QSPCanadaProduct.dbo.ProgramSectionType                 stype,
             	 	QSPCanadaProduct.dbo.ProgramSection                         ps,
   		QSPCanadaCommon.dbo.QSPProductLine                      qsp,
		QSPCanadaOrderManagement.dbo.CustomerOrderDetail od
   	Where od.InvoiceNumber = @InvoiceID 
    	And od.ProgramSectionId= ps.Id
    	And ps.Type 		= stype.Id
    	And stype.Id 		= @SectionTypeID
    	And od.Producttype	= qsp.Id
    	And qsp.Entityid	    	= @EntityId
          
	If @RowCount > 0
	Begin
	       Set @v_Exists = 'Y'
	End

	Return @v_Exists

End
GO
