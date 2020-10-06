using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private CombatSystem system;

    public void Init(CombatSystem sys)
    {
        system = sys;
    }

}
