using GraphicLibrary.Materials;
using GraphicLibrary.Models.Interfaces.AbstractModels;
using GraphicLibrary.Models.Interfaces.Components;
using GraphicLibrary.Models.Unit;
using GraphicLibrary.Shaders;

namespace GraphicLibrary.LightSources
{
    /// <summary>
    /// Абстрактный класс, описывающий статический источник света
    /// </summary>
    public abstract class InformationLightSource : TransformationInfoComplexModel, IInformationComponents
    {
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
        public IReadOnlyList<InformationModelUnit> Models => models;



        /// <summary>
        /// Конструктор класса StaticLightSource
        /// </summary>
        /// <param name="phongParameters"> компоненты для затенения по Фонгу </param>
        /// <param name="attenuation"> параметры затухания света </param>
        protected InformationLightSource(PhongModel phongParameters, LightAttenuationParameters attenuation) : base()
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
        protected InformationLightSource(PhongModel phongParameters, LightAttenuationParameters attenuation, ModelUnit unit) 
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
        protected InformationLightSource(PhongModel phongParameters, LightAttenuationParameters attenuation, IEnumerable<ModelUnit> units)
            : base(units)
        {
            PhongParameters = phongParameters;
            Attenuation = attenuation;
        }
    }
}
