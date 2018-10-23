$projects = @(
    ".\CLinq.Core\CLinq.Core.csproj",
    ".\CLinq.EF6\CLinq.EF6.csproj",
    ".\CLinq.EFCore1\CLinq.EFCore1.csproj",
    ".\CLinq.EFCore2\CLinq.EFCore2.csproj"
)

$msBuild = "C:\Program Files (x86)\Microsoft Visual Studio\2017\Professional\MSBuild\15.0\Bin\MSBuild.exe"

foreach ($project in $projects) {
    & "$msBuild" $project /t:clean,pack /p:IncludeSymbols=true /p:Configuration=Release /verbosity:minimal
}