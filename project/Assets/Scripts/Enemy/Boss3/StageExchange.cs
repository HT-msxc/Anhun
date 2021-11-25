using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageExchange : MonoBehaviour
{
    public Boss3Battle boss3Battle;
    bool isTriggered;
    private void OnTriggerEnter2D(Collider2D other) {
        if (!isTriggered && other.CompareTag("Player"))
        {
            if (boss3Battle.isStart)
            {
                boss3Battle.GetIntoNextState();
            }
            else boss3Battle.StartBattle();
            isTriggered = true;
        }
    }
}
