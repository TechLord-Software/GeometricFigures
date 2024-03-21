using GraphicLibrary.Materials;
using GraphicLibrary.Models.Unit;
using OpenTK.Mathematics;
using System.Globalization;
using System.Text.RegularExpressions;

namespace GraphicLibrary.Models.Interfaces.AbstractModels
{
    /// <summary>
    /// Абстрактный класс комплексной модели
    /// </summary>
    public abstract class ComplexModel
    {
        /// <summary>
        /// Составные части сложной модели
        /// </summary>
        protected List<ModelUnit> models;

        /// <summary>
        /// Координаты модели
        /// </summary>
        protected Vector3 position;
        public Vector3 Position => position;


        /// <summary>
        /// Конструктор класса ComplexModel
        /// </summary>
        protected ComplexModel()
        {
            models = new List<ModelUnit>();
            position = Vector3.Zero;
        }
        /// <summary>
        /// Конструктор класса ComplexModel, принимающиий одну простую модель
        /// </summary>
        /// <param name="unit"> простая модель </param>
        protected ComplexModel(ModelUnit unit)
        {
            models = new List<ModelUnit>() { (ModelUnit)unit.Clone() };
            position = Vector3.Zero;
        }
        /// <summary>
        /// Конструктор класса ComplexModel, принимающий перечисление простых объектов
        /// </summary>
        /// <param name="units"> перечисление простых обюъектов </param>
        protected ComplexModel(IEnumerable<ModelUnit> units)
        {
            models = new List<ModelUnit>();
            foreach (var unit in units)
            {
                models.Add((ModelUnit)unit.Clone());
            }
            position = Vector3.Zero;
        }



        /// <summary>
        /// Метод добавления новой простой модели
        /// </summary>
        /// <param name="unit"> простая модель </param>
        public void Add(ModelUnit unit)
        {
            models.Add(unit);
        }
        /// <summary>
        /// Метод удаления новой простой модели
        /// </summary>
        /// <param name="unit"> простая модель </param>
        public void Remove(ModelUnit unit)
        {
            models.Remove(unit);
        }
        /// <summary>
        /// Копирование простых моделей в другую комплексную модель
        /// </summary>
        /// <param name="model"> комплексная модель </param>
        public void CopyModelsTo(ComplexModel model)
        {
            foreach (ModelUnit unit in models)
            {
                model.Add((ModelUnit)unit.Clone());
            }
        }
        /// <summary>
        /// Удаление всех простых моделей
        /// </summary>
        public void ClearModels()
        {
            models.Clear();
        }




        /// <summary>
        /// Загрузка модели из файла с расширением .obj
        /// </summary>
        /// <param name="path"> путь к файлу </param>
        /// <returns> загруженная модель </returns>
        public static T Parse<T>(string path) where T : ComplexModel, new()
        {
            if (!File.Exists(path))
                throw new ArgumentException("Такого файла не существует");
            if (Path.GetExtension(path) != ".obj")
                throw new ArgumentException("Неправильное расширение файла");


            var (objFileData, mtlPath) = ReadObj(path);
            var materials = Material.Parse(mtlPath);
            var pairs = MakePairs(objFileData, materials);

            T result = new T();

            foreach (var pair in pairs)
            {
                var (model, material) = pair;
                result.Add(ModelUnit.Create(model, material));
            }

            return result;
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
