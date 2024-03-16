using OpenTK.Mathematics;
using OpenTK.Windowing.Common;

namespace GraphicLibrary.Cameras.Settings
{
    public struct CameraSettings
    {
        /// <summary>
        /// Расстояние до ближней плоскости отсечения
        /// </summary>
        public const float DepthNear = 0.01f;


        /// <summary>
        /// Максимальный угол обзора
        /// </summary>
        private const float MAX_FOV = 3 * MathHelper.PiOver4;
        /// <summary>
        /// Минимальный угол обзора
        /// </summary>
        private const float MIN_FOV = MathHelper.PiOver4;
        /// <summary>
        /// Стандартный угол обзора
        /// </summary>
        private const float DEFAULT_FOV = MathHelper.PiOver2;
        /// <summary>
        /// Соотношение сторон по умолчанию
        /// </summary>
        private const float DEFAULT_ASPECT_RATIO = 16.0f / 9;
        /// <summary>
        /// Минимальное расстояние до дальней плоскости отсечения
        /// </summary>
        private const float MIN_RENDER_DISTANCE = 10f;
        /// <summary>
        /// Расстояние до дальней плоскости отсечения по умолчанию
        /// </summary>
        private const float DEFAULT_RENDER_DISTANCE = 1000f;
        /// <summary>
        /// Минимальная чувствительность мыши
        /// </summary>
        private const float MIN_MOUSE_SENSITIVITY = 0.01f;
        /// <summary>
        /// Максимальная чувствительность мыши
        /// </summary>
        private const float MAX_MOUSE_SENSITIVITY = 10f;
        /// <summary>
        /// Чувствительность мыши по умолчанию
        /// </summary>
        private const float DEFAULT_MOUSE_SENSITIVITY = 0.05f;
        /// <summary>
        /// Минимальная чувствительность колесика мыши
        /// </summary>
        private const float MIN_WHEEL_SENSITIVITY = 0.1f;
        /// <summary>
        /// Максимальная чувствительность колесика мыши
        /// </summary>
        private const float MAX_WHEEL_SENSITIVITY = 50f;
        /// <summary>
        /// Чувствительность колесика мыши по умолчанию
        /// </summary>
        private const float DEFAULT_WHEEL_SENSITIVITY = 1f;
        /// <summary>
        /// Тип курсора по умолчанию
        /// </summary>
        private const CursorState DEFAULT_CURSOR_STATE = CursorState.Normal;
       

        /// <summary>
        /// Чувствительность мыши
        /// </summary>
        private float _mouseSensitivity;
        /// <summary>
        /// Чувствительность колесика мыши
        /// </summary>
        private float _wheelSensitivity;
        /// <summary>
        /// Расстояние до дальней плоскости отсечения
        /// </summary>
        private float _renderDistance;
        /// <summary>
        /// Угол обзора
        /// </summary>
        private float _fov;




        /// <summary>
        /// Соотношение сторон
        /// </summary>
        public float AspectRatio;
        /// <summary>
        /// Состояние курсора
        /// </summary>
        public CursorState CursorState;



        public float MouseSensitivity
        {
            get => _mouseSensitivity;
            set => _mouseSensitivity = MathHelper.Clamp(value, MIN_MOUSE_SENSITIVITY, MAX_MOUSE_SENSITIVITY);
        }
        public float WheelSensitivity
        {
            get => _wheelSensitivity;
            set => _wheelSensitivity = MathHelper.Clamp(value, MIN_WHEEL_SENSITIVITY, MAX_WHEEL_SENSITIVITY);
        }
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


        /// <summary>
        /// Конструктор структуры CameraSettings
        /// </summary>
        /// <param name="renderDistance"> расстояние до дальней плоскости отсечения </param>
        /// <param name="aspectRatio"> соотношение сторон </param>
        /// <param name="fov"> угол обзора </param>
        /// <param name="mouseSensitivity"> чувствительность мыши </param>
        /// <param name="wheelSensitivity"> чувствительносьб колесика мыши </param>
        /// <param name="cursorState"> состояние курсора </param>
        public CameraSettings(float renderDistance, float aspectRatio, float fov, 
            float mouseSensitivity, float wheelSensitivity, CursorState cursorState)
        {
            CursorState = cursorState;
            AspectRatio = aspectRatio;
            RenderDistance = renderDistance;
            Fov = fov;
            MouseSensitivity = mouseSensitivity;
            WheelSensitivity = wheelSensitivity;  
        }
        public CameraSettings(float renderDistance, float aspectRatio, float fov,float mouseSensitivity, float wheelSensitivity)
            : this(renderDistance, aspectRatio, fov, mouseSensitivity, wheelSensitivity, DEFAULT_CURSOR_STATE) { }
        public CameraSettings(float renderDistance, float aspectRatio, float fov, float mouseSensitivity)
            : this(renderDistance, aspectRatio, fov, mouseSensitivity, DEFAULT_WHEEL_SENSITIVITY) { }
        public CameraSettings(float renderDistance, float aspectRatio, float fov)
            : this(renderDistance, aspectRatio, fov, DEFAULT_MOUSE_SENSITIVITY) { }
        public CameraSettings(float renderDistance, float aspectRatio)
            : this(renderDistance, aspectRatio, DEFAULT_FOV) { }
        public CameraSettings(float renderDistance)
            : this(renderDistance, DEFAULT_ASPECT_RATIO) { }
        public CameraSettings()
            : this(DEFAULT_RENDER_DISTANCE) { }
    }
}
