using Pathfinding;
using System.Collections;
using UnityEngine;

namespace LittleHalberd
{
    [System.Serializable]
    public class PathfinderData
    {
        public Transform Target;
        public float ActivateDistance;
        public float ReachedDist;
        public float PathUpdateTimer;
        public float MoveDirDist;

        public Path path;
        public int currentWayPoint;
        public Seeker seeker;

        public Coroutine UpdatePathRoutine;
    }
}