using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Antisystems.BigCrunch
{
    [RequireComponent(typeof(Rigidbody))]
    public class PlayerController : MonoBehaviour
    {

        public float FrontBoost = 1.0f;
        public float SideBoost = 1.0f;

        public ParticleSystem FrontThrusterParticle;
        public AudioSource FrontThrusterSound;

        public ParticleSystem LeftThrusterParticle;
        public ParticleSystem RightThrusterParticle;

        private Rigidbody m_rigidbody;

        private ParticleSystem.EmissionModule m_emission;
        private ParticleSystem.EmissionModule m_leftEmission;
        private ParticleSystem.EmissionModule m_rightEmission;
        void Awake()
        {
            m_rigidbody = GetComponent<Rigidbody>();
            m_emission = FrontThrusterParticle.emission;
            m_leftEmission = LeftThrusterParticle.emission;
            m_rightEmission = RightThrusterParticle.emission;
        }

        
        void Update()
        {
            m_emission.rateOverTime = 0.0f;
            m_leftEmission.rateOverTime = 0;
            m_rightEmission.rateOverTime = 0;
            FrontThrusterSound.volume = 0;
            if (Input.GetButton("FrontBuster"))
            {
                m_rigidbody.AddForce(transform.forward * FrontBoost, ForceMode.Force);
                m_emission.rateOverTime = 50.0f;
                FrontThrusterSound.volume = 1;
            }
            if (Input.GetButton("LeftBuster"))
            {
                m_rigidbody.AddTorque(-transform.up * SideBoost, ForceMode.Force);
                m_leftEmission.rateOverTime = 50;
            }
            if (Input.GetButton("RightBuster"))
            {
                m_rigidbody.AddTorque(transform.up * SideBoost, ForceMode.Force);
                m_rightEmission.rateOverTime = 50;
            }
        }

        private void OnCollisionEnter(Collision collision)
        {
            Debug.Log("Hit!!!");
        }
    }
}
