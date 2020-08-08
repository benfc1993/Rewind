using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Utils.UtilsClass;

public class PlayerLookAt : MonoBehaviour
{
    Camera viewCamera;
    public Transform crosshairs;

    public Vector3 point;
    // Start is called before the first frame update
    private void Start()
    {
        viewCamera = Camera.main;
        Cursor.visible = false;
    }

    // Update is called once per frame
    private void Update()
    {
        Ray ray = viewCamera.ScreenPointToRay(Input.mousePosition);
        Plane groundPlane = new Plane(Vector3.up, Vector3.zero);
        float rayDistance;
        if (groundPlane.Raycast(ray, out rayDistance))
        {
            point = ray.GetPoint(rayDistance);
            Debug.DrawLine(ray.origin, point, Color.cyan);

            transform.LookAt(new Vector3(point.x, transform.position.y, point.z));
            crosshairs.position = new Vector3(point.x, 5, point.z);
        }
    }
}
