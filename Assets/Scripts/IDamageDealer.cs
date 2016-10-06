using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public interface IDamageDealer
{
    void OnAttackHitTarget(AttackResult result);
}