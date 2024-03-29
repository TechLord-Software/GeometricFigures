﻿using GraphicLibrary.Cameras.Settings;
using GraphicLibrary.Models.Unit;
using OpenTK.Mathematics;
using OpenTK.Windowing.GraphicsLibraryFramework;

namespace GraphicLibrary.Cameras
{
    /// <summary>
    /// Камера для двухмерного отображения
    /// </summary>
    public class Camera2D : Camera
    {
        public Camera2D(Vector3 position, Vector3 target, CameraSettings settings) : base(position, target, settings)
        {
        }

        public Camera2D(Vector3 position, Vector3 target, CameraSettings settings, ModelUnit unit) : base(position, target, settings, unit)
        {
        }

        public Camera2D(Vector3 position, Vector3 target, CameraSettings settings, IEnumerable<ModelUnit> units) : base(position, target, settings, units)
        {
        }

        public override void OnKeyDown(KeyboardState input)
        {
            throw new NotImplementedException();
        }

        public override void OnMouseDown(MouseState mouse)
        {
            throw new NotImplementedException();
        }

        public override void OnMouseMove(MouseState mouse)
        {
            throw new NotImplementedException();
        }

        public override void OnMouseScroll(float offset)
        {
            throw new NotImplementedException();
        }

        public override void OnMouseUp(MouseState mouse)
        {
            throw new NotImplementedException();
        }

        protected override void Update()
        {
            throw new NotImplementedException();
        }
    }
}
