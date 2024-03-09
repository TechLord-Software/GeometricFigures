namespace GraphicLibrary.Cameras.Interfaces
{
    public interface IMouseScroll
    {
        float ScrollSensitivity { get; set; }
        void MouseScroll(float offset);
    }
}
