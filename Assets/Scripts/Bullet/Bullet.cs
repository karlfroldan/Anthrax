using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    // Start is called before the first frame update
    private Rigidbody2D m_RigidBody;
    void Start()
    {

        m_RigidBody = gameObject.GetComponent<Rigidbody2D>();
        Debug.Log("is m_RigidBody simulated" + m_RigidBody.simulated);
    }

    // Update is called once per frame
    public void Init(Vector3 direction)
    {
        Debug.Log("We are now shooting");
        m_RigidBody.AddForce(direction);
    }
}
