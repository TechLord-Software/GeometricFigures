using OpenTK.Windowing.Common;
using OpenTK.Windowing.GraphicsLibraryFramework;

namespace GraphicLibrary.Cameras.Interfaces
{
    public interface IMouseMove
    {
        float MouseSensitivity { get; set; }
        void MouseMove(MouseState mousePosition, MouseButtonEventArgs e);
    }
}
