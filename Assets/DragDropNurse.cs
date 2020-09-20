using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragDropNurse : MonoBehaviour
{
    public bool isDraggable = true;
    public bool isDragged = false;
    void Update(){
        if (isDragged)
            transform.position = (Vector2) Camera.main.ScreenToWorldPoint(Input.mousePosition);
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
