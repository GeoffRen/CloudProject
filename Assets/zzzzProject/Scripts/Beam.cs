using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Beam : MonoBehaviour
{

    public Transform origin;
    public LineRenderer laserLineRenderer;
    private float laserMaxLength = 100f;
    public float LaserMaxLength
    {
        get { return laserMaxLength; }
        set { laserMaxLength = value; }
    }

    private bool isShooting;
    public bool IsShooting
    {
        get { return isShooting; }
        set { isShooting = value; }
    }

    protected void Update()
    {
        if(isShooting) 
        {
            ShootLaserFromTargetPosition(origin.position, origin.TransformDirection(Vector3.forward));
            laserLineRenderer.enabled = true;
        }
        else 
        {
            laserLineRenderer.enabled = false;
        }
    }
    
    private void ShootLaserFromTargetPosition(Vector3 targetPosition, Vector3 direction)
    {
        Ray ray = new Ray(targetPosition, direction);
        RaycastHit raycastHit;
        Vector3 endPosition = targetPosition + laserMaxLength * direction;
 
        if( Physics.Raycast(ray, out raycastHit, laserMaxLength)) 
        {
            endPosition = raycastHit.point;
            GameObject hitObj = raycastHit.collider.gameObject;
            if (hitObj.tag.Equals("Player"))
            {
                var healthManager = hitObj.GetComponent<HealthManager>();
                Debug.Log(string.Format("TRIGGERSTAY: {0} Health at: {1}", hitObj.name, healthManager.Health));
                healthManager.takeDamage(.01f);
            }
        }
 
        laserLineRenderer.SetPosition(0, targetPosition);
        laserLineRenderer.SetPosition(1, endPosition);
    }
}
