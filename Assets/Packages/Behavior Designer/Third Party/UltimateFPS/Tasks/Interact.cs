﻿using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

namespace BehaviorDesigner.Runtime.Tasks.UFPS
{
    [TaskDescription("Interacts with the current object.")]
    [HelpURL("http://www.opsive.com/assets/BehaviorDesigner/documentation.php?id=99")]
    [TaskCategory("UFPS")]
    [TaskIcon("Assets/Behavior Designer/Third Party/UltimateFPS/Editor/Icon.png")]
    public class Interact : Action
    {
        public vp_PlayerEventHandler agent;

        public override void OnAwake()
        {
            if (agent == null) {
                agent = gameObject.GetComponent<vp_PlayerEventHandler>();
            }
        }

        public override TaskStatus OnUpdate()
        {
            if (agent == null) {
                return TaskStatus.Failure;
            }
            return agent.Interact.TryStart() ? TaskStatus.Success : TaskStatus.Failure;
        }

        public override void OnReset()
        {
            agent = null;
        }
    }
}