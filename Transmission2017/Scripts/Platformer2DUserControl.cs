using System;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

namespace UnityStandardAssets._2D
{
    [RequireComponent(typeof (PlatformerCharacter2D))]
    public class Platformer2DUserControl : MonoBehaviour
    {
        private PlatformerCharacter2D m_Character;
        private bool m_Jump;
        private bool m_AimUp;
        private bool m_AimDown;
        private bool m_AimStrong;
        private bool m_Firing;

        private float m_RotateAim;
        private float m_TransX;
        private float m_TransY;


        private void Awake()
        {
            m_Character = GetComponent<PlatformerCharacter2D>();
            m_AimUp = false;
            m_AimDown = false;
            m_AimStrong = false;
            m_Firing = false;
            m_TransX = 0f;
            m_TransY = 0f;
        }



        private void Update()
        {
            if (!m_Jump)
            {
                // Read the jump input in Update so button presses aren't missed.
                m_Jump = CrossPlatformInputManager.GetButtonDown("Jump");
            }

             // Check if W or S is being held
            m_AimUp = Input.GetKey(KeyCode.W);
            m_AimDown = Input.GetKey(KeyCode.S);

            if (m_AimUp || m_AimDown)
            {
                m_AimStrong = Input.GetKey(KeyCode.LeftShift);
            }
            else
            {
                m_AimStrong = false;
            }

            // Set firing animation
            m_Firing = Input.GetKey(KeyCode.Mouse0);
            m_Character.SetFiringAnimation(m_Firing);


            // Set Z rotation
            SetRotation();

            m_Character.Aim(m_AimUp, m_AimDown, m_AimStrong);
        }



        private void FixedUpdate()
        {
            // Read the inputs.
            bool crouch = Input.GetKey(KeyCode.LeftControl);
            float h = CrossPlatformInputManager.GetAxis("Horizontal");
            // Pass all parameters to the character control script.
            m_Character.Move(h, crouch, m_Jump);
            m_Jump = false;
        }


        private void SetRotation()
        {
            // If player is aiming up
            if (m_AimUp)
            {
                // If player is aiminng up and strong aim is active
                if (m_AimUp && m_AimStrong)
                {
                    if (m_Character.FacingRight())
                    {
                        m_RotateAim = 90.0f;
                        m_TransX = -0.70f;
                        m_TransY = 0.5f;
                    }
                    else
                    {
                        m_RotateAim = 90.0f;
                        m_TransX = 0.6f;
                        m_TransY = 0.5f;
                    }
                }
                else
                {
                    m_RotateAim = 35.0f;
                    m_TransX = 0f;
                    m_TransY = 0.5f;
                }
            }

            else if(m_AimDown)
            {
                // If player is aiminng up and strong aim is active
                if (m_AimDown && m_AimStrong)
                {
                    if (m_Character.FacingRight())
                    {
                        m_RotateAim = -90.0f;
                        m_TransX = -0.75f;
                        m_TransY = -0.5f;
                    }
                    else
                    {
                        m_RotateAim = -90.0f;
                        m_TransX = 0.5f;
                        m_TransY = -0.5f;
                    }
                    
                }
                else
                {
                    if (m_Character.FacingRight())
                    {
                        m_RotateAim = -25.0f;
                        m_TransX = 0f;
                        m_TransY = -0.5f;
                    }
                    else
                    {
                        m_RotateAim = -25.0f;
                        m_TransX = 0f;
                        m_TransY = -0.5f;
                    }
                }
            }
            else
            {
                m_RotateAim = 0.0f;
                m_TransX = 0f;
                m_TransY = 0f;
            }
        }


        public float GetRotation() { return m_RotateAim; }
        public float GetTransX() { return m_TransX; }
        public float GetTransY() { return m_TransY; }
    }
}
