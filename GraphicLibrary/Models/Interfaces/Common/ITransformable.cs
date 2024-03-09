using GraphicLibrary.Models.Information;
using OpenTK.Mathematics;

namespace GraphicLibrary.Models.Interfaces.Common
{
    public interface ITransformable
    {
        void Move(Vector3 shifts);
        void Scale(Size scale);
        void Rotate(RotationAngles angles);
    }
}
