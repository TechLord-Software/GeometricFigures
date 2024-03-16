using GraphicLibrary.Materials;
using OpenTK.Mathematics;

namespace GraphicLibrary.Models.Unit
{
    /// <summary>
    /// Абстрактный класс, описывающий свойства модели
    /// </summary>
    public abstract class InformationModelUnit
    {
        /// <summary>
        /// Имя по умолчанию
        /// </summary>
        protected const string DEFAULT_NAME = "DefaultName";


        /// <summary>
        /// Матрица поворота
        /// </summary>
        protected Matrix4 rotationMatrix;
        /// <summary>
        /// Матрица перемещения
        /// </summary>
        protected Matrix4 translationMatrix;
        /// <summary>
        /// Матрица масштабирования
        /// </summary>
        protected Matrix4 scaleMatrix;



        /// <summary>
        /// Имя модели
        /// </summary>
        public string Name;
        /// <summary>
        /// Материал модели
        /// </summary>
        public Material Material;




        public Matrix4 RotationMatrix => rotationMatrix;
        public Matrix4 TranslationMatrix => translationMatrix;
        public Matrix4 ScaleMatrix => scaleMatrix;
        /// <summary>
        /// Матрица преобразования
        /// </summary>
        public Matrix4 ModelMatrix => translationMatrix * scaleMatrix * rotationMatrix;



        /// <summary>
        /// Конструктор класса StaticModelUnit
        /// </summary>
        /// <param name="name"> имя модели </param>
        /// <param name="material"> материал модели </param>
        protected InformationModelUnit(string name, Material material)
        {
            Name = name;
            Material = material;

            rotationMatrix = Matrix4.Identity;
            translationMatrix = Matrix4.Identity;
            scaleMatrix = Matrix4.Identity;
        }
        protected InformationModelUnit(string name) 
            : this(name, Material.Default) { }
        protected InformationModelUnit(Material material) 
            : this(DEFAULT_NAME, material) { }
        protected InformationModelUnit() 
            : this(DEFAULT_NAME) { }
    }
}
