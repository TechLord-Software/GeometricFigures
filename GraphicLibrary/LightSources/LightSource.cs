using GraphicLibrary.Materials;

namespace GraphicLibrary.LightSources
{
    /// <summary>
    /// Класс, описывающий источник света
    /// </summary>
    public class LightSource : ExternallyChangeableModel
    {
        /// <summary>
        /// Список всех источников света
        /// </summary>
        private static List<LightSource> _lightSources;
        /// <summary>
        /// Объект класса по умолчанию
        /// </summary>
        public static readonly LightSource Default;


        /// <summary>
        /// Структура, содержащая компоненты для затенения по Фонгу
        /// </summary>
        public PhongModel PhongParameters;
        /// <summary>
        /// Параметры затухания света с увеличением расстояния
        /// </summary>
        public LightAttenuationParameters LightAttenuationParameters;



        /// <summary>
        /// Статический конструктор
        /// </summary>
        static LightSource()
        {
            // Добавить объект(ы) по умолчанию
        }


        /// <summary>
        /// Конструктор класса LightSource
        /// </summary>
        /// <param name="phongParameters"> структура, содержащая компоненты для затенения по Фонгу </param>
        /// <param name="lightAttenuationParameters"> параметры затухания света с увеличением расстояния </param>
        public LightSource(PhongModel phongParameters, LightAttenuationParameters lightAttenuationParameters)
        {
            PhongParameters = phongParameters;
            LightAttenuationParameters = lightAttenuationParameters;
            // FIXME
        }


        public override void Draw()
        {

        }
    }
}
