using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroStats : MonoBehaviour
{
    public int pv = 10;
    public int maxPv = 10;

    public void ChangeStatPv(int change)
    {
        pv += change;
        if (pv > maxPv) pv = maxPv;
    }
}
