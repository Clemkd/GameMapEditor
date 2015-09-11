using ProtoBuf;

namespace GameMapEditor
{
    //[Serializable]
    [ProtoContract]
    public enum LayerType
    {
        [ProtoMember(1)]
        /// <summary>
        /// Layer de type supérieur
        /// </summary>
        Upper,

        [ProtoMember(2)]
        /// <summary>
        /// Layer de type inférieur
        /// </summary>
        Lower
    }
}
