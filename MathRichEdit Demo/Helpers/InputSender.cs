using System;
using System.Runtime.InteropServices;

namespace MathRichEdit_Demo.Helpers;

/// <summary>
/// Provides low-level methods for simulating Unicode keyboard input using the Win32 <c>SendInput</c> API.
/// </summary>
/// <remarks>
/// This static class enables injection of Unicode characters into the input stream of the active window,
/// bypassing keyboard layouts and allowing direct control of input simulation. It uses native interop with
/// the Win32 <c>SendInput</c> function and constructs keyboard input events for both key press and key release.
/// 
/// Internally, it uses P/Invoke with tightly packed structs matching the Windows API layout for <c>INPUT</c>,
/// <c>KEYBDINPUT</c>, <c>MOUSEINPUT</c>, and <c>HARDWAREINPUT</c>. Only keyboard input is utilized for Unicode
/// character simulation.
/// 
/// This is useful for scenarios such as automated testing, input automation, on-screen keyboards,
/// or controlled text injection in custom applications.
/// </remarks>
static class InputSender
{
    [StructLayout(LayoutKind.Sequential)]
    struct INPUT
    {
        public uint type;
        public InputUnion u;
    }

    [StructLayout(LayoutKind.Explicit)]
    struct InputUnion
    {
        [FieldOffset(0)] public MOUSEINPUT mi;
        [FieldOffset(0)] public KEYBDINPUT ki;
        [FieldOffset(0)] public HARDWAREINPUT hi;
    }

    [StructLayout(LayoutKind.Sequential)]
    struct MOUSEINPUT
    {
        public int dx;
        public int dy;
        public uint mouseData;
        public uint dwFlags;
        public uint time;
        public IntPtr dwExtraInfo;
    }

    [StructLayout(LayoutKind.Sequential)]
    struct KEYBDINPUT
    {
        public ushort wVk;
        public ushort wScan;
        public uint dwFlags;
        public uint time;
        public IntPtr dwExtraInfo;
    }

    [StructLayout(LayoutKind.Sequential)]
    struct HARDWAREINPUT
    {
        public uint uMsg;
        public ushort wParamL;
        public ushort wParamH;
    }

    const uint INPUT_KEYBOARD = 1;
    const uint KEYEVENTF_KEYUP = 0x0002;
    const uint KEYEVENTF_UNICODE = 0x0004;

    [DllImport("user32.dll", SetLastError = true)]
    static extern uint SendInput(uint nInputs, INPUT[] pInputs, int cbSize);

    /// <summary>
    /// Sends a single Unicode character as keyboard input to the active window.
    /// </summary>
    /// <param name="character">The Unicode character to send.</param>
    /// <remarks>
    /// This method uses the SendInput API to simulate keyboard input for Unicode characters,
    /// witout the need to switch keyboard layouts or input methods.
    /// 
    /// - The first input simulates a key press (keydown) using the Unicode scan code.
    /// - The second input simulates a key release (keyup) to complete the character entry.
    /// 
    /// If the input fails, it throws a Win32 exception with the system error code.
    /// </remarks>
    public static void SendUnicodeCharacter(char character)
    {
        INPUT[] inputs = new INPUT[2];
        int structSize = Marshal.SizeOf(typeof(INPUT));

        // Key Down (Unicode)
        inputs[0] = new INPUT
        {
            type = INPUT_KEYBOARD,
            u = new InputUnion
            {
                ki = new KEYBDINPUT
                {
                    wVk = 0, // Virtual key is not used for Unicode input
                    wScan = character, // Unicode character to send
                    dwFlags = KEYEVENTF_UNICODE, // Unicode flag for key down
                    time = 0,
                    dwExtraInfo = IntPtr.Zero
                }
            }
        };

        // Key Up (Unicode)
        inputs[1] = new INPUT
        {
            type = INPUT_KEYBOARD,
            u = new InputUnion
            {
                ki = new KEYBDINPUT
                {
                    wVk = 0,
                    wScan = character,
                    dwFlags = KEYEVENTF_UNICODE | KEYEVENTF_KEYUP, // Unicode key up event
                    time = 0,
                    dwExtraInfo = IntPtr.Zero
                }
            }
        };

        uint result = SendInput((uint)inputs.Length, inputs, structSize);

        if (result == 0)
        {
            int error = Marshal.GetLastWin32Error();
            throw new System.ComponentModel.Win32Exception(error);
        }
    }

    /// <summary>
    /// Sends a sequence of Unicode characters individually.
    /// </summary>
    /// <param name="unicodeText">The Unicode string to be sent character by character.</param>
    /// <remarks>
    /// This method uses <c>SendUnicodeCharacter()</c> to simulate keyboard input for each character
    /// in the provided string using the Win32 <c>SendInput</c> API.
    /// 
    /// Ideal for scenarios where text integrity is critical, such as math editors, programming environments, or input automation.
    /// 
    /// If a character fails to send, a detailed exception is thrown with the Unicode code point that caused the failure.
    /// </remarks>
    public static void SendUnicodeText(string unicodeText)
    {
        foreach (char c in unicodeText)
        {
            try
            {
                SendUnicodeCharacter(c);
            }
            catch (System.ComponentModel.Win32Exception ex)
            {
                throw new Exception(
                    $"Failed to send character '{c}' (U+{((int)c):X4}). Unicode text input aborted.",
                    ex
                );
            }
        }
    }
}
