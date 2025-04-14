using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using TMPro;

public class LevelManager : MonoBehaviour
{
 public float minLevelScore = 500;
 [SerializeField] TextMeshProUGUI text;
 [SerializeField] Player playerGO;
void Update(){
 if(playerGO.score >= minLevelScore){
    playerGO.canLeave = true;
        }
    text.text = "Score: " + playerGO.score.ToString() + " of " + minLevelScore.ToString();
    }


}
