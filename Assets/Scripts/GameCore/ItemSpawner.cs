using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game.Core;

[RequireComponent(typeof(ItemKeeper))]
public class ItemSpawner : MonoBehaviour
{
    private const float SPAWN_DELAY = 1f;

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
        int count = 0;

        while (true)
        {
            Item item = Instantiate(_item);

            if (_keeper.TryAddItem(item, out Vector3 localPosition))
            {
                item.transform.position = localPosition;

                item.name = $"{count}";
                count++;

                yield return new WaitForSeconds(_spawnDelay);
            }
            else
                Destroy(item.gameObject);

            yield return new WaitForSeconds(SPAWN_DELAY);
        }
    }
}
