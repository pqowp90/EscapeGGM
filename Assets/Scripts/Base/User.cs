using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class User
{
    public string userName;
    public long energy;
    public long ePC;
    public List<Soldier> solderList = new List<Soldier>();
}
