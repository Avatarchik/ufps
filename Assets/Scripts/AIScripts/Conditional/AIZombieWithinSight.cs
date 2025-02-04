﻿using System.Collections.Generic;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using UnityEngine;

public class AIZombieWithinSight : HumanizedReactionConditional
{
    public float ConeAngle = 90;
    public float ConeDistance = 5;

    public float ProbabilityToAvoidTrigger = .5f;

    public string ZombieTag = "Enemy";

    public SharedTransform Target;

    private GameObject[] _targets;
    private List<GameObject> _lastTargetsInSight = new List<GameObject>();

    public override void OnAwake()
    {
        base.OnAwake();

        _targets = GameObject.FindGameObjectsWithTag(ZombieTag); // todo create a pool or make sure new zombies will be registered in this array
    }

    public override TaskStatus OnUpdate()
    {
        var targetsInSight = TargetsInSight();

        if (waiting)
            return TaskStatus.Running;

        //try track the current target
        if (null != Target.Value &&
            targetsInSight.Contains(Target.Value.gameObject))
        {
            _lastTargetsInSight = targetsInSight;
            return TaskStatus.Success;
        }

        if (null == Target.Value || !targetsInSight.Contains(Target.Value.gameObject))
        {
            foreach (var targetInSight in targetsInSight)
            {
                if (_lastTargetsInSight.Contains(targetInSight)) continue;

                //we have a new target, and we do the proba test
                if (Random.Range(0, 1f) >= ProbabilityToAvoidTrigger)
                {
                    //proba test succeed : follow
                    StartCoroutine(Countdown());
                    Target.Value = targetInSight.transform;
                    _lastTargetsInSight = targetsInSight;
                    return TaskStatus.Failure;
                }
            }
        }

        _lastTargetsInSight = targetsInSight;
        return TaskStatus.Failure;
    }

    List<GameObject> TargetsInSight()
    {
        var currentTargetsInSight = new List<GameObject>();

        foreach (var target in _targets)
        {
            if (target == null || target.transform == transform) continue;
            if (IsTargetInSight(target))
                currentTargetsInSight.Add(target);
        }
        return currentTargetsInSight;
    }

    bool IsTargetInSight(GameObject target)
    {
        var angle = Vector3.Angle(transform.forward, target.transform.position - transform.position);

        var dst = Vector3.Distance(transform.position, target.transform.position);
        return (dst < ConeDistance && angle < ConeAngle);
    }
}
