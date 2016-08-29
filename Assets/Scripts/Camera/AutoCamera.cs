using UnityEngine;
using System.Collections;
using System;

[ExecuteInEditMode]
public class AutoCamera : PivotBasedCameraRig
{
    [SerializeField]
    private float m_MoveSpeed = 3; // How fast the target will move to keep up the target's position

    protected override void FollowTarget(float deltaTime)
    {
        // If no target or not time passed then do nothing
        if((m_Target == null) || !(deltaTime > 0))
        {
            return;
        }

        // camera position moves towards target position: the position of (deltaTime * m_MoveSpeed) % towards m_Target+m_Offset
        Vector3 targetPosx = new Vector3(m_Target.position.x, transform.position.y, transform.position.z); 
		transform.position = Vector3.Lerp(transform.position, targetPosx + m_Offset, deltaTime * m_MoveSpeed);

    }
}
