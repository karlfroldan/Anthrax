using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformSuperclass : MonoBehaviour{
    public int x_position;
    public int y_position;

    public void set_xpos(int x_position) {
        this.x_position = x_position;
    }
    
    public void set_ypos() {
        this.y_position = y_position;
    }

    public int get_xpos() {
        return this.x_position;
    }

    public int get_ypos() {
        return this.y_position;
    }

    public int plat_area(){
        return 1;
    }

}
