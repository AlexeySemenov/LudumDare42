using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Antisystems.BigCrunch
{
    public class EndingTrigger : MonoBehaviour {

        // Use this for initialization
        void Start() {

        }
        IEnumerator TheEnd()
        {
            yield return new WaitForSeconds(3);
            SceneManager.LoadScene(2);
        }
        // Update is called once per frame
        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.GetComponent<PlayerController>() != null)
            {
                other.gameObject.GetComponent<PlayerController>().IsWin = true;
                StartCoroutine(TheEnd());
            }
        }
    }
}