using OpenTK.Mathematics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphicLibrary.Models.Figures
{
    internal class Camera
    {
        public string Name = "Camera";
        public string MaterialName = "Material";
        public List<Vector3> Vertices = new List<Vector3>()
        {

        };
        public List<Vector2> Textures;
        public List<Vector3> Normals;
        public List<Vector3i> Faces;
    }
}
