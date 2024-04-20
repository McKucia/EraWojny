using FishNet.CodeGenerating;
using FishNet.Connection;
using FishNet.Documenting;
using FishNet.Managing;
using FishNet.Object;
using FishNet.Object.Prediction;
using FishNet.Utility;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo(UtilityConstants.GENERATED_ASSEMBLY_NAME)]
namespace FishNet.Serializing
{
    /* THIS IS IN DRAFTING / WIP. Do not attempt to use or modify this file. */
    /* THIS IS IN DRAFTING / WIP. Do not attempt to use or modify this file. */
    /* THIS IS IN DRAFTING / WIP. Do not attempt to use or modify this file. */

    /// <summary>
    /// Writes data to a buffer.
    /// </summary>
    public partial class Writer
    {
        internal void WriteInt32Delta(int a, int b)
        {
            WriteUInt32Delta((uint)a, (uint)b);
        }

        internal void WriteUInt32Delta(uint a, uint b)
        {
            long difference = (b - a);
            WriteInt64(difference);
        }


        [NotSerializer]
        internal void WriteUInt16Delta(ushort a, ushort b)
        {
            int difference = (b - a);
            WriteInt32(difference);
        }
        [NotSerializer]
        internal void WriteInt16Delta(short a, short b) => WriteUInt16Delta((ushort)a, (ushort)b);

        //Draft / WIP below. May become discarded.

        //internal enum DeltaPackType
        //{
        //    Packed = 1,
        //    PackedLess = 2,
        //}
        //internal enum Vector3DeltaA : byte
        //{
        //    Unset = 0,
        //    XPacked = 1,
        //    XPackedLess = 2,
        //    XUnpacked = 4,
        //    YPacked = 8,
        //    YPackedLess = 16,
        //    YUnpacked = 32,
        //    ZPacked = 64,
        //    ZPackedLess = 128,
        //    ZUnpacked = 4,
        //}

        //internal const float LARGEST_PACKED_DIFFERENCE = ((float)sbyte.MaxValue / ACCURACY);
        //internal const float LARGEST_PACKEDLESS_DIFFERENCE = ((float)short.MaxValue / ACCURACY);
        //internal const float ACCURACY = 1000f;
        //internal const float ACCURACY_DECIMAL = 0.001f;


        //[NotSerializer]
        //public void WriteVector3Delta(Vector3 a, Vector3 b)
        //{
        //    //Start as the highest.

        //    float xDiff = (b.x - a.x);
        //    float yDiff = (b.x - a.x);
        //    float zDiff = (b.x - a.x);

        //    float absXDiff = Mathf.Abs(xDiff);
        //    float absYDiff = Mathf.Abs(yDiff);
        //    float absZDiff = Mathf.Abs(zDiff);

        //    float largestDiff = 0f;
        //    Vector3Delta delta = Vector3Delta.Unset;
        //    if (absXDiff >= ACCURACY_DECIMAL)
        //    {
        //        delta |= Vector3Delta.HasX;
        //        largestDiff = absXDiff;
        //    }
        //    if (absYDiff >= ACCURACY_DECIMAL)
        //    {
        //        delta |= Vector3Delta.HasY;
        //        largestDiff = Mathf.Max(largestDiff, absYDiff);
        //    }
        //    if (absZDiff >= ACCURACY_DECIMAL)
        //    {
        //        delta |= Vector3Delta.HasZ;
        //        largestDiff = Mathf.Max(largestDiff, absZDiff);
        //    }

        //    /* If packed is not specified then unpacked
        //     * is assumed. */
        //    if (largestDiff <= LARGEST_PACKED_DIFFERENCE)
        //        delta |= Vector3Delta.Packed;
        //    else if (largestDiff <= LARGEST_PACKEDLESS_DIFFERENCE)
        //        delta |= Vector3Delta.PackedLess;



        //    xDiff = Mathf.CeilToInt(xDiff * ACCURACY);
        //    yDiff = Mathf.CeilToInt(yDiff * ACCURACY);
        //    zDiff = Mathf.CeilToInt(zDiff * ACCURACY);

        //    Reserve(1);


        //    UIntFloat valA;
        //    UIntFloat valB;

        //    valA = new UIntFloat(a.x);
        //    valB = new UIntFloat(b.x);
        //    WriteUInt32(valB.UIntValue - valA.UIntValue, packType);
        //    Debug.Log("Diff " + (valB.UIntValue - valA.UIntValue) + ", " + (valA.UIntValue - valB.UIntValue));
        //    valA = new UIntFloat(a.y);
        //    valB = new UIntFloat(b.y);
        //    WriteUInt32(valB.UIntValue - valA.UIntValue, packType);
        //    valA = new UIntFloat(a.z);
        //    valB = new UIntFloat(b.z);
        //    WriteUInt32(valB.UIntValue - valA.UIntValue, packType);
        //}

    }
}
