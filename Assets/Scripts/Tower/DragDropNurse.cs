using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragDropNurse : MonoBehaviour{
    public bool isDraggable = true;
    public bool isDragged = false;

    private double pos_x = 12.00;       // will be updated constantly
    private double pos_y = -2.66;       // will be updated constantly

    private double original_pos_x = 12.00;      // use this to snapback to original x position
    private double original_pos_y = -2.66;       // use this to snapback to original y position

    private bool check(float x, float y) {
        if ( (this.pos_x <= x+1 && this.pos_x >= x-1) && (this.pos_y <= y+1 && this.pos_y >= y-1) ){
            return true;
        }

        return false;
    }
    void Update(){
        if (isDragged){
            if (this.check(GameObject.Find("platform0").transform.position.x, GameObject.Find("platform0").transform.position.y)){
                transform.position = GameObject.Find("platform0").transform.position;
            }
            else if (this.check(GameObject.Find("platform1").transform.position.x, GameObject.Find("platform1").transform.position.y)){
                transform.position = GameObject.Find("platform1").transform.position;
            }
            else if (this.check(GameObject.Find("platform2").transform.position.x, GameObject.Find("platform2").transform.position.y)){
                transform.position = GameObject.Find("platform2").transform.position;
            }
            else if (this.check(GameObject.Find("platform3").transform.position.x, GameObject.Find("platform3").transform.position.y)){
                transform.position = GameObject.Find("platform3").transform.position;
            }
            else if (this.check(GameObject.Find("platform4").transform.position.x, GameObject.Find("platform4").transform.position.y)){
                transform.position = GameObject.Find("platform4").transform.position;
            }
            else if (this.check(GameObject.Find("platform5").transform.position.x, GameObject.Find("platform5").transform.position.y)){
                transform.position = GameObject.Find("platform5").transform.position;
            }
            else if (this.check(GameObject.Find("platform6").transform.position.x, GameObject.Find("platform6").transform.position.y)){
                transform.position = GameObject.Find("platform6").transform.position;
            }
            else {
                transform.position = (Vector2) Camera.main.ScreenToWorldPoint(Input.mousePosition);
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
