using GraphicLibrary.Materials;
using GraphicLibrary.Models.Information;
using GraphicLibrary.Models.Interfaces.Common;
using GraphicLibrary.Models.Interfaces.Components;
using GraphicLibrary.Models.Unit;
using OpenTK.Mathematics;

namespace GraphicLibrary.LightSources
{
    public abstract class TransformableLightSource : StaticLightSource, IDynamicComponents, ITransformable
    {
        /// <summary>
        /// Список простых моделей
        /// </summary>
        public new IReadOnlyList<ITransformableModelUnit> Models => models;



        /// <summary>
        /// Конструктор класса TransformableLightSource
        /// </summary>
        /// <param name="phongParameters"> компоненты для затенения по Фонгу </param>
        /// <param name="lightAttenuationParameters"> параметры затухания света </param>
        protected TransformableLightSource(PhongModel phongParameters, LightAttenuationParameters lightAttenuationParameters) 
            : base(phongParameters, lightAttenuationParameters) { }
        /// <summary>
        /// Конструктор класса TransformableLightSource, принимающего простую модель
        /// </summary>
        /// <param name="phongParameters"> компоненты для затенения по Фонгу </param>
        /// <param name="attenuation"> параметры затухания света </param>
        /// <param name="unit"> простая модель </param>
        protected TransformableLightSource(PhongModel phongParameters, LightAttenuationParameters attenuation, ModelUnit unit)
            : base(phongParameters, attenuation, unit)
        {
            PhongParameters = phongParameters;
            Attenuation = attenuation;
        }
        /// <summary>
        /// Конструктор класса TransformableLightSource, принимающий перечисление простых моделей
        /// </summary>
        /// <param name="phongParameters"> компоненты для затенения по Фонгу </param>
        /// <param name="attenuation"> параметры затухания света </param>
        /// <param name="units"> перечисление простых моделей </param>
        protected TransformableLightSource(PhongModel phongParameters, LightAttenuationParameters attenuation, IEnumerable<ModelUnit> units)
            : base(phongParameters, attenuation, units) { }






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
