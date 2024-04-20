using FishNet.CodeGenerating;
using FishNet.Utility;
using System.Runtime.CompilerServices;

/* THIS IS IN DRAFTING / WIP. Do not attempt to use or modify this file. */
/* THIS IS IN DRAFTING / WIP. Do not attempt to use or modify this file. */
/* THIS IS IN DRAFTING / WIP. Do not attempt to use or modify this file. */

[assembly: InternalsVisibleTo(UtilityConstants.GENERATED_ASSEMBLY_NAME)]
//Required for internal tests.
[assembly: InternalsVisibleTo(UtilityConstants.TEST_ASSEMBLY_NAME)]
namespace FishNet.Serializing
{

    /// <summary>
    /// Reads data from a buffer.
    /// </summary>
    public partial class Reader
    {
        //[NotSerializer]
        //public Vector3 ReadVector3Delta(AutoPackType packType = AutoPackType.Packed)
        //{
        //    IntFloat x = new IntFloat(ReadInt32(packType));
        //    IntFloat y = new IntFloat(ReadInt32(packType));
        //    IntFloat z = new IntFloat(ReadInt32(packType));
        //    return new Vector3(x.FloatValue, y.FloatValue, z.FloatValue);

        //}

        [NotSerializer]
        internal uint ReadUInt32Delta(uint prev)
        {
            long next = ReadInt64();
            return (prev + (uint)next);
        }
    }
}
