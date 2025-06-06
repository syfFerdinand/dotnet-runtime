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
    public sealed class Vector256Tests
    {
        /// <summary>Verifies that two <see cref="Vector256{Single}" /> values are equal, within the <paramref name="variance" />.</summary>
        /// <param name="expected">The expected value</param>
        /// <param name="actual">The value to be compared against</param>
        /// <param name="variance">The total variance allowed between the expected and actual results.</param>
        /// <exception cref="EqualException">Thrown when the values are not equal</exception>
        internal static void AssertEqual(Vector256<float> expected, Vector256<float> actual, Vector256<float> variance)
        {
            Vector128Tests.AssertEqual(expected.GetLower(), actual.GetLower(), variance.GetLower());
            Vector128Tests.AssertEqual(expected.GetUpper(), actual.GetUpper(), variance.GetUpper());
        }

        /// <summary>Verifies that two <see cref="Vector256{Double}" /> values are equal, within the <paramref name="variance" />.</summary>
        /// <param name="expected">The expected value</param>
        /// <param name="actual">The value to be compared against</param>
        /// <param name="variance">The total variance allowed between the expected and actual results.</param>
        /// <exception cref="EqualException">Thrown when the values are not equal</exception>
        internal static void AssertEqual(Vector256<double> expected, Vector256<double> actual, Vector256<double> variance)
        {
            Vector128Tests.AssertEqual(expected.GetLower(), actual.GetLower(), variance.GetLower());
            Vector128Tests.AssertEqual(expected.GetUpper(), actual.GetUpper(), variance.GetUpper());
        }

        [Fact]
        public unsafe void Vector256IsHardwareAcceleratedTest()
        {
            MethodInfo methodInfo = typeof(Vector256).GetMethod("get_IsHardwareAccelerated");
            Assert.Equal(Vector256.IsHardwareAccelerated, methodInfo.Invoke(null, null));
        }

        [Fact]
        public unsafe void Vector256ByteExtractMostSignificantBitsTest()
        {
            Vector256<byte> vector = Vector256.Create(
                0x01,
                0x80,
                0x01,
                0x80,
                0x01,
                0x80,
                0x01,
                0x80,
                0x01,
                0x80,
                0x01,
                0x80,
                0x01,
                0x80,
                0x01,
                0x80,
                0x01,
                0x80,
                0x01,
                0x80,
                0x01,
                0x80,
                0x01,
                0x80,
                0x01,
                0x80,
                0x01,
                0x80,
                0x01,
                0x80,
                0x01,
                0x80
            );

            uint result = Vector256.ExtractMostSignificantBits(vector);
            Assert.Equal(0b10101010_10101010_10101010_10101010u, result);
        }

        [Fact]
        public unsafe void Vector256DoubleExtractMostSignificantBitsTest()
        {
            Vector256<double> vector = Vector256.Create(
                +1.0,
                -0.0,
                +1.0,
                -0.0
            );

            uint result = Vector256.ExtractMostSignificantBits(vector);
            Assert.Equal(0b1010u, result);
        }

        [Fact]
        public unsafe void Vector256Int16ExtractMostSignificantBitsTest()
        {
            Vector256<short> vector = Vector256.Create(
                0x0001,
                0x8000,
                0x0001,
                0x8000,
                0x0001,
                0x8000,
                0x0001,
                0x8000,
                0x0001,
                0x8000,
                0x0001,
                0x8000,
                0x0001,
                0x8000,
                0x0001,
                0x8000
            ).AsInt16();

            uint result = Vector256.ExtractMostSignificantBits(vector);
            Assert.Equal(0b10101010_10101010u, result);
        }

        [Fact]
        public unsafe void Vector256Int32ExtractMostSignificantBitsTest()
        {
            Vector256<int> vector = Vector256.Create(
                0x00000001U,
                0x80000000U,
                0x00000001U,
                0x80000000U,
                0x00000001U,
                0x80000000U,
                0x00000001U,
                0x80000000U
            ).AsInt32();

            uint result = Vector256.ExtractMostSignificantBits(vector);
            Assert.Equal(0b10101010u, result);
        }

        [Fact]
        public unsafe void Vector256Int64ExtractMostSignificantBitsTest()
        {
            Vector256<long> vector = Vector256.Create(
                0x0000000000000001UL,
                0x8000000000000000UL,
                0x0000000000000001UL,
                0x8000000000000000UL
            ).AsInt64();

            uint result = Vector256.ExtractMostSignificantBits(vector);
            Assert.Equal(0b1010u, result);
        }

        [Fact]
        public unsafe void Vector256NIntExtractMostSignificantBitsTest()
        {
            if (Environment.Is64BitProcess)
            {
                Vector256<nint> vector = Vector256.Create(
                    0x0000000000000001UL,
                    0x8000000000000000UL,
                    0x0000000000000001UL,
                    0x8000000000000000UL
                ).AsNInt();

                uint result = Vector256.ExtractMostSignificantBits(vector);
                Assert.Equal(0b1010u, result);
            }
            else
            {
                Vector256<nint> vector = Vector256.Create(
                    0x00000001U,
                    0x80000000U,
                    0x00000001U,
                    0x80000000U,
                    0x00000001U,
                    0x80000000U,
                    0x00000001U,
                    0x80000000U
                ).AsNInt();

                uint result = Vector256.ExtractMostSignificantBits(vector);
                Assert.Equal(0b10101010u, result);
            }
        }

        [Fact]
        public unsafe void Vector256NUIntExtractMostSignificantBitsTest()
        {
            if (Environment.Is64BitProcess)
            {
                Vector256<nuint> vector = Vector256.Create(
                    0x0000000000000001UL,
                    0x8000000000000000UL,
                    0x0000000000000001UL,
                    0x8000000000000000UL
                ).AsNUInt();

                uint result = Vector256.ExtractMostSignificantBits(vector);
                Assert.Equal(0b1010u, result);
            }
            else
            {
                Vector256<nuint> vector = Vector256.Create(
                    0x00000001U,
                    0x80000000U,
                    0x00000001U,
                    0x80000000U,
                    0x00000001U,
                    0x80000000U,
                    0x00000001U,
                    0x80000000U
                ).AsNUInt();

                uint result = Vector256.ExtractMostSignificantBits(vector);
                Assert.Equal(0b10101010u, result);
            }
        }

        [Fact]
        public unsafe void Vector256SByteExtractMostSignificantBitsTest()
        {
            Vector256<sbyte> vector = Vector256.Create(
                0x01,
                0x80,
                0x01,
                0x80,
                0x01,
                0x80,
                0x01,
                0x80,
                0x01,
                0x80,
                0x01,
                0x80,
                0x01,
                0x80,
                0x01,
                0x80,
                0x01,
                0x80,
                0x01,
                0x80,
                0x01,
                0x80,
                0x01,
                0x80,
                0x01,
                0x80,
                0x01,
                0x80,
                0x01,
                0x80,
                0x01,
                0x80
            ).AsSByte();

            uint result = Vector256.ExtractMostSignificantBits(vector);
            Assert.Equal(0b10101010_10101010_10101010_10101010u, result);
        }

        [Fact]
        public unsafe void Vector256SingleExtractMostSignificantBitsTest()
        {
            Vector256<float> vector = Vector256.Create(
                +1.0f,
                -0.0f,
                +1.0f,
                -0.0f,
                +1.0f,
                -0.0f,
                +1.0f,
                -0.0f
            );

            uint result = Vector256.ExtractMostSignificantBits(vector);
            Assert.Equal(0b10101010u, result);
        }

        [Fact]
        public unsafe void Vector256UInt16ExtractMostSignificantBitsTest()
        {
            Vector256<ushort> vector = Vector256.Create(
                0x0001,
                0x8000,
                0x0001,
                0x8000,
                0x0001,
                0x8000,
                0x0001,
                0x8000,
                0x0001,
                0x8000,
                0x0001,
                0x8000,
                0x0001,
                0x8000,
                0x0001,
                0x8000
            );

            uint result = Vector256.ExtractMostSignificantBits(vector);
            Assert.Equal(0b10101010_10101010u, result);
        }

        [Fact]
        public unsafe void Vector256UInt32ExtractMostSignificantBitsTest()
        {
            Vector256<uint> vector = Vector256.Create(
                0x00000001U,
                0x80000000U,
                0x00000001U,
                0x80000000U,
                0x00000001U,
                0x80000000U,
                0x00000001U,
                0x80000000U
            );

            uint result = Vector256.ExtractMostSignificantBits(vector);
            Assert.Equal(0b10101010u, result);
        }

        [Fact]
        public unsafe void Vector256UInt64ExtractMostSignificantBitsTest()
        {
            Vector256<ulong> vector = Vector256.Create(
                0x0000000000000001UL,
                0x8000000000000000UL,
                0x0000000000000001UL,
                0x8000000000000000UL
            );

            uint result = Vector256.ExtractMostSignificantBits(vector);
            Assert.Equal(0b1010u, result);
        }

        [Fact]
        public unsafe void Vector256ByteLoadTest()
        {
            byte* value = stackalloc byte[32] {
                0,
                1,
                2,
                3,
                4,
                5,
                6,
                7,
                8,
                9,
                10,
                11,
                12,
                13,
                14,
                15,
                16,
                17,
                18,
                19,
                20,
                21,
                22,
                23,
                24,
                25,
                26,
                27,
                28,
                29,
                30,
                31,
            };

            Vector256<byte> vector = Vector256.Load(value);

            for (int index = 0; index < Vector256<byte>.Count; index++)
            {
                Assert.Equal((byte)index, vector.GetElement(index));
            }
        }

        [Fact]
        public unsafe void Vector256DoubleLoadTest()
        {
            double* value = stackalloc double[4] {
                0,
                1,
                2,
                3,
            };

            Vector256<double> vector = Vector256.Load(value);

            for (int index = 0; index < Vector256<double>.Count; index++)
            {
                Assert.Equal((double)index, vector.GetElement(index));
            }
        }

        [Fact]
        public unsafe void Vector256Int16LoadTest()
        {
            short* value = stackalloc short[16] {
                0,
                1,
                2,
                3,
                4,
                5,
                6,
                7,
                8,
                9,
                10,
                11,
                12,
                13,
                14,
                15,
            };

            Vector256<short> vector = Vector256.Load(value);

            for (int index = 0; index < Vector256<short>.Count; index++)
            {
                Assert.Equal((short)index, vector.GetElement(index));
            }
        }

        [Fact]
        public unsafe void Vector256Int32LoadTest()
        {
            int* value = stackalloc int[8] {
                0,
                1,
                2,
                3,
                4,
                5,
                6,
                7,
            };

            Vector256<int> vector = Vector256.Load(value);

            for (int index = 0; index < Vector256<int>.Count; index++)
            {
                Assert.Equal((int)index, vector.GetElement(index));
            }
        }

        [Fact]
        public unsafe void Vector256Int64LoadTest()
        {
            long* value = stackalloc long[4] {
                0,
                1,
                2,
                3,
            };

            Vector256<long> vector = Vector256.Load(value);

            for (int index = 0; index < Vector256<long>.Count; index++)
            {
                Assert.Equal((long)index, vector.GetElement(index));
            }
        }

        [Fact]
        public unsafe void Vector256NIntLoadTest()
        {
            if (Environment.Is64BitProcess)
            {
                nint* value = stackalloc nint[4] {
                    0,
                    1,
                    2,
                    3,
                };

                Vector256<nint> vector = Vector256.Load(value);

                for (int index = 0; index < Vector256<nint>.Count; index++)
                {
                    Assert.Equal((nint)index, vector.GetElement(index));
                }
            }
            else
            {
                nint* value = stackalloc nint[8] {
                    0,
                    1,
                    2,
                    3,
                    4,
                    5,
                    6,
                    7,
                };

                Vector256<nint> vector = Vector256.Load(value);

                for (int index = 0; index < Vector256<nint>.Count; index++)
                {
                    Assert.Equal((nint)index, vector.GetElement(index));
                }
            }
        }

        [Fact]
        public unsafe void Vector256NUIntLoadTest()
        {
            if (Environment.Is64BitProcess)
            {
                nuint* value = stackalloc nuint[4] {
                    0,
                    1,
                    2,
                    3,
                };

                Vector256<nuint> vector = Vector256.Load(value);

                for (int index = 0; index < Vector256<nuint>.Count; index++)
                {
                    Assert.Equal((nuint)index, vector.GetElement(index));
                }
            }
            else
            {
                nuint* value = stackalloc nuint[8] {
                    0,
                    1,
                    2,
                    3,
                    4,
                    5,
                    6,
                    7,
                };

                Vector256<nuint> vector = Vector256.Load(value);

                for (int index = 0; index < Vector256<nuint>.Count; index++)
                {
                    Assert.Equal((nuint)index, vector.GetElement(index));
                }
            }
        }

        [Fact]
        public unsafe void Vector256SByteLoadTest()
        {
            sbyte* value = stackalloc sbyte[32] {
                0,
                1,
                2,
                3,
                4,
                5,
                6,
                7,
                8,
                9,
                10,
                11,
                12,
                13,
                14,
                15,
                16,
                17,
                18,
                19,
                20,
                21,
                22,
                23,
                24,
                25,
                26,
                27,
                28,
                29,
                30,
                31,
            };

            Vector256<sbyte> vector = Vector256.Load(value);

            for (int index = 0; index < Vector256<sbyte>.Count; index++)
            {
                Assert.Equal((sbyte)index, vector.GetElement(index));
            }
        }

        [Fact]
        public unsafe void Vector256SingleLoadTest()
        {
            float* value = stackalloc float[8] {
                0,
                1,
                2,
                3,
                4,
                5,
                6,
                7,
            };

            Vector256<float> vector = Vector256.Load(value);

            for (int index = 0; index < Vector256<float>.Count; index++)
            {
                Assert.Equal((float)index, vector.GetElement(index));
            }
        }

        [Fact]
        public unsafe void Vector256UInt16LoadTest()
        {
            ushort* value = stackalloc ushort[16] {
                0,
                1,
                2,
                3,
                4,
                5,
                6,
                7,
                8,
                9,
                10,
                11,
                12,
                13,
                14,
                15,
            };

            Vector256<ushort> vector = Vector256.Load(value);

            for (int index = 0; index < Vector256<ushort>.Count; index++)
            {
                Assert.Equal((ushort)index, vector.GetElement(index));
            }
        }

        [Fact]
        public unsafe void Vector256UInt32LoadTest()
        {
            uint* value = stackalloc uint[8] {
                0,
                1,
                2,
                3,
                4,
                5,
                6,
                7,
            };

            Vector256<uint> vector = Vector256.Load(value);

            for (int index = 0; index < Vector256<uint>.Count; index++)
            {
                Assert.Equal((uint)index, vector.GetElement(index));
            }
        }

        [Fact]
        public unsafe void Vector256UInt64LoadTest()
        {
            ulong* value = stackalloc ulong[4] {
                0,
                1,
                2,
                3,
            };

            Vector256<ulong> vector = Vector256.Load(value);

            for (int index = 0; index < Vector256<ulong>.Count; index++)
            {
                Assert.Equal((ulong)index, vector.GetElement(index));
            }
        }

        [Fact]
        public unsafe void Vector256ByteLoadAlignedTest()
        {
            byte* value = null;

            try
            {
                value = (byte*)NativeMemory.AlignedAlloc(byteCount: 32, alignment: 32);

                value[0] = 0;
                value[1] = 1;
                value[2] = 2;
                value[3] = 3;
                value[4] = 4;
                value[5] = 5;
                value[6] = 6;
                value[7] = 7;
                value[8] = 8;
                value[9] = 9;
                value[10] = 10;
                value[11] = 11;
                value[12] = 12;
                value[13] = 13;
                value[14] = 14;
                value[15] = 15;
                value[16] = 16;
                value[17] = 17;
                value[18] = 18;
                value[19] = 19;
                value[20] = 20;
                value[21] = 21;
                value[22] = 22;
                value[23] = 23;
                value[24] = 24;
                value[25] = 25;
                value[26] = 26;
                value[27] = 27;
                value[28] = 28;
                value[29] = 29;
                value[30] = 30;
                value[31] = 31;

                Vector256<byte> vector = Vector256.LoadAligned(value);

                for (int index = 0; index < Vector256<byte>.Count; index++)
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
        public unsafe void Vector256DoubleLoadAlignedTest()
        {
            double* value = null;

            try
            {
                value = (double*)NativeMemory.AlignedAlloc(byteCount: 32, alignment: 32);

                value[0] = 0;
                value[1] = 1;
                value[2] = 2;
                value[3] = 3;

                Vector256<double> vector = Vector256.LoadAligned(value);

                for (int index = 0; index < Vector256<double>.Count; index++)
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
        public unsafe void Vector256Int16LoadAlignedTest()
        {
            short* value = null;

            try
            {
                value = (short*)NativeMemory.AlignedAlloc(byteCount: 32, alignment: 32);

                value[0] = 0;
                value[1] = 1;
                value[2] = 2;
                value[3] = 3;
                value[4] = 4;
                value[5] = 5;
                value[6] = 6;
                value[7] = 7;
                value[8] = 8;
                value[9] = 9;
                value[10] = 10;
                value[11] = 11;
                value[12] = 12;
                value[13] = 13;
                value[14] = 14;
                value[15] = 15;

                Vector256<short> vector = Vector256.LoadAligned(value);

                for (int index = 0; index < Vector256<short>.Count; index++)
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
        public unsafe void Vector256Int32LoadAlignedTest()
        {
            int* value = null;

            try
            {
                value = (int*)NativeMemory.AlignedAlloc(byteCount: 32, alignment: 32);

                value[0] = 0;
                value[1] = 1;
                value[2] = 2;
                value[3] = 3;
                value[4] = 4;
                value[5] = 5;
                value[6] = 6;
                value[7] = 7;

                Vector256<int> vector = Vector256.LoadAligned(value);

                for (int index = 0; index < Vector256<int>.Count; index++)
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
        public unsafe void Vector256Int64LoadAlignedTest()
        {
            long* value = null;

            try
            {
                value = (long*)NativeMemory.AlignedAlloc(byteCount: 32, alignment: 32);

                value[0] = 0;
                value[1] = 1;
                value[2] = 2;
                value[3] = 3;

                Vector256<long> vector = Vector256.LoadAligned(value);

                for (int index = 0; index < Vector256<long>.Count; index++)
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
        public unsafe void Vector256NIntLoadAlignedTest()
        {
            nint* value = null;

            try
            {
                value = (nint*)NativeMemory.AlignedAlloc(byteCount: 32, alignment: 32);

                if (Environment.Is64BitProcess)
                {
                    value[0] = 0;
                    value[1] = 1;
                    value[2] = 2;
                    value[3] = 3;
                }
                else
                {
                    value[0] = 0;
                    value[1] = 1;
                    value[2] = 2;
                    value[3] = 3;
                    value[4] = 4;
                    value[5] = 5;
                    value[6] = 6;
                    value[7] = 7;
                }

                Vector256<nint> vector = Vector256.LoadAligned(value);

                for (int index = 0; index < Vector256<nint>.Count; index++)
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
        public unsafe void Vector256NUIntLoadAlignedTest()
        {
            nuint* value = null;

            try
            {
                value = (nuint*)NativeMemory.AlignedAlloc(byteCount: 32, alignment: 32);

                if (Environment.Is64BitProcess)
                {
                    value[0] = 0;
                    value[1] = 1;
                    value[2] = 2;
                    value[3] = 3;
                }
                else
                {
                    value[0] = 0;
                    value[1] = 1;
                    value[2] = 2;
                    value[3] = 3;
                    value[4] = 4;
                    value[5] = 5;
                    value[6] = 6;
                    value[7] = 7;
                }

                Vector256<nuint> vector = Vector256.LoadAligned(value);

                for (int index = 0; index < Vector256<nuint>.Count; index++)
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
        public unsafe void Vector256SByteLoadAlignedTest()
        {
            sbyte* value = null;

            try
            {
                value = (sbyte*)NativeMemory.AlignedAlloc(byteCount: 32, alignment: 32);

                value[0] = 0;
                value[1] = 1;
                value[2] = 2;
                value[3] = 3;
                value[4] = 4;
                value[5] = 5;
                value[6] = 6;
                value[7] = 7;
                value[8] = 8;
                value[9] = 9;
                value[10] = 10;
                value[11] = 11;
                value[12] = 12;
                value[13] = 13;
                value[14] = 14;
                value[15] = 15;
                value[16] = 16;
                value[17] = 17;
                value[18] = 18;
                value[19] = 19;
                value[20] = 20;
                value[21] = 21;
                value[22] = 22;
                value[23] = 23;
                value[24] = 24;
                value[25] = 25;
                value[26] = 26;
                value[27] = 27;
                value[28] = 28;
                value[29] = 29;
                value[30] = 30;
                value[31] = 31;

                Vector256<sbyte> vector = Vector256.LoadAligned(value);

                for (int index = 0; index < Vector256<sbyte>.Count; index++)
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
        public unsafe void Vector256SingleLoadAlignedTest()
        {
            float* value = null;

            try
            {
                value = (float*)NativeMemory.AlignedAlloc(byteCount: 32, alignment: 32);

                value[0] = 0;
                value[1] = 1;
                value[2] = 2;
                value[3] = 3;
                value[4] = 4;
                value[5] = 5;
                value[6] = 6;
                value[7] = 7;

                Vector256<float> vector = Vector256.LoadAligned(value);

                for (int index = 0; index < Vector256<float>.Count; index++)
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
        public unsafe void Vector256UInt16LoadAlignedTest()
        {
            ushort* value = null;

            try
            {
                value = (ushort*)NativeMemory.AlignedAlloc(byteCount: 32, alignment: 32);

                value[0] = 0;
                value[1] = 1;
                value[2] = 2;
                value[3] = 3;
                value[4] = 4;
                value[5] = 5;
                value[6] = 6;
                value[7] = 7;
                value[8] = 8;
                value[9] = 9;
                value[10] = 10;
                value[11] = 11;
                value[12] = 12;
                value[13] = 13;
                value[14] = 14;
                value[15] = 15;

                Vector256<ushort> vector = Vector256.LoadAligned(value);

                for (int index = 0; index < Vector256<ushort>.Count; index++)
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
        public unsafe void Vector256UInt32LoadAlignedTest()
        {
            uint* value = null;

            try
            {
                value = (uint*)NativeMemory.AlignedAlloc(byteCount: 32, alignment: 32);

                value[0] = 0;
                value[1] = 1;
                value[2] = 2;
                value[3] = 3;
                value[4] = 4;
                value[5] = 5;
                value[6] = 6;
                value[7] = 7;

                Vector256<uint> vector = Vector256.LoadAligned(value);

                for (int index = 0; index < Vector256<uint>.Count; index++)
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
        public unsafe void Vector256UInt64LoadAlignedTest()
        {
            ulong* value = null;

            try
            {
                value = (ulong*)NativeMemory.AlignedAlloc(byteCount: 32, alignment: 32);

                value[0] = 0;
                value[1] = 1;
                value[2] = 2;
                value[3] = 3;

                Vector256<ulong> vector = Vector256.LoadAligned(value);

                for (int index = 0; index < Vector256<ulong>.Count; index++)
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
        public unsafe void Vector256ByteLoadAlignedNonTemporalTest()
        {
            byte* value = null;

            try
            {
                value = (byte*)NativeMemory.AlignedAlloc(byteCount: 32, alignment: 32);

                value[0] = 0;
                value[1] = 1;
                value[2] = 2;
                value[3] = 3;
                value[4] = 4;
                value[5] = 5;
                value[6] = 6;
                value[7] = 7;
                value[8] = 8;
                value[9] = 9;
                value[10] = 10;
                value[11] = 11;
                value[12] = 12;
                value[13] = 13;
                value[14] = 14;
                value[15] = 15;
                value[16] = 16;
                value[17] = 17;
                value[18] = 18;
                value[19] = 19;
                value[20] = 20;
                value[21] = 21;
                value[22] = 22;
                value[23] = 23;
                value[24] = 24;
                value[25] = 25;
                value[26] = 26;
                value[27] = 27;
                value[28] = 28;
                value[29] = 29;
                value[30] = 30;
                value[31] = 31;

                Vector256<byte> vector = Vector256.LoadAlignedNonTemporal(value);

                for (int index = 0; index < Vector256<byte>.Count; index++)
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
        public unsafe void Vector256DoubleLoadAlignedNonTemporalTest()
        {
            double* value = null;

            try
            {
                value = (double*)NativeMemory.AlignedAlloc(byteCount: 32, alignment: 32);

                value[0] = 0;
                value[1] = 1;
                value[2] = 2;
                value[3] = 3;

                Vector256<double> vector = Vector256.LoadAlignedNonTemporal(value);

                for (int index = 0; index < Vector256<double>.Count; index++)
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
        public unsafe void Vector256Int16LoadAlignedNonTemporalTest()
        {
            short* value = null;

            try
            {
                value = (short*)NativeMemory.AlignedAlloc(byteCount: 32, alignment: 32);

                value[0] = 0;
                value[1] = 1;
                value[2] = 2;
                value[3] = 3;
                value[4] = 4;
                value[5] = 5;
                value[6] = 6;
                value[7] = 7;
                value[8] = 8;
                value[9] = 9;
                value[10] = 10;
                value[11] = 11;
                value[12] = 12;
                value[13] = 13;
                value[14] = 14;
                value[15] = 15;

                Vector256<short> vector = Vector256.LoadAlignedNonTemporal(value);

                for (int index = 0; index < Vector256<short>.Count; index++)
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
        public unsafe void Vector256Int32LoadAlignedNonTemporalTest()
        {
            int* value = null;

            try
            {
                value = (int*)NativeMemory.AlignedAlloc(byteCount: 32, alignment: 32);

                value[0] = 0;
                value[1] = 1;
                value[2] = 2;
                value[3] = 3;
                value[4] = 4;
                value[5] = 5;
                value[6] = 6;
                value[7] = 7;

                Vector256<int> vector = Vector256.LoadAlignedNonTemporal(value);

                for (int index = 0; index < Vector256<int>.Count; index++)
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
        public unsafe void Vector256Int64LoadAlignedNonTemporalTest()
        {
            long* value = null;

            try
            {
                value = (long*)NativeMemory.AlignedAlloc(byteCount: 32, alignment: 32);

                value[0] = 0;
                value[1] = 1;
                value[2] = 2;
                value[3] = 3;

                Vector256<long> vector = Vector256.LoadAlignedNonTemporal(value);

                for (int index = 0; index < Vector256<long>.Count; index++)
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
        public unsafe void Vector256NIntLoadAlignedNonTemporalTest()
        {
            nint* value = null;

            try
            {
                value = (nint*)NativeMemory.AlignedAlloc(byteCount: 32, alignment: 32);

                if (Environment.Is64BitProcess)
                {
                    value[0] = 0;
                    value[1] = 1;
                    value[2] = 2;
                    value[3] = 3;
                }
                else
                {
                    value[0] = 0;
                    value[1] = 1;
                    value[2] = 2;
                    value[3] = 3;
                    value[4] = 4;
                    value[5] = 5;
                    value[6] = 6;
                    value[7] = 7;
                }

                Vector256<nint> vector = Vector256.LoadAlignedNonTemporal(value);

                for (int index = 0; index < Vector256<nint>.Count; index++)
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
        public unsafe void Vector256NUIntLoadAlignedNonTemporalTest()
        {
            nuint* value = null;

            try
            {
                value = (nuint*)NativeMemory.AlignedAlloc(byteCount: 32, alignment: 32);

                if (Environment.Is64BitProcess)
                {
                    value[0] = 0;
                    value[1] = 1;
                    value[2] = 2;
                    value[3] = 3;
                }
                else
                {
                    value[0] = 0;
                    value[1] = 1;
                    value[2] = 2;
                    value[3] = 3;
                    value[4] = 4;
                    value[5] = 5;
                    value[6] = 6;
                    value[7] = 7;
                }

                Vector256<nuint> vector = Vector256.LoadAlignedNonTemporal(value);

                for (int index = 0; index < Vector256<nuint>.Count; index++)
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
        public unsafe void Vector256SByteLoadAlignedNonTemporalTest()
        {
            sbyte* value = null;

            try
            {
                value = (sbyte*)NativeMemory.AlignedAlloc(byteCount: 32, alignment: 32);

                value[0] = 0;
                value[1] = 1;
                value[2] = 2;
                value[3] = 3;
                value[4] = 4;
                value[5] = 5;
                value[6] = 6;
                value[7] = 7;
                value[8] = 8;
                value[9] = 9;
                value[10] = 10;
                value[11] = 11;
                value[12] = 12;
                value[13] = 13;
                value[14] = 14;
                value[15] = 15;
                value[16] = 16;
                value[17] = 17;
                value[18] = 18;
                value[19] = 19;
                value[20] = 20;
                value[21] = 21;
                value[22] = 22;
                value[23] = 23;
                value[24] = 24;
                value[25] = 25;
                value[26] = 26;
                value[27] = 27;
                value[28] = 28;
                value[29] = 29;
                value[30] = 30;
                value[31] = 31;

                Vector256<sbyte> vector = Vector256.LoadAlignedNonTemporal(value);

                for (int index = 0; index < Vector256<sbyte>.Count; index++)
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
        public unsafe void Vector256SingleLoadAlignedNonTemporalTest()
        {
            float* value = null;

            try
            {
                value = (float*)NativeMemory.AlignedAlloc(byteCount: 32, alignment: 32);

                value[0] = 0;
                value[1] = 1;
                value[2] = 2;
                value[3] = 3;
                value[4] = 4;
                value[5] = 5;
                value[6] = 6;
                value[7] = 7;

                Vector256<float> vector = Vector256.LoadAlignedNonTemporal(value);

                for (int index = 0; index < Vector256<float>.Count; index++)
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
        public unsafe void Vector256UInt16LoadAlignedNonTemporalTest()
        {
            ushort* value = null;

            try
            {
                value = (ushort*)NativeMemory.AlignedAlloc(byteCount: 32, alignment: 32);

                value[0] = 0;
                value[1] = 1;
                value[2] = 2;
                value[3] = 3;
                value[4] = 4;
                value[5] = 5;
                value[6] = 6;
                value[7] = 7;
                value[8] = 8;
                value[9] = 9;
                value[10] = 10;
                value[11] = 11;
                value[12] = 12;
                value[13] = 13;
                value[14] = 14;
                value[15] = 15;

                Vector256<ushort> vector = Vector256.LoadAlignedNonTemporal(value);

                for (int index = 0; index < Vector256<ushort>.Count; index++)
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
        public unsafe void Vector256UInt32LoadAlignedNonTemporalTest()
        {
            uint* value = null;

            try
            {
                value = (uint*)NativeMemory.AlignedAlloc(byteCount: 32, alignment: 32);

                value[0] = 0;
                value[1] = 1;
                value[2] = 2;
                value[3] = 3;
                value[4] = 4;
                value[5] = 5;
                value[6] = 6;
                value[7] = 7;

                Vector256<uint> vector = Vector256.LoadAlignedNonTemporal(value);

                for (int index = 0; index < Vector256<uint>.Count; index++)
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
        public unsafe void Vector256UInt64LoadAlignedNonTemporalTest()
        {
            ulong* value = null;

            try
            {
                value = (ulong*)NativeMemory.AlignedAlloc(byteCount: 32, alignment: 32);

                value[0] = 0;
                value[1] = 1;
                value[2] = 2;
                value[3] = 3;

                Vector256<ulong> vector = Vector256.LoadAlignedNonTemporal(value);

                for (int index = 0; index < Vector256<ulong>.Count; index++)
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
        public unsafe void Vector256ByteLoadUnsafeTest()
        {
            byte* value = stackalloc byte[32] {
                0,
                1,
                2,
                3,
                4,
                5,
                6,
                7,
                8,
                9,
                10,
                11,
                12,
                13,
                14,
                15,
                16,
                17,
                18,
                19,
                20,
                21,
                22,
                23,
                24,
                25,
                26,
                27,
                28,
                29,
                30,
                31,
            };

            Vector256<byte> vector = Vector256.LoadUnsafe(ref value[0]);

            for (int index = 0; index < Vector256<byte>.Count; index++)
            {
                Assert.Equal((byte)index, vector.GetElement(index));
            }
        }

        [Fact]
        public unsafe void Vector256DoubleLoadUnsafeTest()
        {
            double* value = stackalloc double[4] {
                0,
                1,
                2,
                3,
            };

            Vector256<double> vector = Vector256.LoadUnsafe(ref value[0]);

            for (int index = 0; index < Vector256<double>.Count; index++)
            {
                Assert.Equal((double)index, vector.GetElement(index));
            }
        }

        [Fact]
        public unsafe void Vector256Int16LoadUnsafeTest()
        {
            short* value = stackalloc short[16] {
                0,
                1,
                2,
                3,
                4,
                5,
                6,
                7,
                8,
                9,
                10,
                11,
                12,
                13,
                14,
                15,
            };

            Vector256<short> vector = Vector256.LoadUnsafe(ref value[0]);

            for (int index = 0; index < Vector256<short>.Count; index++)
            {
                Assert.Equal((short)index, vector.GetElement(index));
            }
        }

        [Fact]
        public unsafe void Vector256Int32LoadUnsafeTest()
        {
            int* value = stackalloc int[8] {
                0,
                1,
                2,
                3,
                4,
                5,
                6,
                7,
            };

            Vector256<int> vector = Vector256.LoadUnsafe(ref value[0]);

            for (int index = 0; index < Vector256<int>.Count; index++)
            {
                Assert.Equal((int)index, vector.GetElement(index));
            }
        }

        [Fact]
        public unsafe void Vector256Int64LoadUnsafeTest()
        {
            long* value = stackalloc long[4] {
                0,
                1,
                2,
                3,
            };

            Vector256<long> vector = Vector256.LoadUnsafe(ref value[0]);

            for (int index = 0; index < Vector256<long>.Count; index++)
            {
                Assert.Equal((long)index, vector.GetElement(index));
            }
        }

        [Fact]
        public unsafe void Vector256NIntLoadUnsafeTest()
        {
            if (Environment.Is64BitProcess)
            {
                nint* value = stackalloc nint[4] {
                    0,
                    1,
                    2,
                    3,
                };

                Vector256<nint> vector = Vector256.LoadUnsafe(ref value[0]);

                for (int index = 0; index < Vector256<nint>.Count; index++)
                {
                    Assert.Equal((nint)index, vector.GetElement(index));
                }
            }
            else
            {
                nint* value = stackalloc nint[8] {
                    0,
                    1,
                    2,
                    3,
                    4,
                    5,
                    6,
                    7,
                };

                Vector256<nint> vector = Vector256.LoadUnsafe(ref value[0]);

                for (int index = 0; index < Vector256<nint>.Count; index++)
                {
                    Assert.Equal((nint)index, vector.GetElement(index));
                }
            }
        }

        [Fact]
        public unsafe void Vector256NUIntLoadUnsafeTest()
        {
            if (Environment.Is64BitProcess)
            {
                nuint* value = stackalloc nuint[4] {
                    0,
                    1,
                    2,
                    3,
                };

                Vector256<nuint> vector = Vector256.LoadUnsafe(ref value[0]);

                for (int index = 0; index < Vector256<nuint>.Count; index++)
                {
                    Assert.Equal((nuint)index, vector.GetElement(index));
                }
            }
            else
            {
                nuint* value = stackalloc nuint[8] {
                    0,
                    1,
                    2,
                    3,
                    4,
                    5,
                    6,
                    7,
                };

                Vector256<nuint> vector = Vector256.LoadUnsafe(ref value[0]);

                for (int index = 0; index < Vector256<nuint>.Count; index++)
                {
                    Assert.Equal((nuint)index, vector.GetElement(index));
                }
            }
        }

        [Fact]
        public unsafe void Vector256SByteLoadUnsafeTest()
        {
            sbyte* value = stackalloc sbyte[32] {
                0,
                1,
                2,
                3,
                4,
                5,
                6,
                7,
                8,
                9,
                10,
                11,
                12,
                13,
                14,
                15,
                16,
                17,
                18,
                19,
                20,
                21,
                22,
                23,
                24,
                25,
                26,
                27,
                28,
                29,
                30,
                31,
            };

            Vector256<sbyte> vector = Vector256.LoadUnsafe(ref value[0]);

            for (int index = 0; index < Vector256<sbyte>.Count; index++)
            {
                Assert.Equal((sbyte)index, vector.GetElement(index));
            }
        }

        [Fact]
        public unsafe void Vector256SingleLoadUnsafeTest()
        {
            float* value = stackalloc float[8] {
                0,
                1,
                2,
                3,
                4,
                5,
                6,
                7,
            };

            Vector256<float> vector = Vector256.LoadUnsafe(ref value[0]);

            for (int index = 0; index < Vector256<float>.Count; index++)
            {
                Assert.Equal((float)index, vector.GetElement(index));
            }
        }

        [Fact]
        public unsafe void Vector256UInt16LoadUnsafeTest()
        {
            ushort* value = stackalloc ushort[16] {
                0,
                1,
                2,
                3,
                4,
                5,
                6,
                7,
                8,
                9,
                10,
                11,
                12,
                13,
                14,
                15,
            };

            Vector256<ushort> vector = Vector256.LoadUnsafe(ref value[0]);

            for (int index = 0; index < Vector256<ushort>.Count; index++)
            {
                Assert.Equal((ushort)index, vector.GetElement(index));
            }
        }

        [Fact]
        public unsafe void Vector256UInt32LoadUnsafeTest()
        {
            uint* value = stackalloc uint[8] {
                0,
                1,
                2,
                3,
                4,
                5,
                6,
                7,
            };

            Vector256<uint> vector = Vector256.LoadUnsafe(ref value[0]);

            for (int index = 0; index < Vector256<uint>.Count; index++)
            {
                Assert.Equal((uint)index, vector.GetElement(index));
            }
        }

        [Fact]
        public unsafe void Vector256UInt64LoadUnsafeTest()
        {
            ulong* value = stackalloc ulong[4] {
                0,
                1,
                2,
                3,
            };

            Vector256<ulong> vector = Vector256.LoadUnsafe(ref value[0]);

            for (int index = 0; index < Vector256<ulong>.Count; index++)
            {
                Assert.Equal((ulong)index, vector.GetElement(index));
            }
        }

        [Fact]
        public unsafe void Vector256ByteLoadUnsafeIndexTest()
        {
            byte* value = stackalloc byte[32 + 1] {
                0,
                1,
                2,
                3,
                4,
                5,
                6,
                7,
                8,
                9,
                10,
                11,
                12,
                13,
                14,
                15,
                16,
                17,
                18,
                19,
                20,
                21,
                22,
                23,
                24,
                25,
                26,
                27,
                28,
                29,
                30,
                31,
                32,
            };

            Vector256<byte> vector = Vector256.LoadUnsafe(ref value[0], 1);

            for (int index = 0; index < Vector256<byte>.Count; index++)
            {
                Assert.Equal((byte)(index + 1), vector.GetElement(index));
            }
        }

        [Fact]
        public unsafe void Vector256DoubleLoadUnsafeIndexTest()
        {
            double* value = stackalloc double[4 + 1] {
                0,
                1,
                2,
                3,
                4,
            };

            Vector256<double> vector = Vector256.LoadUnsafe(ref value[0], 1);

            for (int index = 0; index < Vector256<double>.Count; index++)
            {
                Assert.Equal((double)(index + 1), vector.GetElement(index));
            }
        }

        [Fact]
        public unsafe void Vector256Int16LoadUnsafeIndexTest()
        {
            short* value = stackalloc short[16 + 1] {
                0,
                1,
                2,
                3,
                4,
                5,
                6,
                7,
                8,
                9,
                10,
                11,
                12,
                13,
                14,
                15,
                16,
            };

            Vector256<short> vector = Vector256.LoadUnsafe(ref value[0], 1);

            for (int index = 0; index < Vector256<short>.Count; index++)
            {
                Assert.Equal((short)(index + 1), vector.GetElement(index));
            }
        }

        [Fact]
        public unsafe void Vector256Int32LoadUnsafeIndexTest()
        {
            int* value = stackalloc int[8 + 1] {
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

            Vector256<int> vector = Vector256.LoadUnsafe(ref value[0], 1);

            for (int index = 0; index < Vector256<int>.Count; index++)
            {
                Assert.Equal((int)(index + 1), vector.GetElement(index));
            }
        }

        [Fact]
        public unsafe void Vector256Int64LoadUnsafeIndexTest()
        {
            long* value = stackalloc long[4 + 1] {
                0,
                1,
                2,
                3,
                4,
            };

            Vector256<long> vector = Vector256.LoadUnsafe(ref value[0], 1);

            for (int index = 0; index < Vector256<long>.Count; index++)
            {
                Assert.Equal((long)(index + 1), vector.GetElement(index));
            }
        }

        [Fact]
        public unsafe void Vector256NIntLoadUnsafeIndexTest()
        {
            if (Environment.Is64BitProcess)
            {
                nint* value = stackalloc nint[4 + 1] {
                    0,
                    1,
                    2,
                    3,
                    4,
                };

                Vector256<nint> vector = Vector256.LoadUnsafe(ref value[0], 1);

                for (int index = 0; index < Vector256<nint>.Count; index++)
                {
                    Assert.Equal((nint)(index + 1), vector.GetElement(index));
                }
            }
            else
            {
                nint* value = stackalloc nint[8 + 1] {
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

                Vector256<nint> vector = Vector256.LoadUnsafe(ref value[0], 1);

                for (int index = 0; index < Vector256<nint>.Count; index++)
                {
                    Assert.Equal((nint)(index + 1), vector.GetElement(index));
                }
            }
        }

        [Fact]
        public unsafe void Vector256NUIntLoadUnsafeIndexTest()
        {
            if (Environment.Is64BitProcess)
            {
                nuint* value = stackalloc nuint[4 + 1] {
                    0,
                    1,
                    2,
                    3,
                    4,
                };

                Vector256<nuint> vector = Vector256.LoadUnsafe(ref value[0], 1);

                for (int index = 0; index < Vector256<nuint>.Count; index++)
                {
                    Assert.Equal((nuint)(index + 1), vector.GetElement(index));
                }
            }
            else
            {
                nuint* value = stackalloc nuint[8 + 1] {
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

                Vector256<nuint> vector = Vector256.LoadUnsafe(ref value[0], 1);

                for (int index = 0; index < Vector256<nuint>.Count; index++)
                {
                    Assert.Equal((nuint)(index + 1), vector.GetElement(index));
                }
            }
        }

        [Fact]
        public unsafe void Vector256SByteLoadUnsafeIndexTest()
        {
            sbyte* value = stackalloc sbyte[32 + 1] {
                0,
                1,
                2,
                3,
                4,
                5,
                6,
                7,
                8,
                9,
                10,
                11,
                12,
                13,
                14,
                15,
                16,
                17,
                18,
                19,
                20,
                21,
                22,
                23,
                24,
                25,
                26,
                27,
                28,
                29,
                30,
                31,
                32,
            };

            Vector256<sbyte> vector = Vector256.LoadUnsafe(ref value[0], 1);

            for (int index = 0; index < Vector256<sbyte>.Count; index++)
            {
                Assert.Equal((sbyte)(index + 1), vector.GetElement(index));
            }
        }

        [Fact]
        public unsafe void Vector256SingleLoadUnsafeIndexTest()
        {
            float* value = stackalloc float[8 + 1] {
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

            Vector256<float> vector = Vector256.LoadUnsafe(ref value[0], 1);

            for (int index = 0; index < Vector256<float>.Count; index++)
            {
                Assert.Equal((float)(index + 1), vector.GetElement(index));
            }
        }

        [Fact]
        public unsafe void Vector256UInt16LoadUnsafeIndexTest()
        {
            ushort* value = stackalloc ushort[16 + 1] {
                0,
                1,
                2,
                3,
                4,
                5,
                6,
                7,
                8,
                9,
                10,
                11,
                12,
                13,
                14,
                15,
                16,
            };

            Vector256<ushort> vector = Vector256.LoadUnsafe(ref value[0], 1);

            for (int index = 0; index < Vector256<ushort>.Count; index++)
            {
                Assert.Equal((ushort)(index + 1), vector.GetElement(index));
            }
        }

        [Fact]
        public unsafe void Vector256UInt32LoadUnsafeIndexTest()
        {
            uint* value = stackalloc uint[8 + 1] {
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

            Vector256<uint> vector = Vector256.LoadUnsafe(ref value[0], 1);

            for (int index = 0; index < Vector256<uint>.Count; index++)
            {
                Assert.Equal((uint)(index + 1), vector.GetElement(index));
            }
        }

        [Fact]
        public unsafe void Vector256UInt64LoadUnsafeIndexTest()
        {
            ulong* value = stackalloc ulong[4 + 1] {
                0,
                1,
                2,
                3,
                4,
            };

            Vector256<ulong> vector = Vector256.LoadUnsafe(ref value[0], 1);

            for (int index = 0; index < Vector256<ulong>.Count; index++)
            {
                Assert.Equal((ulong)(index + 1), vector.GetElement(index));
            }
        }

        [Fact]
        public void Vector256ByteShiftLeftTest()
        {
            Vector256<byte> vector = Vector256.Create((byte)0x01);
            vector = Vector256.ShiftLeft(vector, 4);

            for (int index = 0; index < Vector256<byte>.Count; index++)
            {
                Assert.Equal((byte)0x10, vector.GetElement(index));
            }
        }

        [Fact]
        public void Vector256Int16ShiftLeftTest()
        {
            Vector256<short> vector = Vector256.Create((short)0x01);
            vector = Vector256.ShiftLeft(vector, 4);

            for (int index = 0; index < Vector256<short>.Count; index++)
            {
                Assert.Equal((short)0x10, vector.GetElement(index));
            }
        }

        [Fact]
        public void Vector256Int32ShiftLeftTest()
        {
            Vector256<int> vector = Vector256.Create((int)0x01);
            vector = Vector256.ShiftLeft(vector, 4);

            for (int index = 0; index < Vector256<int>.Count; index++)
            {
                Assert.Equal((int)0x10, vector.GetElement(index));
            }
        }

        [Fact]
        public void Vector256Int64ShiftLeftTest()
        {
            Vector256<long> vector = Vector256.Create((long)0x01);
            vector = Vector256.ShiftLeft(vector, 4);

            for (int index = 0; index < Vector256<long>.Count; index++)
            {
                Assert.Equal((long)0x10, vector.GetElement(index));
            }
        }

        [Fact]
        public void Vector256NIntShiftLeftTest()
        {
            Vector256<nint> vector = Vector256.Create((nint)0x01);
            vector = Vector256.ShiftLeft(vector, 4);

            for (int index = 0; index < Vector256<nint>.Count; index++)
            {
                Assert.Equal((nint)0x10, vector.GetElement(index));
            }
        }

        [Fact]
        public void Vector256NUIntShiftLeftTest()
        {
            Vector256<nuint> vector = Vector256.Create((nuint)0x01);
            vector = Vector256.ShiftLeft(vector, 4);

            for (int index = 0; index < Vector256<nuint>.Count; index++)
            {
                Assert.Equal((nuint)0x10, vector.GetElement(index));
            }
        }

        [Fact]
        public void Vector256SByteShiftLeftTest()
        {
            Vector256<sbyte> vector = Vector256.Create((sbyte)0x01);
            vector = Vector256.ShiftLeft(vector, 4);

            for (int index = 0; index < Vector256<sbyte>.Count; index++)
            {
                Assert.Equal((sbyte)0x10, vector.GetElement(index));
            }
        }

        [Fact]
        public void Vector256UInt16ShiftLeftTest()
        {
            Vector256<ushort> vector = Vector256.Create((ushort)0x01);
            vector = Vector256.ShiftLeft(vector, 4);

            for (int index = 0; index < Vector256<ushort>.Count; index++)
            {
                Assert.Equal((ushort)0x10, vector.GetElement(index));
            }
        }

        [Fact]
        public void Vector256UInt32ShiftLeftTest()
        {
            Vector256<uint> vector = Vector256.Create((uint)0x01);
            vector = Vector256.ShiftLeft(vector, 4);

            for (int index = 0; index < Vector256<uint>.Count; index++)
            {
                Assert.Equal((uint)0x10, vector.GetElement(index));
            }
        }

        [Fact]
        public void Vector256UInt64ShiftLeftTest()
        {
            Vector256<ulong> vector = Vector256.Create((ulong)0x01);
            vector = Vector256.ShiftLeft(vector, 4);

            for (int index = 0; index < Vector256<ulong>.Count; index++)
            {
                Assert.Equal((ulong)0x10, vector.GetElement(index));
            }
        }

        [Fact]
        public void Vector256Int16ShiftRightArithmeticTest()
        {
            Vector256<short> vector = Vector256.Create(unchecked((short)0x8000));
            vector = Vector256.ShiftRightArithmetic(vector, 4);

            for (int index = 0; index < Vector256<short>.Count; index++)
            {
                Assert.Equal(unchecked((short)0xF800), vector.GetElement(index));
            }
        }

        [Fact]
        public void Vector256Int32ShiftRightArithmeticTest()
        {
            Vector256<int> vector = Vector256.Create(unchecked((int)0x80000000));
            vector = Vector256.ShiftRightArithmetic(vector, 4);

            for (int index = 0; index < Vector256<int>.Count; index++)
            {
                Assert.Equal(unchecked((int)0xF8000000), vector.GetElement(index));
            }
        }

        [Fact]
        public void Vector256Int64ShiftRightArithmeticTest()
        {
            Vector256<long> vector = Vector256.Create(unchecked((long)0x8000000000000000));
            vector = Vector256.ShiftRightArithmetic(vector, 4);

            for (int index = 0; index < Vector256<long>.Count; index++)
            {
                Assert.Equal(unchecked((long)0xF800000000000000), vector.GetElement(index));
            }
        }

        [Fact]
        public void Vector256NIntShiftRightArithmeticTest()
        {
            if (Environment.Is64BitProcess)
            {
                Vector256<nint> vector = Vector256.Create(unchecked((nint)0x8000000000000000));
                vector = Vector256.ShiftRightArithmetic(vector, 4);

                for (int index = 0; index < Vector256<nint>.Count; index++)
                {
                    Assert.Equal(unchecked((nint)0xF800000000000000), vector.GetElement(index));
                }
            }
            else
            {
                Vector256<nint> vector = Vector256.Create(unchecked((nint)0x80000000));
                vector = Vector256.ShiftRightArithmetic(vector, 4);

                for (int index = 0; index < Vector256<nint>.Count; index++)
                {
                    Assert.Equal(unchecked((nint)0xF8000000), vector.GetElement(index));
                }
            }
        }

        [Fact]
        public void Vector256SByteShiftRightArithmeticTest()
        {
            Vector256<sbyte> vector = Vector256.Create(unchecked((sbyte)0x80));
            vector = Vector256.ShiftRightArithmetic(vector, 4);

            for (int index = 0; index < Vector256<sbyte>.Count; index++)
            {
                Assert.Equal(unchecked((sbyte)0xF8), vector.GetElement(index));
            }
        }

        [Fact]
        public void Vector256ByteShiftRightLogicalTest()
        {
            Vector256<byte> vector = Vector256.Create((byte)0x80);
            vector = Vector256.ShiftRightLogical(vector, 4);

            for (int index = 0; index < Vector256<byte>.Count; index++)
            {
                Assert.Equal((byte)0x08, vector.GetElement(index));
            }
        }

        [Fact]
        public void Vector256Int16ShiftRightLogicalTest()
        {
            Vector256<short> vector = Vector256.Create(unchecked((short)0x8000));
            vector = Vector256.ShiftRightLogical(vector, 4);

            for (int index = 0; index < Vector256<short>.Count; index++)
            {
                Assert.Equal((short)0x0800, vector.GetElement(index));
            }
        }

        [Fact]
        public void Vector256Int32ShiftRightLogicalTest()
        {
            Vector256<int> vector = Vector256.Create(unchecked((int)0x80000000));
            vector = Vector256.ShiftRightLogical(vector, 4);

            for (int index = 0; index < Vector256<int>.Count; index++)
            {
                Assert.Equal((int)0x08000000, vector.GetElement(index));
            }
        }

        [Fact]
        public void Vector256Int64ShiftRightLogicalTest()
        {
            Vector256<long> vector = Vector256.Create(unchecked((long)0x8000000000000000));
            vector = Vector256.ShiftRightLogical(vector, 4);

            for (int index = 0; index < Vector256<long>.Count; index++)
            {
                Assert.Equal((long)0x0800000000000000, vector.GetElement(index));
            }
        }

        [Fact]
        public void Vector256NIntShiftRightLogicalTest()
        {
            if (Environment.Is64BitProcess)
            {
                Vector256<nint> vector = Vector256.Create(unchecked((nint)0x8000000000000000));
                vector = Vector256.ShiftRightLogical(vector, 4);

                for (int index = 0; index < Vector256<nint>.Count; index++)
                {
                    Assert.Equal(unchecked((nint)0x0800000000000000), vector.GetElement(index));
                }
            }
            else
            {
                Vector256<nint> vector = Vector256.Create(unchecked((nint)0x80000000));
                vector = Vector256.ShiftRightLogical(vector, 4);

                for (int index = 0; index < Vector256<nint>.Count; index++)
                {
                    Assert.Equal(unchecked((nint)0x08000000), vector.GetElement(index));
                }
            }
        }

        [Fact]
        public void Vector256NUIntShiftRightLogicalTest()
        {
            if (Environment.Is64BitProcess)
            {
                Vector256<nuint> vector = Vector256.Create(unchecked((nuint)0x8000000000000000));
                vector = Vector256.ShiftRightLogical(vector, 4);

                for (int index = 0; index < Vector256<nuint>.Count; index++)
                {
                    Assert.Equal(unchecked((nuint)0x0800000000000000), vector.GetElement(index));
                }
            }
            else
            {
                Vector256<nuint> vector = Vector256.Create(unchecked((nuint)0x80000000));
                vector = Vector256.ShiftRightLogical(vector, 4);

                for (int index = 0; index < Vector256<nuint>.Count; index++)
                {
                    Assert.Equal(unchecked((nuint)0x08000000), vector.GetElement(index));
                }
            }
        }

        [Fact]
        public void Vector256SByteShiftRightLogicalTest()
        {
            Vector256<sbyte> vector = Vector256.Create(unchecked((sbyte)0x80));
            vector = Vector256.ShiftRightLogical(vector, 4);

            for (int index = 0; index < Vector256<sbyte>.Count; index++)
            {
                Assert.Equal((sbyte)0x08, vector.GetElement(index));
            }
        }

        [Fact]
        public void Vector256UInt16ShiftRightLogicalTest()
        {
            Vector256<ushort> vector = Vector256.Create(unchecked((ushort)0x8000));
            vector = Vector256.ShiftRightLogical(vector, 4);

            for (int index = 0; index < Vector256<ushort>.Count; index++)
            {
                Assert.Equal((ushort)0x0800, vector.GetElement(index));
            }
        }

        [Fact]
        public void Vector256UInt32ShiftRightLogicalTest()
        {
            Vector256<uint> vector = Vector256.Create(0x80000000);
            vector = Vector256.ShiftRightLogical(vector, 4);

            for (int index = 0; index < Vector256<uint>.Count; index++)
            {
                Assert.Equal((uint)0x08000000, vector.GetElement(index));
            }
        }

        [Fact]
        public void Vector256UInt64ShiftRightLogicalTest()
        {
            Vector256<ulong> vector = Vector256.Create(0x8000000000000000);
            vector = Vector256.ShiftRightLogical(vector, 4);

            for (int index = 0; index < Vector256<ulong>.Count; index++)
            {
                Assert.Equal((ulong)0x0800000000000000, vector.GetElement(index));
            }
        }

        [Fact]
        public void Vector256ByteShuffleOneInputTest()
        {
            Vector256<byte> vector = Vector256.Create((byte)1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24, 25, 26, 27, 28, 29, 30, 31, 32);
            Vector256<byte> result = Vector256.Shuffle(vector, Vector256.Create((byte)31, 30, 29, 28, 27, 26, 25, 24, 23, 22, 21, 20, 19, 18, 17, 16, 15, 14, 13, 12, 11, 10, 9, 8, 7, 6, 5, 4, 3, 2, 1, 0));

            for (int index = 0; index < Vector256<byte>.Count; index++)
            {
                Assert.Equal((byte)(Vector256<byte>.Count - index), result.GetElement(index));
            }
        }

        [Fact]
        public void Vector256DoubleShuffleOneInputTest()
        {
            Vector256<double> vector = Vector256.Create((double)1, 2, 3, 4);
            Vector256<double> result = Vector256.Shuffle(vector, Vector256.Create((long)3, 2, 1, 0));

            for (int index = 0; index < Vector256<double>.Count; index++)
            {
                Assert.Equal((double)(Vector256<double>.Count - index), result.GetElement(index));
            }
        }

        [Fact]
        public void Vector256Int16ShuffleOneInputTest()
        {
            Vector256<short> vector = Vector256.Create((short)1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16);
            Vector256<short> result = Vector256.Shuffle(vector, Vector256.Create((short)15, 14, 13, 12, 11, 10, 9, 8, 7, 6, 5, 4, 3, 2, 1, 0));

            for (int index = 0; index < Vector256<short>.Count; index++)
            {
                Assert.Equal((short)(Vector256<short>.Count - index), result.GetElement(index));
            }
        }

        [Fact]
        public void Vector256Int32ShuffleOneInputTest()
        {
            Vector256<int> vector = Vector256.Create((int)1, 2, 3, 4, 5, 6, 7, 8);
            Vector256<int> result = Vector256.Shuffle(vector, Vector256.Create((int)7, 6, 5, 4, 3, 2, 1, 0));

            for (int index = 0; index < Vector256<int>.Count; index++)
            {
                Assert.Equal((int)(Vector256<int>.Count - index), result.GetElement(index));
            }
        }

        [Fact]
        public void Vector256Int64ShuffleOneInputTest()
        {
            Vector256<long> vector = Vector256.Create((long)1, 2, 3, 4);
            Vector256<long> result = Vector256.Shuffle(vector, Vector256.Create((long)3, 2, 1, 0));

            for (int index = 0; index < Vector256<long>.Count; index++)
            {
                Assert.Equal((long)(Vector256<long>.Count - index), result.GetElement(index));
            }
        }

        [Fact]
        public void Vector256SByteShuffleOneInputTest()
        {
            Vector256<sbyte> vector = Vector256.Create((sbyte)1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24, 25, 26, 27, 28, 29, 30, 31, 32);
            Vector256<sbyte> result = Vector256.Shuffle(vector, Vector256.Create((sbyte)31, 30, 29, 28, 27, 26, 25, 24, 23, 22, 21, 20, 19, 18, 17, 16, 15, 14, 13, 12, 11, 10, 9, 8, 7, 6, 5, 4, 3, 2, 1, 0));

            for (int index = 0; index < Vector256<sbyte>.Count; index++)
            {
                Assert.Equal((sbyte)(Vector256<sbyte>.Count - index), result.GetElement(index));
            }
        }

        [Fact]
        public void Vector256SingleShuffleOneInputTest()
        {
            Vector256<float> vector = Vector256.Create((float)1, 2, 3, 4, 5, 6, 7, 8);
            Vector256<float> result = Vector256.Shuffle(vector, Vector256.Create((int)7, 6, 5, 4, 3, 2, 1, 0));

            for (int index = 0; index < Vector256<float>.Count; index++)
            {
                Assert.Equal((float)(Vector256<float>.Count - index), result.GetElement(index));
            }
        }

        [Fact]
        public void Vector256UInt16ShuffleOneInputTest()
        {
            Vector256<ushort> vector = Vector256.Create((ushort)1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16);
            Vector256<ushort> result = Vector256.Shuffle(vector, Vector256.Create((ushort)15, 14, 13, 12, 11, 10, 9, 8, 7, 6, 5, 4, 3, 2, 1, 0));

            for (int index = 0; index < Vector256<ushort>.Count; index++)
            {
                Assert.Equal((ushort)(Vector256<ushort>.Count - index), result.GetElement(index));
            }
        }

        [Fact]
        public void Vector256UInt32ShuffleOneInputTest()
        {
            Vector256<uint> vector = Vector256.Create((uint)1, 2, 3, 4, 5, 6, 7, 8);
            Vector256<uint> result = Vector256.Shuffle(vector, Vector256.Create((uint)7, 6, 5, 4, 3, 2, 1, 0));

            for (int index = 0; index < Vector256<uint>.Count; index++)
            {
                Assert.Equal((uint)(Vector256<uint>.Count - index), result.GetElement(index));
            }
        }

        [Fact]
        public void Vector256UInt64ShuffleOneInputTest()
        {
            Vector256<ulong> vector = Vector256.Create((ulong)1, 2, 3, 4);
            Vector256<ulong> result = Vector256.Shuffle(vector, Vector256.Create((ulong)3, 2, 1, 0));

            for (int index = 0; index < Vector256<ulong>.Count; index++)
            {
                Assert.Equal((ulong)(Vector256<ulong>.Count - index), result.GetElement(index));
            }
        }

        [Fact]
        public void Vector256ByteShuffleOneInputWithDirectVectorTest()
        {
            Vector256<byte> result = Vector256.Shuffle(Vector256.Create((byte)1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24, 25, 26, 27, 28, 29, 30, 31, 32), Vector256.Create((byte)31, 30, 29, 28, 27, 26, 25, 24, 23, 22, 21, 20, 19, 18, 17, 16, 15, 14, 13, 12, 11, 10, 9, 8, 7, 6, 5, 4, 3, 2, 1, 0));

            for (int index = 0; index < Vector256<byte>.Count; index++)
            {
                Assert.Equal((byte)(Vector256<byte>.Count - index), result.GetElement(index));
            }
        }

        [Fact]
        public void Vector256DoubleShuffleOneInputWithDirectVectorTest()
        {
            Vector256<double> result = Vector256.Shuffle(Vector256.Create((double)1, 2, 3, 4), Vector256.Create((long)3, 2, 1, 0));

            for (int index = 0; index < Vector256<double>.Count; index++)
            {
                Assert.Equal((double)(Vector256<double>.Count - index), result.GetElement(index));
            }
        }

        [Fact]
        public void Vector256Int16ShuffleOneInputWithDirectVectorTest()
        {
            Vector256<short> result = Vector256.Shuffle(Vector256.Create((short)1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16), Vector256.Create((short)15, 14, 13, 12, 11, 10, 9, 8, 7, 6, 5, 4, 3, 2, 1, 0));

            for (int index = 0; index < Vector256<short>.Count; index++)
            {
                Assert.Equal((short)(Vector256<short>.Count - index), result.GetElement(index));
            }
        }

        [Fact]
        public void Vector256Int32ShuffleOneInputWithDirectVectorTest()
        {
            Vector256<int> result = Vector256.Shuffle(Vector256.Create((int)1, 2, 3, 4, 5, 6, 7, 8), Vector256.Create((int)7, 6, 5, 4, 3, 2, 1, 0));

            for (int index = 0; index < Vector256<int>.Count; index++)
            {
                Assert.Equal((int)(Vector256<int>.Count - index), result.GetElement(index));
            }
        }

        [Fact]
        public void Vector256Int64ShuffleOneInputWithDirectVectorTest()
        {
            Vector256<long> result = Vector256.Shuffle(Vector256.Create((long)1, 2, 3, 4), Vector256.Create((long)3, 2, 1, 0));

            for (int index = 0; index < Vector256<long>.Count; index++)
            {
                Assert.Equal((long)(Vector256<long>.Count - index), result.GetElement(index));
            }
        }

        [Fact]
        public void Vector256SByteShuffleOneInputWithDirectVectorTest()
        {
            Vector256<sbyte> result = Vector256.Shuffle(Vector256.Create((sbyte)1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24, 25, 26, 27, 28, 29, 30, 31, 32), Vector256.Create((sbyte)31, 30, 29, 28, 27, 26, 25, 24, 23, 22, 21, 20, 19, 18, 17, 16, 15, 14, 13, 12, 11, 10, 9, 8, 7, 6, 5, 4, 3, 2, 1, 0));

            for (int index = 0; index < Vector256<sbyte>.Count; index++)
            {
                Assert.Equal((sbyte)(Vector256<sbyte>.Count - index), result.GetElement(index));
            }
        }

        [Fact]
        public void Vector256SingleShuffleOneInputWithDirectVectorTest()
        {
            Vector256<float> result = Vector256.Shuffle(Vector256.Create((float)1, 2, 3, 4, 5, 6, 7, 8), Vector256.Create((int)7, 6, 5, 4, 3, 2, 1, 0));

            for (int index = 0; index < Vector256<float>.Count; index++)
            {
                Assert.Equal((float)(Vector256<float>.Count - index), result.GetElement(index));
            }
        }

        [Fact]
        public void Vector256UInt16ShuffleOneInputWithDirectVectorTest()
        {
            Vector256<ushort> result = Vector256.Shuffle(Vector256.Create((ushort)1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16), Vector256.Create((ushort)15, 14, 13, 12, 11, 10, 9, 8, 7, 6, 5, 4, 3, 2, 1, 0));

            for (int index = 0; index < Vector256<ushort>.Count; index++)
            {
                Assert.Equal((ushort)(Vector256<ushort>.Count - index), result.GetElement(index));
            }
        }

        [Fact]
        public void Vector256UInt32ShuffleOneInputWithDirectVectorTest()
        {
            Vector256<uint> result = Vector256.Shuffle(Vector256.Create((uint)1, 2, 3, 4, 5, 6, 7, 8), Vector256.Create((uint)7, 6, 5, 4, 3, 2, 1, 0));

            for (int index = 0; index < Vector256<uint>.Count; index++)
            {
                Assert.Equal((uint)(Vector256<uint>.Count - index), result.GetElement(index));
            }
        }

        [Fact]
        public void Vector256UInt64ShuffleOneInputWithDirectVectorTest()
        {
            Vector256<ulong> result = Vector256.Shuffle(Vector256.Create((ulong)1, 2, 3, 4), Vector256.Create((ulong)3, 2, 1, 0));

            for (int index = 0; index < Vector256<ulong>.Count; index++)
            {
                Assert.Equal((ulong)(Vector256<ulong>.Count - index), result.GetElement(index));
            }
        }

        [Fact]
        public void Vector256ByteShuffleOneInputWithDirectVectorAndNoCrossLaneTest()
        {
            Vector256<byte> result = Vector256.Shuffle(Vector256.Create((byte)1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24, 25, 26, 27, 28, 29, 30, 31, 32), Vector256.Create((byte)15, 14, 13, 12, 11, 10, 9, 8, 7, 6, 5, 4, 3, 2, 1, 0, 31, 30, 29, 28, 27, 26, 25, 24, 23, 22, 21, 20, 19, 18, 17, 16));

            for (int index = 0; index < Vector128<byte>.Count; index++)
            {
                Assert.Equal((byte)(Vector128<byte>.Count - index), result.GetElement(index));
            }

            for (int index = Vector128<byte>.Count; index < Vector256<byte>.Count; index++)
            {
                Assert.Equal((byte)(Vector256<byte>.Count - (index - Vector128<byte>.Count)), result.GetElement(index));
            }
        }

        [Fact]
        public void Vector256DoubleShuffleOneInputWithDirectVectorAndNoCrossLaneTest()
        {
            Vector256<double> result = Vector256.Shuffle(Vector256.Create((double)1, 2, 3, 4), Vector256.Create((long)1, 0, 3, 2));

            for (int index = 0; index < Vector128<double>.Count; index++)
            {
                Assert.Equal((double)(Vector128<double>.Count - index), result.GetElement(index));
            }

            for (int index = Vector128<double>.Count; index < Vector256<double>.Count; index++)
            {
                Assert.Equal((double)(Vector256<double>.Count - (index - Vector128<double>.Count)), result.GetElement(index));
            }
        }

        [Fact]
        public void Vector256Int16ShuffleOneInputWithDirectVectorAndNoCrossLaneTest()
        {
            Vector256<short> result = Vector256.Shuffle(Vector256.Create((short)1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16), Vector256.Create((short)7, 6, 5, 4, 3, 2, 1, 0, 15, 14, 13, 12, 11, 10, 9, 8));

            for (int index = 0; index < Vector128<short>.Count; index++)
            {
                Assert.Equal((short)(Vector128<short>.Count - index), result.GetElement(index));
            }

            for (int index = Vector128<short>.Count; index < Vector256<short>.Count; index++)
            {
                Assert.Equal((short)(Vector256<short>.Count - (index - Vector128<short>.Count)), result.GetElement(index));
            }
        }

        [Fact]
        public void Vector256Int32ShuffleOneInputWithDirectVectorAndNoCrossLaneTest()
        {
            Vector256<int> result = Vector256.Shuffle(Vector256.Create((int)1, 2, 3, 4, 5, 6, 7, 8), Vector256.Create((int)3, 2, 1, 0, 7, 6, 5, 4));

            for (int index = 0; index < Vector128<int>.Count; index++)
            {
                Assert.Equal((int)(Vector128<int>.Count - index), result.GetElement(index));
            }

            for (int index = Vector128<int>.Count; index < Vector256<int>.Count; index++)
            {
                Assert.Equal((int)(Vector256<int>.Count - (index - Vector128<int>.Count)), result.GetElement(index));
            }
        }

        [Fact]
        public void Vector256Int64ShuffleOneInputWithDirectVectorAndNoCrossLaneTest()
        {
            Vector256<long> result = Vector256.Shuffle(Vector256.Create((long)1, 2, 3, 4), Vector256.Create((long)1, 0, 3, 2));

            for (int index = 0; index < Vector128<long>.Count; index++)
            {
                Assert.Equal((long)(Vector128<long>.Count - index), result.GetElement(index));
            }

            for (int index = Vector128<long>.Count; index < Vector256<long>.Count; index++)
            {
                Assert.Equal((long)(Vector256<long>.Count - (index - Vector128<long>.Count)), result.GetElement(index));
            }
        }

        [Fact]
        public void Vector256SByteShuffleOneInputWithDirectVectorAndNoCrossLaneTest()
        {
            Vector256<sbyte> result = Vector256.Shuffle(Vector256.Create((sbyte)1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24, 25, 26, 27, 28, 29, 30, 31, 32), Vector256.Create((sbyte)15, 14, 13, 12, 11, 10, 9, 8, 7, 6, 5, 4, 3, 2, 1, 0, 31, 30, 29, 28, 27, 26, 25, 24, 23, 22, 21, 20, 19, 18, 17, 16));

            for (int index = 0; index < Vector128<sbyte>.Count; index++)
            {
                Assert.Equal((sbyte)(Vector128<sbyte>.Count - index), result.GetElement(index));
            }

            for (int index = Vector128<sbyte>.Count; index < Vector256<sbyte>.Count; index++)
            {
                Assert.Equal((sbyte)(Vector256<sbyte>.Count - (index - Vector128<sbyte>.Count)), result.GetElement(index));
            }
        }

        [Fact]
        public void Vector256SingleShuffleOneInputWithDirectVectorAndNoCrossLaneTest()
        {
            Vector256<float> result = Vector256.Shuffle(Vector256.Create((float)1, 2, 3, 4, 5, 6, 7, 8), Vector256.Create((int)3, 2, 1, 0, 7, 6, 5, 4));

            for (int index = 0; index < Vector128<float>.Count; index++)
            {
                Assert.Equal((float)(Vector128<float>.Count - index), result.GetElement(index));
            }

            for (int index = Vector128<float>.Count; index < Vector256<float>.Count; index++)
            {
                Assert.Equal((float)(Vector256<float>.Count - (index - Vector128<float>.Count)), result.GetElement(index));
            }
        }

        [Fact]
        public void Vector256UInt16ShuffleOneInputWithDirectVectorAndNoCrossLaneTest()
        {
            Vector256<ushort> result = Vector256.Shuffle(Vector256.Create((ushort)1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16), Vector256.Create((ushort)7, 6, 5, 4, 3, 2, 1, 0, 15, 14, 13, 12, 11, 10, 9, 8));

            for (int index = 0; index < Vector128<ushort>.Count; index++)
            {
                Assert.Equal((ushort)(Vector128<ushort>.Count - index), result.GetElement(index));
            }

            for (int index = Vector128<ushort>.Count; index < Vector256<ushort>.Count; index++)
            {
                Assert.Equal((ushort)(Vector256<ushort>.Count - (index - Vector128<ushort>.Count)), result.GetElement(index));
            }
        }

        [Fact]
        public void Vector256UInt32ShuffleOneInputWithDirectVectorAndNoCrossLaneTest()
        {
            Vector256<uint> result = Vector256.Shuffle(Vector256.Create((uint)1, 2, 3, 4, 5, 6, 7, 8), Vector256.Create((uint)3, 2, 1, 0, 7, 6, 5, 4));

            for (int index = 0; index < Vector128<uint>.Count; index++)
            {
                Assert.Equal((uint)(Vector128<uint>.Count - index), result.GetElement(index));
            }

            for (int index = Vector128<uint>.Count; index < Vector256<uint>.Count; index++)
            {
                Assert.Equal((uint)(Vector256<uint>.Count - (index - Vector128<uint>.Count)), result.GetElement(index));
            }
        }

        [Fact]
        public void Vector256UInt64ShuffleOneInputWithDirectVectorAndNoCrossLaneTest()
        {
            Vector256<ulong> result = Vector256.Shuffle(Vector256.Create((ulong)1, 2, 3, 4), Vector256.Create((ulong)1, 0, 3, 2));

            for (int index = 0; index < Vector128<ulong>.Count; index++)
            {
                Assert.Equal((ulong)(Vector128<ulong>.Count - index), result.GetElement(index));
            }

            for (int index = Vector128<ulong>.Count; index < Vector256<ulong>.Count; index++)
            {
                Assert.Equal((ulong)(Vector256<ulong>.Count - (index - Vector128<ulong>.Count)), result.GetElement(index));
            }
        }

        [Fact]
        public void Vector256ByteShuffleOneInputWithLocalIndicesTest()
        {
            Vector256<byte> vector = Vector256.Create((byte)1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24, 25, 26, 27, 28, 29, 30, 31, 32);
            Vector256<byte> indices = Vector256.Create((byte)31, 30, 29, 28, 27, 26, 25, 24, 23, 22, 21, 20, 19, 18, 17, 16, 15, 14, 13, 12, 11, 10, 9, 8, 7, 6, 5, 4, 3, 2, 1, 0);
            Vector256<byte> result = Vector256.Shuffle(vector, indices);

            for (int index = 0; index < Vector256<byte>.Count; index++)
            {
                Assert.Equal((byte)(Vector256<byte>.Count - index), result.GetElement(index));
            }
        }

        [Fact]
        public void Vector256DoubleShuffleOneInputWithLocalIndicesTest()
        {
            Vector256<double> vector = Vector256.Create((double)1, 2, 3, 4);
            Vector256<long> indices = Vector256.Create((long)3, 2, 1, 0);
            Vector256<double> result = Vector256.Shuffle(vector, indices);

            for (int index = 0; index < Vector256<double>.Count; index++)
            {
                Assert.Equal((double)(Vector256<double>.Count - index), result.GetElement(index));
            }
        }

        [Fact]
        public void Vector256Int16ShuffleOneInputWithLocalIndicesTest()
        {
            Vector256<short> vector = Vector256.Create((short)1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16);
            Vector256<short> indices = Vector256.Create((short)15, 14, 13, 12, 11, 10, 9, 8, 7, 6, 5, 4, 3, 2, 1, 0);
            Vector256<short> result = Vector256.Shuffle(vector, indices);

            for (int index = 0; index < Vector256<short>.Count; index++)
            {
                Assert.Equal((short)(Vector256<short>.Count - index), result.GetElement(index));
            }
        }

        [Fact]
        public void Vector256Int32ShuffleOneInputWithLocalIndicesTest()
        {
            Vector256<int> vector = Vector256.Create((int)1, 2, 3, 4, 5, 6, 7, 8);
            Vector256<int> indices = Vector256.Create((int)7, 6, 5, 4, 3, 2, 1, 0);
            Vector256<int> result = Vector256.Shuffle(vector, indices);

            for (int index = 0; index < Vector256<int>.Count; index++)
            {
                Assert.Equal((int)(Vector256<int>.Count - index), result.GetElement(index));
            }
        }

        [Fact]
        public void Vector256Int64ShuffleOneInputWithLocalIndicesTest()
        {
            Vector256<long> vector = Vector256.Create((long)1, 2, 3, 4);
            Vector256<long> indices = Vector256.Create((long)3, 2, 1, 0);
            Vector256<long> result = Vector256.Shuffle(vector, indices);

            for (int index = 0; index < Vector256<long>.Count; index++)
            {
                Assert.Equal((long)(Vector256<long>.Count - index), result.GetElement(index));
            }
        }

        [Fact]
        public void Vector256SByteShuffleOneInputWithLocalIndicesTest()
        {
            Vector256<sbyte> vector = Vector256.Create((sbyte)1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24, 25, 26, 27, 28, 29, 30, 31, 32);
            Vector256<sbyte> indices = Vector256.Create((sbyte)31, 30, 29, 28, 27, 26, 25, 24, 23, 22, 21, 20, 19, 18, 17, 16, 15, 14, 13, 12, 11, 10, 9, 8, 7, 6, 5, 4, 3, 2, 1, 0);
            Vector256<sbyte> result = Vector256.Shuffle(vector, indices);

            for (int index = 0; index < Vector256<sbyte>.Count; index++)
            {
                Assert.Equal((sbyte)(Vector256<sbyte>.Count - index), result.GetElement(index));
            }
        }

        [Fact]
        public void Vector256SingleShuffleOneInputWithLocalIndicesTest()
        {
            Vector256<float> vector = Vector256.Create((float)1, 2, 3, 4, 5, 6, 7, 8);
            Vector256<int> indices = Vector256.Create((int)7, 6, 5, 4, 3, 2, 1, 0);
            Vector256<float> result = Vector256.Shuffle(vector, indices);

            for (int index = 0; index < Vector256<float>.Count; index++)
            {
                Assert.Equal((float)(Vector256<float>.Count - index), result.GetElement(index));
            }
        }

        [Fact]
        public void Vector256UInt16ShuffleOneInputWithLocalIndicesTest()
        {
            Vector256<ushort> vector = Vector256.Create((ushort)1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16);
            Vector256<ushort> indices = Vector256.Create((ushort)15, 14, 13, 12, 11, 10, 9, 8, 7, 6, 5, 4, 3, 2, 1, 0);
            Vector256<ushort> result = Vector256.Shuffle(vector, indices);

            for (int index = 0; index < Vector256<ushort>.Count; index++)
            {
                Assert.Equal((ushort)(Vector256<ushort>.Count - index), result.GetElement(index));
            }
        }

        [Fact]
        public void Vector256UInt32ShuffleOneInputWithLocalIndicesTest()
        {
            Vector256<uint> vector = Vector256.Create((uint)1, 2, 3, 4, 5, 6, 7, 8);
            Vector256<uint> indices = Vector256.Create((uint)7, 6, 5, 4, 3, 2, 1, 0);
            Vector256<uint> result = Vector256.Shuffle(vector, indices);

            for (int index = 0; index < Vector256<uint>.Count; index++)
            {
                Assert.Equal((uint)(Vector256<uint>.Count - index), result.GetElement(index));
            }
        }

        [Fact]
        public void Vector256UInt64ShuffleOneInputWithLocalIndicesTest()
        {
            Vector256<ulong> vector = Vector256.Create((ulong)1, 2, 3, 4);
            Vector256<ulong> indices = Vector256.Create((ulong)3, 2, 1, 0);
            Vector256<ulong> result = Vector256.Shuffle(vector, indices);

            for (int index = 0; index < Vector256<ulong>.Count; index++)
            {
                Assert.Equal((ulong)(Vector256<ulong>.Count - index), result.GetElement(index));
            }
        }

        [Fact]
        public void Vector256ByteShuffleOneInputWithAllBitsSetIndicesTest()
        {
            Vector256<byte> vector = Vector256.Create((byte)1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24, 25, 26, 27, 28, 29, 30, 31, 32);
            Vector256<byte> result = Vector256.Shuffle(vector, Vector256<byte>.AllBitsSet);

            for (int index = 0; index < Vector256<byte>.Count; index++)
            {
                Assert.Equal((byte)0, result.GetElement(index));
            }
        }

        [Fact]
        public void Vector256DoubleShuffleOneInputWithAllBitsSetIndicesTest()
        {
            Vector256<double> vector = Vector256.Create((double)1, 2, 3, 4);
            Vector256<double> result = Vector256.Shuffle(vector, Vector256<long>.AllBitsSet);

            for (int index = 0; index < Vector256<double>.Count; index++)
            {
                Assert.Equal((double)0, result.GetElement(index));
            }
        }

        [Fact]
        public void Vector256Int16ShuffleOneInputWithAllBitsSetIndicesTest()
        {
            Vector256<short> vector = Vector256.Create((short)1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16);
            Vector256<short> result = Vector256.Shuffle(vector, Vector256<short>.AllBitsSet);

            for (int index = 0; index < Vector256<short>.Count; index++)
            {
                Assert.Equal((short)0, result.GetElement(index));
            }
        }

        [Fact]
        public void Vector256Int32ShuffleOneInputWithAllBitsSetIndicesTest()
        {
            Vector256<int> vector = Vector256.Create((int)1, 2, 3, 4, 5, 6, 7, 8);
            Vector256<int> result = Vector256.Shuffle(vector, Vector256<int>.AllBitsSet);

            for (int index = 0; index < Vector256<int>.Count; index++)
            {
                Assert.Equal((int)0, result.GetElement(index));
            }
        }

        [Fact]
        public void Vector256Int64ShuffleOneInputWithAllBitsSetIndicesTest()
        {
            Vector256<long> vector = Vector256.Create((long)1, 2, 3, 4);
            Vector256<long> result = Vector256.Shuffle(vector, Vector256<long>.AllBitsSet);

            for (int index = 0; index < Vector256<long>.Count; index++)
            {
                Assert.Equal((long)0, result.GetElement(index));
            }
        }

        [Fact]
        public void Vector256SByteShuffleOneInputWithAllBitsSetIndicesTest()
        {
            Vector256<sbyte> vector = Vector256.Create((sbyte)1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24, 25, 26, 27, 28, 29, 30, 31, 32);
            Vector256<sbyte> result = Vector256.Shuffle(vector, Vector256<sbyte>.AllBitsSet);

            for (int index = 0; index < Vector256<sbyte>.Count; index++)
            {
                Assert.Equal((sbyte)0, result.GetElement(index));
            }
        }

        [Fact]
        public void Vector256SingleShuffleOneInputWithAllBitsSetIndicesTest()
        {
            Vector256<float> vector = Vector256.Create((float)1, 2, 3, 4, 5, 6, 7, 8);
            Vector256<float> result = Vector256.Shuffle(vector, Vector256<int>.AllBitsSet);

            for (int index = 0; index < Vector256<float>.Count; index++)
            {
                Assert.Equal((float)0, result.GetElement(index));
            }
        }

        [Fact]
        public void Vector256UInt16ShuffleOneInputWithAllBitsSetIndicesTest()
        {
            Vector256<ushort> vector = Vector256.Create((ushort)1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16);
            Vector256<ushort> result = Vector256.Shuffle(vector, Vector256<ushort>.AllBitsSet);

            for (int index = 0; index < Vector256<ushort>.Count; index++)
            {
                Assert.Equal((ushort)0, result.GetElement(index));
            }
        }

        [Fact]
        public void Vector256UInt32ShuffleOneInputWithAllBitsSetIndicesTest()
        {
            Vector256<uint> vector = Vector256.Create((uint)1, 2, 3, 4, 5, 6, 7, 8);
            Vector256<uint> result = Vector256.Shuffle(vector, Vector256<uint>.AllBitsSet);

            for (int index = 0; index < Vector256<uint>.Count; index++)
            {
                Assert.Equal((uint)0, result.GetElement(index));
            }
        }

        [Fact]
        public void Vector256UInt64ShuffleOneInputWithAllBitsSetIndicesTest()
        {
            Vector256<ulong> vector = Vector256.Create((ulong)1, 2, 3, 4);
            Vector256<ulong> result = Vector256.Shuffle(vector, Vector256<ulong>.AllBitsSet);

            for (int index = 0; index < Vector256<ulong>.Count; index++)
            {
                Assert.Equal((ulong)0, result.GetElement(index));
            }
        }

        [Fact]
        public void Vector256ByteShuffleOneInputWithZeroIndicesTest()
        {
            Vector256<byte> vector = Vector256.Create((byte)1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24, 25, 26, 27, 28, 29, 30, 31, 32);
            Vector256<byte> result = Vector256.Shuffle(vector, Vector256<byte>.Zero);

            for (int index = 0; index < Vector256<byte>.Count; index++)
            {
                Assert.Equal((byte)1, result.GetElement(index));
            }
        }

        [Fact]
        public void Vector256DoubleShuffleOneInputWithZeroIndicesTest()
        {
            Vector256<double> vector = Vector256.Create((double)1, 2, 3, 4);
            Vector256<double> result = Vector256.Shuffle(vector, Vector256<long>.Zero);

            for (int index = 0; index < Vector256<double>.Count; index++)
            {
                Assert.Equal((double)1, result.GetElement(index));
            }
        }

        [Fact]
        public void Vector256Int16ShuffleOneInputWithZeroIndicesTest()
        {
            Vector256<short> vector = Vector256.Create((short)1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16);
            Vector256<short> result = Vector256.Shuffle(vector, Vector256<short>.Zero);

            for (int index = 0; index < Vector256<short>.Count; index++)
            {
                Assert.Equal((short)1, result.GetElement(index));
            }
        }

        [Fact]
        public void Vector256Int32ShuffleOneInputWithZeroIndicesTest()
        {
            Vector256<int> vector = Vector256.Create((int)1, 2, 3, 4, 5, 6, 7, 8);
            Vector256<int> result = Vector256.Shuffle(vector, Vector256<int>.Zero);

            for (int index = 0; index < Vector256<int>.Count; index++)
            {
                Assert.Equal((int)1, result.GetElement(index));
            }
        }

        [Fact]
        public void Vector256Int64ShuffleOneInputWithZeroIndicesTest()
        {
            Vector256<long> vector = Vector256.Create((long)1, 2, 3, 4);
            Vector256<long> result = Vector256.Shuffle(vector, Vector256<long>.Zero);

            for (int index = 0; index < Vector256<long>.Count; index++)
            {
                Assert.Equal((long)1, result.GetElement(index));
            }
        }

        [Fact]
        public void Vector256SByteShuffleOneInputWithZeroIndicesTest()
        {
            Vector256<sbyte> vector = Vector256.Create((sbyte)1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24, 25, 26, 27, 28, 29, 30, 31, 32);
            Vector256<sbyte> result = Vector256.Shuffle(vector, Vector256<sbyte>.Zero);

            for (int index = 0; index < Vector256<sbyte>.Count; index++)
            {
                Assert.Equal((sbyte)1, result.GetElement(index));
            }
        }

        [Fact]
        public void Vector256SingleShuffleOneInputWithZeroIndicesTest()
        {
            Vector256<float> vector = Vector256.Create((float)1, 2, 3, 4, 5, 6, 7, 8);
            Vector256<float> result = Vector256.Shuffle(vector, Vector256<int>.Zero);

            for (int index = 0; index < Vector256<float>.Count; index++)
            {
                Assert.Equal((float)1, result.GetElement(index));
            }
        }

        [Fact]
        public void Vector256UInt16ShuffleOneInputWithZeroIndicesTest()
        {
            Vector256<ushort> vector = Vector256.Create((ushort)1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16);
            Vector256<ushort> result = Vector256.Shuffle(vector, Vector256<ushort>.Zero);

            for (int index = 0; index < Vector256<ushort>.Count; index++)
            {
                Assert.Equal((ushort)1, result.GetElement(index));
            }
        }

        [Fact]
        public void Vector256UInt32ShuffleOneInputWithZeroIndicesTest()
        {
            Vector256<uint> vector = Vector256.Create((uint)1, 2, 3, 4, 5, 6, 7, 8);
            Vector256<uint> result = Vector256.Shuffle(vector, Vector256<uint>.Zero);

            for (int index = 0; index < Vector256<uint>.Count; index++)
            {
                Assert.Equal((uint)1, result.GetElement(index));
            }
        }

        [Fact]
        public void Vector256UInt64ShuffleOneInputWithZeroIndicesTest()
        {
            Vector256<ulong> vector = Vector256.Create((ulong)1, 2, 3, 4);
            Vector256<ulong> result = Vector256.Shuffle(vector, Vector256<ulong>.Zero);

            for (int index = 0; index < Vector256<ulong>.Count; index++)
            {
                Assert.Equal((ulong)1, result.GetElement(index));
            }
        }

        [Fact]
        public void Vector256ByteShuffleNativeOneInputTest()
        {
            Vector256<byte> vector = Vector256.Create((byte)1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24, 25, 26, 27, 28, 29, 30, 31, 32);
            Vector256<byte> result = Vector256.ShuffleNative(vector, Vector256.Create((byte)31, 30, 29, 28, 27, 26, 25, 24, 23, 22, 21, 20, 19, 18, 17, 16, 15, 14, 13, 12, 11, 10, 9, 8, 7, 6, 5, 4, 3, 2, 1, 0));

            for (int index = 0; index < Vector256<byte>.Count; index++)
            {
                Assert.Equal((byte)(Vector256<byte>.Count - index), result.GetElement(index));
            }
        }

        [Fact]
        public void Vector256DoubleShuffleNativeOneInputTest()
        {
            Vector256<double> vector = Vector256.Create((double)1, 2, 3, 4);
            Vector256<double> result = Vector256.ShuffleNative(vector, Vector256.Create((long)3, 2, 1, 0));

            for (int index = 0; index < Vector256<double>.Count; index++)
            {
                Assert.Equal((double)(Vector256<double>.Count - index), result.GetElement(index));
            }
        }

        [Fact]
        public void Vector256Int16ShuffleNativeOneInputTest()
        {
            Vector256<short> vector = Vector256.Create((short)1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16);
            Vector256<short> result = Vector256.ShuffleNative(vector, Vector256.Create((short)15, 14, 13, 12, 11, 10, 9, 8, 7, 6, 5, 4, 3, 2, 1, 0));

            for (int index = 0; index < Vector256<short>.Count; index++)
            {
                Assert.Equal((short)(Vector256<short>.Count - index), result.GetElement(index));
            }
        }

        [Fact]
        public void Vector256Int32ShuffleNativeOneInputTest()
        {
            Vector256<int> vector = Vector256.Create((int)1, 2, 3, 4, 5, 6, 7, 8);
            Vector256<int> result = Vector256.ShuffleNative(vector, Vector256.Create((int)7, 6, 5, 4, 3, 2, 1, 0));

            for (int index = 0; index < Vector256<int>.Count; index++)
            {
                Assert.Equal((int)(Vector256<int>.Count - index), result.GetElement(index));
            }
        }

        [Fact]
        public void Vector256Int64ShuffleNativeOneInputTest()
        {
            Vector256<long> vector = Vector256.Create((long)1, 2, 3, 4);
            Vector256<long> result = Vector256.ShuffleNative(vector, Vector256.Create((long)3, 2, 1, 0));

            for (int index = 0; index < Vector256<long>.Count; index++)
            {
                Assert.Equal((long)(Vector256<long>.Count - index), result.GetElement(index));
            }
        }

        [Fact]
        public void Vector256SByteShuffleNativeOneInputTest()
        {
            Vector256<sbyte> vector = Vector256.Create((sbyte)1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24, 25, 26, 27, 28, 29, 30, 31, 32);
            Vector256<sbyte> result = Vector256.ShuffleNative(vector, Vector256.Create((sbyte)31, 30, 29, 28, 27, 26, 25, 24, 23, 22, 21, 20, 19, 18, 17, 16, 15, 14, 13, 12, 11, 10, 9, 8, 7, 6, 5, 4, 3, 2, 1, 0));

            for (int index = 0; index < Vector256<sbyte>.Count; index++)
            {
                Assert.Equal((sbyte)(Vector256<sbyte>.Count - index), result.GetElement(index));
            }
        }

        [Fact]
        public void Vector256SingleShuffleNativeOneInputTest()
        {
            Vector256<float> vector = Vector256.Create((float)1, 2, 3, 4, 5, 6, 7, 8);
            Vector256<float> result = Vector256.ShuffleNative(vector, Vector256.Create((int)7, 6, 5, 4, 3, 2, 1, 0));

            for (int index = 0; index < Vector256<float>.Count; index++)
            {
                Assert.Equal((float)(Vector256<float>.Count - index), result.GetElement(index));
            }
        }

        [Fact]
        public void Vector256UInt16ShuffleNativeOneInputTest()
        {
            Vector256<ushort> vector = Vector256.Create((ushort)1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16);
            Vector256<ushort> result = Vector256.ShuffleNative(vector, Vector256.Create((ushort)15, 14, 13, 12, 11, 10, 9, 8, 7, 6, 5, 4, 3, 2, 1, 0));

            for (int index = 0; index < Vector256<ushort>.Count; index++)
            {
                Assert.Equal((ushort)(Vector256<ushort>.Count - index), result.GetElement(index));
            }
        }

        [Fact]
        public void Vector256UInt32ShuffleNativeOneInputTest()
        {
            Vector256<uint> vector = Vector256.Create((uint)1, 2, 3, 4, 5, 6, 7, 8);
            Vector256<uint> result = Vector256.ShuffleNative(vector, Vector256.Create((uint)7, 6, 5, 4, 3, 2, 1, 0));

            for (int index = 0; index < Vector256<uint>.Count; index++)
            {
                Assert.Equal((uint)(Vector256<uint>.Count - index), result.GetElement(index));
            }
        }

        [Fact]
        public void Vector256UInt64ShuffleNativeOneInputTest()
        {
            Vector256<ulong> vector = Vector256.Create((ulong)1, 2, 3, 4);
            Vector256<ulong> result = Vector256.ShuffleNative(vector, Vector256.Create((ulong)3, 2, 1, 0));

            for (int index = 0; index < Vector256<ulong>.Count; index++)
            {
                Assert.Equal((ulong)(Vector256<ulong>.Count - index), result.GetElement(index));
            }
        }

        [Fact]
        public void Vector256ByteShuffleNativeOneInputWithDirectVectorTest()
        {
            Vector256<byte> result = Vector256.ShuffleNative(Vector256.Create((byte)1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24, 25, 26, 27, 28, 29, 30, 31, 32), Vector256.Create((byte)31, 30, 29, 28, 27, 26, 25, 24, 23, 22, 21, 20, 19, 18, 17, 16, 15, 14, 13, 12, 11, 10, 9, 8, 7, 6, 5, 4, 3, 2, 1, 0));

            for (int index = 0; index < Vector256<byte>.Count; index++)
            {
                Assert.Equal((byte)(Vector256<byte>.Count - index), result.GetElement(index));
            }
        }

        [Fact]
        public void Vector256DoubleShuffleNativeOneInputWithDirectVectorTest()
        {
            Vector256<double> result = Vector256.ShuffleNative(Vector256.Create((double)1, 2, 3, 4), Vector256.Create((long)3, 2, 1, 0));

            for (int index = 0; index < Vector256<double>.Count; index++)
            {
                Assert.Equal((double)(Vector256<double>.Count - index), result.GetElement(index));
            }
        }

        [Fact]
        public void Vector256Int16ShuffleNativeOneInputWithDirectVectorTest()
        {
            Vector256<short> result = Vector256.ShuffleNative(Vector256.Create((short)1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16), Vector256.Create((short)15, 14, 13, 12, 11, 10, 9, 8, 7, 6, 5, 4, 3, 2, 1, 0));

            for (int index = 0; index < Vector256<short>.Count; index++)
            {
                Assert.Equal((short)(Vector256<short>.Count - index), result.GetElement(index));
            }
        }

        [Fact]
        public void Vector256Int32ShuffleNativeOneInputWithDirectVectorTest()
        {
            Vector256<int> result = Vector256.ShuffleNative(Vector256.Create((int)1, 2, 3, 4, 5, 6, 7, 8), Vector256.Create((int)7, 6, 5, 4, 3, 2, 1, 0));

            for (int index = 0; index < Vector256<int>.Count; index++)
            {
                Assert.Equal((int)(Vector256<int>.Count - index), result.GetElement(index));
            }
        }

        [Fact]
        public void Vector256Int64ShuffleNativeOneInputWithDirectVectorTest()
        {
            Vector256<long> result = Vector256.ShuffleNative(Vector256.Create((long)1, 2, 3, 4), Vector256.Create((long)3, 2, 1, 0));

            for (int index = 0; index < Vector256<long>.Count; index++)
            {
                Assert.Equal((long)(Vector256<long>.Count - index), result.GetElement(index));
            }
        }

        [Fact]
        public void Vector256SByteShuffleNativeOneInputWithDirectVectorTest()
        {
            Vector256<sbyte> result = Vector256.ShuffleNative(Vector256.Create((sbyte)1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24, 25, 26, 27, 28, 29, 30, 31, 32), Vector256.Create((sbyte)31, 30, 29, 28, 27, 26, 25, 24, 23, 22, 21, 20, 19, 18, 17, 16, 15, 14, 13, 12, 11, 10, 9, 8, 7, 6, 5, 4, 3, 2, 1, 0));

            for (int index = 0; index < Vector256<sbyte>.Count; index++)
            {
                Assert.Equal((sbyte)(Vector256<sbyte>.Count - index), result.GetElement(index));
            }
        }

        [Fact]
        public void Vector256SingleShuffleNativeOneInputWithDirectVectorTest()
        {
            Vector256<float> result = Vector256.ShuffleNative(Vector256.Create((float)1, 2, 3, 4, 5, 6, 7, 8), Vector256.Create((int)7, 6, 5, 4, 3, 2, 1, 0));

            for (int index = 0; index < Vector256<float>.Count; index++)
            {
                Assert.Equal((float)(Vector256<float>.Count - index), result.GetElement(index));
            }
        }

        [Fact]
        public void Vector256UInt16ShuffleNativeOneInputWithDirectVectorTest()
        {
            Vector256<ushort> result = Vector256.ShuffleNative(Vector256.Create((ushort)1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16), Vector256.Create((ushort)15, 14, 13, 12, 11, 10, 9, 8, 7, 6, 5, 4, 3, 2, 1, 0));

            for (int index = 0; index < Vector256<ushort>.Count; index++)
            {
                Assert.Equal((ushort)(Vector256<ushort>.Count - index), result.GetElement(index));
            }
        }

        [Fact]
        public void Vector256UInt32ShuffleNativeOneInputWithDirectVectorTest()
        {
            Vector256<uint> result = Vector256.ShuffleNative(Vector256.Create((uint)1, 2, 3, 4, 5, 6, 7, 8), Vector256.Create((uint)7, 6, 5, 4, 3, 2, 1, 0));

            for (int index = 0; index < Vector256<uint>.Count; index++)
            {
                Assert.Equal((uint)(Vector256<uint>.Count - index), result.GetElement(index));
            }
        }

        [Fact]
        public void Vector256UInt64ShuffleNativeOneInputWithDirectVectorTest()
        {
            Vector256<ulong> result = Vector256.ShuffleNative(Vector256.Create((ulong)1, 2, 3, 4), Vector256.Create((ulong)3, 2, 1, 0));

            for (int index = 0; index < Vector256<ulong>.Count; index++)
            {
                Assert.Equal((ulong)(Vector256<ulong>.Count - index), result.GetElement(index));
            }
        }

        [Fact]
        public void Vector256ByteShuffleNativeOneInputWithDirectVectorAndNoCrossLaneTest()
        {
            Vector256<byte> result = Vector256.ShuffleNative(Vector256.Create((byte)1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24, 25, 26, 27, 28, 29, 30, 31, 32), Vector256.Create((byte)15, 14, 13, 12, 11, 10, 9, 8, 7, 6, 5, 4, 3, 2, 1, 0, 31, 30, 29, 28, 27, 26, 25, 24, 23, 22, 21, 20, 19, 18, 17, 16));

            for (int index = 0; index < Vector128<byte>.Count; index++)
            {
                Assert.Equal((byte)(Vector128<byte>.Count - index), result.GetElement(index));
            }

            for (int index = Vector128<byte>.Count; index < Vector256<byte>.Count; index++)
            {
                Assert.Equal((byte)(Vector256<byte>.Count - (index - Vector128<byte>.Count)), result.GetElement(index));
            }
        }

        [Fact]
        public void Vector256DoubleShuffleNativeOneInputWithDirectVectorAndNoCrossLaneTest()
        {
            Vector256<double> result = Vector256.ShuffleNative(Vector256.Create((double)1, 2, 3, 4), Vector256.Create((long)1, 0, 3, 2));

            for (int index = 0; index < Vector128<double>.Count; index++)
            {
                Assert.Equal((double)(Vector128<double>.Count - index), result.GetElement(index));
            }

            for (int index = Vector128<double>.Count; index < Vector256<double>.Count; index++)
            {
                Assert.Equal((double)(Vector256<double>.Count - (index - Vector128<double>.Count)), result.GetElement(index));
            }
        }

        [Fact]
        public void Vector256Int16ShuffleNativeOneInputWithDirectVectorAndNoCrossLaneTest()
        {
            Vector256<short> result = Vector256.ShuffleNative(Vector256.Create((short)1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16), Vector256.Create((short)7, 6, 5, 4, 3, 2, 1, 0, 15, 14, 13, 12, 11, 10, 9, 8));

            for (int index = 0; index < Vector128<short>.Count; index++)
            {
                Assert.Equal((short)(Vector128<short>.Count - index), result.GetElement(index));
            }

            for (int index = Vector128<short>.Count; index < Vector256<short>.Count; index++)
            {
                Assert.Equal((short)(Vector256<short>.Count - (index - Vector128<short>.Count)), result.GetElement(index));
            }
        }

        [Fact]
        public void Vector256Int32ShuffleNativeOneInputWithDirectVectorAndNoCrossLaneTest()
        {
            Vector256<int> result = Vector256.ShuffleNative(Vector256.Create((int)1, 2, 3, 4, 5, 6, 7, 8), Vector256.Create((int)3, 2, 1, 0, 7, 6, 5, 4));

            for (int index = 0; index < Vector128<int>.Count; index++)
            {
                Assert.Equal((int)(Vector128<int>.Count - index), result.GetElement(index));
            }

            for (int index = Vector128<int>.Count; index < Vector256<int>.Count; index++)
            {
                Assert.Equal((int)(Vector256<int>.Count - (index - Vector128<int>.Count)), result.GetElement(index));
            }
        }

        [Fact]
        public void Vector256Int64ShuffleNativeOneInputWithDirectVectorAndNoCrossLaneTest()
        {
            Vector256<long> result = Vector256.ShuffleNative(Vector256.Create((long)1, 2, 3, 4), Vector256.Create((long)1, 0, 3, 2));

            for (int index = 0; index < Vector128<long>.Count; index++)
            {
                Assert.Equal((long)(Vector128<long>.Count - index), result.GetElement(index));
            }

            for (int index = Vector128<long>.Count; index < Vector256<long>.Count; index++)
            {
                Assert.Equal((long)(Vector256<long>.Count - (index - Vector128<long>.Count)), result.GetElement(index));
            }
        }

        [Fact]
        public void Vector256SByteShuffleNativeOneInputWithDirectVectorAndNoCrossLaneTest()
        {
            Vector256<sbyte> result = Vector256.ShuffleNative(Vector256.Create((sbyte)1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24, 25, 26, 27, 28, 29, 30, 31, 32), Vector256.Create((sbyte)15, 14, 13, 12, 11, 10, 9, 8, 7, 6, 5, 4, 3, 2, 1, 0, 31, 30, 29, 28, 27, 26, 25, 24, 23, 22, 21, 20, 19, 18, 17, 16));

            for (int index = 0; index < Vector128<sbyte>.Count; index++)
            {
                Assert.Equal((sbyte)(Vector128<sbyte>.Count - index), result.GetElement(index));
            }

            for (int index = Vector128<sbyte>.Count; index < Vector256<sbyte>.Count; index++)
            {
                Assert.Equal((sbyte)(Vector256<sbyte>.Count - (index - Vector128<sbyte>.Count)), result.GetElement(index));
            }
        }

        [Fact]
        public void Vector256SingleShuffleNativeOneInputWithDirectVectorAndNoCrossLaneTest()
        {
            Vector256<float> result = Vector256.ShuffleNative(Vector256.Create((float)1, 2, 3, 4, 5, 6, 7, 8), Vector256.Create((int)3, 2, 1, 0, 7, 6, 5, 4));

            for (int index = 0; index < Vector128<float>.Count; index++)
            {
                Assert.Equal((float)(Vector128<float>.Count - index), result.GetElement(index));
            }

            for (int index = Vector128<float>.Count; index < Vector256<float>.Count; index++)
            {
                Assert.Equal((float)(Vector256<float>.Count - (index - Vector128<float>.Count)), result.GetElement(index));
            }
        }

        [Fact]
        public void Vector256UInt16ShuffleNativeOneInputWithDirectVectorAndNoCrossLaneTest()
        {
            Vector256<ushort> result = Vector256.ShuffleNative(Vector256.Create((ushort)1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16), Vector256.Create((ushort)7, 6, 5, 4, 3, 2, 1, 0, 15, 14, 13, 12, 11, 10, 9, 8));

            for (int index = 0; index < Vector128<ushort>.Count; index++)
            {
                Assert.Equal((ushort)(Vector128<ushort>.Count - index), result.GetElement(index));
            }

            for (int index = Vector128<ushort>.Count; index < Vector256<ushort>.Count; index++)
            {
                Assert.Equal((ushort)(Vector256<ushort>.Count - (index - Vector128<ushort>.Count)), result.GetElement(index));
            }
        }

        [Fact]
        public void Vector256UInt32ShuffleNativeOneInputWithDirectVectorAndNoCrossLaneTest()
        {
            Vector256<uint> result = Vector256.ShuffleNative(Vector256.Create((uint)1, 2, 3, 4, 5, 6, 7, 8), Vector256.Create((uint)3, 2, 1, 0, 7, 6, 5, 4));

            for (int index = 0; index < Vector128<uint>.Count; index++)
            {
                Assert.Equal((uint)(Vector128<uint>.Count - index), result.GetElement(index));
            }

            for (int index = Vector128<uint>.Count; index < Vector256<uint>.Count; index++)
            {
                Assert.Equal((uint)(Vector256<uint>.Count - (index - Vector128<uint>.Count)), result.GetElement(index));
            }
        }

        [Fact]
        public void Vector256UInt64ShuffleNativeOneInputWithDirectVectorAndNoCrossLaneTest()
        {
            Vector256<ulong> result = Vector256.ShuffleNative(Vector256.Create((ulong)1, 2, 3, 4), Vector256.Create((ulong)1, 0, 3, 2));

            for (int index = 0; index < Vector128<ulong>.Count; index++)
            {
                Assert.Equal((ulong)(Vector128<ulong>.Count - index), result.GetElement(index));
            }

            for (int index = Vector128<ulong>.Count; index < Vector256<ulong>.Count; index++)
            {
                Assert.Equal((ulong)(Vector256<ulong>.Count - (index - Vector128<ulong>.Count)), result.GetElement(index));
            }
        }

        [Fact]
        public void Vector256ByteShuffleNativeOneInputWithLocalIndicesTest()
        {
            Vector256<byte> vector = Vector256.Create((byte)1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24, 25, 26, 27, 28, 29, 30, 31, 32);
            Vector256<byte> indices = Vector256.Create((byte)31, 30, 29, 28, 27, 26, 25, 24, 23, 22, 21, 20, 19, 18, 17, 16, 15, 14, 13, 12, 11, 10, 9, 8, 7, 6, 5, 4, 3, 2, 1, 0);
            Vector256<byte> result = Vector256.ShuffleNative(vector, indices);

            for (int index = 0; index < Vector256<byte>.Count; index++)
            {
                Assert.Equal((byte)(Vector256<byte>.Count - index), result.GetElement(index));
            }
        }

        [Fact]
        public void Vector256DoubleShuffleNativeOneInputWithLocalIndicesTest()
        {
            Vector256<double> vector = Vector256.Create((double)1, 2, 3, 4);
            Vector256<long> indices = Vector256.Create((long)3, 2, 1, 0);
            Vector256<double> result = Vector256.ShuffleNative(vector, indices);

            for (int index = 0; index < Vector256<double>.Count; index++)
            {
                Assert.Equal((double)(Vector256<double>.Count - index), result.GetElement(index));
            }
        }

        [Fact]
        public void Vector256Int16ShuffleNativeOneInputWithLocalIndicesTest()
        {
            Vector256<short> vector = Vector256.Create((short)1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16);
            Vector256<short> indices = Vector256.Create((short)15, 14, 13, 12, 11, 10, 9, 8, 7, 6, 5, 4, 3, 2, 1, 0);
            Vector256<short> result = Vector256.ShuffleNative(vector, indices);

            for (int index = 0; index < Vector256<short>.Count; index++)
            {
                Assert.Equal((short)(Vector256<short>.Count - index), result.GetElement(index));
            }
        }

        [Fact]
        public void Vector256Int32ShuffleNativeOneInputWithLocalIndicesTest()
        {
            Vector256<int> vector = Vector256.Create((int)1, 2, 3, 4, 5, 6, 7, 8);
            Vector256<int> indices = Vector256.Create((int)7, 6, 5, 4, 3, 2, 1, 0);
            Vector256<int> result = Vector256.ShuffleNative(vector, indices);

            for (int index = 0; index < Vector256<int>.Count; index++)
            {
                Assert.Equal((int)(Vector256<int>.Count - index), result.GetElement(index));
            }
        }

        [Fact]
        public void Vector256Int64ShuffleNativeOneInputWithLocalIndicesTest()
        {
            Vector256<long> vector = Vector256.Create((long)1, 2, 3, 4);
            Vector256<long> indices = Vector256.Create((long)3, 2, 1, 0);
            Vector256<long> result = Vector256.ShuffleNative(vector, indices);

            for (int index = 0; index < Vector256<long>.Count; index++)
            {
                Assert.Equal((long)(Vector256<long>.Count - index), result.GetElement(index));
            }
        }

        [Fact]
        public void Vector256SByteShuffleNativeOneInputWithLocalIndicesTest()
        {
            Vector256<sbyte> vector = Vector256.Create((sbyte)1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24, 25, 26, 27, 28, 29, 30, 31, 32);
            Vector256<sbyte> indices = Vector256.Create((sbyte)31, 30, 29, 28, 27, 26, 25, 24, 23, 22, 21, 20, 19, 18, 17, 16, 15, 14, 13, 12, 11, 10, 9, 8, 7, 6, 5, 4, 3, 2, 1, 0);
            Vector256<sbyte> result = Vector256.ShuffleNative(vector, indices);

            for (int index = 0; index < Vector256<sbyte>.Count; index++)
            {
                Assert.Equal((sbyte)(Vector256<sbyte>.Count - index), result.GetElement(index));
            }
        }

        [Fact]
        public void Vector256SingleShuffleNativeOneInputWithLocalIndicesTest()
        {
            Vector256<float> vector = Vector256.Create((float)1, 2, 3, 4, 5, 6, 7, 8);
            Vector256<int> indices = Vector256.Create((int)7, 6, 5, 4, 3, 2, 1, 0);
            Vector256<float> result = Vector256.ShuffleNative(vector, indices);

            for (int index = 0; index < Vector256<float>.Count; index++)
            {
                Assert.Equal((float)(Vector256<float>.Count - index), result.GetElement(index));
            }
        }

        [Fact]
        public void Vector256UInt16ShuffleNativeOneInputWithLocalIndicesTest()
        {
            Vector256<ushort> vector = Vector256.Create((ushort)1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16);
            Vector256<ushort> indices = Vector256.Create((ushort)15, 14, 13, 12, 11, 10, 9, 8, 7, 6, 5, 4, 3, 2, 1, 0);
            Vector256<ushort> result = Vector256.ShuffleNative(vector, indices);

            for (int index = 0; index < Vector256<ushort>.Count; index++)
            {
                Assert.Equal((ushort)(Vector256<ushort>.Count - index), result.GetElement(index));
            }
        }

        [Fact]
        public void Vector256UInt32ShuffleNativeOneInputWithLocalIndicesTest()
        {
            Vector256<uint> vector = Vector256.Create((uint)1, 2, 3, 4, 5, 6, 7, 8);
            Vector256<uint> indices = Vector256.Create((uint)7, 6, 5, 4, 3, 2, 1, 0);
            Vector256<uint> result = Vector256.ShuffleNative(vector, indices);

            for (int index = 0; index < Vector256<uint>.Count; index++)
            {
                Assert.Equal((uint)(Vector256<uint>.Count - index), result.GetElement(index));
            }
        }

        [Fact]
        public void Vector256UInt64ShuffleNativeOneInputWithLocalIndicesTest()
        {
            Vector256<ulong> vector = Vector256.Create((ulong)1, 2, 3, 4);
            Vector256<ulong> indices = Vector256.Create((ulong)3, 2, 1, 0);
            Vector256<ulong> result = Vector256.ShuffleNative(vector, indices);

            for (int index = 0; index < Vector256<ulong>.Count; index++)
            {
                Assert.Equal((ulong)(Vector256<ulong>.Count - index), result.GetElement(index));
            }
        }

        [Fact]
        public void Vector256ByteShuffleNativeOneInputWithZeroIndicesTest()
        {
            Vector256<byte> vector = Vector256.Create((byte)1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24, 25, 26, 27, 28, 29, 30, 31, 32);
            Vector256<byte> result = Vector256.ShuffleNative(vector, Vector256<byte>.Zero);

            for (int index = 0; index < Vector256<byte>.Count; index++)
            {
                Assert.Equal((byte)1, result.GetElement(index));
            }
        }

        [Fact]
        public void Vector256DoubleShuffleNativeOneInputWithZeroIndicesTest()
        {
            Vector256<double> vector = Vector256.Create((double)1, 2, 3, 4);
            Vector256<double> result = Vector256.ShuffleNative(vector, Vector256<long>.Zero);

            for (int index = 0; index < Vector256<double>.Count; index++)
            {
                Assert.Equal((double)1, result.GetElement(index));
            }
        }

        [Fact]
        public void Vector256Int16ShuffleNativeOneInputWithZeroIndicesTest()
        {
            Vector256<short> vector = Vector256.Create((short)1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16);
            Vector256<short> result = Vector256.ShuffleNative(vector, Vector256<short>.Zero);

            for (int index = 0; index < Vector256<short>.Count; index++)
            {
                Assert.Equal((short)1, result.GetElement(index));
            }
        }

        [Fact]
        public void Vector256Int32ShuffleNativeOneInputWithZeroIndicesTest()
        {
            Vector256<int> vector = Vector256.Create((int)1, 2, 3, 4, 5, 6, 7, 8);
            Vector256<int> result = Vector256.ShuffleNative(vector, Vector256<int>.Zero);

            for (int index = 0; index < Vector256<int>.Count; index++)
            {
                Assert.Equal((int)1, result.GetElement(index));
            }
        }

        [Fact]
        public void Vector256Int64ShuffleNativeOneInputWithZeroIndicesTest()
        {
            Vector256<long> vector = Vector256.Create((long)1, 2, 3, 4);
            Vector256<long> result = Vector256.ShuffleNative(vector, Vector256<long>.Zero);

            for (int index = 0; index < Vector256<long>.Count; index++)
            {
                Assert.Equal((long)1, result.GetElement(index));
            }
        }

        [Fact]
        public void Vector256SByteShuffleNativeOneInputWithZeroIndicesTest()
        {
            Vector256<sbyte> vector = Vector256.Create((sbyte)1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24, 25, 26, 27, 28, 29, 30, 31, 32);
            Vector256<sbyte> result = Vector256.ShuffleNative(vector, Vector256<sbyte>.Zero);

            for (int index = 0; index < Vector256<sbyte>.Count; index++)
            {
                Assert.Equal((sbyte)1, result.GetElement(index));
            }
        }

        [Fact]
        public void Vector256SingleShuffleNativeOneInputWithZeroIndicesTest()
        {
            Vector256<float> vector = Vector256.Create((float)1, 2, 3, 4, 5, 6, 7, 8);
            Vector256<float> result = Vector256.ShuffleNative(vector, Vector256<int>.Zero);

            for (int index = 0; index < Vector256<float>.Count; index++)
            {
                Assert.Equal((float)1, result.GetElement(index));
            }
        }

        [Fact]
        public void Vector256UInt16ShuffleNativeOneInputWithZeroIndicesTest()
        {
            Vector256<ushort> vector = Vector256.Create((ushort)1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16);
            Vector256<ushort> result = Vector256.ShuffleNative(vector, Vector256<ushort>.Zero);

            for (int index = 0; index < Vector256<ushort>.Count; index++)
            {
                Assert.Equal((ushort)1, result.GetElement(index));
            }
        }

        [Fact]
        public void Vector256UInt32ShuffleNativeOneInputWithZeroIndicesTest()
        {
            Vector256<uint> vector = Vector256.Create((uint)1, 2, 3, 4, 5, 6, 7, 8);
            Vector256<uint> result = Vector256.ShuffleNative(vector, Vector256<uint>.Zero);

            for (int index = 0; index < Vector256<uint>.Count; index++)
            {
                Assert.Equal((uint)1, result.GetElement(index));
            }
        }

        [Fact]
        public void Vector256UInt64ShuffleNativeOneInputWithZeroIndicesTest()
        {
            Vector256<ulong> vector = Vector256.Create((ulong)1, 2, 3, 4);
            Vector256<ulong> result = Vector256.ShuffleNative(vector, Vector256<ulong>.Zero);

            for (int index = 0; index < Vector256<ulong>.Count; index++)
            {
                Assert.Equal((ulong)1, result.GetElement(index));
            }
        }

        [Fact]
        public unsafe void Vector256ByteStoreTest()
        {
            byte* value = stackalloc byte[32] {
                0,
                1,
                2,
                3,
                4,
                5,
                6,
                7,
                8,
                9,
                10,
                11,
                12,
                13,
                14,
                15,
                16,
                17,
                18,
                19,
                20,
                21,
                22,
                23,
                24,
                25,
                26,
                27,
                28,
                29,
                30,
                31,
            };

            Vector256.Create((byte)0x1).Store(value);

            for (int index = 0; index < Vector256<byte>.Count; index++)
            {
                Assert.Equal((byte)0x1, value[index]);
            }
        }

        [Fact]
        public unsafe void Vector256DoubleStoreTest()
        {
            double* value = stackalloc double[4] {
                0,
                1,
                2,
                3,
            };

            Vector256.Create((double)0x1).Store(value);

            for (int index = 0; index < Vector256<double>.Count; index++)
            {
                Assert.Equal((double)0x1, value[index]);
            }
        }

        [Fact]
        public unsafe void Vector256Int16StoreTest()
        {
            short* value = stackalloc short[16] {
                0,
                1,
                2,
                3,
                4,
                5,
                6,
                7,
                8,
                9,
                10,
                11,
                12,
                13,
                14,
                15,
            };

            Vector256.Create((short)0x1).Store(value);

            for (int index = 0; index < Vector256<short>.Count; index++)
            {
                Assert.Equal((short)0x1, value[index]);
            }
        }

        [Fact]
        public unsafe void Vector256Int32StoreTest()
        {
            int* value = stackalloc int[8] {
                0,
                1,
                2,
                3,
                4,
                5,
                6,
                7,
            };

            Vector256.Create((int)0x1).Store(value);

            for (int index = 0; index < Vector256<int>.Count; index++)
            {
                Assert.Equal((int)0x1, value[index]);
            }
        }

        [Fact]
        public unsafe void Vector256Int64StoreTest()
        {
            long* value = stackalloc long[4] {
                0,
                1,
                2,
                3,
            };

            Vector256.Create((long)0x1).Store(value);

            for (int index = 0; index < Vector256<long>.Count; index++)
            {
                Assert.Equal((long)0x1, value[index]);
            }
        }

        [Fact]
        public unsafe void Vector256NIntStoreTest()
        {
            if (Environment.Is64BitProcess)
            {
                nint* value = stackalloc nint[4] {
                    0,
                    1,
                    2,
                    3,
                };

                Vector256.Create((nint)0x1).Store(value);

                for (int index = 0; index < Vector256<nint>.Count; index++)
                {
                    Assert.Equal((nint)0x1, value[index]);
                }
            }
            else
            {
                nint* value = stackalloc nint[8] {
                    0,
                    1,
                    2,
                    3,
                    4,
                    5,
                    6,
                    7,
                };

                Vector256.Create((nint)0x1).Store(value);

                for (int index = 0; index < Vector256<nint>.Count; index++)
                {
                    Assert.Equal((nint)0x1, value[index]);
                }
            }
        }

        [Fact]
        public unsafe void Vector256NUIntStoreTest()
        {
            if (Environment.Is64BitProcess)
            {
                nuint* value = stackalloc nuint[4] {
                    0,
                    1,
                    2,
                    3,
                };

                Vector256.Create((nuint)0x1).Store(value);

                for (int index = 0; index < Vector256<nuint>.Count; index++)
                {
                    Assert.Equal((nuint)0x1, value[index]);
                }
            }
            else
            {
                nuint* value = stackalloc nuint[8] {
                    0,
                    1,
                    2,
                    3,
                    4,
                    5,
                    6,
                    7,
                };

                Vector256.Create((nuint)0x1).Store(value);

                for (int index = 0; index < Vector256<nuint>.Count; index++)
                {
                    Assert.Equal((nuint)0x1, value[index]);
                }
            }
        }

        [Fact]
        public unsafe void Vector256SByteStoreTest()
        {
            sbyte* value = stackalloc sbyte[32] {
                0,
                1,
                2,
                3,
                4,
                5,
                6,
                7,
                8,
                9,
                10,
                11,
                12,
                13,
                14,
                15,
                16,
                17,
                18,
                19,
                20,
                21,
                22,
                23,
                24,
                25,
                26,
                27,
                28,
                29,
                30,
                31,
            };

            Vector256.Create((sbyte)0x1).Store(value);

            for (int index = 0; index < Vector256<sbyte>.Count; index++)
            {
                Assert.Equal((sbyte)0x1, value[index]);
            }
        }

        [Fact]
        public unsafe void Vector256SingleStoreTest()
        {
            float* value = stackalloc float[8] {
                0,
                1,
                2,
                3,
                4,
                5,
                6,
                7,
            };

            Vector256.Create((float)0x1).Store(value);

            for (int index = 0; index < Vector256<float>.Count; index++)
            {
                Assert.Equal((float)0x1, value[index]);
            }
        }

        [Fact]
        public unsafe void Vector256UInt16StoreTest()
        {
            ushort* value = stackalloc ushort[16] {
                0,
                1,
                2,
                3,
                4,
                5,
                6,
                7,
                8,
                9,
                10,
                11,
                12,
                13,
                14,
                15,
            };

            Vector256.Create((ushort)0x1).Store(value);

            for (int index = 0; index < Vector256<ushort>.Count; index++)
            {
                Assert.Equal((ushort)0x1, value[index]);
            }
        }

        [Fact]
        public unsafe void Vector256UInt32StoreTest()
        {
            uint* value = stackalloc uint[8] {
                0,
                1,
                2,
                3,
                4,
                5,
                6,
                7,
            };

            Vector256.Create((uint)0x1).Store(value);

            for (int index = 0; index < Vector256<uint>.Count; index++)
            {
                Assert.Equal((uint)0x1, value[index]);
            }
        }

        [Fact]
        public unsafe void Vector256UInt64StoreTest()
        {
            ulong* value = stackalloc ulong[4] {
                0,
                1,
                2,
                3,
            };

            Vector256.Create((ulong)0x1).Store(value);

            for (int index = 0; index < Vector256<ulong>.Count; index++)
            {
                Assert.Equal((ulong)0x1, value[index]);
            }
        }

        [Fact]
        public unsafe void Vector256ByteStoreAlignedTest()
        {
            byte* value = null;

            try
            {
                value = (byte*)NativeMemory.AlignedAlloc(byteCount: 32, alignment: 32);

                value[0] = 0;
                value[1] = 1;
                value[2] = 2;
                value[3] = 3;
                value[4] = 4;
                value[5] = 5;
                value[6] = 6;
                value[7] = 7;
                value[8] = 8;
                value[9] = 9;
                value[10] = 10;
                value[11] = 11;
                value[12] = 12;
                value[13] = 13;
                value[14] = 14;
                value[15] = 15;
                value[16] = 16;
                value[17] = 17;
                value[18] = 18;
                value[19] = 19;
                value[20] = 20;
                value[21] = 21;
                value[22] = 22;
                value[23] = 23;
                value[24] = 24;
                value[25] = 25;
                value[26] = 26;
                value[27] = 27;
                value[28] = 28;
                value[29] = 29;
                value[30] = 30;
                value[31] = 31;

                Vector256.Create((byte)0x1).StoreAligned(value);

                for (int index = 0; index < Vector256<byte>.Count; index++)
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
        public unsafe void Vector256DoubleStoreAlignedTest()
        {
            double* value = null;

            try
            {
                value = (double*)NativeMemory.AlignedAlloc(byteCount: 32, alignment: 32);

                value[0] = 0;
                value[1] = 1;
                value[2] = 2;
                value[3] = 3;

                Vector256.Create((double)0x1).StoreAligned(value);

                for (int index = 0; index < Vector256<double>.Count; index++)
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
        public unsafe void Vector256Int16StoreAlignedTest()
        {
            short* value = null;

            try
            {
                value = (short*)NativeMemory.AlignedAlloc(byteCount: 32, alignment: 32);

                value[0] = 0;
                value[1] = 1;
                value[2] = 2;
                value[3] = 3;
                value[4] = 4;
                value[5] = 5;
                value[6] = 6;
                value[7] = 7;
                value[8] = 8;
                value[9] = 9;
                value[10] = 10;
                value[11] = 11;
                value[12] = 12;
                value[13] = 13;
                value[14] = 14;
                value[15] = 15;

                Vector256.Create((short)0x1).StoreAligned(value);

                for (int index = 0; index < Vector256<short>.Count; index++)
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
        public unsafe void Vector256Int32StoreAlignedTest()
        {
            int* value = null;

            try
            {
                value = (int*)NativeMemory.AlignedAlloc(byteCount: 32, alignment: 32);

                value[0] = 0;
                value[1] = 1;
                value[2] = 2;
                value[3] = 3;
                value[4] = 4;
                value[5] = 5;
                value[6] = 6;
                value[7] = 7;

                Vector256.Create((int)0x1).StoreAligned(value);

                for (int index = 0; index < Vector256<int>.Count; index++)
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
        public unsafe void Vector256Int64StoreAlignedTest()
        {
            long* value = null;

            try
            {
                value = (long*)NativeMemory.AlignedAlloc(byteCount: 32, alignment: 32);

                value[0] = 0;
                value[1] = 1;
                value[2] = 2;
                value[3] = 3;

                Vector256.Create((long)0x1).StoreAligned(value);

                for (int index = 0; index < Vector256<long>.Count; index++)
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
        public unsafe void Vector256NIntStoreAlignedTest()
        {
            nint* value = null;

            try
            {
                value = (nint*)NativeMemory.AlignedAlloc(byteCount: 32, alignment: 32);

                if (Environment.Is64BitProcess)
                {
                    value[0] = 0;
                    value[1] = 1;
                    value[2] = 2;
                    value[3] = 3;
                }
                else
                {
                    value[0] = 0;
                    value[1] = 1;
                    value[2] = 2;
                    value[3] = 3;
                    value[4] = 4;
                    value[5] = 5;
                    value[6] = 6;
                    value[7] = 7;
                }

                Vector256.Create((nint)0x1).StoreAligned(value);

                for (int index = 0; index < Vector256<nint>.Count; index++)
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
        public unsafe void Vector256NUIntStoreAlignedTest()
        {
            nuint* value = null;

            try
            {
                value = (nuint*)NativeMemory.AlignedAlloc(byteCount: 32, alignment: 32);

                if (Environment.Is64BitProcess)
                {
                    value[0] = 0;
                    value[1] = 1;
                    value[2] = 2;
                    value[3] = 3;
                }
                else
                {
                    value[0] = 0;
                    value[1] = 1;
                    value[2] = 2;
                    value[3] = 3;
                    value[4] = 4;
                    value[5] = 5;
                    value[6] = 6;
                    value[7] = 7;
                }

                Vector256.Create((nuint)0x1).StoreAligned(value);

                for (int index = 0; index < Vector256<nuint>.Count; index++)
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
        public unsafe void Vector256SByteStoreAlignedTest()
        {
            sbyte* value = null;

            try
            {
                value = (sbyte*)NativeMemory.AlignedAlloc(byteCount: 32, alignment: 32);

                value[0] = 0;
                value[1] = 1;
                value[2] = 2;
                value[3] = 3;
                value[4] = 4;
                value[5] = 5;
                value[6] = 6;
                value[7] = 7;
                value[8] = 8;
                value[9] = 9;
                value[10] = 10;
                value[11] = 11;
                value[12] = 12;
                value[13] = 13;
                value[14] = 14;
                value[15] = 15;
                value[16] = 16;
                value[17] = 17;
                value[18] = 18;
                value[19] = 19;
                value[20] = 20;
                value[21] = 21;
                value[22] = 22;
                value[23] = 23;
                value[24] = 24;
                value[25] = 25;
                value[26] = 26;
                value[27] = 27;
                value[28] = 28;
                value[29] = 29;
                value[30] = 30;
                value[31] = 31;

                Vector256.Create((sbyte)0x1).StoreAligned(value);

                for (int index = 0; index < Vector256<sbyte>.Count; index++)
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
        public unsafe void Vector256SingleStoreAlignedTest()
        {
            float* value = null;

            try
            {
                value = (float*)NativeMemory.AlignedAlloc(byteCount: 32, alignment: 32);

                value[0] = 0;
                value[1] = 1;
                value[2] = 2;
                value[3] = 3;
                value[4] = 4;
                value[5] = 5;
                value[6] = 6;
                value[7] = 7;

                Vector256.Create((float)0x1).StoreAligned(value);

                for (int index = 0; index < Vector256<float>.Count; index++)
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
        public unsafe void Vector256UInt16StoreAlignedTest()
        {
            ushort* value = null;

            try
            {
                value = (ushort*)NativeMemory.AlignedAlloc(byteCount: 32, alignment: 32);

                value[0] = 0;
                value[1] = 1;
                value[2] = 2;
                value[3] = 3;
                value[4] = 4;
                value[5] = 5;
                value[6] = 6;
                value[7] = 7;
                value[8] = 8;
                value[9] = 9;
                value[10] = 10;
                value[11] = 11;
                value[12] = 12;
                value[13] = 13;
                value[14] = 14;
                value[15] = 15;

                Vector256.Create((ushort)0x1).StoreAligned(value);

                for (int index = 0; index < Vector256<ushort>.Count; index++)
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
        public unsafe void Vector256UInt32StoreAlignedTest()
        {
            uint* value = null;

            try
            {
                value = (uint*)NativeMemory.AlignedAlloc(byteCount: 32, alignment: 32);

                value[0] = 0;
                value[1] = 1;
                value[2] = 2;
                value[3] = 3;
                value[4] = 4;
                value[5] = 5;
                value[6] = 6;
                value[7] = 7;

                Vector256.Create((uint)0x1).StoreAligned(value);

                for (int index = 0; index < Vector256<uint>.Count; index++)
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
        public unsafe void Vector256UInt64StoreAlignedTest()
        {
            ulong* value = null;

            try
            {
                value = (ulong*)NativeMemory.AlignedAlloc(byteCount: 32, alignment: 32);

                value[0] = 0;
                value[1] = 1;
                value[2] = 2;
                value[3] = 3;

                Vector256.Create((ulong)0x1).StoreAligned(value);

                for (int index = 0; index < Vector256<ulong>.Count; index++)
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
        public unsafe void Vector256ByteStoreAlignedNonTemporalTest()
        {
            byte* value = null;

            try
            {
                value = (byte*)NativeMemory.AlignedAlloc(byteCount: 32, alignment: 32);

                value[0] = 0;
                value[1] = 1;
                value[2] = 2;
                value[3] = 3;
                value[4] = 4;
                value[5] = 5;
                value[6] = 6;
                value[7] = 7;
                value[8] = 8;
                value[9] = 9;
                value[10] = 10;
                value[11] = 11;
                value[12] = 12;
                value[13] = 13;
                value[14] = 14;
                value[15] = 15;
                value[16] = 16;
                value[17] = 17;
                value[18] = 18;
                value[19] = 19;
                value[20] = 20;
                value[21] = 21;
                value[22] = 22;
                value[23] = 23;
                value[24] = 24;
                value[25] = 25;
                value[26] = 26;
                value[27] = 27;
                value[28] = 28;
                value[29] = 29;
                value[30] = 30;
                value[31] = 31;

                Vector256.Create((byte)0x1).StoreAlignedNonTemporal(value);

                for (int index = 0; index < Vector256<byte>.Count; index++)
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
        public unsafe void Vector256DoubleStoreAlignedNonTemporalTest()
        {
            double* value = null;

            try
            {
                value = (double*)NativeMemory.AlignedAlloc(byteCount: 32, alignment: 32);

                value[0] = 0;
                value[1] = 1;
                value[2] = 2;
                value[3] = 3;

                Vector256.Create((double)0x1).StoreAlignedNonTemporal(value);

                for (int index = 0; index < Vector256<double>.Count; index++)
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
        public unsafe void Vector256Int16StoreAlignedNonTemporalTest()
        {
            short* value = null;

            try
            {
                value = (short*)NativeMemory.AlignedAlloc(byteCount: 32, alignment: 32);

                value[0] = 0;
                value[1] = 1;
                value[2] = 2;
                value[3] = 3;
                value[4] = 4;
                value[5] = 5;
                value[6] = 6;
                value[7] = 7;
                value[8] = 8;
                value[9] = 9;
                value[10] = 10;
                value[11] = 11;
                value[12] = 12;
                value[13] = 13;
                value[14] = 14;
                value[15] = 15;

                Vector256.Create((short)0x1).StoreAlignedNonTemporal(value);

                for (int index = 0; index < Vector256<short>.Count; index++)
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
        public unsafe void Vector256Int32StoreAlignedNonTemporalTest()
        {
            int* value = null;

            try
            {
                value = (int*)NativeMemory.AlignedAlloc(byteCount: 32, alignment: 32);

                value[0] = 0;
                value[1] = 1;
                value[2] = 2;
                value[3] = 3;
                value[4] = 4;
                value[5] = 5;
                value[6] = 6;
                value[7] = 7;

                Vector256.Create((int)0x1).StoreAlignedNonTemporal(value);

                for (int index = 0; index < Vector256<int>.Count; index++)
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
        public unsafe void Vector256Int64StoreAlignedNonTemporalTest()
        {
            long* value = null;

            try
            {
                value = (long*)NativeMemory.AlignedAlloc(byteCount: 32, alignment: 32);

                value[0] = 0;
                value[1] = 1;
                value[2] = 2;
                value[3] = 3;

                Vector256.Create((long)0x1).StoreAlignedNonTemporal(value);

                for (int index = 0; index < Vector256<long>.Count; index++)
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
        public unsafe void Vector256NIntStoreAlignedNonTemporalTest()
        {
            nint* value = null;

            try
            {
                value = (nint*)NativeMemory.AlignedAlloc(byteCount: 32, alignment: 32);

                if (Environment.Is64BitProcess)
                {
                    value[0] = 0;
                    value[1] = 1;
                    value[2] = 2;
                    value[3] = 3;
                }
                else
                {
                    value[0] = 0;
                    value[1] = 1;
                    value[2] = 2;
                    value[3] = 3;
                    value[4] = 4;
                    value[5] = 5;
                    value[6] = 6;
                    value[7] = 7;
                }

                Vector256.Create((nint)0x1).StoreAlignedNonTemporal(value);

                for (int index = 0; index < Vector256<nint>.Count; index++)
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
        public unsafe void Vector256NUIntStoreAlignedNonTemporalTest()
        {
            nuint* value = null;

            try
            {
                value = (nuint*)NativeMemory.AlignedAlloc(byteCount: 32, alignment: 32);

                if (Environment.Is64BitProcess)
                {
                    value[0] = 0;
                    value[1] = 1;
                    value[2] = 2;
                    value[3] = 3;
                }
                else
                {
                    value[0] = 0;
                    value[1] = 1;
                    value[2] = 2;
                    value[3] = 3;
                    value[4] = 4;
                    value[5] = 5;
                    value[6] = 6;
                    value[7] = 7;
                }

                Vector256.Create((nuint)0x1).StoreAlignedNonTemporal(value);

                for (int index = 0; index < Vector256<nuint>.Count; index++)
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
        public unsafe void Vector256SByteStoreAlignedNonTemporalTest()
        {
            sbyte* value = null;

            try
            {
                value = (sbyte*)NativeMemory.AlignedAlloc(byteCount: 32, alignment: 32);

                value[0] = 0;
                value[1] = 1;
                value[2] = 2;
                value[3] = 3;
                value[4] = 4;
                value[5] = 5;
                value[6] = 6;
                value[7] = 7;
                value[8] = 8;
                value[9] = 9;
                value[10] = 10;
                value[11] = 11;
                value[12] = 12;
                value[13] = 13;
                value[14] = 14;
                value[15] = 15;
                value[16] = 16;
                value[17] = 17;
                value[18] = 18;
                value[19] = 19;
                value[20] = 20;
                value[21] = 21;
                value[22] = 22;
                value[23] = 23;
                value[24] = 24;
                value[25] = 25;
                value[26] = 26;
                value[27] = 27;
                value[28] = 28;
                value[29] = 29;
                value[30] = 30;
                value[31] = 31;

                Vector256.Create((sbyte)0x1).StoreAlignedNonTemporal(value);

                for (int index = 0; index < Vector256<sbyte>.Count; index++)
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
        public unsafe void Vector256SingleStoreAlignedNonTemporalTest()
        {
            float* value = null;

            try
            {
                value = (float*)NativeMemory.AlignedAlloc(byteCount: 32, alignment: 32);

                value[0] = 0;
                value[1] = 1;
                value[2] = 2;
                value[3] = 3;
                value[4] = 4;
                value[5] = 5;
                value[6] = 6;
                value[7] = 7;

                Vector256.Create((float)0x1).StoreAlignedNonTemporal(value);

                for (int index = 0; index < Vector256<float>.Count; index++)
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
        public unsafe void Vector256UInt16StoreAlignedNonTemporalTest()
        {
            ushort* value = null;

            try
            {
                value = (ushort*)NativeMemory.AlignedAlloc(byteCount: 32, alignment: 32);

                value[0] = 0;
                value[1] = 1;
                value[2] = 2;
                value[3] = 3;
                value[4] = 4;
                value[5] = 5;
                value[6] = 6;
                value[7] = 7;
                value[8] = 8;
                value[9] = 9;
                value[10] = 10;
                value[11] = 11;
                value[12] = 12;
                value[13] = 13;
                value[14] = 14;
                value[15] = 15;

                Vector256.Create((ushort)0x1).StoreAlignedNonTemporal(value);

                for (int index = 0; index < Vector256<ushort>.Count; index++)
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
        public unsafe void Vector256UInt32StoreAlignedNonTemporalTest()
        {
            uint* value = null;

            try
            {
                value = (uint*)NativeMemory.AlignedAlloc(byteCount: 32, alignment: 32);

                value[0] = 0;
                value[1] = 1;
                value[2] = 2;
                value[3] = 3;
                value[4] = 4;
                value[5] = 5;
                value[6] = 6;
                value[7] = 7;

                Vector256.Create((uint)0x1).StoreAlignedNonTemporal(value);

                for (int index = 0; index < Vector256<uint>.Count; index++)
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
        public unsafe void Vector256UInt64StoreAlignedNonTemporalTest()
        {
            ulong* value = null;

            try
            {
                value = (ulong*)NativeMemory.AlignedAlloc(byteCount: 32, alignment: 32);

                value[0] = 0;
                value[1] = 1;
                value[2] = 2;
                value[3] = 3;

                Vector256.Create((ulong)0x1).StoreAlignedNonTemporal(value);

                for (int index = 0; index < Vector256<ulong>.Count; index++)
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
        public unsafe void Vector256ByteStoreUnsafeTest()
        {
            byte* value = stackalloc byte[32] {
                0,
                1,
                2,
                3,
                4,
                5,
                6,
                7,
                8,
                9,
                10,
                11,
                12,
                13,
                14,
                15,
                16,
                17,
                18,
                19,
                20,
                21,
                22,
                23,
                24,
                25,
                26,
                27,
                28,
                29,
                30,
                31,
            };

            Vector256.Create((byte)0x1).StoreUnsafe(ref value[0]);

            for (int index = 0; index < Vector256<byte>.Count; index++)
            {
                Assert.Equal((byte)0x1, value[index]);
            }
        }

        [Fact]
        public unsafe void Vector256DoubleStoreUnsafeTest()
        {
            double* value = stackalloc double[4] {
                0,
                1,
                2,
                3,
            };

            Vector256.Create((double)0x1).StoreUnsafe(ref value[0]);

            for (int index = 0; index < Vector256<double>.Count; index++)
            {
                Assert.Equal((double)0x1, value[index]);
            }
        }

        [Fact]
        public unsafe void Vector256Int16StoreUnsafeTest()
        {
            short* value = stackalloc short[16] {
                0,
                1,
                2,
                3,
                4,
                5,
                6,
                7,
                8,
                9,
                10,
                11,
                12,
                13,
                14,
                15,
            };

            Vector256.Create((short)0x1).StoreUnsafe(ref value[0]);

            for (int index = 0; index < Vector256<short>.Count; index++)
            {
                Assert.Equal((short)0x1, value[index]);
            }
        }

        [Fact]
        public unsafe void Vector256Int32StoreUnsafeTest()
        {
            int* value = stackalloc int[8] {
                0,
                1,
                2,
                3,
                4,
                5,
                6,
                7,
            };

            Vector256.Create((int)0x1).StoreUnsafe(ref value[0]);

            for (int index = 0; index < Vector256<int>.Count; index++)
            {
                Assert.Equal((int)0x1, value[index]);
            }
        }

        [Fact]
        public unsafe void Vector256Int64StoreUnsafeTest()
        {
            long* value = stackalloc long[4] {
                0,
                1,
                2,
                3,
            };

            Vector256.Create((long)0x1).StoreUnsafe(ref value[0]);

            for (int index = 0; index < Vector256<long>.Count; index++)
            {
                Assert.Equal((long)0x1, value[index]);
            }
        }

        [Fact]
        public unsafe void Vector256NIntStoreUnsafeTest()
        {
            if (Environment.Is64BitProcess)
            {
                nint* value = stackalloc nint[4] {
                    0,
                    1,
                    2,
                    3,
                };

                Vector256.Create((nint)0x1).StoreUnsafe(ref value[0]);

                for (int index = 0; index < Vector256<nint>.Count; index++)
                {
                    Assert.Equal((nint)0x1, value[index]);
                }
            }
            else
            {
                nint* value = stackalloc nint[8] {
                    0,
                    1,
                    2,
                    3,
                    4,
                    5,
                    6,
                    7,
                };

                Vector256.Create((nint)0x1).StoreUnsafe(ref value[0]);

                for (int index = 0; index < Vector256<nint>.Count; index++)
                {
                    Assert.Equal((nint)0x1, value[index]);
                }
            }
        }

        [Fact]
        public unsafe void Vector256NUIntStoreUnsafeTest()
        {
            if (Environment.Is64BitProcess)
            {
                nuint* value = stackalloc nuint[4] {
                    0,
                    1,
                    2,
                    3,
                };

                Vector256.Create((nuint)0x1).StoreUnsafe(ref value[0]);

                for (int index = 0; index < Vector256<nuint>.Count; index++)
                {
                    Assert.Equal((nuint)0x1, value[index]);
                }
            }
            else
            {
                nuint* value = stackalloc nuint[8] {
                    0,
                    1,
                    2,
                    3,
                    4,
                    5,
                    6,
                    7,
                };

                Vector256.Create((nuint)0x1).StoreUnsafe(ref value[0]);

                for (int index = 0; index < Vector256<nuint>.Count; index++)
                {
                    Assert.Equal((nuint)0x1, value[index]);
                }
            }
        }

        [Fact]
        public unsafe void Vector256SByteStoreUnsafeTest()
        {
            sbyte* value = stackalloc sbyte[32] {
                0,
                1,
                2,
                3,
                4,
                5,
                6,
                7,
                8,
                9,
                10,
                11,
                12,
                13,
                14,
                15,
                16,
                17,
                18,
                19,
                20,
                21,
                22,
                23,
                24,
                25,
                26,
                27,
                28,
                29,
                30,
                31,
            };

            Vector256.Create((sbyte)0x1).StoreUnsafe(ref value[0]);

            for (int index = 0; index < Vector256<sbyte>.Count; index++)
            {
                Assert.Equal((sbyte)0x1, value[index]);
            }
        }

        [Fact]
        public unsafe void Vector256SingleStoreUnsafeTest()
        {
            float* value = stackalloc float[8] {
                0,
                1,
                2,
                3,
                4,
                5,
                6,
                7,
            };

            Vector256.Create((float)0x1).StoreUnsafe(ref value[0]);

            for (int index = 0; index < Vector256<float>.Count; index++)
            {
                Assert.Equal((float)0x1, value[index]);
            }
        }

        [Fact]
        public unsafe void Vector256UInt16StoreUnsafeTest()
        {
            ushort* value = stackalloc ushort[16] {
                0,
                1,
                2,
                3,
                4,
                5,
                6,
                7,
                8,
                9,
                10,
                11,
                12,
                13,
                14,
                15,
            };

            Vector256.Create((ushort)0x1).StoreUnsafe(ref value[0]);

            for (int index = 0; index < Vector256<ushort>.Count; index++)
            {
                Assert.Equal((ushort)0x1, value[index]);
            }
        }

        [Fact]
        public unsafe void Vector256UInt32StoreUnsafeTest()
        {
            uint* value = stackalloc uint[8] {
                0,
                1,
                2,
                3,
                4,
                5,
                6,
                7,
            };

            Vector256.Create((uint)0x1).StoreUnsafe(ref value[0]);

            for (int index = 0; index < Vector256<uint>.Count; index++)
            {
                Assert.Equal((uint)0x1, value[index]);
            }
        }

        [Fact]
        public unsafe void Vector256UInt64StoreUnsafeTest()
        {
            ulong* value = stackalloc ulong[4] {
                0,
                1,
                2,
                3,
            };

            Vector256.Create((ulong)0x1).StoreUnsafe(ref value[0]);

            for (int index = 0; index < Vector256<ulong>.Count; index++)
            {
                Assert.Equal((ulong)0x1, value[index]);
            }
        }

        [Fact]
        public unsafe void Vector256ByteStoreUnsafeIndexTest()
        {
            byte* value = stackalloc byte[32 + 1] {
                0,
                1,
                2,
                3,
                4,
                5,
                6,
                7,
                8,
                9,
                10,
                11,
                12,
                13,
                14,
                15,
                16,
                17,
                18,
                19,
                20,
                21,
                22,
                23,
                24,
                25,
                26,
                27,
                28,
                29,
                30,
                31,
                32,
            };

            Vector256.Create((byte)0x1).StoreUnsafe(ref value[0], 1);

            for (int index = 0; index < Vector256<byte>.Count; index++)
            {
                Assert.Equal((byte)0x1, value[index + 1]);
            }
        }

        [Fact]
        public unsafe void Vector256DoubleStoreUnsafeIndexTest()
        {
            double* value = stackalloc double[4 + 1] {
                0,
                1,
                2,
                3,
                4,
            };

            Vector256.Create((double)0x1).StoreUnsafe(ref value[0], 1);

            for (int index = 0; index < Vector256<double>.Count; index++)
            {
                Assert.Equal((double)0x1, value[index + 1]);
            }
        }

        [Fact]
        public unsafe void Vector256Int16StoreUnsafeIndexTest()
        {
            short* value = stackalloc short[16 + 1] {
                0,
                1,
                2,
                3,
                4,
                5,
                6,
                7,
                8,
                9,
                10,
                11,
                12,
                13,
                14,
                15,
                16,
            };

            Vector256.Create((short)0x1).StoreUnsafe(ref value[0], 1);

            for (int index = 0; index < Vector256<short>.Count; index++)
            {
                Assert.Equal((short)0x1, value[index + 1]);
            }
        }

        [Fact]
        public unsafe void Vector256Int32StoreUnsafeIndexTest()
        {
            int* value = stackalloc int[8 + 1] {
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

            Vector256.Create((int)0x1).StoreUnsafe(ref value[0], 1);

            for (int index = 0; index < Vector256<int>.Count; index++)
            {
                Assert.Equal((int)0x1, value[index + 1]);
            }
        }

        [Fact]
        public unsafe void Vector256Int64StoreUnsafeIndexTest()
        {
            long* value = stackalloc long[4 + 1] {
                0,
                1,
                2,
                3,
                4,
            };

            Vector256.Create((long)0x1).StoreUnsafe(ref value[0], 1);

            for (int index = 0; index < Vector256<long>.Count; index++)
            {
                Assert.Equal((long)0x1, value[index + 1]);
            }
        }

        [Fact]
        public unsafe void Vector256NIntStoreUnsafeIndexTest()
        {
            if (Environment.Is64BitProcess)
            {
                nint* value = stackalloc nint[4 + 1] {
                    0,
                    1,
                    2,
                    3,
                    4,
                };

                Vector256.Create((nint)0x1).StoreUnsafe(ref value[0], 1);

                for (int index = 0; index < Vector256<nint>.Count; index++)
                {
                    Assert.Equal((nint)0x1, value[index + 1]);
                }
            }
            else
            {
                nint* value = stackalloc nint[8 + 1] {
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

                Vector256.Create((nint)0x1).StoreUnsafe(ref value[0], 1);

                for (int index = 0; index < Vector256<nint>.Count; index++)
                {
                    Assert.Equal((nint)0x1, value[index + 1]);
                }
            }
        }

        [Fact]
        public unsafe void Vector256NUIntStoreUnsafeIndexTest()
        {
            if (Environment.Is64BitProcess)
            {
                nuint* value = stackalloc nuint[4 + 1] {
                    0,
                    1,
                    2,
                    3,
                    4,
                };

                Vector256.Create((nuint)0x1).StoreUnsafe(ref value[0], 1);

                for (int index = 0; index < Vector256<nuint>.Count; index++)
                {
                    Assert.Equal((nuint)0x1, value[index + 1]);
                }
            }
            else
            {
                nuint* value = stackalloc nuint[8 + 1] {
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

                Vector256.Create((nuint)0x1).StoreUnsafe(ref value[0], 1);

                for (int index = 0; index < Vector256<nuint>.Count; index++)
                {
                    Assert.Equal((nuint)0x1, value[index + 1]);
                }
            }
        }

        [Fact]
        public unsafe void Vector256SByteStoreUnsafeIndexTest()
        {
            sbyte* value = stackalloc sbyte[32 + 1] {
                0,
                1,
                2,
                3,
                4,
                5,
                6,
                7,
                8,
                9,
                10,
                11,
                12,
                13,
                14,
                15,
                16,
                17,
                18,
                19,
                20,
                21,
                22,
                23,
                24,
                25,
                26,
                27,
                28,
                29,
                30,
                31,
                32,
            };

            Vector256.Create((sbyte)0x1).StoreUnsafe(ref value[0], 1);

            for (int index = 0; index < Vector256<sbyte>.Count; index++)
            {
                Assert.Equal((sbyte)0x1, value[index + 1]);
            }
        }

        [Fact]
        public unsafe void Vector256SingleStoreUnsafeIndexTest()
        {
            float* value = stackalloc float[8 + 1] {
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

            Vector256.Create((float)0x1).StoreUnsafe(ref value[0], 1);

            for (int index = 0; index < Vector256<float>.Count; index++)
            {
                Assert.Equal((float)0x1, value[index + 1]);
            }
        }

        [Fact]
        public unsafe void Vector256UInt16StoreUnsafeIndexTest()
        {
            ushort* value = stackalloc ushort[16 + 1] {
                0,
                1,
                2,
                3,
                4,
                5,
                6,
                7,
                8,
                9,
                10,
                11,
                12,
                13,
                14,
                15,
                16,
            };

            Vector256.Create((ushort)0x1).StoreUnsafe(ref value[0], 1);

            for (int index = 0; index < Vector256<ushort>.Count; index++)
            {
                Assert.Equal((ushort)0x1, value[index + 1]);
            }
        }

        [Fact]
        public unsafe void Vector256UInt32StoreUnsafeIndexTest()
        {
            uint* value = stackalloc uint[8 + 1] {
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

            Vector256.Create((uint)0x1).StoreUnsafe(ref value[0], 1);

            for (int index = 0; index < Vector256<uint>.Count; index++)
            {
                Assert.Equal((uint)0x1, value[index + 1]);
            }
        }

        [Fact]
        public unsafe void Vector256UInt64StoreUnsafeIndexTest()
        {
            ulong* value = stackalloc ulong[4 + 1] {
                0,
                1,
                2,
                3,
                4,
            };

            Vector256.Create((ulong)0x1).StoreUnsafe(ref value[0], 1);

            for (int index = 0; index < Vector256<ulong>.Count; index++)
            {
                Assert.Equal((ulong)0x1, value[index + 1]);
            }
        }

        [Fact]
        public void Vector256ByteSumTest()
        {
            Vector256<byte> vector = Vector256.Create((byte)0x01);
            Assert.Equal((byte)32, Vector256.Sum(vector));
        }

        [Fact]
        public void Vector256DoubleSumTest()
        {
            Vector256<double> vector = Vector256.Create((double)0x01);
            Assert.Equal(4.0, Vector256.Sum(vector));
        }

        [Fact]
        public void Vector256Int16SumTest()
        {
            Vector256<short> vector = Vector256.Create((short)0x01);
            Assert.Equal((short)16, Vector256.Sum(vector));
        }

        [Fact]
        public void Vector256Int32SumTest()
        {
            Vector256<int> vector = Vector256.Create((int)0x01);
            Assert.Equal((int)8, Vector256.Sum(vector));
        }

        [Fact]
        public void Vector256Int64SumTest()
        {
            Vector256<long> vector = Vector256.Create((long)0x01);
            Assert.Equal((long)4, Vector256.Sum(vector));
        }

        [Fact]
        public void Vector256NIntSumTest()
        {
            Vector256<nint> vector = Vector256.Create((nint)0x01);

            if (Environment.Is64BitProcess)
            {
                Assert.Equal((nint)4, Vector256.Sum(vector));
            }
            else
            {
                Assert.Equal((nint)8, Vector256.Sum(vector));
            }
        }

        [Fact]
        public void Vector256NUIntSumTest()
        {
            Vector256<nuint> vector = Vector256.Create((nuint)0x01);

            if (Environment.Is64BitProcess)
            {
                Assert.Equal((nuint)4, Vector256.Sum(vector));
            }
            else
            {
                Assert.Equal((nuint)8, Vector256.Sum(vector));
            }
        }

        [Fact]
        public void Vector256SByteSumTest()
        {
            Vector256<sbyte> vector = Vector256.Create((sbyte)0x01);
            Assert.Equal((sbyte)32, Vector256.Sum(vector));
        }

        [Fact]
        public void Vector256SingleSumTest()
        {
            Vector256<float> vector = Vector256.Create((float)0x01);
            Assert.Equal(8.0f, Vector256.Sum(vector));
        }

        [Fact]
        public void Vector256UInt16SumTest()
        {
            Vector256<ushort> vector = Vector256.Create((ushort)0x01);
            Assert.Equal((ushort)16, Vector256.Sum(vector));
        }

        [Fact]
        public void Vector256UInt32SumTest()
        {
            Vector256<uint> vector = Vector256.Create((uint)0x01);
            Assert.Equal((uint)8, Vector256.Sum(vector));
        }

        [Fact]
        public void Vector256UInt64SumTest()
        {
            Vector256<ulong> vector = Vector256.Create((ulong)0x01);
            Assert.Equal((ulong)4, Vector256.Sum(vector));
        }

        [Theory]
        [InlineData(0, 0, 0, 0, 0, 0, 0, 0)]
        [InlineData(1, 1, 1, 1, 1, 1, 1, 1)]
        [InlineData(-1, -1, -1, -1, -1, -1, -1, -1)]
        [InlineData(0, 1, 2, 3, 4, 5, 6, 7, 8)]
        [InlineData(0, 0, 50, 430, -64, 0, int.MaxValue, int.MinValue)]
        public void Vector256Int32IndexerTest(params int[] values)
        {
            var vector = Vector256.Create(values);

            Assert.Equal(vector[0], values[0]);
            Assert.Equal(vector[1], values[1]);
            Assert.Equal(vector[2], values[2]);
            Assert.Equal(vector[3], values[3]);
            Assert.Equal(vector[4], values[4]);
            Assert.Equal(vector[5], values[5]);
            Assert.Equal(vector[6], values[6]);
            Assert.Equal(vector[7], values[7]);
        }

        [Theory]
        [InlineData(0L, 0L, 0L, 0L)]
        [InlineData(1L, 1L, 1L, 1L)]
        [InlineData(0L, 1L, 2L, 3L, 4L, 5L, 6L, 7L, 8L)]
        [InlineData(0L, 0L, 50L, 430L, -64L, 0L, long.MaxValue, long.MinValue)]
        public void Vector256Int64IndexerTest(params long[] values)
        {
            var vector = Vector256.Create(values);

            Assert.Equal(vector[0], values[0]);
            Assert.Equal(vector[1], values[1]);
            Assert.Equal(vector[2], values[2]);
            Assert.Equal(vector[3], values[3]);
        }

        [Fact]
        public void Vector256DoubleEqualsNaNTest()
        {
            Vector256<double> nan = Vector256.Create(double.NaN);
            Assert.True(nan.Equals(nan));
        }

        [Fact]
        public void Vector256SingleEqualsNaNTest()
        {
            Vector256<float> nan = Vector256.Create(float.NaN);
            Assert.True(nan.Equals(nan));
        }

        [Fact]
        public void Vector256DoubleEqualsNonCanonicalNaNTest()
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
                    Assert.True(Vector256.Create(i).Equals(Vector256.Create(j)));
                    Assert.False(Vector256.Create(i) == Vector256.Create(j));
                }
            }
        }

        [Fact]
        public void Vector256SingleEqualsNonCanonicalNaNTest()
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
                    Assert.True(Vector256.Create(i).Equals(Vector256.Create(j)));
                    Assert.False(Vector256.Create(i) == Vector256.Create(j));
                }
            }
        }

        [Fact]
        public void Vector256SingleCreateFromArrayTest()
        {
            float[] array = [1.0f, 2.0f, 3.0f, 4.0f, 5.0f, 6.0f, 7.0f, 8.0f, 9.0f];
            Vector256<float> vector = Vector256.Create(array);
            Assert.Equal(Vector256.Create(1.0f, 2.0f, 3.0f, 4.0f, 5.0f, 6.0f, 7.0f, 8.0f), vector);
        }

        [Fact]
        public void Vector256SingleCreateFromArrayOffsetTest()
        {
            float[] array = [1.0f, 2.0f, 3.0f, 4.0f, 5.0f, 6.0f, 7.0f, 8.0f, 9.0f];
            Vector256<float> vector = Vector256.Create(array, 1);
            Assert.Equal(Vector256.Create(2.0f, 3.0f, 4.0f, 5.0f, 6.0f, 7.0f, 8.0f, 9.0f), vector);
        }

        [Fact]
        public void Vector256SingleCopyToTest()
        {
            float[] array = new float[8];
            Vector256.Create(2.0f).CopyTo(array);
            Assert.True(array.AsSpan().SequenceEqual([2.0f, 2.0f, 2.0f, 2.0f, 2.0f, 2.0f, 2.0f, 2.0f]));
        }

        [Fact]
        public void Vector256SingleCopyToOffsetTest()
        {
            float[] array = new float[9];
            Vector256.Create(2.0f).CopyTo(array, 1);
            Assert.True(array.AsSpan().SequenceEqual([0.0f, 2.0f, 2.0f, 2.0f, 2.0f, 2.0f, 2.0f, 2.0f, 2.0f]));
        }

        [Fact]
        public void Vector256SByteAbs_MinValue()
        {
            Vector256<sbyte> vector = Vector256.Create(sbyte.MinValue);
            Vector256<sbyte> abs = Vector256.Abs(vector);
            for (int index = 0; index < Vector256<sbyte>.Count; index++)
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
            Assert.True(Vector256<T>.IsSupported);

            MethodInfo methodInfo = typeof(Vector256<T>).GetProperty("IsSupported", BindingFlags.Public | BindingFlags.Static).GetMethod;
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
            Assert.False(Vector256<T>.IsSupported);

            MethodInfo methodInfo = typeof(Vector256<T>).GetProperty("IsSupported", BindingFlags.Public | BindingFlags.Static).GetMethod;
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
            Assert.Equal(Vector256<T>.One, Vector256.Create(T.One));

            MethodInfo methodInfo = typeof(Vector256<T>).GetProperty("One", BindingFlags.Public | BindingFlags.Static).GetMethod;
            Assert.Equal((Vector256<T>)methodInfo.Invoke(null, null), Vector256.Create(T.One));
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
            Vector256<T> indices = Vector256<T>.Indices;

            for (int index = 0; index < Vector256<T>.Count; index++)
            {
                Assert.Equal(T.CreateTruncating(index), indices.GetElement(index));
            }
        }

        [Theory]
        [InlineData(0, 2)]
        [InlineData(3, 3)]
        [InlineData(31, unchecked((byte)(-1)))]
        public void CreateSequenceByteTest(byte start, byte step) => TestCreateSequence<byte>(start, step);

        [Theory]
        [InlineData(0.0, +2.0)]
        [InlineData(3.0, +3.0)]
        [InlineData(3.0, -1.0)]
        public void CreateSequenceDoubleTest(double start, double step) => TestCreateSequence<double>(start, step);

        [Theory]
        [InlineData(0, +2)]
        [InlineData(3, +3)]
        [InlineData(15, -1)]
        public void CreateSequenceInt16Test(short start, short step) => TestCreateSequence<short>(start, step);

        [Theory]
        [InlineData(0, +2)]
        [InlineData(3, +3)]
        [InlineData(7, -1)]
        public void CreateSequenceInt32Test(int start, int step) => TestCreateSequence<int>(start, step);

        [Theory]
        [InlineData(0, +2)]
        [InlineData(3, +3)]
        [InlineData(15, -1)]
        public void CreateSequenceInt64Test(long start, long step) => TestCreateSequence<long>(start, step);

        [Theory]
        [InlineData(0, +2)]
        [InlineData(3, +3)]
        [InlineData(31, -1)]
        public void CreateSequenceSByteTest(sbyte start, sbyte step) => TestCreateSequence<sbyte>(start, step);

        [Theory]
        [InlineData(0.0f, +2.0f)]
        [InlineData(3.0f, +3.0f)]
        [InlineData(7.0f, -1.0f)]
        public void CreateSequenceSingleTest(float start, float step) => TestCreateSequence<float>(start, step);

        [Theory]
        [InlineData(0, 2)]
        [InlineData(3, 3)]
        [InlineData(15, unchecked((ushort)(-1)))]
        public void CreateSequenceUInt16Test(ushort start, ushort step) => TestCreateSequence<ushort>(start, step);

        [Theory]
        [InlineData(0, 2)]
        [InlineData(3, 3)]
        [InlineData(7, unchecked((uint)(-1)))]
        public void CreateSequenceUInt32Test(uint start, uint step) => TestCreateSequence<uint>(start, step);

        [Theory]
        [InlineData(0, 2)]
        [InlineData(3, 3)]
        [InlineData(3, unchecked((ulong)(-1)))]
        public void CreateSequenceUInt64Test(ulong start, ulong step) => TestCreateSequence<ulong>(start, step);

        private static void TestCreateSequence<T>(T start, T step)
            where T : INumber<T>
        {
            Vector256<T> sequence = Vector256.CreateSequence(start, step);
            T expected = start;

            for (int index = 0; index < Vector256<T>.Count; index++)
            {
                Assert.Equal(expected, sequence.GetElement(index));
                expected += step;
            }
        }

        [Theory]
        [MemberData(nameof(GenericMathTestMemberData.CosDouble), MemberType = typeof(GenericMathTestMemberData))]
        public void CosDoubleTest(double value, double expectedResult, double variance)
        {
            Vector256<double> actualResult = Vector256.Cos(Vector256.Create(value));
            AssertEqual(Vector256.Create(expectedResult), actualResult, Vector256.Create(variance));
        }

        [Theory]
        [MemberData(nameof(GenericMathTestMemberData.CosSingle), MemberType = typeof(GenericMathTestMemberData))]
        public void CosSingleTest(float value, float expectedResult, float variance)
        {
            Vector256<float> actualResult = Vector256.Cos(Vector256.Create(value));
            AssertEqual(Vector256.Create(expectedResult), actualResult, Vector256.Create(variance));
        }

        [Theory]
        [MemberData(nameof(GenericMathTestMemberData.ExpDouble), MemberType = typeof(GenericMathTestMemberData))]
        public void ExpDoubleTest(double value, double expectedResult, double variance)
        {
            Vector256<double> actualResult = Vector256.Exp(Vector256.Create(value));
            AssertEqual(Vector256.Create(expectedResult), actualResult, Vector256.Create(variance));
        }

        [Theory]
        [MemberData(nameof(GenericMathTestMemberData.ExpSingle), MemberType = typeof(GenericMathTestMemberData))]
        public void ExpSingleTest(float value, float expectedResult, float variance)
        {
            Vector256<float> actualResult = Vector256.Exp(Vector256.Create(value));
            AssertEqual(Vector256.Create(expectedResult), actualResult, Vector256.Create(variance));
        }

        [Theory]
        [MemberData(nameof(GenericMathTestMemberData.LogDouble), MemberType = typeof(GenericMathTestMemberData))]
        public void LogDoubleTest(double value, double expectedResult, double variance)
        {
            Vector256<double> actualResult = Vector256.Log(Vector256.Create(value));
            AssertEqual(Vector256.Create(expectedResult), actualResult, Vector256.Create(variance));
        }

        [Theory]
        [MemberData(nameof(GenericMathTestMemberData.LogSingle), MemberType = typeof(GenericMathTestMemberData))]
        public void LogSingleTest(float value, float expectedResult, float variance)
        {
            Vector256<float> actualResult = Vector256.Log(Vector256.Create(value));
            AssertEqual(Vector256.Create(expectedResult), actualResult, Vector256.Create(variance));
        }

        [Theory]
        [MemberData(nameof(GenericMathTestMemberData.Log2Double), MemberType = typeof(GenericMathTestMemberData))]
        public void Log2DoubleTest(double value, double expectedResult, double variance)
        {
            Vector256<double> actualResult = Vector256.Log2(Vector256.Create(value));
            AssertEqual(Vector256.Create(expectedResult), actualResult, Vector256.Create(variance));
        }

        [Theory]
        [MemberData(nameof(GenericMathTestMemberData.Log2Single), MemberType = typeof(GenericMathTestMemberData))]
        public void Log2SingleTest(float value, float expectedResult, float variance)
        {
            Vector256<float> actualResult = Vector256.Log2(Vector256.Create(value));
            AssertEqual(Vector256.Create(expectedResult), actualResult, Vector256.Create(variance));
        }

        [Theory]
        [MemberData(nameof(GenericMathTestMemberData.FusedMultiplyAddDouble), MemberType = typeof(GenericMathTestMemberData))]
        public void FusedMultiplyAddDoubleTest(double left, double right, double addend, double expectedResult)
        {
            AssertEqual(Vector256.Create(expectedResult), Vector256.FusedMultiplyAdd(Vector256.Create(left), Vector256.Create(right), Vector256.Create(addend)), Vector256<double>.Zero);
            AssertEqual(Vector256.Create(double.MultiplyAddEstimate(left, right, addend)), Vector256.MultiplyAddEstimate(Vector256.Create(left), Vector256.Create(right), Vector256.Create(addend)), Vector256<double>.Zero);
        }

        [Theory]
        [MemberData(nameof(GenericMathTestMemberData.FusedMultiplyAddSingle), MemberType = typeof(GenericMathTestMemberData))]
        public void FusedMultiplyAddSingleTest(float left, float right, float addend, float expectedResult)
        {
            AssertEqual(Vector256.Create(expectedResult), Vector256.FusedMultiplyAdd(Vector256.Create(left), Vector256.Create(right), Vector256.Create(addend)), Vector256<float>.Zero);
            AssertEqual(Vector256.Create(float.MultiplyAddEstimate(left, right, addend)), Vector256.MultiplyAddEstimate(Vector256.Create(left), Vector256.Create(right), Vector256.Create(addend)), Vector256<float>.Zero);
        }

        [Fact]
        [SkipOnMono("https://github.com/dotnet/runtime/issues/100368")]
        public void ConvertToInt32Test()
        {
            Assert.Equal(Vector256.Create(float.ConvertToInteger<int>(float.MinValue)), Vector256.ConvertToInt32(Vector256.Create(float.MinValue)));
            Assert.Equal(Vector256.Create(float.ConvertToInteger<int>(2.6f)), Vector256.ConvertToInt32(Vector256.Create(2.6f)));
            Assert.Equal(Vector256.Create(float.ConvertToInteger<int>(float.MaxValue)), Vector256.ConvertToInt32(Vector256.Create(float.MaxValue)));
        }

        [Fact]
        [SkipOnMono("https://github.com/dotnet/runtime/issues/100368")]
        public void ConvertToInt32NativeTest()
        {
            Assert.Equal(Vector256.Create(float.ConvertToIntegerNative<int>(float.MinValue)), Vector256.ConvertToInt32Native(Vector256.Create(float.MinValue)));
            Assert.Equal(Vector256.Create(float.ConvertToIntegerNative<int>(2.6f)), Vector256.ConvertToInt32Native(Vector256.Create(2.6f)));
            Assert.Equal(Vector256.Create(float.ConvertToIntegerNative<int>(float.MaxValue)), Vector256.ConvertToInt32Native(Vector256.Create(float.MaxValue)));
        }

        [Fact]
        [SkipOnMono("https://github.com/dotnet/runtime/issues/100368")]
        public void ConvertToInt64Test()
        {
            Assert.Equal(Vector256.Create(double.ConvertToInteger<long>(double.MinValue)), Vector256.ConvertToInt64(Vector256.Create(double.MinValue)));
            Assert.Equal(Vector256.Create(double.ConvertToInteger<long>(2.6)), Vector256.ConvertToInt64(Vector256.Create(2.6)));
            Assert.Equal(Vector256.Create(double.ConvertToInteger<long>(double.MaxValue)), Vector256.ConvertToInt64(Vector256.Create(double.MaxValue)));
        }

        [Fact]
        [SkipOnMono("https://github.com/dotnet/runtime/issues/100368")]
        public void ConvertToInt64NativeTest()
        {
            Assert.Equal(Vector256.Create(double.ConvertToIntegerNative<long>(double.MinValue)), Vector256.ConvertToInt64Native(Vector256.Create(double.MinValue)));
            Assert.Equal(Vector256.Create(double.ConvertToIntegerNative<long>(2.6)), Vector256.ConvertToInt64Native(Vector256.Create(2.6)));

            if (Environment.Is64BitProcess)
            {
                // This isn't accelerated on all 32-bit systems today and may fallback to ConvertToInteger behavior
                Assert.Equal(Vector256.Create(double.ConvertToIntegerNative<long>(double.MaxValue)), Vector256.ConvertToInt64Native(Vector256.Create(double.MaxValue)));
            }
        }

        [Fact]
        [SkipOnMono("https://github.com/dotnet/runtime/issues/100368")]
        public void ConvertToUInt32Test()
        {
            Assert.Equal(Vector256.Create(float.ConvertToInteger<uint>(float.MinValue)), Vector256.ConvertToUInt32(Vector256.Create(float.MinValue)));
            Assert.Equal(Vector256.Create(float.ConvertToInteger<uint>(2.6f)), Vector256.ConvertToUInt32(Vector256.Create(2.6f)));
            Assert.Equal(Vector256.Create(float.ConvertToInteger<uint>(float.MaxValue)), Vector256.ConvertToUInt32(Vector256.Create(float.MaxValue)));
        }

        [Fact]
        [SkipOnMono("https://github.com/dotnet/runtime/issues/100368")]
        public void ConvertToUInt32NativeTest()
        {
            Assert.Equal(Vector256.Create(float.ConvertToIntegerNative<uint>(float.MinValue)), Vector256.ConvertToUInt32Native(Vector256.Create(float.MinValue)));
            Assert.Equal(Vector256.Create(float.ConvertToIntegerNative<uint>(2.6f)), Vector256.ConvertToUInt32Native(Vector256.Create(2.6f)));
            Assert.Equal(Vector256.Create(float.ConvertToIntegerNative<uint>(float.MaxValue)), Vector256.ConvertToUInt32Native(Vector256.Create(float.MaxValue)));
        }

        [Fact]
        [SkipOnMono("https://github.com/dotnet/runtime/issues/100368")]
        public void ConvertToUInt64Test()
        {
            Assert.Equal(Vector256.Create(double.ConvertToInteger<ulong>(double.MinValue)), Vector256.ConvertToUInt64(Vector256.Create(double.MinValue)));
            Assert.Equal(Vector256.Create(double.ConvertToInteger<ulong>(2.6)), Vector256.ConvertToUInt64(Vector256.Create(2.6)));
            Assert.Equal(Vector256.Create(double.ConvertToInteger<ulong>(double.MaxValue)), Vector256.ConvertToUInt64(Vector256.Create(double.MaxValue)));
        }

        [Fact]
        [SkipOnMono("https://github.com/dotnet/runtime/issues/100368")]
        public void ConvertToUInt64NativeTest()
        {
            if (Environment.Is64BitProcess)
            {
                // This isn't accelerated on all 32-bit systems today and may fallback to ConvertToInteger behavior
                Assert.Equal(Vector256.Create(double.ConvertToIntegerNative<ulong>(double.MinValue)), Vector256.ConvertToUInt64Native(Vector256.Create(double.MinValue)));
            }

            Assert.Equal(Vector256.Create(double.ConvertToIntegerNative<ulong>(2.6)), Vector256.ConvertToUInt64Native(Vector256.Create(2.6)));
            Assert.Equal(Vector256.Create(double.ConvertToIntegerNative<ulong>(double.MaxValue)), Vector256.ConvertToUInt64Native(Vector256.Create(double.MaxValue)));
        }

        [Theory]
        [MemberData(nameof(GenericMathTestMemberData.ClampDouble), MemberType = typeof(GenericMathTestMemberData))]
        public void ClampDoubleTest(double x, double min, double max, double expectedResult)
        {
            Vector256<double> actualResult = Vector256.Clamp(Vector256.Create(x), Vector256.Create(min), Vector256.Create(max));
            AssertEqual(Vector256.Create(expectedResult), actualResult, Vector256<double>.Zero);
        }

        [Theory]
        [MemberData(nameof(GenericMathTestMemberData.ClampSingle), MemberType = typeof(GenericMathTestMemberData))]
        public void ClampSingleTest(float x, float min, float max, float expectedResult)
        {
            Vector256<float> actualResult = Vector256.Clamp(Vector256.Create(x), Vector256.Create(min), Vector256.Create(max));
            AssertEqual(Vector256.Create(expectedResult), actualResult, Vector256<float>.Zero);
        }

        [Theory]
        [MemberData(nameof(GenericMathTestMemberData.CopySignDouble), MemberType = typeof(GenericMathTestMemberData))]
        public void CopySignDoubleTest(double x, double y, double expectedResult)
        {
            Vector256<double> actualResult = Vector256.CopySign(Vector256.Create(x), Vector256.Create(y));
            AssertEqual(Vector256.Create(expectedResult), actualResult, Vector256<double>.Zero);
        }

        [Theory]
        [MemberData(nameof(GenericMathTestMemberData.CopySignSingle), MemberType = typeof(GenericMathTestMemberData))]
        public void CopySignSingleTest(float x, float y, float expectedResult)
        {
            Vector256<float> actualResult = Vector256.CopySign(Vector256.Create(x), Vector256.Create(y));
            AssertEqual(Vector256.Create(expectedResult), actualResult, Vector256<float>.Zero);
        }

        [Theory]
        [MemberData(nameof(GenericMathTestMemberData.DegreesToRadiansDouble), MemberType = typeof(GenericMathTestMemberData))]
        public void DegreesToRadiansDoubleTest(double value, double expectedResult, double variance)
        {
            Vector256<double> actualResult1 = Vector256.DegreesToRadians(Vector256.Create(-value));
            AssertEqual(Vector256.Create(-expectedResult), actualResult1, Vector256.Create(variance));

            Vector256<double> actualResult2 = Vector256.DegreesToRadians(Vector256.Create(+value));
            AssertEqual(Vector256.Create(+expectedResult), actualResult2, Vector256.Create(variance));
        }

        [Theory]
        [MemberData(nameof(GenericMathTestMemberData.DegreesToRadiansSingle), MemberType = typeof(GenericMathTestMemberData))]
        public void DegreesToRadiansSingleTest(float value, float expectedResult, float variance)
        {
            AssertEqual(Vector256.Create(-expectedResult), Vector256.DegreesToRadians(Vector256.Create(-value)), Vector256.Create(variance));
            AssertEqual(Vector256.Create(+expectedResult), Vector256.DegreesToRadians(Vector256.Create(+value)), Vector256.Create(variance));
        }

        [Theory]
        [MemberData(nameof(GenericMathTestMemberData.HypotDouble), MemberType = typeof(GenericMathTestMemberData))]
        public void HypotDoubleTest(double x, double y, double expectedResult, double variance)
        {
            AssertEqual(Vector256.Create(expectedResult), Vector256.Hypot(Vector256.Create(-x), Vector256.Create(-y)), Vector256.Create(variance));
            AssertEqual(Vector256.Create(expectedResult), Vector256.Hypot(Vector256.Create(-x), Vector256.Create(+y)), Vector256.Create(variance));
            AssertEqual(Vector256.Create(expectedResult), Vector256.Hypot(Vector256.Create(+x), Vector256.Create(-y)), Vector256.Create(variance));
            AssertEqual(Vector256.Create(expectedResult), Vector256.Hypot(Vector256.Create(+x), Vector256.Create(+y)), Vector256.Create(variance));

            AssertEqual(Vector256.Create(expectedResult), Vector256.Hypot(Vector256.Create(-y), Vector256.Create(-x)), Vector256.Create(variance));
            AssertEqual(Vector256.Create(expectedResult), Vector256.Hypot(Vector256.Create(-y), Vector256.Create(+x)), Vector256.Create(variance));
            AssertEqual(Vector256.Create(expectedResult), Vector256.Hypot(Vector256.Create(+y), Vector256.Create(-x)), Vector256.Create(variance));
            AssertEqual(Vector256.Create(expectedResult), Vector256.Hypot(Vector256.Create(+y), Vector256.Create(+x)), Vector256.Create(variance));
        }

        [Theory]
        [MemberData(nameof(GenericMathTestMemberData.HypotSingle), MemberType = typeof(GenericMathTestMemberData))]
        public void HypotSingleTest(float x, float y, float expectedResult, float variance)
        {
            AssertEqual(Vector256.Create(expectedResult), Vector256.Hypot(Vector256.Create(-x), Vector256.Create(-y)), Vector256.Create(variance));
            AssertEqual(Vector256.Create(expectedResult), Vector256.Hypot(Vector256.Create(-x), Vector256.Create(+y)), Vector256.Create(variance));
            AssertEqual(Vector256.Create(expectedResult), Vector256.Hypot(Vector256.Create(+x), Vector256.Create(-y)), Vector256.Create(variance));
            AssertEqual(Vector256.Create(expectedResult), Vector256.Hypot(Vector256.Create(+x), Vector256.Create(+y)), Vector256.Create(variance));

            AssertEqual(Vector256.Create(expectedResult), Vector256.Hypot(Vector256.Create(-y), Vector256.Create(-x)), Vector256.Create(variance));
            AssertEqual(Vector256.Create(expectedResult), Vector256.Hypot(Vector256.Create(-y), Vector256.Create(+x)), Vector256.Create(variance));
            AssertEqual(Vector256.Create(expectedResult), Vector256.Hypot(Vector256.Create(+y), Vector256.Create(-x)), Vector256.Create(variance));
            AssertEqual(Vector256.Create(expectedResult), Vector256.Hypot(Vector256.Create(+y), Vector256.Create(+x)), Vector256.Create(variance));
        }

        private void IsEvenInteger<T>(T value)
            where T : INumber<T>
        {
            Assert.Equal(T.IsEvenInteger(value) ? Vector256<T>.AllBitsSet : Vector256<T>.Zero, Vector256.IsEvenInteger(Vector256.Create(value)));
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
            Assert.Equal(T.IsFinite(value) ? Vector256<T>.AllBitsSet : Vector256<T>.Zero, Vector256.IsFinite(Vector256.Create(value)));
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
            Assert.Equal(T.IsInfinity(value) ? Vector256<T>.AllBitsSet : Vector256<T>.Zero, Vector256.IsInfinity(Vector256.Create(value)));
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
            Assert.Equal(T.IsInteger(value) ? Vector256<T>.AllBitsSet : Vector256<T>.Zero, Vector256.IsInteger(Vector256.Create(value)));
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
            Assert.Equal(T.IsNaN(value) ? Vector256<T>.AllBitsSet : Vector256<T>.Zero, Vector256.IsNaN(Vector256.Create(value)));
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
            Assert.Equal(T.IsNegative(value) ? Vector256<T>.AllBitsSet : Vector256<T>.Zero, Vector256.IsNegative(Vector256.Create(value)));
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
            Assert.Equal(T.IsNegativeInfinity(value) ? Vector256<T>.AllBitsSet : Vector256<T>.Zero, Vector256.IsNegativeInfinity(Vector256.Create(value)));
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
            Assert.Equal(T.IsNormal(value) ? Vector256<T>.AllBitsSet : Vector256<T>.Zero, Vector256.IsNormal(Vector256.Create(value)));
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
            Assert.Equal(T.IsOddInteger(value) ? Vector256<T>.AllBitsSet : Vector256<T>.Zero, Vector256.IsOddInteger(Vector256.Create(value)));
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
            Assert.Equal(T.IsPositive(value) ? Vector256<T>.AllBitsSet : Vector256<T>.Zero, Vector256.IsPositive(Vector256.Create(value)));
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
            Assert.Equal(T.IsPositiveInfinity(value) ? Vector256<T>.AllBitsSet : Vector256<T>.Zero, Vector256.IsPositiveInfinity(Vector256.Create(value)));
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
            Assert.Equal(T.IsSubnormal(value) ? Vector256<T>.AllBitsSet : Vector256<T>.Zero, Vector256.IsSubnormal(Vector256.Create(value)));
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
            Assert.Equal(T.IsZero(value) ? Vector256<T>.AllBitsSet : Vector256<T>.Zero, Vector256.IsZero(Vector256.Create(value)));
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
            AssertEqual(Vector256.Create(+expectedResult), Vector256.Lerp(Vector256.Create(+x), Vector256.Create(+y), Vector256.Create(amount)), Vector256<double>.Zero);
            AssertEqual(Vector256.Create((expectedResult == 0.0) ? expectedResult : -expectedResult), Vector256.Lerp(Vector256.Create(-x), Vector256.Create(-y), Vector256.Create(amount)), Vector256<double>.Zero);
        }

        [Theory]
        [MemberData(nameof(GenericMathTestMemberData.LerpSingle), MemberType = typeof(GenericMathTestMemberData))]
        public void LerpSingleTest(float x, float y, float amount, float expectedResult)
        {
            AssertEqual(Vector256.Create(+expectedResult), Vector256.Lerp(Vector256.Create(+x), Vector256.Create(+y), Vector256.Create(amount)), Vector256<float>.Zero);
            AssertEqual(Vector256.Create((expectedResult == 0.0f) ? expectedResult : -expectedResult), Vector256.Lerp(Vector256.Create(-x), Vector256.Create(-y), Vector256.Create(amount)), Vector256<float>.Zero);
        }

        [Theory]
        [MemberData(nameof(GenericMathTestMemberData.MaxDouble), MemberType = typeof(GenericMathTestMemberData))]
        public void MaxDoubleTest(double x, double y, double expectedResult)
        {
            Vector256<double> actualResult = Vector256.Max(Vector256.Create(x), Vector256.Create(y));
            AssertEqual(Vector256.Create(expectedResult), actualResult, Vector256<double>.Zero);
        }

        [Theory]
        [MemberData(nameof(GenericMathTestMemberData.MaxSingle), MemberType = typeof(GenericMathTestMemberData))]
        public void MaxSingleTest(float x, float y, float expectedResult)
        {
            Vector256<float> actualResult = Vector256.Max(Vector256.Create(x), Vector256.Create(y));
            AssertEqual(Vector256.Create(expectedResult), actualResult, Vector256<float>.Zero);
        }

        [Theory]
        [MemberData(nameof(GenericMathTestMemberData.MaxMagnitudeDouble), MemberType = typeof(GenericMathTestMemberData))]
        public void MaxMagnitudeDoubleTest(double x, double y, double expectedResult)
        {
            Vector256<double> actualResult = Vector256.MaxMagnitude(Vector256.Create(x), Vector256.Create(y));
            AssertEqual(Vector256.Create(expectedResult), actualResult, Vector256<double>.Zero);
        }

        [Theory]
        [MemberData(nameof(GenericMathTestMemberData.MaxMagnitudeSingle), MemberType = typeof(GenericMathTestMemberData))]
        public void MaxMagnitudeSingleTest(float x, float y, float expectedResult)
        {
            Vector256<float> actualResult = Vector256.MaxMagnitude(Vector256.Create(x), Vector256.Create(y));
            AssertEqual(Vector256.Create(expectedResult), actualResult, Vector256<float>.Zero);
        }

        [Theory]
        [MemberData(nameof(GenericMathTestMemberData.MaxMagnitudeNumberDouble), MemberType = typeof(GenericMathTestMemberData))]
        public void MaxMagnitudeNumberDoubleTest(double x, double y, double expectedResult)
        {
            Vector256<double> actualResult = Vector256.MaxMagnitudeNumber(Vector256.Create(x), Vector256.Create(y));
            AssertEqual(Vector256.Create(expectedResult), actualResult, Vector256<double>.Zero);
        }

        [Theory]
        [MemberData(nameof(GenericMathTestMemberData.MaxMagnitudeNumberSingle), MemberType = typeof(GenericMathTestMemberData))]
        public void MaxMagnitudeNumberSingleTest(float x, float y, float expectedResult)
        {
            Vector256<float> actualResult = Vector256.MaxMagnitudeNumber(Vector256.Create(x), Vector256.Create(y));
            AssertEqual(Vector256.Create(expectedResult), actualResult, Vector256<float>.Zero);
        }

        [Theory]
        [MemberData(nameof(GenericMathTestMemberData.MaxNumberDouble), MemberType = typeof(GenericMathTestMemberData))]
        public void MaxNumberDoubleTest(double x, double y, double expectedResult)
        {
            Vector256<double> actualResult = Vector256.MaxNumber(Vector256.Create(x), Vector256.Create(y));
            AssertEqual(Vector256.Create(expectedResult), actualResult, Vector256<double>.Zero);
        }

        [Theory]
        [MemberData(nameof(GenericMathTestMemberData.MaxNumberSingle), MemberType = typeof(GenericMathTestMemberData))]
        public void MaxNumberSingleTest(float x, float y, float expectedResult)
        {
            Vector256<float> actualResult = Vector256.MaxNumber(Vector256.Create(x), Vector256.Create(y));
            AssertEqual(Vector256.Create(expectedResult), actualResult, Vector256<float>.Zero);
        }

        [Theory]
        [MemberData(nameof(GenericMathTestMemberData.MinDouble), MemberType = typeof(GenericMathTestMemberData))]
        public void MinDoubleTest(double x, double y, double expectedResult)
        {
            Vector256<double> actualResult = Vector256.Min(Vector256.Create(x), Vector256.Create(y));
            AssertEqual(Vector256.Create(expectedResult), actualResult, Vector256<double>.Zero);
        }

        [Theory]
        [MemberData(nameof(GenericMathTestMemberData.MinSingle), MemberType = typeof(GenericMathTestMemberData))]
        public void MinSingleTest(float x, float y, float expectedResult)
        {
            Vector256<float> actualResult = Vector256.Min(Vector256.Create(x), Vector256.Create(y));
            AssertEqual(Vector256.Create(expectedResult), actualResult, Vector256<float>.Zero);
        }

        [Theory]
        [MemberData(nameof(GenericMathTestMemberData.MinMagnitudeDouble), MemberType = typeof(GenericMathTestMemberData))]
        public void MinMagnitudeDoubleTest(double x, double y, double expectedResult)
        {
            Vector256<double> actualResult = Vector256.MinMagnitude(Vector256.Create(x), Vector256.Create(y));
            AssertEqual(Vector256.Create(expectedResult), actualResult, Vector256<double>.Zero);
        }

        [Theory]
        [MemberData(nameof(GenericMathTestMemberData.MinMagnitudeSingle), MemberType = typeof(GenericMathTestMemberData))]
        public void MinMagnitudeSingleTest(float x, float y, float expectedResult)
        {
            Vector256<float> actualResult = Vector256.MinMagnitude(Vector256.Create(x), Vector256.Create(y));
            AssertEqual(Vector256.Create(expectedResult), actualResult, Vector256<float>.Zero);
        }

        [Theory]
        [MemberData(nameof(GenericMathTestMemberData.MinMagnitudeNumberDouble), MemberType = typeof(GenericMathTestMemberData))]
        public void MinMagnitudeNumberDoubleTest(double x, double y, double expectedResult)
        {
            Vector256<double> actualResult = Vector256.MinMagnitudeNumber(Vector256.Create(x), Vector256.Create(y));
            AssertEqual(Vector256.Create(expectedResult), actualResult, Vector256<double>.Zero);
        }

        [Theory]
        [MemberData(nameof(GenericMathTestMemberData.MinMagnitudeNumberSingle), MemberType = typeof(GenericMathTestMemberData))]
        public void MinMagnitudeNumberSingleTest(float x, float y, float expectedResult)
        {
            Vector256<float> actualResult = Vector256.MinMagnitudeNumber(Vector256.Create(x), Vector256.Create(y));
            AssertEqual(Vector256.Create(expectedResult), actualResult, Vector256<float>.Zero);
        }

        [Theory]
        [MemberData(nameof(GenericMathTestMemberData.MinNumberDouble), MemberType = typeof(GenericMathTestMemberData))]
        public void MinNumberDoubleTest(double x, double y, double expectedResult)
        {
            Vector256<double> actualResult = Vector256.MinNumber(Vector256.Create(x), Vector256.Create(y));
            AssertEqual(Vector256.Create(expectedResult), actualResult, Vector256<double>.Zero);
        }

        [Theory]
        [MemberData(nameof(GenericMathTestMemberData.MinNumberSingle), MemberType = typeof(GenericMathTestMemberData))]
        public void MinNumberSingleTest(float x, float y, float expectedResult)
        {
            Vector256<float> actualResult = Vector256.MinNumber(Vector256.Create(x), Vector256.Create(y));
            AssertEqual(Vector256.Create(expectedResult), actualResult, Vector256<float>.Zero);
        }

        [Theory]
        [MemberData(nameof(GenericMathTestMemberData.RadiansToDegreesDouble), MemberType = typeof(GenericMathTestMemberData))]
        public void RadiansToDegreesDoubleTest(double value, double expectedResult, double variance)
        {
            AssertEqual(Vector256.Create(-expectedResult), Vector256.RadiansToDegrees(Vector256.Create(-value)), Vector256.Create(variance));
            AssertEqual(Vector256.Create(+expectedResult), Vector256.RadiansToDegrees(Vector256.Create(+value)), Vector256.Create(variance));
        }

        [Theory]
        [MemberData(nameof(GenericMathTestMemberData.RadiansToDegreesSingle), MemberType = typeof(GenericMathTestMemberData))]
        public void RadiansToDegreesSingleTest(float value, float expectedResult, float variance)
        {
            AssertEqual(Vector256.Create(-expectedResult), Vector256.RadiansToDegrees(Vector256.Create(-value)), Vector256.Create(variance));
            AssertEqual(Vector256.Create(+expectedResult), Vector256.RadiansToDegrees(Vector256.Create(+value)), Vector256.Create(variance));
        }

        [Theory]
        [MemberData(nameof(GenericMathTestMemberData.RoundDouble), MemberType = typeof(GenericMathTestMemberData))]
        public void RoundDoubleTest(double value, double expectedResult)
        {
            Vector256<double> actualResult = Vector256.Round(Vector256.Create(value));
            AssertEqual(Vector256.Create(expectedResult), actualResult, Vector256<double>.Zero);
        }

        [Theory]
        [MemberData(nameof(GenericMathTestMemberData.RoundSingle), MemberType = typeof(GenericMathTestMemberData))]
        public void RoundSingleTest(float value, float expectedResult)
        {
            Vector256<float> actualResult = Vector256.Round(Vector256.Create(value));
            AssertEqual(Vector256.Create(expectedResult), actualResult, Vector256<float>.Zero);
        }

        [Theory]
        [MemberData(nameof(GenericMathTestMemberData.RoundAwayFromZeroDouble), MemberType = typeof(GenericMathTestMemberData))]
        public void RoundAwayFromZeroDoubleTest(double value, double expectedResult)
        {
            Vector256<double> actualResult = Vector256.Round(Vector256.Create(value), MidpointRounding.AwayFromZero);
            AssertEqual(Vector256.Create(expectedResult), actualResult, Vector256<double>.Zero);
        }

        [Theory]
        [MemberData(nameof(GenericMathTestMemberData.RoundAwayFromZeroSingle), MemberType = typeof(GenericMathTestMemberData))]
        public void RoundAwayFromZeroSingleTest(float value, float expectedResult)
        {
            Vector256<float> actualResult = Vector256.Round(Vector256.Create(value), MidpointRounding.AwayFromZero);
            AssertEqual(Vector256.Create(expectedResult), actualResult, Vector256<float>.Zero);
        }

        [Theory]
        [MemberData(nameof(GenericMathTestMemberData.RoundToEvenDouble), MemberType = typeof(GenericMathTestMemberData))]
        public void RoundToEvenDoubleTest(double value, double expectedResult)
        {
            Vector256<double> actualResult = Vector256.Round(Vector256.Create(value), MidpointRounding.ToEven);
            AssertEqual(Vector256.Create(expectedResult), actualResult, Vector256<double>.Zero);
        }

        [Theory]
        [MemberData(nameof(GenericMathTestMemberData.RoundToEvenSingle), MemberType = typeof(GenericMathTestMemberData))]
        public void RoundToEvenSingleTest(float value, float expectedResult)
        {
            Vector256<float> actualResult = Vector256.Round(Vector256.Create(value), MidpointRounding.ToEven);
            AssertEqual(Vector256.Create(expectedResult), actualResult, Vector256<float>.Zero);
        }

        [Theory]
        [MemberData(nameof(GenericMathTestMemberData.SinDouble), MemberType = typeof(GenericMathTestMemberData))]
        public void SinDoubleTest(double value, double expectedResult, double variance)
        {
            Vector256<double> actualResult = Vector256.Sin(Vector256.Create(value));
            AssertEqual(Vector256.Create(expectedResult), actualResult, Vector256.Create(variance));
        }

        [Theory]
        [MemberData(nameof(GenericMathTestMemberData.SinSingle), MemberType = typeof(GenericMathTestMemberData))]
        public void SinSingleTest(float value, float expectedResult, float variance)
        {
            Vector256<float> actualResult = Vector256.Sin(Vector256.Create(value));
            AssertEqual(Vector256.Create(expectedResult), actualResult, Vector256.Create(variance));
        }

        [Theory]
        [MemberData(nameof(GenericMathTestMemberData.SinCosDouble), MemberType = typeof(GenericMathTestMemberData))]
        public void SinCosDoubleTest(double value, double expectedResultSin, double expectedResultCos, double allowedVarianceSin, double allowedVarianceCos)
        {
            (Vector256<double> resultSin, Vector256<double> resultCos) = Vector256.SinCos(Vector256.Create(value));
            AssertEqual(Vector256.Create(expectedResultSin), resultSin, Vector256.Create(allowedVarianceSin));
            AssertEqual(Vector256.Create(expectedResultCos), resultCos, Vector256.Create(allowedVarianceCos));
        }

        [Theory]
        [MemberData(nameof(GenericMathTestMemberData.SinCosSingle), MemberType = typeof(GenericMathTestMemberData))]
        public void SinCosSingleTest(float value, float expectedResultSin, float expectedResultCos, float allowedVarianceSin, float allowedVarianceCos)
        {
            (Vector256<float> resultSin, Vector256<float> resultCos) = Vector256.SinCos(Vector256.Create(value));
            AssertEqual(Vector256.Create(expectedResultSin), resultSin, Vector256.Create(allowedVarianceSin));
            AssertEqual(Vector256.Create(expectedResultCos), resultCos, Vector256.Create(allowedVarianceCos));
        }

        [Theory]
        [MemberData(nameof(GenericMathTestMemberData.TruncateDouble), MemberType = typeof(GenericMathTestMemberData))]
        public void TruncateDoubleTest(double value, double expectedResult)
        {
            Vector256<double> actualResult = Vector256.Truncate(Vector256.Create(value));
            AssertEqual(Vector256.Create(expectedResult), actualResult, Vector256<double>.Zero);
        }

        [Theory]
        [MemberData(nameof(GenericMathTestMemberData.TruncateSingle), MemberType = typeof(GenericMathTestMemberData))]
        public void TruncateSingleTest(float value, float expectedResult)
        {
            Vector256<float> actualResult = Vector256.Truncate(Vector256.Create(value));
            AssertEqual(Vector256.Create(expectedResult), actualResult, Vector256<float>.Zero);
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        private void AllAnyNoneTest<T>(T value1, T value2)
            where T : struct, INumber<T>
        {
            var input1 = Vector256.Create<T>(value1);
            var input2 = Vector256.Create<T>(value2);

            Assert.True(Vector256.All(input1, value1));
            Assert.True(Vector256.All(input2, value2));
            Assert.False(Vector256.All(input1.WithElement(0, value2), value1));
            Assert.False(Vector256.All(input2.WithElement(0, value1), value2));
            Assert.False(Vector256.All(input1, value2));
            Assert.False(Vector256.All(input2, value1));
            Assert.False(Vector256.All(input1.WithElement(0, value2), value2));
            Assert.False(Vector256.All(input2.WithElement(0, value1), value1));

            Assert.True(Vector256.Any(input1, value1));
            Assert.True(Vector256.Any(input2, value2));
            Assert.True(Vector256.Any(input1.WithElement(0, value2), value1));
            Assert.True(Vector256.Any(input2.WithElement(0, value1), value2));
            Assert.False(Vector256.Any(input1, value2));
            Assert.False(Vector256.Any(input2, value1));
            Assert.True(Vector256.Any(input1.WithElement(0, value2), value2));
            Assert.True(Vector256.Any(input2.WithElement(0, value1), value1));

            Assert.False(Vector256.None(input1, value1));
            Assert.False(Vector256.None(input2, value2));
            Assert.False(Vector256.None(input1.WithElement(0, value2), value1));
            Assert.False(Vector256.None(input2.WithElement(0, value1), value2));
            Assert.True(Vector256.None(input1, value2));
            Assert.True(Vector256.None(input2, value1));
            Assert.False(Vector256.None(input1.WithElement(0, value2), value2));
            Assert.False(Vector256.None(input2.WithElement(0, value1), value1));
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        private void AllAnyNoneTest_IFloatingPointIeee754<T>(T value)
            where T : struct, IFloatingPointIeee754<T>
        {
            var input = Vector256.Create<T>(value);

            Assert.False(Vector256.All(input, value));
            Assert.False(Vector256.Any(input, value));
            Assert.True(Vector256.None(input, value));
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
            var input1 = Vector256.Create<T>(allBitsSet);
            var input2 = Vector256.Create<T>(value2);

            Assert.True(Vector256.AllWhereAllBitsSet(input1));
            Assert.False(Vector256.AllWhereAllBitsSet(input2));
            Assert.False(Vector256.AllWhereAllBitsSet(input1.WithElement(0, value2)));
            Assert.False(Vector256.AllWhereAllBitsSet(input2.WithElement(0, allBitsSet)));

            Assert.True(Vector256.AnyWhereAllBitsSet(input1));
            Assert.False(Vector256.AnyWhereAllBitsSet(input2));
            Assert.True(Vector256.AnyWhereAllBitsSet(input1.WithElement(0, value2)));
            Assert.True(Vector256.AnyWhereAllBitsSet(input2.WithElement(0, allBitsSet)));

            Assert.False(Vector256.NoneWhereAllBitsSet(input1));
            Assert.True(Vector256.NoneWhereAllBitsSet(input2));
            Assert.False(Vector256.NoneWhereAllBitsSet(input1.WithElement(0, value2)));
            Assert.False(Vector256.NoneWhereAllBitsSet(input2.WithElement(0, allBitsSet)));
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
            var input1 = Vector256.Create<T>(value1);
            var input2 = Vector256.Create<T>(value2);

            Assert.Equal(Vector256<T>.Count, Vector256.Count(input1, value1));
            Assert.Equal(Vector256<T>.Count, Vector256.Count(input2, value2));
            Assert.Equal(Vector256<T>.Count - 1, Vector256.Count(input1.WithElement(0, value2), value1));
            Assert.Equal(Vector256<T>.Count - 1, Vector256.Count(input2.WithElement(0, value1), value2));
            Assert.Equal(0, Vector256.Count(input1, value2));
            Assert.Equal(0, Vector256.Count(input2, value1));
            Assert.Equal(1, Vector256.Count(input1.WithElement(0, value2), value2));
            Assert.Equal(1, Vector256.Count(input2.WithElement(0, value1), value1));

            Assert.Equal(0, Vector256.IndexOf(input1, value1));
            Assert.Equal(0, Vector256.IndexOf(input2, value2));
            Assert.Equal(1, Vector256.IndexOf(input1.WithElement(0, value2), value1));
            Assert.Equal(1, Vector256.IndexOf(input2.WithElement(0, value1), value2));
            Assert.Equal(-1, Vector256.IndexOf(input1, value2));
            Assert.Equal(-1, Vector256.IndexOf(input2, value1));
            Assert.Equal(0, Vector256.IndexOf(input1.WithElement(0, value2), value2));
            Assert.Equal(0, Vector256.IndexOf(input2.WithElement(0, value1), value1));

            Assert.Equal(Vector256<T>.Count - 1, Vector256.LastIndexOf(input1, value1));
            Assert.Equal(Vector256<T>.Count - 1, Vector256.LastIndexOf(input2, value2));
            Assert.Equal(Vector256<T>.Count - 1, Vector256.LastIndexOf(input1.WithElement(0, value2), value1));
            Assert.Equal(Vector256<T>.Count - 1, Vector256.LastIndexOf(input2.WithElement(0, value1), value2));
            Assert.Equal(-1, Vector256.LastIndexOf(input1, value2));
            Assert.Equal(-1, Vector256.LastIndexOf(input2, value1));
            Assert.Equal(0, Vector256.LastIndexOf(input1.WithElement(0, value2), value2));
            Assert.Equal(0, Vector256.LastIndexOf(input2.WithElement(0, value1), value1));
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        private void CountIndexOfLastIndexOfTest_IFloatingPointIeee754<T>(T value)
            where T : struct, IFloatingPointIeee754<T>
        {
            var input = Vector256.Create<T>(value);

            Assert.Equal(0, Vector256.Count(input, value));
            Assert.Equal(-1, Vector256.IndexOf(input, value));
            Assert.Equal(-1, Vector256.LastIndexOf(input, value));
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
            var input1 = Vector256.Create<T>(allBitsSet);
            var input2 = Vector256.Create<T>(value2);

            Assert.Equal(Vector256<T>.Count, Vector256.CountWhereAllBitsSet(input1));
            Assert.Equal(0, Vector256.CountWhereAllBitsSet(input2));
            Assert.Equal(Vector256<T>.Count - 1, Vector256.CountWhereAllBitsSet(input1.WithElement(0, value2)));
            Assert.Equal(1, Vector256.CountWhereAllBitsSet(input2.WithElement(0, allBitsSet)));

            Assert.Equal(0, Vector256.IndexOfWhereAllBitsSet(input1));
            Assert.Equal(-1, Vector256.IndexOfWhereAllBitsSet(input2));
            Assert.Equal(1, Vector256.IndexOfWhereAllBitsSet(input1.WithElement(0, value2)));
            Assert.Equal(0, Vector256.IndexOfWhereAllBitsSet(input2.WithElement(0, allBitsSet)));

            Assert.Equal(Vector256<T>.Count - 1, Vector256.LastIndexOfWhereAllBitsSet(input1));
            Assert.Equal(-1, Vector256.LastIndexOfWhereAllBitsSet(input2));
            Assert.Equal(Vector256<T>.Count - 1, Vector256.LastIndexOfWhereAllBitsSet(input1.WithElement(0, value2)));
            Assert.Equal(0, Vector256.LastIndexOfWhereAllBitsSet(input2.WithElement(0, allBitsSet)));
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

            Vector256<T> left = Vector256.CreateSequence<T>(start, T.One);
            Vector256<T> right = Vector256.Create<T>(T.MaxValue - T.CreateTruncating(Vector256<T>.Count) + T.One);

            Vector256<T> result = Vector256.AddSaturate(left, right);

            for (int i = 0; i < Vector256<T>.Count - 1; i++)
            {
                T expectedResult = left[i] + right[i];
                Assert.Equal(expectedResult, result[i]);
            }

            Assert.Equal(T.MaxValue, result[Vector256<T>.Count - 1]);
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        private void AddSaturateToMinTest<T>(T start)
            where T : struct, ISignedNumber<T>, IMinMaxValue<T>
        {
            // We just take it as a parameter to prevent constant folding
            Debug.Assert(start == T.NegativeOne);

            Vector256<T> left = Vector256.CreateSequence<T>(start, T.NegativeOne);
            Vector256<T> right = Vector256.Create<T>(T.MinValue + T.CreateTruncating(Vector256<T>.Count) - T.One);

            Vector256<T> result = Vector256.AddSaturate(left, right);

            for (int i = 0; i < Vector256<T>.Count - 1; i++)
            {
                T expectedResult = left[i] + right[i];
                Assert.Equal(expectedResult, result[i]);
            }

            Assert.Equal(T.MinValue, result[Vector256<T>.Count - 1]);
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

        private (Vector256<TFrom> lower, Vector256<TFrom> upper) GetNarrowWithSaturationInputs<TFrom, TTo>()
            where TFrom : unmanaged, IMinMaxValue<TFrom>, INumber<TFrom>
            where TTo : unmanaged, IMinMaxValue<TTo>, INumber<TTo>
        {
            Vector256<TFrom> lower = Vector256.Create<TFrom>(TFrom.CreateTruncating(TTo.MaxValue) - TFrom.CreateTruncating(Vector256<TFrom>.Count) + TFrom.One)
                                  + Vector256.CreateSequence<TFrom>(TFrom.One, TFrom.One);

            Vector256<TFrom> upper = Vector256.Create<TFrom>(TFrom.CreateTruncating(TTo.MinValue) + TFrom.CreateTruncating(Vector256<TFrom>.Count) - TFrom.One)
                                  - Vector256.CreateSequence<TFrom>(TFrom.One, TFrom.One);

            return (lower, upper);
        }

        private void NarrowWithSaturationTest<TFrom, TTo>(Vector256<TFrom> lower, Vector256<TFrom> upper, Vector256<TTo> result)
            where TFrom : unmanaged, INumber<TFrom>
            where TTo : unmanaged, INumber<TTo>
        {
            for (int i = 0; i < Vector256<TFrom>.Count; i++)
            {
                TTo expectedResult = TTo.CreateSaturating(lower[i]);
                Assert.Equal(expectedResult, result[i]);
            }

            for (int i = 0; i < Vector256<TFrom>.Count; i++)
            {
                TTo expectedResult = TTo.CreateSaturating(upper[i]);
                Assert.Equal(expectedResult, result[Vector256<TFrom>.Count + i]);
            }
        }

        [Fact]
        public void NarrowWithSaturationInt16Test()
        {
            (Vector256<short> lower, Vector256<short> upper) = GetNarrowWithSaturationInputs<short, sbyte>();
            Vector256<sbyte> result = Vector256.NarrowWithSaturation(lower, upper);
            NarrowWithSaturationTest(lower, upper, result);
        }

        [Fact]
        public void NarrowWithSaturationInt32Test()
        {
            (Vector256<int> lower, Vector256<int> upper) = GetNarrowWithSaturationInputs<int, short>();
            Vector256<short> result = Vector256.NarrowWithSaturation(lower, upper);
            NarrowWithSaturationTest(lower, upper, result);
        }

        [Fact]
        public void NarrowWithSaturationInt64Test()
        {
            (Vector256<long> lower, Vector256<long> upper) = GetNarrowWithSaturationInputs<long, int>();
            Vector256<int> result = Vector256.NarrowWithSaturation(lower, upper);
            NarrowWithSaturationTest(lower, upper, result);
        }

        [Fact]
        public void NarrowWithSaturationUInt16Test()
        {
            (Vector256<ushort> lower, Vector256<ushort> upper) = GetNarrowWithSaturationInputs<ushort, byte>();
            Vector256<byte> result = Vector256.NarrowWithSaturation(lower, upper);
            NarrowWithSaturationTest(lower, upper, result);
        }

        [Fact]
        public void NarrowWithSaturationUInt32Test()
        {
            (Vector256<uint> lower, Vector256<uint> upper) = GetNarrowWithSaturationInputs<uint, ushort>();
            Vector256<ushort> result = Vector256.NarrowWithSaturation(lower, upper);
            NarrowWithSaturationTest(lower, upper, result);
        }

        [Fact]
        public void NarrowWithSaturationUInt64Test()
        {
            (Vector256<ulong> lower, Vector256<ulong> upper) = GetNarrowWithSaturationInputs<ulong, uint>();
            Vector256<uint> result = Vector256.NarrowWithSaturation(lower, upper);
            NarrowWithSaturationTest(lower, upper, result);
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        private void SubtractSaturateToMaxTest<T>(T start)
            where T : struct, ISignedNumber<T>, IMinMaxValue<T>
        {
            // We just take it as a parameter to prevent constant folding
            Debug.Assert(start == T.NegativeOne);

            Vector256<T> left = Vector256.Create<T>(T.MaxValue - T.CreateTruncating(Vector256<T>.Count) + T.One);
            Vector256<T> right = Vector256.CreateSequence<T>(start, T.NegativeOne);

            Vector256<T> result = Vector256.SubtractSaturate(left, right);

            for (int i = 0; i < Vector256<T>.Count - 1; i++)
            {
                T expectedResult = left[i] - right[i];
                Assert.Equal(expectedResult, result[i]);
            }

            Assert.Equal(T.MaxValue, result[Vector256<T>.Count - 1]);
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        private void SubtractSaturateToMinTest<T>(T start)
            where T : struct, INumber<T>, IMinMaxValue<T>
        {
            // We just take it as a parameter to prevent constant folding
            Debug.Assert(start == T.One);

            Vector256<T> left = Vector256.Create<T>(T.MinValue + T.CreateTruncating(Vector256<T>.Count) - T.One);
            Vector256<T> right = Vector256.CreateSequence<T>(start, T.One);

            Vector256<T> result = Vector256.SubtractSaturate(left, right);

            for (int i = 0; i < Vector256<T>.Count - 1; i++)
            {
                T expectedResult = left[i] - right[i];
                Assert.Equal(expectedResult, result[i]);
            }

            Assert.Equal(T.MinValue, result[Vector256<T>.Count - 1]);
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
