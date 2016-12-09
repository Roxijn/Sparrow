using System;
using System.Text;
using System.Collections;
using System.Collections.Generic;

namespace Sparrow.Numerics
{
    public partial struct Vector<T> : IEnumerable<T> where T : struct
    {
        public Vector(T x, T y) : this(new T[] {x, y}) {}
        public Vector(T x, T y, T z) : this(new T[] {x, y, z}) {}
        public Vector(T x, T y, T z, T w) : this(new T[] {x, y, z, w}) {}
        public Vector(params T[] items) { this.items = items; }

        static Vector()
        {
            if (typeof(T) == typeof(float))
                math = new MathFloat() as Math<T>;
            else if (typeof(T) == typeof(double))
                math = new MathDouble() as Math<T>;
            if (math == null)
                throw new InvalidOperationException("Type " + typeof(T) + " is not supported by Vector.");
        }

        private readonly T[] items;
        private static readonly Math<T> math;

        public T this[int i] { get { return items[i]; } }
        public int Count { get { return items.Length; } }

        public T x { get { return items[0]; } }
        public T y { get { return items[1]; } }
        public T z { get { return items[2]; } }
        public T w { get { return items[3]; } }

        public static T Dot(Vector<T> a, Vector<T> b) { return math.Dot(a, b); }
        public static Vector<T> Cross(Vector<T> a, Vector<T> b) { return math.Cross(a, b); }
        public static Vector<T> Zeros(int size) { return math.Zeros(size); }
        public static Vector<T> Ones(int size) { return math.Ones(size); }
        public static Vector<T> operator +(Vector<T> a, Vector<T> b) { return math.Add(a, b); }
        public static Vector<T> operator -(Vector<T> a, Vector<T> b) { return math.Sub(a, b); }
        public static Vector<T> operator *(Vector<T> a, T b) { return math.Mul(a, b); }
        public static Vector<T> operator *(T a, Vector<T> b) { return math.Mul(b, a); }

        public Vector<T> Unit { get { return math.Unit(this); } }
        public T Magnitude { get { return math.Magnitude(this); } }
        public T SquareMagnitude { get { return math.SquareMagnitude(this); } }

        public Vector<T> Resize(int size)
        {
            if (Count == size) return this;
            var result = new T[size];
            for (int i = Math.Min(size, Count); i-- != 0;)
                result[i] = this[i];
            return new Vector<T>(result);
        }

        IEnumerator<T> IEnumerable<T>.GetEnumerator() { return items.GetEnumerator() as IEnumerator<T>; }
        IEnumerator IEnumerable.GetEnumerator() { return items.GetEnumerator(); }
        
        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append("Vector<");
            sb.Append(typeof(T));
            sb.Append(">(");
            for(int i = 0; i < this.Count; i++)
            {
                sb.Append(items[i]);
            }
            sb.Append(")");
            return sb.ToString();
        }
    }
}