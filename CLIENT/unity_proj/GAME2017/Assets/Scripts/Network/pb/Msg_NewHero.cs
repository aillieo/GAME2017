//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

// Generated from: Msg_NewHero.proto
// Note: requires additional types generated from: Dat_General.proto
// Note: requires additional types generated from: Dat_HeroData.proto
namespace ProtoBuf
{
  [global::System.Serializable, global::ProtoBuf.ProtoContract(Name=@"C2S_NewHero")]
  public partial class C2S_NewHero : global::ProtoBuf.IExtensible
  {
    public C2S_NewHero() {}
    
    private ProtoBuf.DAT_ElementType _elementType;
    [global::ProtoBuf.ProtoMember(1, IsRequired = true, Name=@"elementType", DataFormat = global::ProtoBuf.DataFormat.Default)]
    public ProtoBuf.DAT_ElementType elementType
    {
      get { return _elementType; }
      set { _elementType = value; }
    }
    private global::ProtoBuf.IExtension extensionObject;
    global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
      { return global::ProtoBuf.Extensible.GetExtensionObject(ref extensionObject, createIfMissing); }
  }
  
  [global::System.Serializable, global::ProtoBuf.ProtoContract(Name=@"S2C_NewHero")]
  public partial class S2C_NewHero : global::ProtoBuf.IExtensible
  {
    public S2C_NewHero() {}
    
    private int _ret;
    [global::ProtoBuf.ProtoMember(1, IsRequired = true, Name=@"ret", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    public int ret
    {
      get { return _ret; }
      set { _ret = value; }
    }
    private ProtoBuf.DAT_HeroData _hero;
    [global::ProtoBuf.ProtoMember(2, IsRequired = true, Name=@"hero", DataFormat = global::ProtoBuf.DataFormat.Default)]
    public ProtoBuf.DAT_HeroData hero
    {
      get { return _hero; }
      set { _hero = value; }
    }
    private global::ProtoBuf.IExtension extensionObject;
    global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
      { return global::ProtoBuf.Extensible.GetExtensionObject(ref extensionObject, createIfMissing); }
  }
  
}