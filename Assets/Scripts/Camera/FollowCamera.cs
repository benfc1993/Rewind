using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    public Transform Player;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if(Player != null)
        {
            transform.position = new Vector3(Player.position.x, transform.position.y, Player.position.z);        
        }
    }
}
