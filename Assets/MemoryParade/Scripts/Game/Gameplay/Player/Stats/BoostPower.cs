using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.MemoryParade.Scripts.Game.Gameplay.Player.Stats
{
    public class BoostPower: MonoBehaviour
    {
        public void OnClick()
        {
            if (PlayerСharacteristics.Instance.numberOfWins != 0)
            {
                PlayerСharacteristics.Instance.baseAttack++;
                PlayerСharacteristics.Instance.Boost();
            }
        }
    }
}
