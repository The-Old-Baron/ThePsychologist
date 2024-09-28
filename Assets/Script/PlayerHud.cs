using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHud : MonoBehaviour
{
    public static PlayerHud Instance;
    public Slider lifeBar;
    public PauseGame PauseMenu;
    // Update is called once per frame
    void Awake()
    {
        Instance = this;
    }
    void Update()
    {
        lifeBar.value = Player.Instance.Life;
        lifeBar.maxValue = Player.Instance.MaxLife;
    }
}
