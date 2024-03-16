using GraphicLibrary.Cameras;
using GraphicLibrary.Materials;
using GraphicLibrary.Models.Interfaces.Common;
using GraphicLibrary.Models.Unit;
using GraphicLibrary.Scenes;
using GraphicLibrary.Shaders;

namespace GraphicLibrary.LightSources
{
    /// <summary>
    /// Класс, описывающий источник света
    /// </summary>
    public class LightSource : TransformableLightSource, IDrawable, ICloneable
    {
        /// <summary>
        /// Объект класса по умолчанию
        /// </summary>
        private static readonly LightSource DEFAULT;


        public static LightSource Default => (LightSource)DEFAULT.Clone();


        /// <summary>
        /// Статический конструктор
        /// </summary>
        static LightSource()
        {
            DEFAULT = new LightSource(PhongModel.Default, LightAttenuationParameters.Default);
        }
        /// <summary>
        /// Конструктор класса LightSource
        /// </summary>
        /// <param name="phongParameters"> структура, содержащая компоненты для затенения по Фонгу </param>
        /// <param name="lightAttenuationParameters"> параметры затухания света с увеличением расстояния </param>
        public LightSource(PhongModel phongParameters, LightAttenuationParameters lightAttenuationParameters)
            : base(phongParameters, lightAttenuationParameters) { }
        /// <summary>
        /// Конструктор класса LightSource, принимающего простую модель
        /// </summary>
        /// <param name="phongParameters"> компоненты для затенения по Фонгу </param>
        /// <param name="attenuation"> параметры затухания света </param>
        /// <param name="unit"> простая модель </param>
        protected LightSource(PhongModel phongParameters, LightAttenuationParameters attenuation, ModelUnit unit)
            : base(phongParameters, attenuation, unit) { }
        /// <summary>
        /// Конструктор класса LightSource, принимающий перечисление простых моделей
        /// </summary>
        /// <param name="phongParameters"> компоненты для затенения по Фонгу </param>
        /// <param name="attenuation"> параметры затухания света </param>
        /// <param name="units"> перечисление простых моделей </param>
        protected LightSource(PhongModel phongParameters, LightAttenuationParameters attenuation, IEnumerable<ModelUnit> units)
            : base(phongParameters, attenuation, units) { }



        /// <summary>
        /// Метод отрисовки модели
        /// </summary>
        /// <param name="shader"> шейдер </param>
        /// <param name="scene"> текущая сцена </param>
        public void Draw(Shader shader, Scene scene)
        {
            shader.Activate();
            shader.UseCamera(scene.CurrentCamera);
            foreach (var model in models)
            {
                model.Draw(shader, scene);
            }
        }

        public object Clone()
        {
            return new LightSource(PhongParameters, Attenuation, models);
        }
    }
}
