using UnityEngine;

public class LevelManager : MonoBehaviour
{
 public float minLevelScore = 500;
 [SerializeField] Player playerGO;
void Update(){
 if(playerGO.score >= minLevelScore){
    playerGO.canLeave = true;
        }
    }
}
