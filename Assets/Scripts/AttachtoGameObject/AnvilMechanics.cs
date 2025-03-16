using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnvilMechanics : MonoBehaviour
{
    public Collider2D anvilCollider;
    public Transform anvil, shadow;

    public ParticleSystem leftSmoke, rightSmoke;
    public Transform smoke1, smoke2;

    public AudioManager audioManager;

    public Animation shadowAnim, anvilAnim;
    Transform anvilIcon, anvilShadoww;

    private void Awake()
    {
        anvilShadoww = transform.Find("anvilShadow");
        shadowAnim = anvilShadoww.gameObject.GetComponent<Animation>();

        anvilIcon = transform.Find("anvilParent/anvilICon");
        anvilAnim = anvilIcon.gameObject.GetComponent<Animation>();

        GameObject audioTransform = GameObject.Find("AudioManager");
        audioManager = audioTransform.GetComponent<AudioManager>();

        smoke1 = transform.Find("SmokeRight"); smoke2 = transform.Find("SmokeLeft");
        rightSmoke = smoke1.GetComponent<ParticleSystem>();
        leftSmoke = smoke2.GetComponent<ParticleSystem>();

        anvil = transform.Find("anvilParent");
        shadow = transform.Find("anvilShadow");
        anvilCollider = gameObject.GetComponent<Collider2D>();
    }

    private void OnEnable()
    {
        Color color1 = anvilIcon.gameObject.GetComponent<SpriteRenderer>().color;
        color1.a = 1f;
        anvilIcon.gameObject.GetComponent<SpriteRenderer>().color = color1;

        Color color = anvilShadoww.gameObject.GetComponent<SpriteRenderer>().color;
        color.a = 1f;
        anvilShadoww.gameObject.GetComponent<SpriteRenderer>().color = color;

        smoke1.gameObject.SetActive(false);
        smoke2.gameObject.SetActive(false);

        anvilCollider.enabled = false;

        anvil.gameObject.SetActive(true);
        shadow.gameObject.SetActive(true);

        anvil.transform.localPosition = new Vector2(0, 2300);

        StartCoroutine(MoveAnvilDown());
    }

    IEnumerator MoveAnvilDown()
    {
        Vector2 endPos = new Vector2(0,5);

        float speed = 3100;

        while ((Vector2)anvil.transform.localPosition != endPos)
        {
            anvil.transform.localPosition = Vector2.MoveTowards(
                anvil.transform.localPosition,
                endPos,
                speed * Time.deltaTime
            );

            yield return null;
        }

        smoke1.gameObject.SetActive(true);
        smoke2.gameObject.SetActive(true);

        rightSmoke.Play();
        leftSmoke.Play();

        anvil.transform.localPosition = endPos;
        anvilCollider.enabled = true;

        audioManager.Play("Anvil");

        yield return new WaitForSeconds(0.2f);

        shadowAnim.Play();
        anvilAnim.Play();

        yield return new WaitForSeconds(0.3f);

        anvil.gameObject.SetActive(false);
        shadow.gameObject.SetActive(false);

        ObjectPool.instance.ReturnMeteorToPool(gameObject);
    }
}
