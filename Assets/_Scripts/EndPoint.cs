using UnityEngine;
using System.Collections;
using System.Collections.Generic; 
using UnityEngine.SceneManagement;

public class EndPoint : MonoBehaviour
{
        void OnTriggerEnter(Collider coll){
        GameObject otherGO = coll.gameObject;
            if(otherGO.transform.parent == null) return;
        
        Player p = otherGO.transform.parent.GetComponent<Player>();
        if(p != null && p.canLeave){
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
        }
}
