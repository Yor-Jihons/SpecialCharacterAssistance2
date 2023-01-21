@echo off

if "%1" == "debug" (
    dotnet build -c Debug -nologo
) else (
    if "%1" == "release" (
        dotnet build -c Release -nologo
    ) else (
        if "%1" == "publish" (
            dotnet publish -o .\bin\Publish -c Release --self-contained true -r win-x64 -nologo
        ) else (
            if "%1" == "md" (
                Markdown2HTML --list=".\doc\definitions_list.txt" --template=".\doc\definitions_src\template.html"
            ) else (
                echo Wrong commandline arguments for compile.bat
                echo You should restart compile.bat
                echo CMD: $ compile { debug, release, publish, md }
            )
        )
    )
)