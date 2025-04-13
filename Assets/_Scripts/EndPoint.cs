using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class EndPoint : MonoBehaviour
{
        void OnTriggerEnter(Collider coll){
        GameObject otherGO = coll.gameObject;
            if(otherGO.transform.parent == null) return;
        
        Player p = otherGO.transform.parent.GetComponent<Player>();
        if(p != null){
         Destroy(this.gameObject);   
        }
        }
}
