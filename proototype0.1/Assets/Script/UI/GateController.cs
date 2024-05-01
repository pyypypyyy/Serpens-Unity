using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GateController : MonoBehaviour
{
    public EnemyManager enemyManager;
    private bool allEnemiesDefeated = false;

    // 下一个关卡的名称
    public string nextLevelName = "level1";

    private void Start()
    {
        gameObject.SetActive(false); // 初始状态下隐藏传送门
    }

    private void Update()
    {
        // 检查敌人是否全部被消灭
        if (allEnemiesDefeated && enemyManager.GetLastWave() && enemyManager.enemyCount == 1)
        {
            allEnemiesDefeated = true;
            OpenGate();
        }
    }

    // 打开传送门
    private void OpenGate()
    {
        gameObject.SetActive(true);
    }

    // 触发进入下一个关卡的逻辑
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            SceneManager.LoadScene(nextLevelName);
        }
    }
}