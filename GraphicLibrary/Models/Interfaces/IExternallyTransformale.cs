using OpenTK.Mathematics;

namespace GraphicLibrary.Models.Interfaces
{
    public interface IExternallyTransformable
    {
        void Move(Vector3 shifts);
        void Scale(Size scale);
        void Rotate(RotationAngles angles);
    }
}
