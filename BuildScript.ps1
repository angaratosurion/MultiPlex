properties {
    # setable properties
    $configuration = if ($env:TEAMCITY_PROJECT_NAME -ne $NULL) { 'Release' } else { 'Debug' }
    $buildNumber = if ($env:build_number -ne $NULL) { $env:build_number } else { '1.0.0.0' }
    
    # paths
    $baseDir = resolve-path .
    $archiveDir = "$baseDir\_zip"
    $helpDir = "$baseDir\_help\"
    $nugetDir = "$baseDir\_nuget"
    $sampleDir = "$baseDir\Sample"
    $slnPath = "$baseDir\WikiPlex.sln"
    $sln35Path = "$baseDir\WikiPlex35.sln"
    $docSlnPath = "$baseDir\WikiPlex.Documentation\WikiPlex.Documentation.shfbproj"
}

task default -depends run-clean, run-build, run-tests
task perf -depends default, run-perf-tests
task net35 -depends run-clean35, set-version, run-build35
task ci -depends run-clean, set-version, run-build, run-tests, build-documentation, clean-documentation-files, build-package
task doc -depends prepare-documentation
task cleandoc -depends clean-documentation-files
task builddoc -depends prepare-documentation, build-documentation, clean-documentation-files

task run-clean-dirs {
    clean $archiveDir, $helpDir, $sampleDir, $nugetDir
}

task run-clean35 -depends run-clean-dirs {
    exec { msbuild $sln35Path /t:Clean /p:Configuration=$configuration /v:quiet }
}

task run-clean -depends run-clean-dirs {   
    exec { msbuild $slnPath /t:Clean /p:Configuration=$configuration /v:quiet }
}

task run-build {
    exec { msbuild $slnPath /t:Build /p:Configuration=$configuration /v:quiet }
}

task run-build35 {
    exec { msbuild $sln35Path /t:Build /p:Configuration=$configuration /v:quiet }
}

task run-tests {
    execute-tests "$baseDir\WikiPlex.Tests\bin\$configuration\WikiPlex.Tests.dll" "Tests"
    execute-tests "$baseDir\WikiPlex.IntegrationTests\bin\$configuration\WikiPlex.IntegrationTests.dll" "Integration Tests"
}

task run-perf-tests {
    execute-tests "$baseDir\WikiPlex.PerformanceTests\bin\$configuration\WikiPlex.PerformanceTests.dll" "Performance Tests"
}

task set-version {
    $assemInfo = "$baseDir\GlobalAssemblyInfo.cs"
    exec { attrib -r $assemInfo }
    regex-replace $assemInfo 'AssemblyVersion\("\d+\.\d+\.\d+\.\d+"\)' "AssemblyVersion(`"$buildNumber`")"
    regex-replace $assemInfo 'AssemblyFileVersion\("\d+\.\d+\.\d+\.\d+"\)' "AssemblyFileVersion(`"$buildNumber`")"
}

task build-package -depends prepare-sample, prepare-nuget {
    create $archiveDir
    
    roboexec { robocopy "$baseDir\WikiPlex.Web.Sample" $sampleDir /E }
    
    exec { .\lib\zip.exe -9 -A -j -q `
                              "$archiveDir\WikiPlex-net35.zip" `
                              "$baseDir\WikiPlex\bin\net35\$configuration\*.dll" `
                              "$baseDir\WikiPlex\bin\net35\$configuration\*.pdb" `
                              "$baseDir\WikiPlex\bin\net35\$configuration\*.xml" `
                              "$baseDir\License.txt"
    }
    
    exec { .\lib\zip.exe -9 -A -j -q `
                              "$archiveDir\WikiPlex-net40.zip" `
                              "$baseDir\WikiPlex\bin\$configuration\*.dll" `
                              "$baseDir\WikiPlex\bin\$configuration\*.pdb" `
                              "$baseDir\WikiPlex\bin\$configuration\*.xml" `
                              "$baseDir\License.txt"
    }
    
    exec { .\lib\zip.exe -9 -A -r -q `
                              "$archiveDir\WikiPlex-Sample.zip" `
                              "Sample" `
                              "Sample-Readme.txt" `
                              "License.txt"
    }
    
    exec { .\lib\zip.exe -9 -A -r -q `
                              "$archiveDir\WikiPlex-NuGet.zip" `
                              "_nuget"
    }
    
    exec { &"$baseDir\.nuget\NuGet.exe" pack "$nugetDir\WikiPlex.nuspec" >> $NULL }
    move-item "$baseDir\*.nupkg" -destination $archiveDir
    
    clean $sampleDir, $nugetDir
}

task prepare-sample {   
    copy-item "$baseDir\WikiPlex.Web.Sample" -destination $sampleDir -recurse -container
    copy-item "$baseDir\GlobalAssemblyInfo.cs" -destination "$sampleDir\Properties"
    
    $csproj = "$sampleDir\WikiPlex.Web.Sample.csproj"
    
    regex-replace $csproj '(?m)Include="\.\.\\GlobalAssemblyInfo.cs"' 'Include="Properties\GlobalAssemblyInfo.cs"'
    regex-replace $csproj '(?m)<Link>Properties\\GlobalAssemblyInfo.cs</Link>' ''
    regex-replace $csproj '(?ms)<ProjectReference Include="\.\.\\WikiPlex\\WikiPlex\.csproj">.+?</ProjectReference>' '<Reference Include="WikiPlex" />'
}

task prepare-nuget {
    $nuget20 = "$nugetDir\lib\Net20"
    $nuget40 = "$nugetDir\lib\Net40"
    $nuspec = "$baseDir\WikiPlex.nuspec"
    
    create $nugetDir, $nuget20, $nuget40
    
    copy-item "$baseDir\License.txt", $nuspec -destination $nugetDir
    copy-item "$baseDir\WikiPlex\bin\net35\$configuration\WikiPlex.*" -destination $nuget20
    copy-item "$baseDir\WikiPlex\bin\$configuration\WikiPlex.*" -destination $nuget40
    
    $version = new-object -TypeName System.Version -ArgumentList $buildNumber
    regex-replace "$nugetDir\WikiPlex.nuspec" '(?m)@Version@' $version.ToString(2)
}

task prepare-documentation -depends run-build {
    clean $helpDir
    create $helpDir
    
    copy-item "$baseDir\WikiPlex\bin\$configuration\*.dll", "$baseDir\WikiPlex\bin\$configuration\*.xml" -destination "$baseDir\WikiPlex.Documentation"
    copy-item "$baseDir\WikiPlex\bin\$configuration\*.dll", "$baseDir\lib\WikiMaml\*.*" -destination $helpDir
    
    exec { .\_help\WikiMaml.Console.exe "$baseDir\WikiPlex.Documentation\Source" "$baseDir\WikiPlex.Documentation" }
}

task build-documentation -depends prepare-documentation {
    exec { msbuild $docSlnPath /p:"Configuration=$configuration;Platform=AnyCpu;OutDir=$helpDir" }
}

task clean-documentation-files {
    $docPath = "$baseDir\WikiPlex.Documentation"
    foreach ($path in get-childitem $docPath -include *.dll, *.xml, *.aml -recurse) { remove-item $path }
    foreach ($path in get-childitem $docPath -exclude Source | where-object { $_.PSIsContainer }) { remove-item $path }
}

function global:execute-tests($assembly, $message) {
    exec { .\lib\xUnit\xunit.console.clr4.x86.exe $assembly } "Failure running $message"
}

function global:clean([string[]]$paths) {
	foreach ($path in $paths) {
		remove-item -force -recurse $path -ErrorAction SilentlyContinue
	}
}

function global:create([string[]]$paths) {
  foreach ($path in $paths) {
    new-item -path $path -type directory | out-null
  }
}

function global:regex-replace($filePath, $find, $replacement) {
    $regex = [regex] $find
    $content = [System.IO.File]::ReadAllText($filePath)
    
    Assert $regex.IsMatch($content) "Unable to find the regex '$find' to update the file '$filePath'"
    
    [System.IO.File]::WriteAllText($filePath, $regex.Replace($content, $replacement))
}

function global:roboexec([scriptblock]$cmd) {
    & $cmd | out-null
    if ($lastexitcode -eq 0) { throw "No files were copied for command: " + $cmd }
}