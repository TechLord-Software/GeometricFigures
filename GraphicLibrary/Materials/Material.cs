using OpenTK.Mathematics;

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
        private static readonly Material Default;



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
        public PhongModel PhongParameters { get; set; }
         


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
