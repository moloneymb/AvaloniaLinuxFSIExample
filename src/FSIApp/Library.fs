module FSIApp 
open Avalonia
open Avalonia.Markup.Xaml.Styling
open System
open System.Threading


// Without this you will get a blank screen
type App() =
    inherit Application()
    override this.Initialize() = 
        let baseUri = Uri("resm:base")
        [
            "resm:Avalonia.Themes.Default.DefaultTheme.xaml?assembly=Avalonia.Themes.Default"
            "resm:Avalonia.Themes.Default.Accents.BaseLight.xaml?assembly=Avalonia.Themes.Default"
        ] |> List.iter (fun x -> this.Styles.Add(StyleInclude(baseUri, Source=Uri(x))))


let createApp() = 
    let ctSource = new CancellationTokenSource()
    async {
        let app = AppBuilder.Configure<App>().UsePlatformDetect().SetupWithoutStarting()
        let application = app.Instance
        application.Run(ctSource.Token)
    } |> Async.Start
