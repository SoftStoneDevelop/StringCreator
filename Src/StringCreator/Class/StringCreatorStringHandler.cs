#if NET6_0_OR_GREATER
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.Runtime.CompilerServices;

namespace StringCreator.Class
{
    [EditorBrowsable(EditorBrowsableState.Never)]
    [InterpolatedStringHandler]
    public struct StringCreatorStringHandler
    {
        private readonly StringCreator _stringCreator;
        private readonly IFormatProvider _provider;
        private readonly bool _hasCustomFormatter;

        public StringCreatorStringHandler(int literalLength, int formattedCount, StringCreator stringCreator)
        {
            _stringCreator = stringCreator;
            _provider = null;
            _hasCustomFormatter = false;
        }

        public StringCreatorStringHandler(int literalLength, int formattedCount, StringCreator stringCreator, IFormatProvider provider)
        {
            _stringCreator = stringCreator;
            _provider = provider;
            _hasCustomFormatter = provider is not null && HasCustomFormatter(provider);
        }

        public void AppendLiteral(string value) => _stringCreator.Append(value);

        #region AppendFormatted

        #region AppendFormatted T
        public void AppendFormatted<T>(T value)
        {
            if (_hasCustomFormatter)
            {
                AppendCustomFormatter(value, format: null);
            }
            else if (value is IFormattable)
            {
                if (value is ISpanFormattable)
                {
                    unsafe
                    {
                        fixed(char* pDest = _stringCreator._str)
                        {
                            if (((ISpanFormattable)value).TryFormat(new Span<char>(pDest + _stringCreator._length, _stringCreator._str.Length - _stringCreator._length), out int charsWritten, default, _provider))
                            {
                                _stringCreator._length += charsWritten;
                            }
                            else
                            {
                                StringCreator.ThrowMaximumStringLength(_stringCreator._length);
                            }
                        }
                    }
                }
                else
                {
                    _stringCreator.Append(((IFormattable)value).ToString(format: null, _provider));
                }
            }
            else if (value is not null)
            {
                _stringCreator.Append(value.ToString());
            }
        }

        public void AppendFormatted<T>(T value, string format)
        {
            if (_hasCustomFormatter)
            {
                AppendCustomFormatter(value, format);
            }
            else if (value is IFormattable)
            {
                if (value is ISpanFormattable)
                {
                    unsafe
                    {
                        fixed (char* pDest = _stringCreator._str)
                        {
                            if (((ISpanFormattable)value).TryFormat(new Span<char>(pDest + _stringCreator._length, _stringCreator._str.Length - _stringCreator._length), out int charsWritten, format, _provider))
                            {
                                _stringCreator._length += charsWritten;
                            }
                            else
                            {
                                StringCreator.ThrowMaximumStringLength(_stringCreator._length);
                            }
                        }
                    }
                }
                else
                {
                    _stringCreator.Append(((IFormattable)value).ToString(format, _provider));
                }
            }
            else if (value is not null)
            {
                _stringCreator.Append(value.ToString());
            }
        }

        public void AppendFormatted<T>(T value, int alignment) => AppendFormatted(value, alignment, format: null);

        public void AppendFormatted<T>(T value, int alignment, string format)
        {
            if (alignment == 0)
            {
                AppendFormatted(value, format);
            }
            else if (alignment < 0)
            {
                var start = _stringCreator._length;
                AppendFormatted(value, format);
                int paddingRequired = -alignment - (_stringCreator._str.Length - start);
                if (paddingRequired > 0)
                {
                    for (int i = 0; i < paddingRequired; i++)
                    {
                        _stringCreator.Append(' ');
                    }
                }
            }
            else
            {
                for (int i = 0; i < alignment; i++)
                {
                    _stringCreator.Append(' ');
                }
                AppendFormatted(value, format);
            }
        }
        #endregion

        #region AppendFormatted string
        public void AppendFormatted(string value)
        {
            if (!_hasCustomFormatter)
            {
                _stringCreator.Append(value);
            }
            else
            {
                AppendFormatted<string>(value);
            }
        }

        public void AppendFormatted(string value, int alignment = 0, string format = null) => AppendFormatted<string>(value, alignment, format);
        #endregion

        #region AppendFormatted object
        public void AppendFormatted(object value, int alignment = 0, string format = null) => AppendFormatted<object>(value, alignment, format);
        #endregion

        #region AppendFormatted ReadOnlySpan<char>
        public void AppendFormatted(ReadOnlySpan<char> value) => _stringCreator.Append(value);

        public void AppendFormatted(ReadOnlySpan<char> value, int alignment = 0, string format = null)
        {
            if (alignment == 0)
            {
                _stringCreator.Append(value);
            }
            else
            {
                bool leftAlign = false;
                if (alignment < 0)
                {
                    leftAlign = true;
                    alignment = -alignment;
                }

                int paddingRequired = alignment - value.Length;
                if (paddingRequired <= 0)
                {
                    _stringCreator.Append(value);
                }
                else if (leftAlign)
                {
                    _stringCreator.Append(value);
                    for (int i = 0; i < paddingRequired; i++)
                    {
                        _stringCreator.Append(' ');
                    }
                }
                else
                {
                    for (int i = 0; i < paddingRequired; i++)
                    {
                        _stringCreator.Append(' ');
                    }
                    _stringCreator.Append(value);
                }
            }
        }
        #endregion

        #endregion

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void AppendCustomFormatter<T>(T value, string format)
        {
            Debug.Assert(_hasCustomFormatter);
            Debug.Assert(_provider != null);

            ICustomFormatter formatter = (ICustomFormatter)_provider.GetFormat(typeof(ICustomFormatter));
            Debug.Assert(formatter != null, "An incorrectly written provider said it implemented ICustomFormatter, and then didn't");

            if (formatter is not null)
            {
                _stringCreator.Append(formatter.Format(format, value, _provider));
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static bool HasCustomFormatter(IFormatProvider provider)
        {
            Debug.Assert(provider is not null);
            Debug.Assert(provider is not CultureInfo || provider.GetFormat(typeof(ICustomFormatter)) is null, "Expected CultureInfo to not provide a custom formatter");
            return
                provider.GetType() != typeof(CultureInfo) &&
                provider.GetFormat(typeof(ICustomFormatter)) != null;
        }
    }
}

#endif