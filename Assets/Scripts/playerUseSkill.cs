﻿using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class playerUseSkill : MonoBehaviour
{
    private Rigidbody2D myRigidbody2D;
    private Animator myAnimator;
    private SpriteRenderer sp;
    public TextMeshProUGUI[] keys = new TextMeshProUGUI[5];
    private KeyCode[] keycodes = new KeyCode[5];
    public bool isAtk = false;
    GameObject objPlayer;
    ParameterPlayer paraPlayer;
    [SerializeField]
    private GameObject[] prefabsSpell;
    public AudioSource buttonSpaceAudio;
    public AudioSource buttonQAudio;
    [SerializeField]
    private Transform exitPoint;
    void Start()
    {
        myRigidbody2D = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
        sp = GetComponent<SpriteRenderer>();
        objPlayer = GameObject.Find("MainPlayer");
        paraPlayer = objPlayer.GetComponent<ParameterPlayer>();
    }

    // Update is called once per frame
    void Update()
    {
        ReadOptions();
        GetInput();
    }
    private void ReadOptions()
    {
        for (int i = 0; i < 5; i++)
        {
            keycodes[i] = (KeyCode)Enum.Parse(typeof(KeyCode), keys[i].text);
        }
    }
    private void GetInput()
    {
        paraPlayer = objPlayer.GetComponent<ParameterPlayer>();
        if (Input.GetKeyDown(KeyCode.Space) && Input.GetKey(KeyCode.Space) && paraPlayer.waitRecSkill[0] == false)
        {
            buttonSpaceAudio.Play();
            startAttack();
            StartCoroutine(AttackNormal());
        }
        if (Input.GetKeyDown(keycodes[0]) && Input.GetKey(keycodes[0]) && paraPlayer.waitRecSkill[1] == false)
        {
            buttonQAudio.Play();
            startAttack();
            StartCoroutine(Attack1());
        }
        if (Input.GetKeyDown(keycodes[2]) && Input.GetKey(keycodes[2]) && paraPlayer.waitRecSkill[3] == false)
        {
            
            startAttack();
            StartCoroutine(Attack3());
        }
    }
    private IEnumerator AttackNormal()
    {
        sword();
        paraPlayer.skill = 0;
        yield return new WaitForSeconds(0.5f);
        stopAttack();
    }
    private IEnumerator Attack1()
    {
        shuriken();
        paraPlayer.skill = 1;
        yield return new WaitForSeconds(0.3f);
        GameObject objEffSpell = Instantiate(prefabsSpell[0], exitPoint.position, Quaternion.identity);
        Rigidbody2D rgbSpell = objEffSpell.GetComponent<Rigidbody2D>();
        rgbSpell.velocity = new Vector2(objPlayer.transform.localScale.x * 20f, rgbSpell.velocity.y);
        yield return new WaitForSeconds(0.4f);
        Debug.Log("attack");
        stopAttack();
    }
    private IEnumerator Attack3()
    {
        fire();
        paraPlayer.skill = 3;
        yield return new WaitForSeconds(0.4f);
        GameObject objEffSpell = Instantiate(prefabsSpell[2], exitPoint.position, Quaternion.identity);
        Vector3 theScale = objEffSpell.transform.localScale;
        theScale.x = objPlayer.transform.localScale.x * 2.6286f;
        objEffSpell.transform.localScale = theScale;
        Rigidbody2D rgbSpell = objEffSpell.GetComponent<Rigidbody2D>();
        rgbSpell.velocity = new Vector2(objPlayer.transform.localScale.x * 20f, rgbSpell.velocity.y);
        yield return new WaitForSeconds(0.5f);
        Debug.Log("attack");
        stopAttack();
    }
    private void startAttack()
    {
        isAtk = true;
        resetAttack();
        ActivateLayer("AttackPlayer");
        myAnimator.SetBool("attack", true);
    }
    private void stopAttack()
    {
        isAtk = false;
        myAnimator.SetBool("attack", false);
        ActivateLayer("MovePlayer");
    } 
    public void ActivateLayer(string layerName)
    {
        for (int i = 0; i < myAnimator.layerCount; i++)
        {
            myAnimator.SetLayerWeight(i, 0);
        }
        myAnimator.SetLayerWeight(myAnimator.GetLayerIndex(layerName), 1);
    }
    private void resetAttack()
    {
        myAnimator.SetFloat("sword", 0);
        myAnimator.SetFloat("shuriken", 0);
        myAnimator.SetFloat("fire", 0);
        myAnimator.SetFloat("rasengan1", 0);
        myAnimator.SetFloat("resengan2", 0);
    }
    private void sword()
    {
        myAnimator.SetFloat("sword", 1);
        myAnimator.SetFloat("shuriken", 0);
        myAnimator.SetFloat("fire", 0);
        myAnimator.SetFloat("rasengan1", 0);
        myAnimator.SetFloat("resengan2", 0);
    }
    private void fire()
    {
        myAnimator.SetFloat("sword", 0);
        myAnimator.SetFloat("shuriken", 0);
        myAnimator.SetFloat("fire", 1);
        myAnimator.SetFloat("rasengan1", 0);
        myAnimator.SetFloat("resengan2", 0);
    }
    private void shuriken()
    {
        myAnimator.SetFloat("sword", 0);
        myAnimator.SetFloat("shuriken", 1);
        myAnimator.SetFloat("fire", 0);
        myAnimator.SetFloat("rasengan1", 0);
        myAnimator.SetFloat("resengan2", 0);
    }
}
