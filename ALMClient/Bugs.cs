
/// <remarks/>
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
[System.Xml.Serialization.XmlRootAttribute(Namespace = "", IsNullable = false)]
public partial class Entity
{
    [System.Xml.Serialization.XmlArrayItemAttribute("Field", IsNullable = false)]
    public EntityField[] Fields { get; set; }

    [System.Xml.Serialization.XmlAttributeAttribute()]
    public string Type { get; set; }
}

[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
public partial class EntityField
{
    public string Value { get; set; }

    [System.Xml.Serialization.XmlAttributeAttribute()]
    public string Name { get; set; }
}

