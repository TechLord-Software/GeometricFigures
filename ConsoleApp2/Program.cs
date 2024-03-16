using GraphicLibrary.Scenes;
using OpenTK.Mathematics;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;

namespace TestWindow
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
            using (var testWindow = Scene.Default)
            {
                testWindow.Run();
            }
        }
    }
}