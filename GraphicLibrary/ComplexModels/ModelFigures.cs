using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphicLibrary.ComplexModels
{
    public partial class ModelFigures
    {       
        private static string circlePath = @"ComplexModels\Figures\circle.obj";
        private static string conePath = @"ComplexModels\Figures\Cone.obj";
        private static string cameraPath = @"ComplexModels\Figures\camera.obj";
        private static string cubePath = @"ComplexModels\Figures\cube.obj";
        private static string cylinderPath = @"ComplexModels\Figures\Cylinder.obj";
        private static string icosahedronPath = @"ComplexModels\Figures\Icosahedron.obj";
        private static string octahedronPath = @"ComplexModels\Figures\Octahedron.obj";
        private static string spherePath = @"ComplexModels\Figures\Sphere.obj";
        private static string squarePath = @"ComplexModels\Figures\square.obj";
        private static string tetrahedronPath = @"ComplexModels\Figures\Tetrahedron.obj";
        private static string thorPath = @"ComplexModels\Figures\Thor.obj";
        private static string trianglePath = @"ComplexModels\Figures\triangle.obj";

        private static Model circle = Model.Parse(circlePath);
        private static Model cone = Model.Parse(conePath);
        private static Model camera = Model.Parse(cameraPath);
        private static Model cube = Model.Parse(cubePath);
        private static Model cylinder = Model.Parse(cylinderPath);
        private static Model octahedron = Model.Parse(octahedronPath);
        private static Model sphere = Model.Parse(spherePath);
        private static Model icosahedron = Model.Parse(icosahedronPath);
        private static Model square = Model.Parse(squarePath);
        private static Model tetrahedron = Model.Parse(tetrahedronPath);
        private static Model thor = Model.Parse(thorPath);
        private static Model triangle = Model.Parse(trianglePath);

        public Model Circle = circle;
    }
}
