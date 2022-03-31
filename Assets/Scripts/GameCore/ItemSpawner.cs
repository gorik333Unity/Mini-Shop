using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game.Core;

[RequireComponent(typeof(ItemKeeper))]
public class ItemSpawner : MonoBehaviour
{
    private const float CHECK_IF_CAN_SPAWN_DELAY = 1f;

    [SerializeField]
    private Item _item;

    [SerializeField]
    private ItemKeeper _keeper;

    [SerializeField]
    private float _spawnDelay;

    public void StartSpawn()
    {
        StartCoroutine(IESpawnItems());
    }

    private IEnumerator IESpawnItems()
    {
        while (true)
        {
            var item = Instantiate(_item);

            if (_keeper.TryAddItem(item))
                yield return new WaitForSeconds(_spawnDelay);
            else
                Destroy(item.gameObject);

            yield return new WaitForSeconds(CHECK_IF_CAN_SPAWN_DELAY);
        }
    }
}
