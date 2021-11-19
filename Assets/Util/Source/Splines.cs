using System.Collections.Generic;
using UnityEngine;

namespace Util {
    public class Splines {

        public static List<Vector3> GenerateCubicSpline (Vector3 p1, Vector3 p2, Vector3 p3, float step = 0.05f) {
            var coeff1 = (p1 - 2 * p2 + p3);
            var coeff2 = 2 * (p2 - p1);
            var coeff3 = p1;

            var vertices = new List<Vector3>();

            for (var t = 0.0f; t < 1.0f; t += step) {
                var vertex = t * t * coeff1 + t * coeff2 + coeff3;
                vertices.Add(vertex);
            }
            vertices.Add(p3);

            return vertices;
        }
        
    }
}