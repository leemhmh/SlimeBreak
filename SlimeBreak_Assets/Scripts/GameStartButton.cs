using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameStartButton : MonoBehaviour
{
    public void SceneChange() {
        SceneManager.LoadScene("second");
        
    }
}
