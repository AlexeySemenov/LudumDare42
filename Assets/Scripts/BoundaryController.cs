using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Antisystems.BigCrunch
{
    public class BoundaryController : MonoBehaviour
    {
        public float CrunchFactor = 0.1f;
        private Transform m_transform;

        private static bool IsPlayerInside = false;

        private float m_randomFactor;
        // Use this for initialization
        private void Awake()
        {
            m_transform = transform;
            m_randomFactor = Random.Range(0.5f, 2);
        }
        void Start()
        {

        }
        private void OnTriggerEnter(Collider other)
        {
            PlayerController player = other.gameObject.GetComponent<PlayerController>();
            if (player != null)
            {
                IsPlayerInside = true;
            }
        }
        private void OnTriggerExit(Collider other)
        {
             
            PlanetView planet = other.gameObject.GetComponent<PlanetView>();
            WormholeView hole = other.gameObject.GetComponent<WormholeView>();
            PlayerController player = other.gameObject.GetComponent<PlayerController>();
            if (planet != null)
                planet.StartDesctruction();
            if (hole != null)
                hole.StartDesctruction();

            if(player != null)
            {
                IsPlayerInside = false;
                StartCoroutine(CheckPlayerInside(player));
            }
        }
        IEnumerator CheckPlayerInside(PlayerController player)
        {
            yield return new WaitForSeconds(0.2f);
            if (!IsPlayerInside)
                player.GameOver();
        }
        // Update is called once per frame
        void Update()
        {
            m_transform.localScale = m_transform.localScale * (1.0f - CrunchFactor * Time.deltaTime * m_randomFactor);
        }
    }
}
