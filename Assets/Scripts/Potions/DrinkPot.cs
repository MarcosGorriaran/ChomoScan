using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrinkPot : MonoBehaviour
{
    #region Variables
    [SerializeField] private Potions potions;
    public bool potionDrinked = false;
    private bool playerInTrigger = false;
    private Animator animator;
    [SerializeField] private GameObject bannerLight;
    SoundManager soundManager;
    #endregion
    public void Awake()
    {
        soundManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<SoundManager>();
    }
    public void Start()
    {
        bannerLight.SetActive(false);
        animator = GetComponent<Animator>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInTrigger = true;
        }
    }
    private void Update()
    {
        if (potions.potionsSelected && Input.GetKeyDown(KeyCode.O) && playerInTrigger)
        {
            animator.SetBool("changeBack", true);
            bannerLight.SetActive(true);
            StartCoroutine(Drink());
        }
    }
    private IEnumerator Drink()
    {
        soundManager.PlaySound(soundManager.Drinking);
        yield return new WaitForSeconds(2f);
        potions.msg.SetActive(false);
        potionDrinked = true;
    }
}

