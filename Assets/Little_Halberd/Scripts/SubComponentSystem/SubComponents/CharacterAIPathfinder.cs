using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LittleHalberd
{
    public class CharacterAIPathfinder : SubComponent
    {
        public float ActivateDistance;
        public float ReachedDistance;
        public float PathUpdateTimer;
        public float MoveDirDistance;

        public PathfinderData pathfinderData;

        private void Start()
        {
            pathfinderData = new PathfinderData
            {
                Target = CharacterManager.Instance.PlayableCharacter.transform,
                ActivateDistance = this.ActivateDistance,
                ReachedDist = this.ReachedDistance,
                PathUpdateTimer = this.PathUpdateTimer,
                MoveDirDist = this.MoveDirDistance,

                path = null,
                currentWayPoint = 0,
                seeker = control.gameObject.GetComponent<Seeker>(),
                UpdatePathRoutine = null,
            };

            subComponentProcessor.pathfinderData = pathfinderData;

            pathfinderData.UpdatePathRoutine = 
                StartCoroutine(UpdatePath(subComponentProcessor.characterAIData.AICurrentState,
                                          subComponentProcessor.characterAIData.FollowEnabled,
                                          control.PATHFINDER_DATA.PathUpdateTimer));

        }
        public override void OnFixedUpdate()
        {            
        }

        public override void OnUpdate()
        {     
        }
        private IEnumerator UpdatePath(AIState aiCurrentState, bool followEnabled, float updateDelay)
        {
            while(true)
            {

                if (followEnabled && pathfinderData.seeker.IsDone())
                {
                    pathfinderData.seeker.StartPath(control.RIGID_BODY.position,
                                                    pathfinderData.Target.position, OnPathComplete);
                }
                yield return new WaitForSeconds(updateDelay);
                
            }
        }
        private void OnPathComplete(Path p)
        {
            if (!p.error)
            {
                pathfinderData.path = p;
                pathfinderData.currentWayPoint = 0;
            }
        }
    }
}