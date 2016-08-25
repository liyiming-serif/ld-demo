using UnityEngine;

public abstract class AbstractTargetFollower : MonoBehaviour
{
    public enum UpdatePrototype // The available methods of updating are: (ZK: Put in the front of a class)
    {
        FixedUpdate,    // for tracking rigidbodies (they don't move in Update)
        LateUpdate,     // for tracking objects that are moved in Update
        ManualUpdate    // user must call to update camera
    };

    [SerializeField]
    protected Transform m_Target; // the target object to follow
    [SerializeField]
    private bool m_AutoTargetPlayer = true; // the rig automatically targets the object tagged with "Player"
    [SerializeField]
    private UpdatePrototype m_updateType; // stores the selected update type

    protected Rigidbody targetRigidbody; // (ZK: This is missed. TODO What is this object used for? What happens if it's 2D.)

    //
    // protected virtual void Start()
    // if auto targeting is on, find the object tagged with "Player"
    // any class inheriting from this shouldcall base.Start() to perform this acction!s
    // 
    protected virtual void Start()
    {
        if (m_AutoTargetPlayer)
        {
            FindPlayerTarget();
        }
        if(m_Target == null)
        {
            return;
        }
        targetRigidbody = m_Target.GetComponent<Rigidbody>();
    }

    //
    // public void SetTarget(Transform newTrans)
    // can be overwritten
    // 
    public virtual void SetTarget(Transform newTrans)
    {
        m_Target = newTrans;
    }

    //
    // public void FindTarget()
    // find the Gameobject tagged with "Player"
    //
    public void FindPlayerTarget()
    {
        var targetObject = GameObject.FindGameObjectWithTag("Player"); // First assign it to an implicit var in case of null (which will break GetComponent<>
        if (targetObject)
        {
            SetTarget(targetObject.transform);
        }
    }

    // 
    // private void FixedUpdate()
    // if the target has a rigidbody, and isn't kinematic (moving in Update())
    // Called by Unity
    //
    protected void FixedUpdate()
    {
        if (m_AutoTargetPlayer && (m_Target == null || !m_Target.gameObject.activeSelf))
        {
            FindPlayerTarget();
        }
        if (m_updateType == UpdatePrototype.FixedUpdate)
        {
            FollowTarget(Time.deltaTime);
        }
    }

    //
    // private void LateUpdate()
    // Update this gameOjbect's position after the target has moved (have a rigidbody that set to kinematic)
    // Or does not have a rigidbody (import Physics)
    // Called by Unity
    //
    protected void LateUpdate()
    {
        if (m_AutoTargetPlayer && (m_Target == null || !m_Target.gameObject.activeSelf))
        {
            FindPlayerTarget();
        }
        if (m_updateType == UpdatePrototype.LateUpdate)
        {
            FollowTarget(Time.deltaTime);
        }
    }

    //
    // public void ManualUpdate()
    // Update the camera's position only whenever it is called
    //
    public void ManualUpdate()
    {
        if (m_AutoTargetPlayer && (m_Target == null || !m_Target.gameObject.activeSelf))
        {
            FindPlayerTarget();
        }
        if (m_updateType == UpdatePrototype.ManualUpdate)
        {
            FollowTarget(Time.deltaTime);
        }
    }

    //
    // public void MoveTowardsTarget()
    // Update this GameObject's (e.g. Camera) position.
    //
    protected abstract void FollowTarget(float deltaTime);

}
