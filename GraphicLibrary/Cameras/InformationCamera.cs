﻿using GraphicLibrary.Cameras.Settings;
using GraphicLibrary.Models.Interfaces.AbstractModels;
using GraphicLibrary.Models.Unit;
using OpenTK.Mathematics;

namespace GraphicLibrary.Cameras
{
    public abstract class InformationCamera : ComplexModel
    {
        /// <summary>
        /// Матрица преобразования камеры
        /// </summary>
        private Matrix4 _viewMatrix;
        /// <summary>
        /// Матрица проекции
        /// </summary>
        private Matrix4 _projectionMatrix;
        /// <summary>
        /// Настройки камеры
        /// </summary>
        private CameraSettings _settings;

        
        public CameraSettings Settings
        {
            get => _settings;
            set
            {
                _settings = value;
                Update();
            }
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
        /// Список простых моделей
        /// </summary>
        public IReadOnlyList<InformationModelUnit> Models => models;
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
        /// Конструтор абстрактного класса InformationCamera
        /// </summary>
        /// <param name="position"> кооринаты камеры </param>
        /// <param name="target"> координаты цели, куда направлена камера </param>
        /// <param name="settings"> настройки камеры </param>
        public InformationCamera(Vector3 position, Vector3 target, CameraSettings settings)
            : base()
        {
            Initialize(position, target, settings);
        }
        /// <summary>
        /// Конструктор класса InformationCamera, с передачей простой модели
        /// </summary>
        /// <param name="position"> кооринаты камеры </param>
        /// <param name="target"> координаты цели, куда направлена камера </param>
        /// <param name="settings"> настройки камеры </param>
        /// <param name="unit"> простая модель </param>
        public InformationCamera(Vector3 position, Vector3 target, CameraSettings settings, ModelUnit unit)
            : base(unit)
        {
            Initialize(position, target, settings);
        }
        /// <summary>
        /// Конструктор класса InformationCamera, с передачей перечисления простых моделей
        /// </summary>
        /// <param name="position"> кооринаты камеры </param>
        /// <param name="target"> координаты цели, куда направлена камера </param>
        /// <param name="settings"> настройки камеры </param>
        /// <param name="units"> перечисление простых моделей </param>
        public InformationCamera(Vector3 position, Vector3 target, CameraSettings settings, IEnumerable<ModelUnit> units)
            : base(units)
        {
            Initialize(position, target, settings);
        }





        /// <summary>
        /// Метод инициализации полей и автосвойств класса
        /// </summary>
        /// <param name="position"> кооринаты камеры </param>
        /// <param name="target"> координаты цели, куда направлена камера </param>
        /// <param name="settings"> настройки камеры </param>
        private void Initialize(Vector3 position, Vector3 target, CameraSettings settings)
        {
            base.position = position;
            Target = target;
            _settings = settings;
        }
        /// <summary>
        /// Обновление векторов и матриц
        /// </summary>
        protected abstract void Update();
    }
}