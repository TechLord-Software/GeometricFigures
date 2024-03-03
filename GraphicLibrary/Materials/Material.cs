using OpenTK.Mathematics;

namespace GraphicLibrary.Materials
{
    public class Material
    {
        /// <summary>
        /// Минимальное значение поля _shininess
        /// </summary>
        private const float MIN_SHININESS = 0f;
        /// <summary>
        /// Максимальное значение поля _shininess
        /// </summary>
        private const float MAX_SHININESS = 1000f;
        /// <summary>
        /// Значение по умолчанию для поля _shininess
        /// </summary>
        private const float DEFAULT_SHININESS = 64f;
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
        /// Параметр, характеризующий способность предмета блестеть
        /// Чем выше значение, тем больше предмет бликует
        /// </summary>
        private float _shininess;
        /// <summary>
        /// Прозрачность предмета
        /// 0 - абсолютно непрозрачный
        /// 1 - абсолютно прозрачный
        /// </summary>
        private float _transparency;

        /// <summary>
        /// Коэффициент фонового освещения
        /// </summary>
        public Vector3 Ambient;
        /// <summary>
        /// Коэффициент диффузного освещения
        /// </summary>
        public Vector3 Diffuse;
        /// <summary>
        /// Коэффициент бликового освещения
        /// </summary>
        public Vector3 Specular;
        public float Shininess
        {
            get => _shininess;
            set => _shininess = MathHelper.Clamp(value, MIN_SHININESS, MAX_SHININESS);
        }
        public float Transparency
        {
            get => _transparency;
            set => _transparency = MathHelper.Clamp(value, MIN_TRANSPARENCY, MAX_TRANSPARENCY);
        }
        /// <summary>
        /// Имя материала
        /// </summary>
        public string Name { get; set; }


        /// <summary>
        /// Конструктор класса Material
        /// </summary>
        /// <param name="ambient"> коэффициент фонового освещения </param>
        /// <param name="diffuse"> коэффициент диффузного освещения </param>
        /// <param name="specular"> коэффициент бликового освещения </param>
        /// <param name="shininness"> параметр, характеризующий способность предмета блестеть </param>
        /// <param name="transparency"> прозрачность </param>
        public Material(Vector3 ambient, Vector3 diffuse, Vector3 specular, float shininness, float transparency)
        {
            Ambient = ambient;
            Diffuse = diffuse;
            Specular = specular;
            Shininess = shininness;
            Transparency = transparency;
        }
        public Material(Vector3 ambient, Vector3 diffuse, Vector3 specular, float shininness) :
            this(ambient, diffuse, specular, shininness, DEFAULT_TRANSPARENCY)
        { }
        public Material(Vector3 ambient, Vector3 diffuse, Vector3 specular) :
            this(ambient, diffuse, specular, DEFAULT_SHININESS)
        { }

        /// <summary>
        /// Метод парсинга файла формата .mtl
        /// </summary>
        /// <param name="path"> путь к файлу </param>
        /// <returns> объект Material </returns>
        public static Material Parse(string path)
        {

        }
        /// <summary>
        /// Сохранение параметров текущего объекта в файл формата .mtl
        /// </summary>
        /// <param name="path"> путь к файлу </param>
        public void Save(string path)
        {

        }
    }
}
