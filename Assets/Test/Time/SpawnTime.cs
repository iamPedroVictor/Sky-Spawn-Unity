using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SkySpawn.Interfaces;

namespace SkySpawn
{
    public class SpawnTime : MonoBehaviour, ISpawnManager
    {
        [System.Serializable]
        public struct SpawnDataTime
        {
            public string name;
            public float TimeToSpawn;
            public GameObject objectPrefab;
            public int amount;
        }

        public SpawnDataTime[] spawnData;
        private int current = 0;
        private float currentTime = 0;
        private bool currentTimeUpdate;

        public Waypoints waypoints;

        public bool isSpawn()
        {
            return currentTime >= spawnData[current].TimeToSpawn;
        }

        public void OnUpdate()
        {
            if(currentTimeUpdate)
                currentTime += Time.deltaTime;
            
            if (isSpawn())
            {
                EventManager.TriggerEvent("SpawnTime", spawnData[current].objectPrefab, spawnData[current].amount);
                if(current < spawnData.Length - 1)
                {
                    Next();
                }
            }
        }

        public void Spawn(GameObject go, int amount)
        {
            for(int i = 0; i < amount; i++)
            {
                int indexWaypoint = Random.Range(0, waypoints.Length());
                Vector3 pos = waypoints.GetVector3Position(indexWaypoint);
                Instantiate(go, pos, Quaternion.identity);
                waypoints.Delete(indexWaypoint);
            }

        }

        public void Next()
        {
            current++;
            if(current == spawnData.Length - 1)
            {
                StopTime();
            }
        }

        private void StopTime()
        {
            currentTimeUpdate = false;
        }

        // Use this for initialization
        void Start()
        {
            currentTime = 0;
            currentTimeUpdate = true;
            current = 0;
        }

        // Update is called once per frame
        void Update()
        {
            OnUpdate();
        }

        private void OnEnable()
        {
            EventManager.StartListening("SpawnTime", Spawn);
        }

        private void OnDisable()
        {
            EventManager.StopListening("SpawnTime", Spawn);
        }
    }
}
