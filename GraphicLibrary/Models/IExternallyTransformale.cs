using OpenTK.Mathematics;

namespace GraphicLibrary.Models
{
    public interface IExternallyTransformable
    {
        void Move(Vector3 shifts);
        void Scale(Size scale);
        void Rotate(RotationAngles angles);
    }
}
