using System.Collections;
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

    private void Start()
    {
        StartSpawn();
    }

    private IEnumerator IESpawnItems()
    {
        while (true)
        {
            yield return SPAWN_DELAY;  

            Item item = Instantiate(_item);

            if (_keeper.CanAddItem(item, out Vector3 localPosition))
            {
                _keeper.AddItem(item);

                item.transform.localPosition = localPosition;

                yield return new WaitForSeconds(_spawnDelay);
            }
            else
                Destroy(item.gameObject);
        }
    }
}
