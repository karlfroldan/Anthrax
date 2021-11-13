using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{
    public GameObject attachedTower;
    // Start is called before the first frame update
    void Start() {
        attachedTower = null;
    }

    private void MakeShooter(GameObject tower) {
        DraggingAndDropping dad = tower.GetComponent<DraggingAndDropping>();
        GameObject towerShooterObject = new GameObject("Tower Shooter Object");
        towerShooterObject.transform.parent = tower.transform;
        /* Add the tower shooter component */
        towerShooterObject.AddComponent<TowerShooting>();
        TowerShooting towerShooter = towerShooterObject.GetComponent<TowerShooting>();
        towerShooter.parentName = tower.name;
        towerShooter.SetColliderRadius(dad.colliderRadius);
        towerShooter.SetPosition(transform.position);
        dad.SetSnapped(true);
    }

    public void AttachToTower(GameObject tower) {
        if (attachedTower == null) {
            attachedTower = tower;
            MakeShooter(tower);
        } else { /* There is current a tower occupying this */
            Destroy(attachedTower);
            attachedTower = null;
            
            AttachToTower(tower);
        }
    }

    // Update is called once per frame
    void Update(){
        
    }
}
