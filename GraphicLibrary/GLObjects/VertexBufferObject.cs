using OpenTK.Graphics.OpenGL4;

namespace GraphicLibrary.GLObjects
{
    /// <summary>
    /// Обертка над объектом буфера вершин (vertex buffer object - vbo)
    /// </summary>
    public struct VertexBufferObject
    {
        /// <summary>
        /// Значения для деактивации текущего vbo
        /// </summary>
        private const int UNBIND_VALUE = 0;

        /// <summary>
        /// Идентификтор объекта
        /// </summary>
        public int Id { get; init; }

        /// <summary>
        /// Конструктор, создающий vbo и отправляющий данные координат вершин в видеопамять 
        /// </summary>
        /// <param name="vertices"> массив трехкомпонентных вершин (x, y, z) </param>
        public VertexBufferObject(float[] vertices)
        {
            Id = GL.GenBuffer();
            Activate();
            GL.BufferData(BufferTarget.ArrayBuffer, vertices.Length * sizeof(float), vertices, BufferUsageHint.StaticDraw);
            Deactivate();
        }
        /// <summary>
        /// Активация (привязка) этого объекта
        /// </summary>
        public void Activate()
        {
            GL.BindBuffer(BufferTarget.ArrayBuffer, Id);
        }
        /// <summary>
        /// Деактивация (отвязка) этого объекта
        /// </summary>
        public void Deactivate()
        {
            DeactivateCurrent();
        }
        /// <summary>
        /// Деактивация (отвязка) текущего привязанного объекта
        /// </summary>
        public static void DeactivateCurrent()
        {
            GL.BindBuffer(BufferTarget.ArrayBuffer, UNBIND_VALUE);
        }
    }
}
