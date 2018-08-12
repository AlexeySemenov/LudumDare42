using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

namespace Antisystems.BigCrunch
{
    public class WarpController : MonoBehaviour
    {
        [SerializeField]
        private CinemachineVirtualCamera m_camera;

        [SerializeField]
        private ArcView m_arc;

        [SerializeField]
        private UniverseView m_currentUniverse;

        [SerializeField]
        private AudioSource m_warpSound;

        [SerializeField]
        private AudioSource m_blockSound;

        private bool m_isChooseWarp = false;

        private Transform m_player;
        // Use this for initialization
        void Start()
        {
            m_player = GameObject.FindGameObjectWithTag("Player").transform;
        }

        public void ChooseWarp()
        {
            m_isChooseWarp = true;
            m_camera.enabled = true;
            m_arc.SetActive(true);
        }

        private void Warp(Vector3 position)
        {
            m_warpSound.Play();
            m_player.position = position;
            Debug.Log(position);
        }

        private void Update()
        {
            if (Input.GetMouseButtonDown(0) && m_isChooseWarp)
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hitInfo = new RaycastHit();
                // Does the ray intersect any objects excluding the player layer
                if (Physics.Raycast(ray, out hitInfo, 10000, LayerMask.NameToLayer("UCollider")))
                {
                    UniverseView view = hitInfo.collider.gameObject.GetComponentInParent<UniverseView>();
                    if (view != null)
                    {
                        int delta = Mathf.Abs(view.row - m_currentUniverse.row) +
                            Mathf.Abs(view.column - m_currentUniverse.column);
                        if (delta < 2)
                        {
                            PlanetGenerator pg = view.gameObject.GetComponentInChildren<PlanetGenerator>();
                            if (pg == null)
                            {
                                Warp(hitInfo.collider.bounds.center);
                                Debug.Log("End");
                                m_isChooseWarp = false;
                                m_camera.enabled = false;
                                m_arc.SetActive(false);
                                return;
                            }
                            Vector3 hole = pg.GetHolePosition();
                            if (hole != Vector3.zero)
                            {
                                m_currentUniverse = view;
                                Warp(new Vector3(hole.x, 0, hole.z));
                                Debug.Log("UHit");
                                m_isChooseWarp = false;
                                m_camera.enabled = false;
                                m_arc.SetActive(false);
                            }
                        }
                        else
                        {
                            m_blockSound.Play();
                        }
                    }
                }
            }
        }
    }
}
