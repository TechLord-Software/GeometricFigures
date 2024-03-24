using GraphicLibrary.Models.Information;
using OpenTK.Mathematics;

namespace GraphicLibrary.ModelBehaviour
{
    public partial class Behaviour
    {
        public readonly record struct TranslationParameters(Vector3 Shifts, Vector3 MinPositions, Vector3 MaxPositions);
        public readonly record struct ScalingParameters(Size ScaleCoefficients, Size MinScaleCoefficients, Size MaxScaleCoefficients);
        public readonly record struct SphericRotationParameters(Vector3 DefaultPosition, float DeltaPhi, float DeltaTheta);


        public static Behaviour CreateBehaviour1(TranslationParameters translationParameters, 
            ScalingParameters scalingParameters, RotationAngles rotationAngles)
        {
            Translation translation = CreateLinearTranslationPerSecond(translationParameters.Shifts, 
                translationParameters.MinPositions, translationParameters.MaxPositions);

            Scaling scaling = CreateScalingPerSecond(scalingParameters.ScaleCoefficients,
                scalingParameters.MinScaleCoefficients, scalingParameters.MaxScaleCoefficients);

            Rotation rotation = CreateRotationPerSecond(rotationAngles);

            return new Behaviour(translation, scaling, rotation);
        }
        public static Behaviour CreateBehaviour2(TranslationParameters translationParameters, RotationAngles rotationAngles)
        {
            Translation translation = CreateLinearTranslationPerSecond(translationParameters.Shifts,
                translationParameters.MinPositions, translationParameters.MaxPositions);

            Rotation rotation = CreateRotationPerSecond(rotationAngles);

            return new Behaviour(translation, rotation);
        }
        public static Behaviour CreateBehaviour3(RotationAngles rotationAngles)
        {
            Rotation rotation = CreateRotationPerSecond(rotationAngles);
            return new Behaviour(rotation);
        }
        public static Behaviour CreateBehaviour4(SphericRotationParameters parameters)
        {
            Translation translation = CreateSphericalTranslationPerSecond(parameters.DefaultPosition, 
                parameters.DeltaPhi, parameters.DeltaTheta);

            return new Behaviour(translation);
        }
        public static Translation CreateLinearTranslationPerSecond(Vector3 shifts, Vector3 minPosition, Vector3 maxPosition)
        {
            if (minPosition.X > 0 || minPosition.Y > 0 || minPosition.Z > 0)
                throw new ArgumentException("Неверные минимальные значения координат");
            if (maxPosition.X < 0 || maxPosition.Y < 0 || maxPosition.Z < 0)
                throw new ArgumentException("Неверные максимальные значения координат");


            Vector3 direction = Vector3.One;

            void Clamp(ref float position, ref float direction, float min, float max)
            {
                if (position >= max)
                {
                    position = max;
                    direction = -1;
                }
                if (position <= min)
                {
                    position = min;
                    direction = 1;
                }
            }

            void Translate(ref Vector3 position, ref float time, float deltaTime)
            {
                position += direction * shifts * deltaTime;

                Clamp(ref position.X, ref direction.X, minPosition.X, maxPosition.X);
                Clamp(ref position.Y, ref direction.Y, minPosition.Y, maxPosition.Y);
                Clamp(ref position.Z, ref direction.Z, minPosition.Z, maxPosition.Z);
            }
            return Translate;
        }
        public static Translation CreateSinusoidalTranslationPerSecond(Vector3 shifts, Vector3 minPosition, Vector3 maxPosition)
        {
            if (minPosition.X > 0 || minPosition.Y > 0 || minPosition.Z > 0)
                throw new ArgumentException("Неверные минимальные значения координат");
            if (maxPosition.X < 0 || maxPosition.Y < 0 || maxPosition.Z < 0)
                throw new ArgumentException("Неверные максимальные значения координат");


            Vector3 direction = Vector3.One;

            void Clamp(ref float position, ref float direction, float min, float max)
            {
                if (position >= max)
                {
                    position = max;
                    direction = -1;
                }
                if (position <= min)
                {
                    position = min;
                    direction = 1;
                }
            }

            void Translate(ref Vector3 position, ref float time, float deltaTime)
            {
                position += direction * shifts * MathF.Sin(MathHelper.PiOver2 * (time % 4));

                Clamp(ref position.X, ref direction.X, minPosition.X, maxPosition.X);
                Clamp(ref position.Y, ref direction.Y, minPosition.Y, maxPosition.Y);
                Clamp(ref position.Z, ref direction.Z, minPosition.Z, maxPosition.Z);
            }
            return Translate;
        }
        public static Translation CreateSphericalTranslationPerSecond(Vector3 position, float deltaPhi, float deltaTheta)
        {
            if (position == Vector3.Zero)
                throw new ArgumentException("Неверный параметр позиции");


            float r = MathF.Sqrt(position.X * position.X + position.Y * position.Y + position.Z * position.Z);
            float theta = MathF.Acos(position.Y / r);
            float phi = MathF.Atan(position.Z / position.X);

            void ClampAngles()
            {
                if (theta < 0)
                {
                    theta = -theta;
                    phi = MathHelper.ClampRadians(phi + MathHelper.Pi);
                }
                if (theta > MathHelper.Pi)
                {
                    theta = MathHelper.TwoPi - theta;
                    phi = MathHelper.ClampRadians(phi + MathHelper.Pi);
                }
            }

            void Translate(ref Vector3 position, ref float time, float deltaTime)
            {
                phi = MathHelper.ClampRadians(phi + deltaPhi * deltaTime);
                theta += deltaTheta * deltaTime;

                ClampAngles();

                float x = r * MathF.Sin(theta) * MathF.Cos(phi);
                float y = r * MathF.Cos(theta);
                float z = r * MathF.Sin(theta) * MathF.Sin(phi);

                position = new Vector3(x, y , z);
            }
            return Translate;
        }
        public static Scaling CreateScalingPerSecond(Size scaleCoefficients, Size minSize, Size maxSize)
        {
            if (minSize.X < 0 || minSize.Y < 0 || minSize.Z < 0 || minSize.X > 1 || minSize.Y > 1 || minSize.Z > 1)
                throw new ArgumentException("Неверные минимальные значения размера");
            if (maxSize.X < 1 || maxSize.Y < 1 || maxSize.Z < 1)
                throw new ArgumentException("Неверные максимальные значения размера");


            Size direction = Size.One;

            void Clamp(ref float size, ref float direction, float min, float max)
            {
                if (size >= max)
                {
                    size = max;
                    direction = -1;
                }
                if (size <= min)
                {
                    size = min;
                    direction = 1;
                }
            }



            void Scale(ref Size size, ref float time, float deltaTime)
            {
                size += direction * scaleCoefficients * deltaTime;

                Clamp(ref size.X, ref direction.X, minSize.X, maxSize.X);
                Clamp(ref size.Y, ref direction.Y, minSize.Y, maxSize.Y);
                Clamp(ref size.Z, ref direction.Z, minSize.Z, maxSize.Z);
            }
            return Scale;
        }
        public static Rotation CreateRotationPerSecond(RotationAngles angles)
        {
            void Rotate(ref RotationAngles rotationAngles, ref float time, float deltaTime)
            {
                rotationAngles += angles * deltaTime;
            }
            return Rotate;
        }
    }
}
