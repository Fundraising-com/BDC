param($installPath, $toolsPath, $package, $project)
[System.Reflection.Assembly]::LoadFile("$($installPath)\lib\net35\System.Data.CData.GoogleContacts.dll")
[System.Data.CData.GoogleContacts.Nuget]::CheckNugetLicense("nuget")