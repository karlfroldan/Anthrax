using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeMonkey.Utils;

public class Bullet : MonoBehaviour
{
    // Start is called before the first frame update
    private Rigidbody2D m_RigidBody;
    private Vector3 shootDir;
    void Start()
    {
        m_RigidBody = gameObject.GetComponent<Rigidbody2D>();
    }

    public void Setup(Vector3 shootDir)
    {
        this.shootDir = shootDir;
        transform.eulerAngles = new Vector3(0, 0, UtilsClass.GetAngleFromVectorFloat(shootDir));
    }

    void Update()
    {
        float moveSpeed = 29.432f;
        transform.position += shootDir * moveSpeed * Time.deltaTime;
    }
}
