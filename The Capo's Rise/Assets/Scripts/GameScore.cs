using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameScore : MonoBehaviour
{
    public PlayerController player;
    public TextMeshProUGUI textLife;
    public TextMeshProUGUI textDeadEnemies;
    public static GameScore Instance { get; private set; }

    [Header("Contadores de Jogo")]
    public int TotalDeadEnemies = 0;

    private void Awake()
    {
        // Implementação do Singleton
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Opcional: Mantém o objeto entre cenas
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Método para incrementar o contador de inimigos mortos
    public void IncrementDeadEnemies()
    {
        TotalDeadEnemies++;
    }
    void Update()
    {
        textLife.text = player.life.ToString();
        textDeadEnemies.text = TotalDeadEnemies.ToString();
    }
}
