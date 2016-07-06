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

using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody2D))]
public class Knife : MonoBehaviour {
    [SerializeField]
    private float speed;

    private bool fall;
    private float fallTimer;

    private float fallColDuration;

    private Rigidbody2D myRigidbody;

    private Vector2 direction;

    private IgnoreCollision ignoreCollision;

    private float iColor;
    private AudioSource sound;

	// Use this for initialization
	void Start () {
        myRigidbody = GetComponent<Rigidbody2D>();
        fall = false;
        fallColDuration = 0.5f;
        ignoreCollision = GetComponent<IgnoreCollision>();
        ignoreCollision.setOther(GameObject.Find("Player"));
        sound = GetComponent<AudioSource>();
	}
	
    void FixedUpdate()
    {
        myRigidbody.velocity = direction * speed;
    }

    public void Initialize(Vector2 direction)
    {
        this.direction = direction;
    }

	// Update is called once per frame
	void Update () {
        if (fall) Falling();
	}

    void OnBecameInvisible()
    {
        Destroy(gameObject);
    }

    void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.tag == "diana")
        {
            fall = true;
            sound.Play();
        }
    }

    void Falling()
    {
        fallTimer += Time.deltaTime;

        if(fallTimer >= fallColDuration)
        {
           gameObject.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f,0.5f);
             Destroy(gameObject);
        }
    }

}
