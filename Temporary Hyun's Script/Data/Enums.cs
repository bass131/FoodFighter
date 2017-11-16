using System.Xml;
using System.Xml.Serialization;

namespace Croquette.Data
{
    public enum DT_TYPE : int
    {
        DT_PLAYER,
        DT_MONSTER,
        DT_PLACEMENT,
    }

    public enum kEntityType
    {
        PLAYER,
        MONSTER,
    }

    public enum ObjectType : int
    {
        PLAYER = 0,
        //MONSTER1 = 50, // example
        //MONSTER2 = 51, // example
        OBSTACLE = 1,
        MONSTER = 2,
    }
}
