using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GamePlay : MonoBehaviour
{
    public static GamePlay Instance;
    public static float money;

    public Image interactImg;
    public TextMeshProUGUI txtMoney;
    public GameObject inventoryObj;
    public GameObject itemDetailObj;
    public GameObject inventoryButton;
    public Transform itemParent;

    [HideInInspector]
    public InteractableObject interact;
    [HideInInspector]
    public Dictionary<GameItem, InventoryItemDisplay> inventory;

    float timeCount = 0f;
    string status = "sleep";

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            money = 10f;
            inventory = new Dictionary<GameItem, InventoryItemDisplay>();
            ServerSystem.sendRequest = true;
            inventoryObj.SetActive(false);
            itemDetailObj.SetActive(false);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        List<string> objs = new List<string>() { "Items/Objects/flower1", "Items/Objects/flower2", "Items/Objects/flower3", "Items/Objects/flower6" };
        for (int i = 0; i < 100; i++)
        {
            Vector3 pos = new Vector3(UnityEngine.Random.Range(-30f, 30f), UnityEngine.Random.Range(-30f, 30f));
            int index = UnityEngine.Random.Range(0, objs.Count);
            CreateGameObjectFromPath(objs[index], pos);
        }

        for (int i = 0; i < 20; i++)
        {
            Vector3 pos = new Vector3(UnityEngine.Random.Range(-30f, 30f), UnityEngine.Random.Range(-30f, 30f));
            int index = UnityEngine.Random.Range(0, objs.Count);
            CreateGameObjectFromPath("Environment/bigtree", pos);
        }

        for (int i = 0; i < 200; i++)
        {
            Vector3 pos = new Vector3(UnityEngine.Random.Range(-30f, 30f), UnityEngine.Random.Range(-30f, 30f));
            int index = UnityEngine.Random.Range(0, objs.Count);
            CreateGameObjectFromPath("Environment/grass", pos);
        }
    }

    void Update()
    {
    }

    public static Vector3 StringToVector(string str)
    {
        float[] newPosCoordinates = str.Split(new string[] { ", " }, StringSplitOptions.None).Select(x => float.Parse(x)).ToArray();
        Vector3 newpos = new Vector3(newPosCoordinates[0], newPosCoordinates[1], newPosCoordinates[2]);
        return newpos;
    }

    public void ShowItemDetail(InventoryItemDisplay inventoryItem)
    {
        itemDetailObj.SetActive(true);
        itemDetailObj.GetComponent<ItemDetailDisplay>().UpdateDisplay(inventoryItem);
    }

    public void ShowInventory()
    {
        inventoryObj.SetActive(true);
        inventoryButton.SetActive(false);
    }

    public void HideInventory()
    {
        inventoryObj.SetActive(false);
        inventoryButton.SetActive(true);
    }

    public void Interact()
    {
        StartCoroutine(InteractCoroutine());
    }

    public IEnumerator InteractCoroutine()
    {
        if (interact != null)
        {
            GameObject go = Resources.Load<GameObject>("Effect/doing") as GameObject;
            GameObject spawn = Instantiate(go, interact.transform.position, Quaternion.identity);
            ServerSystem.curPlayer.s = "stop";
            yield return new WaitForSeconds(0.5f);

            AddItem(interact.item, 1);
            Destroy(interact.gameObject);
            interact = null;
            interactImg.sprite = null;
            ServerSystem.curPlayer.s = "stand";
            Destroy(spawn);
        }
    }

    public void AddItem(GameItem item, int amount)
    {
        if (!inventory.ContainsKey(item))
        {
            GameObject go = CreateGameObjectFromPath("UI/inventoryItemDisplay", itemParent);
            go.transform.localScale = new Vector3(1, 1, 1);
            go.GetComponent<InventoryItemDisplay>().item = item;
            inventory.Add(item, go.GetComponent<InventoryItemDisplay>());
            inventory[item].UpdateDisplay();
        }
        else
        {
            inventory[item].amount += amount;
            inventory[item].UpdateDisplay();
        }
    }

    public bool RemoveItem(GameItem item, int amount)
    {
        if (inventory[item].amount >= amount)
        {
            inventory[item].amount -= amount;
            inventory[item].UpdateDisplay();
            return true;
        }
        return false;
    }

    public GameObject CreateGameObjectFromPath(string path, Vector3 position)
    {
        GameObject go = Resources.Load<GameObject>(path) as GameObject;
        GameObject spawn = Instantiate(go, position, Quaternion.identity);
        return spawn;
    }

    public GameObject CreateGameObjectFromPath(string path, Transform transform)
    {
        GameObject go = Resources.Load<GameObject>(path) as GameObject;
        GameObject spawn = Instantiate(go, transform);
        return spawn;
    }

    public void AddMoney(float amount) {
        money += amount;
        UpdateStat();
    }

    public bool SpendMoney(float amount) {
        if (money < amount)
            return false;
        money -= amount;
        UpdateStat();
        return true;
    }

    public void UpdateStat()
    {
    }

    public void DestroyDelay(GameObject go, float sec)
    {
        StartCoroutine(DestroyDelayCoroutine(go, sec));
    }

    private IEnumerator DestroyDelayCoroutine(GameObject go, float sec)
    {
        yield return new WaitForSeconds(sec);
        Destroy(go);
    }

    public void SetTextDelay(TextMeshPro txt, string content, float sec)
    {
        StartCoroutine(SetTextDelayCoroutine(txt,content, sec));
    }

    private IEnumerator SetTextDelayCoroutine(TextMeshPro txt, string content, float sec)
    {
        yield return new WaitForSeconds(sec);
        txt.text = content;
    }

    public void DoFunctionLoop(float sec, Action action)
    {
        StartCoroutine(DoFunctionLoopCoroutine(sec, action));
    }

    public IEnumerator DoFunctionLoopCoroutine(float sec, Action action)
    {
        yield return new WaitForSeconds(sec);
        action.Invoke();
        StartCoroutine(DoFunctionLoopCoroutine(sec, action));
    }

    public static string GetRandomId(int length)
    {
        string s = "0123456789abcdefghijklmnopqrstuvwxABCDEFGHIJKLMNOPQRSTUVWXYZ";
        string result = "";
        for (int i = 0; i < length; i++)
        {
            result += s[UnityEngine.Random.Range(0, s.Length)];
        }
        return result;
    }
}