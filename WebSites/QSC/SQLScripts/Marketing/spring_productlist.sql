
--ALTER  procedure dbo.pr_GenerateResolveProductList
--Select distinct type from product where product_year=2013
--Select description, * from Product join qspcanadacommon.dbo.codedetail cd on cd.instance = type where product_year=2013 and type  in ( 46001, 46002,  46006, 46018, 46019)
drop table #t
select distinct
D.Code,
B.Product_Code,
	B.Product_Sort_Name,
PD.Nbr_Of_Issues,
pd.qsp_price as Price, pricing_year, pricing_season, 
Case when B.Type = 46007 then 46007 
	 when B.Type=46012 then 46012 else
			productline+46000 
end as productline

, pd.taxregionid
INTO #T
	  from 	
 QSPCanadaProduct..Program_Master D ,
QSPCanadaProduct..ProgramSection C,
QSPCanadaProduct..Pricing_Details PD ,
QSPCanadaProduct..Product B ,
QSPCanadaCommon..TaxRegion  T 
where 
 pd.product_code=b.product_code
and PD.Pricing_year=b.product_year
and pd.pricing_season = b.product_season
and C.CatalogCode= D.Code         -- catalog code
and  C.Id = PD.ProgramSectionId   -- program section
and  PD.Product_Code = B.Product_Code
and  T.ID=PD.TaxregionId
and productline not in (4,8,14,13,46013)
and pricing_season='F'
and pricing_year=2013
and pd.taxregionid in (1, 2,0)
--and T.TaxRegion in (1, 2)
order by D.Code, b.product_code, pricing_year,pricing_season, taxregionid
--SELECT * FROM #T where product_code='59HA'

DECLARE	 @SQL			VARCHAR(2000)
        ,@DDL			VARCHAR(4000)
		,@SERVERNAME	NVARCHAR(255)
        ,@WKS_NAME		VARCHAR(255)
        ,@PATH			VARCHAR(255)
		,@EXIST			INT
		,@CMD			VARCHAR(200)
		,@E_ERROR		INT
		

	-- INIT OLD DB VERIABLES
	SET @SERVERNAME	= 'EXP_EXCEL1'
	SET @WKS_NAME	= 'ResolveProductList' -- IT IS SHEET NAME
	SET @DDL	= 'CREATE TABLE '+@WKS_NAME+' (CODE TEXT, PRODUCT_CODE TEXT, PRODUCT_SORT_NAME TEXT, NBR_OF_ISSUES NUMBER, PRICE NUMBER, PRICING_YEAR NUMBER, PRICING_SEASON TEXT, PRODUCTLINE NUMBER, TAXREGIONID NUMBER)'
	SET @SQL	= 'INSERT INTO ' + @SERVERNAME + '...' + @WKS_NAME + ' (CODE, PRODUCT_CODE, PRODUCT_SORT_NAME, NBR_OF_ISSUES, PRICE, PRICING_YEAR, PRICING_SEASON, PRODUCTLINE, TAXREGIONID) '
	SET @SQL	= @SQL + 'SELECT CODE, PRODUCT_CODE, PRODUCT_SORT_NAME, NBR_OF_ISSUES, PRICE, PRICING_YEAR, PRICING_SEASON, PRODUCTLINE, TAXREGIONID FROM #T'

	SET @PATH	= 'E:\EXCEL_FILES\ResolveProductList_Fall2013.XLS'

-- CHECK FILE EXISTENCE
EXEC MASTER..XP_FILEEXIST @PATH, @EXIST output 
print @EXIST

IF @EXIST <> 0
BEGIN
	SET @CMD = 'DEL ' + @PATH
	EXEC MASTER..XP_CMDSHELL @CMD
END

-- EXPORT DATA INOT EXCEL FILE
EXEC QSPCANADACOMMON.DBO.USP_EXPEXCEL @SQL, @DDL, @SERVERNAME, @WKS_NAME, @PATH, @E_ERROR OUTPUT

--drop table #t
/*

-- SENDING EMAIL WITH ATTACHMENT
DECLARE @RC INT

	exec @rc = master.dbo.xp_smtp_sendmail
	    @FROM 	= 'SQL@ReadersDigest',
	    @TO 	= 'DONG_YANG@readersdigest.com',
--	    @CC 	= 'dong_yang@readersdigest.com',
	    @subject 	= 'ResolveProductList',
	    @message  	= ' ',
		@attachment = @PATH,
	    @server 	= 'nasmtp.us.rdigest.com'


MASTER..XP_CMDSHELL 'DIR E:\EXCEL_FILES\'
*/