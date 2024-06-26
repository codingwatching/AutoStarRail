/* this is generated by nino */
using System.Runtime.CompilerServices;

namespace Game
{
    public partial class RoleData
    {
        public static RoleData.SerializationHelper NinoSerializationHelper = new RoleData.SerializationHelper();
        public unsafe class SerializationHelper: Nino.Serialization.NinoWrapperBase<RoleData>
        {
            #region NINO_CODEGEN
            public SerializationHelper()
            {

            }
            
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public override void Serialize(RoleData value, ref Nino.Serialization.Writer writer)
            {
                if(value == null)
                {
                    writer.Write(false);
                    return;
                }
                writer.Write(true);
                writer.Write(value.RoleId);
                writer.Write(value.RoleName);
                writer.Write(value.RoleLevel);
                writer.Write(value.RoleExp);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public override RoleData Deserialize(Nino.Serialization.Reader reader)
            {
                if(!reader.ReadBool())
                    return null;
                RoleData value = new RoleData();
                value.RoleId = reader.Read<System.Int32>(sizeof(System.Int32));
                value.RoleName = reader.ReadString();
                value.RoleLevel = reader.Read<System.Int32>(sizeof(System.Int32));
                value.RoleExp = reader.Read<System.Int32>(sizeof(System.Int32));
                return value;
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public override int GetSize(RoleData value)
            {
                if(value == null)
                {
                    return 1;
                }
                int ret = 1;
                ret += 4; // size for RoleId
                ret += Nino.Serialization.Serializer.GetSize(value.RoleName);
                ret += 4; // size for RoleLevel
                ret += 4; // size for RoleExp
                return ret;
            }
            #endregion
        }
    }
}