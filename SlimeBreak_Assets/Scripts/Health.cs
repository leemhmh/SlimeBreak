using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Health : MonoBehaviour
{
    public int PlayerHp;
    public int maxHP = 3;

    public Gamedirector gameDirector;
    // Start is called before the first frame update
    void Start()
    {
        PlayerHp = maxHP;
        this.gameDirector.Init(this.PlayerHp);
    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerHp > maxHP) {
            PlayerHp = maxHP;
        }
        this.gameDirector.Init(this.PlayerHp);
    }
}
