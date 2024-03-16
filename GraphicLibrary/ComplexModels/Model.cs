using GraphicLibrary.Cameras;
using GraphicLibrary.Materials;
using GraphicLibrary.Models.Interfaces.Common;
using GraphicLibrary.Models.Unit;
using GraphicLibrary.Scenes;
using GraphicLibrary.Shaders;
using OpenTK.Mathematics;
using System.Globalization;
using System.Text.RegularExpressions;

namespace GraphicLibrary.ComplexModels
{
    /// <summary>
    /// Класс комплексной модели
    /// </summary>
    public partial class Model : TransformableModel, IDrawable, ICloneable
    {
        /// <summary>
        /// Конструктор класса Model
        /// </summary>
        public Model() : base() { }
        /// <summary>
        /// Конструктор класса Model, принимающиий одну простую модель
        /// </summary>
        /// <param name="unit"> простая модель </param>
        public Model(ModelUnit unit) : base(unit) { }
        /// <summary>
        /// Конструктор класса Model, принимающий перечисление простых объектов
        /// </summary>
        /// <param name="units"> перечисление простых объектов </param>
        public Model(IEnumerable<ModelUnit> units) : base(units) { }



        /// <summary>
        /// Метод отрисовки модели
        /// </summary>
        /// <param name="shader"> шейдер </param>
        /// <param name="scene"> текущая сцена </param>
        public void Draw(Shader shader, Scene scene)
        {
            shader.UseLightSources(scene.LightSources);
            shader.UseCamera(scene.CurrentCamera);
            foreach (var model in models)
            {
                model.Draw(shader, scene);
            }
        }

        /// <summary>
        /// Копирование объекта
        /// </summary>
        /// <returns></returns>
        public object Clone()
        {
            return new Model(models);
        }


        /// <summary>
        /// Загрузка модели из файла с расширением .obj
        /// </summary>
        /// <param name="path"> путь к файлу </param>
        /// <returns> загруженная модель </returns>
        public static Model Parse(string path)
        {
            if (!File.Exists(path)) 
                throw new ArgumentException("Такого файла не существует");
            if (Path.GetExtension(path) != ".obj") 
                throw new ArgumentException("Неправильное расширение файла");


            var (objFileData, mtlPath) = ReadObj(path);
            var materials = Material.Parse(mtlPath);
            var pairs = MakePairs(objFileData, materials);
            var units = new List<ModelUnit>();

            foreach (var pair in pairs)
            {
                var (model, material) = pair;
                units.Add(ModelUnit.Create(model, material));
            }

            return new Model(units);
        }
        /// <summary>
        /// Метод, составлюяющий пары [ObjFileData, Material] из двух коллекций каждого типа
        /// </summary>
        /// <param name="objData"> массив данных .obj файла </param>
        /// <param name="materials"> массив материалов </param>
        /// <returns> массив пар </returns>
        /// <exception cref="InvalidDataException"></exception>
        private static Tuple<ObjFileData, Material>[] MakePairs(ObjFileData[] objData, Material[] materials)
        {
            var result = new List<Tuple<ObjFileData, Material>>();
            foreach (var obj in objData)
            {
                bool found = false;
                foreach (var material in materials)
                {
                    if (obj.MaterialName == material.Name)
                    {
                        var pair = new Tuple<ObjFileData, Material>(obj, material);
                        result.Add(pair);
                        found = true;
                        continue;
                    }
                }
                if (!found) 
                    throw new InvalidDataException($"В .mtl файле нет нужного материала для объекта {obj.Name}");
            }

            return result.ToArray();
        }
        /// <summary>
        /// Чтение данных из файла с расширением .obj
        /// В .obj файле хранятся данные об объектах
        /// Объект имеет несколько текстовых параметров, основные из которых:
        /// mtllib - путь к файлу с материалами .mtl
        ///     mtllib fileName.mtl
        ///     
        /// o - название объектов
        ///     o Cube
        ///     
        /// v - координаты вершин
        ///     v 1.000000 0.000000 -1.000000
        ///     
        /// vn - нормали
        ///     vn -0.0000 1.0000 -0.0000
        ///     
        /// vt - координаты точки файла текстуры
        ///     vt 0.000000 0.000000
        ///     
        /// usemtl - имя материла
        ///     usemtl materialName
        ///     
        /// f - список, описывающий полигон с n вершинами, где n - длина списка
        /// Элементы списка имеют вид v/vt/vn и описывают вершину в полигоне
        ///     f 5/1/1 3/2/1 1/3/1
        ///     
        /// </summary>
        /// <param name="path"> путь к файлу </param>
        /// <returns> данные файла </returns>
        /// <exception cref="InvalidDataException"> ошибка данных в файле </exception>
        private static (ObjFileData[], string) ReadObj(string path)
        {
            List<ObjFileData> modelsData = new List<ObjFileData>();
            ObjFileData obj = new ObjFileData();
            Regex regex = new Regex(@"\s+");
            string? mtlFilePath = null;
            string? line;
            bool first = true;

            int verticesShift = 0;
            int normalsShift = 0;
            int texturesShift = 0;

            CultureInfo culture = CultureInfo.InvariantCulture;
            NumberStyles numberStyle = NumberStyles.Float;

            void AddData()
            {
                if (obj.Name == null) throw new InvalidDataException("В файле не указано имя модели");
                if (obj.MaterialName == null) throw new InvalidDataException("В файле не указано имя материала");
                modelsData.Add((ObjFileData)obj.Clone());
            };

            using (var reader = new StreamReader(path))
            {
                while ((line = reader.ReadLine()) != null)
                {
                    line = regex.Replace(line, " ").Trim();
                    string[] tokens = line.Split(' ');
                    if (line.StartsWith("mtllib"))
                    {
                        mtlFilePath = @$"{Path.GetDirectoryName(path)}\{tokens[1]}";
                    }
                    else if (line.StartsWith("usemtl"))
                    {
                        obj.MaterialName = tokens[1];
                    }
                    else if (line.StartsWith("o "))
                    {
                        if (first)
                        {
                            obj.Name = tokens[1];
                            first = false;
                            continue;
                        }

                        AddData();

                        verticesShift += obj.Vertices.Count;
                        normalsShift += obj.Normals.Count;
                        texturesShift += obj.Textures.Count;

                        obj.Name = tokens[1];
                        obj.ClearLists();
                    }   
                    else if (line.StartsWith("v "))
                    {
                        float x = float.Parse(tokens[1], numberStyle, culture);
                        float y = float.Parse(tokens[2], numberStyle, culture);
                        float z = float.Parse(tokens[3], numberStyle, culture);
                        obj.Vertices.Add(new Vector3(x, y, z));
                    }
                    else if (line.StartsWith("vn "))
                    {
                        float x = float.Parse(tokens[1], numberStyle, culture);
                        float y = float.Parse(tokens[2], numberStyle, culture);
                        float z = float.Parse(tokens[3], numberStyle, culture);
                        obj.Normals.Add(new Vector3(x, y, z));
                    }
                    else if (line.StartsWith("vt "))
                    {
                        float x = float.Parse(tokens[1], numberStyle, culture);
                        float y = float.Parse(tokens[2], numberStyle, culture);
                        obj.Textures.Add(new Vector2(x, y));
                    }    
                    else if (line.StartsWith("f "))
                    {
                        for (int i = 1; i < tokens.Length; i++)
                        {
                            string[] fTokens = tokens[i].Split('/');
                            int x = int.Parse(fTokens[0]) - 1;
                            int y = int.Parse(fTokens[1]) - 1;
                            int z = int.Parse(fTokens[2]) - 1;
                            obj.Faces.Add(new Vector3i(x, y, z));
                        } 
                    }
                }
            }

            AddData();
            if (mtlFilePath == null) 
                throw new InvalidDataException("В файле не указан путь к файлу .mtl");

            return (modelsData.ToArray(), mtlFilePath);
        }
    }
}
