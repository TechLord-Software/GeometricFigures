using GraphicLibrary.ComplexModels;
using GraphicLibrary.LightSources;
using GraphicLibrary.Models.Information;
using GraphicLibrary.Scenes;
using OpenTK.Mathematics;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;

namespace ConsoleApp1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var nativeWindowSettings = new NativeWindowSettings()
            {
                Profile = ContextProfile.Compatability,
                Size = new Vector2i(900, 900),
            };
            using (var testWindow = new Scene(GameWindowSettings.Default, nativeWindowSettings))
            {
                testWindow.BackgroundColor = new Vector4(0.2f, 0.2f, 0.2f, 1f);
                var model = Model.Parse(@"C:\Users\d3nis\OneDrive\Рабочий стол\1.obj");
                model.Models[0].Material.PhongParameters.Diffuse = new Vector3(0, 1, 0);
                
                testWindow.AddModel(model);
                testWindow.AddLightSource(LightSource.Default);
                testWindow.LightSources[0].Move(new Vector3(3, 3, 3));
                testWindow.Run();
            }
        }
    }
}
