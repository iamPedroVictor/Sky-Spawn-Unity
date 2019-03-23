using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SkySpawn.Interfaces;
using System;

namespace SkySpawn
{
    public class Waypoints : MonoBehaviour, IWaypoint
    {
        [SerializeField]
        private float Radius = 1f;
        [SerializeField]
        private List<Transform> waypoints;
        [SerializeField]
        private Color waypointsColor = Color.white;
        private Transform[] tArray;


        public Vector3 GetVector3Position(int index = 0)
        {
            return waypoints[index].position;
        }

        public Quaternion GetQuaternion(int index = 0)
        {
            return waypoints[index].rotation;
        }

        public int Length()
        {
            return waypoints.Count;
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = waypointsColor;
            tArray = GetComponentsInChildren<Transform>();
            waypoints.Clear();
            foreach (var pathObj in tArray)
            {   
                if(pathObj != this.transform)
                {
                    waypoints.Add(pathObj);
                }

            }

            if (waypoints.Count > 0 && (waypoints.Count >= 2 && tArray[1] == null))
                return;

            for (int i = 0; i < waypoints.Count; i++)
            {
                Gizmos.DrawWireSphere(waypoints[i].position, Radius);
            }
        }

        public void Delete(int index)
        {
            var waypointToDelate = waypoints[index];
            Destroy(waypointToDelate);
            waypoints.Remove(waypointToDelate);
        }
    }

}
