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
using TMPro.EditorUtilities;
using Unity.VisualScripting;

public class PlayerСharacteristics: MonoBehaviour
{
    public PlayerСharacteristics Instance;
    public int healthPoints = 100;
    public int baseAttack = 2;
    public int numberOfWins = 0;

    private TextMeshProUGUI wins;

    void Start()
    {
        //DontDestroyOnLoad(this);
        wins = GameObject.Find("WinsCount").GetComponent<TextMeshProUGUI>();
    }

    private void Update()
    {
        if (wins != null)
        {
            wins.text = numberOfWins.ToString();
        }
    }
}