using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Gamedirector : MonoBehaviour
{
    public GameObject[] lifes;
    public void Init(int playerHp) {
        for(int i = 0; i < lifes.Length; i++)
            this.lifes[i].SetActive(false);

        for(int i = 0; i < playerHp; i++)
            this.lifes[i].SetActive(true);
    }
}
