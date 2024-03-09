using GraphicLibrary.GLObjects;
using GraphicLibrary.Materials;
using GraphicLibrary.Models.Information;
using GraphicLibrary.Models.Interfaces.Common;
using GraphicLibrary.Shaders;
using OpenTK.Mathematics;

namespace GraphicLibrary.Models.Unit
{
    /// <summary>
    /// Класс трехмерной модели
    /// </summary>
    public class ModelUnit : ITransformableModelUnit, IDrawable, ICloneable
    {
        /// <summary>
        /// Матрица поворота
        /// </summary>
        private Matrix4 _rotationMatrix;
        /// <summary>
        /// Матрица перемещения
        /// </summary>
        private Matrix4 _translationMatrix;
        /// <summary>
        /// Матрица масштабирования
        /// </summary>
        private Matrix4 _scaleMatrix;


        /// <summary>
        /// Объект, хранящий вершины модели
        /// </summary>
        private VertexBufferObject _vboVertices;
        /// <summary>
        /// Объект, хранящий нормали каждой из вершин
        /// </summary>
        private VertexBufferObject _vboNormals;
        /// <summary>
        /// Объект, задающий правила передачи данных из объекта vbo в шейдеры
        /// </summary>
        private VertexArrayObject _vao;
        /// <summary>
        /// Объект, хранящий индексы вершин
        /// </summary>
        private ElementBufferObject _ebo;




        /// <summary>
        /// Материал модели
        /// </summary>
        public Material Material { get; set; }



        public Matrix4 RotationMatrix => _rotationMatrix;
        public Matrix4 TranslationMatrix => _translationMatrix;
        public Matrix4 ScaleMatrix => _scaleMatrix;
        /// <summary>
        /// Матрица преобразования
        /// </summary>
        public Matrix4 ModelMatrix => _translationMatrix * _scaleMatrix * _rotationMatrix;


        /// <summary>
        /// Конструктор класса модели
        /// </summary>
        /// <param name="vertices"> вершины </param>
        /// <param name="indices"> индексы вершин </param>
        /// <param name="normals"> нормали вершин </param>
        /// <param name="material"> материал модели </param>
        public ModelUnit(float[] vertices, uint[] indices, float[] normals, Material material)
        {
            Material = material;

            _rotationMatrix = Matrix4.Identity;
            _translationMatrix = Matrix4.Identity;
            _scaleMatrix = Matrix4.Identity;


            _vboVertices = new VertexBufferObject(vertices);
            _vboNormals = new VertexBufferObject(normals);
            _vao = new VertexArrayObject();

            _vao.Activate();

            _vboVertices.Activate();
            _vao.AttribPointer(Shader.VertexLocation, Shader.VertexCount, Shader.VertexCount, 0);

            _vboNormals.Activate();
            _vao.AttribPointer(Shader.NormalLocation, Shader.NormalCount, Shader.NormalCount, 0);

            _ebo = new ElementBufferObject(indices);


            VertexBufferObject.DeactivateCurrent();
            VertexArrayObject.DeactivateCurrent();
            ElementBufferObject.DeactivateCurrent();
        }
        public ModelUnit(float[] vertices, uint[] indices, float[] normals)
            : this(vertices, indices, normals, Material.Default) { }


        /// <summary>
        /// Конструктор копирования
        /// </summary>
        /// <param name="unit"> простая модель </param>
        private ModelUnit(ModelUnit unit)
        {
            Material = unit.Material;

            _rotationMatrix = unit.RotationMatrix;
            _translationMatrix = unit.TranslationMatrix;
            _scaleMatrix = unit.ScaleMatrix;

            _vboVertices = unit._vboVertices;
            _vboNormals = unit._vboNormals;
            _vao = unit._vao;
            _ebo = unit._ebo;
        }


        /// <summary>
        /// Метод отрисовки модели
        /// </summary>
        public void Draw()
        {
            _vao.DrawElements(_ebo);
        }
        /// <summary>
        /// Метод перемещения модели
        /// </summary>
        /// <param name="shifts"> вектор перемещения </param>
        public void Move(Vector3 shifts)
        {
            _translationMatrix *= Matrix4.CreateTranslation(shifts);
        }
        /// <summary>
        /// Метод масштабирования модели
        /// </summary>
        /// <param name="size"> коэффициенты масштабирования </param>
        public void Scale(Size size)
        {
            _scaleMatrix *= Matrix4.CreateScale(size.X, size.Y, size.Z);
        }
        /// <summary>
        /// Метод вращения модели
        /// </summary>
        /// <param name="angles"> углы вращения </param>
        public void Rotate(RotationAngles angles)
        {
            _rotationMatrix *= Matrix4.CreateRotationX(angles.X);
            _rotationMatrix *= Matrix4.CreateRotationY(angles.Y);
            _rotationMatrix *= Matrix4.CreateRotationY(angles.Z);
        }

        public object Clone()
        {
            return new ModelUnit(this);
        }
    }
}
