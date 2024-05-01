using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GateController : MonoBehaviour
{
    public EnemyManager enemyManager;
    private bool allEnemiesDefeated = false;

    // ��һ���ؿ�������
    public string nextLevelName = "level1";

    private void Start()
    {
        gameObject.SetActive(false); // ��ʼ״̬�����ش�����
    }

    private void Update()
    {
        // �������Ƿ�ȫ��������
        if (allEnemiesDefeated && enemyManager.GetLastWave() && enemyManager.enemyCount == 1)
        {
            allEnemiesDefeated = true;
            OpenGate();
        }
    }

    // �򿪴�����
    private void OpenGate()
    {
        gameObject.SetActive(true);
    }

    // ����������һ���ؿ����߼�
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            SceneManager.LoadScene(nextLevelName);
        }
    }
}