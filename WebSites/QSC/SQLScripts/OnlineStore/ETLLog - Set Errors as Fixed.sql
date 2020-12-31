USE [GA]
GO

SELECT l.ETLLogID
	  ,jt.Literal as JobType
	  ,at.Literal as ActionType
	  ,ast.Literal as ActionStatusType
	  ,l.Message
	  ,l.Error
	  ,l.ErrorFixed
	  ,l.CreateDate
  FROM [GA].[Integration].[ETLLog] l with (nolock)
  JOIN ETLJobType jt with (nolock) ON l.ETLJobTypeCode = jt.Code
  JOIN ActionType at with (nolock) ON l.ActionTypeCode = at.Code
  JOIN ActionStatusType ast with (nolock) ON l.ActionStatusTypeCode = ast.Code
  WHERE ISNULL(l.ErrorFixed,0) = 0
  AND (l.ActionTypeCode = 3 -- Error
  OR l.ActionStatusTypeCode = 2) -- Fail
  AND l.Message NOT LIKE '%an active brochure could not be found for ProgramType 117.' --TEMP.  Hide these errors because there will be no TRT faculty brochure yet  
  AND l.Message NOT LIKE '%an active brochure could not be found for ProgramType 120.'
  AND l.Message NOT LIKE '%an active brochure could not be found for ProgramType 133.'
  AND l.Message NOT LIKE '%an active brochure could not be found for ProgramType 136.'
  AND l.Message NOT LIKE '%an active brochure could not be found for ProgramType 151.'
  AND l.Message NOT LIKE '%an active brochure could not be found for ProgramType 152.'
  AND l.Message NOT LIKE '%an active brochure could not be found for ProgramType 153.'
  AND l.Message NOT LIKE '%an active brochure could not be found for ProgramType 154.'
  AND l.Message NOT LIKE '%an active brochure could not be found for ProgramType 155.'
  AND l.Message NOT LIKE '%an active brochure could not be found for ProgramType 156.'
  AND l.Message NOT LIKE '%an active brochure could not be found for ProgramType 157.'
  AND l.Message NOT LIKE '%an active brochure could not be found for ProgramType 134.'
  AND l.Message NOT LIKE '%an active brochure could not be found for ProgramType 171.'
  AND l.Message NOT LIKE '%an active brochure could not be found for ProgramType 176.'
  AND l.Message NOT LIKE '%an active brochure could not be found for ProgramType 137.'
  AND l.Message NOT LIKE '%an active brochure could not be found for ProgramType 149.'
  AND l.Message NOT LIKE '%an active brochure could not be found for ProgramType 177.'
  AND l.Message NOT LIKE '%an active brochure could not be found for ProgramType 132.'
  ORDER BY l.ETLLogID
  
begin tran

update l
set ErrorFixed = 1
FROM [GA].[Integration].[ETLLog] l
JOIN ETLJobType jt ON l.ETLJobTypeCode = jt.Code
JOIN ActionType at ON l.ActionTypeCode = at.Code
JOIN ActionStatusType ast ON l.ActionStatusTypeCode = ast.Code
WHERE ISNULL(l.ErrorFixed,0) = 0
AND (l.ActionTypeCode = 3 -- Error
OR l.ActionStatusTypeCode = 2) -- Fail
AND l.Message NOT LIKE '%an active brochure could not be found for ProgramType 117.' --TEMP.  Hide these errors because there will be no TRT faculty brochure yet  
AND l.Message NOT LIKE '%an active brochure could not be found for ProgramType 120.'
AND l.Message NOT LIKE '%an active brochure could not be found for ProgramType 133.'
AND l.Message NOT LIKE '%an active brochure could not be found for ProgramType 136.'
AND l.Message NOT LIKE '%an active brochure could not be found for ProgramType 151.'
AND l.Message NOT LIKE '%an active brochure could not be found for ProgramType 152.'
AND l.Message NOT LIKE '%an active brochure could not be found for ProgramType 153.'
AND l.Message NOT LIKE '%an active brochure could not be found for ProgramType 154.'
AND l.Message NOT LIKE '%an active brochure could not be found for ProgramType 155.'
AND l.Message NOT LIKE '%an active brochure could not be found for ProgramType 156.'
AND l.Message NOT LIKE '%an active brochure could not be found for ProgramType 157.'
AND l.Message NOT LIKE '%an active brochure could not be found for ProgramType 134.'
AND l.Message NOT LIKE '%an active brochure could not be found for ProgramType 171.'
--commit tran
