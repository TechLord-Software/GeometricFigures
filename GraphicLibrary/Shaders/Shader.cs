using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;

namespace GraphicLibrary.Shaders
{
    /// <summary>
    /// Класс, описывающий вершинный и фрагментный шейдеры
    /// </summary>
    public class Shader
    {
        /// <summary>
        /// Путь к вершинному шейдеру для объектов
        /// </summary>
        private const string COLOR_VERTEX_PATH = @"Shaders\Data\color.vert";
        /// <summary>
        /// Путь к фрагментному шейдеру для объектов
        /// </summary>
        private const string COLOR_FRAGMENT_PATH = @"Shaders\Data\color.frag";
        /// <summary>
        /// Путь к вершинному шейдеру для источников освещения
        /// </summary>
        private const string LIGHT_VERTEX_PATH = @"Shaders\Data\light.vert";
        /// <summary>
        /// Путь к фрагментному шейдеру для источников освещения
        /// </summary>
        private const string LIGHT_FRAGMENT_PATH = @"Shaders\Data\light.frag";
        /// <summary>
        /// Значения для деактивации шейдерной программы
        /// </summary>
        private const int UNBIND_VALUE = 0;


        /// <summary>
        /// Позиция для вершин во всех шейдерах
        /// </summary>
        public const int VertexLocation = 0;
        /// <summary>
        /// Позиция для нормалей во всех шейдерах
        /// </summary>
        public const int NormalLocation = 1;
        /// <summary>
        /// Количество элементов массива вершин, отправялемых в шейдеры за раз
        /// </summary>
        public const int VertexCount = 3;
        /// <summary>
        /// Количество элементов массива нормалей, отправляемых за раз
        /// </summary>
        public const int NormalCount = 3;



        /// <summary>
        /// Шейдер для объектов, не являющихся источниками освещения
        /// </summary>
        public static readonly Shader ColorShader;
        /// <summary>
        /// Шейдер для источников освещения
        /// </summary>
        public static readonly Shader LightShader;

        
        
        /// <summary>
        /// Идентификатор вершинного шейдера
        /// </summary>
        private int _vertex;
        /// <summary>
        /// Идентификатор фрагментного шейдера
        /// </summary>
        private int _fragment;
        /// <summary>
        /// Идентификатор шейдерной программы
        /// </summary>
        private int _program;



        /// <summary>
        /// Приватный конструктор, инициализирующий шейдеры для источников освещения и объектов
        /// </summary>
        static Shader()
        {
            ColorShader = new Shader(COLOR_VERTEX_PATH, COLOR_FRAGMENT_PATH);
            LightShader = new Shader(LIGHT_VERTEX_PATH, LIGHT_FRAGMENT_PATH);
        }
        /// <summary>
        /// Компиляция шейдеров по путям vertPath и fragPath, создание шейдерной программы
        /// </summary>
        /// <param name="vertPath"> путь к вершнному шейдеру </param>
        /// <param name="fragPath"> путь к фрагментному шейдеру </param>
        /// <exception cref="ShaderException"> ошибка линковки шейдерной программы </exception>
        public Shader(string vertPath, string fragPath)
        {
            _vertex = CreateShader(ShaderType.VertexShader, vertPath);
            _fragment = CreateShader(ShaderType.FragmentShader, fragPath);
            _program = GL.CreateProgram();

            GL.AttachShader(_program, _vertex);
            GL.AttachShader(_program, _fragment);
            GL.LinkProgram(_program);

            GL.GetProgram(_program, GetProgramParameterName.LinkStatus, out var status);
            if (status != (int)All.True)
                throw new ShaderException($"Ошибка при линковке шейдерной программы #{_program}: {GL.GetProgramInfoLog(_program)}");
      
            DeleteShader(_vertex);
            DeleteShader(_fragment);
        }
        /// <summary>
        /// Компиляция и создание шейдера
        /// </summary>
        /// <param name="type"> тип шейдера </param>
        /// <param name="path"> путь к шейдеру </param>
        /// <returns> идентификатор шейдера </returns>
        /// <exception cref="ShaderException"> ошибка компиляции шейдера </exception>
        private int CreateShader(ShaderType type, string path)
        {
            string shaderCode = File.ReadAllText(path);
            int shaderId = GL.CreateShader(type);

            GL.ShaderSource(shaderId, shaderCode);
            GL.CompileShader(shaderId);

            GL.GetShader(shaderId, ShaderParameter.CompileStatus, out var status);
            if (status != (int)All.True)
                throw new ShaderException($"Ошибка при компиляции шейдера #{shaderId}: {GL.GetShaderInfoLog(shaderId)}");

            return shaderId;
        }
        /// <summary>
        /// Удаление и освобождение памяти шейдера
        /// </summary>
        /// <param name="shaderId"> шейдер </param>
        private void DeleteShader(int shaderId)
        {
            GL.DetachShader(_program, shaderId);
            GL.DeleteShader(shaderId);
        }
        /// <summary>
        /// Активация шейдерной программы
        /// </summary>
        public void Activate()
        {
            GL.UseProgram(_program);
        }
        /// <summary>
        /// Деактивация шейдерной программы
        /// </summary>
        public void Deactivate()
        {
            GL.UseProgram(UNBIND_VALUE);
        }
        /// <summary>
        /// Удаление и освобождение памяти шейдерной программы
        /// </summary>
        public void DeleteProgram()
        {
            GL.DeleteProgram(_program);
        }
        /// <summary>
        /// Метод отправляющий матрицу в шейдер
        /// </summary>
        /// <param name="name"> имя формы </param>
        /// <param name="data"> матрица </param>
        public void SetMatrixUniform(string name, ref Matrix4 data)
        {
            Activate();
            int location = GL.GetUniformLocation(_program, name);
            GL.UniformMatrix4(location, true, ref data);
            Deactivate();
        }
        /// <summary>
        /// Метод, отправляющий целочисленное значение в шейдер
        /// </summary>
        /// <param name="name"> имя формы </param>
        /// <param name="data"> целочисленное значение </param>
        public void SetIntUniform(string name, int data)
        {
            Activate();
            int location = GL.GetUniformLocation(_program, name);
            GL.Uniform1(location, data);
            Deactivate();
        }
        public void SetFloatUniform(string name, float data)
        {
            Activate();
            int location = GL.GetUniformLocation(_program, name);
            GL.Uniform1(location, data);
            Deactivate();
        }
        /// <summary>
        /// Метод, отправляющий трекомпонентный вектор в шейдер
        /// </summary>
        /// <param name="name"> имя формы </param>
        /// <param name="data"> вектор </param>
        public void SetVector3Uniform(string name, Vector3 data)
        {
            Activate();
            int location = GL.GetUniformLocation(_program, name);
            GL.Uniform3(location, data);
            Deactivate();
        }
        /// <summary>
        /// Метод, отправляющий четырехкомпонентный вектор в шейдер
        /// </summary>
        /// <param name="name"> имя формы </param>
        /// <param name="data"> вектор </param>
        public void SetVec4Uniform(string name, ref Vector4 data)
        {
            Activate();
            int location = GL.GetUniformLocation(_program, name);
            GL.Uniform4(location, ref data);
            Deactivate();
        }
    }
}
