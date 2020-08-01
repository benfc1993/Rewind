using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    [SerializeField] private Transform bullet;

    // Start is called before the first frame update
    private void Awake()
    {
        GetComponent<PlayerController>().OnShoot += PlayerShootProjectiles_OnShoot;
    }
    private void PlayerShootProjectiles_OnShoot(object sender, PlayerController.OnShootEventArgs e)
    {
        Debug.Log("Shoot!");
        Transform bulletTransform = Instantiate(bullet, e.gunEndPointPosition, Quaternion.identity);

        Vector3 shootDir = e.shootPosition - e.gunEndPointPosition;
        Debug.Log(e.shootPosition);
        Debug.Log(e.gunEndPointPosition);
        Debug.Log(shootDir);
        bulletTransform.GetComponent<Bullet>().Setup(shootDir);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
