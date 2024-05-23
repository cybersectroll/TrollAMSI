# TrollAMSI


# Benefits
- No P/Invoke or win32 API calls
- No amsi.dll patching
  
# Usage 

## IEX 
```
>c:\Windows\Microsoft.NET\Framework64\v4.0.30319\csc.exe /t:library /out:TrollAMSI.dll WorkingAMSIbypass.cs /reference:c:\Windows\assembly\GAC_MSIL\System.Management.Automation\1.0.0.0__31bf3856ad364e35\System.Management.Automation.dll
>[System.Reflection.Assembly]::Load([System.IO.File]::ReadAllBytes("C:\TrollAMSI.dll"))
>[Tro
```
## Compiled
```
>c:\Windows\Microsoft.NET\Framework64\v4.0.30319\csc.exe /t:library /out:TrollAMSI.dll WorkingAMSIbypass.cs /reference:c:\Windows\assembly\GAC_MSIL\System.Management.Automation\1.0.0.0__31bf3856ad364e35\System.Management.Automation.dll
>[System.Reflection.Assembly]::Load([System.IO.File]::ReadAllBytes("C:\TrollAMSI.dll"))
>[TrollAMSI]::troll()
```

# OPSEC
## Static 
- obfuscate "AmsiUtils" and "ScanContent" maybe?
## Dynamic
- Nothing much really
  
# Wishlist - Project was done over overnight and I have no time/intent to pursue the following:
- same technique should be applicable to ETW and CLM, maybe there's more?
  
# Disclaimer
Should only be used for educational purposes!
