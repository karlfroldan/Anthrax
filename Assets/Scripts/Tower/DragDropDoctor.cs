using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragDropDoctor : MonoBehaviour{
    public bool isDraggable = true;
    public bool isDragged = false;

    private double pos_x = 13.26;       // will be updated constantly
    private double pos_y = -2.07;        // will be updated constantly

    private const double original_pos_x = 13.26;      // use this to snapback to original x position
    private const double original_pos_y = -2.07;       // use this to snapback to original y position

    // KARL THIS IS YOUR CODE
    //private bool isSnappedToPlatform = false;
    //private bool isCurrentPlatformAssigned = false;
    // END OF YOUR CODE

    private bool isSticked = false;
    private bool isReleased;
    // list of platforms to be used
    public List<GameObject> platforms;

    private bool check(float x, float y) {
        if ( (this.pos_x <= x+1.5 && this.pos_x >= x-1.5) && (this.pos_y <= y+1.5 && this.pos_y >= y-1.5) ){
            return true;
        }

        return false;
    }
    void Update(){
        GameObject currentPlatform = platforms[0];

        if (this.isDragged){
            transform.position = (Vector2) Camera.main.ScreenToWorldPoint(Input.mousePosition);

        }    
        else {
            foreach(GameObject platform in platforms)
            {
                if(check(platform.transform.position.x, platform.transform.position.y))
                {
                    transform.position = platform.transform.position;
                    this.isSticked = true;
                    this.isDraggable = false;

                    // KARL THIS IS YOUR CODE

                    currentPlatform = platform;
                    //isCurrentPlatformAssigned = true;

                    // END OF YOUR CODE
                }
            }
        }

        if(!this.isSticked && !this.isDragged) {
            transform.position = new Vector2((float)original_pos_x, (float)original_pos_y);    
        }

        this.pos_x = transform.position.x;
        this.pos_y = transform.position.y;

        // KARL THIS IS YOUR CODE

        if(this.isSticked)
        {
            // create a GameObject that does the shooting
            GameObject towerShooterObject = new GameObject("Tower Shooter Object");
            towerShooterObject.AddComponent<TowerShooting>();
            TowerShooting towerShooter = towerShooterObject.GetComponent<TowerShooting>();
            Debug.Log("Game Object name: " + gameObject.name);
            towerShooter.parentName = gameObject.name;
            towerShooter.SetColliderRadius(7.4f);
            towerShooter.SetPosition(gameObject.transform.position);
        }
        
        // END OF YOUR CODE

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
