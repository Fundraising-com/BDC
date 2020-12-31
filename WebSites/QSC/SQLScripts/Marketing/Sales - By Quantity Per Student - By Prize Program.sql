USE QSPCanadaOrderManagement

SELECT		CASE prog.Name WHEN 'Large Chart (Pick A Prize) with #''s' THEN 'Large Chart (Pick A Prize)' ELSE prog.Name END PrizeProgram,
			camp.ID CampaignID, 
			s.Instance StudentInstance, 
			s.Firstname StudentFirstname,
			s.Lastname StudentLastName,
			SUM(CASE WHEN cod.ProductType in (46001,46006,46007,46023,46024) THEN 1 ELSE 0 END) * 2 MagQuantity,
			SUM(CASE WHEN cod.ProductType not in ( 46001,46006,46007, 46023, 46024 ) THEN cod.Quantity ELSE 0 END) OtherQuantity
INTO		#s
FROM		QSPCanadaCommon..Campaign camp
JOIN		QSPCanadaCommon..CAccount acc
				ON	acc.ID = camp.BillToAccountID
JOIN		QSPCanadaCommon..CampaignProgram cp
				ON	cp.CampaignID = camp.ID
				AND	cp.DeletedTF = 0
				AND	cp.ProgramID IN (9,23,39,42,48)
JOIN		QSPCanadaCommon..Program prog
				ON	prog.ID = cp.ProgramID
JOIN		QSPCanadaCommon..FieldManager fm
				ON	fm.FMID = camp.FMID
JOIN		Batch b
				ON	b.CampaignID = camp.ID
JOIN		CustomerOrderHeader coh
				ON	coh.OrderBatchID = b.ID
				AND	coh.OrderBatchDate = b.Date
JOIN		CustomerOrderDetail cod
				ON	cod.CustomerOrderHeaderInstance = coh.Instance
JOIN		Student s
				ON	s.Instance = coh.StudentInstance
WHERE		cod.CreationDate BETWEEN '2017-07-01' AND '2017-12-31'
AND			cod.ProductType NOT IN (46008,46013,46014,46015, 46017, 46021)
AND			b.StatusInstance NOT IN (40005)
AND			cod.DelFlag = 0
AND			b.Date <=	(SELECT		MAX(bL.Date)
						FROM		Batch bL
						JOIN		CustomerOrderHeader coh
										ON	coh.OrderBatchID = bL.ID
										AND	coh.OrderBatchDate = bL.Date
						JOIN		CustomerOrderDetail cod
										ON	cod.CustomerOrderHeaderInstance = coh.Instance
						WHERE		(cod.ProductType IN (46013, 46014) OR cp.ProgramID IN (9,23,39))
						AND			cod.DelFlag = 0
						AND			bL.CampaignID = camp.ID
						AND			(b.OrderQualifierID IN (39001, 39002) OR b.OrderQualifierID = 39009 AND bL.OrderQualifierID = 39001))
GROUP BY	prog.Name, camp.ID, s.Instance, s.Firstname, s.Lastname
ORDER BY	prog.Name, camp.ID, s.Instance, s.Firstname, s.Lastname

SELECT		PrizeProgram, (MagQuantity + OtherQuantity) TotalQuantity, COUNT(StudentInstance) NumberStudents
FROM		#s
GROUP BY	PrizeProgram, (MagQuantity + OtherQuantity)
ORDER BY	PrizeProgram, (MagQuantity + OtherQuantity)
