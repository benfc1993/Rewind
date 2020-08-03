using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shell : SmartBullet
{
    public int Pellets;
    public Transform Pellet;
    // Start is called before the first frame update
    protected override void Start()
    {
        for(int i = 0; i < Pellets; i++)
        {
            Quaternion rotation = new Quaternion();
            rotation.eulerAngles = transform.forward;
            Transform new_Pellet = Instantiate(Pellet, transform.position, Quaternion.identity);
            new_Pellet.rotation = GunEnd.rotation * Quaternion.AngleAxis(30 - ((60 / Pellets) * i), Vector3.up);
            new_Pellet.GetComponent<SmartBullet>()._shell = transform;
        }
        base.Start();
    }
}
