# SpecialCharacterAssistance2

This is the input assistance for special characters like "д" as a desktop application (for Windows OS).

## 1. Requirements

- dotnet 6.0.202
- Windows OS (later 7)
- [NLog - NuGet Gallery](https://www.nuget.org/packages/NLog/)

## 2. Install

Step 1. Run the bat-file init.bat.

```
$ init
```
Step 2. Run the bat-file commpile.bat with a command-line argument.
You can pass the arguments { "debug" | "release" | "publish" }.
You can also run dotnet as usual.

```
$ compile publish
```

or like

```
$ dotnet publish -o .\bin\Publish -c Release --self-contained true -r win-x64 -nologo
```

## 3. Usage



## 4. Licenses

This library is released under the MIT License. See also [LICENCE which included](./LICENSE) or [on GitHub](https://github.com/Yor-Jihons/SpecialCharacterAssistance2/blob/main/SpecialCharacterAssistance2/LICENSE).

[NLog - NuGet Gallery](https://www.nuget.org/packages/NLog/) is under the BSD-3-Clause license.

The image files in the directory "main/res/Frames" are downloaded from [GAHAG | 著作権フリー写真・イラスト素材集](https://gahag.net/), and under the [CC0](https://creativecommons.org/share-your-work/public-domain/cc0) License.

## 5. Development Environment

- dotnet on Windows
- Language: C#
- Framework: WPF

## 6. Changes


## 7. Contact

Author: Yor-Jihons  
GitHub: [SpecialCharacterAssistance2](https://github.com/Yor-Jihons/SpecialCharacterAssistance2)  
