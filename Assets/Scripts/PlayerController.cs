using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Antisystems.BigCrunch
{
    [RequireComponent(typeof(Rigidbody))]
    public class PlayerController : MonoBehaviour
    {
        public int HitPoints = 4;
        public float FrontBoost = 1.0f;
        public float SideBoost = 1.0f;

        public ParticleSystem FrontThrusterParticle;
        

        public ParticleSystem LeftThrusterParticle;
        public ParticleSystem RightThrusterParticle;

        public GameObject Mesh;
        public ParticleSystem DestructParticels;

        public AudioSource FrontThrusterSound;
        public AudioSource SideThrusterSound;
        public AudioSource HitSound;
        public AudioSource DestructSound;
        public AudioSource WarpSound;

        public GameObject Canvas;

        private Rigidbody m_rigidbody;

        private ParticleSystem.EmissionModule m_emission;
        private ParticleSystem.EmissionModule m_leftEmission;
        private ParticleSystem.EmissionModule m_rightEmission;

        private bool m_isWormhole = false;
        private bool m_isGameOver = false;
        public bool IsWin = false;
        
        public void GameOver()
        {
            if (!IsWin)
            {
                Canvas.SetActive(true);
                m_isGameOver = true;
                Debug.Log("GameOver");
                DestructParticels.Play();
                DestructSound.Play();
                Mesh.SetActive(false);
            }
        }
        void Awake()
        {
            m_rigidbody = GetComponent<Rigidbody>();
            m_emission = FrontThrusterParticle.emission;
            m_leftEmission = LeftThrusterParticle.emission;
            m_rightEmission = RightThrusterParticle.emission;
            m_isWormhole = false;
            DestructParticels.Stop();
        }

        
        void Update()
        {
            if (!m_isGameOver)
            {
                m_emission.rateOverTime = 0.0f;
                m_leftEmission.rateOverTime = 0;
                m_rightEmission.rateOverTime = 0;
                FrontThrusterSound.volume = 0;
                SideThrusterSound.volume = 0;
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
                    SideThrusterSound.volume = 1;
                }
                if (Input.GetButton("RightBuster"))
                {
                    m_rigidbody.AddTorque(transform.up * SideBoost, ForceMode.Force);
                    m_rightEmission.rateOverTime = 50;
                    SideThrusterSound.volume = 1;
                }
                if (Input.GetButtonDown("Warp") && m_isWormhole)
                {
                    WarpController warpController =
                        GameObject.FindGameObjectWithTag("WarpController").GetComponent<WarpController>();
                    warpController.ChooseWarp();
                    Debug.Log("Warp");
                    WarpSound.Play();
                }
            }
        }

        private void OnCollisionEnter(Collision collision)
        {
            HitSound.Play();
            HitPoints--;
            if (HitPoints == 0)
                GameOver();
            Debug.Log("Hit!!!");
        }
        private void OnTriggerEnter(Collider other)
        {
            if(other.gameObject.GetComponent<WormholeView>() != null)
                m_isWormhole = true;
        }
        private void OnTriggerExit(Collider other)
        {
            if (other.gameObject.GetComponent<WormholeView>() != null)
                m_isWormhole = false;
        }
    }
}
