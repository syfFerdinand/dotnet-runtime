﻿// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using System.Diagnostics;
using System.Numerics;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Tests;
using Xunit;

namespace System.Runtime.Intrinsics.Tests.Vectors
{
    public sealed class Vector64Tests
    {
        /// <summary>Verifies that two <see cref="Vector64{Single}" /> values are equal, within the <paramref name="variance" />.</summary>
        /// <param name="expected">The expected value</param>
        /// <param name="actual">The value to be compared against</param>
        /// <param name="variance">The total variance allowed between the expected and actual results.</param>
        /// <exception cref="EqualException">Thrown when the values are not equal</exception>
        internal static void AssertEqual(Vector64<float> expected, Vector64<float> actual, Vector64<float> variance)
        {
            for (int i = 0; i < Vector64<float>.Count; i++)
            {
                AssertExtensions.Equal(expected.GetElement(i), actual.GetElement(i), variance.GetElement(i));
            }
        }

        /// <summary>Verifies that two <see cref="Vector64{Double}" /> values are equal, within the <paramref name="variance" />.</summary>
        /// <param name="expected">The expected value</param>
        /// <param name="actual">The value to be compared against</param>
        /// <param name="variance">The total variance allowed between the expected and actual results.</param>
        /// <exception cref="EqualException">Thrown when the values are not equal</exception>
        internal static void AssertEqual(Vector64<double> expected, Vector64<double> actual, Vector64<double> variance)
        {
            for (int i = 0; i < Vector64<double>.Count; i++)
            {
                AssertExtensions.Equal(expected.GetElement(i), actual.GetElement(i), variance.GetElement(i));
            }
        }

        [Fact]
        public unsafe void Vector64IsHardwareAcceleratedTest()
        {
            MethodInfo methodInfo = typeof(Vector64).GetMethod("get_IsHardwareAccelerated");
            Assert.Equal(Vector64.IsHardwareAccelerated, methodInfo.Invoke(null, null));
        }

        [Fact]
        public unsafe void Vector64ByteExtractMostSignificantBitsTest()
        {
            Vector64<byte> vector = Vector64.Create(
                0x01,
                0x80,
                0x01,
                0x80,
                0x01,
                0x80,
                0x01,
                0x80
            );

            uint result = Vector64.ExtractMostSignificantBits(vector);
            Assert.Equal(0b10101010u, result);
        }

        [Fact]
        public unsafe void Vector64DoubleExtractMostSignificantBitsTest()
        {
            Vector64<double> vector = Vector64.Create(
                +1.0
            );

            uint result = Vector64.ExtractMostSignificantBits(vector);
            Assert.Equal(0b0u, result);

            vector = Vector64.Create(
                -0.0
            );

            result = Vector64.ExtractMostSignificantBits(vector);
            Assert.Equal(0b1u, result);
        }

        [Fact]
        public unsafe void Vector64Int16ExtractMostSignificantBitsTest()
        {
            Vector64<short> vector = Vector64.Create(
                0x0001,
                0x8000,
                0x0001,
                0x8000
            ).AsInt16();

            uint result = Vector64.ExtractMostSignificantBits(vector);
            Assert.Equal(0b1010u, result);
        }

        [Fact]
        public unsafe void Vector64Int32ExtractMostSignificantBitsTest()
        {
            Vector64<int> vector = Vector64.Create(
                0x00000001U,
                0x80000000U
            ).AsInt32();

            uint result = Vector64.ExtractMostSignificantBits(vector);
            Assert.Equal(0b10u, result);
        }

        [Fact]
        public unsafe void Vector64Int64ExtractMostSignificantBitsTest()
        {
            Vector64<long> vector = Vector64.Create(
                0x0000000000000001UL
            ).AsInt64();

            uint result = Vector64.ExtractMostSignificantBits(vector);
            Assert.Equal(0b0u, result);

            vector = Vector64.Create(
                0x8000000000000000UL
            ).AsInt64();

            result = Vector64.ExtractMostSignificantBits(vector);
            Assert.Equal(0b1u, result);
        }

        [Fact]
        public unsafe void Vector64NIntExtractMostSignificantBitsTest()
        {
            if (Environment.Is64BitProcess)
            {
                Vector64<nint> vector = Vector64.Create(
                    0x0000000000000001UL
                ).AsNInt();

                uint result = Vector64.ExtractMostSignificantBits(vector);
                Assert.Equal(0b0u, result);

                vector = Vector64.Create(
                    0x8000000000000000UL
                ).AsNInt();

                result = Vector64.ExtractMostSignificantBits(vector);
                Assert.Equal(0b1u, result);
            }
            else
            {
                Vector64<nint> vector = Vector64.Create(
                    0x00000001U,
                    0x80000000U
                ).AsNInt();

                uint result = Vector64.ExtractMostSignificantBits(vector);
                Assert.Equal(0b10u, result);
            }
        }

        [Fact]
        public unsafe void Vector64NUIntExtractMostSignificantBitsTest()
        {
            if (Environment.Is64BitProcess)
            {
                Vector64<nuint> vector = Vector64.Create(
                    0x0000000000000001UL
                ).AsNUInt();

                uint result = Vector64.ExtractMostSignificantBits(vector);
                Assert.Equal(0b0u, result);

                vector = Vector64.Create(
                    0x8000000000000000UL
                ).AsNUInt();

                result = Vector64.ExtractMostSignificantBits(vector);
                Assert.Equal(0b1u, result);
            }
            else
            {
                Vector64<nuint> vector = Vector64.Create(
                    0x00000001U,
                    0x80000000U
                ).AsNUInt();

                uint result = Vector64.ExtractMostSignificantBits(vector);
                Assert.Equal(0b10u, result);
            }
        }

        [Fact]
        public unsafe void Vector64SByteExtractMostSignificantBitsTest()
        {
            Vector64<sbyte> vector = Vector64.Create(
                0x01,
                0x80,
                0x01,
                0x80,
                0x01,
                0x80,
                0x01,
                0x80
            ).AsSByte();

            uint result = Vector64.ExtractMostSignificantBits(vector);
            Assert.Equal(0b10101010u, result);
        }

        [Fact]
        public unsafe void Vector64SingleExtractMostSignificantBitsTest()
        {
            Vector64<float> vector = Vector64.Create(
                +1.0f,
                -0.0f
            );

            uint result = Vector64.ExtractMostSignificantBits(vector);
            Assert.Equal(0b10u, result);
        }

        [Fact]
        public unsafe void Vector64UInt16ExtractMostSignificantBitsTest()
        {
            Vector64<ushort> vector = Vector64.Create(
                0x0001,
                0x8000,
                0x0001,
                0x8000
            );

            uint result = Vector64.ExtractMostSignificantBits(vector);
            Assert.Equal(0b1010u, result);
        }

        [Fact]
        public unsafe void Vector64UInt32ExtractMostSignificantBitsTest()
        {
            Vector64<uint> vector = Vector64.Create(
                0x00000001U,
                0x80000000U
            );

            uint result = Vector64.ExtractMostSignificantBits(vector);
            Assert.Equal(0b10u, result);
        }

        [Fact]
        public unsafe void Vector64UInt64ExtractMostSignificantBitsTest()
        {
            Vector64<ulong> vector = Vector64.Create(
                0x0000000000000001UL
            );

            uint result = Vector64.ExtractMostSignificantBits(vector);
            Assert.Equal(0b0u, result);

            vector = Vector64.Create(
                0x8000000000000000UL
            );

            result = Vector64.ExtractMostSignificantBits(vector);
            Assert.Equal(0b1u, result);
        }

        [Fact]
        public unsafe void Vector64ByteLoadTest()
        {
            byte* value = stackalloc byte[8] {
                0,
                1,
                2,
                3,
                4,
                5,
                6,
                7,
            };

            Vector64<byte> vector = Vector64.Load(value);

            for (int index = 0; index < Vector64<byte>.Count; index++)
            {
                Assert.Equal((byte)index, vector.GetElement(index));
            }
        }

        [Fact]
        public unsafe void Vector64DoubleLoadTest()
        {
            double* value = stackalloc double[1] {
                0,
            };

            Vector64<double> vector = Vector64.Load(value);

            for (int index = 0; index < Vector64<double>.Count; index++)
            {
                Assert.Equal((double)index, vector.GetElement(index));
            }
        }

        [Fact]
        public unsafe void Vector64Int16LoadTest()
        {
            short* value = stackalloc short[4] {
                0,
                1,
                2,
                3,
            };

            Vector64<short> vector = Vector64.Load(value);

            for (int index = 0; index < Vector64<short>.Count; index++)
            {
                Assert.Equal((short)index, vector.GetElement(index));
            }
        }

        [Fact]
        public unsafe void Vector64Int32LoadTest()
        {
            int* value = stackalloc int[2] {
                0,
                1,
            };

            Vector64<int> vector = Vector64.Load(value);

            for (int index = 0; index < Vector64<int>.Count; index++)
            {
                Assert.Equal((int)index, vector.GetElement(index));
            }
        }

        [Fact]
        public unsafe void Vector64Int64LoadTest()
        {
            long* value = stackalloc long[1] {
                0,
            };

            Vector64<long> vector = Vector64.Load(value);

            for (int index = 0; index < Vector64<long>.Count; index++)
            {
                Assert.Equal((long)index, vector.GetElement(index));
            }
        }

        [Fact]
        public unsafe void Vector64NIntLoadTest()
        {
            if (Environment.Is64BitProcess)
            {
                nint* value = stackalloc nint[1] {
                    0,
                };

                Vector64<nint> vector = Vector64.Load(value);

                for (int index = 0; index < Vector64<nint>.Count; index++)
                {
                    Assert.Equal((nint)index, vector.GetElement(index));
                }
            }
            else
            {
                nint* value = stackalloc nint[2] {
                    0,
                    1,
                };

                Vector64<nint> vector = Vector64.Load(value);

                for (int index = 0; index < Vector64<nint>.Count; index++)
                {
                    Assert.Equal((nint)index, vector.GetElement(index));
                }
            }
        }

        [Fact]
        public unsafe void Vector64NUIntLoadTest()
        {
            if (Environment.Is64BitProcess)
            {
                nuint* value = stackalloc nuint[1] {
                    0,
                };

                Vector64<nuint> vector = Vector64.Load(value);

                for (int index = 0; index < Vector64<nuint>.Count; index++)
                {
                    Assert.Equal((nuint)index, vector.GetElement(index));
                }
            }
            else
            {
                nuint* value = stackalloc nuint[2] {
                    0,
                    1,
                };

                Vector64<nuint> vector = Vector64.Load(value);

                for (int index = 0; index < Vector64<nuint>.Count; index++)
                {
                    Assert.Equal((nuint)index, vector.GetElement(index));
                }
            }
        }

        [Fact]
        public unsafe void Vector64SByteLoadTest()
        {
            sbyte* value = stackalloc sbyte[8] {
                0,
                1,
                2,
                3,
                4,
                5,
                6,
                7,
            };

            Vector64<sbyte> vector = Vector64.Load(value);

            for (int index = 0; index < Vector64<sbyte>.Count; index++)
            {
                Assert.Equal((sbyte)index, vector.GetElement(index));
            }
        }

        [Fact]
        public unsafe void Vector64SingleLoadTest()
        {
            float* value = stackalloc float[2] {
                0,
                1,
            };

            Vector64<float> vector = Vector64.Load(value);

            for (int index = 0; index < Vector64<float>.Count; index++)
            {
                Assert.Equal((float)index, vector.GetElement(index));
            }
        }

        [Fact]
        public unsafe void Vector64UInt16LoadTest()
        {
            ushort* value = stackalloc ushort[4] {
                0,
                1,
                2,
                3,
            };

            Vector64<ushort> vector = Vector64.Load(value);

            for (int index = 0; index < Vector64<ushort>.Count; index++)
            {
                Assert.Equal((ushort)index, vector.GetElement(index));
            }
        }

        [Fact]
        public unsafe void Vector64UInt32LoadTest()
        {
            uint* value = stackalloc uint[2] {
                0,
                1,
            };

            Vector64<uint> vector = Vector64.Load(value);

            for (int index = 0; index < Vector64<uint>.Count; index++)
            {
                Assert.Equal((uint)index, vector.GetElement(index));
            }
        }

        [Fact]
        public unsafe void Vector64UInt64LoadTest()
        {
            ulong* value = stackalloc ulong[1] {
                0,
            };

            Vector64<ulong> vector = Vector64.Load(value);

            for (int index = 0; index < Vector64<ulong>.Count; index++)
            {
                Assert.Equal((ulong)index, vector.GetElement(index));
            }
        }

        [Fact]
        public unsafe void Vector64ByteLoadAlignedTest()
        {
            byte* value = null;

            try
            {
                value = (byte*)NativeMemory.AlignedAlloc(byteCount: 8, alignment: 8);

                value[0] = 0;
                value[1] = 1;
                value[2] = 2;
                value[3] = 3;
                value[4] = 4;
                value[5] = 5;
                value[6] = 6;
                value[7] = 7;

                Vector64<byte> vector = Vector64.LoadAligned(value);

                for (int index = 0; index < Vector64<byte>.Count; index++)
                {
                    Assert.Equal((byte)index, vector.GetElement(index));
                }
            }
            finally
            {
                NativeMemory.AlignedFree(value);
            }
        }

        [Fact]
        public unsafe void Vector64DoubleLoadAlignedTest()
        {
            double* value = null;

            try
            {
                value = (double*)NativeMemory.AlignedAlloc(byteCount: 8, alignment: 8);

                value[0] = 0;

                Vector64<double> vector = Vector64.LoadAligned(value);

                for (int index = 0; index < Vector64<double>.Count; index++)
                {
                    Assert.Equal((double)index, vector.GetElement(index));
                }
            }
            finally
            {
                NativeMemory.AlignedFree(value);
            }
        }

        [Fact]
        public unsafe void Vector64Int16LoadAlignedTest()
        {
            short* value = null;

            try
            {
                value = (short*)NativeMemory.AlignedAlloc(byteCount: 8, alignment: 8);

                value[0] = 0;
                value[1] = 1;
                value[2] = 2;
                value[3] = 3;

                Vector64<short> vector = Vector64.LoadAligned(value);

                for (int index = 0; index < Vector64<short>.Count; index++)
                {
                    Assert.Equal((short)index, vector.GetElement(index));
                }
            }
            finally
            {
                NativeMemory.AlignedFree(value);
            }
        }

        [Fact]
        public unsafe void Vector64Int32LoadAlignedTest()
        {
            int* value = null;

            try
            {
                value = (int*)NativeMemory.AlignedAlloc(byteCount: 8, alignment: 8);

                value[0] = 0;
                value[1] = 1;

                Vector64<int> vector = Vector64.LoadAligned(value);

                for (int index = 0; index < Vector64<int>.Count; index++)
                {
                    Assert.Equal((int)index, vector.GetElement(index));
                }
            }
            finally
            {
                NativeMemory.AlignedFree(value);
            }
        }

        [Fact]
        public unsafe void Vector64Int64LoadAlignedTest()
        {
            long* value = null;

            try
            {
                value = (long*)NativeMemory.AlignedAlloc(byteCount: 8, alignment: 8);

                value[0] = 0;

                Vector64<long> vector = Vector64.LoadAligned(value);

                for (int index = 0; index < Vector64<long>.Count; index++)
                {
                    Assert.Equal((long)index, vector.GetElement(index));
                }
            }
            finally
            {
                NativeMemory.AlignedFree(value);
            }
        }

        [Fact]
        public unsafe void Vector64NIntLoadAlignedTest()
        {
            nint* value = null;

            try
            {
                value = (nint*)NativeMemory.AlignedAlloc(byteCount: 8, alignment: 8);

                if (Environment.Is64BitProcess)
                {
                    value[0] = 0;
                }
                else
                {
                    value[0] = 0;
                    value[1] = 1;
                }

                Vector64<nint> vector = Vector64.LoadAligned(value);

                for (int index = 0; index < Vector64<nint>.Count; index++)
                {
                    Assert.Equal((nint)index, vector.GetElement(index));
                }
            }
            finally
            {
                NativeMemory.AlignedFree(value);
            }
        }

        [Fact]
        public unsafe void Vector64NUIntLoadAlignedTest()
        {
            nuint* value = null;

            try
            {
                value = (nuint*)NativeMemory.AlignedAlloc(byteCount: 8, alignment: 8);

                if (Environment.Is64BitProcess)
                {
                    value[0] = 0;
                }
                else
                {
                    value[0] = 0;
                    value[1] = 1;
                }

                Vector64<nuint> vector = Vector64.LoadAligned(value);

                for (int index = 0; index < Vector64<nuint>.Count; index++)
                {
                    Assert.Equal((nuint)index, vector.GetElement(index));
                }
            }
            finally
            {
                NativeMemory.AlignedFree(value);
            }
        }

        [Fact]
        public unsafe void Vector64SByteLoadAlignedTest()
        {
            sbyte* value = null;

            try
            {
                value = (sbyte*)NativeMemory.AlignedAlloc(byteCount: 8, alignment: 8);

                value[0] = 0;
                value[1] = 1;
                value[2] = 2;
                value[3] = 3;
                value[4] = 4;
                value[5] = 5;
                value[6] = 6;
                value[7] = 7;

                Vector64<sbyte> vector = Vector64.LoadAligned(value);

                for (int index = 0; index < Vector64<sbyte>.Count; index++)
                {
                    Assert.Equal((sbyte)index, vector.GetElement(index));
                }
            }
            finally
            {
                NativeMemory.AlignedFree(value);
            }
        }

        [Fact]
        public unsafe void Vector64SingleLoadAlignedTest()
        {
            float* value = null;

            try
            {
                value = (float*)NativeMemory.AlignedAlloc(byteCount: 8, alignment: 8);

                value[0] = 0;
                value[1] = 1;

                Vector64<float> vector = Vector64.LoadAligned(value);

                for (int index = 0; index < Vector64<float>.Count; index++)
                {
                    Assert.Equal((float)index, vector.GetElement(index));
                }
            }
            finally
            {
                NativeMemory.AlignedFree(value);
            }
        }

        [Fact]
        public unsafe void Vector64UInt16LoadAlignedTest()
        {
            ushort* value = null;

            try
            {
                value = (ushort*)NativeMemory.AlignedAlloc(byteCount: 8, alignment: 8);

                value[0] = 0;
                value[1] = 1;
                value[2] = 2;
                value[3] = 3;

                Vector64<ushort> vector = Vector64.LoadAligned(value);

                for (int index = 0; index < Vector64<ushort>.Count; index++)
                {
                    Assert.Equal((ushort)index, vector.GetElement(index));
                }
            }
            finally
            {
                NativeMemory.AlignedFree(value);
            }
        }

        [Fact]
        public unsafe void Vector64UInt32LoadAlignedTest()
        {
            uint* value = null;

            try
            {
                value = (uint*)NativeMemory.AlignedAlloc(byteCount: 8, alignment: 8);

                value[0] = 0;
                value[1] = 1;

                Vector64<uint> vector = Vector64.LoadAligned(value);

                for (int index = 0; index < Vector64<uint>.Count; index++)
                {
                    Assert.Equal((uint)index, vector.GetElement(index));
                }
            }
            finally
            {
                NativeMemory.AlignedFree(value);
            }
        }

        [Fact]
        public unsafe void Vector64UInt64LoadAlignedTest()
        {
            ulong* value = null;

            try
            {
                value = (ulong*)NativeMemory.AlignedAlloc(byteCount: 8, alignment: 8);

                value[0] = 0;

                Vector64<ulong> vector = Vector64.LoadAligned(value);

                for (int index = 0; index < Vector64<ulong>.Count; index++)
                {
                    Assert.Equal((ulong)index, vector.GetElement(index));
                }
            }
            finally
            {
                NativeMemory.AlignedFree(value);
            }
        }

        [Fact]
        public unsafe void Vector64ByteLoadAlignedNonTemporalTest()
        {
            byte* value = null;

            try
            {
                value = (byte*)NativeMemory.AlignedAlloc(byteCount: 8, alignment: 8);

                value[0] = 0;
                value[1] = 1;
                value[2] = 2;
                value[3] = 3;
                value[4] = 4;
                value[5] = 5;
                value[6] = 6;
                value[7] = 7;

                Vector64<byte> vector = Vector64.LoadAlignedNonTemporal(value);

                for (int index = 0; index < Vector64<byte>.Count; index++)
                {
                    Assert.Equal((byte)index, vector.GetElement(index));
                }
            }
            finally
            {
                NativeMemory.AlignedFree(value);
            }
        }

        [Fact]
        public unsafe void Vector64DoubleLoadAlignedNonTemporalTest()
        {
            double* value = null;

            try
            {
                value = (double*)NativeMemory.AlignedAlloc(byteCount: 8, alignment: 8);

                value[0] = 0;

                Vector64<double> vector = Vector64.LoadAlignedNonTemporal(value);

                for (int index = 0; index < Vector64<double>.Count; index++)
                {
                    Assert.Equal((double)index, vector.GetElement(index));
                }
            }
            finally
            {
                NativeMemory.AlignedFree(value);
            }
        }

        [Fact]
        public unsafe void Vector64Int16LoadAlignedNonTemporalTest()
        {
            short* value = null;

            try
            {
                value = (short*)NativeMemory.AlignedAlloc(byteCount: 8, alignment: 8);

                value[0] = 0;
                value[1] = 1;
                value[2] = 2;
                value[3] = 3;

                Vector64<short> vector = Vector64.LoadAlignedNonTemporal(value);

                for (int index = 0; index < Vector64<short>.Count; index++)
                {
                    Assert.Equal((short)index, vector.GetElement(index));
                }
            }
            finally
            {
                NativeMemory.AlignedFree(value);
            }
        }

        [Fact]
        public unsafe void Vector64Int32LoadAlignedNonTemporalTest()
        {
            int* value = null;

            try
            {
                value = (int*)NativeMemory.AlignedAlloc(byteCount: 8, alignment: 8);

                value[0] = 0;
                value[1] = 1;

                Vector64<int> vector = Vector64.LoadAlignedNonTemporal(value);

                for (int index = 0; index < Vector64<int>.Count; index++)
                {
                    Assert.Equal((int)index, vector.GetElement(index));
                }
            }
            finally
            {
                NativeMemory.AlignedFree(value);
            }
        }

        [Fact]
        public unsafe void Vector64Int64LoadAlignedNonTemporalTest()
        {
            long* value = null;

            try
            {
                value = (long*)NativeMemory.AlignedAlloc(byteCount: 8, alignment: 8);

                value[0] = 0;

                Vector64<long> vector = Vector64.LoadAlignedNonTemporal(value);

                for (int index = 0; index < Vector64<long>.Count; index++)
                {
                    Assert.Equal((long)index, vector.GetElement(index));
                }
            }
            finally
            {
                NativeMemory.AlignedFree(value);
            }
        }

        [Fact]
        public unsafe void Vector64NIntLoadAlignedNonTemporalTest()
        {
            nint* value = null;

            try
            {
                value = (nint*)NativeMemory.AlignedAlloc(byteCount: 8, alignment: 8);

                if (Environment.Is64BitProcess)
                {
                    value[0] = 0;
                }
                else
                {
                    value[0] = 0;
                    value[1] = 1;
                }

                Vector64<nint> vector = Vector64.LoadAlignedNonTemporal(value);

                for (int index = 0; index < Vector64<nint>.Count; index++)
                {
                    Assert.Equal((nint)index, vector.GetElement(index));
                }
            }
            finally
            {
                NativeMemory.AlignedFree(value);
            }
        }

        [Fact]
        public unsafe void Vector64NUIntLoadAlignedNonTemporalTest()
        {
            nuint* value = null;

            try
            {
                value = (nuint*)NativeMemory.AlignedAlloc(byteCount: 8, alignment: 8);

                if (Environment.Is64BitProcess)
                {
                    value[0] = 0;
                }
                else
                {
                    value[0] = 0;
                    value[1] = 1;
                }

                Vector64<nuint> vector = Vector64.LoadAlignedNonTemporal(value);

                for (int index = 0; index < Vector64<nuint>.Count; index++)
                {
                    Assert.Equal((nuint)index, vector.GetElement(index));
                }
            }
            finally
            {
                NativeMemory.AlignedFree(value);
            }
        }

        [Fact]
        public unsafe void Vector64SByteLoadAlignedNonTemporalTest()
        {
            sbyte* value = null;

            try
            {
                value = (sbyte*)NativeMemory.AlignedAlloc(byteCount: 8, alignment: 8);

                value[0] = 0;
                value[1] = 1;
                value[2] = 2;
                value[3] = 3;
                value[4] = 4;
                value[5] = 5;
                value[6] = 6;
                value[7] = 7;

                Vector64<sbyte> vector = Vector64.LoadAlignedNonTemporal(value);

                for (int index = 0; index < Vector64<sbyte>.Count; index++)
                {
                    Assert.Equal((sbyte)index, vector.GetElement(index));
                }
            }
            finally
            {
                NativeMemory.AlignedFree(value);
            }
        }

        [Fact]
        public unsafe void Vector64SingleLoadAlignedNonTemporalTest()
        {
            float* value = null;

            try
            {
                value = (float*)NativeMemory.AlignedAlloc(byteCount: 8, alignment: 8);

                value[0] = 0;
                value[1] = 1;

                Vector64<float> vector = Vector64.LoadAlignedNonTemporal(value);

                for (int index = 0; index < Vector64<float>.Count; index++)
                {
                    Assert.Equal((float)index, vector.GetElement(index));
                }
            }
            finally
            {
                NativeMemory.AlignedFree(value);
            }
        }

        [Fact]
        public unsafe void Vector64UInt16LoadAlignedNonTemporalTest()
        {
            ushort* value = null;

            try
            {
                value = (ushort*)NativeMemory.AlignedAlloc(byteCount: 8, alignment: 8);

                value[0] = 0;
                value[1] = 1;
                value[2] = 2;
                value[3] = 3;

                Vector64<ushort> vector = Vector64.LoadAlignedNonTemporal(value);

                for (int index = 0; index < Vector64<ushort>.Count; index++)
                {
                    Assert.Equal((ushort)index, vector.GetElement(index));
                }
            }
            finally
            {
                NativeMemory.AlignedFree(value);
            }
        }

        [Fact]
        public unsafe void Vector64UInt32LoadAlignedNonTemporalTest()
        {
            uint* value = null;

            try
            {
                value = (uint*)NativeMemory.AlignedAlloc(byteCount: 8, alignment: 8);

                value[0] = 0;
                value[1] = 1;

                Vector64<uint> vector = Vector64.LoadAlignedNonTemporal(value);

                for (int index = 0; index < Vector64<uint>.Count; index++)
                {
                    Assert.Equal((uint)index, vector.GetElement(index));
                }
            }
            finally
            {
                NativeMemory.AlignedFree(value);
            }
        }

        [Fact]
        public unsafe void Vector64UInt64LoadAlignedNonTemporalTest()
        {
            ulong* value = null;

            try
            {
                value = (ulong*)NativeMemory.AlignedAlloc(byteCount: 8, alignment: 8);

                value[0] = 0;

                Vector64<ulong> vector = Vector64.LoadAlignedNonTemporal(value);

                for (int index = 0; index < Vector64<ulong>.Count; index++)
                {
                    Assert.Equal((ulong)index, vector.GetElement(index));
                }
            }
            finally
            {
                NativeMemory.AlignedFree(value);
            }
        }

        [Fact]
        public unsafe void Vector64ByteLoadUnsafeTest()
        {
            byte* value = stackalloc byte[8] {
                0,
                1,
                2,
                3,
                4,
                5,
                6,
                7,
            };

            Vector64<byte> vector = Vector64.LoadUnsafe(ref value[0]);

            for (int index = 0; index < Vector64<byte>.Count; index++)
            {
                Assert.Equal((byte)index, vector.GetElement(index));
            }
        }

        [Fact]
        public unsafe void Vector64DoubleLoadUnsafeTest()
        {
            double* value = stackalloc double[1] {
                0,
            };

            Vector64<double> vector = Vector64.LoadUnsafe(ref value[0]);

            for (int index = 0; index < Vector64<double>.Count; index++)
            {
                Assert.Equal((double)index, vector.GetElement(index));
            }
        }

        [Fact]
        public unsafe void Vector64Int16LoadUnsafeTest()
        {
            short* value = stackalloc short[4] {
                0,
                1,
                2,
                3,
            };

            Vector64<short> vector = Vector64.LoadUnsafe(ref value[0]);

            for (int index = 0; index < Vector64<short>.Count; index++)
            {
                Assert.Equal((short)index, vector.GetElement(index));
            }
        }

        [Fact]
        public unsafe void Vector64Int32LoadUnsafeTest()
        {
            int* value = stackalloc int[2] {
                0,
                1,
            };

            Vector64<int> vector = Vector64.LoadUnsafe(ref value[0]);

            for (int index = 0; index < Vector64<int>.Count; index++)
            {
                Assert.Equal((int)index, vector.GetElement(index));
            }
        }

        [Fact]
        public unsafe void Vector64Int64LoadUnsafeTest()
        {
            long* value = stackalloc long[1] {
                0,
            };

            Vector64<long> vector = Vector64.LoadUnsafe(ref value[0]);

            for (int index = 0; index < Vector64<long>.Count; index++)
            {
                Assert.Equal((long)index, vector.GetElement(index));
            }
        }

        [Fact]
        public unsafe void Vector64NIntLoadUnsafeTest()
        {
            if (Environment.Is64BitProcess)
            {
                nint* value = stackalloc nint[1] {
                    0,
                };

                Vector64<nint> vector = Vector64.LoadUnsafe(ref value[0]);

                for (int index = 0; index < Vector64<nint>.Count; index++)
                {
                    Assert.Equal((nint)index, vector.GetElement(index));
                }
            }
            else
            {
                nint* value = stackalloc nint[2] {
                    0,
                    1,
                };

                Vector64<nint> vector = Vector64.LoadUnsafe(ref value[0]);

                for (int index = 0; index < Vector64<nint>.Count; index++)
                {
                    Assert.Equal((nint)index, vector.GetElement(index));
                }
            }
        }

        [Fact]
        public unsafe void Vector64NUIntLoadUnsafeTest()
        {
            if (Environment.Is64BitProcess)
            {
                nuint* value = stackalloc nuint[1] {
                    0,
                };

                Vector64<nuint> vector = Vector64.LoadUnsafe(ref value[0]);

                for (int index = 0; index < Vector64<nuint>.Count; index++)
                {
                    Assert.Equal((nuint)index, vector.GetElement(index));
                }
            }
            else
            {
                nuint* value = stackalloc nuint[2] {
                    0,
                    1,
                };

                Vector64<nuint> vector = Vector64.LoadUnsafe(ref value[0]);

                for (int index = 0; index < Vector64<nuint>.Count; index++)
                {
                    Assert.Equal((nuint)index, vector.GetElement(index));
                }
            }
        }

        [Fact]
        public unsafe void Vector64SByteLoadUnsafeTest()
        {
            sbyte* value = stackalloc sbyte[8] {
                0,
                1,
                2,
                3,
                4,
                5,
                6,
                7,
            };

            Vector64<sbyte> vector = Vector64.LoadUnsafe(ref value[0]);

            for (int index = 0; index < Vector64<sbyte>.Count; index++)
            {
                Assert.Equal((sbyte)index, vector.GetElement(index));
            }
        }

        [Fact]
        public unsafe void Vector64SingleLoadUnsafeTest()
        {
            float* value = stackalloc float[2] {
                0,
                1,
            };

            Vector64<float> vector = Vector64.LoadUnsafe(ref value[0]);

            for (int index = 0; index < Vector64<float>.Count; index++)
            {
                Assert.Equal((float)index, vector.GetElement(index));
            }
        }

        [Fact]
        public unsafe void Vector64UInt16LoadUnsafeTest()
        {
            ushort* value = stackalloc ushort[4] {
                0,
                1,
                2,
                3,
            };

            Vector64<ushort> vector = Vector64.LoadUnsafe(ref value[0]);

            for (int index = 0; index < Vector64<ushort>.Count; index++)
            {
                Assert.Equal((ushort)index, vector.GetElement(index));
            }
        }

        [Fact]
        public unsafe void Vector64UInt32LoadUnsafeTest()
        {
            uint* value = stackalloc uint[2] {
                0,
                1,
            };

            Vector64<uint> vector = Vector64.LoadUnsafe(ref value[0]);

            for (int index = 0; index < Vector64<uint>.Count; index++)
            {
                Assert.Equal((uint)index, vector.GetElement(index));
            }
        }

        [Fact]
        public unsafe void Vector64UInt64LoadUnsafeTest()
        {
            ulong* value = stackalloc ulong[1] {
                0,
            };

            Vector64<ulong> vector = Vector64.LoadUnsafe(ref value[0]);

            for (int index = 0; index < Vector64<ulong>.Count; index++)
            {
                Assert.Equal((ulong)index, vector.GetElement(index));
            }
        }

        [Fact]
        public unsafe void Vector64ByteLoadUnsafeIndexTest()
        {
            byte* value = stackalloc byte[8 + 1] {
                0,
                1,
                2,
                3,
                4,
                5,
                6,
                7,
                8,
            };

            Vector64<byte> vector = Vector64.LoadUnsafe(ref value[0], 1);

            for (int index = 0; index < Vector64<byte>.Count; index++)
            {
                Assert.Equal((byte)(index + 1), vector.GetElement(index));
            }
        }

        [Fact]
        public unsafe void Vector64DoubleLoadUnsafeIndexTest()
        {
            double* value = stackalloc double[1 + 1] {
                0,
                1,
            };

            Vector64<double> vector = Vector64.LoadUnsafe(ref value[0], 1);

            for (int index = 0; index < Vector64<double>.Count; index++)
            {
                Assert.Equal((double)(index + 1), vector.GetElement(index));
            }
        }

        [Fact]
        public unsafe void Vector64Int16LoadUnsafeIndexTest()
        {
            short* value = stackalloc short[4 + 1] {
                0,
                1,
                2,
                3,
                4,
            };

            Vector64<short> vector = Vector64.LoadUnsafe(ref value[0], 1);

            for (int index = 0; index < Vector64<short>.Count; index++)
            {
                Assert.Equal((short)(index + 1), vector.GetElement(index));
            }
        }

        [Fact]
        public unsafe void Vector64Int32LoadUnsafeIndexTest()
        {
            int* value = stackalloc int[2 + 1] {
                0,
                1,
                2,
            };

            Vector64<int> vector = Vector64.LoadUnsafe(ref value[0], 1);

            for (int index = 0; index < Vector64<int>.Count; index++)
            {
                Assert.Equal((int)(index + 1), vector.GetElement(index));
            }
        }

        [Fact]
        public unsafe void Vector64Int64LoadUnsafeIndexTest()
        {
            long* value = stackalloc long[1 + 1] {
                0,
                1,
            };

            Vector64<long> vector = Vector64.LoadUnsafe(ref value[0], 1);

            for (int index = 0; index < Vector64<long>.Count; index++)
            {
                Assert.Equal((long)(index + 1), vector.GetElement(index));
            }
        }

        [Fact]
        public unsafe void Vector64NIntLoadUnsafeIndexTest()
        {
            if (Environment.Is64BitProcess)
            {
                nint* value = stackalloc nint[1 + 1] {
                    0,
                    1,
                };

                Vector64<nint> vector = Vector64.LoadUnsafe(ref value[0], 1);

                for (int index = 0; index < Vector64<nint>.Count; index++)
                {
                    Assert.Equal((nint)(index + 1), vector.GetElement(index));
                }
            }
            else
            {
                nint* value = stackalloc nint[2 + 1] {
                    0,
                    1,
                    2,
                };

                Vector64<nint> vector = Vector64.LoadUnsafe(ref value[0], 1);

                for (int index = 0; index < Vector64<nint>.Count; index++)
                {
                    Assert.Equal((nint)(index + 1), vector.GetElement(index));
                }
            }
        }

        [Fact]
        public unsafe void Vector64NUIntLoadUnsafeIndexTest()
        {
            if (Environment.Is64BitProcess)
            {
                nuint* value = stackalloc nuint[1 + 1] {
                    0,
                    1,
                };

                Vector64<nuint> vector = Vector64.LoadUnsafe(ref value[0], 1);

                for (int index = 0; index < Vector64<nuint>.Count; index++)
                {
                    Assert.Equal((nuint)(index + 1), vector.GetElement(index));
                }
            }
            else
            {
                nuint* value = stackalloc nuint[2 + 1] {
                    0,
                    1,
                    2,
                };

                Vector64<nuint> vector = Vector64.LoadUnsafe(ref value[0], 1);

                for (int index = 0; index < Vector64<nuint>.Count; index++)
                {
                    Assert.Equal((nuint)(index + 1), vector.GetElement(index));
                }
            }
        }

        [Fact]
        public unsafe void Vector64SByteLoadUnsafeIndexTest()
        {
            sbyte* value = stackalloc sbyte[8 + 1] {
                0,
                1,
                2,
                3,
                4,
                5,
                6,
                7,
                8,
            };

            Vector64<sbyte> vector = Vector64.LoadUnsafe(ref value[0], 1);

            for (int index = 0; index < Vector64<sbyte>.Count; index++)
            {
                Assert.Equal((sbyte)(index + 1), vector.GetElement(index));
            }
        }

        [Fact]
        public unsafe void Vector64SingleLoadUnsafeIndexTest()
        {
            float* value = stackalloc float[2 + 1] {
                0,
                1,
                2,
            };

            Vector64<float> vector = Vector64.LoadUnsafe(ref value[0], 1);

            for (int index = 0; index < Vector64<float>.Count; index++)
            {
                Assert.Equal((float)(index + 1), vector.GetElement(index));
            }
        }

        [Fact]
        public unsafe void Vector64UInt16LoadUnsafeIndexTest()
        {
            ushort* value = stackalloc ushort[4 + 1] {
                0,
                1,
                2,
                3,
                4,
            };

            Vector64<ushort> vector = Vector64.LoadUnsafe(ref value[0], 1);

            for (int index = 0; index < Vector64<ushort>.Count; index++)
            {
                Assert.Equal((ushort)(index + 1), vector.GetElement(index));
            }
        }

        [Fact]
        public unsafe void Vector64UInt32LoadUnsafeIndexTest()
        {
            uint* value = stackalloc uint[2 + 1] {
                0,
                1,
                2,
            };

            Vector64<uint> vector = Vector64.LoadUnsafe(ref value[0], 1);

            for (int index = 0; index < Vector64<uint>.Count; index++)
            {
                Assert.Equal((uint)(index + 1), vector.GetElement(index));
            }
        }

        [Fact]
        public unsafe void Vector64UInt64LoadUnsafeIndexTest()
        {
            ulong* value = stackalloc ulong[1 + 1] {
                0,
                1,
            };

            Vector64<ulong> vector = Vector64.LoadUnsafe(ref value[0], 1);

            for (int index = 0; index < Vector64<ulong>.Count; index++)
            {
                Assert.Equal((ulong)(index + 1), vector.GetElement(index));
            }
        }

        [Fact]
        public void Vector64ByteShiftLeftTest()
        {
            Vector64<byte> vector = Vector64.Create((byte)0x01);
            vector = Vector64.ShiftLeft(vector, 4);

            for (int index = 0; index < Vector64<byte>.Count; index++)
            {
                Assert.Equal((byte)0x10, vector.GetElement(index));
            }
        }

        [Fact]
        public void Vector64Int16ShiftLeftTest()
        {
            Vector64<short> vector = Vector64.Create((short)0x01);
            vector = Vector64.ShiftLeft(vector, 4);

            for (int index = 0; index < Vector64<short>.Count; index++)
            {
                Assert.Equal((short)0x10, vector.GetElement(index));
            }
        }

        [Fact]
        public void Vector64Int32ShiftLeftTest()
        {
            Vector64<int> vector = Vector64.Create((int)0x01);
            vector = Vector64.ShiftLeft(vector, 4);

            for (int index = 0; index < Vector64<int>.Count; index++)
            {
                Assert.Equal((int)0x10, vector.GetElement(index));
            }
        }

        [Fact]
        public void Vector64Int64ShiftLeftTest()
        {
            Vector64<long> vector = Vector64.Create((long)0x01);
            vector = Vector64.ShiftLeft(vector, 4);

            for (int index = 0; index < Vector64<long>.Count; index++)
            {
                Assert.Equal((long)0x10, vector.GetElement(index));
            }
        }

        [Fact]
        public void Vector64NIntShiftLeftTest()
        {
            Vector64<nint> vector = Vector64.Create((nint)0x01);
            vector = Vector64.ShiftLeft(vector, 4);

            for (int index = 0; index < Vector64<nint>.Count; index++)
            {
                Assert.Equal((nint)0x10, vector.GetElement(index));
            }
        }

        [Fact]
        public void Vector64NUIntShiftLeftTest()
        {
            Vector64<nuint> vector = Vector64.Create((nuint)0x01);
            vector = Vector64.ShiftLeft(vector, 4);

            for (int index = 0; index < Vector64<nuint>.Count; index++)
            {
                Assert.Equal((nuint)0x10, vector.GetElement(index));
            }
        }

        [Fact]
        public void Vector64SByteShiftLeftTest()
        {
            Vector64<sbyte> vector = Vector64.Create((sbyte)0x01);
            vector = Vector64.ShiftLeft(vector, 4);

            for (int index = 0; index < Vector64<sbyte>.Count; index++)
            {
                Assert.Equal((sbyte)0x10, vector.GetElement(index));
            }
        }

        [Fact]
        public void Vector64UInt16ShiftLeftTest()
        {
            Vector64<ushort> vector = Vector64.Create((ushort)0x01);
            vector = Vector64.ShiftLeft(vector, 4);

            for (int index = 0; index < Vector64<ushort>.Count; index++)
            {
                Assert.Equal((ushort)0x10, vector.GetElement(index));
            }
        }

        [Fact]
        public void Vector64UInt32ShiftLeftTest()
        {
            Vector64<uint> vector = Vector64.Create((uint)0x01);
            vector = Vector64.ShiftLeft(vector, 4);

            for (int index = 0; index < Vector64<uint>.Count; index++)
            {
                Assert.Equal((uint)0x10, vector.GetElement(index));
            }
        }

        [Fact]
        public void Vector64UInt64ShiftLeftTest()
        {
            Vector64<ulong> vector = Vector64.Create((ulong)0x01);
            vector = Vector64.ShiftLeft(vector, 4);

            for (int index = 0; index < Vector64<ulong>.Count; index++)
            {
                Assert.Equal((ulong)0x10, vector.GetElement(index));
            }
        }

        [Fact]
        public void Vector64Int16ShiftRightArithmeticTest()
        {
            Vector64<short> vector = Vector64.Create(unchecked((short)0x8000));
            vector = Vector64.ShiftRightArithmetic(vector, 4);

            for (int index = 0; index < Vector64<short>.Count; index++)
            {
                Assert.Equal(unchecked((short)0xF800), vector.GetElement(index));
            }
        }

        [Fact]
        public void Vector64Int32ShiftRightArithmeticTest()
        {
            Vector64<int> vector = Vector64.Create(unchecked((int)0x80000000));
            vector = Vector64.ShiftRightArithmetic(vector, 4);

            for (int index = 0; index < Vector64<int>.Count; index++)
            {
                Assert.Equal(unchecked((int)0xF8000000), vector.GetElement(index));
            }
        }

        [Fact]
        public void Vector64Int64ShiftRightArithmeticTest()
        {
            Vector64<long> vector = Vector64.Create(unchecked((long)0x8000000000000000));
            vector = Vector64.ShiftRightArithmetic(vector, 4);

            for (int index = 0; index < Vector64<long>.Count; index++)
            {
                Assert.Equal(unchecked((long)0xF800000000000000), vector.GetElement(index));
            }
        }

        [Fact]
        public void Vector64NIntShiftRightArithmeticTest()
        {
            if (Environment.Is64BitProcess)
            {
                Vector64<nint> vector = Vector64.Create(unchecked((nint)0x8000000000000000));
                vector = Vector64.ShiftRightArithmetic(vector, 4);

                for (int index = 0; index < Vector64<nint>.Count; index++)
                {
                    Assert.Equal(unchecked((nint)0xF800000000000000), vector.GetElement(index));
                }
            }
            else
            {
                Vector64<nint> vector = Vector64.Create(unchecked((nint)0x80000000));
                vector = Vector64.ShiftRightArithmetic(vector, 4);

                for (int index = 0; index < Vector64<nint>.Count; index++)
                {
                    Assert.Equal(unchecked((nint)0xF8000000), vector.GetElement(index));
                }
            }
        }

        [Fact]
        public void Vector64SByteShiftRightArithmeticTest()
        {
            Vector64<sbyte> vector = Vector64.Create(unchecked((sbyte)0x80));
            vector = Vector64.ShiftRightArithmetic(vector, 4);

            for (int index = 0; index < Vector64<sbyte>.Count; index++)
            {
                Assert.Equal(unchecked((sbyte)0xF8), vector.GetElement(index));
            }
        }

        [Fact]
        public void Vector64ByteShiftRightLogicalTest()
        {
            Vector64<byte> vector = Vector64.Create((byte)0x80);
            vector = Vector64.ShiftRightLogical(vector, 4);

            for (int index = 0; index < Vector64<byte>.Count; index++)
            {
                Assert.Equal((byte)0x08, vector.GetElement(index));
            }
        }

        [Fact]
        public void Vector64Int16ShiftRightLogicalTest()
        {
            Vector64<short> vector = Vector64.Create(unchecked((short)0x8000));
            vector = Vector64.ShiftRightLogical(vector, 4);

            for (int index = 0; index < Vector64<short>.Count; index++)
            {
                Assert.Equal((short)0x0800, vector.GetElement(index));
            }
        }

        [Fact]
        public void Vector64Int32ShiftRightLogicalTest()
        {
            Vector64<int> vector = Vector64.Create(unchecked((int)0x80000000));
            vector = Vector64.ShiftRightLogical(vector, 4);

            for (int index = 0; index < Vector64<int>.Count; index++)
            {
                Assert.Equal((int)0x08000000, vector.GetElement(index));
            }
        }

        [Fact]
        public void Vector64Int64ShiftRightLogicalTest()
        {
            Vector64<long> vector = Vector64.Create(unchecked((long)0x8000000000000000));
            vector = Vector64.ShiftRightLogical(vector, 4);

            for (int index = 0; index < Vector64<long>.Count; index++)
            {
                Assert.Equal((long)0x0800000000000000, vector.GetElement(index));
            }
        }

        [Fact]
        public void Vector64NIntShiftRightLogicalTest()
        {
            if (Environment.Is64BitProcess)
            {
                Vector64<nint> vector = Vector64.Create(unchecked((nint)0x8000000000000000));
                vector = Vector64.ShiftRightLogical(vector, 4);

                for (int index = 0; index < Vector64<nint>.Count; index++)
                {
                    Assert.Equal(unchecked((nint)0x0800000000000000), vector.GetElement(index));
                }
            }
            else
            {
                Vector64<nint> vector = Vector64.Create(unchecked((nint)0x80000000));
                vector = Vector64.ShiftRightLogical(vector, 4);

                for (int index = 0; index < Vector64<nint>.Count; index++)
                {
                    Assert.Equal(unchecked((nint)0x08000000), vector.GetElement(index));
                }
            }
        }

        [Fact]
        public void Vector64NUIntShiftRightLogicalTest()
        {
            if (Environment.Is64BitProcess)
            {
                Vector64<nuint> vector = Vector64.Create(unchecked((nuint)0x8000000000000000));
                vector = Vector64.ShiftRightLogical(vector, 4);

                for (int index = 0; index < Vector64<nuint>.Count; index++)
                {
                    Assert.Equal(unchecked((nuint)0x0800000000000000), vector.GetElement(index));
                }
            }
            else
            {
                Vector64<nuint> vector = Vector64.Create(unchecked((nuint)0x80000000));
                vector = Vector64.ShiftRightLogical(vector, 4);

                for (int index = 0; index < Vector64<nuint>.Count; index++)
                {
                    Assert.Equal(unchecked((nuint)0x08000000), vector.GetElement(index));
                }
            }
        }

        [Fact]
        public void Vector64SByteShiftRightLogicalTest()
        {
            Vector64<sbyte> vector = Vector64.Create(unchecked((sbyte)0x80));
            vector = Vector64.ShiftRightLogical(vector, 4);

            for (int index = 0; index < Vector64<sbyte>.Count; index++)
            {
                Assert.Equal((sbyte)0x08, vector.GetElement(index));
            }
        }

        [Fact]
        public void Vector64UInt16ShiftRightLogicalTest()
        {
            Vector64<ushort> vector = Vector64.Create(unchecked((ushort)0x8000));
            vector = Vector64.ShiftRightLogical(vector, 4);

            for (int index = 0; index < Vector64<ushort>.Count; index++)
            {
                Assert.Equal((ushort)0x0800, vector.GetElement(index));
            }
        }

        [Fact]
        public void Vector64UInt32ShiftRightLogicalTest()
        {
            Vector64<uint> vector = Vector64.Create(0x80000000);
            vector = Vector64.ShiftRightLogical(vector, 4);

            for (int index = 0; index < Vector64<uint>.Count; index++)
            {
                Assert.Equal((uint)0x08000000, vector.GetElement(index));
            }
        }

        [Fact]
        public void Vector64UInt64ShiftRightLogicalTest()
        {
            Vector64<ulong> vector = Vector64.Create(0x8000000000000000);
            vector = Vector64.ShiftRightLogical(vector, 4);

            for (int index = 0; index < Vector64<ulong>.Count; index++)
            {
                Assert.Equal((ulong)0x0800000000000000, vector.GetElement(index));
            }
        }

        [Fact]
        public void Vector64ByteShuffleOneInputTest()
        {
            Vector64<byte> vector = Vector64.Create((byte)1, 2, 3, 4, 5, 6, 7, 8);
            Vector64<byte> result = Vector64.Shuffle(vector, Vector64.Create((byte)7, 6, 5, 4, 3, 2, 1, 0));

            for (int index = 0; index < Vector64<byte>.Count; index++)
            {
                Assert.Equal((byte)(Vector64<byte>.Count - index), result.GetElement(index));
            }
        }

        [Fact]
        public void Vector64Int16ShuffleOneInputTest()
        {
            Vector64<short> vector = Vector64.Create((short)1, 2, 3, 4);
            Vector64<short> result = Vector64.Shuffle(vector, Vector64.Create((short)3, 2, 1, 0));

            for (int index = 0; index < Vector64<short>.Count; index++)
            {
                Assert.Equal((short)(Vector64<short>.Count - index), result.GetElement(index));
            }
        }

        [Fact]
        public void Vector64Int32ShuffleOneInputTest()
        {
            Vector64<int> vector = Vector64.Create((int)1, 2);
            Vector64<int> result = Vector64.Shuffle(vector, Vector64.Create((int)1, 0));

            for (int index = 0; index < Vector64<int>.Count; index++)
            {
                Assert.Equal((int)(Vector64<int>.Count - index), result.GetElement(index));
            }
        }

        [Fact]
        public void Vector64SByteShuffleOneInputTest()
        {
            Vector64<sbyte> vector = Vector64.Create((sbyte)1, 2, 3, 4, 5, 6, 7, 8);
            Vector64<sbyte> result = Vector64.Shuffle(vector, Vector64.Create((sbyte)7, 6, 5, 4, 3, 2, 1, 0));

            for (int index = 0; index < Vector64<sbyte>.Count; index++)
            {
                Assert.Equal((sbyte)(Vector64<sbyte>.Count - index), result.GetElement(index));
            }
        }

        [Fact]
        public void Vector64SingleShuffleOneInputTest()
        {
            Vector64<float> vector = Vector64.Create((float)1, 2);
            Vector64<float> result = Vector64.Shuffle(vector, Vector64.Create((int)1, 0));

            for (int index = 0; index < Vector64<float>.Count; index++)
            {
                Assert.Equal((float)(Vector64<float>.Count - index), result.GetElement(index));
            }
        }

        [Fact]
        public void Vector64UInt16ShuffleOneInputTest()
        {
            Vector64<ushort> vector = Vector64.Create((ushort)1, 2, 3, 4);
            Vector64<ushort> result = Vector64.Shuffle(vector, Vector64.Create((ushort)3, 2, 1, 0));

            for (int index = 0; index < Vector64<ushort>.Count; index++)
            {
                Assert.Equal((ushort)(Vector64<ushort>.Count - index), result.GetElement(index));
            }
        }

        [Fact]
        public void Vector64UInt32ShuffleOneInputTest()
        {
            Vector64<uint> vector = Vector64.Create((uint)1, 2);
            Vector64<uint> result = Vector64.Shuffle(vector, Vector64.Create((uint)1, 0));

            for (int index = 0; index < Vector64<uint>.Count; index++)
            {
                Assert.Equal((uint)(Vector64<uint>.Count - index), result.GetElement(index));
            }
        }

        [Fact]
        public void Vector64ByteShuffleOneInputWithDirectVectorTest()
        {
            Vector64<byte> result = Vector64.Shuffle(Vector64.Create((byte)1, 2, 3, 4, 5, 6, 7, 8), Vector64.Create((byte)7, 6, 5, 4, 3, 2, 1, 0));

            for (int index = 0; index < Vector64<byte>.Count; index++)
            {
                Assert.Equal((byte)(Vector64<byte>.Count - index), result.GetElement(index));
            }
        }

        [Fact]
        public void Vector64Int16ShuffleOneInputWithDirectVectorTest()
        {
            Vector64<short> result = Vector64.Shuffle(Vector64.Create((short)1, 2, 3, 4), Vector64.Create((short)3, 2, 1, 0));

            for (int index = 0; index < Vector64<short>.Count; index++)
            {
                Assert.Equal((short)(Vector64<short>.Count - index), result.GetElement(index));
            }
        }

        [Fact]
        public void Vector64Int32ShuffleOneInputWithDirectVectorTest()
        {
            Vector64<int> result = Vector64.Shuffle(Vector64.Create((int)1, 2), Vector64.Create((int)1, 0));

            for (int index = 0; index < Vector64<int>.Count; index++)
            {
                Assert.Equal((int)(Vector64<int>.Count - index), result.GetElement(index));
            }
        }

        [Fact]
        public void Vector64SByteShuffleOneInputWithDirectVectorTest()
        {
            Vector64<sbyte> result = Vector64.Shuffle(Vector64.Create((sbyte)1, 2, 3, 4, 5, 6, 7, 8), Vector64.Create((sbyte)7, 6, 5, 4, 3, 2, 1, 0));

            for (int index = 0; index < Vector64<sbyte>.Count; index++)
            {
                Assert.Equal((sbyte)(Vector64<sbyte>.Count - index), result.GetElement(index));
            }
        }

        [Fact]
        public void Vector64SingleShuffleOneInputWithDirectVectorTest()
        {
            Vector64<float> result = Vector64.Shuffle(Vector64.Create((float)1, 2), Vector64.Create((int)1, 0));

            for (int index = 0; index < Vector64<float>.Count; index++)
            {
                Assert.Equal((float)(Vector64<float>.Count - index), result.GetElement(index));
            }
        }

        [Fact]
        public void Vector64UInt16ShuffleOneInputWithDirectVectorTest()
        {
            Vector64<ushort> result = Vector64.Shuffle(Vector64.Create((ushort)1, 2, 3, 4), Vector64.Create((ushort)3, 2, 1, 0));

            for (int index = 0; index < Vector64<ushort>.Count; index++)
            {
                Assert.Equal((ushort)(Vector64<ushort>.Count - index), result.GetElement(index));
            }
        }

        [Fact]
        public void Vector64UInt32ShuffleOneInputWithDirectVectorTest()
        {
            Vector64<uint> result = Vector64.Shuffle(Vector64.Create((uint)1, 2), Vector64.Create((uint)1, 0));

            for (int index = 0; index < Vector64<uint>.Count; index++)
            {
                Assert.Equal((uint)(Vector64<uint>.Count - index), result.GetElement(index));
            }
        }

        [Fact]
        public void Vector64ByteShuffleOneInputWithLocalIndicesTest()
        {
            Vector64<byte> vector = Vector64.Create((byte)1, 2, 3, 4, 5, 6, 7, 8);
            Vector64<byte> indices = Vector64.Create((byte)7, 6, 5, 4, 3, 2, 1, 0);
            Vector64<byte> result = Vector64.Shuffle(vector, indices);

            for (int index = 0; index < Vector64<byte>.Count; index++)
            {
                Assert.Equal((byte)(Vector64<byte>.Count - index), result.GetElement(index));
            }
        }

        [Fact]
        public void Vector64Int16ShuffleOneInputWithLocalIndicesTest()
        {
            Vector64<short> vector = Vector64.Create((short)1, 2, 3, 4);
            Vector64<short> indices = Vector64.Create((short)3, 2, 1, 0);
            Vector64<short> result = Vector64.Shuffle(vector, indices);

            for (int index = 0; index < Vector64<short>.Count; index++)
            {
                Assert.Equal((short)(Vector64<short>.Count - index), result.GetElement(index));
            }
        }

        [Fact]
        public void Vector64Int32ShuffleOneInputWithLocalIndicesTest()
        {
            Vector64<int> vector = Vector64.Create((int)1, 2);
            Vector64<int> indices = Vector64.Create((int)1, 0);
            Vector64<int> result = Vector64.Shuffle(vector, indices);

            for (int index = 0; index < Vector64<int>.Count; index++)
            {
                Assert.Equal((int)(Vector64<int>.Count - index), result.GetElement(index));
            }
        }

        [Fact]
        public void Vector64SByteShuffleOneInputWithLocalIndicesTest()
        {
            Vector64<sbyte> vector = Vector64.Create((sbyte)1, 2, 3, 4, 5, 6, 7, 8);
            Vector64<sbyte> indices = Vector64.Create((sbyte)7, 6, 5, 4, 3, 2, 1, 0);
            Vector64<sbyte> result = Vector64.Shuffle(vector, indices);

            for (int index = 0; index < Vector64<sbyte>.Count; index++)
            {
                Assert.Equal((sbyte)(Vector64<sbyte>.Count - index), result.GetElement(index));
            }
        }

        [Fact]
        public void Vector64SingleShuffleOneInputWithLocalIndicesTest()
        {
            Vector64<float> vector = Vector64.Create((float)1, 2);
            Vector64<int> indices = Vector64.Create((int)1, 0);
            Vector64<float> result = Vector64.Shuffle(vector, indices);

            for (int index = 0; index < Vector64<float>.Count; index++)
            {
                Assert.Equal((float)(Vector64<float>.Count - index), result.GetElement(index));
            }
        }

        [Fact]
        public void Vector64UInt16ShuffleOneInputWithLocalIndicesTest()
        {
            Vector64<ushort> vector = Vector64.Create((ushort)1, 2, 3, 4);
            Vector64<ushort> indices = Vector64.Create((ushort)3, 2, 1, 0);
            Vector64<ushort> result = Vector64.Shuffle(vector, indices);

            for (int index = 0; index < Vector64<ushort>.Count; index++)
            {
                Assert.Equal((ushort)(Vector64<ushort>.Count - index), result.GetElement(index));
            }
        }

        [Fact]
        public void Vector64UInt32ShuffleOneInputWithLocalIndicesTest()
        {
            Vector64<uint> vector = Vector64.Create((uint)1, 2);
            Vector64<uint> indices = Vector64.Create((uint)1, 0);
            Vector64<uint> result = Vector64.Shuffle(vector, indices);

            for (int index = 0; index < Vector64<uint>.Count; index++)
            {
                Assert.Equal((uint)(Vector64<uint>.Count - index), result.GetElement(index));
            }
        }

        [Fact]
        public void Vector64ByteShuffleOneInputWithAllBitsSetIndicesTest()
        {
            Vector64<byte> vector = Vector64.Create((byte)1, 2, 3, 4, 5, 6, 7, 8);
            Vector64<byte> result = Vector64.Shuffle(vector, Vector64<byte>.AllBitsSet);

            for (int index = 0; index < Vector64<byte>.Count; index++)
            {
                Assert.Equal((byte)0, result.GetElement(index));
            }
        }

        [Fact]
        public void Vector64Int16ShuffleOneInputWithAllBitsSetIndicesTest()
        {
            Vector64<short> vector = Vector64.Create((short)1, 2, 3, 4);
            Vector64<short> result = Vector64.Shuffle(vector, Vector64<short>.AllBitsSet);

            for (int index = 0; index < Vector64<short>.Count; index++)
            {
                Assert.Equal(0, result.GetElement(index));
            }
        }

        [Fact]
        public void Vector64Int32ShuffleOneInputWithAllBitsSetIndicesTest()
        {
            Vector64<int> vector = Vector64.Create((int)1, 2);
            Vector64<int> result = Vector64.Shuffle(vector, Vector64<int>.AllBitsSet);

            for (int index = 0; index < Vector64<int>.Count; index++)
            {
                Assert.Equal((int)0, result.GetElement(index));
            }
        }

        [Fact]
        public void Vector64SByteShuffleOneInputWithAllBitsSetIndicesTest()
        {
            Vector64<sbyte> vector = Vector64.Create((sbyte)1, 2, 3, 4, 5, 6, 7, 8);
            Vector64<sbyte> result = Vector64.Shuffle(vector, Vector64<sbyte>.AllBitsSet);

            for (int index = 0; index < Vector64<sbyte>.Count; index++)
            {
                Assert.Equal((sbyte)0, result.GetElement(index));
            }
        }

        [Fact]
        public void Vector64SingleShuffleOneInputWithAllBitsSetIndicesTest()
        {
            Vector64<float> vector = Vector64.Create((float)1, 2);
            Vector64<float> result = Vector64.Shuffle(vector, Vector64<int>.AllBitsSet);

            for (int index = 0; index < Vector64<float>.Count; index++)
            {
                Assert.Equal((float)0, result.GetElement(index));
            }
        }

        [Fact]
        public void Vector64UInt16ShuffleOneInputWithAllBitsSetIndicesTest()
        {
            Vector64<ushort> vector = Vector64.Create((ushort)1, 2, 3, 4);
            Vector64<ushort> result = Vector64.Shuffle(vector, Vector64<ushort>.AllBitsSet);

            for (int index = 0; index < Vector64<ushort>.Count; index++)
            {
                Assert.Equal((ushort)0, result.GetElement(index));
            }
        }

        [Fact]
        public void Vector64UInt32ShuffleOneInputWithAllBitsSetIndicesTest()
        {
            Vector64<uint> vector = Vector64.Create((uint)1, 2);
            Vector64<uint> result = Vector64.Shuffle(vector, Vector64<uint>.AllBitsSet);

            for (int index = 0; index < Vector64<uint>.Count; index++)
            {
                Assert.Equal((uint)0, result.GetElement(index));
            }
        }

        [Fact]
        public void Vector64ByteShuffleOneInputWithZeroIndicesTest()
        {
            Vector64<byte> vector = Vector64.Create((byte)1, 2, 3, 4, 5, 6, 7, 8);
            Vector64<byte> result = Vector64.Shuffle(vector, Vector64<byte>.Zero);

            for (int index = 0; index < Vector64<byte>.Count; index++)
            {
                Assert.Equal((byte)1, result.GetElement(index));
            }
        }

        [Fact]
        public void Vector64Int16ShuffleOneInputWithZeroIndicesTest()
        {
            Vector64<short> vector = Vector64.Create((short)1, 2, 3, 4);
            Vector64<short> result = Vector64.Shuffle(vector, Vector64<short>.Zero);

            for (int index = 0; index < Vector64<short>.Count; index++)
            {
                Assert.Equal(1, result.GetElement(index));
            }
        }

        [Fact]
        public void Vector64Int32ShuffleOneInputWithZeroIndicesTest()
        {
            Vector64<int> vector = Vector64.Create((int)1, 2);
            Vector64<int> result = Vector64.Shuffle(vector, Vector64<int>.Zero);

            for (int index = 0; index < Vector64<int>.Count; index++)
            {
                Assert.Equal((int)1, result.GetElement(index));
            }
        }

        [Fact]
        public void Vector64SByteShuffleOneInputWithZeroIndicesTest()
        {
            Vector64<sbyte> vector = Vector64.Create((sbyte)1, 2, 3, 4, 5, 6, 7, 8);
            Vector64<sbyte> result = Vector64.Shuffle(vector, Vector64<sbyte>.Zero);

            for (int index = 0; index < Vector64<sbyte>.Count; index++)
            {
                Assert.Equal((sbyte)1, result.GetElement(index));
            }
        }

        [Fact]
        public void Vector64SingleShuffleOneInputWithZeroIndicesTest()
        {
            Vector64<float> vector = Vector64.Create((float)1, 2);
            Vector64<float> result = Vector64.Shuffle(vector, Vector64<int>.Zero);

            for (int index = 0; index < Vector64<float>.Count; index++)
            {
                Assert.Equal((float)1, result.GetElement(index));
            }
        }

        [Fact]
        public void Vector64UInt16ShuffleOneInputWithZeroIndicesTest()
        {
            Vector64<ushort> vector = Vector64.Create((ushort)1, 2, 3, 4);
            Vector64<ushort> result = Vector64.Shuffle(vector, Vector64<ushort>.Zero);

            for (int index = 0; index < Vector64<ushort>.Count; index++)
            {
                Assert.Equal((ushort)1, result.GetElement(index));
            }
        }

        [Fact]
        public void Vector64UInt32ShuffleOneInputWithZeroIndicesTest()
        {
            Vector64<uint> vector = Vector64.Create((uint)1, 2);
            Vector64<uint> result = Vector64.Shuffle(vector, Vector64<uint>.Zero);

            for (int index = 0; index < Vector64<uint>.Count; index++)
            {
                Assert.Equal((uint)1, result.GetElement(index));
            }
        }

        [Fact]
        public void Vector64ByteShuffleNativeOneInputTest()
        {
            Vector64<byte> vector = Vector64.Create((byte)1, 2, 3, 4, 5, 6, 7, 8);
            Vector64<byte> result = Vector64.ShuffleNative(vector, Vector64.Create((byte)7, 6, 5, 4, 3, 2, 1, 0));

            for (int index = 0; index < Vector64<byte>.Count; index++)
            {
                Assert.Equal((byte)(Vector64<byte>.Count - index), result.GetElement(index));
            }
        }

        [Fact]
        public void Vector64Int16ShuffleNativeOneInputTest()
        {
            Vector64<short> vector = Vector64.Create((short)1, 2, 3, 4);
            Vector64<short> result = Vector64.ShuffleNative(vector, Vector64.Create((short)3, 2, 1, 0));

            for (int index = 0; index < Vector64<short>.Count; index++)
            {
                Assert.Equal((short)(Vector64<short>.Count - index), result.GetElement(index));
            }
        }

        [Fact]
        public void Vector64Int32ShuffleNativeOneInputTest()
        {
            Vector64<int> vector = Vector64.Create((int)1, 2);
            Vector64<int> result = Vector64.ShuffleNative(vector, Vector64.Create((int)1, 0));

            for (int index = 0; index < Vector64<int>.Count; index++)
            {
                Assert.Equal((int)(Vector64<int>.Count - index), result.GetElement(index));
            }
        }

        [Fact]
        public void Vector64SByteShuffleNativeOneInputTest()
        {
            Vector64<sbyte> vector = Vector64.Create((sbyte)1, 2, 3, 4, 5, 6, 7, 8);
            Vector64<sbyte> result = Vector64.ShuffleNative(vector, Vector64.Create((sbyte)7, 6, 5, 4, 3, 2, 1, 0));

            for (int index = 0; index < Vector64<sbyte>.Count; index++)
            {
                Assert.Equal((sbyte)(Vector64<sbyte>.Count - index), result.GetElement(index));
            }
        }

        [Fact]
        public void Vector64SingleShuffleNativeOneInputTest()
        {
            Vector64<float> vector = Vector64.Create((float)1, 2);
            Vector64<float> result = Vector64.ShuffleNative(vector, Vector64.Create((int)1, 0));

            for (int index = 0; index < Vector64<float>.Count; index++)
            {
                Assert.Equal((float)(Vector64<float>.Count - index), result.GetElement(index));
            }
        }

        [Fact]
        public void Vector64UInt16ShuffleNativeOneInputTest()
        {
            Vector64<ushort> vector = Vector64.Create((ushort)1, 2, 3, 4);
            Vector64<ushort> result = Vector64.ShuffleNative(vector, Vector64.Create((ushort)3, 2, 1, 0));

            for (int index = 0; index < Vector64<ushort>.Count; index++)
            {
                Assert.Equal((ushort)(Vector64<ushort>.Count - index), result.GetElement(index));
            }
        }

        [Fact]
        public void Vector64UInt32ShuffleNativeOneInputTest()
        {
            Vector64<uint> vector = Vector64.Create((uint)1, 2);
            Vector64<uint> result = Vector64.ShuffleNative(vector, Vector64.Create((uint)1, 0));

            for (int index = 0; index < Vector64<uint>.Count; index++)
            {
                Assert.Equal((uint)(Vector64<uint>.Count - index), result.GetElement(index));
            }
        }

        [Fact]
        public void Vector64ByteShuffleNativeOneInputWithDirectVectorTest()
        {
            Vector64<byte> result = Vector64.ShuffleNative(Vector64.Create((byte)1, 2, 3, 4, 5, 6, 7, 8), Vector64.Create((byte)7, 6, 5, 4, 3, 2, 1, 0));

            for (int index = 0; index < Vector64<byte>.Count; index++)
            {
                Assert.Equal((byte)(Vector64<byte>.Count - index), result.GetElement(index));
            }
        }

        [Fact]
        public void Vector64Int16ShuffleNativeOneInputWithDirectVectorTest()
        {
            Vector64<short> result = Vector64.ShuffleNative(Vector64.Create((short)1, 2, 3, 4), Vector64.Create((short)3, 2, 1, 0));

            for (int index = 0; index < Vector64<short>.Count; index++)
            {
                Assert.Equal((short)(Vector64<short>.Count - index), result.GetElement(index));
            }
        }

        [Fact]
        public void Vector64Int32ShuffleNativeOneInputWithDirectVectorTest()
        {
            Vector64<int> result = Vector64.ShuffleNative(Vector64.Create((int)1, 2), Vector64.Create((int)1, 0));

            for (int index = 0; index < Vector64<int>.Count; index++)
            {
                Assert.Equal((int)(Vector64<int>.Count - index), result.GetElement(index));
            }
        }

        [Fact]
        public void Vector64SByteShuffleNativeOneInputWithDirectVectorTest()
        {
            Vector64<sbyte> result = Vector64.ShuffleNative(Vector64.Create((sbyte)1, 2, 3, 4, 5, 6, 7, 8), Vector64.Create((sbyte)7, 6, 5, 4, 3, 2, 1, 0));

            for (int index = 0; index < Vector64<sbyte>.Count; index++)
            {
                Assert.Equal((sbyte)(Vector64<sbyte>.Count - index), result.GetElement(index));
            }
        }

        [Fact]
        public void Vector64SingleShuffleNativeOneInputWithDirectVectorTest()
        {
            Vector64<float> result = Vector64.ShuffleNative(Vector64.Create((float)1, 2), Vector64.Create((int)1, 0));

            for (int index = 0; index < Vector64<float>.Count; index++)
            {
                Assert.Equal((float)(Vector64<float>.Count - index), result.GetElement(index));
            }
        }

        [Fact]
        public void Vector64UInt16ShuffleNativeOneInputWithDirectVectorTest()
        {
            Vector64<ushort> result = Vector64.ShuffleNative(Vector64.Create((ushort)1, 2, 3, 4), Vector64.Create((ushort)3, 2, 1, 0));

            for (int index = 0; index < Vector64<ushort>.Count; index++)
            {
                Assert.Equal((ushort)(Vector64<ushort>.Count - index), result.GetElement(index));
            }
        }

        [Fact]
        public void Vector64UInt32ShuffleNativeOneInputWithDirectVectorTest()
        {
            Vector64<uint> result = Vector64.ShuffleNative(Vector64.Create((uint)1, 2), Vector64.Create((uint)1, 0));

            for (int index = 0; index < Vector64<uint>.Count; index++)
            {
                Assert.Equal((uint)(Vector64<uint>.Count - index), result.GetElement(index));
            }
        }

        [Fact]
        public void Vector64ByteShuffleNativeOneInputWithLocalIndicesTest()
        {
            Vector64<byte> vector = Vector64.Create((byte)1, 2, 3, 4, 5, 6, 7, 8);
            Vector64<byte> indices = Vector64.Create((byte)7, 6, 5, 4, 3, 2, 1, 0);
            Vector64<byte> result = Vector64.ShuffleNative(vector, indices);

            for (int index = 0; index < Vector64<byte>.Count; index++)
            {
                Assert.Equal((byte)(Vector64<byte>.Count - index), result.GetElement(index));
            }
        }

        [Fact]
        public void Vector64Int16ShuffleNativeOneInputWithLocalIndicesTest()
        {
            Vector64<short> vector = Vector64.Create((short)1, 2, 3, 4);
            Vector64<short> indices = Vector64.Create((short)3, 2, 1, 0);
            Vector64<short> result = Vector64.ShuffleNative(vector, indices);

            for (int index = 0; index < Vector64<short>.Count; index++)
            {
                Assert.Equal((short)(Vector64<short>.Count - index), result.GetElement(index));
            }
        }

        [Fact]
        public void Vector64Int32ShuffleNativeOneInputWithLocalIndicesTest()
        {
            Vector64<int> vector = Vector64.Create((int)1, 2);
            Vector64<int> indices = Vector64.Create((int)1, 0);
            Vector64<int> result = Vector64.ShuffleNative(vector, indices);

            for (int index = 0; index < Vector64<int>.Count; index++)
            {
                Assert.Equal((int)(Vector64<int>.Count - index), result.GetElement(index));
            }
        }

        [Fact]
        public void Vector64SByteShuffleNativeOneInputWithLocalIndicesTest()
        {
            Vector64<sbyte> vector = Vector64.Create((sbyte)1, 2, 3, 4, 5, 6, 7, 8);
            Vector64<sbyte> indices = Vector64.Create((sbyte)7, 6, 5, 4, 3, 2, 1, 0);
            Vector64<sbyte> result = Vector64.ShuffleNative(vector, indices);

            for (int index = 0; index < Vector64<sbyte>.Count; index++)
            {
                Assert.Equal((sbyte)(Vector64<sbyte>.Count - index), result.GetElement(index));
            }
        }

        [Fact]
        public void Vector64SingleShuffleNativeOneInputWithLocalIndicesTest()
        {
            Vector64<float> vector = Vector64.Create((float)1, 2);
            Vector64<int> indices = Vector64.Create((int)1, 0);
            Vector64<float> result = Vector64.ShuffleNative(vector, indices);

            for (int index = 0; index < Vector64<float>.Count; index++)
            {
                Assert.Equal((float)(Vector64<float>.Count - index), result.GetElement(index));
            }
        }

        [Fact]
        public void Vector64UInt16ShuffleNativeOneInputWithLocalIndicesTest()
        {
            Vector64<ushort> vector = Vector64.Create((ushort)1, 2, 3, 4);
            Vector64<ushort> indices = Vector64.Create((ushort)3, 2, 1, 0);
            Vector64<ushort> result = Vector64.ShuffleNative(vector, indices);

            for (int index = 0; index < Vector64<ushort>.Count; index++)
            {
                Assert.Equal((ushort)(Vector64<ushort>.Count - index), result.GetElement(index));
            }
        }

        [Fact]
        public void Vector64UInt32ShuffleNativeOneInputWithLocalIndicesTest()
        {
            Vector64<uint> vector = Vector64.Create((uint)1, 2);
            Vector64<uint> indices = Vector64.Create((uint)1, 0);
            Vector64<uint> result = Vector64.ShuffleNative(vector, indices);

            for (int index = 0; index < Vector64<uint>.Count; index++)
            {
                Assert.Equal((uint)(Vector64<uint>.Count - index), result.GetElement(index));
            }
        }

        [Fact]
        public void Vector64ByteShuffleNativeOneInputWithZeroIndicesTest()
        {
            Vector64<byte> vector = Vector64.Create((byte)1, 2, 3, 4, 5, 6, 7, 8);
            Vector64<byte> result = Vector64.ShuffleNative(vector, Vector64<byte>.Zero);

            for (int index = 0; index < Vector64<byte>.Count; index++)
            {
                Assert.Equal((byte)1, result.GetElement(index));
            }
        }

        [Fact]
        public void Vector64Int16ShuffleNativeOneInputWithZeroIndicesTest()
        {
            Vector64<short> vector = Vector64.Create((short)1, 2, 3, 4);
            Vector64<short> result = Vector64.ShuffleNative(vector, Vector64<short>.Zero);

            for (int index = 0; index < Vector64<short>.Count; index++)
            {
                Assert.Equal(1, result.GetElement(index));
            }
        }

        [Fact]
        public void Vector64Int32ShuffleNativeOneInputWithZeroIndicesTest()
        {
            Vector64<int> vector = Vector64.Create((int)1, 2);
            Vector64<int> result = Vector64.ShuffleNative(vector, Vector64<int>.Zero);

            for (int index = 0; index < Vector64<int>.Count; index++)
            {
                Assert.Equal((int)1, result.GetElement(index));
            }
        }

        [Fact]
        public void Vector64SByteShuffleNativeOneInputWithZeroIndicesTest()
        {
            Vector64<sbyte> vector = Vector64.Create((sbyte)1, 2, 3, 4, 5, 6, 7, 8);
            Vector64<sbyte> result = Vector64.ShuffleNative(vector, Vector64<sbyte>.Zero);

            for (int index = 0; index < Vector64<sbyte>.Count; index++)
            {
                Assert.Equal((sbyte)1, result.GetElement(index));
            }
        }

        [Fact]
        public void Vector64SingleShuffleNativeOneInputWithZeroIndicesTest()
        {
            Vector64<float> vector = Vector64.Create((float)1, 2);
            Vector64<float> result = Vector64.ShuffleNative(vector, Vector64<int>.Zero);

            for (int index = 0; index < Vector64<float>.Count; index++)
            {
                Assert.Equal((float)1, result.GetElement(index));
            }
        }

        [Fact]
        public void Vector64UInt16ShuffleNativeOneInputWithZeroIndicesTest()
        {
            Vector64<ushort> vector = Vector64.Create((ushort)1, 2, 3, 4);
            Vector64<ushort> result = Vector64.ShuffleNative(vector, Vector64<ushort>.Zero);

            for (int index = 0; index < Vector64<ushort>.Count; index++)
            {
                Assert.Equal((ushort)1, result.GetElement(index));
            }
        }

        [Fact]
        public void Vector64UInt32ShuffleNativeOneInputWithZeroIndicesTest()
        {
            Vector64<uint> vector = Vector64.Create((uint)1, 2);
            Vector64<uint> result = Vector64.ShuffleNative(vector, Vector64<uint>.Zero);

            for (int index = 0; index < Vector64<uint>.Count; index++)
            {
                Assert.Equal((uint)1, result.GetElement(index));
            }
        }

        [Fact]
        public unsafe void Vector64ByteStoreTest()
        {
            byte* value = stackalloc byte[8] {
                0,
                1,
                2,
                3,
                4,
                5,
                6,
                7,
            };

            Vector64.Create((byte)0x1).Store(value);

            for (int index = 0; index < Vector64<byte>.Count; index++)
            {
                Assert.Equal((byte)0x1, value[index]);
            }
        }

        [Fact]
        public unsafe void Vector64DoubleStoreTest()
        {
            double* value = stackalloc double[1] {
                0,
            };

            Vector64.Create((double)0x1).Store(value);

            for (int index = 0; index < Vector64<double>.Count; index++)
            {
                Assert.Equal((double)0x1, value[index]);
            }
        }

        [Fact]
        public unsafe void Vector64Int16StoreTest()
        {
            short* value = stackalloc short[4] {
                0,
                1,
                2,
                3,
            };

            Vector64.Create((short)0x1).Store(value);

            for (int index = 0; index < Vector64<short>.Count; index++)
            {
                Assert.Equal((short)0x1, value[index]);
            }
        }

        [Fact]
        public unsafe void Vector64Int32StoreTest()
        {
            int* value = stackalloc int[2] {
                0,
                1,
            };

            Vector64.Create((int)0x1).Store(value);

            for (int index = 0; index < Vector64<int>.Count; index++)
            {
                Assert.Equal((int)0x1, value[index]);
            }
        }

        [Fact]
        public unsafe void Vector64Int64StoreTest()
        {
            long* value = stackalloc long[1] {
                0,
            };

            Vector64.Create((long)0x1).Store(value);

            for (int index = 0; index < Vector64<long>.Count; index++)
            {
                Assert.Equal((long)0x1, value[index]);
            }
        }

        [Fact]
        public unsafe void Vector64NIntStoreTest()
        {
            if (Environment.Is64BitProcess)
            {
                nint* value = stackalloc nint[1] {
                    0,
                };

                Vector64.Create((nint)0x1).Store(value);

                for (int index = 0; index < Vector64<nint>.Count; index++)
                {
                    Assert.Equal((nint)0x1, value[index]);
                }
            }
            else
            {
                nint* value = stackalloc nint[2] {
                    0,
                    1,
                };

                Vector64.Create((nint)0x1).Store(value);

                for (int index = 0; index < Vector64<nint>.Count; index++)
                {
                    Assert.Equal((nint)0x1, value[index]);
                }
            }
        }

        [Fact]
        public unsafe void Vector64NUIntStoreTest()
        {
            if (Environment.Is64BitProcess)
            {
                nuint* value = stackalloc nuint[1] {
                    0,
                };

                Vector64.Create((nuint)0x1).Store(value);

                for (int index = 0; index < Vector64<nuint>.Count; index++)
                {
                    Assert.Equal((nuint)0x1, value[index]);
                }
            }
            else
            {
                nuint* value = stackalloc nuint[2] {
                    0,
                    1,
                };

                Vector64.Create((nuint)0x1).Store(value);

                for (int index = 0; index < Vector64<nuint>.Count; index++)
                {
                    Assert.Equal((nuint)0x1, value[index]);
                }
            }
        }

        [Fact]
        public unsafe void Vector64SByteStoreTest()
        {
            sbyte* value = stackalloc sbyte[8] {
                0,
                1,
                2,
                3,
                4,
                5,
                6,
                7,
            };

            Vector64.Create((sbyte)0x1).Store(value);

            for (int index = 0; index < Vector64<sbyte>.Count; index++)
            {
                Assert.Equal((sbyte)0x1, value[index]);
            }
        }

        [Fact]
        public unsafe void Vector64SingleStoreTest()
        {
            float* value = stackalloc float[2] {
                0,
                1,
            };

            Vector64.Create((float)0x1).Store(value);

            for (int index = 0; index < Vector64<float>.Count; index++)
            {
                Assert.Equal((float)0x1, value[index]);
            }
        }

        [Fact]
        public unsafe void Vector64UInt16StoreTest()
        {
            ushort* value = stackalloc ushort[4] {
                0,
                1,
                2,
                3,
            };

            Vector64.Create((ushort)0x1).Store(value);

            for (int index = 0; index < Vector64<ushort>.Count; index++)
            {
                Assert.Equal((ushort)0x1, value[index]);
            }
        }

        [Fact]
        public unsafe void Vector64UInt32StoreTest()
        {
            uint* value = stackalloc uint[2] {
                0,
                1,
            };

            Vector64.Create((uint)0x1).Store(value);

            for (int index = 0; index < Vector64<uint>.Count; index++)
            {
                Assert.Equal((uint)0x1, value[index]);
            }
        }

        [Fact]
        public unsafe void Vector64UInt64StoreTest()
        {
            ulong* value = stackalloc ulong[1] {
                0,
            };

            Vector64.Create((ulong)0x1).Store(value);

            for (int index = 0; index < Vector64<ulong>.Count; index++)
            {
                Assert.Equal((ulong)0x1, value[index]);
            }
        }

        [Fact]
        public unsafe void Vector64ByteStoreAlignedTest()
        {
            byte* value = null;

            try
            {
                value = (byte*)NativeMemory.AlignedAlloc(byteCount: 8, alignment: 8);

                value[0] = 0;
                value[1] = 1;
                value[2] = 2;
                value[3] = 3;
                value[4] = 4;
                value[5] = 5;
                value[6] = 6;
                value[7] = 7;

                Vector64.Create((byte)0x1).StoreAligned(value);

                for (int index = 0; index < Vector64<byte>.Count; index++)
                {
                    Assert.Equal((byte)0x1, value[index]);
                }
            }
            finally
            {
                NativeMemory.AlignedFree(value);
            }
        }

        [Fact]
        public unsafe void Vector64DoubleStoreAlignedTest()
        {
            double* value = null;

            try
            {
                value = (double*)NativeMemory.AlignedAlloc(byteCount: 8, alignment: 8);

                value[0] = 0;

                Vector64.Create((double)0x1).StoreAligned(value);

                for (int index = 0; index < Vector64<double>.Count; index++)
                {
                    Assert.Equal((double)0x1, value[index]);
                }
            }
            finally
            {
                NativeMemory.AlignedFree(value);
            }
        }

        [Fact]
        public unsafe void Vector64Int16StoreAlignedTest()
        {
            short* value = null;

            try
            {
                value = (short*)NativeMemory.AlignedAlloc(byteCount: 8, alignment: 8);

                value[0] = 0;
                value[1] = 1;
                value[2] = 2;
                value[3] = 3;

                Vector64.Create((short)0x1).StoreAligned(value);

                for (int index = 0; index < Vector64<short>.Count; index++)
                {
                    Assert.Equal((short)0x1, value[index]);
                }
            }
            finally
            {
                NativeMemory.AlignedFree(value);
            }
        }

        [Fact]
        public unsafe void Vector64Int32StoreAlignedTest()
        {
            int* value = null;

            try
            {
                value = (int*)NativeMemory.AlignedAlloc(byteCount: 8, alignment: 8);

                value[0] = 0;
                value[1] = 1;

                Vector64.Create((int)0x1).StoreAligned(value);

                for (int index = 0; index < Vector64<int>.Count; index++)
                {
                    Assert.Equal((int)0x1, value[index]);
                }
            }
            finally
            {
                NativeMemory.AlignedFree(value);
            }
        }

        [Fact]
        public unsafe void Vector64Int64StoreAlignedTest()
        {
            long* value = null;

            try
            {
                value = (long*)NativeMemory.AlignedAlloc(byteCount: 8, alignment: 8);

                value[0] = 0;

                Vector64.Create((long)0x1).StoreAligned(value);

                for (int index = 0; index < Vector64<long>.Count; index++)
                {
                    Assert.Equal((long)0x1, value[index]);
                }
            }
            finally
            {
                NativeMemory.AlignedFree(value);
            }
        }

        [Fact]
        public unsafe void Vector64NIntStoreAlignedTest()
        {
            nint* value = null;

            try
            {
                value = (nint*)NativeMemory.AlignedAlloc(byteCount: 8, alignment: 8);

                if (Environment.Is64BitProcess)
                {
                    value[0] = 0;
                }
                else
                {
                    value[0] = 0;
                    value[1] = 1;
                }

                Vector64.Create((nint)0x1).StoreAligned(value);

                for (int index = 0; index < Vector64<nint>.Count; index++)
                {
                    Assert.Equal((nint)0x1, value[index]);
                }
            }
            finally
            {
                NativeMemory.AlignedFree(value);
            }
        }

        [Fact]
        public unsafe void Vector64NUIntStoreAlignedTest()
        {
            nuint* value = null;

            try
            {
                value = (nuint*)NativeMemory.AlignedAlloc(byteCount: 8, alignment: 8);

                if (Environment.Is64BitProcess)
                {
                    value[0] = 0;
                }
                else
                {
                    value[0] = 0;
                    value[1] = 1;
                }

                Vector64.Create((nuint)0x1).StoreAligned(value);

                for (int index = 0; index < Vector64<nuint>.Count; index++)
                {
                    Assert.Equal((nuint)0x1, value[index]);
                }
            }
            finally
            {
                NativeMemory.AlignedFree(value);
            }
        }

        [Fact]
        public unsafe void Vector64SByteStoreAlignedTest()
        {
            sbyte* value = null;

            try
            {
                value = (sbyte*)NativeMemory.AlignedAlloc(byteCount: 8, alignment: 8);

                value[0] = 0;
                value[1] = 1;
                value[2] = 2;
                value[3] = 3;
                value[4] = 4;
                value[5] = 5;
                value[6] = 6;
                value[7] = 7;

                Vector64.Create((sbyte)0x1).StoreAligned(value);

                for (int index = 0; index < Vector64<sbyte>.Count; index++)
                {
                    Assert.Equal((sbyte)0x1, value[index]);
                }
            }
            finally
            {
                NativeMemory.AlignedFree(value);
            }
        }

        [Fact]
        public unsafe void Vector64SingleStoreAlignedTest()
        {
            float* value = null;

            try
            {
                value = (float*)NativeMemory.AlignedAlloc(byteCount: 8, alignment: 8);

                value[0] = 0;
                value[1] = 1;

                Vector64.Create((float)0x1).StoreAligned(value);

                for (int index = 0; index < Vector64<float>.Count; index++)
                {
                    Assert.Equal((float)0x1, value[index]);
                }
            }
            finally
            {
                NativeMemory.AlignedFree(value);
            }
        }

        [Fact]
        public unsafe void Vector64UInt16StoreAlignedTest()
        {
            ushort* value = null;

            try
            {
                value = (ushort*)NativeMemory.AlignedAlloc(byteCount: 8, alignment: 8);

                value[0] = 0;
                value[1] = 1;
                value[2] = 2;
                value[3] = 3;

                Vector64.Create((ushort)0x1).StoreAligned(value);

                for (int index = 0; index < Vector64<ushort>.Count; index++)
                {
                    Assert.Equal((ushort)0x1, value[index]);
                }
            }
            finally
            {
                NativeMemory.AlignedFree(value);
            }
        }

        [Fact]
        public unsafe void Vector64UInt32StoreAlignedTest()
        {
            uint* value = null;

            try
            {
                value = (uint*)NativeMemory.AlignedAlloc(byteCount: 8, alignment: 8);

                value[0] = 0;
                value[1] = 1;

                Vector64.Create((uint)0x1).StoreAligned(value);

                for (int index = 0; index < Vector64<uint>.Count; index++)
                {
                    Assert.Equal((uint)0x1, value[index]);
                }
            }
            finally
            {
                NativeMemory.AlignedFree(value);
            }
        }

        [Fact]
        public unsafe void Vector64UInt64StoreAlignedTest()
        {
            ulong* value = null;

            try
            {
                value = (ulong*)NativeMemory.AlignedAlloc(byteCount: 8, alignment: 8);

                value[0] = 0;

                Vector64.Create((ulong)0x1).StoreAligned(value);

                for (int index = 0; index < Vector64<ulong>.Count; index++)
                {
                    Assert.Equal((ulong)0x1, value[index]);
                }
            }
            finally
            {
                NativeMemory.AlignedFree(value);
            }
        }

        [Fact]
        public unsafe void Vector64ByteStoreAlignedNonTemporalTest()
        {
            byte* value = null;

            try
            {
                value = (byte*)NativeMemory.AlignedAlloc(byteCount: 8, alignment: 8);

                value[0] = 0;
                value[1] = 1;
                value[2] = 2;
                value[3] = 3;
                value[4] = 4;
                value[5] = 5;
                value[6] = 6;
                value[7] = 7;

                Vector64.Create((byte)0x1).StoreAlignedNonTemporal(value);

                for (int index = 0; index < Vector64<byte>.Count; index++)
                {
                    Assert.Equal((byte)0x1, value[index]);
                }
            }
            finally
            {
                NativeMemory.AlignedFree(value);
            }
        }

        [Fact]
        public unsafe void Vector64DoubleStoreAlignedNonTemporalTest()
        {
            double* value = null;

            try
            {
                value = (double*)NativeMemory.AlignedAlloc(byteCount: 8, alignment: 8);

                value[0] = 0;

                Vector64.Create((double)0x1).StoreAlignedNonTemporal(value);

                for (int index = 0; index < Vector64<double>.Count; index++)
                {
                    Assert.Equal((double)0x1, value[index]);
                }
            }
            finally
            {
                NativeMemory.AlignedFree(value);
            }
        }

        [Fact]
        public unsafe void Vector64Int16StoreAlignedNonTemporalTest()
        {
            short* value = null;

            try
            {
                value = (short*)NativeMemory.AlignedAlloc(byteCount: 8, alignment: 8);

                value[0] = 0;
                value[1] = 1;
                value[2] = 2;
                value[3] = 3;

                Vector64.Create((short)0x1).StoreAlignedNonTemporal(value);

                for (int index = 0; index < Vector64<short>.Count; index++)
                {
                    Assert.Equal((short)0x1, value[index]);
                }
            }
            finally
            {
                NativeMemory.AlignedFree(value);
            }
        }

        [Fact]
        public unsafe void Vector64Int32StoreAlignedNonTemporalTest()
        {
            int* value = null;

            try
            {
                value = (int*)NativeMemory.AlignedAlloc(byteCount: 8, alignment: 8);

                value[0] = 0;
                value[1] = 1;

                Vector64.Create((int)0x1).StoreAlignedNonTemporal(value);

                for (int index = 0; index < Vector64<int>.Count; index++)
                {
                    Assert.Equal((int)0x1, value[index]);
                }
            }
            finally
            {
                NativeMemory.AlignedFree(value);
            }
        }

        [Fact]
        public unsafe void Vector64Int64StoreAlignedNonTemporalTest()
        {
            long* value = null;

            try
            {
                value = (long*)NativeMemory.AlignedAlloc(byteCount: 8, alignment: 8);

                value[0] = 0;

                Vector64.Create((long)0x1).StoreAlignedNonTemporal(value);

                for (int index = 0; index < Vector64<long>.Count; index++)
                {
                    Assert.Equal((long)0x1, value[index]);
                }
            }
            finally
            {
                NativeMemory.AlignedFree(value);
            }
        }

        [Fact]
        public unsafe void Vector64NIntStoreAlignedNonTemporalTest()
        {
            nint* value = null;

            try
            {
                value = (nint*)NativeMemory.AlignedAlloc(byteCount: 8, alignment: 8);

                if (Environment.Is64BitProcess)
                {
                    value[0] = 0;
                }
                else
                {
                    value[0] = 0;
                    value[1] = 1;
                }

                Vector64.Create((nint)0x1).StoreAlignedNonTemporal(value);

                for (int index = 0; index < Vector64<nint>.Count; index++)
                {
                    Assert.Equal((nint)0x1, value[index]);
                }
            }
            finally
            {
                NativeMemory.AlignedFree(value);
            }
        }

        [Fact]
        public unsafe void Vector64NUIntStoreAlignedNonTemporalTest()
        {
            nuint* value = null;

            try
            {
                value = (nuint*)NativeMemory.AlignedAlloc(byteCount: 8, alignment: 8);

                if (Environment.Is64BitProcess)
                {
                    value[0] = 0;
                }
                else
                {
                    value[0] = 0;
                    value[1] = 1;
                }

                Vector64.Create((nuint)0x1).StoreAlignedNonTemporal(value);

                for (int index = 0; index < Vector64<nuint>.Count; index++)
                {
                    Assert.Equal((nuint)0x1, value[index]);
                }
            }
            finally
            {
                NativeMemory.AlignedFree(value);
            }
        }

        [Fact]
        public unsafe void Vector64SByteStoreAlignedNonTemporalTest()
        {
            sbyte* value = null;

            try
            {
                value = (sbyte*)NativeMemory.AlignedAlloc(byteCount: 8, alignment: 8);

                value[0] = 0;
                value[1] = 1;
                value[2] = 2;
                value[3] = 3;
                value[4] = 4;
                value[5] = 5;
                value[6] = 6;
                value[7] = 7;

                Vector64.Create((sbyte)0x1).StoreAlignedNonTemporal(value);

                for (int index = 0; index < Vector64<sbyte>.Count; index++)
                {
                    Assert.Equal((sbyte)0x1, value[index]);
                }
            }
            finally
            {
                NativeMemory.AlignedFree(value);
            }
        }

        [Fact]
        public unsafe void Vector64SingleStoreAlignedNonTemporalTest()
        {
            float* value = null;

            try
            {
                value = (float*)NativeMemory.AlignedAlloc(byteCount: 8, alignment: 8);

                value[0] = 0;
                value[1] = 1;

                Vector64.Create((float)0x1).StoreAlignedNonTemporal(value);

                for (int index = 0; index < Vector64<float>.Count; index++)
                {
                    Assert.Equal((float)0x1, value[index]);
                }
            }
            finally
            {
                NativeMemory.AlignedFree(value);
            }
        }

        [Fact]
        public unsafe void Vector64UInt16StoreAlignedNonTemporalTest()
        {
            ushort* value = null;

            try
            {
                value = (ushort*)NativeMemory.AlignedAlloc(byteCount: 8, alignment: 8);

                value[0] = 0;
                value[1] = 1;
                value[2] = 2;
                value[3] = 3;

                Vector64.Create((ushort)0x1).StoreAlignedNonTemporal(value);

                for (int index = 0; index < Vector64<ushort>.Count; index++)
                {
                    Assert.Equal((ushort)0x1, value[index]);
                }
            }
            finally
            {
                NativeMemory.AlignedFree(value);
            }
        }

        [Fact]
        public unsafe void Vector64UInt32StoreAlignedNonTemporalTest()
        {
            uint* value = null;

            try
            {
                value = (uint*)NativeMemory.AlignedAlloc(byteCount: 8, alignment: 8);

                value[0] = 0;
                value[1] = 1;

                Vector64.Create((uint)0x1).StoreAlignedNonTemporal(value);

                for (int index = 0; index < Vector64<uint>.Count; index++)
                {
                    Assert.Equal((uint)0x1, value[index]);
                }
            }
            finally
            {
                NativeMemory.AlignedFree(value);
            }
        }

        [Fact]
        public unsafe void Vector64UInt64StoreAlignedNonTemporalTest()
        {
            ulong* value = null;

            try
            {
                value = (ulong*)NativeMemory.AlignedAlloc(byteCount: 8, alignment: 8);

                value[0] = 0;

                Vector64.Create((ulong)0x1).StoreAlignedNonTemporal(value);

                for (int index = 0; index < Vector64<ulong>.Count; index++)
                {
                    Assert.Equal((ulong)0x1, value[index]);
                }
            }
            finally
            {
                NativeMemory.AlignedFree(value);
            }
        }

        [Fact]
        public unsafe void Vector64ByteStoreUnsafeTest()
        {
            byte* value = stackalloc byte[8] {
                0,
                1,
                2,
                3,
                4,
                5,
                6,
                7,
            };

            Vector64.Create((byte)0x1).StoreUnsafe(ref value[0]);

            for (int index = 0; index < Vector64<byte>.Count; index++)
            {
                Assert.Equal((byte)0x1, value[index]);
            }
        }

        [Fact]
        public unsafe void Vector64DoubleStoreUnsafeTest()
        {
            double* value = stackalloc double[1] {
                0,
            };

            Vector64.Create((double)0x1).StoreUnsafe(ref value[0]);

            for (int index = 0; index < Vector64<double>.Count; index++)
            {
                Assert.Equal((double)0x1, value[index]);
            }
        }

        [Fact]
        public unsafe void Vector64Int16StoreUnsafeTest()
        {
            short* value = stackalloc short[4] {
                0,
                1,
                2,
                3,
            };

            Vector64.Create((short)0x1).StoreUnsafe(ref value[0]);

            for (int index = 0; index < Vector64<short>.Count; index++)
            {
                Assert.Equal((short)0x1, value[index]);
            }
        }

        [Fact]
        public unsafe void Vector64Int32StoreUnsafeTest()
        {
            int* value = stackalloc int[2] {
                0,
                1,
            };

            Vector64.Create((int)0x1).StoreUnsafe(ref value[0]);

            for (int index = 0; index < Vector64<int>.Count; index++)
            {
                Assert.Equal((int)0x1, value[index]);
            }
        }

        [Fact]
        public unsafe void Vector64Int64StoreUnsafeTest()
        {
            long* value = stackalloc long[1] {
                0,
            };

            Vector64.Create((long)0x1).StoreUnsafe(ref value[0]);

            for (int index = 0; index < Vector64<long>.Count; index++)
            {
                Assert.Equal((long)0x1, value[index]);
            }
        }

        [Fact]
        public unsafe void Vector64NIntStoreUnsafeTest()
        {
            if (Environment.Is64BitProcess)
            {
                nint* value = stackalloc nint[1] {
                    0,
                };

                Vector64.Create((nint)0x1).StoreUnsafe(ref value[0]);

                for (int index = 0; index < Vector64<nint>.Count; index++)
                {
                    Assert.Equal((nint)0x1, value[index]);
                }
            }
            else
            {
                nint* value = stackalloc nint[2] {
                    0,
                    1,
                };

                Vector64.Create((nint)0x1).StoreUnsafe(ref value[0]);

                for (int index = 0; index < Vector64<nint>.Count; index++)
                {
                    Assert.Equal((nint)0x1, value[index]);
                }
            }
        }

        [Fact]
        public unsafe void Vector64NUIntStoreUnsafeTest()
        {
            if (Environment.Is64BitProcess)
            {
                nuint* value = stackalloc nuint[1] {
                    0,
                };

                Vector64.Create((nuint)0x1).StoreUnsafe(ref value[0]);

                for (int index = 0; index < Vector64<nuint>.Count; index++)
                {
                    Assert.Equal((nuint)0x1, value[index]);
                }
            }
            else
            {
                nuint* value = stackalloc nuint[2] {
                    0,
                    1,
                };

                Vector64.Create((nuint)0x1).StoreUnsafe(ref value[0]);

                for (int index = 0; index < Vector64<nuint>.Count; index++)
                {
                    Assert.Equal((nuint)0x1, value[index]);
                }
            }
        }

        [Fact]
        public unsafe void Vector64SByteStoreUnsafeTest()
        {
            sbyte* value = stackalloc sbyte[8] {
                0,
                1,
                2,
                3,
                4,
                5,
                6,
                7,
            };

            Vector64.Create((sbyte)0x1).StoreUnsafe(ref value[0]);

            for (int index = 0; index < Vector64<sbyte>.Count; index++)
            {
                Assert.Equal((sbyte)0x1, value[index]);
            }
        }

        [Fact]
        public unsafe void Vector64SingleStoreUnsafeTest()
        {
            float* value = stackalloc float[2] {
                0,
                1,
            };

            Vector64.Create((float)0x1).StoreUnsafe(ref value[0]);

            for (int index = 0; index < Vector64<float>.Count; index++)
            {
                Assert.Equal((float)0x1, value[index]);
            }
        }

        [Fact]
        public unsafe void Vector64UInt16StoreUnsafeTest()
        {
            ushort* value = stackalloc ushort[4] {
                0,
                1,
                2,
                3,
            };

            Vector64.Create((ushort)0x1).StoreUnsafe(ref value[0]);

            for (int index = 0; index < Vector64<ushort>.Count; index++)
            {
                Assert.Equal((ushort)0x1, value[index]);
            }
        }

        [Fact]
        public unsafe void Vector64UInt32StoreUnsafeTest()
        {
            uint* value = stackalloc uint[2] {
                0,
                1,
            };

            Vector64.Create((uint)0x1).StoreUnsafe(ref value[0]);

            for (int index = 0; index < Vector64<uint>.Count; index++)
            {
                Assert.Equal((uint)0x1, value[index]);
            }
        }

        [Fact]
        public unsafe void Vector64UInt64StoreUnsafeTest()
        {
            ulong* value = stackalloc ulong[1] {
                0,
            };

            Vector64.Create((ulong)0x1).StoreUnsafe(ref value[0]);

            for (int index = 0; index < Vector64<ulong>.Count; index++)
            {
                Assert.Equal((ulong)0x1, value[index]);
            }
        }

        [Fact]
        public unsafe void Vector64ByteStoreUnsafeIndexTest()
        {
            byte* value = stackalloc byte[8 + 1] {
                0,
                1,
                2,
                3,
                4,
                5,
                6,
                7,
                8,
            };

            Vector64.Create((byte)0x1).StoreUnsafe(ref value[0], 1);

            for (int index = 0; index < Vector64<byte>.Count; index++)
            {
                Assert.Equal((byte)0x1, value[index + 1]);
            }
        }

        [Fact]
        public unsafe void Vector64DoubleStoreUnsafeIndexTest()
        {
            double* value = stackalloc double[1 + 1] {
                0,
                1,
            };

            Vector64.Create((double)0x1).StoreUnsafe(ref value[0], 1);

            for (int index = 0; index < Vector64<double>.Count; index++)
            {
                Assert.Equal((double)0x1, value[index + 1]);
            }
        }

        [Fact]
        public unsafe void Vector64Int16StoreUnsafeIndexTest()
        {
            short* value = stackalloc short[4 + 1] {
                0,
                1,
                2,
                3,
                4,
            };

            Vector64.Create((short)0x1).StoreUnsafe(ref value[0], 1);

            for (int index = 0; index < Vector64<short>.Count; index++)
            {
                Assert.Equal((short)0x1, value[index + 1]);
            }
        }

        [Fact]
        public unsafe void Vector64Int32StoreUnsafeIndexTest()
        {
            int* value = stackalloc int[2 + 1] {
                0,
                1,
                2,
            };

            Vector64.Create((int)0x1).StoreUnsafe(ref value[0], 1);

            for (int index = 0; index < Vector64<int>.Count; index++)
            {
                Assert.Equal((int)0x1, value[index + 1]);
            }
        }

        [Fact]
        public unsafe void Vector64Int64StoreUnsafeIndexTest()
        {
            long* value = stackalloc long[1 + 1] {
                0,
                1,
            };

            Vector64.Create((long)0x1).StoreUnsafe(ref value[0], 1);

            for (int index = 0; index < Vector64<long>.Count; index++)
            {
                Assert.Equal((long)0x1, value[index + 1]);
            }
        }

        [Fact]
        public unsafe void Vector64NIntStoreUnsafeIndexTest()
        {
            if (Environment.Is64BitProcess)
            {
                nint* value = stackalloc nint[1 + 1] {
                    0,
                    1,
                };

                Vector64.Create((nint)0x1).StoreUnsafe(ref value[0], 1);

                for (int index = 0; index < Vector64<nint>.Count; index++)
                {
                    Assert.Equal((nint)0x1, value[index + 1]);
                }
            }
            else
            {
                nint* value = stackalloc nint[2 + 1] {
                    0,
                    1,
                    2,
                };

                Vector64.Create((nint)0x1).StoreUnsafe(ref value[0], 1);

                for (int index = 0; index < Vector64<nint>.Count; index++)
                {
                    Assert.Equal((nint)0x1, value[index + 1]);
                }
            }
        }

        [Fact]
        public unsafe void Vector64NUIntStoreUnsafeIndexTest()
        {
            if (Environment.Is64BitProcess)
            {
                nuint* value = stackalloc nuint[1 + 1] {
                    0,
                    1,
                };

                Vector64.Create((nuint)0x1).StoreUnsafe(ref value[0], 1);

                for (int index = 0; index < Vector64<nuint>.Count; index++)
                {
                    Assert.Equal((nuint)0x1, value[index + 1]);
                }
            }
            else
            {
                nuint* value = stackalloc nuint[2 + 1] {
                    0,
                    1,
                    2,
                };

                Vector64.Create((nuint)0x1).StoreUnsafe(ref value[0], 1);

                for (int index = 0; index < Vector64<nuint>.Count; index++)
                {
                    Assert.Equal((nuint)0x1, value[index + 1]);
                }
            }
        }

        [Fact]
        public unsafe void Vector64SByteStoreUnsafeIndexTest()
        {
            sbyte* value = stackalloc sbyte[8 + 1] {
                0,
                1,
                2,
                3,
                4,
                5,
                6,
                7,
                8,
            };

            Vector64.Create((sbyte)0x1).StoreUnsafe(ref value[0], 1);

            for (int index = 0; index < Vector64<sbyte>.Count; index++)
            {
                Assert.Equal((sbyte)0x1, value[index + 1]);
            }
        }

        [Fact]
        public unsafe void Vector64SingleStoreUnsafeIndexTest()
        {
            float* value = stackalloc float[2 + 1] {
                0,
                1,
                2,
            };

            Vector64.Create((float)0x1).StoreUnsafe(ref value[0], 1);

            for (int index = 0; index < Vector64<float>.Count; index++)
            {
                Assert.Equal((float)0x1, value[index + 1]);
            }
        }

        [Fact]
        public unsafe void Vector64UInt16StoreUnsafeIndexTest()
        {
            ushort* value = stackalloc ushort[4 + 1] {
                0,
                1,
                2,
                3,
                4,
            };

            Vector64.Create((ushort)0x1).StoreUnsafe(ref value[0], 1);

            for (int index = 0; index < Vector64<ushort>.Count; index++)
            {
                Assert.Equal((ushort)0x1, value[index + 1]);
            }
        }

        [Fact]
        public unsafe void Vector64UInt32StoreUnsafeIndexTest()
        {
            uint* value = stackalloc uint[2 + 1] {
                0,
                1,
                2,
            };

            Vector64.Create((uint)0x1).StoreUnsafe(ref value[0], 1);

            for (int index = 0; index < Vector64<uint>.Count; index++)
            {
                Assert.Equal((uint)0x1, value[index + 1]);
            }
        }

        [Fact]
        public unsafe void Vector64UInt64StoreUnsafeIndexTest()
        {
            ulong* value = stackalloc ulong[1 + 1] {
                0,
                1,
            };

            Vector64.Create((ulong)0x1).StoreUnsafe(ref value[0], 1);

            for (int index = 0; index < Vector64<ulong>.Count; index++)
            {
                Assert.Equal((ulong)0x1, value[index + 1]);
            }
        }

        [Fact]
        public void Vector64ByteSumTest()
        {
            Vector64<byte> vector = Vector64.Create((byte)0x01);
            Assert.Equal((byte)8, Vector64.Sum(vector));
        }

        [Fact]
        public void Vector64DoubleSumTest()
        {
            Vector64<double> vector = Vector64.Create((double)0x01);
            Assert.Equal(1.0, Vector64.Sum(vector));
        }

        [Fact]
        public void Vector64Int16SumTest()
        {
            Vector64<short> vector = Vector64.Create((short)0x01);
            Assert.Equal((short)4, Vector64.Sum(vector));
        }

        [Fact]
        public void Vector64Int32SumTest()
        {
            Vector64<int> vector = Vector64.Create((int)0x01);
            Assert.Equal((int)2, Vector64.Sum(vector));
        }

        [Fact]
        public void Vector64Int64SumTest()
        {
            Vector64<long> vector = Vector64.Create((long)0x01);
            Assert.Equal((long)1, Vector64.Sum(vector));
        }

        [Fact]
        public void Vector64NIntSumTest()
        {
            Vector64<nint> vector = Vector64.Create((nint)0x01);

            if (Environment.Is64BitProcess)
            {
                Assert.Equal((nint)1, Vector64.Sum(vector));
            }
            else
            {
                Assert.Equal((nint)2, Vector64.Sum(vector));
            }
        }

        [Fact]
        public void Vector64NUIntSumTest()
        {
            Vector64<nuint> vector = Vector64.Create((nuint)0x01);

            if (Environment.Is64BitProcess)
            {
                Assert.Equal((nuint)1, Vector64.Sum(vector));
            }
            else
            {
                Assert.Equal((nuint)2, Vector64.Sum(vector));
            }
        }

        [Fact]
        public void Vector64SByteSumTest()
        {
            Vector64<sbyte> vector = Vector64.Create((sbyte)0x01);
            Assert.Equal((sbyte)8, Vector64.Sum(vector));
        }

        [Fact]
        public void Vector64SingleSumTest()
        {
            Vector64<float> vector = Vector64.Create((float)0x01);
            Assert.Equal(2.0f, Vector64.Sum(vector));
        }

        [Fact]
        public void Vector64UInt16SumTest()
        {
            Vector64<ushort> vector = Vector64.Create((ushort)0x01);
            Assert.Equal((ushort)4, Vector64.Sum(vector));
        }

        [Fact]
        public void Vector64UInt32SumTest()
        {
            Vector64<uint> vector = Vector64.Create((uint)0x01);
            Assert.Equal((uint)2, Vector64.Sum(vector));
        }

        [Fact]
        public void Vector64UInt64SumTest()
        {
            Vector64<ulong> vector = Vector64.Create((ulong)0x01);
            Assert.Equal((ulong)1, Vector64.Sum(vector));
        }

        [Theory]
        [InlineData(0, 0)]
        [InlineData(1, 1)]
        [InlineData(0, 1, 2, 3, 4, 5, 6, 7, 8)]
        [InlineData(50, 430, int.MaxValue, int.MinValue)]
        public void Vector64Int32IndexerTest(params int[] values)
        {
            var vector = Vector64.Create(values);

            Assert.Equal(vector[0], values[0]);
            Assert.Equal(vector[1], values[1]);
        }

        [Theory]
        [InlineData(0L)]
        [InlineData(1L)]
        [InlineData(0L, 1L, 2L, 3L, 4L, 5L, 6L, 7L, 8L)]
        [InlineData(50L, 430L, long.MaxValue, long.MinValue)]
        public void Vector64Int64IndexerTest(params long[] values)
        {
            var vector = Vector64.Create(values);

            Assert.Equal(vector[0], values[0]);
        }

        [Fact]
        public void Vector64DoubleEqualsNaNTest()
        {
            Vector64<double> nan = Vector64.Create(double.NaN);
            Assert.True(nan.Equals(nan));
        }

        [Fact]
        public void Vector64SingleEqualsNaNTest()
        {
            Vector64<float> nan = Vector64.Create(float.NaN);
            Assert.True(nan.Equals(nan));
        }

        [Fact]
        public void Vector64DoubleEqualsNonCanonicalNaNTest()
        {
            // max 8 bit exponent, just under half max mantissa
            var snan = BitConverter.UInt64BitsToDouble(0x7FF7_FFFF_FFFF_FFFF);
            var nans = new double[]
            {
                double.CopySign(double.NaN, -0.0), // -qnan same as double.NaN
                double.CopySign(double.NaN, +0.0), // +qnan
                double.CopySign(snan, -0.0),       // -snan
                double.CopySign(snan, +0.0),       // +snan
            };

            // all Vector<double> NaNs .Equals compare the same, but == compare as different
            foreach (var i in nans)
            {
                foreach (var j in nans)
                {
                    Assert.True(Vector64.Create(i).Equals(Vector64.Create(j)));
                    Assert.False(Vector64.Create(i) == Vector64.Create(j));
                }
            }
        }

        [Fact]
        public void Vector64SingleEqualsNonCanonicalNaNTest()
        {
            // max 11 bit exponent, just under half max mantissa
            var snan = BitConverter.UInt32BitsToSingle(0x7FBF_FFFF);
            var nans = new float[]
            {
                float.CopySign(float.NaN, -0.0f), // -qnan same as float.NaN
                float.CopySign(float.NaN, +0.0f), // +qnan
                float.CopySign(snan, -0.0f),      // -snan
                float.CopySign(snan, +0.0f),      // +snan
            };

            // all Vector<float> NaNs .Equals compare the same, but == compare as different
            foreach (var i in nans)
            {
                foreach (var j in nans)
                {
                    Assert.True(Vector64.Create(i).Equals(Vector64.Create(j)));
                    Assert.False(Vector64.Create(i) == Vector64.Create(j));
                }
            }
        }

        [Fact]
        public void Vector64SingleCreateFromArrayTest()
        {
            float[] array = [1.0f, 2.0f, 3.0f];
            Vector64<float> vector = Vector64.Create(array);
            Assert.Equal(Vector64.Create(1.0f, 2.0f), vector);
        }

        [Fact]
        public void Vector64SingleCreateFromArrayOffsetTest()
        {
            float[] array = [1.0f, 2.0f, 3.0f];
            Vector64<float> vector = Vector64.Create(array, 1);
            Assert.Equal(Vector64.Create(2.0f, 3.0f), vector);
        }

        [Fact]
        public void Vector64SingleCopyToTest()
        {
            float[] array = new float[2];
            Vector64.Create(2.0f).CopyTo(array);
            Assert.True(array.AsSpan().SequenceEqual([2.0f, 2.0f]));
        }

        [Fact]
        public void Vector64SingleCopyToOffsetTest()
        {
            float[] array = new float[3];
            Vector64.Create(2.0f).CopyTo(array, 1);
            Assert.True(array.AsSpan().SequenceEqual([0.0f, 2.0f, 2.0f]));
        }

        [Fact]
        public void Vector64SByteAbs_MinValue()
        {
            Vector64<sbyte> vector = Vector64.Create(sbyte.MinValue);
            Vector64<sbyte> abs = Vector64.Abs(vector);
            for (int index = 0; index < Vector64<sbyte>.Count; index++)
            {
                Assert.Equal(sbyte.MinValue, vector.GetElement(index));
            }
        }

        [Fact]
        public void IsSupportedByte() => TestIsSupported<byte>();

        [Fact]
        public void IsSupportedDouble() => TestIsSupported<double>();

        [Fact]
        public void IsSupportedInt16() => TestIsSupported<short>();

        [Fact]
        public void IsSupportedInt32() => TestIsSupported<int>();

        [Fact]
        public void IsSupportedInt64() => TestIsSupported<long>();

        [Fact]
        public void IsSupportedIntPtr() => TestIsSupported<nint>();

        [Fact]
        public void IsSupportedSByte() => TestIsSupported<sbyte>();

        [Fact]
        public void IsSupportedSingle() => TestIsSupported<float>();

        [Fact]
        public void IsSupportedUInt16() => TestIsSupported<ushort>();

        [Fact]
        public void IsSupportedUInt32() => TestIsSupported<uint>();

        [Fact]
        public void IsSupportedUInt64() => TestIsSupported<ulong>();

        [Fact]
        public void IsSupportedUIntPtr() => TestIsSupported<nuint>();

        private static void TestIsSupported<T>()
            where T : struct
        {
            Assert.True(Vector64<T>.IsSupported);

            MethodInfo methodInfo = typeof(Vector64<T>).GetProperty("IsSupported", BindingFlags.Public | BindingFlags.Static).GetMethod;
            Assert.True((bool)methodInfo.Invoke(null, null));
        }

        [Fact]
        public void IsNotSupportedBoolean() => TestIsNotSupported<bool>();

        [Fact]
        public void IsNotSupportedChar() => TestIsNotSupported<char>();

        [Fact]
        public void IsNotSupportedHalf() => TestIsNotSupported<Half>();

        [Fact]
        public void IsNotSupportedInt128() => TestIsNotSupported<Int128>();

        [Fact]
        public void IsNotSupportedUInt128() => TestIsNotSupported<UInt128>();

        private static void TestIsNotSupported<T>()
            where T : struct
        {
            Assert.False(Vector64<T>.IsSupported);

            MethodInfo methodInfo = typeof(Vector64<T>).GetProperty("IsSupported", BindingFlags.Public | BindingFlags.Static).GetMethod;
            Assert.False((bool)methodInfo.Invoke(null, null));
        }

        [Fact]
        public void GetOneByte() => TestGetOne<byte>();

        [Fact]
        public void GetOneDouble() => TestGetOne<double>();

        [Fact]
        public void GetOneInt16() => TestGetOne<short>();

        [Fact]
        public void GetOneInt32() => TestGetOne<int>();

        [Fact]
        public void GetOneInt64() => TestGetOne<long>();

        [Fact]
        public void GetOneIntPtr() => TestGetOne<nint>();

        [Fact]
        public void GetOneSByte() => TestGetOne<sbyte>();

        [Fact]
        public void GetOneSingle() => TestGetOne<float>();

        [Fact]
        public void GetOneUInt16() => TestGetOne<ushort>();

        [Fact]
        public void GetOneUInt32() => TestGetOne<uint>();

        [Fact]
        public void GetOneUInt64() => TestGetOne<ulong>();

        [Fact]
        public void GetOneUIntPtr() => TestGetOne<nuint>();

        private static void TestGetOne<T>()
            where T : struct, INumber<T>
        {
            Assert.Equal(Vector64<T>.One, Vector64.Create(T.One));

            MethodInfo methodInfo = typeof(Vector64<T>).GetProperty("One", BindingFlags.Public | BindingFlags.Static).GetMethod;
            Assert.Equal((Vector64<T>)methodInfo.Invoke(null, null), Vector64.Create(T.One));
        }

        [Fact]
        public void GetIndicesByteTest() => TestGetIndices<byte>();

        [Fact]
        public void GetIndicesDoubleTest() => TestGetIndices<double>();

        [Fact]
        public void GetIndicesInt16Test() => TestGetIndices<short>();

        [Fact]
        public void GetIndicesInt32Test() => TestGetIndices<int>();

        [Fact]
        public void GetIndicesInt64Test() => TestGetIndices<long>();

        [Fact]
        public void GetIndicesNIntTest() => TestGetIndices<nint>();

        [Fact]
        public void GetIndicesNUIntTest() => TestGetIndices<nuint>();

        [Fact]
        public void GetIndicesSByteTest() => TestGetIndices<sbyte>();

        [Fact]
        public void GetIndicesSingleTest() => TestGetIndices<float>();

        [Fact]
        public void GetIndicesUInt16Test() => TestGetIndices<ushort>();

        [Fact]
        public void GetIndicesUInt32Test() => TestGetIndices<uint>();

        [Fact]
        public void GetIndicesUInt64Test() => TestGetIndices<ulong>();

        private static void TestGetIndices<T>()
            where T : INumber<T>
        {
            Vector64<T> indices = Vector64<T>.Indices;

            for (int index = 0; index < Vector64<T>.Count; index++)
            {
                Assert.Equal(T.CreateTruncating(index), indices.GetElement(index));
            }
        }

        [Theory]
        [InlineData(0, 2)]
        [InlineData(3, 3)]
        [InlineData(7, unchecked((byte)(-1)))]
        public void CreateSequenceByteTest(byte start, byte step) => TestCreateSequence<byte>(start, step);

        [Theory]
        [InlineData(0.0, +2.0)]
        [InlineData(3.0, +3.0)]
        [InlineData(0.0, -1.0)]
        public void CreateSequenceDoubleTest(double start, double step) => TestCreateSequence<double>(start, step);

        [Theory]
        [InlineData(0, +2)]
        [InlineData(3, +3)]
        [InlineData(3, -1)]
        public void CreateSequenceInt16Test(short start, short step) => TestCreateSequence<short>(start, step);

        [Theory]
        [InlineData(0, +2)]
        [InlineData(3, +3)]
        [InlineData(1, -1)]
        public void CreateSequenceInt32Test(int start, int step) => TestCreateSequence<int>(start, step);

        [Theory]
        [InlineData(0, +2)]
        [InlineData(3, +3)]
        [InlineData(3, -1)]
        public void CreateSequenceInt64Test(long start, long step) => TestCreateSequence<long>(start, step);

        [Theory]
        [InlineData(0, +2)]
        [InlineData(3, +3)]
        [InlineData(7, -1)]
        public void CreateSequenceSByteTest(sbyte start, sbyte step) => TestCreateSequence<sbyte>(start, step);

        [Theory]
        [InlineData(0.0f, +2.0f)]
        [InlineData(3.0f, +3.0f)]
        [InlineData(1.0f, -1.0f)]
        public void CreateSequenceSingleTest(float start, float step) => TestCreateSequence<float>(start, step);

        [Theory]
        [InlineData(0, 2)]
        [InlineData(3, 3)]
        [InlineData(3, unchecked((ushort)(-1)))]
        public void CreateSequenceUInt16Test(ushort start, ushort step) => TestCreateSequence<ushort>(start, step);

        [Theory]
        [InlineData(0, 2)]
        [InlineData(3, 3)]
        [InlineData(1, unchecked((uint)(-1)))]
        public void CreateSequenceUInt32Test(uint start, uint step) => TestCreateSequence<uint>(start, step);

        [Theory]
        [InlineData(0, 2)]
        [InlineData(3, 3)]
        [InlineData(0, unchecked((ulong)(-1)))]
        public void CreateSequenceUInt64Test(ulong start, ulong step) => TestCreateSequence<ulong>(start, step);

        private static void TestCreateSequence<T>(T start, T step)
            where T : INumber<T>
        {
            Vector64<T> sequence = Vector64.CreateSequence(start, step);
            T expected = start;

            for (int index = 0; index < Vector64<T>.Count; index++)
            {
                Assert.Equal(expected, sequence.GetElement(index));
                expected += step;
            }
        }

        [Theory]
        [MemberData(nameof(GenericMathTestMemberData.CosDouble), MemberType = typeof(GenericMathTestMemberData))]
        public void CosDoubleTest(double value, double expectedResult, double variance)
        {
            Vector64<double> actualResult = Vector64.Cos(Vector64.Create(value));
            AssertEqual(Vector64.Create(expectedResult), actualResult, Vector64.Create(variance));
        }

        [Theory]
        [MemberData(nameof(GenericMathTestMemberData.CosSingle), MemberType = typeof(GenericMathTestMemberData))]
        public void CosSingleTest(float value, float expectedResult, float variance)
        {
            Vector64<float> actualResult = Vector64.Cos(Vector64.Create(value));
            AssertEqual(Vector64.Create(expectedResult), actualResult, Vector64.Create(variance));
        }

        [Theory]
        [MemberData(nameof(GenericMathTestMemberData.ExpDouble), MemberType = typeof(GenericMathTestMemberData))]
        public void ExpDoubleTest(double value, double expectedResult, double variance)
        {
            Vector64<double> actualResult = Vector64.Exp(Vector64.Create(value));
            AssertEqual(Vector64.Create(expectedResult), actualResult, Vector64.Create(variance));
        }

        [Theory]
        [MemberData(nameof(GenericMathTestMemberData.ExpSingle), MemberType = typeof(GenericMathTestMemberData))]
        public void ExpSingleTest(float value, float expectedResult, float variance)
        {
            Vector64<float> actualResult = Vector64.Exp(Vector64.Create(value));
            AssertEqual(Vector64.Create(expectedResult), actualResult, Vector64.Create(variance));
        }

        [Theory]
        [MemberData(nameof(GenericMathTestMemberData.LogDouble), MemberType = typeof(GenericMathTestMemberData))]
        public void LogDoubleTest(double value, double expectedResult, double variance)
        {
            Vector64<double> actualResult = Vector64.Log(Vector64.Create(value));
            AssertEqual(Vector64.Create(expectedResult), actualResult, Vector64.Create(variance));
        }

        [Theory]
        [MemberData(nameof(GenericMathTestMemberData.LogSingle), MemberType = typeof(GenericMathTestMemberData))]
        public void LogSingleTest(float value, float expectedResult, float variance)
        {
            Vector64<float> actualResult = Vector64.Log(Vector64.Create(value));
            AssertEqual(Vector64.Create(expectedResult), actualResult, Vector64.Create(variance));
        }

        [Theory]
        [MemberData(nameof(GenericMathTestMemberData.Log2Double), MemberType = typeof(GenericMathTestMemberData))]
        public void Log2DoubleTest(double value, double expectedResult, double variance)
        {
            Vector64<double> actualResult = Vector64.Log2(Vector64.Create(value));
            AssertEqual(Vector64.Create(expectedResult), actualResult, Vector64.Create(variance));
        }

        [Theory]
        [MemberData(nameof(GenericMathTestMemberData.Log2Single), MemberType = typeof(GenericMathTestMemberData))]
        public void Log2SingleTest(float value, float expectedResult, float variance)
        {
            Vector64<float> actualResult = Vector64.Log2(Vector64.Create(value));
            AssertEqual(Vector64.Create(expectedResult), actualResult, Vector64.Create(variance));
        }

        [Theory]
        [MemberData(nameof(GenericMathTestMemberData.FusedMultiplyAddDouble), MemberType = typeof(GenericMathTestMemberData))]
        public void FusedMultiplyAddDoubleTest(double left, double right, double addend, double expectedResult)
        {
            AssertEqual(Vector64.Create(expectedResult), Vector64.FusedMultiplyAdd(Vector64.Create(left), Vector64.Create(right), Vector64.Create(addend)), Vector64<double>.Zero);
            AssertEqual(Vector64.Create(double.MultiplyAddEstimate(left, right, addend)), Vector64.MultiplyAddEstimate(Vector64.Create(left), Vector64.Create(right), Vector64.Create(addend)), Vector64<double>.Zero);
        }

        [Theory]
        [MemberData(nameof(GenericMathTestMemberData.FusedMultiplyAddSingle), MemberType = typeof(GenericMathTestMemberData))]
        public void FusedMultiplyAddSingleTest(float left, float right, float addend, float expectedResult)
        {
            AssertEqual(Vector64.Create(expectedResult), Vector64.FusedMultiplyAdd(Vector64.Create(left), Vector64.Create(right), Vector64.Create(addend)), Vector64<float>.Zero);
            AssertEqual(Vector64.Create(float.MultiplyAddEstimate(left, right, addend)), Vector64.MultiplyAddEstimate(Vector64.Create(left), Vector64.Create(right), Vector64.Create(addend)), Vector64<float>.Zero);
        }

        [Fact]
        [SkipOnMono("https://github.com/dotnet/runtime/issues/100368")]
        public void ConvertToInt32Test()
        {
            Assert.Equal(Vector64.Create(float.ConvertToInteger<int>(float.MinValue)), Vector64.ConvertToInt32(Vector64.Create(float.MinValue)));
            Assert.Equal(Vector64.Create(float.ConvertToInteger<int>(2.6f)), Vector64.ConvertToInt32(Vector64.Create(2.6f)));
            Assert.Equal(Vector64.Create(float.ConvertToInteger<int>(float.MaxValue)), Vector64.ConvertToInt32(Vector64.Create(float.MaxValue)));
        }

        [Fact]
        [SkipOnMono("https://github.com/dotnet/runtime/issues/100368")]
        public void ConvertToInt32NativeTest()
        {
            Assert.Equal(Vector64.Create(float.ConvertToIntegerNative<int>(float.MinValue)), Vector64.ConvertToInt32Native(Vector64.Create(float.MinValue)));
            Assert.Equal(Vector64.Create(float.ConvertToIntegerNative<int>(2.6f)), Vector64.ConvertToInt32Native(Vector64.Create(2.6f)));
            Assert.Equal(Vector64.Create(float.ConvertToIntegerNative<int>(float.MaxValue)), Vector64.ConvertToInt32Native(Vector64.Create(float.MaxValue)));
        }

        [Fact]
        [SkipOnMono("https://github.com/dotnet/runtime/issues/100368")]
        public void ConvertToInt64Test()
        {
            Assert.Equal(Vector64.Create(double.ConvertToInteger<long>(double.MinValue)), Vector64.ConvertToInt64(Vector64.Create(double.MinValue)));
            Assert.Equal(Vector64.Create(double.ConvertToInteger<long>(2.6)), Vector64.ConvertToInt64(Vector64.Create(2.6)));
            Assert.Equal(Vector64.Create(double.ConvertToInteger<long>(double.MaxValue)), Vector64.ConvertToInt64(Vector64.Create(double.MaxValue)));
        }

        [Fact]
        [SkipOnMono("https://github.com/dotnet/runtime/issues/100368")]
        public void ConvertToInt64NativeTest()
        {
            Assert.Equal(Vector64.Create(double.ConvertToIntegerNative<long>(double.MinValue)), Vector64.ConvertToInt64Native(Vector64.Create(double.MinValue)));
            Assert.Equal(Vector64.Create(double.ConvertToIntegerNative<long>(2.6)), Vector64.ConvertToInt64Native(Vector64.Create(2.6)));
            Assert.Equal(Vector64.Create(double.ConvertToIntegerNative<long>(double.MaxValue)), Vector64.ConvertToInt64Native(Vector64.Create(double.MaxValue)));
        }

        [Fact]
        [SkipOnMono("https://github.com/dotnet/runtime/issues/100368")]
        public void ConvertToUInt32Test()
        {
            Assert.Equal(Vector64.Create(float.ConvertToInteger<uint>(float.MinValue)), Vector64.ConvertToUInt32(Vector64.Create(float.MinValue)));
            Assert.Equal(Vector64.Create(float.ConvertToInteger<uint>(2.6f)), Vector64.ConvertToUInt32(Vector64.Create(2.6f)));
            Assert.Equal(Vector64.Create(float.ConvertToInteger<uint>(float.MaxValue)), Vector64.ConvertToUInt32(Vector64.Create(float.MaxValue)));
        }

        [Fact]
        [SkipOnMono("https://github.com/dotnet/runtime/issues/100368")]
        public void ConvertToUInt32NativeTest()
        {
            Assert.Equal(Vector64.Create(float.ConvertToIntegerNative<uint>(float.MinValue)), Vector64.ConvertToUInt32Native(Vector64.Create(float.MinValue)));
            Assert.Equal(Vector64.Create(float.ConvertToIntegerNative<uint>(2.6f)), Vector64.ConvertToUInt32Native(Vector64.Create(2.6f)));
            Assert.Equal(Vector64.Create(float.ConvertToIntegerNative<uint>(float.MaxValue)), Vector64.ConvertToUInt32Native(Vector64.Create(float.MaxValue)));
        }

        [Fact]
        [SkipOnMono("https://github.com/dotnet/runtime/issues/100368")]
        public void ConvertToUInt64Test()
        {
            Assert.Equal(Vector64.Create(double.ConvertToInteger<ulong>(double.MinValue)), Vector64.ConvertToUInt64(Vector64.Create(double.MinValue)));
            Assert.Equal(Vector64.Create(double.ConvertToInteger<ulong>(2.6)), Vector64.ConvertToUInt64(Vector64.Create(2.6)));
            Assert.Equal(Vector64.Create(double.ConvertToInteger<ulong>(double.MaxValue)), Vector64.ConvertToUInt64(Vector64.Create(double.MaxValue)));
        }

        [Fact]
        [SkipOnMono("https://github.com/dotnet/runtime/issues/100368")]
        public void ConvertToUInt64NativeTest()
        {
            Assert.Equal(Vector64.Create(double.ConvertToIntegerNative<ulong>(double.MinValue)), Vector64.ConvertToUInt64Native(Vector64.Create(double.MinValue)));
            Assert.Equal(Vector64.Create(double.ConvertToIntegerNative<ulong>(2.6)), Vector64.ConvertToUInt64Native(Vector64.Create(2.6)));
            Assert.Equal(Vector64.Create(double.ConvertToIntegerNative<ulong>(double.MaxValue)), Vector64.ConvertToUInt64Native(Vector64.Create(double.MaxValue)));
        }

        [Theory]
        [MemberData(nameof(GenericMathTestMemberData.ClampDouble), MemberType = typeof(GenericMathTestMemberData))]
        public void ClampDoubleTest(double x, double min, double max, double expectedResult)
        {
            Vector64<double> actualResult = Vector64.Clamp(Vector64.Create(x), Vector64.Create(min), Vector64.Create(max));
            AssertEqual(Vector64.Create(expectedResult), actualResult, Vector64<double>.Zero);
        }

        [Theory]
        [MemberData(nameof(GenericMathTestMemberData.ClampSingle), MemberType = typeof(GenericMathTestMemberData))]
        public void ClampSingleTest(float x, float min, float max, float expectedResult)
        {
            Vector64<float> actualResult = Vector64.Clamp(Vector64.Create(x), Vector64.Create(min), Vector64.Create(max));
            AssertEqual(Vector64.Create(expectedResult), actualResult, Vector64<float>.Zero);
        }

        [Theory]
        [MemberData(nameof(GenericMathTestMemberData.CopySignDouble), MemberType = typeof(GenericMathTestMemberData))]
        public void CopySignDoubleTest(double x, double y, double expectedResult)
        {
            Vector64<double> actualResult = Vector64.CopySign(Vector64.Create(x), Vector64.Create(y));
            AssertEqual(Vector64.Create(expectedResult), actualResult, Vector64<double>.Zero);
        }

        [Theory]
        [MemberData(nameof(GenericMathTestMemberData.CopySignSingle), MemberType = typeof(GenericMathTestMemberData))]
        public void CopySignSingleTest(float x, float y, float expectedResult)
        {
            Vector64<float> actualResult = Vector64.CopySign(Vector64.Create(x), Vector64.Create(y));
            AssertEqual(Vector64.Create(expectedResult), actualResult, Vector64<float>.Zero);
        }

        [Theory]
        [MemberData(nameof(GenericMathTestMemberData.DegreesToRadiansDouble), MemberType = typeof(GenericMathTestMemberData))]
        public void DegreesToRadiansDoubleTest(double value, double expectedResult, double variance)
        {
            Vector64<double> actualResult1 = Vector64.DegreesToRadians(Vector64.Create(-value));
            AssertEqual(Vector64.Create(-expectedResult), actualResult1, Vector64.Create(variance));

            Vector64<double> actualResult2 = Vector64.DegreesToRadians(Vector64.Create(+value));
            AssertEqual(Vector64.Create(+expectedResult), actualResult2, Vector64.Create(variance));
        }

        [Theory]
        [MemberData(nameof(GenericMathTestMemberData.DegreesToRadiansSingle), MemberType = typeof(GenericMathTestMemberData))]
        public void DegreesToRadiansSingleTest(float value, float expectedResult, float variance)
        {
            AssertEqual(Vector64.Create(-expectedResult), Vector64.DegreesToRadians(Vector64.Create(-value)), Vector64.Create(variance));
            AssertEqual(Vector64.Create(+expectedResult), Vector64.DegreesToRadians(Vector64.Create(+value)), Vector64.Create(variance));
        }

        [Theory]
        [MemberData(nameof(GenericMathTestMemberData.HypotDouble), MemberType = typeof(GenericMathTestMemberData))]
        public void HypotDoubleTest(double x, double y, double expectedResult, double variance)
        {
            AssertEqual(Vector64.Create(expectedResult), Vector64.Hypot(Vector64.Create(-x), Vector64.Create(-y)), Vector64.Create(variance));
            AssertEqual(Vector64.Create(expectedResult), Vector64.Hypot(Vector64.Create(-x), Vector64.Create(+y)), Vector64.Create(variance));
            AssertEqual(Vector64.Create(expectedResult), Vector64.Hypot(Vector64.Create(+x), Vector64.Create(-y)), Vector64.Create(variance));
            AssertEqual(Vector64.Create(expectedResult), Vector64.Hypot(Vector64.Create(+x), Vector64.Create(+y)), Vector64.Create(variance));

            AssertEqual(Vector64.Create(expectedResult), Vector64.Hypot(Vector64.Create(-y), Vector64.Create(-x)), Vector64.Create(variance));
            AssertEqual(Vector64.Create(expectedResult), Vector64.Hypot(Vector64.Create(-y), Vector64.Create(+x)), Vector64.Create(variance));
            AssertEqual(Vector64.Create(expectedResult), Vector64.Hypot(Vector64.Create(+y), Vector64.Create(-x)), Vector64.Create(variance));
            AssertEqual(Vector64.Create(expectedResult), Vector64.Hypot(Vector64.Create(+y), Vector64.Create(+x)), Vector64.Create(variance));
        }

        [Theory]
        [MemberData(nameof(GenericMathTestMemberData.HypotSingle), MemberType = typeof(GenericMathTestMemberData))]
        public void HypotSingleTest(float x, float y, float expectedResult, float variance)
        {
            AssertEqual(Vector64.Create(expectedResult), Vector64.Hypot(Vector64.Create(-x), Vector64.Create(-y)), Vector64.Create(variance));
            AssertEqual(Vector64.Create(expectedResult), Vector64.Hypot(Vector64.Create(-x), Vector64.Create(+y)), Vector64.Create(variance));
            AssertEqual(Vector64.Create(expectedResult), Vector64.Hypot(Vector64.Create(+x), Vector64.Create(-y)), Vector64.Create(variance));
            AssertEqual(Vector64.Create(expectedResult), Vector64.Hypot(Vector64.Create(+x), Vector64.Create(+y)), Vector64.Create(variance));

            AssertEqual(Vector64.Create(expectedResult), Vector64.Hypot(Vector64.Create(-y), Vector64.Create(-x)), Vector64.Create(variance));
            AssertEqual(Vector64.Create(expectedResult), Vector64.Hypot(Vector64.Create(-y), Vector64.Create(+x)), Vector64.Create(variance));
            AssertEqual(Vector64.Create(expectedResult), Vector64.Hypot(Vector64.Create(+y), Vector64.Create(-x)), Vector64.Create(variance));
            AssertEqual(Vector64.Create(expectedResult), Vector64.Hypot(Vector64.Create(+y), Vector64.Create(+x)), Vector64.Create(variance));
        }

        private void IsEvenInteger<T>(T value)
            where T : INumber<T>
        {
            Assert.Equal(T.IsEvenInteger(value) ? Vector64<T>.AllBitsSet : Vector64<T>.Zero, Vector64.IsEvenInteger(Vector64.Create(value)));
        }

        [Theory]
        [MemberData(nameof(GenericMathTestMemberData.IsTestByte), MemberType = typeof(GenericMathTestMemberData))]
        public void IsEvenIntegerByteTest(byte value) => IsEvenInteger(value);

        [Theory]
        [MemberData(nameof(GenericMathTestMemberData.IsTestDouble), MemberType = typeof(GenericMathTestMemberData))]
        public void IsEvenIntegerDoubleTest(double value) => IsEvenInteger(value);

        [Theory]
        [MemberData(nameof(GenericMathTestMemberData.IsTestInt16), MemberType = typeof(GenericMathTestMemberData))]
        public void IsEvenIntegerInt16Test(short value) => IsEvenInteger(value);

        [Theory]
        [MemberData(nameof(GenericMathTestMemberData.IsTestInt32), MemberType = typeof(GenericMathTestMemberData))]
        public void IsEvenIntegerInt32Test(int value) => IsEvenInteger(value);

        [Theory]
        [MemberData(nameof(GenericMathTestMemberData.IsTestInt64), MemberType = typeof(GenericMathTestMemberData))]
        public void IsEvenIntegerInt64Test(long value) => IsEvenInteger(value);

        [Theory]
        [MemberData(nameof(GenericMathTestMemberData.IsTestSByte), MemberType = typeof(GenericMathTestMemberData))]
        public void IsEvenIntegerSByteTest(sbyte value) => IsEvenInteger(value);

        [Theory]
        [MemberData(nameof(GenericMathTestMemberData.IsTestSingle), MemberType = typeof(GenericMathTestMemberData))]
        public void IsEvenIntegerSingleTest(float value) => IsEvenInteger(value);

        [Theory]
        [MemberData(nameof(GenericMathTestMemberData.IsTestUInt16), MemberType = typeof(GenericMathTestMemberData))]
        public void IsEvenIntegerUInt16Test(ushort value) => IsEvenInteger(value);

        [Theory]
        [MemberData(nameof(GenericMathTestMemberData.IsTestUInt32), MemberType = typeof(GenericMathTestMemberData))]
        public void IsEvenIntegerUInt32Test(uint value) => IsEvenInteger(value);

        [Theory]
        [MemberData(nameof(GenericMathTestMemberData.IsTestUInt64), MemberType = typeof(GenericMathTestMemberData))]
        public void IsEvenIntegerUInt64Test(ulong value) => IsEvenInteger(value);

        private void IsFinite<T>(T value)
            where T : INumber<T>
        {
            Assert.Equal(T.IsFinite(value) ? Vector64<T>.AllBitsSet : Vector64<T>.Zero, Vector64.IsFinite(Vector64.Create(value)));
        }

        [Theory]
        [MemberData(nameof(GenericMathTestMemberData.IsTestByte), MemberType = typeof(GenericMathTestMemberData))]
        public void IsFiniteByteTest(byte value) => IsFinite(value);

        [Theory]
        [MemberData(nameof(GenericMathTestMemberData.IsTestDouble), MemberType = typeof(GenericMathTestMemberData))]
        public void IsFiniteDoubleTest(double value) => IsFinite(value);

        [Theory]
        [MemberData(nameof(GenericMathTestMemberData.IsTestInt16), MemberType = typeof(GenericMathTestMemberData))]
        public void IsFiniteInt16Test(short value) => IsFinite(value);

        [Theory]
        [MemberData(nameof(GenericMathTestMemberData.IsTestInt32), MemberType = typeof(GenericMathTestMemberData))]
        public void IsFiniteInt32Test(int value) => IsFinite(value);

        [Theory]
        [MemberData(nameof(GenericMathTestMemberData.IsTestInt64), MemberType = typeof(GenericMathTestMemberData))]
        public void IsFiniteInt64Test(long value) => IsFinite(value);

        [Theory]
        [MemberData(nameof(GenericMathTestMemberData.IsTestSByte), MemberType = typeof(GenericMathTestMemberData))]
        public void IsFiniteSByteTest(sbyte value) => IsFinite(value);

        [Theory]
        [MemberData(nameof(GenericMathTestMemberData.IsTestSingle), MemberType = typeof(GenericMathTestMemberData))]
        public void IsFiniteSingleTest(float value) => IsFinite(value);

        [Theory]
        [MemberData(nameof(GenericMathTestMemberData.IsTestUInt16), MemberType = typeof(GenericMathTestMemberData))]
        public void IsFiniteUInt16Test(ushort value) => IsFinite(value);

        [Theory]
        [MemberData(nameof(GenericMathTestMemberData.IsTestUInt32), MemberType = typeof(GenericMathTestMemberData))]
        public void IsFiniteUInt32Test(uint value) => IsFinite(value);

        [Theory]
        [MemberData(nameof(GenericMathTestMemberData.IsTestUInt64), MemberType = typeof(GenericMathTestMemberData))]
        public void IsFiniteUInt64Test(ulong value) => IsFinite(value);

        private void IsInfinity<T>(T value)
            where T : INumber<T>
        {
            Assert.Equal(T.IsInfinity(value) ? Vector64<T>.AllBitsSet : Vector64<T>.Zero, Vector64.IsInfinity(Vector64.Create(value)));
        }

        [Theory]
        [MemberData(nameof(GenericMathTestMemberData.IsTestByte), MemberType = typeof(GenericMathTestMemberData))]
        public void IsInfinityByteTest(byte value) => IsInfinity(value);

        [Theory]
        [MemberData(nameof(GenericMathTestMemberData.IsTestDouble), MemberType = typeof(GenericMathTestMemberData))]
        public void IsInfinityDoubleTest(double value) => IsInfinity(value);

        [Theory]
        [MemberData(nameof(GenericMathTestMemberData.IsTestInt16), MemberType = typeof(GenericMathTestMemberData))]
        public void IsInfinityInt16Test(short value) => IsInfinity(value);

        [Theory]
        [MemberData(nameof(GenericMathTestMemberData.IsTestInt32), MemberType = typeof(GenericMathTestMemberData))]
        public void IsInfinityInt32Test(int value) => IsInfinity(value);

        [Theory]
        [MemberData(nameof(GenericMathTestMemberData.IsTestInt64), MemberType = typeof(GenericMathTestMemberData))]
        public void IsInfinityInt64Test(long value) => IsInfinity(value);

        [Theory]
        [MemberData(nameof(GenericMathTestMemberData.IsTestSByte), MemberType = typeof(GenericMathTestMemberData))]
        public void IsInfinitySByteTest(sbyte value) => IsInfinity(value);

        [Theory]
        [MemberData(nameof(GenericMathTestMemberData.IsTestSingle), MemberType = typeof(GenericMathTestMemberData))]
        public void IsInfinitySingleTest(float value) => IsInfinity(value);

        [Theory]
        [MemberData(nameof(GenericMathTestMemberData.IsTestUInt16), MemberType = typeof(GenericMathTestMemberData))]
        public void IsInfinityUInt16Test(ushort value) => IsInfinity(value);

        [Theory]
        [MemberData(nameof(GenericMathTestMemberData.IsTestUInt32), MemberType = typeof(GenericMathTestMemberData))]
        public void IsInfinityUInt32Test(uint value) => IsInfinity(value);

        [Theory]
        [MemberData(nameof(GenericMathTestMemberData.IsTestUInt64), MemberType = typeof(GenericMathTestMemberData))]
        public void IsInfinityUInt64Test(ulong value) => IsInfinity(value);

        private void IsInteger<T>(T value)
            where T : INumber<T>
        {
            Assert.Equal(T.IsInteger(value) ? Vector64<T>.AllBitsSet : Vector64<T>.Zero, Vector64.IsInteger(Vector64.Create(value)));
        }

        [Theory]
        [MemberData(nameof(GenericMathTestMemberData.IsTestByte), MemberType = typeof(GenericMathTestMemberData))]
        public void IsIntegerByteTest(byte value) => IsInteger(value);

        [Theory]
        [MemberData(nameof(GenericMathTestMemberData.IsTestDouble), MemberType = typeof(GenericMathTestMemberData))]
        public void IsIntegerDoubleTest(double value) => IsInteger(value);

        [Theory]
        [MemberData(nameof(GenericMathTestMemberData.IsTestInt16), MemberType = typeof(GenericMathTestMemberData))]
        public void IsIntegerInt16Test(short value) => IsInteger(value);

        [Theory]
        [MemberData(nameof(GenericMathTestMemberData.IsTestInt32), MemberType = typeof(GenericMathTestMemberData))]
        public void IsIntegerInt32Test(int value) => IsInteger(value);

        [Theory]
        [MemberData(nameof(GenericMathTestMemberData.IsTestInt64), MemberType = typeof(GenericMathTestMemberData))]
        public void IsIntegerInt64Test(long value) => IsInteger(value);

        [Theory]
        [MemberData(nameof(GenericMathTestMemberData.IsTestSByte), MemberType = typeof(GenericMathTestMemberData))]
        public void IsIntegerSByteTest(sbyte value) => IsInteger(value);

        [Theory]
        [MemberData(nameof(GenericMathTestMemberData.IsTestSingle), MemberType = typeof(GenericMathTestMemberData))]
        public void IsIntegerSingleTest(float value) => IsInteger(value);

        [Theory]
        [MemberData(nameof(GenericMathTestMemberData.IsTestUInt16), MemberType = typeof(GenericMathTestMemberData))]
        public void IsIntegerUInt16Test(ushort value) => IsInteger(value);

        [Theory]
        [MemberData(nameof(GenericMathTestMemberData.IsTestUInt32), MemberType = typeof(GenericMathTestMemberData))]
        public void IsIntegerUInt32Test(uint value) => IsInteger(value);

        [Theory]
        [MemberData(nameof(GenericMathTestMemberData.IsTestUInt64), MemberType = typeof(GenericMathTestMemberData))]
        public void IsIntegerUInt64Test(ulong value) => IsInteger(value);

        private void IsNaN<T>(T value)
            where T : INumber<T>
        {
            Assert.Equal(T.IsNaN(value) ? Vector64<T>.AllBitsSet : Vector64<T>.Zero, Vector64.IsNaN(Vector64.Create(value)));
        }

        [Theory]
        [MemberData(nameof(GenericMathTestMemberData.IsTestByte), MemberType = typeof(GenericMathTestMemberData))]
        public void IsNaNByteTest(byte value) => IsNaN(value);

        [Theory]
        [MemberData(nameof(GenericMathTestMemberData.IsTestDouble), MemberType = typeof(GenericMathTestMemberData))]
        public void IsNaNDoubleTest(double value) => IsNaN(value);

        [Theory]
        [MemberData(nameof(GenericMathTestMemberData.IsTestInt16), MemberType = typeof(GenericMathTestMemberData))]
        public void IsNaNInt16Test(short value) => IsNaN(value);

        [Theory]
        [MemberData(nameof(GenericMathTestMemberData.IsTestInt32), MemberType = typeof(GenericMathTestMemberData))]
        public void IsNaNInt32Test(int value) => IsNaN(value);

        [Theory]
        [MemberData(nameof(GenericMathTestMemberData.IsTestInt64), MemberType = typeof(GenericMathTestMemberData))]
        public void IsNaNInt64Test(long value) => IsNaN(value);

        [Theory]
        [MemberData(nameof(GenericMathTestMemberData.IsTestSByte), MemberType = typeof(GenericMathTestMemberData))]
        public void IsNaNSByteTest(sbyte value) => IsNaN(value);

        [Theory]
        [MemberData(nameof(GenericMathTestMemberData.IsTestSingle), MemberType = typeof(GenericMathTestMemberData))]
        public void IsNaNSingleTest(float value) => IsNaN(value);

        [Theory]
        [MemberData(nameof(GenericMathTestMemberData.IsTestUInt16), MemberType = typeof(GenericMathTestMemberData))]
        public void IsNaNUInt16Test(ushort value) => IsNaN(value);

        [Theory]
        [MemberData(nameof(GenericMathTestMemberData.IsTestUInt32), MemberType = typeof(GenericMathTestMemberData))]
        public void IsNaNUInt32Test(uint value) => IsNaN(value);

        [Theory]
        [MemberData(nameof(GenericMathTestMemberData.IsTestUInt64), MemberType = typeof(GenericMathTestMemberData))]
        public void IsNaNUInt64Test(ulong value) => IsNaN(value);

        private void IsNegative<T>(T value)
            where T : INumber<T>
        {
            Assert.Equal(T.IsNegative(value) ? Vector64<T>.AllBitsSet : Vector64<T>.Zero, Vector64.IsNegative(Vector64.Create(value)));
        }

        [Theory]
        [MemberData(nameof(GenericMathTestMemberData.IsTestByte), MemberType = typeof(GenericMathTestMemberData))]
        public void IsNegativeByteTest(byte value) => IsNegative(value);

        [Theory]
        [MemberData(nameof(GenericMathTestMemberData.IsTestDouble), MemberType = typeof(GenericMathTestMemberData))]
        public void IsNegativeDoubleTest(double value) => IsNegative(value);

        [Theory]
        [MemberData(nameof(GenericMathTestMemberData.IsTestInt16), MemberType = typeof(GenericMathTestMemberData))]
        public void IsNegativeInt16Test(short value) => IsNegative(value);

        [Theory]
        [MemberData(nameof(GenericMathTestMemberData.IsTestInt32), MemberType = typeof(GenericMathTestMemberData))]
        public void IsNegativeInt32Test(int value) => IsNegative(value);

        [Theory]
        [MemberData(nameof(GenericMathTestMemberData.IsTestInt64), MemberType = typeof(GenericMathTestMemberData))]
        public void IsNegativeInt64Test(long value) => IsNegative(value);

        [Theory]
        [MemberData(nameof(GenericMathTestMemberData.IsTestSByte), MemberType = typeof(GenericMathTestMemberData))]
        public void IsNegativeSByteTest(sbyte value) => IsNegative(value);

        [Theory]
        [MemberData(nameof(GenericMathTestMemberData.IsTestSingle), MemberType = typeof(GenericMathTestMemberData))]
        public void IsNegativeSingleTest(float value) => IsNegative(value);

        [Theory]
        [MemberData(nameof(GenericMathTestMemberData.IsTestUInt16), MemberType = typeof(GenericMathTestMemberData))]
        public void IsNegativeUInt16Test(ushort value) => IsNegative(value);

        [Theory]
        [MemberData(nameof(GenericMathTestMemberData.IsTestUInt32), MemberType = typeof(GenericMathTestMemberData))]
        public void IsNegativeUInt32Test(uint value) => IsNegative(value);

        [Theory]
        [MemberData(nameof(GenericMathTestMemberData.IsTestUInt64), MemberType = typeof(GenericMathTestMemberData))]
        public void IsNegativeUInt64Test(ulong value) => IsNegative(value);

        private void IsNegativeInfinity<T>(T value)
            where T : INumber<T>
        {
            Assert.Equal(T.IsNegativeInfinity(value) ? Vector64<T>.AllBitsSet : Vector64<T>.Zero, Vector64.IsNegativeInfinity(Vector64.Create(value)));
        }

        [Theory]
        [MemberData(nameof(GenericMathTestMemberData.IsTestByte), MemberType = typeof(GenericMathTestMemberData))]
        public void IsNegativeInfinityByteTest(byte value) => IsNegativeInfinity(value);

        [Theory]
        [MemberData(nameof(GenericMathTestMemberData.IsTestDouble), MemberType = typeof(GenericMathTestMemberData))]
        public void IsNegativeInfinityDoubleTest(double value) => IsNegativeInfinity(value);

        [Theory]
        [MemberData(nameof(GenericMathTestMemberData.IsTestInt16), MemberType = typeof(GenericMathTestMemberData))]
        public void IsNegativeInfinityInt16Test(short value) => IsNegativeInfinity(value);

        [Theory]
        [MemberData(nameof(GenericMathTestMemberData.IsTestInt32), MemberType = typeof(GenericMathTestMemberData))]
        public void IsNegativeInfinityInt32Test(int value) => IsNegativeInfinity(value);

        [Theory]
        [MemberData(nameof(GenericMathTestMemberData.IsTestInt64), MemberType = typeof(GenericMathTestMemberData))]
        public void IsNegativeInfinityInt64Test(long value) => IsNegativeInfinity(value);

        [Theory]
        [MemberData(nameof(GenericMathTestMemberData.IsTestSByte), MemberType = typeof(GenericMathTestMemberData))]
        public void IsNegativeInfinitySByteTest(sbyte value) => IsNegativeInfinity(value);

        [Theory]
        [MemberData(nameof(GenericMathTestMemberData.IsTestSingle), MemberType = typeof(GenericMathTestMemberData))]
        public void IsNegativeInfinitySingleTest(float value) => IsNegativeInfinity(value);

        [Theory]
        [MemberData(nameof(GenericMathTestMemberData.IsTestUInt16), MemberType = typeof(GenericMathTestMemberData))]
        public void IsNegativeInfinityUInt16Test(ushort value) => IsNegativeInfinity(value);

        [Theory]
        [MemberData(nameof(GenericMathTestMemberData.IsTestUInt32), MemberType = typeof(GenericMathTestMemberData))]
        public void IsNegativeInfinityUInt32Test(uint value) => IsNegativeInfinity(value);

        [Theory]
        [MemberData(nameof(GenericMathTestMemberData.IsTestUInt64), MemberType = typeof(GenericMathTestMemberData))]
        public void IsNegativeInfinityUInt64Test(ulong value) => IsNegativeInfinity(value);

        private void IsNormal<T>(T value)
            where T : INumber<T>
        {
            Assert.Equal(T.IsNormal(value) ? Vector64<T>.AllBitsSet : Vector64<T>.Zero, Vector64.IsNormal(Vector64.Create(value)));
        }

        [Theory]
        [MemberData(nameof(GenericMathTestMemberData.IsTestByte), MemberType = typeof(GenericMathTestMemberData))]
        public void IsNormalByteTest(byte value) => IsNormal(value);

        [Theory]
        [MemberData(nameof(GenericMathTestMemberData.IsTestDouble), MemberType = typeof(GenericMathTestMemberData))]
        public void IsNormalDoubleTest(double value) => IsNormal(value);

        [Theory]
        [MemberData(nameof(GenericMathTestMemberData.IsTestInt16), MemberType = typeof(GenericMathTestMemberData))]
        public void IsNormalInt16Test(short value) => IsNormal(value);

        [Theory]
        [MemberData(nameof(GenericMathTestMemberData.IsTestInt32), MemberType = typeof(GenericMathTestMemberData))]
        public void IsNormalInt32Test(int value) => IsNormal(value);

        [Theory]
        [MemberData(nameof(GenericMathTestMemberData.IsTestInt64), MemberType = typeof(GenericMathTestMemberData))]
        public void IsNormalInt64Test(long value) => IsNormal(value);

        [Theory]
        [MemberData(nameof(GenericMathTestMemberData.IsTestSByte), MemberType = typeof(GenericMathTestMemberData))]
        public void IsNormalSByteTest(sbyte value) => IsNormal(value);

        [Theory]
        [MemberData(nameof(GenericMathTestMemberData.IsTestSingle), MemberType = typeof(GenericMathTestMemberData))]
        public void IsNormalSingleTest(float value) => IsNormal(value);

        [Theory]
        [MemberData(nameof(GenericMathTestMemberData.IsTestUInt16), MemberType = typeof(GenericMathTestMemberData))]
        public void IsNormalUInt16Test(ushort value) => IsNormal(value);

        [Theory]
        [MemberData(nameof(GenericMathTestMemberData.IsTestUInt32), MemberType = typeof(GenericMathTestMemberData))]
        public void IsNormalUInt32Test(uint value) => IsNormal(value);

        [Theory]
        [MemberData(nameof(GenericMathTestMemberData.IsTestUInt64), MemberType = typeof(GenericMathTestMemberData))]
        public void IsNormalUInt64Test(ulong value) => IsNormal(value);

        private void IsOddInteger<T>(T value)
            where T : INumber<T>
        {
            Assert.Equal(T.IsOddInteger(value) ? Vector64<T>.AllBitsSet : Vector64<T>.Zero, Vector64.IsOddInteger(Vector64.Create(value)));
        }

        [Theory]
        [MemberData(nameof(GenericMathTestMemberData.IsTestByte), MemberType = typeof(GenericMathTestMemberData))]
        public void IsOddIntegerByteTest(byte value) => IsOddInteger(value);

        [Theory]
        [MemberData(nameof(GenericMathTestMemberData.IsTestDouble), MemberType = typeof(GenericMathTestMemberData))]
        public void IsOddIntegerDoubleTest(double value) => IsOddInteger(value);

        [Theory]
        [MemberData(nameof(GenericMathTestMemberData.IsTestInt16), MemberType = typeof(GenericMathTestMemberData))]
        public void IsOddIntegerInt16Test(short value) => IsOddInteger(value);

        [Theory]
        [MemberData(nameof(GenericMathTestMemberData.IsTestInt32), MemberType = typeof(GenericMathTestMemberData))]
        public void IsOddIntegerInt32Test(int value) => IsOddInteger(value);

        [Theory]
        [MemberData(nameof(GenericMathTestMemberData.IsTestInt64), MemberType = typeof(GenericMathTestMemberData))]
        public void IsOddIntegerInt64Test(long value) => IsOddInteger(value);

        [Theory]
        [MemberData(nameof(GenericMathTestMemberData.IsTestSByte), MemberType = typeof(GenericMathTestMemberData))]
        public void IsOddIntegerSByteTest(sbyte value) => IsOddInteger(value);

        [Theory]
        [MemberData(nameof(GenericMathTestMemberData.IsTestSingle), MemberType = typeof(GenericMathTestMemberData))]
        public void IsOddIntegerSingleTest(float value) => IsOddInteger(value);

        [Theory]
        [MemberData(nameof(GenericMathTestMemberData.IsTestUInt16), MemberType = typeof(GenericMathTestMemberData))]
        public void IsOddIntegerUInt16Test(ushort value) => IsOddInteger(value);

        [Theory]
        [MemberData(nameof(GenericMathTestMemberData.IsTestUInt32), MemberType = typeof(GenericMathTestMemberData))]
        public void IsOddIntegerUInt32Test(uint value) => IsOddInteger(value);

        [Theory]
        [MemberData(nameof(GenericMathTestMemberData.IsTestUInt64), MemberType = typeof(GenericMathTestMemberData))]
        public void IsOddIntegerUInt64Test(ulong value) => IsOddInteger(value);

        private void IsPositive<T>(T value)
            where T : INumber<T>
        {
            Assert.Equal(T.IsPositive(value) ? Vector64<T>.AllBitsSet : Vector64<T>.Zero, Vector64.IsPositive(Vector64.Create(value)));
        }

        [Theory]
        [MemberData(nameof(GenericMathTestMemberData.IsTestByte), MemberType = typeof(GenericMathTestMemberData))]
        public void IsPositiveByteTest(byte value) => IsPositive(value);

        [Theory]
        [MemberData(nameof(GenericMathTestMemberData.IsTestDouble), MemberType = typeof(GenericMathTestMemberData))]
        public void IsPositiveDoubleTest(double value) => IsPositive(value);

        [Theory]
        [MemberData(nameof(GenericMathTestMemberData.IsTestInt16), MemberType = typeof(GenericMathTestMemberData))]
        public void IsPositiveInt16Test(short value) => IsPositive(value);

        [Theory]
        [MemberData(nameof(GenericMathTestMemberData.IsTestInt32), MemberType = typeof(GenericMathTestMemberData))]
        public void IsPositiveInt32Test(int value) => IsPositive(value);

        [Theory]
        [MemberData(nameof(GenericMathTestMemberData.IsTestInt64), MemberType = typeof(GenericMathTestMemberData))]
        public void IsPositiveInt64Test(long value) => IsPositive(value);

        [Theory]
        [MemberData(nameof(GenericMathTestMemberData.IsTestSByte), MemberType = typeof(GenericMathTestMemberData))]
        public void IsPositiveSByteTest(sbyte value) => IsPositive(value);

        [Theory]
        [MemberData(nameof(GenericMathTestMemberData.IsTestSingle), MemberType = typeof(GenericMathTestMemberData))]
        public void IsPositiveSingleTest(float value) => IsPositive(value);

        [Theory]
        [MemberData(nameof(GenericMathTestMemberData.IsTestUInt16), MemberType = typeof(GenericMathTestMemberData))]
        public void IsPositiveUInt16Test(ushort value) => IsPositive(value);

        [Theory]
        [MemberData(nameof(GenericMathTestMemberData.IsTestUInt32), MemberType = typeof(GenericMathTestMemberData))]
        public void IsPositiveUInt32Test(uint value) => IsPositive(value);

        [Theory]
        [MemberData(nameof(GenericMathTestMemberData.IsTestUInt64), MemberType = typeof(GenericMathTestMemberData))]
        public void IsPositiveUInt64Test(ulong value) => IsPositive(value);

        private void IsPositiveInfinity<T>(T value)
            where T : INumber<T>
        {
            Assert.Equal(T.IsPositiveInfinity(value) ? Vector64<T>.AllBitsSet : Vector64<T>.Zero, Vector64.IsPositiveInfinity(Vector64.Create(value)));
        }

        [Theory]
        [MemberData(nameof(GenericMathTestMemberData.IsTestByte), MemberType = typeof(GenericMathTestMemberData))]
        public void IsPositiveInfinityByteTest(byte value) => IsPositiveInfinity(value);

        [Theory]
        [MemberData(nameof(GenericMathTestMemberData.IsTestDouble), MemberType = typeof(GenericMathTestMemberData))]
        public void IsPositiveInfinityDoubleTest(double value) => IsPositiveInfinity(value);

        [Theory]
        [MemberData(nameof(GenericMathTestMemberData.IsTestInt16), MemberType = typeof(GenericMathTestMemberData))]
        public void IsPositiveInfinityInt16Test(short value) => IsPositiveInfinity(value);

        [Theory]
        [MemberData(nameof(GenericMathTestMemberData.IsTestInt32), MemberType = typeof(GenericMathTestMemberData))]
        public void IsPositiveInfinityInt32Test(int value) => IsPositiveInfinity(value);

        [Theory]
        [MemberData(nameof(GenericMathTestMemberData.IsTestInt64), MemberType = typeof(GenericMathTestMemberData))]
        public void IsPositiveInfinityInt64Test(long value) => IsPositiveInfinity(value);

        [Theory]
        [MemberData(nameof(GenericMathTestMemberData.IsTestSByte), MemberType = typeof(GenericMathTestMemberData))]
        public void IsPositiveInfinitySByteTest(sbyte value) => IsPositiveInfinity(value);

        [Theory]
        [MemberData(nameof(GenericMathTestMemberData.IsTestSingle), MemberType = typeof(GenericMathTestMemberData))]
        public void IsPositiveInfinitySingleTest(float value) => IsPositiveInfinity(value);

        [Theory]
        [MemberData(nameof(GenericMathTestMemberData.IsTestUInt16), MemberType = typeof(GenericMathTestMemberData))]
        public void IsPositiveInfinityUInt16Test(ushort value) => IsPositiveInfinity(value);

        [Theory]
        [MemberData(nameof(GenericMathTestMemberData.IsTestUInt32), MemberType = typeof(GenericMathTestMemberData))]
        public void IsPositiveInfinityUInt32Test(uint value) => IsPositiveInfinity(value);

        [Theory]
        [MemberData(nameof(GenericMathTestMemberData.IsTestUInt64), MemberType = typeof(GenericMathTestMemberData))]
        public void IsPositiveInfinityUInt64Test(ulong value) => IsPositiveInfinity(value);

        private void IsSubnormal<T>(T value)
            where T : INumber<T>
        {
            Assert.Equal(T.IsSubnormal(value) ? Vector64<T>.AllBitsSet : Vector64<T>.Zero, Vector64.IsSubnormal(Vector64.Create(value)));
        }

        [Theory]
        [MemberData(nameof(GenericMathTestMemberData.IsTestByte), MemberType = typeof(GenericMathTestMemberData))]
        public void IsSubnormalByteTest(byte value) => IsSubnormal(value);

        [Theory]
        [MemberData(nameof(GenericMathTestMemberData.IsTestDouble), MemberType = typeof(GenericMathTestMemberData))]
        public void IsSubnormalDoubleTest(double value) => IsSubnormal(value);

        [Theory]
        [MemberData(nameof(GenericMathTestMemberData.IsTestInt16), MemberType = typeof(GenericMathTestMemberData))]
        public void IsSubnormalInt16Test(short value) => IsSubnormal(value);

        [Theory]
        [MemberData(nameof(GenericMathTestMemberData.IsTestInt32), MemberType = typeof(GenericMathTestMemberData))]
        public void IsSubnormalInt32Test(int value) => IsSubnormal(value);

        [Theory]
        [MemberData(nameof(GenericMathTestMemberData.IsTestInt64), MemberType = typeof(GenericMathTestMemberData))]
        public void IsSubnormalInt64Test(long value) => IsSubnormal(value);

        [Theory]
        [MemberData(nameof(GenericMathTestMemberData.IsTestSByte), MemberType = typeof(GenericMathTestMemberData))]
        public void IsSubnormalSByteTest(sbyte value) => IsSubnormal(value);

        [Theory]
        [MemberData(nameof(GenericMathTestMemberData.IsTestSingle), MemberType = typeof(GenericMathTestMemberData))]
        public void IsSubnormalSingleTest(float value) => IsSubnormal(value);

        [Theory]
        [MemberData(nameof(GenericMathTestMemberData.IsTestUInt16), MemberType = typeof(GenericMathTestMemberData))]
        public void IsSubnormalUInt16Test(ushort value) => IsSubnormal(value);

        [Theory]
        [MemberData(nameof(GenericMathTestMemberData.IsTestUInt32), MemberType = typeof(GenericMathTestMemberData))]
        public void IsSubnormalUInt32Test(uint value) => IsSubnormal(value);

        [Theory]
        [MemberData(nameof(GenericMathTestMemberData.IsTestUInt64), MemberType = typeof(GenericMathTestMemberData))]
        public void IsSubnormalUInt64Test(ulong value) => IsSubnormal(value);

        private void IsZero<T>(T value)
            where T : INumber<T>
        {
            Assert.Equal(T.IsZero(value) ? Vector64<T>.AllBitsSet : Vector64<T>.Zero, Vector64.IsZero(Vector64.Create(value)));
        }

        [Theory]
        [MemberData(nameof(GenericMathTestMemberData.IsTestByte), MemberType = typeof(GenericMathTestMemberData))]
        public void IsZeroByteTest(byte value) => IsZero(value);

        [Theory]
        [MemberData(nameof(GenericMathTestMemberData.IsTestDouble), MemberType = typeof(GenericMathTestMemberData))]
        public void IsZeroDoubleTest(double value) => IsZero(value);

        [Theory]
        [MemberData(nameof(GenericMathTestMemberData.IsTestInt16), MemberType = typeof(GenericMathTestMemberData))]
        public void IsZeroInt16Test(short value) => IsZero(value);

        [Theory]
        [MemberData(nameof(GenericMathTestMemberData.IsTestInt32), MemberType = typeof(GenericMathTestMemberData))]
        public void IsZeroInt32Test(int value) => IsZero(value);

        [Theory]
        [MemberData(nameof(GenericMathTestMemberData.IsTestInt64), MemberType = typeof(GenericMathTestMemberData))]
        public void IsZeroInt64Test(long value) => IsZero(value);

        [Theory]
        [MemberData(nameof(GenericMathTestMemberData.IsTestSByte), MemberType = typeof(GenericMathTestMemberData))]
        public void IsZeroSByteTest(sbyte value) => IsZero(value);

        [Theory]
        [MemberData(nameof(GenericMathTestMemberData.IsTestSingle), MemberType = typeof(GenericMathTestMemberData))]
        public void IsZeroSingleTest(float value) => IsZero(value);

        [Theory]
        [MemberData(nameof(GenericMathTestMemberData.IsTestUInt16), MemberType = typeof(GenericMathTestMemberData))]
        public void IsZeroUInt16Test(ushort value) => IsZero(value);

        [Theory]
        [MemberData(nameof(GenericMathTestMemberData.IsTestUInt32), MemberType = typeof(GenericMathTestMemberData))]
        public void IsZeroUInt32Test(uint value) => IsZero(value);

        [Theory]
        [MemberData(nameof(GenericMathTestMemberData.IsTestUInt64), MemberType = typeof(GenericMathTestMemberData))]
        public void IsZeroUInt64Test(ulong value) => IsZero(value);

        [Theory]
        [MemberData(nameof(GenericMathTestMemberData.LerpDouble), MemberType = typeof(GenericMathTestMemberData))]
        public void LerpDoubleTest(double x, double y, double amount, double expectedResult)
        {
            AssertEqual(Vector64.Create(+expectedResult), Vector64.Lerp(Vector64.Create(+x), Vector64.Create(+y), Vector64.Create(amount)), Vector64<double>.Zero);
            AssertEqual(Vector64.Create((expectedResult == 0.0) ? expectedResult : -expectedResult), Vector64.Lerp(Vector64.Create(-x), Vector64.Create(-y), Vector64.Create(amount)), Vector64<double>.Zero);
        }

        [Theory]
        [MemberData(nameof(GenericMathTestMemberData.LerpSingle), MemberType = typeof(GenericMathTestMemberData))]
        public void LerpSingleTest(float x, float y, float amount, float expectedResult)
        {
            AssertEqual(Vector64.Create(+expectedResult), Vector64.Lerp(Vector64.Create(+x), Vector64.Create(+y), Vector64.Create(amount)), Vector64<float>.Zero);
            AssertEqual(Vector64.Create((expectedResult == 0.0f) ? expectedResult : -expectedResult), Vector64.Lerp(Vector64.Create(-x), Vector64.Create(-y), Vector64.Create(amount)), Vector64<float>.Zero);
        }

        [Theory]
        [MemberData(nameof(GenericMathTestMemberData.MaxDouble), MemberType = typeof(GenericMathTestMemberData))]
        public void MaxDoubleTest(double x, double y, double expectedResult)
        {
            Vector64<double> actualResult = Vector64.Max(Vector64.Create(x), Vector64.Create(y));
            AssertEqual(Vector64.Create(expectedResult), actualResult, Vector64<double>.Zero);
        }

        [Theory]
        [MemberData(nameof(GenericMathTestMemberData.MaxSingle), MemberType = typeof(GenericMathTestMemberData))]
        public void MaxSingleTest(float x, float y, float expectedResult)
        {
            Vector64<float> actualResult = Vector64.Max(Vector64.Create(x), Vector64.Create(y));
            AssertEqual(Vector64.Create(expectedResult), actualResult, Vector64<float>.Zero);
        }

        [Theory]
        [MemberData(nameof(GenericMathTestMemberData.MaxMagnitudeDouble), MemberType = typeof(GenericMathTestMemberData))]
        public void MaxMagnitudeDoubleTest(double x, double y, double expectedResult)
        {
            Vector64<double> actualResult = Vector64.MaxMagnitude(Vector64.Create(x), Vector64.Create(y));
            AssertEqual(Vector64.Create(expectedResult), actualResult, Vector64<double>.Zero);
        }

        [Theory]
        [MemberData(nameof(GenericMathTestMemberData.MaxMagnitudeSingle), MemberType = typeof(GenericMathTestMemberData))]
        public void MaxMagnitudeSingleTest(float x, float y, float expectedResult)
        {
            Vector64<float> actualResult = Vector64.MaxMagnitude(Vector64.Create(x), Vector64.Create(y));
            AssertEqual(Vector64.Create(expectedResult), actualResult, Vector64<float>.Zero);
        }

        [Theory]
        [MemberData(nameof(GenericMathTestMemberData.MaxMagnitudeNumberDouble), MemberType = typeof(GenericMathTestMemberData))]
        public void MaxMagnitudeNumberDoubleTest(double x, double y, double expectedResult)
        {
            Vector64<double> actualResult = Vector64.MaxMagnitudeNumber(Vector64.Create(x), Vector64.Create(y));
            AssertEqual(Vector64.Create(expectedResult), actualResult, Vector64<double>.Zero);
        }

        [Theory]
        [MemberData(nameof(GenericMathTestMemberData.MaxMagnitudeNumberSingle), MemberType = typeof(GenericMathTestMemberData))]
        public void MaxMagnitudeNumberSingleTest(float x, float y, float expectedResult)
        {
            Vector64<float> actualResult = Vector64.MaxMagnitudeNumber(Vector64.Create(x), Vector64.Create(y));
            AssertEqual(Vector64.Create(expectedResult), actualResult, Vector64<float>.Zero);
        }

        [Theory]
        [MemberData(nameof(GenericMathTestMemberData.MaxNumberDouble), MemberType = typeof(GenericMathTestMemberData))]
        public void MaxNumberDoubleTest(double x, double y, double expectedResult)
        {
            Vector64<double> actualResult = Vector64.MaxNumber(Vector64.Create(x), Vector64.Create(y));
            AssertEqual(Vector64.Create(expectedResult), actualResult, Vector64<double>.Zero);
        }

        [Theory]
        [MemberData(nameof(GenericMathTestMemberData.MaxNumberSingle), MemberType = typeof(GenericMathTestMemberData))]
        public void MaxNumberSingleTest(float x, float y, float expectedResult)
        {
            Vector64<float> actualResult = Vector64.MaxNumber(Vector64.Create(x), Vector64.Create(y));
            AssertEqual(Vector64.Create(expectedResult), actualResult, Vector64<float>.Zero);
        }

        [Theory]
        [MemberData(nameof(GenericMathTestMemberData.MinDouble), MemberType = typeof(GenericMathTestMemberData))]
        public void MinDoubleTest(double x, double y, double expectedResult)
        {
            Vector64<double> actualResult = Vector64.Min(Vector64.Create(x), Vector64.Create(y));
            AssertEqual(Vector64.Create(expectedResult), actualResult, Vector64<double>.Zero);
        }

        [Theory]
        [MemberData(nameof(GenericMathTestMemberData.MinSingle), MemberType = typeof(GenericMathTestMemberData))]
        public void MinSingleTest(float x, float y, float expectedResult)
        {
            Vector64<float> actualResult = Vector64.Min(Vector64.Create(x), Vector64.Create(y));
            AssertEqual(Vector64.Create(expectedResult), actualResult, Vector64<float>.Zero);
        }

        [Theory]
        [MemberData(nameof(GenericMathTestMemberData.MinMagnitudeDouble), MemberType = typeof(GenericMathTestMemberData))]
        public void MinMagnitudeDoubleTest(double x, double y, double expectedResult)
        {
            Vector64<double> actualResult = Vector64.MinMagnitude(Vector64.Create(x), Vector64.Create(y));
            AssertEqual(Vector64.Create(expectedResult), actualResult, Vector64<double>.Zero);
        }

        [Theory]
        [MemberData(nameof(GenericMathTestMemberData.MinMagnitudeSingle), MemberType = typeof(GenericMathTestMemberData))]
        public void MinMagnitudeSingleTest(float x, float y, float expectedResult)
        {
            Vector64<float> actualResult = Vector64.MinMagnitude(Vector64.Create(x), Vector64.Create(y));
            AssertEqual(Vector64.Create(expectedResult), actualResult, Vector64<float>.Zero);
        }

        [Theory]
        [MemberData(nameof(GenericMathTestMemberData.MinMagnitudeNumberDouble), MemberType = typeof(GenericMathTestMemberData))]
        public void MinMagnitudeNumberDoubleTest(double x, double y, double expectedResult)
        {
            Vector64<double> actualResult = Vector64.MinMagnitudeNumber(Vector64.Create(x), Vector64.Create(y));
            AssertEqual(Vector64.Create(expectedResult), actualResult, Vector64<double>.Zero);
        }

        [Theory]
        [MemberData(nameof(GenericMathTestMemberData.MinMagnitudeNumberSingle), MemberType = typeof(GenericMathTestMemberData))]
        public void MinMagnitudeNumberSingleTest(float x, float y, float expectedResult)
        {
            Vector64<float> actualResult = Vector64.MinMagnitudeNumber(Vector64.Create(x), Vector64.Create(y));
            AssertEqual(Vector64.Create(expectedResult), actualResult, Vector64<float>.Zero);
        }

        [Theory]
        [MemberData(nameof(GenericMathTestMemberData.MinNumberDouble), MemberType = typeof(GenericMathTestMemberData))]
        public void MinNumberDoubleTest(double x, double y, double expectedResult)
        {
            Vector64<double> actualResult = Vector64.MinNumber(Vector64.Create(x), Vector64.Create(y));
            AssertEqual(Vector64.Create(expectedResult), actualResult, Vector64<double>.Zero);
        }

        [Theory]
        [MemberData(nameof(GenericMathTestMemberData.MinNumberSingle), MemberType = typeof(GenericMathTestMemberData))]
        public void MinNumberSingleTest(float x, float y, float expectedResult)
        {
            Vector64<float> actualResult = Vector64.MinNumber(Vector64.Create(x), Vector64.Create(y));
            AssertEqual(Vector64.Create(expectedResult), actualResult, Vector64<float>.Zero);
        }

        [Theory]
        [MemberData(nameof(GenericMathTestMemberData.RadiansToDegreesDouble), MemberType = typeof(GenericMathTestMemberData))]
        public void RadiansToDegreesDoubleTest(double value, double expectedResult, double variance)
        {
            AssertEqual(Vector64.Create(-expectedResult), Vector64.RadiansToDegrees(Vector64.Create(-value)), Vector64.Create(variance));
            AssertEqual(Vector64.Create(+expectedResult), Vector64.RadiansToDegrees(Vector64.Create(+value)), Vector64.Create(variance));
        }

        [Theory]
        [MemberData(nameof(GenericMathTestMemberData.RadiansToDegreesSingle), MemberType = typeof(GenericMathTestMemberData))]
        public void RadiansToDegreesSingleTest(float value, float expectedResult, float variance)
        {
            AssertEqual(Vector64.Create(-expectedResult), Vector64.RadiansToDegrees(Vector64.Create(-value)), Vector64.Create(variance));
            AssertEqual(Vector64.Create(+expectedResult), Vector64.RadiansToDegrees(Vector64.Create(+value)), Vector64.Create(variance));
        }

        [Theory]
        [MemberData(nameof(GenericMathTestMemberData.RoundDouble), MemberType = typeof(GenericMathTestMemberData))]
        public void RoundDoubleTest(double value, double expectedResult)
        {
            Vector64<double> actualResult = Vector64.Round(Vector64.Create(value));
            AssertEqual(Vector64.Create(expectedResult), actualResult, Vector64<double>.Zero);
        }

        [Theory]
        [MemberData(nameof(GenericMathTestMemberData.RoundSingle), MemberType = typeof(GenericMathTestMemberData))]
        public void RoundSingleTest(float value, float expectedResult)
        {
            Vector64<float> actualResult = Vector64.Round(Vector64.Create(value));
            AssertEqual(Vector64.Create(expectedResult), actualResult, Vector64<float>.Zero);
        }

        [Theory]
        [MemberData(nameof(GenericMathTestMemberData.RoundAwayFromZeroDouble), MemberType = typeof(GenericMathTestMemberData))]
        public void RoundAwayFromZeroDoubleTest(double value, double expectedResult)
        {
            Vector64<double> actualResult = Vector64.Round(Vector64.Create(value), MidpointRounding.AwayFromZero);
            AssertEqual(Vector64.Create(expectedResult), actualResult, Vector64<double>.Zero);
        }

        [Theory]
        [MemberData(nameof(GenericMathTestMemberData.RoundAwayFromZeroSingle), MemberType = typeof(GenericMathTestMemberData))]
        public void RoundAwayFromZeroSingleTest(float value, float expectedResult)
        {
            Vector64<float> actualResult = Vector64.Round(Vector64.Create(value), MidpointRounding.AwayFromZero);
            AssertEqual(Vector64.Create(expectedResult), actualResult, Vector64<float>.Zero);
        }

        [Theory]
        [MemberData(nameof(GenericMathTestMemberData.RoundToEvenDouble), MemberType = typeof(GenericMathTestMemberData))]
        public void RoundToEvenDoubleTest(double value, double expectedResult)
        {
            Vector64<double> actualResult = Vector64.Round(Vector64.Create(value), MidpointRounding.ToEven);
            AssertEqual(Vector64.Create(expectedResult), actualResult, Vector64<double>.Zero);
        }

        [Theory]
        [MemberData(nameof(GenericMathTestMemberData.RoundToEvenSingle), MemberType = typeof(GenericMathTestMemberData))]
        public void RoundToEvenSingleTest(float value, float expectedResult)
        {
            Vector64<float> actualResult = Vector64.Round(Vector64.Create(value), MidpointRounding.ToEven);
            AssertEqual(Vector64.Create(expectedResult), actualResult, Vector64<float>.Zero);
        }

        [Theory]
        [MemberData(nameof(GenericMathTestMemberData.SinDouble), MemberType = typeof(GenericMathTestMemberData))]
        public void SinDoubleTest(double value, double expectedResult, double variance)
        {
            Vector64<double> actualResult = Vector64.Sin(Vector64.Create(value));
            AssertEqual(Vector64.Create(expectedResult), actualResult, Vector64.Create(variance));
        }

        [Theory]
        [MemberData(nameof(GenericMathTestMemberData.SinSingle), MemberType = typeof(GenericMathTestMemberData))]
        public void SinSingleTest(float value, float expectedResult, float variance)
        {
            Vector64<float> actualResult = Vector64.Sin(Vector64.Create(value));
            AssertEqual(Vector64.Create(expectedResult), actualResult, Vector64.Create(variance));
        }

        [Theory]
        [MemberData(nameof(GenericMathTestMemberData.SinCosDouble), MemberType = typeof(GenericMathTestMemberData))]
        public void SinCosDoubleTest(double value, double expectedResultSin, double expectedResultCos, double allowedVarianceSin, double allowedVarianceCos)
        {
            (Vector64<double> resultSin, Vector64<double> resultCos) = Vector64.SinCos(Vector64.Create(value));
            AssertEqual(Vector64.Create(expectedResultSin), resultSin, Vector64.Create(allowedVarianceSin));
            AssertEqual(Vector64.Create(expectedResultCos), resultCos, Vector64.Create(allowedVarianceCos));
        }

        [Theory]
        [MemberData(nameof(GenericMathTestMemberData.SinCosSingle), MemberType = typeof(GenericMathTestMemberData))]
        public void SinCosSingleTest(float value, float expectedResultSin, float expectedResultCos, float allowedVarianceSin, float allowedVarianceCos)
        {
            (Vector64<float> resultSin, Vector64<float> resultCos) = Vector64.SinCos(Vector64.Create(value));
            AssertEqual(Vector64.Create(expectedResultSin), resultSin, Vector64.Create(allowedVarianceSin));
            AssertEqual(Vector64.Create(expectedResultCos), resultCos, Vector64.Create(allowedVarianceCos));
        }

        [Theory]
        [MemberData(nameof(GenericMathTestMemberData.TruncateDouble), MemberType = typeof(GenericMathTestMemberData))]
        public void TruncateDoubleTest(double value, double expectedResult)
        {
            Vector64<double> actualResult = Vector64.Truncate(Vector64.Create(value));
            AssertEqual(Vector64.Create(expectedResult), actualResult, Vector64<double>.Zero);
        }

        [Theory]
        [MemberData(nameof(GenericMathTestMemberData.TruncateSingle), MemberType = typeof(GenericMathTestMemberData))]
        public void TruncateSingleTest(float value, float expectedResult)
        {
            Vector64<float> actualResult = Vector64.Truncate(Vector64.Create(value));
            AssertEqual(Vector64.Create(expectedResult), actualResult, Vector64<float>.Zero);
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        private void AllAnyNoneTest<T>(T value1, T value2)
            where T : struct, INumber<T>
        {
            var input1 = Vector64.Create<T>(value1);
            var input2 = Vector64.Create<T>(value2);

            Assert.True(Vector64.All(input1, value1));
            Assert.True(Vector64.All(input2, value2));
            Assert.False(Vector64.All(input1.WithElement(0, value2), value1));
            Assert.False(Vector64.All(input2.WithElement(0, value1), value2));
            Assert.False(Vector64.All(input1, value2));
            Assert.False(Vector64.All(input2, value1));
            Assert.Equal(Vector64<T>.Count == 1, Vector64.All(input1.WithElement(0, value2), value2));
            Assert.Equal(Vector64<T>.Count == 1, Vector64.All(input2.WithElement(0, value1), value1));

            Assert.True(Vector64.Any(input1, value1));
            Assert.True(Vector64.Any(input2, value2));
            Assert.Equal(Vector64<T>.Count != 1, Vector64.Any(input1.WithElement(0, value2), value1));
            Assert.Equal(Vector64<T>.Count != 1, Vector64.Any(input2.WithElement(0, value1), value2));
            Assert.False(Vector64.Any(input1, value2));
            Assert.False(Vector64.Any(input2, value1));
            Assert.True(Vector64.Any(input1.WithElement(0, value2), value2));
            Assert.True(Vector64.Any(input2.WithElement(0, value1), value1));

            Assert.False(Vector64.None(input1, value1));
            Assert.False(Vector64.None(input2, value2));
            Assert.Equal(Vector64<T>.Count == 1, Vector64.None(input1.WithElement(0, value2), value1));
            Assert.Equal(Vector64<T>.Count == 1, Vector64.None(input2.WithElement(0, value1), value2));
            Assert.True(Vector64.None(input1, value2));
            Assert.True(Vector64.None(input2, value1));
            Assert.False(Vector64.None(input1.WithElement(0, value2), value2));
            Assert.False(Vector64.None(input2.WithElement(0, value1), value1));
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        private void AllAnyNoneTest_IFloatingPointIeee754<T>(T value)
            where T : struct, IFloatingPointIeee754<T>
        {
            var input = Vector64.Create<T>(value);

            Assert.False(Vector64.All(input, value));
            Assert.False(Vector64.Any(input, value));
            Assert.True(Vector64.None(input, value));
        }

        [Fact]
        public void AllAnyNoneByteTest() => AllAnyNoneTest<byte>(3, 2);

        [Fact]
        public void AllAnyNoneDoubleTest() => AllAnyNoneTest<double>(3, 2);

        [Fact]
        public void AllAnyNoneDoubleTest_AllBitsSet() => AllAnyNoneTest_IFloatingPointIeee754<double>(BitConverter.Int64BitsToDouble(-1));

        [Fact]
        public void AllAnyNoneInt16Test() => AllAnyNoneTest<short>(3, 2);

        [Fact]
        public void AllAnyNoneInt32Test() => AllAnyNoneTest<int>(3, 2);

        [Fact]
        public void AllAnyNoneInt64Test() => AllAnyNoneTest<long>(3, 2);

        [Fact]
        public void AllAnyNoneSByteTest() => AllAnyNoneTest<sbyte>(3, 2);

        [Fact]
        public void AllAnyNoneSingleTest() => AllAnyNoneTest<float>(3, 2);

        [Fact]
        public void AllAnyNoneSingleTest_AllBitsSet() => AllAnyNoneTest_IFloatingPointIeee754<float>(BitConverter.Int32BitsToSingle(-1));

        [Fact]
        public void AllAnyNoneUInt16Test() => AllAnyNoneTest<ushort>(3, 2);

        [Fact]
        public void AllAnyNoneUInt32Test() => AllAnyNoneTest<uint>(3, 2);

        [Fact]
        public void AllAnyNoneUInt64Test() => AllAnyNoneTest<ulong>(3, 2);

        [MethodImpl(MethodImplOptions.NoInlining)]
        private void AllAnyNoneWhereAllBitsSetTest<T>(T allBitsSet, T value2)
            where T : struct, INumber<T>
        {
            var input1 = Vector64.Create<T>(allBitsSet);
            var input2 = Vector64.Create<T>(value2);

            Assert.True(Vector64.AllWhereAllBitsSet(input1));
            Assert.False(Vector64.AllWhereAllBitsSet(input2));
            Assert.False(Vector64.AllWhereAllBitsSet(input1.WithElement(0, value2)));
            Assert.Equal(Vector64<T>.Count == 1, Vector64.AllWhereAllBitsSet(input2.WithElement(0, allBitsSet)));

            Assert.True(Vector64.AnyWhereAllBitsSet(input1));
            Assert.False(Vector64.AnyWhereAllBitsSet(input2));
            Assert.Equal(Vector64<T>.Count != 1, Vector64.AnyWhereAllBitsSet(input1.WithElement(0, value2)));
            Assert.True(Vector64.AnyWhereAllBitsSet(input2.WithElement(0, allBitsSet)));

            Assert.False(Vector64.NoneWhereAllBitsSet(input1));
            Assert.True(Vector64.NoneWhereAllBitsSet(input2));
            Assert.Equal(Vector64<T>.Count == 1, Vector64.NoneWhereAllBitsSet(input1.WithElement(0, value2)));
            Assert.False(Vector64.NoneWhereAllBitsSet(input2.WithElement(0, allBitsSet)));
        }

        [Fact]
        public void AllAnyNoneWhereAllBitsSetByteTest() => AllAnyNoneWhereAllBitsSetTest<byte>(byte.MaxValue, 2);

        [Fact]
        public void AllAnyNoneWhereAllBitsSetDoubleTest() => AllAnyNoneWhereAllBitsSetTest<double>(BitConverter.Int64BitsToDouble(-1), 2);

        [Fact]
        public void AllAnyNoneWhereAllBitsSetInt16Test() => AllAnyNoneWhereAllBitsSetTest<short>(-1, 2);

        [Fact]
        public void AllAnyNoneWhereAllBitsSetInt32Test() => AllAnyNoneWhereAllBitsSetTest<int>(-1, 2);

        [Fact]
        public void AllAnyNoneWhereAllBitsSetInt64Test() => AllAnyNoneWhereAllBitsSetTest<long>(-1, 2);

        [Fact]
        public void AllAnyNoneWhereAllBitsSetSByteTest() => AllAnyNoneWhereAllBitsSetTest<sbyte>(-1, 2);

        [Fact]
        public void AllAnyNoneWhereAllBitsSetSingleTest() => AllAnyNoneWhereAllBitsSetTest<float>(BitConverter.Int32BitsToSingle(-1), 2);

        [Fact]
        public void AllAnyNoneWhereAllBitsSetUInt16Test() => AllAnyNoneWhereAllBitsSetTest<ushort>(ushort.MaxValue, 2);

        [Fact]
        public void AllAnyNoneWhereAllBitsSetUInt32Test() => AllAnyNoneWhereAllBitsSetTest<uint>(uint.MaxValue, 2);

        [Fact]
        public void AllAnyNoneWhereAllBitsSetUInt64Test() => AllAnyNoneWhereAllBitsSetTest<ulong>(ulong.MaxValue, 2);

        [MethodImpl(MethodImplOptions.NoInlining)]
        private void CountIndexOfLastIndexOfTest<T>(T value1, T value2)
            where T : struct, INumber<T>
        {
            var input1 = Vector64.Create<T>(value1);
            var input2 = Vector64.Create<T>(value2);

            Assert.Equal(Vector64<T>.Count, Vector64.Count(input1, value1));
            Assert.Equal(Vector64<T>.Count, Vector64.Count(input2, value2));
            Assert.Equal(Vector64<T>.Count - 1, Vector64.Count(input1.WithElement(0, value2), value1));
            Assert.Equal(Vector64<T>.Count - 1, Vector64.Count(input2.WithElement(0, value1), value2));
            Assert.Equal(0, Vector64.Count(input1, value2));
            Assert.Equal(0, Vector64.Count(input2, value1));
            Assert.Equal(1, Vector64.Count(input1.WithElement(0, value2), value2));
            Assert.Equal(1, Vector64.Count(input2.WithElement(0, value1), value1));

            Assert.Equal(0, Vector64.IndexOf(input1, value1));
            Assert.Equal(0, Vector64.IndexOf(input2, value2));
            Assert.Equal((Vector64<T>.Count != 1) ? 1 : -1, Vector64.IndexOf(input1.WithElement(0, value2), value1));
            Assert.Equal((Vector64<T>.Count != 1) ? 1 : -1, Vector64.IndexOf(input2.WithElement(0, value1), value2));
            Assert.Equal(-1, Vector64.IndexOf(input1, value2));
            Assert.Equal(-1, Vector64.IndexOf(input2, value1));
            Assert.Equal(0, Vector64.IndexOf(input1.WithElement(0, value2), value2));
            Assert.Equal(0, Vector64.IndexOf(input2.WithElement(0, value1), value1));

            Assert.Equal(Vector64<T>.Count - 1, Vector64.LastIndexOf(input1, value1));
            Assert.Equal(Vector64<T>.Count - 1, Vector64.LastIndexOf(input2, value2));
            Assert.Equal((Vector64<T>.Count != 1) ? Vector64<T>.Count - 1 : -1, Vector64.LastIndexOf(input1.WithElement(0, value2), value1));
            Assert.Equal((Vector64<T>.Count != 1) ? Vector64<T>.Count - 1 : -1, Vector64.LastIndexOf(input2.WithElement(0, value1), value2));
            Assert.Equal(-1, Vector64.LastIndexOf(input1, value2));
            Assert.Equal(-1, Vector64.LastIndexOf(input2, value1));
            Assert.Equal(0, Vector64.LastIndexOf(input1.WithElement(0, value2), value2));
            Assert.Equal(0, Vector64.LastIndexOf(input2.WithElement(0, value1), value1));
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        private void CountIndexOfLastIndexOfTest_IFloatingPointIeee754<T>(T value)
            where T : struct, IFloatingPointIeee754<T>
        {
            var input = Vector64.Create<T>(value);

            Assert.Equal(0, Vector64.Count(input, value));
            Assert.Equal(-1, Vector64.IndexOf(input, value));
            Assert.Equal(-1, Vector64.LastIndexOf(input, value));
        }

        [Fact]
        public void CountIndexOfLastIndexOfByteTest() => CountIndexOfLastIndexOfTest<byte>(3, 2);

        [Fact]
        public void CountIndexOfLastIndexOfDoubleTest() => CountIndexOfLastIndexOfTest<double>(3, 2);

        [Fact]
        public void CountIndexOfLastIndexOfDoubleTest_AllBitsSet() => CountIndexOfLastIndexOfTest_IFloatingPointIeee754<double>(BitConverter.Int64BitsToDouble(-1));

        [Fact]
        public void CountIndexOfLastIndexOfInt16Test() => CountIndexOfLastIndexOfTest<short>(3, 2);

        [Fact]
        public void CountIndexOfLastIndexOfInt32Test() => CountIndexOfLastIndexOfTest<int>(3, 2);

        [Fact]
        public void CountIndexOfLastIndexOfInt64Test() => CountIndexOfLastIndexOfTest<long>(3, 2);

        [Fact]
        public void CountIndexOfLastIndexOfSByteTest() => CountIndexOfLastIndexOfTest<sbyte>(3, 2);

        [Fact]
        public void CountIndexOfLastIndexOfSingleTest() => CountIndexOfLastIndexOfTest<float>(3, 2);

        [Fact]
        public void CountIndexOfLastIndexOfSingleTest_AllBitsSet() => CountIndexOfLastIndexOfTest_IFloatingPointIeee754<float>(BitConverter.Int32BitsToSingle(-1));

        [Fact]
        public void CountIndexOfLastIndexOfUInt16Test() => CountIndexOfLastIndexOfTest<ushort>(3, 2);

        [Fact]
        public void CountIndexOfLastIndexOfUInt32Test() => CountIndexOfLastIndexOfTest<uint>(3, 2);

        [Fact]
        public void CountIndexOfLastIndexOfUInt64Test() => CountIndexOfLastIndexOfTest<ulong>(3, 2);

        [MethodImpl(MethodImplOptions.NoInlining)]
        private void CountIndexOfLastIndexOfWhereAllBitsSetTest<T>(T allBitsSet, T value2)
            where T : struct, INumber<T>
        {
            var input1 = Vector64.Create<T>(allBitsSet);
            var input2 = Vector64.Create<T>(value2);

            Assert.Equal(Vector64<T>.Count, Vector64.CountWhereAllBitsSet(input1));
            Assert.Equal(0, Vector64.CountWhereAllBitsSet(input2));
            Assert.Equal(Vector64<T>.Count - 1, Vector64.CountWhereAllBitsSet(input1.WithElement(0, value2)));
            Assert.Equal(1, Vector64.CountWhereAllBitsSet(input2.WithElement(0, allBitsSet)));

            Assert.Equal(0, Vector64.IndexOfWhereAllBitsSet(input1));
            Assert.Equal(-1, Vector64.IndexOfWhereAllBitsSet(input2));
            Assert.Equal((Vector64<T>.Count != 1) ? 1 : -1, Vector64.IndexOfWhereAllBitsSet(input1.WithElement(0, value2)));
            Assert.Equal(0, Vector64.IndexOfWhereAllBitsSet(input2.WithElement(0, allBitsSet)));

            Assert.Equal(Vector64<T>.Count - 1, Vector64.LastIndexOfWhereAllBitsSet(input1));
            Assert.Equal(-1, Vector64.LastIndexOfWhereAllBitsSet(input2));
            Assert.Equal((Vector64<T>.Count != 1) ? Vector64<T>.Count - 1 : -1, Vector64.LastIndexOfWhereAllBitsSet(input1.WithElement(0, value2)));
            Assert.Equal(0, Vector64.LastIndexOfWhereAllBitsSet(input2.WithElement(0, allBitsSet)));
        }

        [Fact]
        public void CountIndexOfLastIndexOfWhereAllBitsSetByteTest() => CountIndexOfLastIndexOfWhereAllBitsSetTest<byte>(byte.MaxValue, 2);

        [Fact]
        public void CountIndexOfLastIndexOfWhereAllBitsSetDoubleTest() => CountIndexOfLastIndexOfWhereAllBitsSetTest<double>(BitConverter.Int64BitsToDouble(-1), 2);

        [Fact]
        public void CountIndexOfLastIndexOfWhereAllBitsSetInt16Test() => CountIndexOfLastIndexOfWhereAllBitsSetTest<short>(-1, 2);

        [Fact]
        public void CountIndexOfLastIndexOfWhereAllBitsSetInt32Test() => CountIndexOfLastIndexOfWhereAllBitsSetTest<int>(-1, 2);

        [Fact]
        public void CountIndexOfLastIndexOfWhereAllBitsSetInt64Test() => CountIndexOfLastIndexOfWhereAllBitsSetTest<long>(-1, 2);

        [Fact]
        public void CountIndexOfLastIndexOfWhereAllBitsSetSByteTest() => CountIndexOfLastIndexOfWhereAllBitsSetTest<sbyte>(-1, 2);

        [Fact]
        public void CountIndexOfLastIndexOfWhereAllBitsSetSingleTest() => CountIndexOfLastIndexOfWhereAllBitsSetTest<float>(BitConverter.Int32BitsToSingle(-1), 2);

        [Fact]
        public void CountIndexOfLastIndexOfWhereAllBitsSetUInt16Test() => CountIndexOfLastIndexOfWhereAllBitsSetTest<ushort>(ushort.MaxValue, 2);

        [Fact]
        public void CountIndexOfLastIndexOfWhereAllBitsSetUInt32Test() => CountIndexOfLastIndexOfWhereAllBitsSetTest<uint>(uint.MaxValue, 2);

        [Fact]
        public void CountIndexOfLastIndexOfWhereAllBitsSetUInt64Test() => CountIndexOfLastIndexOfWhereAllBitsSetTest<ulong>(ulong.MaxValue, 2);

        [MethodImpl(MethodImplOptions.NoInlining)]
        private void AddSaturateToMaxTest<T>(T start)
            where T : struct, INumber<T>, IMinMaxValue<T>
        {
            // We just take it as a parameter to prevent constant folding
            Debug.Assert(start == T.One);

            Vector64<T> left = Vector64.CreateSequence<T>(start, T.One);
            Vector64<T> right = Vector64.Create<T>(T.MaxValue - T.CreateTruncating(Vector64<T>.Count) + T.One);

            Vector64<T> result = Vector64.AddSaturate(left, right);

            for (int i = 0; i < Vector64<T>.Count - 1; i++)
            {
                T expectedResult = left[i] + right[i];
                Assert.Equal(expectedResult, result[i]);
            }

            Assert.Equal(T.MaxValue, result[Vector64<T>.Count - 1]);
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        private void AddSaturateToMinTest<T>(T start)
            where T : struct, ISignedNumber<T>, IMinMaxValue<T>
        {
            // We just take it as a parameter to prevent constant folding
            Debug.Assert(start == T.NegativeOne);

            Vector64<T> left = Vector64.CreateSequence<T>(start, T.NegativeOne);
            Vector64<T> right = Vector64.Create<T>(T.MinValue + T.CreateTruncating(Vector64<T>.Count) - T.One);

            Vector64<T> result = Vector64.AddSaturate(left, right);

            for (int i = 0; i < Vector64<T>.Count - 1; i++)
            {
                T expectedResult = left[i] + right[i];
                Assert.Equal(expectedResult, result[i]);
            }

            Assert.Equal(T.MinValue, result[Vector64<T>.Count - 1]);
        }

        [Fact]
        public void AddSaturateByteTest() => AddSaturateToMaxTest<byte>(1);

        [Fact]
        public void AddSaturateInt16Test()
        {
            AddSaturateToMinTest<short>(-1);
            AddSaturateToMaxTest<short>(+1);
        }

        [Fact]
        public void AddSaturateInt32Test()
        {
            AddSaturateToMinTest<int>(-1);
            AddSaturateToMaxTest<int>(+1);
        }

        [Fact]
        public void AddSaturateInt64Test()
        {
            AddSaturateToMinTest<long>(-1);
            AddSaturateToMaxTest<long>(+1);
        }

        [Fact]
        public void AddSaturateIntPtrTest()
        {
            AddSaturateToMinTest<nint>(-1);
            AddSaturateToMaxTest<nint>(+1);
        }

        [Fact]
        public void AddSaturateSByteTest()
        {
            AddSaturateToMinTest<sbyte>(-1);
            AddSaturateToMaxTest<sbyte>(+1);
        }

        [Fact]
        public void AddSaturateUInt16Test() => AddSaturateToMaxTest<ushort>(1);

        [Fact]
        public void AddSaturateUInt32Test() => AddSaturateToMaxTest<uint>(1);

        [Fact]
        public void AddSaturateUInt64Test() => AddSaturateToMaxTest<ulong>(1);

        [Fact]
        public void AddSaturateUIntPtrTest() => AddSaturateToMaxTest<nuint>(1);

        private (Vector64<TFrom> lower, Vector64<TFrom> upper) GetNarrowWithSaturationInputs<TFrom, TTo>()
            where TFrom : unmanaged, IMinMaxValue<TFrom>, INumber<TFrom>
            where TTo : unmanaged, IMinMaxValue<TTo>, INumber<TTo>
        {
            Vector64<TFrom> lower = Vector64.Create<TFrom>(TFrom.CreateTruncating(TTo.MaxValue) - TFrom.CreateTruncating(Vector64<TFrom>.Count) + TFrom.One)
                                  + Vector64.CreateSequence<TFrom>(TFrom.One, TFrom.One);

            Vector64<TFrom> upper = Vector64.Create<TFrom>(TFrom.CreateTruncating(TTo.MinValue) + TFrom.CreateTruncating(Vector64<TFrom>.Count) - TFrom.One)
                                  - Vector64.CreateSequence<TFrom>(TFrom.One, TFrom.One);

            return (lower, upper);
        }

        private void NarrowWithSaturationTest<TFrom, TTo>(Vector64<TFrom> lower, Vector64<TFrom> upper, Vector64<TTo> result)
            where TFrom : unmanaged, INumber<TFrom>
            where TTo : unmanaged, INumber<TTo>
        {
            for(int i = 0; i < Vector64<TFrom>.Count; i++)
            {
                TTo expectedResult = TTo.CreateSaturating(lower[i]);
                Assert.Equal(expectedResult, result[i]);
            }

            for (int i = 0; i < Vector64<TFrom>.Count; i++)
            {
                TTo expectedResult = TTo.CreateSaturating(upper[i]);
                Assert.Equal(expectedResult, result[Vector64<TFrom>.Count + i]);
            }
        }

        [Fact]
        public void NarrowWithSaturationInt16Test()
        {
            (Vector64<short> lower, Vector64<short> upper) = GetNarrowWithSaturationInputs<short, sbyte>();
            Vector64<sbyte> result = Vector64.NarrowWithSaturation(lower, upper);
            NarrowWithSaturationTest(lower, upper, result);
        }

        [Fact]
        public void NarrowWithSaturationInt32Test()
        {
            (Vector64<int> lower, Vector64<int> upper) = GetNarrowWithSaturationInputs<int, short>();
            Vector64<short> result = Vector64.NarrowWithSaturation(lower, upper);
            NarrowWithSaturationTest(lower, upper, result);
        }

        [Fact]
        public void NarrowWithSaturationInt64Test()
        {
            (Vector64<long> lower, Vector64<long> upper) = GetNarrowWithSaturationInputs<long, int>();
            Vector64<int> result = Vector64.NarrowWithSaturation(lower, upper);
            NarrowWithSaturationTest(lower, upper, result);
        }

        [Fact]
        public void NarrowWithSaturationUInt16Test()
        {
            (Vector64<ushort> lower, Vector64<ushort> upper) = GetNarrowWithSaturationInputs<ushort, byte>();
            Vector64<byte> result = Vector64.NarrowWithSaturation(lower, upper);
            NarrowWithSaturationTest(lower, upper, result);
        }

        [Fact]
        public void NarrowWithSaturationUInt32Test()
        {
            (Vector64<uint> lower, Vector64<uint> upper) = GetNarrowWithSaturationInputs<uint, ushort>();
            Vector64<ushort> result = Vector64.NarrowWithSaturation(lower, upper);
            NarrowWithSaturationTest(lower, upper, result);
        }

        [Fact]
        public void NarrowWithSaturationUInt64Test()
        {
            (Vector64<ulong> lower, Vector64<ulong> upper) = GetNarrowWithSaturationInputs<ulong, uint>();
            Vector64<uint> result = Vector64.NarrowWithSaturation(lower, upper);
            NarrowWithSaturationTest(lower, upper, result);
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        private void SubtractSaturateToMaxTest<T>(T start)
            where T : struct, ISignedNumber<T>, IMinMaxValue<T>
        {
            // We just take it as a parameter to prevent constant folding
            Debug.Assert(start == T.NegativeOne);

            Vector64<T> left = Vector64.Create<T>(T.MaxValue - T.CreateTruncating(Vector64<T>.Count) + T.One);
            Vector64<T> right = Vector64.CreateSequence<T>(start, T.NegativeOne);

            Vector64<T> result = Vector64.SubtractSaturate(left, right);

            for (int i = 0; i < Vector64<T>.Count - 1; i++)
            {
                T expectedResult = left[i] - right[i];
                Assert.Equal(expectedResult, result[i]);
            }

            Assert.Equal(T.MaxValue, result[Vector64<T>.Count - 1]);
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        private void SubtractSaturateToMinTest<T>(T start)
            where T : struct, INumber<T>, IMinMaxValue<T>
        {
            // We just take it as a parameter to prevent constant folding
            Debug.Assert(start == T.One);

            Vector64<T> left = Vector64.Create<T>(T.MinValue + T.CreateTruncating(Vector64<T>.Count) - T.One);
            Vector64<T> right = Vector64.CreateSequence<T>(start, T.One);

            Vector64<T> result = Vector64.SubtractSaturate(left, right);

            for (int i = 0; i < Vector64<T>.Count - 1; i++)
            {
                T expectedResult = left[i] - right[i];
                Assert.Equal(expectedResult, result[i]);
            }

            Assert.Equal(T.MinValue, result[Vector64<T>.Count - 1]);
        }

        [Fact]
        public void SubtractSaturateByteTest() => SubtractSaturateToMinTest<byte>(1);

        [Fact]
        public void SubtractSaturateInt16Test()
        {
            SubtractSaturateToMinTest<short>(+1);
            SubtractSaturateToMaxTest<short>(-1);
        }

        [Fact]
        public void SubtractSaturateInt32Test()
        {
            SubtractSaturateToMinTest<int>(+1);
            SubtractSaturateToMaxTest<int>(-1);
        }

        [Fact]
        public void SubtractSaturateInt64Test()
        {
            SubtractSaturateToMinTest<long>(+1);
            SubtractSaturateToMaxTest<long>(-1);
        }

        [Fact]
        public void SubtractSaturateIntPtrTest()
        {
            SubtractSaturateToMinTest<nint>(+1);
            SubtractSaturateToMaxTest<nint>(-1);
        }

        [Fact]
        public void SubtractSaturateSByteTest()
        {
            SubtractSaturateToMinTest<sbyte>(+1);
            SubtractSaturateToMaxTest<sbyte>(-1);
        }

        [Fact]
        public void SubtractSaturateUInt16Test() => SubtractSaturateToMinTest<ushort>(1);

        [Fact]
        public void SubtractSaturateUInt32Test() => SubtractSaturateToMinTest<uint>(1);

        [Fact]
        public void SubtractSaturateUInt64Test() => SubtractSaturateToMinTest<ulong>(1);

        [Fact]
        public void SubtractSaturateUIntPtrTest() => SubtractSaturateToMinTest<nuint>(1);
    }
}
