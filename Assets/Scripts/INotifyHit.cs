using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public interface INotifyHit
{
    [HideInInspector]
    IDamageDealer Owner { get; set; }
    void NotifyAboutHit(AttackResult effect);
}

