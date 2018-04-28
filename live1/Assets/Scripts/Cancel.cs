using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Cancel : MonoBehaviour {
    public void CancelButton()
    {
        SceneManager.LoadScene(0);
    }
}
