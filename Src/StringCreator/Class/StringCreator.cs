using System;
#if NET5_0_OR_GREATER
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
#endif

namespace StringCreator.Class
{
    public class StringCreator
    {
        internal readonly string _str;
        internal int _length;

        public StringCreator(int length)
        {
#if NET6_0_OR_GREATER
            _str = string.Create<object>(length, null, (_, _) => { });
#else
            _str = StringHelper.FastAllocateString(length);
#endif
        }

#if NET5_0_OR_GREATER
        [DoesNotReturn]
#endif
        internal static void ThrowMaximumStringLength(int length)
        {
            throw new Exception($"Maximum string length({length}) exceeded");
        }

#if NET5_0_OR_GREATER
        public StringCreator Append(ReadOnlySpan<char> chars)
#else
        public StringCreator Append(char[] chars)
#endif
        {
            if (_length + chars.Length > _str.Length)
            {
                ThrowMaximumStringLength(_str.Length);
            }

            unsafe
            {
                fixed (char* pSource = chars)
                fixed (char* pDest = _str)
                {
                    Buffer.MemoryCopy(pSource, pDest + _length, _str.Length * sizeof(char), chars.Length * sizeof(char));
                }

                _length += chars.Length;
            }

            return this;
        }

        public StringCreator Append(string str)
        {
            if (_length + str.Length > _str.Length)
            {
                ThrowMaximumStringLength(_str.Length);
            }

            unsafe
            {
                fixed (char* pSource = str)
                fixed (char* pDest = _str)
                {
                    Buffer.MemoryCopy(pSource, pDest + _length, _str.Length * sizeof(char), str.Length * sizeof(char));
                }

                _length += str.Length;
            }

            return this;
        }

        public StringCreator Append(char letter)
        {
            if(_length == _str.Length)
            {
                ThrowMaximumStringLength(_str.Length);
            }

            unsafe
            {
                fixed (char* pDest = _str)
                {
                    *(pDest + _length++) = letter;
                }
            }

            return this;
        }

        public override string ToString()
        {
            return _str;
        }

#if NET5_0_OR_GREATER
        public ReadOnlySpan<char> AsSpan()
        {
            return _str.AsSpan()[.._length];
        }
#endif

#if NET6_0_OR_GREATER
        public StringCreator Append([InterpolatedStringHandlerArgument("")] ref StringCreatorStringHandler handler) => this;

        public StringCreator Append(IFormatProvider provider, [InterpolatedStringHandlerArgument("", "provider")] ref StringCreatorStringHandler handler) => this;
#endif
    }
}