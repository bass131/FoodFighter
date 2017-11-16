using System;
using System.Xml.Serialization;

namespace Croquette.Data
{
    [Serializable]
    public class DT_Player
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
        [XmlAttribute("Projectile")]
        public int projectile;
    }

    [Serializable]
    [XmlRoot("PlayerCollection")]
    public class DT_PlayerCollection
    {
        [XmlArray("Players")]
        [XmlArrayItem("Player", typeof(DT_Player))]
        public DT_Player[] players;

        public bool HasPlayers()
        {
            return players != null && players.Length > 0;
        }
        public int GetPlayersCount()
        {
            return players != null ? players.Length : 0;
        }
    }
}