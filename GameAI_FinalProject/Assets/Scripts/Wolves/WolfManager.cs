using System.Collections.Generic;
using UnityEngine;

public class WolfManager : MonoBehaviour
{
    public static WolfManager instance;

    private List<WolfController> wolves = new List<WolfController>();
    private bool allWolvesInHuntRange = false;

    public float huntRange = 5f; // Settable distance from the deer

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void RegisterWolf(WolfController wolf)
    {
        wolves.Add(wolf);
    }

    public void MoveAllWolvesToDeer(Transform deer)
    {
        foreach (var wolf in wolves)
        {
            wolf.MoveInToAttack(deer);
        }
    }

    public void CheckAllWolvesInHuntRange(Transform deer)
    {
        int wolvesInRange = 0;
        foreach (var wolf in wolves)
        {
            if (Vector3.Distance(wolf.transform.position, deer.position) <= huntRange)
            {
                wolvesInRange++;
            }
        }

        if (wolvesInRange == wolves.Count)
        {
            allWolvesInHuntRange = true;
            foreach (var wolf in wolves)
            {
                wolf.MoveInToAttack(deer);
            }
        }
    }

    public bool AllWolvesInHuntRange()
    {
        return allWolvesInHuntRange;
    }
}
