using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContinueMusic : MonoBehaviour
{
    void Awake (){
        DontDestroyOnLoad(transform.gameObject);
    }
}
