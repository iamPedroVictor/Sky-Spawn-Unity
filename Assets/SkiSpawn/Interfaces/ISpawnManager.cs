using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace SkySpawn.Interfaces
{
    public interface ISpawnManager
    {
        bool isSpawn();
        void OnUpdate();
        void Spawn(GameObject go, int amount);
        void Next();
    }
}
