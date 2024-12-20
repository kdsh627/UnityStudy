using System;
using System.Collections;
using UnityEngine;

public class BackStep : Skill
{
    public override void Execute(Rigidbody rb = null)
    {
        bool ExcutePossible = IsMinCoolTime();

        if(ExcutePossible)
        {
            InitCoolTime();
            StartCoroutine(ExecuteRoutine(rb));
            ReduceCoolTime();
        }
    }

    protected override IEnumerator ExecuteRoutine(Rigidbody rb)
    {
        executeState = State.BackStep;

        Vector3 cameraBack = -Camera.main.transform.forward;
        cameraBack.y = 0;

        cameraBack.Normalize();

        rb.linearVelocity = Vector3.zero;
        rb.AddForce(speed * cameraBack, ForceMode.Impulse);
        yield return new WaitForSeconds(0.3f);

        rb.linearVelocity = Vector3.zero;
        executeState = State.Idle;
    }
}
