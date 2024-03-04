using OpenTK.Mathematics;

namespace GraphicLibrary.Materials
{
    /// <summary>
    /// Структура, содержащая компоненты для затенения по Фонгу
    /// </summary>
    public struct PhongModel
    {
        /// <summary>
        /// Объект структуры по умолчанию
        /// </summary>
        public static readonly PhongModel Default;


        /// <summary>
        /// Значение по умолчанию для поля Ambient
        /// </summary>
        private static readonly Vector3 DEFAULT_AMBIENT;
        /// <summary>
        /// Значение по умолчанию для поля Diffuse
        /// </summary>
        private static readonly Vector3 DEFAULT_DIFFUSE;
        /// <summary>
        /// Значение по умолчанию для поля Specular
        /// </summary>
        private static readonly Vector3 DEFAULT_SPECULAR;
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
        /// Параметр, характеризующий способность предмета блестеть
        /// Чем выше значение, тем больше предмет бликует
        /// </summary>
        private float _shininess;
        public float Shininess
        {
            get => _shininess;
            set => _shininess = MathHelper.Clamp(value, MIN_SHININESS, MAX_SHININESS);
        }
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

        /// <summary>
        /// Статический конструктор
        /// </summary>
        static PhongModel()
        {
            DEFAULT_AMBIENT = new Vector3 (1.0f, 1.0f, 1.0f);
            DEFAULT_DIFFUSE = new Vector3(1.0f, 1.0f, 1.0f);
            DEFAULT_SPECULAR = new Vector3(0.5f, 0.5f, 0.5f);
            Default = new PhongModel(DEFAULT_AMBIENT, DEFAULT_DIFFUSE, DEFAULT_SPECULAR, DEFAULT_SHININESS);
        }



        
        public PhongModel()
        {
            this = Default;
        }
        public PhongModel(Vector3 ambient, Vector3 diffuse, Vector3 specular) :
            this(ambient, diffuse, specular, DEFAULT_SHININESS)
        { }
        /// <summary>
        /// Конструктор структуры PhongModel
        /// </summary>
        /// <param name="ambient"> коэффициент фонового освещения </param>
        /// <param name="diffuse"> коэффициент диффузного освещения </param>
        /// <param name="specular"> коэффициент бликового освещения </param>
        /// <param name="shininness"> параметр, характеризующий способность предмета блестеть </param>
        public PhongModel(Vector3 ambient, Vector3 diffuse, Vector3 specular, float shininness)
        {
            Ambient = ambient;
            Diffuse = diffuse;
            Specular = specular;
            Shininess = shininness;
        }
        
        
    }
}
