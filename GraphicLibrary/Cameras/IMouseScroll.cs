namespace GraphicLibrary.Cameras
{
    public interface IMouseScroll
    {
        float ScrollSensitivity { get; set; }
        void MouseScroll(float offset);
    }
}
