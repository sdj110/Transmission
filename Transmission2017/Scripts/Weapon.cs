using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets._2D;



// TODO Get raycast to fire based on rotation of gun, not mouse
// Maybe just figure out how to fire raycast straight,
// then rotate it when aim buttons modify the aim

[RequireComponent(typeof(PlatformerCharacter2D))]
public class Weapon : MonoBehaviour
{
    private PlatformerCharacter2D m_Player;
    private Platformer2DUserControl m_PlayerController;
    public float fireRate = 5f;
    public float Damage = 10f;
    public LayerMask whatToHit;

    private float timeToFire = 0f;
    Transform firePoint;


    // Use this for initialization
    void Awake ()
    {
        m_Player = this.transform.parent.GetComponent<PlatformerCharacter2D>();
        m_PlayerController = this.transform.parent.GetComponent<Platformer2DUserControl>();
        firePoint = transform.Find("FirePoint");
        if (firePoint == null)
        {
            Debug.LogError("No FirePoint found");
        }
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (fireRate == 0)
        {
            if (Input.GetButtonDown("Fire1"))
            {
                Shoot();
            }
        }
        else
        {
            if (Input.GetButton("Fire1") && Time.time > timeToFire)
            {
                timeToFire = Time.time + 1 / fireRate;
                Shoot();
            }
        }

	}


    void Shoot()
    {
        // Check direction player is facing
        if (m_Player.FacingRight())
        {
            // Shoot facing right
            createShootRaycast(transform.right, m_PlayerController.GetRotation(), m_PlayerController.GetTransX(), m_PlayerController.GetTransY());
        }
        else
        {
            // Shoot facing left
            createShootRaycast(-transform.right, -m_PlayerController.GetRotation(), m_PlayerController.GetTransX(), m_PlayerController.GetTransY());
        }
    }

    void createShootRaycast(Vector3 dir, float zRot, float transX, float transY)
    {
        // Calcualte location of firepoint and create raycast to check collision
        Vector2 firePointPosition = new Vector2(firePoint.position.x + transX, firePoint.position.y + transY);

        // Rotate direction around z axis
        dir = Quaternion.Euler(0, 0, zRot) * dir;

        // Create raycast depending on direction player is facing
        RaycastHit2D hit = Physics2D.Raycast(firePointPosition, dir, 100, whatToHit);
        Debug.DrawLine(firePointPosition, dir * 100, Color.cyan);

        if (hit.collider != null)
        {
            Debug.DrawLine(firePointPosition, hit.point, Color.red);
            Debug.Log("Hit " + hit.collider.name + " DMG: " + Damage);
        }

    }

}
