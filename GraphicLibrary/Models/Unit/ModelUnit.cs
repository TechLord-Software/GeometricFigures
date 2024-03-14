﻿using GraphicLibrary.GLObjects;
using GraphicLibrary.Materials;
using GraphicLibrary.Models.Interfaces.Common;
using GraphicLibrary.Shaders;
using OpenTK.Mathematics;

namespace GraphicLibrary.Models.Unit
{
    /// <summary>
    /// Класс трехмерной модели
    /// </summary>
    public class ModelUnit : TransformableModelUnit, IDrawable, ICloneable
    {
        /// <summary>
        /// Объект, хранящий вершины модели
        /// </summary>
        private VertexBufferObject _vboVertices;
        /// <summary>
        /// Объект, хранящий нормали каждой из вершин
        /// </summary>
        private VertexBufferObject _vboNormals;
        /// <summary>
        /// Объект, хранящий координаты текстур
        /// </summary>
        private VertexBufferObject _vboTextures;
        /// <summary>
        /// Объект, задающий правила передачи данных из объекта vbo в шейдеры
        /// </summary>
        private VertexArrayObject _vao;
        /// <summary>
        /// Объект, хранящий индексы вершин
        /// </summary>
        private ElementBufferObject _ebo;



        /// <summary>
        /// Конструктор класса модели
        /// </summary>
        /// <param name="vertices"> вершины </param>
        /// <param name="indices"> индексы вершин </param>
        /// <param name="normals"> нормали вершин </param>
        /// <param name="material"> материал модели </param>
        public ModelUnit(float[] vertices, float[] textures, float[] normals, uint[] indices, Material material, string name)
            : base(name, material)
        {
            _vboVertices = new VertexBufferObject(vertices);
            _vboTextures = new VertexBufferObject(textures);
            _vboNormals = new VertexBufferObject(normals);
            _vao = new VertexArrayObject();
            _ebo = new ElementBufferObject(indices);

            _vao.Activate();

            _vboVertices.Activate();
            _vao.AttribPointer(Shader.VertexLocation, Shader.VertexCount, Shader.VertexCount, 0);

            _vboNormals.Activate();
            _vao.AttribPointer(Shader.NormalLocation, Shader.NormalCount, Shader.NormalCount, 0);


            VertexBufferObject.DeactivateCurrent();
            VertexArrayObject.DeactivateCurrent();
            ElementBufferObject.DeactivateCurrent();
        }
        public ModelUnit(float[] vertices, float[] textures, float[] normals, uint[] indices, Material material)
            : this(vertices, textures, normals, indices, material, DEFAULT_NAME) { }
        public ModelUnit(float[] vertices, float[] textures, float[] normals, uint[] indices)
            : this(vertices, textures, normals, indices, Material.Default) { }




        /// <summary>
        /// Конструктор копирования
        /// </summary>
        /// <param name="unit"> простая модель </param>
        private ModelUnit(ModelUnit unit)
        {
            Material = unit.Material;
            Name = unit.Name;

            rotationMatrix = unit.RotationMatrix;
            translationMatrix = unit.TranslationMatrix;
            scaleMatrix = unit.ScaleMatrix;

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
        /// Копирует этот объект
        /// </summary>
        /// <returns> копия этого объекта </returns>
        public object Clone()
        {
            return new ModelUnit(this);
        }
        /// <summary>
        /// Создает объект по данным из структур ObjFileData и Material
        /// </summary>
        /// <param name="objData"> данные .obj файла </param>
        /// <param name="material"> материал </param>
        /// <returns> новый объект ModelUnit </returns>
        public static ModelUnit Create(ObjFileData objData, Material material)
        {
            var vertices = new List<float>();
            var textures = new List<float>();
            var normals = new List<float>();
            var indices = new List<uint>();

            var faces = new Dictionary<Vector3, uint>();

            void AddItemsToList<T>(List<T> list, params T[] items)
            {
                foreach (var item in items)
                    list.Add(item);
            }

            foreach (var face in objData.Faces)
            {
                if (faces.ContainsKey(face))
                {
                    indices.Add(faces[face]);
                }
                else
                {
                    uint index = (uint)vertices.Count / 3;
                    faces[face] = index;
                    indices.Add(index);

                    Vector3 vertex = objData.Vertices[face.X];
                    Vector2 texture = objData.Textures[face.Y];
                    Vector3 normal = objData.Normals[face.Z];

                    AddItemsToList(vertices, vertex.X, vertex.Y, vertex.Z);
                    AddItemsToList(textures, texture.X, texture.Y);
                    AddItemsToList(normals, normal.X, normal.Y, normal.Z);
                }
            }
            return new ModelUnit(vertices.ToArray(), textures.ToArray(), 
                normals.ToArray(), indices.ToArray(), 
                material, objData.Name ?? DEFAULT_NAME);
        }
    }
}
