using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private bool isDead;
    [SerializeField] private int hp;
    [SerializeField] private int currentHp;
    [SerializeField] private Canvas deadCanvas;
    [SerializeField] private GameObject damagedPanel;

    public void Init()
    {
        currentHp = hp;
    }

    public void DecreaseHp(int val)
    {
        if (isDead) return;

        currentHp -= val;

        StartCoroutine(damagedPanel.GetComponent<DamagedPanel>().StartDamagedPanel(0.1f));

        if (currentHp <= 0)
        {
            isDead = true;
            deadCanvas.enabled = true;

            StartCoroutine(GameManager.Instance.loadingSceneController.LoadScene("MainMenu", 2.0f));
        }
    }
}
