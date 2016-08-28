using UnityEngine;
using System.Collections;

public class CamShakeSimple : MonoBehaviour
{

    Vector3 originalCameraPosition;

    float m_shakeAmt = 0;

    public Camera mainCamera;

    void OnCollisionEnter2D(Collision2D coll)
    {

        m_shakeAmt = coll.relativeVelocity.magnitude * .0025f;
        originalCameraPosition = mainCamera.transform.position;
        InvokeRepeating("CameraShake", 0, .01f);
        Invoke("StopShaking", 0.3f);

    }

    void CameraShake()
    {
        if (m_shakeAmt > 0)
        {
            float shakeAmtX = Random.value * this.m_shakeAmt * 2 - this.m_shakeAmt;
            float shakeAmtY = Random.value * this.m_shakeAmt * 2 - this.m_shakeAmt;
            Vector3 pp = mainCamera.transform.position;
            pp.y += shakeAmtY; // can also add to x and/or z
            pp.x += shakeAmtX;
            mainCamera.transform.position = pp;
        }
    }

    void StopShaking()
    {
        CancelInvoke("CameraShake");
        mainCamera.transform.position = originalCameraPosition;
        m_shakeAmt = 0;
    }

    public float shakeAmt
    {
        set
        {
            m_shakeAmt = value;
        }
        get
        {
            return m_shakeAmt;
        }
    }

}