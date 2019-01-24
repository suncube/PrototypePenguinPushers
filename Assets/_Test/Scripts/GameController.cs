using System;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{

    [SerializeField]
    private FinishPlace m_FinishPlace;

    [SerializeField]
    private Transform m_Player;
    private Vector3 startPlayer;

    [Header("Settings")]
    [SerializeField]
    private int m_MaxEnemies = 10;

    [SerializeField]
    private EnemyController[] m_Enemies;
    private Vector3[] enemyPositions;

    [Header("UI")]
    [SerializeField]
    private Text m_ProgressText;


    private int currentEnemies;
    private void Awake()
    {
        LaunchInit();

        StartGame();
    }

    private void LaunchInit()
    {
        // for restart
        enemyPositions = new Vector3[m_Enemies.Length];
        for (var index = 0; index < m_Enemies.Length; index++)
        {
            enemyPositions[index] = m_Enemies[index].transform.position;
        }

        startPlayer = m_Player.position;
        m_FinishPlace.OnFinished += OnEnemyFinished;
        //
    }

    public void StartGame()
    {
        // for restart
        m_Player.position = startPlayer;
        for (var index = 0; index < m_Enemies.Length; index++)
        {
            m_Enemies[index].transform.position = enemyPositions[index];
        }

        if (m_MaxEnemies > m_Enemies.Length)
            m_MaxEnemies = m_Enemies.Length;

        var liveEnemies = (m_Enemies.Length - m_MaxEnemies) - 1;

        for (var index = liveEnemies; index >=0; index--)
        {
            m_Enemies[index].gameObject.SetActive(false);
        }

        currentEnemies = m_MaxEnemies;
        UpdateProgressStatus();
    }

    private void UpdateProgressStatus()
    {
        m_ProgressText.text = string.Format("Catched {0} / {1}", m_MaxEnemies-currentEnemies, m_MaxEnemies);
    }

    private void OnEnemyFinished(EnemyController enemy)
    {
        enemy.gameObject.SetActive(false);
        currentEnemies--;
        UpdateProgressStatus();

        if (currentEnemies <= 0)
            m_ProgressText.text = "GAME ENDED";
    }

    private void OnDestroy()
    {
        m_FinishPlace.OnFinished -= OnEnemyFinished;
    }

}