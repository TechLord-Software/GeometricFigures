using GraphicLibrary.Models.Information;
using OpenTK.Mathematics;

namespace GraphicLibrary.ModelBehaviour
{
    public delegate void Translation(ref Vector3 translation, ref float time, float deltaTime);
    public delegate void Scaling(ref Size size, ref float time, float deltaTime);
    public delegate void Rotation(ref RotationAngles angles, ref float time, float deltaTime);
    public partial class Behaviour
    {

        private Matrix4 _rotationMatrix;
        private Matrix4 _translationMatrix;
        private Matrix4 _scaleMatrix;

        private Vector3 _position;
        private Size _size;
        private RotationAngles _rotationAngles;

        private float _time;


        public Matrix4 RotationMatrix => _rotationMatrix;
        public Matrix4 TranslationMatrix => _translationMatrix;
        public Matrix4 ScaleMatrix => _scaleMatrix;
        public Vector3 Position => _position;
        public Size Size => _size;
        public RotationAngles RotationAngles => _rotationAngles;


        private Translation? _translation;
        private Scaling? _scaling;
        private Rotation? _rotation;


        public Translation? Translation
        {
            get => _translation;
            set
            {
                if ((value?.GetInvocationList().Length ?? 0) > 1)
                    throw new InvalidOperationException("Нельзя добавить больше одного делегата");

                _translation = value;
            }
        }
        public Scaling? Scaling
        {
            get => _scaling;
            set
            {
                if ((value?.GetInvocationList().Length ?? 0) > 1)
                    throw new InvalidOperationException("Нельзя добавить больше одного делегата");

                _scaling = value;
            }
        }
        public Rotation? Rotation
        {
            get => _rotation;
            set
            {
                if ((value?.GetInvocationList().Length ?? 0) > 1)
                    throw new InvalidOperationException("Нельзя добавить больше одного делегата");

                _rotation = value;
            }
        }




        public Behaviour(Translation? translation, Scaling? scaling, Rotation? rotation)
        {
            Translation = translation;
            Scaling = scaling;
            Rotation = rotation;
        }
        public Behaviour(Translation? translation, Scaling? scaling)
            : this(translation, scaling, null) { }
        public Behaviour(Scaling? scaling, Rotation? rotation)
            : this(null, scaling, rotation) { }
        public Behaviour(Translation? translation, Rotation? rotation)
            : this(translation, null, rotation) { }
        public Behaviour(Translation? translation) 
            : this(translation, null, null) { }
        public Behaviour(Scaling? scaling) 
            : this(null, scaling, null) { }
        public Behaviour(Rotation? rotation)
            : this(null, null, rotation) { }
        public Behaviour() 
            : this(null, null, null) { }




        public void Update(float deltaTime)
        {
            Translation?.Invoke(ref _position, ref _time, deltaTime);
            Scaling?.Invoke(ref _size, ref _time, deltaTime);
            Rotation?.Invoke(ref _rotationAngles, ref _time, deltaTime);
            _time += deltaTime;
            UpdateMatrices();
        }
        private void UpdateMatrices()
        {
            _translationMatrix = Matrix4.CreateTranslation(_position);

            _scaleMatrix = Matrix4.CreateScale(_size.X, _size.Y, _size.Z);

            _rotationMatrix = Matrix4.CreateRotationX(_rotationAngles.X) 
                * Matrix4.CreateRotationY(_rotationAngles.Y) 
                * Matrix4.CreateRotationZ(_rotationAngles.Z);
        }
    }
}
