using GraphicLibrary.Cameras.Settings;
using GraphicLibrary.Models.Unit;
using OpenTK.Mathematics;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.GraphicsLibraryFramework;

namespace GraphicLibrary.Cameras
{
    /// <summary>
    /// Камера, расположенная на поверхности сферы и передвигающаяся по ней.
    /// Задается в сферической системе координат (spherical coordinate system - scs)
    /// </summary>
    public class ScsCamera : Camera
    {
        /// <summary>
        /// Минимальный радиус
        /// </summary>
        private const float MIN_RADIUS = 1;
        /// <summary>
        /// Минимальный угол тета
        /// </summary>
        private const float MIN_THETA = 1e-5f;
        /// <summary>
        /// Максимальный угол тета
        /// </summary>
        private const float MAX_THETA = MathHelper.Pi - MIN_THETA;



        /// <summary>
        /// Расстояние от камеры до точки фокусировки
        /// </summary>
        private float _r;
        /// <summary>
        /// Угол тета
        /// </summary>
        private float _theta;
        /// <summary>
        /// Угол фи
        /// </summary>
        private float _phi;



        public float R
        {
            get => _r;
            set
            {
                _r = MathHelper.Clamp(value, MIN_RADIUS, Settings.RenderDistance);
                Update();
            }
        }
        public float Theta
        {
            get => _theta;
            set
            {
                _theta = MathHelper.Clamp(value, MIN_THETA, MAX_THETA);
                Update();
            }
        }
        public float Phi
        {
            get => _phi;
            set
            {
                _phi = MathHelper.ClampRadians(value);
                Update();
            }
        }

        



        /// <summary>
        /// Конструтор класса ScsCamera
        /// </summary>
        /// <param name="position"> кооринаты камеры </param>
        /// <param name="target"> координаты цели, куда направлена камера </param>
        /// <param name="settings"> настройки камеры </param>
        public ScsCamera(Vector3 position, Vector3 target, CameraSettings settings)
            : base(position, target, settings)
        {
            Initialize();
        }
        /// <summary>
        /// Конструтор класса ScsCamera
        /// </summary>
        /// <param name="position"> кооринаты камеры </param>
        /// <param name="target"> координаты цели, куда направлена камера </param>
        /// <param name="settings"> настройки камеры </param>
        /// <param name="unit"> простая модель </param>
        public ScsCamera(Vector3 position, Vector3 target, CameraSettings settings, ModelUnit unit)
            : base(position, target, settings, unit)
        {
            Initialize();
        }
        /// <summary>
        /// Конструтор класса ScsCamera
        /// </summary>
        /// <param name="position"> кооринаты камеры </param>
        /// <param name="target"> координаты цели, куда направлена камера </param>
        /// <param name="settings"> настройки камеры </param>
        /// <param name="units"> сложная модель </param>
        public ScsCamera(Vector3 position, Vector3 target, CameraSettings settings, IEnumerable<ModelUnit> units)
            : base(position, target, settings, units)
        {
            Initialize();
        }


        /// <summary>
        /// Инициализация
        /// </summary>
        private void Initialize()
        {
            _r = MathF.Sqrt(Position.X * Position.X + Position.Y * Position.Y + Position.Z * Position.Z);
            _theta = MathF.Acos(Position.Y / R);
            _phi = MathF.Atan2(Position.Z, Position.X);
            Update();
        }


        /// <summary>
        /// Метод обновления векторов и матриц
        /// </summary>
        protected override void Update()
        {
            // Преобразование сферических координат в декартовы
            position.X = R * MathF.Sin(Theta) * MathF.Cos(Phi) + Target.X;
            position.Y = R * MathF.Cos(Theta) + Target.Y;
            position.Z = R * MathF.Sin(Theta) * MathF.Sin(Phi) + Target.Z;
            
            // Обновление единичных векторов
            Direction = Vector3.Normalize(Position - Target);
            Right = Vector3.Normalize(Vector3.Cross(Vector3.UnitY, Direction));
            Up = Vector3.Normalize(Vector3.Cross(Direction, Right));

            // Обновление матриц
            ViewMatrix = Matrix4.LookAt(Position, Target, Up);
            ProjectionMatrix = Matrix4.CreatePerspectiveFieldOfView(Settings.Fov, Settings.AspectRatio, CameraSettings.DepthNear, Settings.RenderDistance);
        }
        /// <summary>
        /// Метод, обновляющий позицию камеры при движении мышью
        /// </summary>
        /// <param name="mousePosition"> позиция мыши </param>
        /// <param name="e"> аргументы кнопки мыши </param>
        public override void OnMouseMove(MouseState mousePosition, MouseButtonEventArgs e)
        {
            if (!e.IsPressed) return;

            float dx = mousePosition.X - mousePosition.PreviousX;
            float dy = mousePosition.Y - mousePosition.PreviousY;

            if (e.Button == MouseButton.Left)
            {
                // Изменение углов тета и фи (перемещение камеры)
                Phi += dx * Settings.MouseSensitivity;
                Theta -= dy * Settings.MouseSensitivity;
            }
            else if (e.Button == MouseButton.Middle)
            {
                // Изменение точки фокусировки камеры (перемещение центра сферы и изменение позиции камеры)
                Target -= dx * Settings.MouseSensitivity * Right;
                Target += dy * Settings.MouseSensitivity * Up;
                position -= dx * Settings.MouseSensitivity * Right;
                position += dy * Settings.MouseSensitivity * Up;
            }
        }
        /// <summary>
        /// Метод обработки нажатия кнопки
        /// </summary>
        /// <param name="input"> состояние клавиатуры </param>
        public override void OnKeyDown(KeyboardState input)
        {
            return;
        }
        /// <summary>
        /// Метод обработки вращения колесика мыши
        /// </summary>
        /// <param name="offset"> смещение колесика мыши </param>
        public override void OnMouseScroll(float offset)
        {
            R -= offset * Settings.WheelSensitivity;
        }
    }
}
