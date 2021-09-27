using System.Collections.Generic;
using System.Linq;
[System.Serializable]
public class User
{
    public string userName;
    public string money;
    public string ePC;
    public List<Soldier> solderList = new List<Soldier>();
    public List<Weapon> Weapon = new List<Weapon>();
}