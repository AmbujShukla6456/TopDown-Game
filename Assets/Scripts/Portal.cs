using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Portal : collideable
{
    private Scene scene;
    protected override void OnCollide(Collider2D hit)
    {
        scene = SceneManager.GetActiveScene();
        if(hit.name=="Player")
        {
            GameManager.instance.SaveState();
            if(scene.name=="main")    UnityEngine.SceneManagement.SceneManager.LoadScene("Dungeon1");
            else UnityEngine.SceneManagement.SceneManager.LoadScene("main");
        }
    }
}
