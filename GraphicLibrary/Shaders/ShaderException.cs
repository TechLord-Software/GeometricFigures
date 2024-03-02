using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphicLibrary.Shaders
{
    public class ShaderException : Exception
    {
        public ShaderException() : base() { }
        public ShaderException(string message) : base(message) { }
    }
}
