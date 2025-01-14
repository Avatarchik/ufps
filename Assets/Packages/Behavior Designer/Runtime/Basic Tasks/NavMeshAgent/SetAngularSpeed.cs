﻿using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityNavMeshAgent
{
    [TaskCategory("Basic/NavMeshAgent")]
    [TaskDescription("Sets the maximum turning speed in (deg/s) while following a path. Returns Success.")]
    public class SetAngularSpeed : Action
    {
        [Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
        public SharedGameObject targetGameObject;
        [Tooltip("The NavMeshAgent angular speed")]
        public SharedFloat angularSpeed;

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

            navMeshAgent.angularSpeed = angularSpeed.Value;

            return TaskStatus.Success;
        }

        public override void OnReset()
        {
            targetGameObject = null;
            angularSpeed = 0;
        }
    }
}