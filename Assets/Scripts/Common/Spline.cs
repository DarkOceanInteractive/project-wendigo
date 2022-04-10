using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace ProjectWendigo
{
    namespace detail
    {
        /// <summary>
        /// Proxy aimed at providing methods for the primitive type T used by
        /// ASpline.
        /// </summary>
        /// <typeparam name="T">Spline primitive type</typeparam>
        public interface ISplineTypeProxy<T>
        {
            public Vector3 ToVector3(T value);
            public T Zero();
            public T Add(T lhs, T rhs);
            public T Sub(T lhs, T rhs);
            public T Mul(T lhs, float rhs);
            public float Distance(T lhs, T rhs);
            public bool Equals(T lhs, T rhs);
            public T Lerp(T start, T end, float t);
        }

        [Serializable]
        /// <summary>
        /// Spline cache for heavy operations.
        /// </summary>
        public class SplineCache<T, TypeProxy>
          where TypeProxy : ISplineTypeProxy<T>, new()
        {
            private static TypeProxy _proxy = new TypeProxy();
            private TypeProxy Proxy => SplineCache<T, TypeProxy>._proxy;

            private readonly ASpline<T, TypeProxy> _spline;
            private T[] _lastControlPoints;

            [Range(2, 32)]
            [Tooltip("A larger number gives more detailed curve length and t approximations, but requires more computations.")]
            [SerializeField] private int _cumulativeDistanceLookupTableSize = 10;
            private int _lastCumulativeDistanceLookupTableSize;
            private float[] _cumulativeDistanceLUT;

            /// <summary>
            /// Create a spline cache.
            /// </summary>
            /// <param name="spline">Spline object to link the cache to</param>
            public SplineCache(ASpline<T, TypeProxy> spline)
            {
                this._spline = spline;
            }

            /// <summary>
            /// Approximate the spline curve length.
            /// </summary>
            public float GetCurveLength()
            {
                this.EnsureCacheValidity();
                return this.UnsafeGetCurveLength();
            }

            /// <summary>
            /// Approximate the spline curve length.
            /// Skips the checks that ensure the cache is synchronized with its
            /// spline object. This method should be favored over GetCurveLength
            /// if the spline object is constant.
            /// </summary>
            public float UnsafeGetCurveLength()
            {
                if (this._lastControlPoints == null)
                {
                    this.SyncCache();
                }
                if (this._cumulativeDistanceLUT == null)
                {
                    this.BuildCumulativeDistanceLUT();
                }
                return this._cumulativeDistanceLUT[this._cumulativeDistanceLUT.Length - 1];
            }

            /// <summary>
            /// Approximate the t value at which the distance from the beginning
            /// of the spline curve is equal to the given distance.
            /// </summary>
            public float GetTFromDistance(float distance)
            {
                this.EnsureCacheValidity();
                return this.UnsafeGetTFromDistance(distance);
            }

            /// <summary>
            /// Approximate the t value at which the distance from the beginning
            /// of the spline curve is equal to the given distance.
            /// Skips the checks that ensure the cache is synchronized with its
            /// spline object. This method should be favored over
            /// GetTFromDistance if the spline object is constant.
            /// </summary>
            public float UnsafeGetTFromDistance(float distance)
            {
                if (this._lastControlPoints == null)
                {
                    this.SyncCache();
                }
                if (this._cumulativeDistanceLUT == null)
                {
                    this.BuildCumulativeDistanceLUT();
                }
                float curveLength = this._cumulativeDistanceLUT[this._cumulativeDistanceLUT.Length - 1];
                if (distance > 0 && distance <= curveLength)
                {
                    int n = this._cumulativeDistanceLUT.Length;
                    int index = Array.BinarySearch(this._cumulativeDistanceLUT, distance, new CumulativeDistanceComparer());
                    if (index < 0)
                        index = ~index;
                    int lowerIndex = index > 0 ? index - 1 : 0;
                    int upperIndex = index < n ? index : n;
                    float lowerBound = this._cumulativeDistanceLUT[lowerIndex];
                    float upperBound = this._cumulativeDistanceLUT[upperIndex];
                    float step = 1f / (n - 1f);
                    float lowerT = lowerIndex * step;
                    float upperT = upperIndex * step;
                    // Interpolate t between lowerBound and upperBound.
                    return lowerT + (distance - lowerBound) * (upperT - lowerT) / (upperBound - lowerBound);
                }
                return distance / curveLength;
            }

            /// <summary>
            /// Synchronize cache with its spline object.
            /// </summary>
            public void SyncCache()
            {
                this._lastControlPoints = (T[])this._spline.ControlPoints.Clone();
                this._lastCumulativeDistanceLookupTableSize = this._cumulativeDistanceLookupTableSize;
                this._cumulativeDistanceLUT = null;
            }

            /// <summary>
            /// Build and cache the cumulative distance lookup table.
            /// </summary>
            private void BuildCumulativeDistanceLUT()
            {
                this._cumulativeDistanceLUT = new float[this._cumulativeDistanceLookupTableSize + 1];
                this._cumulativeDistanceLUT[0] = 0f;
                float step = 1f / this._cumulativeDistanceLookupTableSize;
                T prev = this._lastControlPoints[0];
                for (int i = 1; i <= this._cumulativeDistanceLookupTableSize; ++i)
                {
                    float t = step * i;
                    T curr = this._spline.Compute(t);
                    float segmentLength = this.Proxy.Distance(prev, curr);
                    this._cumulativeDistanceLUT[i] = this._cumulativeDistanceLUT[i - 1] + segmentLength;
                    prev = curr;
                }
            }

            /// <summary>
            /// Synchronizes the cache with its spline object if it is out of
            /// sync.
            /// </summary>
            private void EnsureCacheValidity()
            {
                if (!this.IsCacheValid())
                    this.SyncCache();
            }

            /// <summary>
            /// Checks if the cache is still synchronized with its spline object.
            /// </summary>
            private bool IsCacheValid()
            {
                if (this._lastCumulativeDistanceLookupTableSize != this._cumulativeDistanceLookupTableSize)
                    return false;
                if (this._lastControlPoints?.Length != this._spline.ControlPoints.Length)
                    return false;
                for (int i = 0; i < this._lastControlPoints.Length; ++i)
                {
                    if (!this.Proxy.Equals(this._lastControlPoints[i], this._spline.ControlPoints[i]))
                        return false;
                }
                return true;
            }

            /// <summary>
            /// Comparer functor that compares cumulative distances.
            /// This can be used in Array.BinarySearch to retrieve the first
            /// cumulative distance larger than the target distance in a
            /// cumulative distance lookup table.
            /// </summary>
            private class CumulativeDistanceComparer : IComparer<float>
            {
                public int Compare(float x, float y)
                {
                    return x < y ? -1 : 1;
                }
            }
        }

        [Serializable]
        /// <summary>
        /// N-dimensional spline base class.
        /// Create Bezier curves with as many control points as desired.
        /// </summary>
        /// <typeparam name="T">Spline primitive type</typeparam>
        /// <typeparam name="TypeProxy">Spline primitive type proxy</typeparam>
        public abstract class ASpline<T, TypeProxy>
          where TypeProxy : ISplineTypeProxy<T>, new()
        {
            private static TypeProxy _proxy = new TypeProxy();

            [SerializeField] public T[] ControlPoints;

            [Serializable]
            public class GizmoOptions
            {
                [Range(0.01f, 0.5f)]
                [SerializeField] public float Step = 0.1f;
                [SerializeField] public bool ShowCoordinates = false;
                [SerializeField] public bool DrawXAxis = false;
                [SerializeField] public bool DrawYAxis = false;
                [SerializeField] public bool DrawDerivativeXAxis = false;
                [SerializeField] public bool DrawDerivativeYAxis = false;
            }
            [SerializeField] public GizmoOptions Gizmo;

            private TypeProxy Proxy => ASpline<T, TypeProxy>._proxy;
            [SerializeField] private SplineCache<T, TypeProxy> _cache;

            /// <summary>
            /// Create an N-dimensional spline from N control points.
            /// </summary>
            /// <param name="controlPoints">Control points defining the spline</param>
            public ASpline(params T[] controlPoints)
            {
                this.ControlPoints = controlPoints;
                this._cache = new SplineCache<T, TypeProxy>(this);
            }

            /// <summary>
            /// Approximate the spline curve length.
            /// </summary>
            public float GetCurveLength()
            {
                return this._cache.GetCurveLength();
            }

            /// <summary>
            /// Approximate the spline curve length.
            /// Spline caches results needed to calculate the curve length.
            /// This method should only be called if the spline object has not
            /// changed since the last call to this method. Otherwise, call
            /// GetCurveLength.
            /// </summary>
            public float UnsafeGetCurveLength()
            {
                return this._cache.UnsafeGetCurveLength();
            }

            /// <summary>
            /// Approximate the t value at which the distance from the beginning
            /// of the spline curve is equal to the given distance.
            /// </summary>
            public float GetTFromDistance(float distance)
            {
                return this._cache.GetTFromDistance(distance);
            }

            /// <summary>
            /// Approximate the t value at which the distance from the beginning
            /// of the spline curve is equal to the given distance.
            /// Calling this method instead of GetTFromDistance on a constant
            /// spline object avoids unnecessary checks. However, if this spline
            /// object changes, call GetTFromDistance instead.
            /// </summary>
            public float UnsafeGetTFromDistance(float distance)
            {
                return this._cache.UnsafeGetTFromDistance(distance);
            }

            /// <summary>
            /// Draw spline debug gizmos.
            /// </summary>
            /// <param name="origin">Point in the world where the gizmos should be drawn from</param>
            /// <param name="step">Step increment used to draw spline portions</param>
            public void DrawGizmos(Vector3 origin)
            {
                GUIStyle style = new GUIStyle();
                style.normal.textColor = Color.black;
                Vector3? prev = origin + this.Proxy.ToVector3(this.Compute(0f));
                for (float t = this.Gizmo.Step; t <= 1f; t += this.Gizmo.Step)
                {
                    Vector3 pt = this.Proxy.ToVector3(this.Compute(t));
                    Vector3 dt = this.Proxy.ToVector3(this.Derivative(t));
                    Vector3 curr = origin + pt;
                    Gizmos.color = Color.green;
                    Gizmos.DrawLine((Vector3)prev, curr);
                    Gizmos.DrawSphere(curr, 0.025f);
                    if (this.Gizmo.DrawXAxis)
                    {
                        Gizmos.color = Color.blue;
                        Gizmos.DrawSphere(origin + new Vector3(t, pt.x, 0f), 0.025f);
                    }
                    if (this.Gizmo.DrawYAxis)
                    {
                        Gizmos.color = Color.red;
                        Gizmos.DrawSphere(origin + new Vector3(t, pt.y, 0f), 0.025f);
                    }
                    if (this.Gizmo.DrawDerivativeXAxis)
                    {
                        Gizmos.color = Color.magenta;
                        Gizmos.DrawSphere(origin + new Vector3(t, dt.x, 0f), 0.025f);
                    }
                    if (this.Gizmo.DrawDerivativeYAxis)
                    {
                        Gizmos.color = Color.yellow;
                        Gizmos.DrawSphere(origin + new Vector3(t, dt.y, 0f), 0.025f);
                    }
                    if (this.Gizmo.ShowCoordinates)
                    {
                        Handles.Label(curr + new Vector3(.1f, 0f, 0f), $"t={t:0.00}: ({pt.x:0.000}; {pt.y:0.000}; {pt.z:0.000})", style);
                    }
                    prev = curr;
                }
                Gizmos.color = Color.green;
                prev = null;
                for (int i = 0; i < this.ControlPoints.Length; ++i)
                {
                    Vector3 cp = this.Proxy.ToVector3(this.ControlPoints[i]);
                    Vector3 curr = origin + cp;
                    Gizmos.DrawSphere(curr, 0.1f);
                    if (prev != null)
                        Gizmos.DrawLine((Vector3)prev, curr);
                    prev = curr;
                }
            }

            /// <summary>
            /// Compute the position on the spline at time t.
            /// </summary>
            /// <param name="t">Time value between 0 and 1</param>
            /// <returns>Computed point</returns>
            public T Compute(float t)
            {
                return this.BezierN(t, this.ControlPoints);
            }

            /// <summary>
            /// Interpolate a point between start and end at time t.
            /// Under the hood, this method calculates the t value on the curve
            /// corresponding to the distance given by curve_length * t, then
            /// performs a linear interpolation between start and end by the y
            /// coordinate of the point on the curve at the computed t value.
            /// </summary>
            /// <param name="start">Start position</param>
            /// <param name="end">End position</param>
            /// <param name="t">Time value between 0 and 1</param>
            /// <param name="lerp">Function to use to compute a linear interpolation between start and end.</param>
            /// <returns>Interpolated point</returns>
            public U Interpolate<U>(U start, U end, float t, Func<U, U, float, U> lerp)
            {
                float distance = this.GetCurveLength() * t;
                float t2 = this.GetTFromDistance(distance);
                float y = this.Proxy.ToVector3(this.Compute(t2)).y;
                return lerp(start, end, y);
            }

            public float Interpolate(float start, float end, float t)
            {
                return this.Interpolate(start, end, t, FloatSpline.Lerp);
            }

            public Vector2 Interpolate(Vector2 start, Vector2 end, float t)
            {
                return this.Interpolate(start, end, t, Vector2Spline.Lerp);
            }

            public Vector3 Interpolate(Vector3 start, Vector3 end, float t)
            {
                return this.Interpolate(start, end, t, Vector3Spline.Lerp);
            }

            /// <summary>
            /// Compute the derivative of the spline at time t.
            /// </summary>
            /// <param name="t">Time value between 0 and 1</param>
            /// <returns>Computed derivative</returns>
            public T Derivative(float t)
            {
                return this.BezierDerivativeN(t, this.ControlPoints);
            }

            /// <summary>
            /// Compute the position of a point at time t along the Bezier curve
            /// defined by the given control points.
            /// </summary>
            /// <param name="t">Time value between 0 and 1</param>
            /// <param name="controlPoints">Control points defining the Bezier curve</param>
            /// <returns>Position of a point along the Bezier curve at time t</returns>
            protected T BezierN(float t, params T[] controlPoints)
            {
                /// The explicit formula for a Bezier curve of order n is:
                /// C(t) = Sum[i=0, n]( b[n, i] * (1 - t)^(n-i) * t^i * P[i] )
                /// where b[n, i] = n! / (i! * (n - i)!)
                /// P[i] = control point at index i
                /// n = number of control points - 1
                int n = controlPoints.Length - 1;
                T sum = this.Proxy.Zero();
                for (int i = 0; i <= n; ++i)
                {
                    float b = Maths.BinomialCoefficient(n, i);
                    sum = this.Proxy.Add(sum, this.Proxy.Mul(controlPoints[i], b * Mathf.Pow(1f - t, n - i) * Mathf.Pow(t, i)));
                }
                return sum;
            }

            protected T BezierDerivativeN(float t, params T[] controlPoints)
            {
                /// The derivative for a Bezier curve of order n is:
                /// C'(t) = Sum[i=0, n-1]( B[n-1, i](t) * Q[n, i] )
                /// where B[n, i](t) = b[n, i] * t^i * (1 - t)^(n-i)
                /// b[n, i] = n! / (i! * (n - i)!)
                /// Q[n, i] = n(P[i+1] - P[i])
                /// P[i] = control point at index i
                /// n = number of control points - 1
                int n = controlPoints.Length - 1;
                T sum = this.Proxy.Zero();
                for (int i = 0; i <= n - 1; ++i)
                {
                    float b = Maths.BinomialCoefficient(n - 1, i);
                    float B = b * Mathf.Pow(t, i) * Mathf.Pow(1f - t, n - 1 - i);
                    T Q = this.Proxy.Mul(this.Proxy.Sub(this.ControlPoints[i + 1], this.ControlPoints[i]), n);
                    sum = this.Proxy.Add(sum, this.Proxy.Mul(Q, B));
                }
                return sum;
            }

            protected static class Maths
            {
                /// <summary>
                /// Compute the factorial of n
                /// </summary>
                public static int Factorial(int n)
                {
                    Debug.Assert(n >= 0);
                    int accumulator = 1;
                    for (int factor = 1; factor <= n; ++factor)
                        accumulator *= factor;
                    return accumulator;
                }

                /// <summary>
                /// Compute the binomial coefficient (n, k).
                /// </summary>
                public static float BinomialCoefficient(int n, int k)
                {
                    return Maths.Factorial(n)
                      / (Maths.Factorial(k) * Maths.Factorial(n - k));
                }
            }
        }
    }

    namespace SplineTypeProxies
    {
        public class FloatSplineTypeProxy : detail.ISplineTypeProxy<float>
        {
            public Vector3 ToVector3(float value) => new Vector3(value, value, 0f);
            public float Zero() => 0f;
            public float Add(float lhs, float rhs) => lhs + rhs;
            public float Sub(float lhs, float rhs) => lhs - rhs;
            public float Mul(float lhs, float rhs) => lhs * rhs;
            public float Distance(float lhs, float rhs) => Mathf.Abs(lhs - rhs);
            public bool Equals(float lhs, float rhs) => lhs == rhs;
            public float Lerp(float start, float end, float t) => FloatSpline.Lerp(start, end, t);
        }

        public class Vector3SplineTypeProxy : detail.ISplineTypeProxy<Vector3>
        {
            public Vector3 ToVector3(Vector3 value) => value;
            public Vector3 Zero() => Vector3.zero;
            public Vector3 Add(Vector3 lhs, Vector3 rhs) => lhs + rhs;
            public Vector3 Sub(Vector3 lhs, Vector3 rhs) => lhs - rhs;
            public Vector3 Mul(Vector3 lhs, float rhs) => lhs * rhs;
            public float Distance(Vector3 lhs, Vector3 rhs) => (lhs - rhs).magnitude;
            public bool Equals(Vector3 lhs, Vector3 rhs) => lhs == rhs;
            public Vector3 Lerp(Vector3 start, Vector3 end, float t) => Vector3Spline.Lerp(start, end, t);
        }

        public class Vector2SplineTypeProxy : detail.ISplineTypeProxy<Vector2>
        {
            public Vector3 ToVector3(Vector2 value) => new Vector3(value.x, value.y, 0f);
            public Vector2 Zero() => Vector2.zero;
            public Vector2 Add(Vector2 lhs, Vector2 rhs) => lhs + rhs;
            public Vector2 Sub(Vector2 lhs, Vector2 rhs) => lhs - rhs;
            public Vector2 Mul(Vector2 lhs, float rhs) => lhs * rhs;
            public float Distance(Vector2 lhs, Vector2 rhs) => (lhs - rhs).magnitude;
            public bool Equals(Vector2 lhs, Vector2 rhs) => lhs == rhs;
            public Vector2 Lerp(Vector2 start, Vector2 end, float t) => Vector2Spline.Lerp(start, end, t);
        }
    }

    [Serializable]
    /// <summary>
    /// Spline class that works with float control points.
    /// </summary>
    public class FloatSpline : detail.ASpline<float, SplineTypeProxies.FloatSplineTypeProxy>
    {
        /// <summary>
        /// Create an N-dimensional spline from N control points.
        /// </summary>
        /// <param name="controlPoints">Control points defining the spline</param>
        public FloatSpline(params float[] controlPoints)
          : base(controlPoints)
        {
        }

        /// <summary>
        /// Perform a linear interpolation between p0 and p1 by t.
        /// </summary>
        public static float Lerp(float p0, float p1, float t)
        {
            return new FloatSpline(p0, p1).Compute(t);
        }

        /// <summary>
        /// Create a linear spline object between p0 and p1.
        /// </summary>
        public static FloatSpline Linear(float p0, float p1)
        {
            return new FloatSpline(p0, p1);
        }

        /// <summary>
        /// Create a quadratic Bezier spline object between p0 and p2, with one
        /// control point p1.
        /// </summary>
        public static FloatSpline QuadraticBezier(float p0, float p1, float p2)
        {
            return new FloatSpline(p0, p1, p2);
        }

        /// <summary>
        /// Create a cubic Bezier spline object between p0 and p3, with two
        /// control points p1 and p2.
        /// </summary>
        public static FloatSpline CubicBezier(float p0, float p1, float p2, float p3)
        {
            return new FloatSpline(p0, p1, p2, p3);
        }
    }

    [Serializable]
    /// <summary>
    /// Spline class that works with Vector3 control points.
    /// </summary>
    public class Vector3Spline : detail.ASpline<Vector3, SplineTypeProxies.Vector3SplineTypeProxy>
    {
        /// <summary>
        /// Create an N-dimensional spline from N control points.
        /// </summary>
        /// <param name="controlPoints">Control points defining the spline</param>
        public Vector3Spline(params Vector3[] controlPoints)
          : base(controlPoints)
        {
        }

        /// <summary>
        /// Perform a linear interpolation between p0 and p1 by t.
        /// </summary>
        public static Vector3 Lerp(Vector3 p0, Vector3 p1, float t)
        {
            return new Vector3Spline(p0, p1).Compute(t);
        }

        /// <summary>
        /// Create a linear spline object between p0 and p1.
        /// </summary>
        public static Vector3Spline Linear(Vector3 p0, Vector3 p1)
        {
            return new Vector3Spline(p0, p1);
        }

        /// <summary>
        /// Create a quadratic Bezier spline object between p0 and p2, with one
        /// control point p1.
        /// </summary>
        public static Vector3Spline QuadraticBezier(Vector3 p0, Vector3 p1, Vector3 p2)
        {
            return new Vector3Spline(p0, p1, p2);
        }

        /// <summary>
        /// Create a cubic Bezier spline object between p0 and p3, with two
        /// control points p1 and p2.
        /// </summary>
        public static Vector3Spline CubicBezier(Vector3 p0, Vector3 p1, Vector3 p2, Vector3 p3)
        {
            return new Vector3Spline(p0, p1, p2, p3);
        }
    }

    [Serializable]
    /// <summary>
    /// Spline class that works with Vector2 control points.
    /// </summary>
    public class Vector2Spline : detail.ASpline<Vector2, SplineTypeProxies.Vector2SplineTypeProxy>
    {
        /// <summary>
        /// Create an N-dimensional spline from N control points.
        /// </summary>
        /// <param name="controlPoints">Control points defining the spline</param>
        public Vector2Spline(params Vector2[] controlPoints)
          : base(controlPoints)
        {
        }

        /// <summary>
        /// Perform a linear interpolation between p0 and p1 by t.
        /// </summary>
        public static Vector2 Lerp(Vector2 p0, Vector2 p1, float t)
        {
            return new Vector2Spline(p0, p1).Compute(t);
        }

        /// <summary>
        /// Create a linear spline object between p0 and p1.
        /// </summary>
        public static Vector2Spline Linear(Vector2 p0, Vector2 p1)
        {
            return new Vector2Spline(p0, p1);
        }

        /// <summary>
        /// Create a quadratic Bezier spline object between p0 and p2, with one
        /// control point p1.
        /// </summary>
        public static Vector2Spline QuadraticBezier(Vector2 p0, Vector2 p1, Vector2 p2)
        {
            return new Vector2Spline(p0, p1, p2);
        }

        /// <summary>
        /// Create a cubic Bezier spline object between p0 and p3, with two
        /// control points p1 and p2.
        /// </summary>
        public static Vector2Spline CubicBezier(Vector2 p0, Vector2 p1, Vector2 p2, Vector2 p3)
        {
            return new Vector2Spline(p0, p1, p2, p3);
        }
    }
}
