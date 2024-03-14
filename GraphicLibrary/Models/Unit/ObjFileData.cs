using OpenTK.Mathematics;

namespace GraphicLibrary.Models.Unit
{
    /// <summary>
    /// Структура, хранящая информацию об одном объекте из .obj файла
    /// </summary>
    public struct ObjFileData : ICloneable
    {
        /// <summary>
        /// Имя объекта
        /// </summary>
        public string? Name;
        /// <summary>
        /// Имя материала
        /// </summary>
        public string? MaterialName;
        /// <summary>
        /// Список вершин
        /// </summary>
        public List<Vector3> Vertices;
        /// <summary>
        /// Список координат текстур
        /// </summary>
        public List<Vector2> Textures;
        /// <summary>
        /// Список нормалей
        /// </summary>
        public List<Vector3> Normals;
        /// <summary>
        /// Список полигонов
        /// </summary>
        public List<Vector3i> Faces;


        /// <summary>
        /// Конструктор по умолчанию
        /// </summary>
        public ObjFileData()
        {
            Name = null;
            MaterialName = null;
            Vertices = new List<Vector3>();
            Textures = new List<Vector2>();
            Normals = new List<Vector3>();
            Faces = new List<Vector3i>();
        }
        /// <summary>
        /// Конструктор структуры ObjFileData
        /// </summary>
        /// <param name="name"> имя объекта </param>
        /// <param name="materialName"> имя материала </param>
        /// <param name="vertices"> списк вершин </param>
        /// <param name="textures"> список координат текстур </param>
        /// <param name="normals"> список нормалей </param>
        /// <param name="faces"> список полигонов </param>
        public ObjFileData(string? name, string? materialName,
            List<Vector3> vertices, List<Vector2> textures,
            List<Vector3> normals, List<Vector3i> faces)
        {
            Name = name;
            MaterialName = materialName;
            Vertices = new List<Vector3>(vertices);
            Textures = new List<Vector2>(textures);
            Normals = new List<Vector3>(normals);
            Faces = new List<Vector3i>(faces);
        }
        /// <summary>
        /// Очищение всех списков
        /// </summary>
        public void ClearLists()
        {
            Vertices.Clear();
            Textures.Clear();
            Normals.Clear();
            Faces.Clear();
        }
        /// <summary>
        /// Копирование этого объекта
        /// </summary>
        /// <returns> копия объекта </returns>
        public object Clone()
        {
            return new ObjFileData(Name, MaterialName, Vertices, Textures, Normals, Faces);
        }
    }
}
