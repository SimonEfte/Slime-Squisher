using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ObjectPool : MonoBehaviour
{
    public static ObjectPool instance;

    [SerializeField] private GameObject slime1Prefab;
    private Queue<GameObject> slime1Pool = new Queue<GameObject>();
    [SerializeField] private int slime1PoolSize = 50;
    public static float slime1Size;

    [SerializeField] private GameObject paperClipPrefab;
    private Queue<GameObject> paperClipPool = new Queue<GameObject>();
    [SerializeField] private int paperClipPoolSize = 50;
    public static float paperClipSize;

    [SerializeField] private GameObject gooPrefab;
    private Queue<GameObject> gooPool = new Queue<GameObject>();
    [SerializeField] private int gooPoolSize = 50;

    [SerializeField] private GameObject coinsPrefab;
    private Queue<GameObject> coinsPool = new Queue<GameObject>();
    [SerializeField] private int coinsPoolSize = 200;

    [SerializeField] private TextMeshProUGUI textPrefab;
    private Queue<TextMeshProUGUI> textPool = new Queue<TextMeshProUGUI>();
    [SerializeField] private int textPoolSize = 200;

    [SerializeField] private TextMeshProUGUI damageTextPrefab;
    private Queue<TextMeshProUGUI> damageTextPool = new Queue<TextMeshProUGUI>();
    [SerializeField] private int damageTextPoolSize = 200;

    private void Awake()
    {
        slime1Size = 45;
        paperClipSize = 0.44f;

        if (instance == null)
        {
            instance = this;
        }
    }

    public Transform slimeParent, projectileParent, coinsParent, textParent, damageTextParent;

    void Start()
    {
        #region Slime 1
        for (int i = 0; i < slime1PoolSize; i++)
        {
            GameObject slime1 = Instantiate(slime1Prefab);
            slime1.name = "Slime " + i;
            slime1Pool.Enqueue(slime1);
            slime1.SetActive(false);
            slime1.transform.SetParent(slimeParent);
            slime1.transform.localScale = new Vector3(slime1Size, slime1Size, slime1Size);
        }
        #endregion

        #region Paper clip
        for (int i = 0; i < paperClipPoolSize; i++)
        {
            GameObject paperClip = Instantiate(paperClipPrefab);
            paperClipPool.Enqueue(paperClip);
            paperClip.SetActive(false);
            paperClip.transform.SetParent(projectileParent);
            paperClip.transform.localScale = new Vector3(paperClipSize, paperClipSize, paperClipSize);
        }
        #endregion

        #region Goo
        for (int i = 0; i < gooPoolSize; i++)
        {
            GameObject goo = Instantiate(gooPrefab);
            gooPool.Enqueue(goo);
            goo.SetActive(false);
            goo.transform.SetParent(slimeParent);
        }
        #endregion

        #region Coin
        for (int i = 0; i < coinsPoolSize; i++)
        {
            GameObject goo = Instantiate(coinsPrefab);
            coinsPool.Enqueue(goo);
            goo.SetActive(false);
            goo.transform.SetParent(coinsParent);
            goo.transform.localScale = new Vector3(32f, 32f, 32f);
        }
        #endregion

        #region Text
        for (int i = 0; i < textPoolSize; i++)
        {
            TextMeshProUGUI text = Instantiate(textPrefab);
            textPool.Enqueue(text);
            text.gameObject.SetActive(false);
            text.transform.SetParent(textParent);
            text.transform.localScale = new Vector2(1.1f, 1.1f);
        }
        #endregion

        #region Text
        for (int i = 0; i < damageTextPoolSize; i++)
        {
            TextMeshProUGUI damageText = Instantiate(damageTextPrefab);
            damageTextPool.Enqueue(damageText);
            damageText.gameObject.SetActive(false);
            damageText.transform.SetParent(damageTextParent);
            damageText.transform.localScale = new Vector2(1.1f, 1.1f);
        }
        #endregion
    }

    #region Slime 1
    public GameObject GetSlime1FromPool()
    {
        if (slime1Pool.Count > 0)
        {
            GameObject slime1 = slime1Pool.Dequeue();
            slime1.SetActive(true);
            return slime1;
        }
        else
        {
            GameObject slime1 = Instantiate(slime1Prefab);
            return slime1;
        }
    }

    public void ReturnSlime1FromPool(GameObject slime1)
    {
        slime1Pool.Enqueue(slime1);
        slime1.SetActive(false);
    }
    #endregion

    #region Paper clip
    public GameObject GetPaperClipFromPool()
    {
        if (paperClipPool.Count > 0)
        {
            GameObject paperClip = paperClipPool.Dequeue();
            paperClip.SetActive(true);
            return paperClip;
        }
        else
        {
            GameObject paperClip = Instantiate(paperClipPrefab);
            return paperClip;
        }
    }

    public void ReturnPaperClipFromPool(GameObject paperClip)
    {
        paperClipPool.Enqueue(paperClip);
        paperClip.SetActive(false);
    }
    #endregion

    #region Goo
    public GameObject GetGooFromPool()
    {
        if (gooPool.Count > 0)
        {
            GameObject goo = gooPool.Dequeue();
            goo.SetActive(true);
            return goo;
        }
        else
        {
            GameObject goo = Instantiate(gooPrefab);
            return goo;
        }
    }

    public void ReturnGooFromPool(GameObject goo)
    {
        gooPool.Enqueue(goo);
        goo.SetActive(false);
    }
    #endregion

    #region Coin
    public GameObject GetCoinFromPool()
    {
        if (coinsPool.Count > 0)
        {
            GameObject coin = coinsPool.Dequeue();
            coin.SetActive(true);
            return coin;
        }
        else
        {
            GameObject coin = Instantiate(coinsPrefab);
            return coin;
        }
    }

    public void ReturnCoinFromPool(GameObject coin)
    {
        coinsPool.Enqueue(coin);
        coin.SetActive(false);
    }
    #endregion

    #region Text
    public TextMeshProUGUI GetTextFromPool()
    {
        if (textPool.Count > 0)
        {
            TextMeshProUGUI text = textPool.Dequeue();
            text.gameObject.SetActive(true);
            return text;
        }
        else
        {
            TextMeshProUGUI text = Instantiate(textPrefab);
            return text;
        }
    }

    public void ReturnTextFromPool(TextMeshProUGUI text)
    {
        textPool.Enqueue(text);
        text.gameObject.SetActive(false);
    }
    #endregion

    #region Damage Text
    public TextMeshProUGUI GetDamageTextFromPool()
    {
        if (damageTextPool.Count > 0)
        {
            TextMeshProUGUI damageText = damageTextPool.Dequeue();
            damageText.gameObject.SetActive(true);
            return damageText;
        }
        else
        {
            TextMeshProUGUI damageText = Instantiate(damageTextPrefab);
            return damageText;
        }
    }

    public void ReturnDamageTextFromPool(TextMeshProUGUI damageText)
    {
        damageTextPool.Enqueue(damageText);
        damageText.gameObject.SetActive(false);
    }
    #endregion
}
