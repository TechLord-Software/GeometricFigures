namespace GraphicLibrary.LightSources
{
    /// <summary>
    /// Структура, содержащая коэффициенты полинома второй степени, 
    /// отвечающего за ослабление света 
    /// с увеличением расстояния от предмета до него: 
    /// P(d) = Quadratic * d^2 + Linear * d + Constant,
    /// где d - расстояние. 
    /// Коэффициент ослабления a = 1 / P(d)
    /// </summary>
    public struct LightAttenuationParameters
    {
        /// <summary>
        /// Объект структуры по умолчанию
        /// </summary>
        public static readonly LightAttenuationParameters Default;


        /// <summary>
        /// Значение свойства Constant по умолчанию
        /// </summary>
        private const float DEFAULT_CONSTANT = 1f;
        /// <summary>
        /// Значение свойства Linear по умолчанию
        /// </summary>
        private const float DEFAULT_LINEAR = 0.09f;
        /// <summary>
        /// Значение свойства Quadratic по умолчанию
        /// </summary>
        private const float DEFAULT_QUADRATIC = 0.032f;


        /// <summary>
        /// Коэффициент с членом в нулевой степени
        /// </summary>
        public float Constant { get; set; }
        /// <summary>
        /// Коэффициент с членом в первой степени
        /// </summary>
        public float Linear { get; set; }
        /// <summary>
        /// Коэффициент с членом во второй степени
        /// </summary>
        public float Quadratic { get; set; }


        static LightAttenuationParameters()
        {
            Default = new LightAttenuationParameters(DEFAULT_CONSTANT, DEFAULT_LINEAR, DEFAULT_QUADRATIC);
        }


        public LightAttenuationParameters()
        {
            this = Default;
        }
        /// <summary>
        /// Конструктор структуры LightAttenuationParameters
        /// </summary>
        /// <param name="constant"> коэффициент с членом в нулевой степени </param>
        /// <param name="linear"> коэффициент с членом в первой степени </param>
        /// <param name="quadratic"> коэффициент с членом во второй степени </param>
        public LightAttenuationParameters(float constant, float linear, float quadratic)
        {
            Constant = constant;
            Linear = linear;
            Quadratic = quadratic;
        }
    }
}
