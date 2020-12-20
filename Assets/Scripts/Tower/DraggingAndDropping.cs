using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class DraggingAndDropping : MonoBehaviour{
    public bool isDraggable = true;
    public int price;
    public float colliderRadius;
    private PauseMenu pauseMenu;

    public int value;

    private double original_pos_x;      // use this to snapback to original x position
    private double original_pos_y;       // use this to snapback to original y position

    private bool isSticked = false;
    private bool isSnapped = false;
    private bool isMovable = false;
    // list of platforms to be used
    public GameObject platforms;
    private List<GameObject> platformList;
    private Coins coins;

    void Start() {
        GameObject coinObject = GameObject.Find("CoinText");
        pauseMenu = GameObject.Find("PCanvas").GetComponent<PauseMenu>();
        coins = coinObject.GetComponent<Coins>();

        Vector3 positions = transform.position;
        original_pos_x = positions.x;

        original_pos_y = positions.y;

        // get all the platforms. This should be the child
        List<Transform> childs = platforms.transform.Cast<Transform>().ToList();
        platformList = new List<GameObject>();
        // then add this to the platformList
        foreach(Transform child in childs) {
            platformList.Add(child.gameObject);
        }
    }

    private bool IsNearObject(Vector2 objPos) {
        return (transform.position.x <= objPos.x + 1.5f && transform.position.x  >= objPos.x - 1.5f) && 
               (transform.position.y <= objPos.y + 1.5f && transform.position.y  >= objPos.y - 1.5f);
    }

    void Update(){
        /* Handle Screen Touch */
        if (Input.touchCount > 0) { /* If we have more than 0 screen touches */
            Touch touch = Input.GetTouch(0);
            Vector3 sToPoint = (Vector2) Camera.main.ScreenToWorldPoint(touch.position);
            bool isNotPaused = !pauseMenu.isPaused;

            /* If we're touching near this object and it can be dragged */
            if (IsNearObject(sToPoint) && isDraggable && isNotPaused) {
                /* We begin touching the object */
                if (touch.phase == TouchPhase.Began) {
                    isMovable = true;
                }

                /* And we're dragging the object */
                if (touch.phase == TouchPhase.Moved && isMovable) {
                    transform.position = new Vector3(sToPoint.x, sToPoint.y, 0.0f);
                }

                /* When we lift the finger */
                if (touch.phase == TouchPhase.Ended) {
                    GameObject nearestPlatform = GetNearestPlatform();
                    /* Snap it to the platform if we ended near it */
                    /* And if we have enough number of coins */
                    if (nearestPlatform != null && coins.HasEnoughCoins(value)) { 
                        coins.DecreaseCoins(value);
                        transform.position = nearestPlatform.transform.position;
                        isSticked = true;
                        isDraggable = false;
                        CreateAnother();
                        Platform platform = nearestPlatform.GetComponent<Platform>();
                        platform.AttachToTower(gameObject);
                    } else { /* Or else, snap it back to its original position and don't let it near a platform */
                        transform.position = new Vector3((float) original_pos_x, (float) original_pos_y, 0.0f);
                    }
                    isMovable = false;
                }
            } else {
                /* If we accidentally dropped the tower */
                if (isMovable && isDraggable && GetNearestPlatform() == null) {
                    isMovable = false;
                    transform.position = new Vector3((float) original_pos_x, (float) original_pos_y, 0.0f);
                }
            }
        }
    }

    private void CreateAnother() {
        Vector3 newPosition = new Vector3((float)original_pos_x, (float)original_pos_y, 0f);
        GameObject newTower = (GameObject)Instantiate(gameObject, newPosition, transform.rotation);
        // then set some values
        DraggingAndDropping newDragDrop = newTower.GetComponent<DraggingAndDropping>();
        newDragDrop.isDraggable = true;
        newDragDrop.isSnapped = false;
        newDragDrop.isMovable = false;
    }

    public void SetSnapped(bool set) {
        isSnapped = set;
    }

    private GameObject GetNearestPlatform() {
        GameObject nearestPlatform = null;

        foreach (GameObject pform in platformList) {
            Vector2 pFormPos = (Vector2) pform.transform.position;
            if (IsNearObject(pFormPos)) {
                nearestPlatform = pform;
                break;
            }
        }
        return nearestPlatform;
    }
}
