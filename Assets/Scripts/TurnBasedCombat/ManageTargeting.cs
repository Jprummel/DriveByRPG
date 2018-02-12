using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ManageTargeting : MonoBehaviour
{
    public static bool IsEnabled;
    private static List<GameObject> _enemies = new List<GameObject>();

    private void Start()
    {
        StartCoroutine(LateStart());
    }

    IEnumerator LateStart()
    {
        yield return new WaitForEndOfFrame();

        for (int i = 0; i < TurnBasedStateMachine.ActiveEnemies.Count; i++)
        {
            _enemies.Add(TurnBasedStateMachine.ActiveEnemies[i].gameObject);
        }
    }

    public static void EnableTargets()
    {
        IsEnabled = true;
    }

    public static void DisableTargets()
    {
        IsEnabled = false;
    }
}
