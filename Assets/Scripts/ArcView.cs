using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Antisystems.BigCrunch
{
    [RequireComponent(typeof(LineRenderer))]
    public class ArcView : MonoBehaviour
    {

        public Transform StartPoint;
        public Transform EndPoint;
        public int SegmentsNum = 50;

        private Vector3[] m_arcPoints;
        private LineRenderer m_lineRenderer;
        private Material m_material;
        private Vector2 m_offset;

        private void Awake()
        {
            m_arcPoints = new Vector3[SegmentsNum + 1];
            for (int i = 0; i <= SegmentsNum; ++i)
                m_arcPoints[i] = Vector3.zero;
            m_lineRenderer = GetComponent<LineRenderer>();
            m_material = m_lineRenderer.material;
            m_offset = Vector2.zero;
        }
        void UpdateCurve()
        {
            float radius2 = Vector3.Distance(StartPoint.position, EndPoint.position);

            float startAngle = 45;
            float endAngle = 135;

            float angle = startAngle;
            float arcLength = endAngle - startAngle;
            for (int i = 0; i <= SegmentsNum; i++)
            {
                float xarc = Mathf.Lerp(StartPoint.position.x, EndPoint.position.x, ((float)i) / SegmentsNum);
                float yarc = Mathf.Sin(Mathf.Deg2Rad * angle) * radius2 - 0.707f * radius2;
                float zarc = Mathf.Lerp(StartPoint.position.z, EndPoint.position.z, ((float)i) / SegmentsNum);

                m_arcPoints[i].x = xarc;
                m_arcPoints[i].y = yarc;
                m_arcPoints[i].z = zarc;

                angle += (arcLength / SegmentsNum);
            }
            m_lineRenderer.positionCount = SegmentsNum + 1;
            m_lineRenderer.SetPositions(m_arcPoints);
        }
        void Start()
        {
            m_lineRenderer.enabled = false;
        }

        // Update is called once per frame
        void Update()
        {
            if (m_lineRenderer.enabled)
            {
                Vector3 mousePosition = Input.mousePosition;
                EndPoint.position = GetWorldPositionOnPlane(mousePosition, 0);

                UpdateCurve();
                m_offset.x -= Time.deltaTime;
                m_material.SetTextureOffset("_MainTex", m_offset);
            }
        }
        public void SetActive(bool isActive)
        {
            m_lineRenderer.enabled = isActive;
            StartPoint.position = GameObject.FindGameObjectWithTag("Player").transform.position;
        }

        public Vector3 GetWorldPositionOnPlane(Vector3 screenPosition, float y)
        {
            Ray ray = Camera.main.ScreenPointToRay(screenPosition);
            Plane xy = new Plane(Vector3.up, new Vector3(0, y, 0));
            float distance;
            xy.Raycast(ray, out distance);
            return ray.GetPoint(distance);
        }
    }
}