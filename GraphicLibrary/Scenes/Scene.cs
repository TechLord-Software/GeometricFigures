using GraphicLibrary.Cameras;
using GraphicLibrary.ComplexModels;
using GraphicLibrary.LightSources;
using OpenTK.Mathematics;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using OpenTK.Graphics.OpenGL4;
using GraphicLibrary.Shaders;

namespace GraphicLibrary.Scenes
{
    public class Scene : GameWindow
    {
        public static readonly Scene Default;
        /// <summary>
        /// Максимальное количество источников освещения в сцене
        /// </summary>
        public const int MaxLightSourses = 32;

        /// <summary>
        /// Цвет фона по умолчанию
        /// </summary>
        private static readonly Vector4 DEFAULT_BACKGROUND_COLOR;

        /// <summary>
        /// Список моделей сцены
        /// </summary>
        private List<Model> _models;
        /// <summary>
        /// Список источников света сцены
        /// </summary>
        private List<LightSource> _lightSources;
        /// <summary>
        /// Список камер сцены
        /// </summary>
        private List<Camera> _cameras;
        /// <summary>
        /// Текущая камера
        /// </summary>
        private Camera _currentCamera;
        /// <summary>
        /// Шейдер для объектов класса Model
        /// </summary>
        private Shader _modelShader;
        /// <summary>
        /// Шейдер для объектов класса LightSource
        /// </summary>
        private Shader _lightSourceShader;
        /// <summary>
        /// Шейдер для объектов класса Camera
        /// </summary>
        private Shader _cameraShader;



        /// <summary>
        /// Цвет фона
        /// </summary>
        public Vector4 BackgroundColor;


        public InformationCamera CurrentCamera => _currentCamera;
        public IReadOnlyList<TransformableModel> Models => _models;
        public IReadOnlyList<TransformableLightSource> LightSources => _lightSources;
        public IReadOnlyList<InformationCamera> Cameras => _cameras;



        /// <summary>
        /// Статический конструктор
        /// </summary>
        static Scene()
        {
            DEFAULT_BACKGROUND_COLOR = new Vector4(0.8f, 0.8f, 0.8f, 1f);

            var nativeWindowSettings = new NativeWindowSettings()
            {
                Profile = ContextProfile.Compatability,
                Size = new Vector2i(900, 900),
            };
            Vector4 backgroundColor = new Vector4(0.2f, 0.2f, 0.2f, 1f);
            Default = new Scene(GameWindowSettings.Default, nativeWindowSettings, Camera.Default, backgroundColor);

            Model tetrahedron = Model.Tetrahedron;
            Model cube = Model.Cube;
            Model icosahedron = Model.Icosahedron;
            Model octahedron = Model.Octahedron;
            Model sphere = Model.Sphere;
            Model thor = Model.Thor;

            tetrahedron.Move(Vector3.UnitX * 3);
            cube.Move(Vector3.UnitX * 6);
            icosahedron.Move(Vector3.UnitX * 9);
            icosahedron.Move(Vector3.UnitX * 12);
            sphere.Move(Vector3.UnitX * 15);
            thor.Move(Vector3.UnitX * 18);

            Default.AddModel(tetrahedron);
            Default.AddModel(cube);
            Default.AddModel(octahedron);
            Default.AddModel(sphere);
            Default.AddModel(thor);

            LightSource tetrahedronLight = LightSource.Default;
            LightSource cubeLight = LightSource.Default;
            LightSource icosahedronLight = LightSource.Default;
            LightSource octahedronLight = LightSource.Default;
            LightSource sphereLight = LightSource.Default;
            LightSource thorLight = LightSource.Default;

            tetrahedronLight.Move(Vector3.UnitX * 3);
            cubeLight.Move(Vector3.UnitX * 6);
            icosahedron.Move(Vector3.UnitX * 9);
            octahedronLight.Move(Vector3.UnitX * 12);
            sphereLight.Move(Vector3.UnitX * 15);
            thorLight.Move(Vector3.UnitX * 18);

            tetrahedronLight.Move(Vector3.UnitY * 3);
            cubeLight.Move(Vector3.UnitY * 3);
            icosahedronLight.Move(Vector3.UnitY * 3);
            octahedronLight.Move(Vector3.UnitY * 3);
            sphereLight.Move(Vector3.UnitY * 3);
            thorLight.Move(Vector3.UnitY * 3);

            tetrahedronLight.Move(Vector3.UnitZ * 3);
            cubeLight.Move(Vector3.UnitZ * 3);
            icosahedronLight.Move(Vector3.UnitZ * 3);
            octahedronLight.Move(Vector3.UnitZ * 3);
            sphereLight.Move(Vector3.UnitZ * 3);
            thorLight.Move(Vector3.UnitZ * 3);

            Default.AddLightSource(tetrahedronLight);
            Default.AddLightSource(cubeLight);
            Default.AddLightSource(icosahedronLight);
            Default.AddLightSource(octahedronLight);
            Default.AddLightSource(sphereLight);
            Default.AddLightSource(thorLight);        
        }
        /// <summary>
        /// Конструктор сцены
        /// </summary>
        /// <param name="gameSettings"> настройки класса GameWindow </param>
        /// <param name="nativeSettings"> настройки класса NativeWindow </param>
        /// <param name="camera"> камера </param>
        /// <param name="backgroundColor"> цвет фона </param>
        public Scene(GameWindowSettings gameSettings, NativeWindowSettings nativeSettings, Camera camera, Vector4 backgroundColor) 
            : base(gameSettings, nativeSettings)
        {
            VSync = VSyncMode.On;

            _models = new List<Model>();
            _lightSources = new List<LightSource>();
            _cameras = new List<Camera>();
            _currentCamera = camera;
            BackgroundColor = backgroundColor;
            OnCameraChanged(camera);
        }
        public Scene(GameWindowSettings gameSettings, NativeWindowSettings nativeSettings, Camera camera)
            : this(gameSettings, nativeSettings, camera, DEFAULT_BACKGROUND_COLOR) { }
        public Scene(GameWindowSettings gameSettings, NativeWindowSettings nativeSettings)
            : this(gameSettings, nativeSettings, Camera.Default) { }

        /// <summary>
        /// Установка настроек
        /// </summary>
        protected override void OnLoad()
        {
            base.OnLoad();

            GL.ClearColor(BackgroundColor.X, BackgroundColor.Y, BackgroundColor.Z, BackgroundColor.W);
            GL.Enable(EnableCap.DepthTest);
            GL.PolygonMode(MaterialFace.Front, PolygonMode.Fill);
            GL.PolygonMode(MaterialFace.Back, PolygonMode.Line);       
        }
        /// <summary>
        /// Метод, в котором происходит отрисовка моделей
        /// </summary>
        /// <param name="args"> аргументы кадра </param>
        protected override void OnRenderFrame(FrameEventArgs args)
        {
            base.OnRenderFrame(args);
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
            CursorState = _currentCamera.Settings.CursorState;

            DrawCameras();
            DrawModels();
            DrawLightSources();

            SwapBuffers();
        }
        protected override void OnMouseMove(MouseMoveEventArgs e)
        {
            base.OnMouseMove(e);

            _currentCamera.OnMouseMove(MouseState);
        }
        protected override void OnMouseDown(MouseButtonEventArgs e)
        {
            base.OnMouseDown(e);

            _currentCamera.OnMouseDown(MouseState);
        }
        protected override void OnMouseUp(MouseButtonEventArgs e)
        {
            base.OnMouseDown(e);

            _currentCamera.OnMouseUp(MouseState);
        }
        protected override void OnMouseWheel(MouseWheelEventArgs e)
        {
            base.OnMouseWheel(e);

            _currentCamera.OnMouseScroll(e.OffsetY);          
        }
        protected override void OnKeyDown(KeyboardKeyEventArgs e)
        {
            base.OnKeyDown(e);

            _currentCamera.OnKeyDown(KeyboardState);
        }
        protected override void OnResize(ResizeEventArgs e)
        {
            base.OnResize(e);

            GL.Viewport(0, 0, Size.X, Size.Y);
            var settings = _currentCamera.Settings;
            settings.AspectRatio = Size.X / (float)Size.Y;
            _currentCamera.Settings = settings;
        }


        public void AddModel(Model model)
        {
            _models.Add(model);
        }
        public void RemoveModel(Model model)
        {
            if (!_models.Contains(model))
                throw new ArgumentException("Модели нет в списке моделей");

            _models.Remove(model);
        }
        public void AddLightSource(LightSource lightSource)
        {
            if (_lightSources.Count == MaxLightSourses)
                throw new InvalidOperationException("Достигнуто максимальное количество источников освещения");

            _lightSources.Add(lightSource);
        }
        public void RemoveLightSource(LightSource lightSource)
        {
            if (!_lightSources.Contains(lightSource))
                throw new ArgumentException("Источника света нет в списке источников света");

            _lightSources.Remove(lightSource);
        }
        public void AddCamera(Camera camera)
        {
            _cameras.Add(camera);
        }
        public void RemoveCamera(Camera camera)
        {
            if (!_cameras.Contains(camera))
                throw new ArgumentException("Камеры нет в списке камер");

            _cameras.Remove(camera);
        }
        public void SetCamera(Camera camera)
        {
            if (!_cameras.Contains(camera))
                throw new ArgumentException("Камеры нет в списке камер");

            _currentCamera = camera;
            OnCameraChanged(camera);
        }


        private void OnCameraChanged(Camera camera)
        {
            switch (camera)
            {
                case Camera2D:
                    _modelShader = Shader.UnlightedShader;
                    _lightSourceShader = Shader.UnlightedShader;
                    _cameraShader = Shader.UnlightedShader;
                    break;
                case FpvCamera:
                case ScsCamera:
                case StaticCamera:
                    _modelShader = Shader.LightedShader;
                    _lightSourceShader = Shader.UnlightedShader;
                    _cameraShader = Shader.UnlightedShader;
                    break;
            }
        }
        private void DrawModels()
        {
            foreach (var model in _models)
            {
                _modelShader.Activate();
                model.Draw(_modelShader, this);
                _modelShader.Deactivate();
            }
        }
        private void DrawCameras()
        {
            foreach (var camera in _cameras)
            {
                _cameraShader.Activate();
                camera.Draw(_cameraShader, this);
                _cameraShader.Deactivate();
            }
        }
        private void DrawLightSources()
        {
            foreach (var lightSource in _lightSources)
            {
                _lightSourceShader.Activate();
                lightSource.Draw(_lightSourceShader, this);
                _lightSourceShader.Deactivate();
            }
        }
    }
}
