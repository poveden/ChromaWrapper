using System;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using ChromaWrapper;
using ChromaWrapper.ChromaLink;
using ChromaWrapper.Keyboard;
using ChromaWrapper.Mouse;

namespace Sandbox
{
    internal static class Program
    {
        private static readonly ChromaColor _red = ChromaColor.FromColor(Color.Red);
        private static readonly ChromaColor _green = ChromaColor.FromRgb(0, 255, 0);
        private static readonly ChromaColor _blue = ChromaColor.FromColor(Color.Blue);

        private static readonly ChromaColor _cyan = ChromaColor.FromColor(Color.Cyan);
        private static readonly ChromaColor _magenta = ChromaColor.FromColor(Color.Magenta);
        private static readonly ChromaColor _yellow = ChromaColor.FromColor(Color.Yellow);

        private static void Main(string[] args)
        {
            // var wex = new Win32Exception(1235); // { HResult = unchecked((int)0x800704D3) };

            for (uint high = 0x8000_0000; high <= 0x8013_0000; high += 0x1_000)
            {
                foreach (ChromaResult cr in Enum.GetValues<ChromaResult>().Where(x => (int)x > 0))
                {
                    uint low = (uint)cr & 0x0000ffff;

                    //int hresult = (int)cr <= 0 ? (int)cr : (int)(0x80070000 | ((uint)cr & 0x0000ffff));

                    int hresult = (int)(high | low);
                    Exception? ex = System.Runtime.InteropServices.Marshal.GetExceptionForHR(hresult);

                    if (ex is System.Runtime.InteropServices.COMException)
                    {
                        continue;
                    }

                    Debug.WriteLine($"{hresult:X8} {cr} -> {ex?.GetType().FullName ?? "(No exception)"}");
                }
            }

            Debug.Write("Initializing Chroma SDK...");
            var tcs = new TaskCompletionSource<bool>();

            var ai = new ChromaAppInfo
            {
                Title = "ChromaWrapper Sandbox",
                Description = "A tool to test the ChromaWrapper library.",
                AuthorName = "Jorge Poveda Coma",
                AuthorContact = "https://github.com/poveden/ChromaWrapper",
                SupportedDevice = ChromaAppInfoDevices.All,
                Category = ChromaAppInfoCategory.Utility,
            };

            using var sdk = new ChromaSdk(ai);
            //sdk.RegisterEventNotification();

            sdk.DeviceAccess += (object? sender, ChromaDeviceAccessEventArgs e) =>
            {
                tcs.SetResult(e.AccessGranted);
            };

            //var t = Task.Run(() =>
            //{
            //    //sdk.RegisterEventNotification((IntPtr)123123);
            //    sdk.RegisterEventNotification();
            //    Application.Run();
            //});

            _ = tcs.Task.Wait(2000);

            Debug.WriteLine(" Done.");

            foreach (var fi in typeof(ChromaDeviceIds).GetFields(BindingFlags.Public | BindingFlags.Static))
            {
                var id = (Guid)fi.GetValue(null)!;
                var di = sdk.QueryDevice(id);

                Debug.WriteLine($"{fi.Name}: {di.Connected} - {di.DeviceType}");
            }

            var kse = new StaticKeyboardEffect
            {
                Color = _green,
            };
            var idKse = sdk.CreateEffect(kse);
            sdk.SetEffect(idKse);

            bool ok = ChromaSdk.IsSdkAvailable();

            foreach (var led in Enum.GetValues<MouseLed>())
            {
                var me = new CustomV2MouseEffect();
                me.Color[led] = _green;
                var idM = sdk.CreateEffect(me);
                sdk.SetEffect(idM);

                Thread.Sleep(100);
            }

            var e = new CustomV2KeyboardEffect();

            e.Key[KeyboardKey.I] = _red;
            e.Color[2, 3] = _cyan;
            var idR = sdk.CreateEffect(e);

            e = new CustomV2KeyboardEffect();
            e.Key[KeyboardKey.O] = _green;
            e.Color[2, 4] = _magenta;
            var idG = sdk.CreateEffect(e);

            e = new CustomV2KeyboardEffect();
            e.Key[KeyboardKey.P] = _blue;
            e.Color[2, 5] = _yellow;
            var idB = sdk.CreateEffect(e);

            var f = new CustomChromaLinkEffect();

            f.Color[0] = _red;
            f.Color[1] = _cyan;
            var idRf = sdk.CreateEffect(f);

            f.Color[0] = _green;
            f.Color[1] = _magenta;
            var idGf = sdk.CreateEffect(f);

            f.Color[0] = _blue;
            f.Color[1] = _yellow;
            var idBf = sdk.CreateEffect(f);

            //var f = new ChromaWrapper.ChromaLink.STATIC_EFFECT_TYPE();

            //f.Color = COLORREF.Red;
            //var idRf = sdk.CreateEffect(f);

            //f.Color = COLORREF.Green;
            //var idGf = sdk.CreateEffect(f);

            //f.Color = COLORREF.Blue;
            //var idBf = sdk.CreateEffect(f);

            do
            {
                sdk.SetEffect(idR);
                sdk.SetEffect(idRf);
                Thread.Sleep(100);
                //if (Console.ReadKey().Key == ConsoleKey.Escape) { break; }
                sdk.SetEffect(idG);
                sdk.SetEffect(idGf);
                Thread.Sleep(100);
                //if (Console.ReadKey().Key == ConsoleKey.Escape) { break; }
                sdk.SetEffect(idB);
                sdk.SetEffect(idBf);
                Thread.Sleep(100);
                //if (Console.ReadKey().Key == ConsoleKey.Escape) { break; }
            }
            while (/*Console.In.Peek() == -1*/!Console.KeyAvailable);

            sdk.DeleteEffect(idR);
            sdk.DeleteEffect(idG);
            sdk.DeleteEffect(idB);
            sdk.DeleteEffect(idRf);
            sdk.DeleteEffect(idGf);
            sdk.DeleteEffect(idBf);
        }
    }
}
