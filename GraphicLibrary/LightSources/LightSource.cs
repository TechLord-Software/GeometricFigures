using GraphicLibrary.Materials;
using GraphicLibrary.Models;
using GraphicLibrary.Models.Interfaces.Common;
using GraphicLibrary.Models.Unit;
using GraphicLibrary.Shaders;
using OpenTK.Mathematics;

namespace GraphicLibrary.LightSources
{
    /// <summary>
    /// Класс, описывающий источник света
    /// </summary>
    public class LightSource : TransformationComplexModel, ITransformableLightSource, IDrawable
    {
        /// <summary>
        /// Список всех источников света
        /// </summary>
        private static List<LightSource> _lightSources;
        /// <summary>
        /// Используемый шейдер
        /// </summary>
        public static Shader? Shader { get; set; }
        /// <summary>
        /// Шейдер для отправки информации об источниках света
        /// </summary>
        public static Shader? LightShader { get; set; }
        /// <summary>
        /// Объект класса по умолчанию
        /// </summary>
        public static readonly LightSource Default;


        /// <summary>
        /// Структура, содержащая компоненты для затенения по Фонгу
        /// </summary>
        public PhongModel PhongParameters { get; set; }
        /// <summary>
        /// Параметры затухания света с увеличением расстояния
        /// </summary>
        public LightAttenuationParameters LightAttenuationParameters { get; set; }


        /// <summary>
        /// Список простых моделей
        /// </summary>
        public IReadOnlyList<ITransformableModelUnit> Models => models;

        /// <summary>
        /// Позиция модели
        /// </summary>
        public Vector3 Position => position;
        /// <summary>
        /// Размер модели
        /// </summary>
        public Size Size => size;
        /// <summary>
        /// Углы поворота модели
        /// </summary>
        public RotationAngles RotationAngles => rotationAngles;



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
        {
            models = new List<ModelUnit>();

            position = Vector3.Zero;
            size = Size.One;
            rotationAngles = RotationAngles.Zero;

            PhongParameters = phongParameters;
            LightAttenuationParameters = lightAttenuationParameters;
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
                LightShader.SetFloatUniform($"pointLights[{i}].constant", _lightSources[i].LightAttenuationParameters.Constant);
                LightShader.SetFloatUniform($"pointLights[{i}].linear", _lightSources[i].LightAttenuationParameters.Linear);
                LightShader.SetFloatUniform($"pointLights[{i}].quadratic", _lightSources[i].LightAttenuationParameters.Quadratic);
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

        /// <summary>
        /// Перемещение модели
        /// </summary>
        /// <param name="shifts"> вектор перемещения </param>
        public void Move(Vector3 shifts)
        {
            foreach (var model in models)
            {
                model.Move(shifts);
            }

            position.X += shifts.X;
            position.Y += shifts.Y;
            position.Z += shifts.Z;
        }

        /// <summary>
        /// Поворот модели
        /// </summary>
        /// <param name="angles"> углы поворота </param>
        public void Rotate(RotationAngles angles)
        {
            foreach (var model in models)
            {
                model.Rotate(angles);
            }

            rotationAngles.X += angles.X;
            rotationAngles.Y += angles.Y;
            rotationAngles.Z += angles.Z;

            size.UpdateAfterRotation(angles);
        }

        /// <summary>
        /// Масштабирование модели
        /// </summary>
        /// <param name="scale"> коэффициенты масштабирования </param>
        public void Scale(Size scale)
        {
            foreach (var model in models)
            {
                model.Scale(scale);
            }

            size.X += scale.X;
            size.Y += scale.Y;
            size.Z += scale.Z;
        }
    }
}
