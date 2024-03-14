using OpenTK.Mathematics;
using System.Globalization;
using System.Text.RegularExpressions;

namespace GraphicLibrary.Materials
{
    /// <summary>
    /// Сттруктура, содержащая информацию о материале объекта
    /// </summary>
    public struct Material
    {
        /// <summary>
        /// Объект структуры по умолчанию
        /// </summary>
        public static readonly Material Default;



        /// <summary>
        /// Минимальное значение поля _transparency
        /// </summary>
        private const float MIN_TRANSPARENCY = 0f;
        /// <summary>
        /// Максимальное значение поля _transparency
        /// </summary>
        private const float MAX_TRANSPARENCY = 1;
        /// <summary>
        /// Значение по умолчанию для поля _transparency
        /// </summary>
        private const float DEFAULT_TRANSPARENCY = 0f;
        /// <summary>
        /// Значение по умолчанию для свойства Name
        /// </summary>
        private const string DEFAULT_NAME = "Material";
        /// <summary>
        /// Прозрачность предмета
        /// 0 - абсолютно непрозрачный
        /// 1 - абсолютно прозрачный
        /// </summary>
        private float _transparency;


        /// <summary>
        /// Структура, содержащая компоненты для затенения по Фонгу
        /// </summary>
        public PhongModel PhongParameters;
         


        public float Transparency
        {
            get => _transparency;
            set => _transparency = MathHelper.Clamp(value, MIN_TRANSPARENCY, MAX_TRANSPARENCY);
        }
        /// <summary>
        /// Имя материала
        /// </summary>
        public string Name;



        /// <summary>
        /// Статический конструктор
        /// </summary>
        static Material()
        {
            Default = new Material(PhongModel.Default, DEFAULT_TRANSPARENCY, DEFAULT_NAME);
        }


        public Material()
        {
            this = Default;
        }
        public Material(PhongModel phongParameters) :
            this(phongParameters, DEFAULT_NAME)
        { }
        public Material(PhongModel phongParameters, float transparency) :
            this(phongParameters, transparency, DEFAULT_NAME)
        { }
        public Material(PhongModel phongParameters, string name) :
            this(phongParameters, DEFAULT_TRANSPARENCY, name)
        { }
        /// <summary>
        /// Конструктор структуры Material
        /// </summary>
        /// <param name="phongParameters"> структура, содержащая компоненты для затенения по Фонгу </param>
        /// <param name="name"> имя </param>
        /// <param name="transparency"> прозрачность </param>
        public Material(PhongModel phongParameters, float transparency, string name)
        {
            PhongParameters = phongParameters;    
            Transparency = transparency;
            Name = name;
        }



        /// <summary>
        /// Парсинг файла формата .mtl
        /// В .mtl файле хранятся данные о матералах
        /// Материал имеет несколько текстовых параметров, основные из которых:
        /// Ka - коэффициент фонового освещения
        ///     Ka 1.000000 1.000000 1.000000
        ///     
        /// Kd - коэффициент диффузного освещения
        ///     Kd 1.000000 1.000000 1.000000
        ///     
        /// Ks - коэффициент бликового освещения
        ///     Ks 0.500000 0.500000 0.500000
        ///     
        /// Ns - экспонента бликового освещения
        ///     Ns 250.000000
        ///     
        /// d - непрозрачность [0; 1]
        ///     d 0.9
        ///     
        /// Tr - прозрачность [0; 1]
        ///     Tr 0.1
        ///     
        /// </summary>
        /// <param name="path"> путь к файлу </param>
        /// <returns> объект Material </returns>
        public static Material[] Parse(string path)
        {
            List<Material> fileData = new List<Material>();
            Material material = new Material();
            Regex regex = new Regex(@"\s+");
            string? line;       
            bool first = true;


            CultureInfo culture = CultureInfo.InvariantCulture;
            NumberStyles numberStyle = NumberStyles.Float;

            var addData = () =>
            {
                if (material.Name == null) throw new InvalidDataException("В файле не указано имя материала");
                fileData.Add(material);
            };


            using (var reader = new StreamReader(path))
            {            
                while ((line = reader.ReadLine()) != null)
                {
                    line = regex.Replace(line, " ").Trim();
                    string[] tokens = line.Split(' ');

                    if (line.StartsWith("newmtl"))
                    {
                        if (first)
                        {
                            material.Name = tokens[1];
                            first = false;
                            continue;
                        }

                        addData.Invoke();
                        material.Name = tokens[1];
                    }
                    else if (line.StartsWith("Ka "))
                    {
                        float x = float.Parse(tokens[1], numberStyle, culture);
                        float y = float.Parse(tokens[2], numberStyle, culture);
                        float z = float.Parse(tokens[3], numberStyle, culture);
                        material.PhongParameters.Ambient = new Vector3(x, y, z);
                    }
                    else if (line.StartsWith("Kd "))
                    {
                        float x = float.Parse(tokens[1], numberStyle, culture);
                        float y = float.Parse(tokens[2], numberStyle, culture);
                        float z = float.Parse(tokens[3], numberStyle, culture);
                        material.PhongParameters.Diffuse = new Vector3(x, y, z);
                    }
                    else if (line.StartsWith("Ks "))
                    {
                        float x = float.Parse(tokens[1], numberStyle, culture);
                        float y = float.Parse(tokens[2], numberStyle, culture);
                        float z = float.Parse(tokens[3], numberStyle, culture);
                        material.PhongParameters.Specular = new Vector3(x, y, z);
                    }
                    else if (line.StartsWith("Ns "))
                    {
                        material.PhongParameters.Shininess = float.Parse(tokens[1], numberStyle, culture);
                    }
                    else if (line.StartsWith("d "))
                    {
                        material.Transparency = 1 - float.Parse(tokens[1], numberStyle, culture);
                    }
                    else if (line.StartsWith("Tr "))
                    {
                        material.Transparency = float.Parse(tokens[1], numberStyle, culture);
                    }
                }
            }

            addData.Invoke();
            return fileData.ToArray();
        }
        /// <summary>
        /// Сохранение материалов в файл формата .mtl
        /// </summary>
        /// <param name="materials"> материалы </param>
        /// <param name="path"> путь к файлу </param>
        public static void Save(Material[] materials, string path)
        {

        }
    }
}
