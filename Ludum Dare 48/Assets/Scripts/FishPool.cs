using PurpleCable;
using System.Collections.Generic;
using System.Linq;

public class FishPool : PrefabPool<Fish, FishDef>
{
    protected override Fish GetPrefab(FishDef category)
    {
        return category.Prefab;
    }

    protected override Dictionary<FishDef, List<Fish>> GetInitialLists()
    {
        return GameManager.FishCollection.GetItems().ToDictionary(x => x, y => new List<Fish>());
    }
}
