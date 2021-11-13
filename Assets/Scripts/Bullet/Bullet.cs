using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeMonkey.Utils;

public class Bullet : MonoBehaviour
{
    // Start is called before the first frame update
    private Rigidbody2D m_RigidBody;
    private Vector3 shootDir;
    private Vector3 targetPos;
    private TowerShooting shotBy;
    void Start()
    {
        m_RigidBody = gameObject.GetComponent<Rigidbody2D>();
    }

    public void Setup(Vector3 shootDir, Vector3 targetPos, TowerShooting shotBy)
    {
        this.shootDir = shootDir;
        this.targetPos = targetPos;
        this.shotBy = shotBy;
        transform.eulerAngles = new Vector3(0, 0, UtilsClass.GetAngleFromVectorFloat(shootDir));
    }

    void Update()
    {
        float moveSpeed = 29.432f;
        transform.position += shootDir * moveSpeed * Time.deltaTime;
        //Debug.Log("Transform.position" + transform.position + " | Enemy pos: " + targetPos);
        if((transform.position.x <= targetPos.x + 0.2f ||
            transform.position.x <= targetPos.x - 0.2f ) &&
           (transform.position.y <= targetPos.y + 0.2f ||
            transform.position.y <= targetPos.y - 0.2f ))
        {
            Destroy(gameObject);
            // send a signal that the bullet has reached its destination
            shotBy.DamageTarget();
        }
        
    }
}
