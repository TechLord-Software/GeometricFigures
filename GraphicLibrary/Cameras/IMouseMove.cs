using OpenTK.Windowing.GraphicsLibraryFramework;

namespace GraphicLibrary.Cameras
{
    public interface IMouseMove
    {
        float MouseSensitivity { get; set; }   
        void MouseMove(MouseState mousePosition);
    }
}
