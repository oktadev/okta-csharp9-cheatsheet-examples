using System;

namespace Cs9CheatSheet.PatternMatching.TypeCheckPattern
{
    class Cube
    {
        public double Side { get; }
    }

    class Sphere
    {
        public double Radius { get; }
    }

    class Cone
    {
        public double Radius { get; }
        public double Height { get; }
    }

    class Volume
    {
        static double Traditional(object solid)
        {
            if (solid.GetType().Equals(typeof(Cube)))
            {
                var cube = solid as Cube;
                if (cube.Side >= 0.0) 
                    return Math.Pow(cube.Side, 3);
            }
            else if (solid.GetType().Equals(typeof(Sphere)))
            {
                var sphere = solid as Sphere;
                return 4.0 / 3.0 * Math.PI * Math.Pow(sphere.Radius, 3);
            }
            else if (solid.GetType().Equals(typeof(Cone)))
            {
                var cone = solid as Cone;
                if (cone.Radius >= 0.0 && cone.Height >= 0)
                    return Math.PI * Math.Pow(cone.Radius, 2) * cone.Height / 3.0;
            }

            return double.NaN;
        }

        static double IsStatement(object solid)
        {
            if (solid is Cube cube && cube.Side >= 0.0) 
                return Math.Pow(cube.Side, 3);
            else if (solid is Sphere sphere && sphere.Radius >= 0.0) 
                return 4.0 / 3.0 * Math.PI * Math.Pow(sphere.Radius, 3);
            else if (solid is Cone cone && cone.Radius >= 0.0 && cone.Height >= 0) 
                return Math.PI * Math.Pow(cone.Radius, 2) * cone.Height / 3.0;
            return double.NaN;
        }

        static double SwitchStatement(object solid)
        {
            switch(solid)
            {
                case Cube cube when cube.Side > 0.0: 
                    return Math.Pow(cube.Side, 3);
                case Sphere sphere when sphere.Radius >= 0.0: 
                    return 4.0 / 3.0 * Math.PI * Math.Pow(sphere.Radius, 3);
                case Cone cone when cone.Radius >= 0.0 && cone.Height >= 0.0 : 
                    return Math.PI * Math.Pow(cone.Radius, 2) * cone.Height / 3.0;
                default: return double.NaN;
            }
        }

        static double SwitchExpression(object solid) => solid switch
        {
            Cube cube when cube.Side >= 0.0 => Math.Pow(cube.Side, 3),
            Sphere sphere when sphere.Radius >= 0.0 => 4.0 / 3.0 * Math.PI * Math.Pow(sphere.Radius, 3),
            Cone cone when cone.Radius >= 0.0 && cone.Height >= 0.0 => Math.PI * Math.Pow(cone.Radius, 2) * cone.Height / 3.0,
            _ => double.NaN,
        };

        static double CascadeSwitchExpression(object solid) => solid switch
        {
            Cube cube => 
                cube.Side switch 
                {
                    >= 0.0 => Math.Pow(cube.Side, 3),
                    _ => throw new ArgumentException("..."),
                },
            Sphere sphere =>
                sphere.Radius switch
                {
                    >= 0.0 => 4.0 / 3.0 * Math.PI * Math.Pow(sphere.Radius, 3),
                    _ => throw new ArgumentException("..."),
                },
            Cone cone => 
                (cone.Radius, cone.Height) switch
                {
                    (>= 0.0, >= 0.0) => Math.PI * Math.Pow(cone.Radius, 2) * cone.Height / 3.0,
                    _ => throw new ArgumentException("..."),
                },
            _ => double.NaN,
        };

    }
}
