using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawner : MonoBehaviour
{
    // Start is called before the first frame update

    private bool isCreating = true;
    private bool isLock = false;
    public List<Item> items = new List<Item>();

    private Item currentItem;

    void Start() { }

    // Update is called once per frame
    void Update()
    {
        if (!isLock)
        {
            if (isCreating)
            {
                StartCoroutine(CreateItem());
            }
            else
            {
                StartCoroutine(DestroyItem());
            }
        }
    }

    IEnumerator DestroyItem()
    {
        isLock = true;
        isCreating = false;
        yield return new WaitForSeconds(3);
        currentItem.DestroyGameObject();
        isCreating = true;
        isLock = false;
    }

    IEnumerator CreateItem()
    {
        isLock = true;
        Vector2 pos = new Vector2(Random.Range(-7.0f, 7.0f), Random.Range(-1.5f, 2.0f));
        isCreating = true;
        yield return new WaitForSeconds(3);
        currentItem = Instantiate(items[Random.Range(0, items.Count)], pos, Quaternion.identity);
        isCreating = false;
        isLock = false;
    }
}
