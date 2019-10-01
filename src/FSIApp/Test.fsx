// NOTE This needs to be run on netcoreapp3.0 i.e. `dotnet /usr/share/dotnet/sdk/3.0.100/FSharp/fsi.exe`
// NOTE If you get a Posix error it can be ignored. The fix did not make the 3.0 release.
// NOTE The WriteableBitmap appears to currently be bugged in Linux on SkiaSharp

#I "./bin/Debug/netcoreapp3.0/publish"
#r "System.Reactive.dll" ;;
#r "Avalonia.Animation.dll" ;;
#r "Avalonia.Base.dll";;
//#r "Avalonia.Build.Tasks.dll";;
//#r "Avalonia.Controls.DataGrid.dll";;
#r "Avalonia.Controls.dll";;
#r "Avalonia.DesignerSupport.dll";;
#r "Avalonia.Desktop.dll";;
#r "Avalonia.DesktopRuntime.dll";;
#r "Avalonia.Diagnostics.dll";;
//#r "Avalonia.Dialogs.dll";;
#r "Avalonia.Direct2D1.dll";;
#r "Avalonia.dll";;
//#r "Avalonia.FreeDesktop.dll";;
#r "Avalonia.Input.dll";;
#r "Avalonia.Interactivity.dll";;
#r "Avalonia.Layout.dll";;
//#r "Avalonia.LinuxFramebuffer.dll";;
#r "Avalonia.Logging.Serilog.dll";;
#r "Avalonia.Markup.dll";;
#r "Avalonia.Markup.Xaml.dll";;
#r "Avalonia.Native.dll";;
#r "Avalonia.OpenGL.dll";;
#r "Avalonia.Remote.Protocol.dll";;
#r "Avalonia.Skia.dll";;
#r "Avalonia.Styling.dll";;
#r "Avalonia.Themes.Default.dll";;
#r "Avalonia.Visuals.dll";;
#r "Avalonia.Win32.dll";;
#r "Avalonia.X11.dll";;
#r "FSIApp.dll";;

open Microsoft.FSharp.NativeInterop
open FSharp.NativeInterop
open System
open System.Threading
open System.Runtime.InteropServices
open Avalonia
open Avalonia.Controls
open Avalonia.Media.Imaging
open Avalonia.Threading

#nowarn "9";;

FSIApp.createApp();; // NOTE: Could probably be done automatically in a static constructor

type MainWindow() as this =
    inherit Window()
    do  
        this.Title <- "FSI WriteableBitmap Test"
        let wb = new WriteableBitmap(PixelSize(1000,1000),Vector(90.,90.), Nullable(Platform.PixelFormat.Bgra8888))
        let rand = Random()
        do
            use fb = wb.Lock()
            let ptr =  fb.Address |> NativePtr.ofNativeInt<uint32>
            for i in 0..1000000 do
                NativePtr.set ptr i (uint32 <| rand.Next())
        let img = Image(Height=1000.,Width=1000.,Source=wb)
        let border = Avalonia.Controls.Border(BorderThickness=Thickness(10.),BorderBrush=Media.Brushes.Red,Child=img)
        //let button = Button(Content="click me")
        //button.Click.Add(fun _ -> printfn "clicked")
        this.Content <- border
;;

Dispatcher.UIThread.Post(Action(fun () -> MainWindow().Show()),DispatcherPriority.Send);;

printfn "Press Any Key To Exit"
Console.Read()