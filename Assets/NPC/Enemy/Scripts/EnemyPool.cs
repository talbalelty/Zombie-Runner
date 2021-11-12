using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EnemyPool : MonoBehaviour
{
    [SerializeField] List<EnemyAI> enemies;
    [SerializeField] Canvas winCanvas;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (enemies.Count == 0)
        {
            StartCoroutine(HandleWin());
        }
        else
        {
            foreach (EnemyAI enemy in enemies)
            {
                if (!enemy.IsAlive)
                {
                    enemies.Remove(enemy);
                    return;
                }
            }
        }
    }

    IEnumerator HandleWin()
    {
        yield return new WaitForSeconds(2f);
        winCanvas.enabled = true;
        Time.timeScale = 0;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
}
