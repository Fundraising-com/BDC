select cd.Description BrochureType, pm.Program_Type BrochureName, pm.Code BrochureCode
from program_master pm
join QSPCanadaCommon..CodeDetail cd ON cd.Instance = pm.SubType
join qspcanadacommon..season s on s.id = pm.season
where getdate() between s.startdate and s.enddate
order by pm.SubType