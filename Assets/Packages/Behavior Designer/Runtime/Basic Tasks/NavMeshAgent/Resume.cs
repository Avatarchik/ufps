﻿using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityNavMeshAgent
{
    [TaskCategory("Basic/NavMeshAgent")]
    [TaskDescription("Resumes the movement along the current path after a pause. Returns Success.")]
    public class Resume : Action
    {
        [Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
        public SharedGameObject targetGameObject;

        // cache the navmeshagent component
        private UnityEngine.AI.NavMeshAgent navMeshAgent;
        private GameObject prevGameObject;

        public override void OnStart()
        {
            var currentGameObject = GetDefaultGameObject(targetGameObject.Value);
            if (currentGameObject != prevGameObject) {
                navMeshAgent = currentGameObject.GetComponent<UnityEngine.AI.NavMeshAgent>();
                prevGameObject = currentGameObject;
            }
        }

        public override TaskStatus OnUpdate()
        {
            if (navMeshAgent == null) {
                Debug.LogWarning("NavMeshAgent is null");
                return TaskStatus.Failure;
            }

            navMeshAgent.Resume();

            return TaskStatus.Success;
        }

        public override void OnReset()
        {
            targetGameObject = null;
        }
    }
}