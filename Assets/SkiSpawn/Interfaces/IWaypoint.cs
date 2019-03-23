using UnityEngine;

namespace SkySpawn.Interfaces
{
    public interface IWaypoint
    {
        Vector3 GetVector3Position(int index = 0);
        Quaternion GetQuaternion(int index = 0);
    }

}
