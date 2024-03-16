using GraphicLibrary.Cameras;
using GraphicLibrary.LightSources;
using GraphicLibrary.Models.Unit;
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
        /// Путь к вершинному шейдеру для объектов, реагирующих на свет
        /// </summary>
        private const string LIGHTED_VERTEX_PATH = @"Shaders\Data\lighted.vert";
        /// <summary>
        /// Путь к фрагментному шейдеру для объектов, реагирующих на свет
        /// </summary>
        private const string LIGHTED_FRAGMENT_PATH = @"Shaders\Data\lighted.frag";
        /// <summary>
        /// Путь к вершинному шейдеру для объектов, не реагирующих на свет
        /// </summary>
        private const string UNLIGHTED_VERTEX_PATH = @"Shaders\Data\unlighted.vert";
        /// <summary>
        /// Путь к фрагментному шейдеру для объектов, не реагирующих на свет
        /// </summary>
        private const string UNLIGHTED_FRAGMENT_PATH = @"Shaders\Data\unlighted.frag";
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
        /// Шейдер для для объектов, реагирующих на свет
        /// </summary>
        public static readonly Shader LightedShader;
        /// <summary>
        /// Шейдер для для объектов, не реагирующих на свет
        /// </summary>
        public static readonly Shader UnlightedShader;

        
        
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

        
        private Action<Shader, InformationCamera>? CameraUsage { get; init; }
        private Action<Shader, ModelUnit>? ModelUnitUsage { get; init; }
        private Action<Shader, IReadOnlyList<InformationLightSource>>? SceneLightSourceUsage { get; init; }



        /// <summary>
        /// Статический конструктор, инициализирующий шейдеры
        /// </summary>
        static Shader()
        {
            void LightedUseCamera(Shader shader, InformationCamera camera)
            {
                Matrix4 view = camera.ViewMatrix;
                Matrix4 projection = camera.ProjectionMatrix;

                Vector3 cameraPosition = camera.Position;

                shader.SetMatrixUniform("view", ref view);
                shader.SetMatrixUniform("projection", ref projection);
                shader.SetVector3Uniform("cameraPosition", cameraPosition);
            }
            void LightedUseModelUnit(Shader shader, ModelUnit unit)
            {
                Matrix4 model = unit.ModelMatrix;
                Vector3 ambient = unit.Material.PhongParameters.Ambient;
                Vector3 diffuse = unit.Material.PhongParameters.Diffuse;
                Vector3 specular = unit.Material.PhongParameters.Specular;
                float shininness = unit.Material.PhongParameters.Shininess;
                float transparency = unit.Material.Transparency;

                shader.SetMatrixUniform("model", ref model);
                shader.SetVector3Uniform("material.ambient", ambient);
                shader.SetVector3Uniform("material.diffuse", diffuse);
                shader.SetVector3Uniform("material.specular", specular);
                shader.SetFloatUniform("material.shininness", shininness);
                shader.SetFloatUniform("material.transparency", transparency);
            }
            void LightedSceneLightSourcesUsage(Shader shader, IReadOnlyList<InformationLightSource> lightSources)
            {
                shader.SetIntUniform("lightSourceCount", lightSources.Count);

                for (int i = 0; i < lightSources.Count; i++)
                {
                    shader.SetVector3Uniform($"lights[{i}].position", lightSources[i].Position);
                    shader.SetVector3Uniform($"lights[{i}].ambient", lightSources[i].PhongParameters.Ambient);
                    shader.SetVector3Uniform($"lights[{i}].diffuse", lightSources[i].PhongParameters.Diffuse);
                    shader.SetVector3Uniform($"lights[{i}].specular", lightSources[i].PhongParameters.Specular);
                    shader.SetFloatUniform($"lights[{i}].constant", lightSources[i].Attenuation.Constant);
                    shader.SetFloatUniform($"lights[{i}].linear", lightSources[i].Attenuation.Linear);
                    shader.SetFloatUniform($"lights[{i}].quadratic", lightSources[i].Attenuation.Quadratic);
                }
            }


            void UnlightedUseCamera(Shader shader, InformationCamera camera)
            {
                Matrix4 view = camera.ViewMatrix;
                Matrix4 projection = camera.ProjectionMatrix;

                shader.SetMatrixUniform("view", ref view);
                shader.SetMatrixUniform("projection", ref projection);
            }
            
            void UnlightedUseModelUnit(Shader shader, ModelUnit unit)
            {
                Matrix4 model = unit.ModelMatrix;
                Vector3 diffuse = unit.Material.PhongParameters.Diffuse;
                float transparency = unit.Material.Transparency;

                shader.SetMatrixUniform("model", ref model);
                shader.SetVector3Uniform("color", diffuse);
                shader.SetFloatUniform("transparency", transparency);
            }
            
            LightedShader = new Shader(LIGHTED_VERTEX_PATH, LIGHTED_FRAGMENT_PATH,
                LightedUseCamera, LightedUseModelUnit, LightedSceneLightSourcesUsage);

            UnlightedShader = new Shader(UNLIGHTED_VERTEX_PATH, UNLIGHTED_FRAGMENT_PATH,
                UnlightedUseCamera, UnlightedUseModelUnit);
        }
        /// <summary>
        /// Компиляция шейдеров по путям vertPath и fragPath, создание шейдерной программы
        /// </summary>
        /// <param name="vertPath"> путь к вершнному шейдеру </param>
        /// <param name="fragPath"> путь к фрагментному шейдеру </param>
        /// <param name="cameraUsage"> делегат, использующий данные камеры </param>
        /// <param name="modelUnitUsage"> делегат, использующий данные модели </param>
        /// <param name="staticLightSourceUsage"> делегат, использующий статические данные источника света </param>
        /// <exception cref="ShaderException"> ошибка линковки шейдерной программы </exception>
        public Shader(string vertPath, string fragPath, Action<Shader, InformationCamera>? cameraUsage, 
            Action<Shader, ModelUnit>? modelUnitUsage, Action<Shader, IReadOnlyList<InformationLightSource>>? staticLightSourceUsage)
        {
            CameraUsage = cameraUsage;
            ModelUnitUsage = modelUnitUsage;
            SceneLightSourceUsage = staticLightSourceUsage;

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
        public Shader(string vertPath, string fragPath, Action<Shader, InformationCamera>? cameraUsage, Action<Shader, ModelUnit>? modelUnitUsage)
            : this(vertPath, fragPath, cameraUsage, modelUnitUsage, null) { }


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
            int location = GL.GetUniformLocation(_program, name);
            GL.UniformMatrix4(location, true, ref data);
        }
        /// <summary>
        /// Метод, отправляющий целочисленное значение в шейдер
        /// </summary>
        /// <param name="name"> имя формы </param>
        /// <param name="data"> целочисленное значение </param>
        public void SetIntUniform(string name, int data)
        {
            int location = GL.GetUniformLocation(_program, name);
            GL.Uniform1(location, data);
        }
        /// <summary>
        /// Метод, отправляющий значение типа float в шейдер
        /// </summary>
        /// <param name="name"> имя формы </param>
        /// <param name="data"> вещественное значение </param>
        public void SetFloatUniform(string name, float data)
        {
            int location = GL.GetUniformLocation(_program, name);
            GL.Uniform1(location, data);
        }
        /// <summary>
        /// Метод, отправляющий трехкомпонентный вектор в шейдер
        /// </summary>
        /// <param name="name"> имя формы </param>
        /// <param name="data"> вектор </param>
        public void SetVector3Uniform(string name, Vector3 data)
        {
            int location = GL.GetUniformLocation(_program, name);
            GL.Uniform3(location, data);
        }
        /// <summary>
        /// Метод, отправляющий четырехкомпонентный вектор в шейдер
        /// </summary>
        /// <param name="name"> имя формы </param>
        /// <param name="data"> вектор </param>
        public void SetVector4Uniform(string name, ref Vector4 data)
        {
            int location = GL.GetUniformLocation(_program, name);
            GL.Uniform4(location, ref data);
        }
        public void UseCamera(InformationCamera camera)
        {
            CameraUsage?.Invoke(this, camera);
        }
        public void UseModelUnit(ModelUnit unit)
        {
            ModelUnitUsage?.Invoke(this, unit);
        }
        public void UseLightSources(IReadOnlyList<InformationLightSource> lightSources)
        {
            SceneLightSourceUsage?.Invoke(this, lightSources);
        }
    }
}
