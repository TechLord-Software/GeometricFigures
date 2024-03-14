using GraphicLibrary.Materials;
using OpenTK.Mathematics;

namespace GraphicLibrary.Models.Unit
{
    public interface IModelUnit
    {
        public string Name { get; set; }
        public Material Material { get; set; }
        public Matrix4 RotationMatrix { get; }
        public Matrix4 TranslationMatrix { get; }
        public Matrix4 ScaleMatrix { get; }
        public Matrix4 ModelMatrix { get; }
    }
}
