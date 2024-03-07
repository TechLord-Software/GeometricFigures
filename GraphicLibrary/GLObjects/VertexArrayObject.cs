using OpenTK.Graphics.OpenGL4;

namespace GraphicLibrary.GLObjects
{
    /// <summary>
    /// Обертка над объектом vertex array object - vao
    /// </summary>
    public struct VertexArrayObject
    {
        /// <summary>
        /// Значения для деактивации текущего vao
        /// </summary>
        private const int UNBIND_VALUE = 0;
        /// <summary>
        /// Идентификтор объекта
        /// </summary>
        public int Id { get; init; }

        /// <summary>
        /// Конструктор, создающий vao
        /// </summary>
        public VertexArrayObject()
        {
            Id = GL.GenVertexArray();
        }
        /// <summary>
        /// Активация (отвязка) этого объекта
        /// </summary>
        public void Activate()
        {
            GL.BindVertexArray(Id);
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
            GL.BindVertexArray(UNBIND_VALUE);
        }
        /// <summary>
        /// Метод, указывающий как интерпретировать данные из активного буфера, а также куда их отправить
        /// Например: 
        ///     Имеется массив вида [x, y, z, r, g, b, a]
        ///     AttribPointer(0, 7, 3, 0);
        ///     AttribPointer(1, 7, 4, 3);
        ///     Интерпретируется как: данные делятся на куски по 7 элементов,
        ///     В шейдеры по позиции 0 отправляются 3 элемента (координаты вершины),
        ///     затем по позиции 1 отправляются 4 элемента (цвет вершины), 
        ///     но уже со сдвигом 3 от начала рассматриваемого куска
        /// </summary>
        /// <param name="index"> позиция в шейдере </param>
        /// <param name="size"> размер куска (в элементах) </param>
        /// <param name="stride"> колчество элементов для обработки </param>
        /// <param name="offset"> свдиг в куске (в элементах) </param>
        public void AttribPointer(int index, int size, int stride, int offset)
        {
            Activate();
            GL.VertexAttribPointer(index, size, VertexAttribPointerType.Float, true, stride * sizeof(float), offset * sizeof(float));
            GL.EnableVertexAttribArray(index);
            Deactivate();
        }

        /// <summary>
        /// Метод отправляющий данные для рендера
        /// </summary>
        /// <param name="ebo"> объект ebo </param>
        public void DrawElements(ElementBufferObject ebo)
        {
            Activate();
            ebo.Activate();
            GL.DrawElements(PrimitiveType.Triangles, ebo.DataLength, DrawElementsType.UnsignedInt, 0);
            Deactivate();
            ebo.Deactivate();
        }
    }
}
