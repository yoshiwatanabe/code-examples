$solutionPackagerExe = $env:UserProfile + "\.nuget\packages\microsoft.crmsdk.coretools\9.1.0.39\content\bin\coretools\SolutionPackager.exe"
$solutionPackagerArgs = "/n /action:Extract /zipfile:yoshi_core_1_3.zip /folder:src /packageType:Both"
$solutionPackagerArgs = $solutionPackagerArgs.Split(" ")
& $solutionPackagerExe $solutionPackagerArgs