using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PureMVC.Unity;


public class Main : Program<GameManager>
{
    private void OnGUI()
    {
        if (GUILayout.Button("切换颜色组 0"))
        {
            GameManager.Notify(ObserverName.CHANGE_COLOR, 0);
        }
        if (GUILayout.Button("切换颜色组 1"))
        {
            GameManager.Notify(ObserverName.CHANGE_COLOR, 1);
        }
        if (GUILayout.Button("随机玩家尺寸"))
        {
            GameManager.Notify(ObserverName.RANDOM_SCALE);
        }
    }
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                GameManager.Execute(EventName.MOVE_PLAYER, hit.point);
            }
        }
    }
}
