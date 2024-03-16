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
        /// <summary>
        /// Максимальное количество источников освещения в сцене
        /// </summary>
        public const int MaxLightSourses = 32;


        private static readonly Vector4 DEFAULT_BACKGROUND_COLOR;

        private List<Model> _models;
        private List<LightSource> _lightSources;
        private List<Camera> _cameras;

        private Camera _currentCamera;

        private Shader _modelShader;
        private Shader _lightSourceShader;
        private Shader _cameraShader;

        public Vector4 BackgroundColor;


        public InformationCamera CurrentCamera => _currentCamera;
        public IReadOnlyList<TransformableModel> Models => _models;
        public IReadOnlyList<TransformableLightSource> LightSources => _lightSources;
        public IReadOnlyList<InformationCamera> Cameras => _cameras;



        static Scene()
        {
            DEFAULT_BACKGROUND_COLOR = new Vector4(0.8f, 0.8f, 0.8f, 1f);
        }
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

        protected override void OnLoad()
        {
            base.OnLoad();

            GL.ClearColor(BackgroundColor.X, BackgroundColor.Y, BackgroundColor.Z, BackgroundColor.W);
            GL.Enable(EnableCap.DepthTest);
            GL.PolygonMode(MaterialFace.Front, PolygonMode.Fill);
            GL.PolygonMode(MaterialFace.Back, PolygonMode.Line);       
        }
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
