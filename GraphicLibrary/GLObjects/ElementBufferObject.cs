using OpenTK.Graphics.OpenGL4;

namespace GraphicLibrary.GLObjects
{
    /// <summary>
    /// Обертка над объектом буфера индексов (index buffer object, второе название - element buffer object - ebo)
    /// </summary>
    public struct ElementBufferObject
    {
        /// <summary>
        /// Значения для деактивации текущего еbo
        /// </summary>
        private const int UNBIND_VALUE = 0;
        /// <summary>
        /// Идентификтор объекта
        /// </summary>
        public int Id { get; init; }
        /// <summary>
        /// Длина массива индексов
        /// </summary>
        public int DataLength { get; init; }

        /// <summary>
        /// Конструктор, создающий ebo и отправляющий данные индексов вершин в видеопамять 
        /// </summary>
        /// <param name="indices"> массив трехкомпонентных индексов треугольников </param>
        public ElementBufferObject(uint[] indices)
        {
            Id = GL.GenBuffer();
            DataLength = indices.Length;
            Activate();
            GL.BufferData(BufferTarget.ElementArrayBuffer, indices.Length * sizeof(uint), indices, BufferUsageHint.StaticDraw);
            Deactivate();
        }
        /// <summary>
        /// Активация (привязка) этого объекта
        /// </summary>
        public void Activate()
        {
            GL.BindBuffer(BufferTarget.ElementArrayBuffer, Id);
        }
        /// <summary>
        /// Деактивация (отвязка) этого объекта
        /// </summary>
        public void Deactivate()
        {
            GL.BindBuffer(BufferTarget.ElementArrayBuffer, UNBIND_VALUE);
        }
    }
}
