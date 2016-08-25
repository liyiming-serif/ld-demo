using System;
using UnityEngine;

public abstract class PivotBasedCameraRig : AbstractTargetFollower {

    // CameraRig
    //  Pivot
    //      Camera

    protected Transform m_Camera;
    protected Transform m_Pivot;
    protected Vector3 m_LastTargetPosition; //(ZK: TODO What is this used for? Why declare it over here? When is it assigned?)
	
	//Find the gameObjects in the hierarchy
    protected virtual void Awake()
    {
        m_Camera = GetComponentInChildren<Camera>().transform;
        m_Pivot = m_Camera.parent;
    }

}
