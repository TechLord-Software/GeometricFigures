using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphicLibrary.ComplexModels
{
    public partial class Model
    {       
        private static string circlePath = @"ComplexModels\Figures\circle.obj";
        private static string conePath = @"ComplexModels\Figures\Cone.obj";
        private static string cameraPath = @"ComplexModels\Figures\camera.obj";
        private static string cubePath = @"ComplexModels\Figures\cube.obj";
        private static string cylinderPath = @"ComplexModels\Figures\Cylinder.obj";
        private static string icosahedronPath = @"ComplexModels\Figures\Icosahedron.obj";
        private static string octahedronPath = @"ComplexModels\Figures\Octahedron.obj";
        private static string sphere1Path = @"ComplexModels\Figures\Sphere1.obj";
        private static string sphere2Path = @"ComplexModels\Figures\Sphere2.obj";
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
        private static Model sphere1 = Model.Parse(sphere1Path);
        private static Model sphere2 = Model.Parse(sphere2Path);
        private static Model icosahedron = Model.Parse(icosahedronPath);
        private static Model square = Model.Parse(squarePath);
        private static Model tetrahedron = Model.Parse(tetrahedronPath);
        private static Model thor = Model.Parse(thorPath);
        private static Model triangle = Model.Parse(trianglePath);

        public static Model Circle { get; }
        public static Model Cone { get; }
        public static Model Camera { get; }
        public static Model Cube { get; }
        public static Model Cylinder { get; }
        public static Model Octahedron { get; }
        public static Model Sphere1 { get; }
        public static Model Sphere2 { get; }
        public static Model Icosahedron { get; }
        public static Model Square { get; }
        public static Model Tetrahedron { get; }
        public static Model Thor { get; }
        public static Model Triangle { get; }

        static Model()
        {
            Circle = (Model)circle.Clone();
            Cone = (Model)cone.Clone();
            Camera = (Model)camera.Clone();
            Cube = (Model)cube.Clone();
            Cylinder = (Model)cylinder.Clone();
            Octahedron = (Model)octahedron.Clone();
            Sphere1 = (Model)sphere1.Clone();
            Sphere2 = (Model)sphere2.Clone();
            Icosahedron = (Model)icosahedron.Clone();
            Square = (Model)square.Clone();
            Tetrahedron = (Model)tetrahedron.Clone();
            Thor = (Model)thor.Clone();
            Triangle = (Model)triangle.Clone();
        }
    }
}
