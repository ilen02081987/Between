using Between.SpellsEffects.Projectile;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileParticleController : MonoBehaviour
{
    [SerializeField]
    private Transform ImpactParticles;

    void Start()
    {
        if (transform.parent != null)
        {
            Projectile parentProjectile = transform.parent.GetComponent<Projectile>();
            parentProjectile.OnLaunch += ControlDirection;
            parentProjectile.OnDestroyed += SpawnImpactParticles;
        }
    }
    private void ControlDirection(Vector3 direction)
    {
        //float angle = Mathf.Atan2(direction.x, direction.y) * Mathf.Rad2Deg;
        float angle = CalculateRotation(direction);
        transform.rotation = Quaternion.Euler(0, 0, angle);
        Debug.Log("Angle: " + angle);
    }

    private void SpawnImpactParticles(object sender, System.EventArgs e)
    {
        Instantiate(ImpactParticles.GetChild(0), transform.position, Quaternion.identity);
        Instantiate(ImpactParticles.GetChild(1), transform.position, Quaternion.identity);
    }
    //Vector3 launchDirection = Vector3.RotateTowards(Vector3.forward, direction, 360, 0.0f);

    //transform.Rotate(0, 0, angle);
    //Debug.Log("direction: " + direction);
    /*
    Debug.Log("launchDirection: " + launchDirection);
    Debug.Log("angleRad: " + angleRad);
    Debug.Log("angle: " + angle);
    */

    private float CalculateRotation(Vector3 direction)
    {
        if (direction.y >= 0)
        {
            if (direction.x >= 0)
            {
                //case 1: x,y - positive
                float anglePercentage = Mathf.InverseLerp(-1f, 1f, direction.y - direction.x);
                float rotation = 90f * anglePercentage;
                return rotation;
            } 
            else
            {
                //case 2: x is negative
                float positiveX = -direction.x;
                float anglePercentage = Mathf.InverseLerp(-1f, 1f, positiveX - direction.y);
                float rotation = (90f * anglePercentage) + 90f;
                return rotation;
            }
        }
        else
        {
            if (direction.x >= 0)
            {
                //case 3: y is negative
                float positiveY = -direction.y;
                float anglePercentage = Mathf.InverseLerp(-1f, 1f, positiveY - direction.x);
                float rotation = -90f * anglePercentage;
                return rotation;
            }
            else
            {
                //case 2: xx,y - negative
                float positiveX = -direction.x;
                float positiveY = -direction.y;
                float anglePercentage = Mathf.InverseLerp(-1f, 1f, positiveX - positiveY);
                float rotation = (-90f * anglePercentage) -90f;
                return rotation;
            }
        }
        
    }

}
