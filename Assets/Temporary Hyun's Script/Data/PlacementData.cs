using System;
using System.Xml.Serialization;

namespace Croquette.Data
{
    [Serializable]
    public class DT_Placement
    {
        [XmlAttribute("Index")]
        public int index;
        [XmlAttribute("StageNumber")]
        public int stageNumber;
        [XmlAttribute("ObjectID")]
        public int objectId;
        [XmlAttribute("ObjectType")]
        public int objectType;
        [XmlAttribute("StartPositionX")]
        public float startPositionX;
        [XmlAttribute("StartPositionY")]
        public float startPositionY;
    }

    [Serializable]
    [XmlRoot("PlacementCollection")]
    public class DT_PlacementCollection
    {
        [XmlArray("Placements")]
        [XmlArrayItem("Placement", typeof(DT_Placement))]
        public DT_Placement[] placement;

        public bool HasPlacement()
        {
            return placement != null && placement.Length > 0;
        }
        public int GetPlacementCount()
        {
            return placement != null ? placement.Length : 0;
        }
    }


}