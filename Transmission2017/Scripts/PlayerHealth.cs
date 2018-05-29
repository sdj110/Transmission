using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{

    private static int NUM_SLOTS = 3;
    [SerializeField]
    private Image[] m_HeartImage = new Image[NUM_SLOTS]; // List of slot images
    [SerializeField]
    private Transform m_StartPos;

    private int m_Health = 3;

    // Use this for initialization
    void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        
	}

    // Reduce health when hit
    public void LoseHealth()
    {
        if (m_Health >= 2)
        {
            m_Health--;
            m_HeartImage[m_Health].enabled = false;
        }
        else if (m_Health == 1)
        {
            // DIE
            m_Health--;
            m_HeartImage[m_Health].enabled = false;

            Die();
            Debug.Log("Die");
        }
        else
        {
            Debug.Log("You Dead Already");
        }

    }

    void Die()
    {
        m_Health = 3;

        for (int i = 0; i < m_Health; i++)
        {
            m_HeartImage[i].enabled = true;
        }

        // Reset position
        transform.position = m_StartPos.position;
    }
    

}
