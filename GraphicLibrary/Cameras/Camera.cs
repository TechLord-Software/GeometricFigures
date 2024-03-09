using GraphicLibrary.Models.Interfaces.Common;
using GraphicLibrary.Models.Interfaces.Components;
using GraphicLibrary.Models.Unit;
using OpenTK.Mathematics;

namespace GraphicLibrary.Cameras
{
    /// <summary>
    /// Абстрактный класс камеры
    /// </summary>
    public abstract class Camera : ComplexModel, IStaticComponents
    {
        /// <summary>
        /// Расстояние до ближней плоскости отсечения
        /// </summary>
        protected const float DEPTH_NEAR = 0.01f;
        /// <summary>
        /// Максимальный угол обзора
        /// </summary>
        protected const float MAX_FOV = 3 * MathHelper.PiOver4;
        /// <summary>
        /// Минимальный угол обзора
        /// </summary>
        protected const float MIN_FOV = MathHelper.PiOver4;
        /// <summary>
        /// Стандартный угол обзора
        /// </summary>
        protected const float DEFALT_FOV = MathHelper.PiOver2;
        /// <summary>
        /// Соотношение сторон по умолчанию
        /// </summary>
        protected const float DEFAULT_ASPECT_RATIO = 16.0f / 9;
        /// <summary>
        /// Минимальное расстояние до дальней плоскости отсечения
        /// </summary>
        protected const float MIN_RENDER_DISTANCE = 10f;



        /// <summary>
        /// Расстояние до дальней плоскости отсечения
        /// </summary>
        private float _renderDistance;
        /// <summary>
        /// Угол обзора
        /// </summary>
        private float _fov;



        /// <summary>
        /// Матрица преобразования камеры
        /// </summary>
        private Matrix4 _viewMatrix;
        /// <summary>
        /// Матрица проекции
        /// </summary>
        private Matrix4 _projectionMatrix;




        public float RenderDistance
        {
            get => _renderDistance;
            set => _renderDistance = Math.Max(value, MIN_RENDER_DISTANCE);
        }
        public float Fov
        {
            get => _fov;
            set => _fov = MathHelper.Clamp(value, MIN_FOV, MAX_FOV);
        }
        public Matrix4 ViewMatrix
        {
            get => _viewMatrix;
            protected set => _viewMatrix = value;
        }
        public Matrix4 ProjectionMatrix
        {
            get => _projectionMatrix;
            protected set => _projectionMatrix = value;
        }


        /// <summary>
        /// Соотношение сторон
        /// </summary>
        public float AspectRatio { get; set; }
        /// <summary>
        /// Список простых моделей
        /// </summary>
        public IReadOnlyList<IModelUnit> Models => models;
        /// <summary>
        /// Направление камеры
        /// </summary>
        public Vector3 Direction { get; protected set; }
        /// <summary>
        /// Вектор, на конец которого направлена камера
        /// </summary>
        public Vector3 Target { get; protected set; }
        /// <summary>
        /// Вертикальный вектор камеры
        /// </summary>
        public Vector3 Up { get; protected set; }
        /// <summary>
        /// Правый вектор камеры
        /// </summary>
        public Vector3 Right { get; protected set; }

        


        /// <summary>
        /// Конструтор абстрактного класса Camera
        /// </summary>
        /// <param name="position"> кооринаты камеры </param>
        /// <param name="target"> координаты цели, куда направлена камера </param>
        /// <param name="renderDistance"> радиус видимости </param>
        /// <param name="aspectRatio"> соотношение сторон </param>
        /// <param name="fov"> поле зрения </param>
        public Camera(Vector3 position, Vector3 target, float renderDistance, float aspectRatio,  float fov)
            : base()
        {
            Initialize(position, target, renderDistance, aspectRatio, fov);
        }
        public Camera(Vector3 position, Vector3 target, float renderDistance, float aspectRatio)
            : this(position, target, renderDistance, aspectRatio, DEFALT_FOV) { }
        public Camera(Vector3 position, Vector3 target, float renderDistance)
            : this(position, target, renderDistance, DEFAULT_ASPECT_RATIO) { }



        /// <summary>
        /// Конструктор класса Camera, с передачей простой модели
        /// </summary>
        /// <param name="unit"> простая модель </param>
        /// <param name="position"> кооринаты камеры </param>
        /// <param name="target"> координаты цели, куда направлена камера </param>
        /// <param name="renderDistance"> радиус видимости </param>
        /// <param name="aspectRatio"> соотношение сторон </param>
        /// <param name="fov"> поле зрения </param>
        public Camera(ModelUnit unit, Vector3 position, Vector3 target, float renderDistance, float aspectRatio, float fov)
            : base(unit)
        {
            Initialize(position, target, renderDistance, aspectRatio, fov);
        }
        public Camera(ModelUnit unit, Vector3 position, Vector3 target, float renderDistance, float aspectRatio)
            : this(unit, position, target, renderDistance, aspectRatio, DEFALT_FOV) { }
        public Camera(ModelUnit unit, Vector3 position, Vector3 target, float renderDistance)
            : this(unit, position, target, renderDistance, DEFAULT_ASPECT_RATIO) { }


        /// <summary>
        /// Конструктор класса Camera, с передачей перечисления простых моделей
        /// </summary>
        /// <param name="units"> перечисление простых моделей </param>
        /// <param name="position"> кооринаты камеры </param>
        /// <param name="target"> координаты цели, куда направлена камера </param>
        /// <param name="renderDistance"> радиус видимости </param>
        /// <param name="aspectRatio"> соотношение сторон </param>
        /// <param name="fov"> поле зрения </param>
        public Camera(IEnumerable<ModelUnit> units, Vector3 position, Vector3 target, float renderDistance, float aspectRatio, float fov)
            : base(units)
        {
            Initialize(position, target, renderDistance, aspectRatio, fov);
        }
        public Camera(IEnumerable<ModelUnit> units, Vector3 position, Vector3 target, float renderDistance, float aspectRatio)
            : this(units, position, target, renderDistance, aspectRatio, DEFALT_FOV) { }
        public Camera(IEnumerable<ModelUnit> units, Vector3 position, Vector3 target, float renderDistance)
            : this(units, position, target, renderDistance, DEFAULT_ASPECT_RATIO) { }



        /// <summary>
        /// Метод инициализации полей и автосвойств класса
        /// </summary>
        /// <param name="position"> кооринаты камеры </param>
        /// <param name="target"> координаты цели, куда направлена камера </param>
        /// <param name="renderDistance"> радиус видимости </param>
        /// <param name="aspectRatio"> соотношение сторон </param>
        /// <param name="fov"> поле зрения </param>
        private void Initialize(Vector3 position, Vector3 target, float renderDistance, float aspectRatio, float fov)
        {
            base.position = position;
            Target = target;
            RenderDistance = renderDistance;
            AspectRatio = aspectRatio;
            Fov = fov;

            Update();
        }


        /// <summary>
        /// Обновление векторов и матриц
        /// </summary>
        protected abstract void Update();
    }
}
