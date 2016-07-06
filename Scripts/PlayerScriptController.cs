/* 
	Copyright © 2016  Ricard Borrull Baraut

	This file is part of NeuroNinja.

    NeuroNinja is free software: you can redistribute it and/or modify
    it under the terms of the GNU General Public License as published by
    the Free Software Foundation, either version 3 of the License, or
    (at your option) any later version.

    NeuroNinja is distributed in the hope that it will be useful,
    but WITHOUT ANY WARRANTY; without even the implied warranty of
    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
    GNU General Public License for more details.

    You should have received a copy of the GNU General Public License
    along with NeuroNinja.  If not, see <http://www.gnu.org/licenses/>.
*/

using System;
using UnityEngine;
using System.Collections;

public class PlayerScriptController : MonoBehaviour {

    private static PlayerScriptController instance;

    public static PlayerScriptController Instance
    {
        get
        {
            if(instance == null)
            {
                instance = GameObject.FindObjectOfType<PlayerScriptController>();
            }
            return instance;
        }
    }

    private bool facingRight;

    private Animator anim;

    private int attention;

    private int meditation;

    [SerializeField]
    private static int attRange;

    [SerializeField]
    private static int attHightRange;

    [SerializeField]
    private static int medRange;

    [SerializeField]
    private static int medHightRange;

    [SerializeField]
    private Transform knifePosition;

    [SerializeField]
    private float fallSpeed;
    
    private float moveSpeed;

    private float climbSpeed;

    [SerializeField]
    private GameObject knifePrefab;

    [SerializeField]
    private bool airControl;

    [SerializeField]
    private float jumpForce;

    [SerializeField]
    private LayerMask whatIsGround;

    [SerializeField]
    private float groundRadius;

    [SerializeField]
    private Transform[] groundPoints;

    [SerializeField]
    private EdgeCollider2D cutCollider;

    [SerializeField]
    private Audio auidos;

    private LevelProgress levelProgress;
    private CanvasManager canvasManager;
    private MainCanvas canvas;

    private bool climbing = false;
    private bool running = false;
    private float horizontal = 1.0f;

    public Rigidbody2D MyRigidbody { get; set; }
    public bool Attack { get; set; }
    public bool Slide { get; set; }
    public bool Jump { get; set; }
    public bool OnGround { get; set; }
    public bool Advancing { get; set; }
    public bool Sleeping { get; set; }


    // Use this for initialization
    void Start ()
    {
        canvas = FindObjectOfType<MainCanvas>();
        canvasManager = FindObjectOfType<CanvasManager>();
        facingRight = true;
       
        MyRigidbody = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        cutCollider.enabled = false;
        levelProgress = GameObject.FindObjectOfType<LevelProgress>();
       
        Sleeping = false;
        Advancing = true;
    }

    // Update is called once per frame
    void Update()
    {
        attention = canvas.getCurrentValue();
        meditation = canvas.getCurrentValue();
        HandleInput();
        PlayMovingSounds();
    }


	void FixedUpdate()
    {

        if (climbing || running)
        {
            if (attention >= attHightRange)
            {
                climbSpeed = 10;
                moveSpeed = 15;
            }
            else if (attention >= attRange)
            {
                climbSpeed = 5;
                moveSpeed = 10;
            }
            else
            {
                climbSpeed = 0;
                moveSpeed = 0;
            }
        }
        else
        {
            moveSpeed = 10;
        }

        
        OnGround = IsGrounded();
        HandleMovement(horizontal);
        Flip(horizontal);
        HandleLayers();
	}

    private void HandleMovement(float horizontal)
    {
        if (MyRigidbody.velocity.y < 0 && !climbing)
        {
            if (Input.GetKey(KeyCode.G))
            {
                anim.SetBool("glide", true);
                anim.SetBool("land", false);
                MyRigidbody.velocity = new Vector2(MyRigidbody.velocity.x, fallSpeed);
            }
            else
            {
                anim.SetBool("land", true);
            }
        }
        if (!Sleeping && !climbing && Advancing && !Attack && !Slide && (OnGround || airControl))
        {
            MyRigidbody.velocity = new Vector2(horizontal * moveSpeed, MyRigidbody.velocity.y);
        }
       
        if(Jump && MyRigidbody.velocity.y == 0)
        {
            MyRigidbody.AddForce(new Vector2(0f, jumpForce));
        }
        
        if(climbing)
        {
            anim.SetBool("climbing", true);
            MyRigidbody.gravityScale = 0f;
            if (climbSpeed > 0.0f)
            { 
                MyRigidbody.velocity = new Vector2(0f, horizontal * climbSpeed);
                this.GetComponent<Animator>().enabled = true;
            }
            else
            {
                MyRigidbody.velocity = Vector2.zero;
                this.GetComponent<Animator>().enabled = false;
            }
        }
        else
        {
            MyRigidbody.gravityScale = 1f;
            anim.SetBool("climbing", false);
        }
            
        if (!Advancing || climbing || Sleeping)
        {
            MyRigidbody.velocity = new Vector2(0f, MyRigidbody.velocity.y);
            anim.SetFloat("speed", 0f);
        }
        else
        {
            anim.SetFloat("speed", Mathf.Abs(horizontal*moveSpeed));
        }
            
    }

    private void HandleInput()
    {
        if (attention >= attRange && !Advancing && !Sleeping)
        {
            switch (levelProgress.GetTasckController())
            {
                case 0:
                    anim.SetTrigger("throw");
                    break;
                case 1:
                    anim.SetTrigger("attack");
                    break;
            }
        }

        if (meditation >= medRange && Sleeping)
        {
            anim.SetBool("asleep", true);
        }
        else
        {
            anim.SetBool("asleep", false);
        }  

    }

    private void Flip(float horizontal)
    {
        if(horizontal > 0 && !facingRight || horizontal < 0 && facingRight)
        {
            facingRight = !facingRight;
            Vector3 theScale = transform.localScale;
            theScale.x *= -1;
            transform.localScale = theScale;
        }
    }

    public int GetMeditationRange()
    {
        return medRange;
    }

    public void SetMeditationRange(int newValue)
    {
        medRange = newValue;
    }

    public int GetMeditationHightRange()
    {
        return medHightRange;
    }

    public void SetMeditationHightRange(int newValue)
    {
        medHightRange = newValue;
    }

    public void SetAttentionRange(int newValue)
    {
        attRange = newValue;
    }

    public void SetAttentionHightRange(int newValue)
    {
        attHightRange = newValue;
    }


    public int getMeditation()
    {
        return meditation;
    }

    private bool IsGrounded()
    {
        if (MyRigidbody.velocity.y <= 0)
        {
            foreach(Transform point in groundPoints)
            {
                Collider2D[] colliders = Physics2D.OverlapCircleAll(point.position, groundRadius, whatIsGround);
                for(int i=0; i<colliders.Length; i++)
                {
                    if(colliders[i].gameObject != gameObject)
                    {
                        return true;
                    }
                }
            }
        }
        return false;
    }

    private void HandleLayers()
    {
        if (!OnGround)
        {
            anim.SetLayerWeight(1, 1);
        }
        else
        {
            anim.SetLayerWeight(1, 0);
        }
    }

    public void ThrowKnife(int value)
    {
        if(!OnGround && value == 1 || OnGround && value == 0)
        {
            if (facingRight)
            {
                GameObject tmp = (GameObject)Instantiate(knifePrefab,knifePosition.position, Quaternion.Euler(new Vector3(0, 0, -90)));
                tmp.GetComponent<Knife>().Initialize(Vector2.right);
            }
            else
            {
                GameObject tmp = (GameObject)Instantiate(knifePrefab, knifePosition.position, Quaternion.Euler(new Vector3(0, 0, 90)));
                tmp.GetComponent<Knife>().Initialize(Vector2.left);
            }
        }

    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("edge"))
        {
            canvasManager.ChangeCanvas();
        }

        if (other.CompareTag("climbingUp"))
        {
            anim.SetFloat("speed",0f);
            climbing = !climbing;
            horizontal = 1.0f;
            canvasManager.SetShowing();
        }

        if (other.CompareTag("climbingDown"))
        {
            anim.SetFloat("speed", 0f);
            climbing = !climbing;
            horizontal = -1.0f;
            canvasManager.SetShowing();
        }

        if (other.CompareTag("run"))
        {
            anim.SetFloat("speed", 0f);
            running = !running;
            canvasManager.SetShowing();
        }

        if (other.CompareTag("jump"))
        {
            anim.SetTrigger("jump");
        }

        if (other.CompareTag("slide"))
        {
            anim.SetTrigger("slide");
        }

        if (other.CompareTag("asleep"))
        {
            Sleeping = !Sleeping;
            canvasManager.SetShowing();
        }
        if (other.CompareTag("attack"))
        {
            Advancing = false;
            canvasManager.SetShowing();
            Destroy(other);
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("climbingUp") || other.CompareTag("climbingDown"))
        {
            climbing = !climbing;
            other.isTrigger = false;
            MyRigidbody.velocity = Vector2.zero;
            horizontal = 1.0f;
            canvasManager.SetShowing();
        }

        if (other.CompareTag("asleep"))
        {
            canvasManager.SetShowing();
        }

    }

    public void MeleeAttack()
    {
        cutCollider.enabled = !cutCollider.enabled;
    }


    void PlayMovingSounds()
    {
        if (MyRigidbody.velocity.x >  0 && Time.timeScale == 1f && MyRigidbody.velocity.y==0 && !Slide)
        {
            auidos.PlayAudioSource(0);
        }
        else
        {
            auidos.StopAudioSource(0);
        }
        if (Slide && Time.timeScale == 1f)
        {
            auidos.PlayAudioSource(1);
        }
        else
        {
            auidos.StopAudioSource(1);
        }
        
    }


    public void PlayAudioAttack()
    {
        auidos.PlayAudioSource(2);
       
    }

}
