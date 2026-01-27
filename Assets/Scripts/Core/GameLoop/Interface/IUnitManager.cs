using UnityEngine;

public interface IUnitManager
{
    public void OnUnitHit(UnitType unitType, int instanceID);
    public void OnUnitDie(UnitType unitType, int instanceID);
}
