namespace GraphicLibrary.Shaders
{
    /// <summary>
    /// Класс для исключений в классе Shader
    /// </summary>
    public class ShaderException : Exception
    {
        public ShaderException() : base() { }
        public ShaderException(string message) : base(message) { }
    }
}
