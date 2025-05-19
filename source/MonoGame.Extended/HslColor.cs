using System;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Microsoft.Xna.Framework;

namespace MonoGame.Extended
{
    /// <summary>
    /// Represents a color in the HSL (Hue, Saturation, Lightness) color space.
    /// </summary>
    /// <remarks>
    /// <list type="bullet">
    ///   <item>Hue (H) represents the color, ranging from 0 to 360 degrees on the color wheel.</item>
    ///   <item>Saturation (S) represents the intensity of the color, ranging from 0.0 (gray) to 1.0 (full color).</item>
    ///   <item>Lightness (L) represents the brightness, ranging from 0.0 (black) to 1.0 (white).</item>
    /// </list>
    /// </remarks>
    [StructLayout(LayoutKind.Sequential)]
    public struct HslColor : IEquatable<HslColor>, IComparable<HslColor>
    {

        /// <summary>
        /// The hue component of the color (in degrees) ranging from 0.0 to 360.0.
        /// </summary>
        public readonly float H;

        /// <summary>
        /// The saturation component of the color, ranging from 0.0 to 1.0.
        /// </summary>
        public readonly float S;

        /// <summary>
        /// The lightness component of the color, ranging from 0.0 to 1.0.
        /// </summary>
        public readonly float L;

        /// <summary>
        /// Initializes a new instance of the <see cref="HslColor"/> value with the
        /// specified hue, saturation and lightness components.
        /// </summary>
        /// <param name="h">The hue component (in degress) from 0.0 to 360.0.</param>
        /// <param name="s">The saturation component from 0.0 to 1.0.</param>
        /// <param name="l">The lightness component from 0.0 to 1.0.</param>
        public HslColor(float h, float s, float l)
        {
            H = Math.Clamp(h, 0.0f, 360.0f);
            S = Math.Clamp(s, 0.0f, 1.0f);
            L = Math.Clamp(l, 0.0f, 1.0f);
        }

        /// <summary>
        /// Copies the values of this <see cref="HslColor"/> value to a new instance.
        /// </summary>
        /// <param name="destination">
        /// When this method returns, contains a copy of this <see cref="HslColor"/>.
        /// </param>
        public readonly void CopyTo(out HslColor destination)
        {
            destination = new HslColor(H, S, L);
        }

        /// <summary>
        /// Copies this <see cref="HslColor"/> to the specified memory location using unsafe direct memory operations.
        /// </summary>
        /// <param name="destination">A pointer to the memory location where the color data will be copied.</param>
        /// <remarks>
        /// This method performs a direct memory copy without invoking constructors or field validations, 
        /// providing optimal performance for high-frequency operations.
        /// </remarks>
        public readonly unsafe void CopyToUnsafe(HslColor* destination)
        {
            Unsafe.Write(destination, Unsafe.As<HslColor, HslColor>(ref Unsafe.AsRef(in this)));
        }

        /// <summary>
        /// Destructures this <see cref="HslColor"/> into its constituent components.
        /// </summary>
        /// <param name="h">
        /// When this method returns, contains the hue component of this <see cref="HslColor"/>.
        /// </param>
        /// <param name="s">
        /// When this method returns, contains the saturation component of this <see cref="HslColor"/>.
        /// </param>
        /// <param name="l">
        /// When this method returns, contains the lightness component of this <see cref="HslColor"/>.
        /// </param>
        public readonly void Destructure(out float h, out float s, out float l)
        {
            h = H;
            s = S;
            l = L;
        }

        /// <summary>
        /// Executes a callback with the components of this <see cref="HslColor"/>.
        /// </summary>
        /// <param name="callback">The callback to execute.</param>
        /// <exception cref="ArgumentNullException">
        /// Thrown when <paramref name="callback"/> is <see langword="null"/>.
        /// </exception>
        public readonly void Match(Action<float, float, float> callback)
        {
            ArgumentNullException.ThrowIfNull(callback);
            callback(H, S, L);
        }

        /// <summary>
        /// Maps the components of this <see cref="HslColor"/> to a new value using the specified mapping function.
        /// </summary>
        /// <typeparam name="T">The type of the result of the mapping function.</typeparam>
        /// <param name="map">The mapping function to apply to the components of this <see cref="HslColor"/>.</param>
        /// <returns>
        /// The result of applying the mapping function to the components of this <see cref="HslColor"/>.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// Thrown when <paramref name="map"/> is <see langword="null"/>.
        /// </exception>
        public readonly T Map<T>(Func<float, float, float, T> map)
        {
            ArgumentNullException.ThrowIfNull(map);
            return map(H, S, L);
        }

        /// <summary>
        /// Implicitly converts a string to an <see cref="HslColor"/>.
        /// </summary>
        /// <param name="value">The string to convert.</param>
        /// <returns>The <see cref="HslColor"/> represented by the string.</returns>
        [Obsolete("This implicit conversion is deprecated and will be removed in a future version. Use HslColor.Parse() instead to make string parsing explicit and improve code readability.")]
        public static implicit operator HslColor(string value)
        {
            return Parse(value);
        }

        /// <summary>
        /// Compares this instance to a specified <see cref="HslColor"/> and returns an integer that indicates whether 
        /// the current instance precedes, follows, or occurs in the same position in the sort order as the specified 
        /// <see cref="HslColor"/>.
        /// </summary>
        /// <param name="other">The <see cref="HslColor"/> to compare with this instance.</param>
        /// <returns>A value that indicates the relative order of the objects being compared.</returns>
        public int CompareTo(HslColor other)
        {

            return H.CompareTo(other.H) * 100 + S.CompareTo(other.S) * 10 + L.CompareTo(other.L);
        }

        /// <summary>
        /// Determines whether the specified object is equal to the current HSL color.
        /// </summary>
        /// <param name="obj">The object to compare with the current HSL color..</param>
        /// <returns>
        /// <see langword="true"/> if the specified object is a <see cref="HslColor"/> and is equal to the
        /// current HSL color; otherwise, <see langword="false"/>.
        /// </returns>
        public override bool Equals([NotNullWhen(true)] object obj)
        {
            return obj is HslColor other && Equals(other);
        }

        /// <summary>
        /// Determines whether the specified HSL color is equal to the current HSL color
        /// </summary>
        /// <param name="other">The HSL color to compare with the current HSL color.</param>
        /// <returns>
        /// <see langword="true"/> if the specified line segment is equal to the current line segment;
        /// otherwise, <see langword="false"/>.
        /// </returns>
        public readonly bool Equals(HslColor other)
        {
            return H.Equals(other.H) &&
                   L.Equals(other.L) &&
                   S.Equals(other.S);
        }


        /// <summary>
        /// Returns the hash code for this HSL color.
        /// </summary>
        /// <returns>A 32-bit signed integer that is the hash code for this instance.</returns>
        public override int GetHashCode()
        {
            return H.GetHashCode() ^
                   S.GetHashCode() ^
                   L.GetHashCode();
        }

        /// <summary>
        /// Returns a string representation of this <see cref="HslColor"/>.
        /// </summary>
        /// <returns>A string representation of this <see cref="HslColor"/>.</returns>
        public override string ToString()
        {
            return string.Format(CultureInfo.InvariantCulture, "H:{0:N1}° S:{1:N1} L:{2:N1}",
                H, 100 * S, 100 * L);
        }

        /// <summary>
        /// Parses a string into an <see cref="HslColor"/>.
        /// </summary>
        /// <param name="s">The string to parse.</param>
        /// <returns>The <see cref="HslColor"/> represented by the string.</returns>
        /// <remarks>
        /// The input string should be in the format "hue,saturation,lightness", where hue is in degrees
        /// (optionally followed by the '°' symbol), and saturation and lightness are decimal values.
        /// </remarks>
        public static HslColor Parse(string s)
        {
            var hsl = s.Split(',');
            var hue = float.Parse(hsl[0].TrimEnd('°'), CultureInfo.InvariantCulture.NumberFormat);
            var sat = float.Parse(hsl[1], CultureInfo.InvariantCulture.NumberFormat);
            var lig = float.Parse(hsl[2], CultureInfo.InvariantCulture.NumberFormat);

            return new HslColor(hue, sat, lig);
        }


        /// <summary>
        /// Determines whether two <see cref="HslColor"/> instances are equal.
        /// </summary>
        /// <param name="lhs">The first <see cref="HslColor"/> to compare.</param>
        /// <param name="rhs">The second <see cref="HslColor"/> to compare.</param>
        /// <returns>
        /// <see langword="true"/> if the HSL colors are equal; otherwise, <see langword="false"/>.
        /// </returns>
        public static bool operator ==(HslColor lhs, HslColor rhs)
        {
            return lhs.Equals(rhs);
        }

        /// <summary>
        /// Determines whether two <see cref="HslColor"/> instances are not equal.
        /// </summary>
        /// <param name="lhs">The first <see cref="HslColor"/> to compare.</param>
        /// <param name="rhs">The second <see cref="HslColor"/> to compare.</param>
        /// <returns>
        /// <see langword="true"/> if the HSL colors are not equal; otherwise, <see langword="false"/>.
        /// </returns>
        public static bool operator !=(HslColor lhs, HslColor rhs)
        {
            return !lhs.Equals(rhs);
        }

        /// <summary>
        /// Adds two <see cref="HslColor"/> instances together.
        /// </summary>
        /// <param name="a">The first <see cref="HslColor"/> to add.</param>
        /// <param name="b">The second <see cref="HslColor"/> to add.</param>
        /// <returns>
        /// A new <see cref="HslColor"/> that is the sum of the two input colors.
        /// </returns>
        public static HslColor operator +(HslColor a, HslColor b)
        {
            return new HslColor(a.H + b.H, a.S + b.S, a.L + b.L);
        }

        /// <summary>
        /// Subtracts one <see cref="HslColor"/> from another.
        /// </summary>
        /// <param name="lhs">The <see cref="HslColor"/> to subtract from.</param>
        /// <param name="rhs">The <see cref="HslColor"/> to subtract.</param>
        /// <returns>A new <see cref="HslColor"/> that is the difference of the two input colors.</returns>
        public static HslColor operator -(HslColor lhs, HslColor rhs)
        {
            return new HslColor(lhs.H - rhs.H, lhs.S - rhs.S, lhs.L - rhs.L);
        }

        /// <summary>
        /// Linearly interpolates between two <see cref="HslColor"/> values.
        /// </summary>
        /// <param name="color1">The first <see cref="HslColor"/>.</param>
        /// <param name="color2">The second <see cref="HslColor"/>.</param>
        /// <param name="amount">The interpolation factor. A value of 0 returns <paramref name="color1"/>, a value of 1 returns <paramref name="color2"/>.</param>
        /// <returns>The interpolated <see cref="HslColor"/>.</returns>
        public static HslColor Lerp(HslColor color1, HslColor color2, float amount)
        {
            var h2 = color2.H >= color1.H ? color2.H : color2.H + 360;
            return new HslColor(
                color1.H + amount * (h2 - color1.H),
                color1.S + amount * (color2.S - color1.S),
                color1.L + amount * (color2.L - color1.L));
        }

        /// <summary>
        /// Converts an RGB color to an HSL color.
        /// </summary>
        /// <param name="color">The RGB color to convert.</param>
        /// <returns>The equivalent HSL color.</returns>
        public static HslColor FromRgb(Color color)
        {
            // derived from http://www.geekymonkey.com/Programming/CSharp/RGB2HSL_HSL2RGB.htm
            var r = color.R / 255f;
            var g = color.G / 255f;
            var b = color.B / 255f;
            var h = 0f;
            var s = 0f;
            var l = 0f;
            var v = Math.Max(r, g);
            v = Math.Max(v, b);

            var m = Math.Min(r, g);
            m = Math.Min(m, b);
            l = (m + v) / 2.0f;

            if (l <= 0.0)
                return new HslColor(h, s, l);

            var vm = v - m;
            s = vm;

            if (s > 0.0)
                s /= l <= 0.5f ? v + m : 2.0f - v - m;
            else
                return new HslColor(h, s, l);

            var r2 = (v - r) / vm;
            var g2 = (v - g) / vm;
            var b2 = (v - b) / vm;

            if (Math.Abs(r - v) < float.Epsilon)
                h = Math.Abs(g - m) < float.Epsilon ? 5.0f + b2 : 1.0f - g2;
            else if (Math.Abs(g - v) < float.Epsilon)
                h = Math.Abs(b - m) < float.Epsilon ? 1.0f + r2 : 3.0f - b2;
            else
                h = Math.Abs(r - m) < float.Epsilon ? 3.0f + g2 : 5.0f - r2;

            h *= 60;

            return new HslColor(h, s, l);
        }
    }
}
