## A practical WinUI 3 demo showcasing `RichEditBox` in `MathMode`.
This app demonstrates how to enable and automate math input in a WinUI 3 application using `RichEditBox` with `RichEditMathMode` (introduced in Windows App SDK 1.7). It covers Nearly Plain Text (NPT) Unicode input, real-time formula rendering, and programmatic Unicode injection via the Win32 `SendInput` API. Ideal for developers building math-heavy UIs, educational tools, or custom equation editors.

![image](https://github.com/user-attachments/assets/d617187b-56b4-4020-b384-4890cca41b74)

*Icon obtained from Icons8 (https://icons8.com)*

---
## Automating Math Input in WinUI 3 Using `RichEditBox` and `SendInput`: A Practical Guide to NPT and Unicode Injection

To add math input to your WinUI 3 application, start with the `RichEditBox` control. Since **Windows App SDK 1.7**, it supports a new feature called **RichEditMathMode**, which allows the control to interpret and convert text into MathML in real time. For example, typing `\pi` renders as **π**, and `1/2` becomes **½**.
```cs
MathEditor.TextDocument.SetMathMode(RichEditMathMode.MathOnly);
```
You don’t need to write MathML manually. Instead, it uses a system called **Nearly Plain Text Unicode (NPT)**, a structured input format that maps common math sequences (like `\infty`, `\sum`, `\sqrt`, etc.) to Unicode symbols. These symbols are then rendered as MathML behind the scenes.

Why is this necessary? Because many math symbols (like integrals, Greek letters, or arrows) aren’t available on any physical keyboard. NPT acts as a semantic shorthand, making it possible to input complex formulas with plain text.

There are two things you must understand to use this effectively:

1. Learn the available NPT input sequences (Unicode math tables).
2. Trigger rendering by sending a `U+0020` (space) or pressing Enter, this finalizes the math input and forces the control to parse and render it properly.

![gif](https://github.com/user-attachments/assets/b03158df-0682-4f01-8933-8b5445e962fb)

The problem is, if you're building an app that involves math input, **you can’t expect users to memorize the entire NPT Unicode spec**, unless your target audience is a group of monks who’ve taken a vow to speak only in NPT Unicode 😂.

Let’s be real: users want to click `π` button and see π, not study a Unicode textbook.

So, what’s the solution? You do the typing for them.

But here’s the catch: **WinUI 3 / Windows App SDK doesn’t provide a built-in way to inject raw Unicode characters into a `RichEditBox`**, especially not in a way that triggers MathMode rendering.

That’s where the **Win32 `SendInput` API** comes in.

`SendInput` is a low-level Windows API that lets you simulate **keyboard or mouse input**, just as if the user typed it. It bypasses the limitations of the high-level UI frameworks by writing directly to the input buffer of the focused window.

In this case, we use it to:

* Send Unicode characters (not keycodes or key names)
* Trigger special math input sequences programmatically
* Mimic real user input to activate RichEditBox’s MathMode rendering

Behind the scenes, we create and dispatch a series of synthetic keyboard events (keydown and keyup) for each Unicode character. This lets us inject unicode chars and sequences like `\u03C0` or `\u25A0(1&&@&1&@&&1)` directly into the control, no physical keyboard needed, no language switching required.

It’s a bit hacky, but it works flawlessly, and it’s the only reliable way to automate Unicode math input in a RichEditBox as of today (This is the best I could do, for now 😂).


### Finaly
- If you found this repo helpful, consider giving it a ⭐.
- Questions or suggestions? Feel free to open an issue.
- Contributions are welcome, just submit a pull request and I’ll review it.

---

## References
- [RichEditMathMode Enum.](https://learn.microsoft.com/en-us/windows/windows-app-sdk/api/winrt/microsoft.ui.text.richeditmathmode?view=windows-app-sdk-1.7)
- [RichEditTextDocument.SetMathMode(RichEditMathMode) Method.](https://learn.microsoft.com/en-us/windows/windows-app-sdk/api/winrt/microsoft.ui.text.richedittextdocument.setmathmode?view=windows-app-sdk-1.7)
- [UnicodeMath, A Nearly Plain-Text Encoding of Mathematics, *Murray Sargent III*.](https://www.unicode.org/notes/tn28/UTN28-PlainTextMath-v3.3.pdf)
- [You can’t simulate keyboard input with PostMessage, revisited, *The Old New Thing* By *Raymond Chen*.](https://devblogs.microsoft.com/oldnewthing/?p=110979)
- [SendInput function (winuser.h).](https://learn.microsoft.com/en-us/windows/win32/api/winuser/nf-winuser-sendinput)

---

## Search Engine Keywords
WinUI 3 RichEditBox, RichEditBox math input, Unicode math input C#, WinUI 3 math editor, RichEditBox LaTeX style input, SendInput Unicode C#, C# RichEditBox example, WinUI 3 text editor, WinUI 3 Unicode input, C# virtual keyboard input, RichEditBox Unicode characters, simulate keyboard input C#, Win32 SendInput example, math formula rendering WinUI, RichEditBox code sample, WinUI 3 custom input, C# math symbols input, RichEditBox advanced usage, input automation C#, insert math symbols RichEditBox, math structures input C#, LaTeX-like input C#, math mode RichEditBox, matrix input RichEditBox, integrals and summation input, math editor Windows app, C# math notation support

---

**Written with passion by [Zakaria Tahri](https://github.com/Zakariathr22) ❤️**

![gif](https://github.com/user-attachments/assets/ac4be485-b54e-4fa7-b359-b72a632df3a3)

