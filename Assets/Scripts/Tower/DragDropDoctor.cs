using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragDropDoctor : MonoBehaviour{
    public bool isDraggable = true;
    public bool isDragged = false;

    private double pos_x = 13.26;       // will be updated constantly
    private double pos_y = -2.07;        // will be updated constantly

    private double original_pos_x = 13.26;      // use this to snapback to original x position
    private double original_pos_y = -2.07;       // use this to snapback to original y position

    private bool isSnappedToPlatform = false;
    // list of platforms to be used
    public List<GameObject> platforms;

    private bool check(float x, float y) {
        if ( (this.pos_x <= x+1 && this.pos_x >= x-1) && (this.pos_y <= y+1 && this.pos_y >= y-1) ){
            return true;
        }

        return false;
    }
    void Update(){
        GameObject currentPlatform = platforms[0];
        bool isCurrentPlatformAssigned = false;

        if (isDragged){
            // check if there's a platform that satisfies the condition that it has the same position vector
            // as our tower.
            foreach(GameObject platform in platforms)
            {
                if(check(platform.transform.position.x, platform.transform.position.y))
                {
                    currentPlatform = platform;
                    isCurrentPlatformAssigned = true;
                }
            }
            // if there's none, do this shit
            if(isCurrentPlatformAssigned == false)
            {
                transform.position = (Vector2) Camera.main.ScreenToWorldPoint(Input.mousePosition);
            }
            else // if there is, then snap that shit. lol
            {
                transform.position = currentPlatform.transform.position;

                if(!isSnappedToPlatform)
                {
                    isSnappedToPlatform = true;
                    // create a GameObject that does the shooting
                    GameObject towerShooterObject = new GameObject("Tower Shooter Object");
                    towerShooterObject.AddComponent<TowerShooting>();
                    TowerShooting towerShooter = towerShooterObject.GetComponent<TowerShooting>();
                    Debug.Log("Game Object name: " + gameObject.name);
                    towerShooter.parentName = gameObject.name;
                    towerShooter.SetColliderRadius(7.4f);
                    towerShooter.SetPosition(gameObject.transform.position);
                }
                
            }

            this.pos_x = transform.position.x;
            this.pos_y = transform.position.y; 
        }
    }
    private void OnMouseOver(){
        if (isDraggable && Input.GetMouseButtonDown(0)) {
            isDragged = true;
        }
    }
    private void OnMouseUp(){
        isDragged = false;
    }
}
