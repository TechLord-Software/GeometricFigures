using OpenTK.Mathematics;

namespace GraphicLibrary.Models
{
    /// <summary>
    /// Класс комплксной модели
    /// </summary>
    public abstract class ComplexModel
    {
        /// <summary>
        /// Составные части сложной модели
        /// </summary>
        protected List<ModelUnit> _models;

        /// <summary>
        /// Координаты модели
        /// </summary>
        private Vector3 _position;
        /// <summary>
        /// Размер модели
        /// </summary>
        private Size _sise;
        /// <summary>
        /// Углы поворота модели вокруг координатных осей 
        /// </summary>
        private RotationAngles _angles;

        public Vector3 Position => _position;
        public Size Sise => _sise;
        public RotationAngles Angle => _angles;

        public IReadOnlyList<ModelUnit> Models => _models;
    }
}
