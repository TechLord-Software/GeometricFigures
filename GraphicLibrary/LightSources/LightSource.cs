using GraphicLibrary.Materials;
using GraphicLibrary.Models.Interfaces.Common;
using GraphicLibrary.Models.Unit;
using GraphicLibrary.Shaders;

namespace GraphicLibrary.LightSources
{
    /// <summary>
    /// Класс, описывающий источник света
    /// </summary>
    public class LightSource : TransformableLightSource, IDrawable
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
        /// Статический конструктор
        /// </summary>
        static LightSource()
        {
            _lightSources = new List<LightSource>();
            Default = new LightSource(PhongModel.Default, LightAttenuationParameters.Default);
        }
        /// <summary>
        /// Конструктор класса LightSource
        /// </summary>
        /// <param name="phongParameters"> структура, содержащая компоненты для затенения по Фонгу </param>
        /// <param name="lightAttenuationParameters"> параметры затухания света с увеличением расстояния </param>
        public LightSource(PhongModel phongParameters, LightAttenuationParameters lightAttenuationParameters)
            : base(phongParameters, lightAttenuationParameters)
        {
            if (_lightSources.Count == MaxLightSourses) 
                throw new InvalidOperationException("Достигнуто максимальное количество источников освещения");

            _lightSources.Add(this);
        }
        /// <summary>
        /// Конструктор класса LightSource, принимающего простую модель
        /// </summary>
        /// <param name="phongParameters"> компоненты для затенения по Фонгу </param>
        /// <param name="attenuation"> параметры затухания света </param>
        /// <param name="unit"> простая модель </param>
        protected LightSource(PhongModel phongParameters, LightAttenuationParameters attenuation, ModelUnit unit)
            : base(phongParameters, attenuation, unit)
        {
            if (_lightSources.Count == MaxLightSourses)
                throw new InvalidOperationException("Достигнуто максимальное количество источников освещения");

            _lightSources.Add(this);
        }
        /// <summary>
        /// Конструктор класса LightSource, принимающий перечисление простых моделей
        /// </summary>
        /// <param name="phongParameters"> компоненты для затенения по Фонгу </param>
        /// <param name="attenuation"> параметры затухания света </param>
        /// <param name="units"> перечисление простых моделей </param>
        protected LightSource(PhongModel phongParameters, LightAttenuationParameters attenuation, IEnumerable<ModelUnit> units)
            : base(phongParameters, attenuation, units)
        {
            if (_lightSources.Count == MaxLightSourses)
                throw new InvalidOperationException("Достигнуто максимальное количество источников освещения");

            _lightSources.Add(this);
        }



        /// <summary>
        /// Загрузка информации об источниках освещения в шейдер
        /// </summary>
        public static void PushLightSources()
        {
            if (LightShader == null) return;

            for (int i = 0; i < _lightSources.Count; i++)
            {
                LightShader.SetVector3Uniform($"pointLights[{i}].position", _lightSources[i].Position);
                LightShader.SetVector3Uniform($"pointLights[{i}].ambient", _lightSources[i].PhongParameters.Ambient);
                LightShader.SetVector3Uniform($"pointLights[{i}].diffuse", _lightSources[i].PhongParameters.Diffuse);
                LightShader.SetVector3Uniform($"pointLights[{i}].specular", _lightSources[i].PhongParameters.Specular);
                LightShader.SetFloatUniform($"pointLights[{i}].constant", _lightSources[i].Attenuation.Constant);
                LightShader.SetFloatUniform($"pointLights[{i}].linear", _lightSources[i].Attenuation.Linear);
                LightShader.SetFloatUniform($"pointLights[{i}].quadratic", _lightSources[i].Attenuation.Quadratic);
            }
        }
        /// <summary>
        /// Отрисовка модели (всех ее составных частей)
        /// </summary>
        public void Draw()
        {
            if (Shader == null) return;

            Shader.Activate();
            foreach (var model in models)
            {
                model.Draw();
            }
            Shader.Deactivate();
        }
    }
}
