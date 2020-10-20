using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class DraggingAndDropping : MonoBehaviour{
    public bool isDraggable = true;
    public bool isDragged = false;
    public float colliderRadius;

    private double pos_x;       // will be updated constantly
    private double pos_y;        // will be updated constantly

    private double original_pos_x;      // use this to snapback to original x position
    private double original_pos_y;       // use this to snapback to original y position

    private bool isSticked = false;
    private bool isSnapped = false;
    private bool isReleased;
    // list of platforms to be used
    public GameObject platforms;
    private List<GameObject> platformList;

    void Start()
    {
        Vector3 positions = transform.position;
        original_pos_x = positions.x;
        pos_x = positions.x;

        original_pos_y = positions.y;
        pos_y = positions.y;

        // get all the platforms. This should be the child
        List<Transform> childs = platforms.transform.Cast<Transform>().ToList();
        platformList = new List<GameObject>();
        // then add this to the platformList
        foreach(Transform child in childs)
        {
            Debug.Log("child name: " + child.gameObject.name);
            platformList.Add(child.gameObject);
        }
    }

    private bool check(float x, float y) {
        if ( (this.pos_x <= x+1.5 && this.pos_x >= x-1.5) && (this.pos_y <= y+1.5 && this.pos_y >= y-1.5) ){
            return true;
        }

        return false;
    }
    void Update(){

        if (this.isDragged){
            transform.position = (Vector2) Camera.main.ScreenToWorldPoint(Input.mousePosition);

        }    
        else {
            /*
             * For each platform, check if the platform's positions is the same as the tower.
             * If it is, then we make sure that it already is sticked and it is not draggable anymore.
             */
            foreach(GameObject pform in platformList)
            {
                if(check(pform.transform.position.x, pform.transform.position.y))
                {
                    transform.position = pform.transform.position;
                    isSticked = true;
                    isDraggable = false;
                }
            }
        }

        if(!this.isSticked && !this.isDragged) {
            transform.position = new Vector2((float)original_pos_x, (float)original_pos_y);    
        }

        this.pos_x = transform.position.x;
        this.pos_y = transform.position.y;

        // KARL THIS IS YOUR CODE
        // if it is sticked and hasn't snapped yet
        if(this.isSticked && !isSnapped)
        {
            // create a GameObject that does the shooting
            GameObject towerShooterObject = new GameObject("Tower Shooter Object");
            towerShooterObject.AddComponent<TowerShooting>();
            TowerShooting towerShooter = towerShooterObject.GetComponent<TowerShooting>();
            Debug.Log("Game Object name: " + gameObject.name);
            towerShooter.parentName = gameObject.name;
            towerShooter.SetColliderRadius(colliderRadius);
            towerShooter.SetPosition(gameObject.transform.position);
            // then make it snapped
            // to prevent making the object above repeatedly :P 
            isSnapped = true;
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
