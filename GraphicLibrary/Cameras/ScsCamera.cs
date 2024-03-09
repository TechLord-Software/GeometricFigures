using GraphicLibrary.Cameras.Interfaces;
using OpenTK.Mathematics;
using OpenTK.Windowing.GraphicsLibraryFramework;

namespace GraphicLibrary.Cameras
{
    /// <summary>
    /// Камера, расположенная на поверхности сферы, и передвигающаяся по ней.
    /// Задается в сферической системе координат (spherical coordinate system - scs)
    /// </summary>
    public class ScsCamera : Camera, IMouseMove, IMouseScroll
    {
        private const float MIN_RADIUS = 1;
        private const float DELTA_THETA = 1e-5f;

        private const float MIN_MOUSE_SENSITIVITY = 0.01f;
        private const float MAX_MOUSE_SENSITIVITY = 10f;
        private const float DEFAULT_MOUSE_SENSITIVITY = 0.3f;

        private const float MIN_SCROLL_SENSITIVITY = 1f;
        private const float MAX_SCROLL_SENSITIVITY = 100f;
        private const float DEFAULT_SCROLL_SENSITIVITY = 25f;



        private float _mouseSensitivity;
        private float _scrollSensitivity;

        private float _r;
        private float _theta;
        private float _phi;



        public float R
        {
            get => _r;
            set
            {
                _r = MathHelper.Clamp(value, MIN_RADIUS, RenderDistance);
                Update();
            }
        }
        public float Theta
        {
            get => _theta;
            set
            {
                _theta = MathHelper.Clamp(value, DELTA_THETA, MathHelper.Pi - DELTA_THETA);
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

        public float MouseSensitivity
        {
            get => _mouseSensitivity;
            set => _mouseSensitivity = MathHelper.Clamp(value, MIN_MOUSE_SENSITIVITY, MAX_MOUSE_SENSITIVITY);
        }
        public float ScrollSensitivity
        {
            get => _scrollSensitivity;
            set => _scrollSensitivity = MathHelper.Clamp(value, MIN_SCROLL_SENSITIVITY, MAX_SCROLL_SENSITIVITY);
        }




        // FIXME
        public ScsCamera()
        {
            _r = MathF.Sqrt(Position.X * Position.X + Position.Y * Position.Y + Position.Z * Position.Z);
            _theta = MathF.Acos(Position.Y / R);
            _phi = MathF.Atan2(Position.Z, Position.X);
        }



        protected override void Update()
        {
            position.X = R * MathF.Sin(Theta) * MathF.Cos(Phi) + Target.X;
            position.Y = R * MathF.Cos(Theta) + Target.Y;
            position.Z = R * MathF.Sin(Theta) * MathF.Sin(Phi) + Target.Z;
            
            Direction = Vector3.Normalize(Position - Target);
            Right = Vector3.Normalize(Vector3.Cross(Vector3.UnitY, Direction));
            Up = Vector3.Normalize(Vector3.Cross(Direction, Right));

            ViewMatrix = Matrix4.LookAt(Position, Target, Up);
            ProjectionMatrix = Matrix4.CreatePerspectiveFieldOfView(Fov, AspectRatio, DEPTH_NEAR, RenderDistance);
        }

        // FIXME
        // ЛКМ + движение мышкой для поворота камерой
        // СКМ + движение мышкой для смены цели
        public void MouseMove(MouseState mousePosition) 
        {
            
        }
        public void MouseScroll(float offset) 
        {
            R -= offset * ScrollSensitivity;
        }
    }
}
