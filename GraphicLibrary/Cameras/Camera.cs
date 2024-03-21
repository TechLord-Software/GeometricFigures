using GraphicLibrary.Cameras.Settings;
using GraphicLibrary.ComplexModels;
using GraphicLibrary.Models.Figures;
using GraphicLibrary.Models.Interfaces.Common;
using GraphicLibrary.Models.Unit;
using GraphicLibrary.Scenes;
using GraphicLibrary.Shaders;
using OpenTK.Mathematics;
using OpenTK.Windowing.GraphicsLibraryFramework;
using static System.Formats.Asn1.AsnWriter;

namespace GraphicLibrary.Cameras
{
    /// <summary>
    /// Абстрактный класс камеры
    /// </summary>
    public abstract class Camera : InformationCamera, IDrawable
    {
        /// <summary>
        /// Камера по умолчанию
        /// </summary>
        public static Camera Default => ScsCamera.Default;


        /// <summary>
        /// Конструтор абстрактного класса Camera
        /// </summary>
        /// <param name="position"> кооринаты камеры </param>
        /// <param name="target"> координаты цели, куда направлена камера </param>
        /// <param name="settings"> настройки камеры </param>
        public Camera(Vector3 position, Vector3 target, CameraSettings settings)
            : base(position, target, settings) { }
        /// <summary>
        /// Конструктор класса Camera, с передачей простой модели
        /// </summary>
        /// <param name="position"> кооринаты камеры </param>
        /// <param name="target"> координаты цели, куда направлена камера </param>
        /// <param name="settings"> настройки камеры </param>
        /// <param name="unit"> простая модель </param>
        public Camera(Vector3 position, Vector3 target, CameraSettings settings, ModelUnit unit)
            : base(position, target, settings, unit) { }
        /// <summary>
        /// Конструктор класса Camera, с передачей перечисления простых моделей
        /// </summary>
        /// <param name="position"> кооринаты камеры </param>
        /// <param name="target"> координаты цели, куда направлена камера </param>
        /// <param name="settings"> настройки камеры </param>
        /// <param name="units"> перечисление простых моделей </param>
        public Camera(Vector3 position, Vector3 target, CameraSettings settings, IEnumerable<ModelUnit> units)
            : base(position, target, settings, units) { }




        /// <summary>
        /// Метод обработки перемещения мыши
        /// </summary>
        /// <param name="mouse"> мышь </param>
        public abstract void OnMouseMove(MouseState mouse);
        /// <summary>
        /// Метод обработки нажатия клавиш мыши
        /// </summary>
        /// <param name="mouse"> мышь </param>
        public abstract void OnMouseDown(MouseState mouse);
        /// <summary>
        /// Метод обработки нажатия клавиш мыши
        /// </summary>
        /// <param name="mouse"> мышь </param>
        public abstract void OnMouseUp(MouseState mouse);
        /// <summary>
        /// Метод обработки нажатия кнопки
        /// </summary>
        /// <param name="input"> состояние клавиатуры </param>
        public abstract void OnKeyDown(KeyboardState input);

        /// <summary>
        /// Метод обработки вращения колесика мыши
        /// </summary>
        /// <param name="offset"> смещение колесика мыши </param>
        public abstract void OnMouseScroll(float offset);

        /// <summary>
        /// Метод отрисовки модели
        /// </summary>
        /// <param name="shader"> шейдер </param>
        /// <param name="scene"> текущая сцена </param>
        public void Draw(Shader shader, Scene scene)
        {
            shader.UseCamera(scene.CurrentCamera);

            if (this == scene.CurrentCamera)
            {
                var transparencies = MakeInvisible();
                DrawModels(shader, scene);
                ReturnTransparency(transparencies);
                return;
            }

            DrawModels(shader, scene);
        }
        private void DrawModels(Shader shader, Scene scene)
        {
            foreach (var model in models)
            {
                model.Draw(shader, scene);
            }
        }
        private List<float> MakeInvisible()
        {
            List<float> transparencies = new List<float>();
            for (int i = 0; i < models.Count; i++)
            {
                transparencies.Add(models[i].Material.Transparency);
                models[i].Material.Transparency = 1;
            }
            return transparencies;
        }
        private void ReturnTransparency(List<float> transparencies)
        {
            for (int i = 0; i < models.Count; i++)
            {
                models[i].Material.Transparency = transparencies[i];
            }
        }
    }
}
