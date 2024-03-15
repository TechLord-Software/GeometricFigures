using GraphicLibrary.Materials;
using GraphicLibrary.Models.Interfaces.Common;
using GraphicLibrary.Models.Interfaces.Components;
using GraphicLibrary.Models.Unit;
using GraphicLibrary.Shaders;

namespace GraphicLibrary.LightSources
{
    /// <summary>
    /// Абстрактный класс, описывающий статический источник света
    /// </summary>
    public abstract class StaticLightSource : TransformationInfoComplexModel, IStaticComponents
    {
        /// <summary>
        /// Максимальное количество источников освещения
        /// </summary>
        public const int MaxLightSourses = 32;


        /// <summary>
        /// Структура, содержащая компоненты для затенения по Фонгу
        /// </summary>
        public PhongModel PhongParameters;
        /// <summary>
        /// Параметры затухания света с увеличением расстояния
        /// </summary>
        public LightAttenuationParameters Attenuation;


        /// <summary>
        /// Список простых моделей
        /// </summary>
        public IReadOnlyList<StaticModelUnit> Models => models;
        /// <summary>
        /// Используемый шейдер
        /// </summary>
        public static Shader? Shader { get; set; }
        /// <summary>
        /// Шейдер для отправки информации об источниках света
        /// </summary>
        public static Shader? LightShader { get; set; }




        /// <summary>
        /// Конструктор класса StaticLightSource
        /// </summary>
        /// <param name="phongParameters"> компоненты для затенения по Фонгу </param>
        /// <param name="attenuation"> параметры затухания света </param>
        protected StaticLightSource(PhongModel phongParameters, LightAttenuationParameters attenuation) : base()
        {
            PhongParameters = phongParameters;
            Attenuation = attenuation;
        }
        /// <summary>
        /// Конструктор класса StaticLightSource, принимающего простую модель
        /// </summary>
        /// <param name="phongParameters"> компоненты для затенения по Фонгу </param>
        /// <param name="attenuation"> параметры затухания света </param>
        /// <param name="unit"> простая модель </param>
        protected StaticLightSource(PhongModel phongParameters, LightAttenuationParameters attenuation, ModelUnit unit) 
            : base(unit)
        {
            PhongParameters = phongParameters;
            Attenuation = attenuation;
        }
        /// <summary>
        /// Конструктор класса StaticLightSource, принимающий перечисление простых моделей
        /// </summary>
        /// <param name="phongParameters"> компоненты для затенения по Фонгу </param>
        /// <param name="attenuation"> параметры затухания света </param>
        /// <param name="units"> перечисление простых моделей </param>
        protected StaticLightSource(PhongModel phongParameters, LightAttenuationParameters attenuation, IEnumerable<ModelUnit> units)
            : base(units)
        {
            PhongParameters = phongParameters;
            Attenuation = attenuation;
        }
    }
}
