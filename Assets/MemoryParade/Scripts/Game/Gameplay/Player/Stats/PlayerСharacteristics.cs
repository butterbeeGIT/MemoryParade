using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using Assets.MemoryParade.Scripts.Game.Gameplay.Player;
using System.Collections;
using TMPro;
using Assets.MemoryParade.Scripts.Game.GameRoot;
public class PlayerСharacteristics: MonoBehaviour
{
    public static PlayerСharacteristics Instance;
    public int healthPoints = 100;
    public int baseAttack = 2;
    public int numberOfWins = 0;
    private TextMeshProUGUI winsText;
    //public TextMeshProUGUI winsText;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Не уничтожаем при смене сцен
        }
        else
        {
            Destroy(gameObject);
        }
    }
    public void AddScore()
    {
        numberOfWins++;
    }

    public void Boost()
    {
        numberOfWins--;
    }
}