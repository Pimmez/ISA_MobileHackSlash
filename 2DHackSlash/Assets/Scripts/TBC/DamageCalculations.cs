using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageCalculations : MonoBehaviour
{
    public int CalculateDamage(int _attackerDamage, int _defenderDefence)
    {
        int _damageDone = (_attackerDamage - _defenderDefence);
        if (_damageDone < 1)
        {
            _damageDone = 1;
        }

        return _damageDone;
    }
}