using System;
using System.Xml.Serialization;

namespace Croquette.Data
{
    [Serializable]
    public class DT_Monster
    {
        [XmlAttribute("ID")]
        public int id;
        [XmlAttribute("Name")]
        public string name;
        [XmlAttribute("Tag")]
        public string tag;
        [XmlAttribute("PrefabPath")]
        public string prefabPath;
        [XmlAttribute("Hp")]
        public float hp;
        [XmlAttribute("Atk")]
        public float atk;
        [XmlAttribute("AtkDelayForSec")]
        public float atkDelayForSec;
        [XmlAttribute("MoveSpeed")]
        public float moveSpeed;
        [XmlAttribute("JumpSpeed")]
        public float jumpSpeed;
    }

    [Serializable]
    [XmlRoot("MonsterCollection")]
    public class DT_MonsterCollection
    {
        [XmlArray("Monsters")]
        [XmlArrayItem("Monster", typeof(DT_Monster))]
        public DT_Monster[] monsters;

        public bool HasMonsters()
        {
            return monsters != null && monsters.Length > 0;
        }
        public int GetMonstersCount()
        {
            return monsters != null ? monsters.Length : 0;
        }
    }
}