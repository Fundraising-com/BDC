USE QSPCanadaFinance

/*
INSERT Adjustment_Type VALUES (49063, 'Prize Income - FM', NULL, 'C', 'N', 'N', 'N', NULL, NULL, 1, 0)
SELECT * FROM Adjustment_Type

INSERT GLAccount VALUES(1, 2, 'Prize Income Expense', '380000','471','46014',null,null,null,null,null,null,null,null,null,null,getdate(),1)
INSERT GLAccountAdjustmentType VALUES (230, 49063, 0, getdate())
INSERT GLAccountAdjustmentType VALUES (235, 49063, 1, getdate())
SELECT * FROM GLAccountAdjustmentType t join glaccount a on a.glaccountid = t.glaccountid where adjustmenttypeid = 49063
select *, dbo.UDF_GLAccount_GetAccountNumber(glaccountid) from glaccount where account = '477210'
*/
select * 
from adjustment adj 
join gl_entry e
			 on e.adjustment_id = adj.adjustment_id 
join gl_transaction t on t.gl_entry_id = e.gl_entry_id 
where adjustment_type_id=49053

CREATE TABLE #Adjustment
(
	AccountID		INT,
	OrderID			INT,
	InternalComment	VARCHAR(100),
	Amount			NUMERIC(10, 2),
	CampaignID		INT,
	AdjustmentType	INT
)

--2015
INSERT INTO #Adjustment VALUES(	33582	,	NULL	,	'FM Commission Payout'	,	4117.60	,	87098	,	49061	)
INSERT INTO #Adjustment VALUES(	33603	,	NULL	,	'FM Commission Payout'	,	1888.79	,	87119	,	49061	)
INSERT INTO #Adjustment VALUES(	33599	,	NULL	,	'FM Commission Payout'	,	575.30	,	87115	,	49061	)
INSERT INTO #Adjustment VALUES(	33598	,	NULL	,	'FM Commission Reduction'	,	2418.95	,	87114	,	49060	)
INSERT INTO #Adjustment VALUES(	33605	,	NULL	,	'FM Commission Payout'	,	5600.05	,	87121	,	49061	)
INSERT INTO #Adjustment VALUES(	33586	,	NULL	,	'FM Commission Payout'	,	1504.67	,	87102	,	49061	)
INSERT INTO #Adjustment VALUES(	33591	,	NULL	,	'FM Commission Reduction'	,	1929.69	,	87107	,	49060	)
INSERT INTO #Adjustment VALUES(	33580	,	NULL	,	'FM Commission Payout'	,	727.19	,	87096	,	49061	)
INSERT INTO #Adjustment VALUES(	33578	,	NULL	,	'FM Commission Payout'	,	5239.52	,	87094	,	49061	)
INSERT INTO #Adjustment VALUES(	33584	,	NULL	,	'FM Commission Reduction'	,	10014.37	,	87100	,	49060	)
INSERT INTO #Adjustment VALUES(	33579	,	NULL	,	'FM Commission Reduction'	,	5350.39	,	87095	,	49060	)
INSERT INTO #Adjustment VALUES(	33588	,	NULL	,	'FM Commission Reduction'	,	8552.77	,	87104	,	49060	)
INSERT INTO #Adjustment VALUES(	33595	,	NULL	,	'FM Commission Payout'	,	4986.70	,	87111	,	49061	)
INSERT INTO #Adjustment VALUES(	33596	,	NULL	,	'FM Commission Reduction'	,	11046.54	,	87112	,	49060	)
INSERT INTO #Adjustment VALUES(	33604	,	NULL	,	'FM Commission Payout'	,	176.14	,	87120	,	49061	)
INSERT INTO #Adjustment VALUES(	33597	,	NULL	,	'FM Commission Reduction'	,	13948.18	,	87113	,	49060	)
INSERT INTO #Adjustment VALUES(	33601	,	NULL	,	'FM Commission Reduction'	,	13103.71	,	87117	,	49060	)
INSERT INTO #Adjustment VALUES(	33585	,	NULL	,	'FM Commission Payout'	,	921.91	,	87101	,	49061	)
INSERT INTO #Adjustment VALUES(	33592	,	NULL	,	'FM Commission Payout'	,	1530.75	,	87108	,	49061	)
INSERT INTO #Adjustment VALUES(	33600	,	NULL	,	'FM Commission Payout'	,	1379.84	,	87116	,	49061	)
INSERT INTO #Adjustment VALUES(	33602	,	NULL	,	'FM Commission Payout'	,	3423.09	,	87118	,	49061	)
INSERT INTO #Adjustment VALUES(	33587	,	NULL	,	'FM Commission Reduction'	,	1149.53	,	87103	,	49060	)
INSERT INTO #Adjustment VALUES(	33593	,	NULL	,	'FM Commission Reduction'	,	6481.59	,	87109	,	49060	)
INSERT INTO #Adjustment VALUES(	33589	,	NULL	,	'FM Commission Reduction'	,	3379.87	,	87105	,	49060	)
INSERT INTO #Adjustment VALUES(	33635	,	NULL	,	'FM Commission Payout'	,	3096.10	,	87278	,	49061	)
INSERT INTO #Adjustment VALUES(	33581	,	NULL	,	'FM Commission Payout'	,	228.02	,	87097	,	49061	)
INSERT INTO #Adjustment VALUES(	33606	,	NULL	,	'FM Commission Payout'	,	3691.63	,	87122	,	49061	)
INSERT INTO #Adjustment VALUES(	33623	,	NULL	,	'FM Commission Reduction'	,	2050.30	,	87235	,	49060	)
INSERT INTO #Adjustment VALUES(	33710	,	NULL	,	'FM Commission Reduction'	,	2421.96	,	91721	,	49060	)
INSERT INTO #Adjustment VALUES(	33711	,	NULL	,	'FM Commission Reduction'	,	368.62	,	91722	,	49060	)
INSERT INTO #Adjustment VALUES(	33862	,	NULL	,	'FM Commission Payout'	,	862.35	,	93290	,	49061	)
INSERT INTO #Adjustment VALUES(	33864	,	NULL	,	'FM Commission Payout'	,	1178.19	,	93292	,	49061	)
INSERT INTO #Adjustment VALUES(	33866	,	NULL	,	'FM Commission Payout'	,	8470.03	,	93294	,	49061	)
INSERT INTO #Adjustment VALUES(	33868	,	NULL	,	'FM Commission Payout'	,	3129.90	,	93296	,	49061	)
INSERT INTO #Adjustment VALUES(	33870	,	NULL	,	'FM Commission Payout'	,	148.65	,	93298	,	49061	)
INSERT INTO #Adjustment VALUES(	33872	,	NULL	,	'FM Commission Payout'	,	558.22	,	93300	,	49061	)
INSERT INTO #Adjustment VALUES(	34265	,	NULL	,	'FM Commission Reduction'	,	63.71	,	97684	,	49060	)
INSERT INTO #Adjustment VALUES(	34289	,	NULL	,	'FM Commission Reduction'	,	31.64	,	97969	,	49060	)
INSERT INTO #Adjustment VALUES(	34287	,	NULL	,	'FM Commission Payout'	,	393.40	,	97971	,	49061	)
INSERT INTO #Adjustment VALUES(	34308	,	NULL	,	'FM Commission Reduction'	,	370.09	,	98186	,	49060	)

--2014
INSERT INTO #Adjustment VALUES(33582,NULL,'FM Commission Payout',11006.04,87098,49061)
INSERT INTO #Adjustment VALUES(33691,NULL,'FM Commission Payout',5380.66,89010,49061)
INSERT INTO #Adjustment VALUES(33603,NULL,'FM Commission Payout',3764.33,87119,49061)
INSERT INTO #Adjustment VALUES(33578,NULL,'FM Commission Payout',3693.08,87094,49061)
INSERT INTO #Adjustment VALUES(33592,NULL,'FM Commission Payout',3264,87108,49061)
INSERT INTO #Adjustment VALUES(33605,NULL,'FM Commission Payout',3261.52,87121,49061)
INSERT INTO #Adjustment VALUES(33635,NULL,'FM Commission Payout',2962.96,87278,49061)
INSERT INTO #Adjustment VALUES(33595,NULL,'FM Commission Payout',2158.17,87111,49061)
INSERT INTO #Adjustment VALUES(33585,NULL,'FM Commission Payout',1946.91,87101,49061)
INSERT INTO #Adjustment VALUES(33586,NULL,'FM Commission Payout',1742.71,87102,49061)
INSERT INTO #Adjustment VALUES(33711,NULL,'FM Commission Payout',1428.37,91722,49061)
INSERT INTO #Adjustment VALUES(33599,NULL,'FM Commission Payout',1240.29,87115,49061)
INSERT INTO #Adjustment VALUES(33583,NULL,'FM Commission Payout',1122.15,87099,49061)
INSERT INTO #Adjustment VALUES(33597,NULL,'FM Commission Payout',922.94,87113,49061)
INSERT INTO #Adjustment VALUES(33600,NULL,'FM Commission Payout',778.81,87116,49061)
INSERT INTO #Adjustment VALUES(33870,NULL,'FM Commission Payout',749.52,93298,49061)
INSERT INTO #Adjustment VALUES(33581,NULL,'FM Commission Payout',352.56,87097,49061)
INSERT INTO #Adjustment VALUES(33872,NULL,'FM Commission Payout',24.52,93300,49061)
INSERT INTO #Adjustment VALUES(33868,NULL,'FM Commission Reduction',23.62,93296,49060)
INSERT INTO #Adjustment VALUES(33605,NULL,'FM Commission Reduction',36,93170,49060)
INSERT INTO #Adjustment VALUES(33710,NULL,'FM Commission Reduction',151.81,91886,49060)
INSERT INTO #Adjustment VALUES(33866,NULL,'FM Commission Reduction',181.41,93294,49060)
INSERT INTO #Adjustment VALUES(33587,NULL,'FM Commission Reduction',211.61,87103,49060)
INSERT INTO #Adjustment VALUES(33606,NULL,'FM Commission Reduction',256.04,87122,49060)
INSERT INTO #Adjustment VALUES(33864,NULL,'FM Commission Reduction',382.56,93292,49060)
INSERT INTO #Adjustment VALUES(33591,NULL,'FM Commission Reduction',533.88,87107,49060)
INSERT INTO #Adjustment VALUES(33862,NULL,'FM Commission Reduction',555.23,93290,49060)
INSERT INTO #Adjustment VALUES(33578,NULL,'FM Commission Reduction',748.55,93168,49060)
INSERT INTO #Adjustment VALUES(33602,NULL,'FM Commission Reduction',819.56,87118,49060)
INSERT INTO #Adjustment VALUES(33710,NULL,'FM Commission Reduction',1290.9,91721,49060)
INSERT INTO #Adjustment VALUES(33580,NULL,'FM Commission Reduction',1796.39,87096,49060)
INSERT INTO #Adjustment VALUES(33579,NULL,'FM Commission Reduction',1907.44,87095,49060)
INSERT INTO #Adjustment VALUES(33598,NULL,'FM Commission Reduction',2898.81,87114,49060)
INSERT INTO #Adjustment VALUES(33589,NULL,'FM Commission Reduction',3089.15,87105,49060)
INSERT INTO #Adjustment VALUES(33584,NULL,'FM Commission Reduction',3157.16,87100,49060)
INSERT INTO #Adjustment VALUES(33582,NULL,'FM Commission Reduction',3330.11,87239,49060)
INSERT INTO #Adjustment VALUES(33588,NULL,'FM Commission Reduction',3355.08,87104,49060)
INSERT INTO #Adjustment VALUES(33584,NULL,'FM Commission Reduction',4235.61,87249,49060)
INSERT INTO #Adjustment VALUES(33623,NULL,'FM Commission Reduction',5489.71,87235,49060)
INSERT INTO #Adjustment VALUES(33604,NULL,'FM Commission Reduction',6711.47,87120,49060)
INSERT INTO #Adjustment VALUES(33596,NULL,'FM Commission Reduction',9428.98,87112,49060)
INSERT INTO #Adjustment VALUES(33593,NULL,'FM Commission Reduction',10490.12,87109,49060)
INSERT INTO #Adjustment VALUES(33601,NULL,'FM Commission Reduction',18875,87117,49060)

--2013
INSERT INTO #Adjustment VALUES(33603,NULL,'FM Commission Reduction',528,87119,49060)
INSERT INTO #Adjustment VALUES(33691,NULL,'FM Commission Reduction',592.75,89010,49060)
INSERT INTO #Adjustment VALUES(33599,NULL,'FM Commission Reduction',1904.95,87115,49060)
INSERT INTO #Adjustment VALUES(33598,NULL,'FM Commission Reduction',4491.88,87114,49060)
INSERT INTO #Adjustment VALUES(33605,NULL,'FM Commission Reduction',1403.16,87121,49060)
INSERT INTO #Adjustment VALUES(33586,NULL,'FM Commission Reduction',1309.91,87102,49060)
INSERT INTO #Adjustment VALUES(33591,NULL,'FM Commission Reduction',2296.81,87107,49060)
INSERT INTO #Adjustment VALUES(33580,NULL,'FM Commission Reduction',1952.55,87096,49060)
INSERT INTO #Adjustment VALUES(33578,NULL,'FM Commission Reduction',1071.63,87094,49060)
INSERT INTO #Adjustment VALUES(33584,NULL,'FM Commission Reduction',7193.09,87100,49060)
INSERT INTO #Adjustment VALUES(33579,NULL,'FM Commission Reduction',2516.7,87095,49060)
INSERT INTO #Adjustment VALUES(33588,NULL,'FM Commission Payout',510.94,87104,49061)
INSERT INTO #Adjustment VALUES(33595,NULL,'FM Commission Reduction',5028.83,87111,49060)
INSERT INTO #Adjustment VALUES(33596,NULL,'FM Commission Reduction',7434.72,87112,49060)
INSERT INTO #Adjustment VALUES(33604,NULL,'FM Commission Reduction',2990.97,87120,49060)
INSERT INTO #Adjustment VALUES(33597,NULL,'FM Commission Reduction',1838.2,87113,49060)
INSERT INTO #Adjustment VALUES(33601,NULL,'FM Commission Reduction',20940.51,87117,49060)
INSERT INTO #Adjustment VALUES(33585,NULL,'FM Commission Reduction',1561,87101,49060)
INSERT INTO #Adjustment VALUES(33592,NULL,'FM Commission Reduction',41.59,87108,49060)
INSERT INTO #Adjustment VALUES(33600,NULL,'FM Commission Reduction',2972.94,87116,49060)
INSERT INTO #Adjustment VALUES(33602,NULL,'FM Commission Reduction',2987.17,87118,49060)
INSERT INTO #Adjustment VALUES(33587,NULL,'FM Commission Reduction',1501.95,87103,49060)
INSERT INTO #Adjustment VALUES(33593,NULL,'FM Commission Reduction',7744.59,87109,49060)
INSERT INTO #Adjustment VALUES(33589,NULL,'FM Commission Reduction',3985.79,87105,49060)
INSERT INTO #Adjustment VALUES(33583,NULL,'FM Commission Reduction',2731.51,87099,49060)
INSERT INTO #Adjustment VALUES(33594,NULL,'FM Commission Reduction',870.59,87110,49060)
INSERT INTO #Adjustment VALUES(33635,NULL,'FM Commission Payout',224.93,87278,49061)
INSERT INTO #Adjustment VALUES(33581,NULL,'FM Commission Payout',23.2,87097,49061)
INSERT INTO #Adjustment VALUES(33606,NULL,'FM Commission Reduction',0,87122,49060)
INSERT INTO #Adjustment VALUES(33623,NULL,'FM Commission Reduction',0,87235,49060)


INSERT INTO #Adjustment VALUES(6559,913092,'Incorrect Jewellery GP','10.24',80637,49053)
INSERT INTO #Adjustment VALUES(5149,912689,'Incorrect Jewellery GP','9.48',80953,49053)
INSERT INTO #Adjustment VALUES(11790,912745,'Incorrect Jewellery GP','33.83',81032,49053)
INSERT INTO #Adjustment VALUES(10605,912854,'Incorrect Jewellery GP','12.31',81129,49053)
INSERT INTO #Adjustment VALUES(16643,913013,'Incorrect Jewellery GP','3.79',81325,49053)
INSERT INTO #Adjustment VALUES(8541,912935,'Incorrect Jewellery GP','12.63',81770,49053)
INSERT INTO #Adjustment VALUES(6340,912786,'Incorrect Jewellery GP','13.32',81912,49053)
INSERT INTO #Adjustment VALUES(4293,912833,'Incorrect Jewellery GP','3.00',82034,49053)
INSERT INTO #Adjustment VALUES(8053,912793,'Incorrect Jewellery GP','1.56',82049,49053)
INSERT INTO #Adjustment VALUES(8053,912717,'Incorrect Jewellery GP','4.09',82049,49053)
INSERT INTO #Adjustment VALUES(16863,912940,'Incorrect Jewellery GP','0.79',82307,49053)
INSERT INTO #Adjustment VALUES(9012,912772,'Incorrect Jewellery GP','2.34',82686,49053)
INSERT INTO #Adjustment VALUES(9026,912862,'Incorrect Jewellery GP','12.02',83015,49053)
INSERT INTO #Adjustment VALUES(9814,912939,'Incorrect Jewellery GP','1.43',83135,49053)
INSERT INTO #Adjustment VALUES(1032,912815,'Incorrect Jewellery GP','11.15',83137,49053)
INSERT INTO #Adjustment VALUES(2524,912864,'Incorrect Jewellery GP','12.68',83350,49053)
INSERT INTO #Adjustment VALUES(31794,912812,'Incorrect Jewellery GP','1.33',83534,49053)
INSERT INTO #Adjustment VALUES(2576,913018,'Incorrect Jewellery GP','4.79',83681,49053)
INSERT INTO #Adjustment VALUES(15891,913044,'Incorrect Jewellery GP','1.01',83784,49053)
INSERT INTO #Adjustment VALUES(5194,912720,'Incorrect Jewellery GP','87.96',83963,49053)
INSERT INTO #Adjustment VALUES(5186,912801,'Incorrect Jewellery GP','35.34',83982,49053)
INSERT INTO #Adjustment VALUES(5297,912863,'Incorrect Jewellery GP','21.71',84033,49053)
INSERT INTO #Adjustment VALUES(11158,913015,'Incorrect Jewellery GP','15.37',84158,49053)
INSERT INTO #Adjustment VALUES(6972,912937,'Incorrect Jewellery GP','9.81',84264,49053)
INSERT INTO #Adjustment VALUES(15745,913014,'Incorrect Jewellery GP','5.84',84342,49053)
INSERT INTO #Adjustment VALUES(1088,912782,'Incorrect Jewellery GP','5.06',84349,49053)
INSERT INTO #Adjustment VALUES(16469,912860,'Incorrect Jewellery GP','6.38',84369,49053)
INSERT INTO #Adjustment VALUES(17558,912936,'Incorrect Jewellery GP','6.56',84563,49053)
INSERT INTO #Adjustment VALUES(6577,912867,'Incorrect Jewellery GP','22.47',84614,49053)
INSERT INTO #Adjustment VALUES(6544,912702,'Incorrect Jewellery GP','4.84',84625,49053)
INSERT INTO #Adjustment VALUES(6600,912779,'Incorrect Jewellery GP','44.89',84718,49053)
INSERT INTO #Adjustment VALUES(6606,912853,'Incorrect Jewellery GP','8.46',84727,49053)
INSERT INTO #Adjustment VALUES(16412,912842,'Incorrect Jewellery GP','44.99',85452,49053)
INSERT INTO #Adjustment VALUES(6718,912703,'Incorrect Jewellery GP','0.99',85594,49053)
INSERT INTO #Adjustment VALUES(2462,913017,'Incorrect Jewellery GP','5.45',85662,49053)

SELECT * FROM #Adjustment

BEGIN TRANSACTION

DECLARE	@AccountID			INT,
		@OrderID			INT,
		@InternalComment	VARCHAR(100),
		@Amount				NUMERIC(10, 2),
		@CampaignID			INT,
		@AdjustmentType		INT,
		@ChangedBy			INT,
		@RefundID			INT,
		@Value				INT 

SET @ChangedBy = 612
SET @RefundID = null

DECLARE		AdjustmentCursor CURSOR FOR
SELECT		AccountID,
			OrderID,
			InternalComment,
			Amount,
			CampaignID,
			AdjustmentType
FROM		#Adjustment
ORDER BY	AdjustmentType,
			CampaignID

OPEN AdjustmentCursor
FETCH NEXT FROM AdjustmentCursor INTO @AccountID, @OrderID, @InternalComment, @Amount, @CampaignID, @AdjustmentType

WHILE @@FETCH_STATUS = 0
BEGIN

	EXEC [AddInvoiceAdjustment] @AccountID = @AccountID, @OrderID = @OrderID, @InternalComment = @InternalComment,
								@Amount = @Amount, @CampaignID = @CampaignID, @AdjustmentType = @AdjustmentType,
								@ChangedBy = @ChangedBy, @RefundID = @RefundID, @Value = @Value

	FETCH NEXT FROM AdjustmentCursor INTO @AccountID, @OrderID, @InternalComment, @Amount, @CampaignID, @AdjustmentType

END
CLOSE AdjustmentCursor
DEALLOCATE AdjustmentCursor

COMMIT

DROP TABLE #Adjustment

--select top 99 * from ADJUSTMENT order by ADJUSTMENT_ID desc