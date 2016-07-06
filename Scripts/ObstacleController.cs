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
using UnityEngine.UI;
using System.Collections;

public class ObstacleController : MonoBehaviour {
    [SerializeField]
    private ParticleSystem particle;
    private Component[] childObjects;
    [SerializeField]
    private int current;
    [SerializeField]
    private Slider slider;
    [SerializeField]
    private int damage;
    [SerializeField]
    private int tasckControll;  //0 -> throw
                                //1 -> Attack

    private AudioSource sound;


    void Awake()
    {
        current = 100;
        sound = GetComponent<AudioSource>();
    }


    void Update()
    {
        slider.value = current;
    }

    public int GetTasckControll()
    {
        return tasckControll;
    }

    void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.tag == "knife")
        {
            current -= damage;

        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("sword"))
        {
            current -= damage;
            sound.Play();
            if(particle)
                particle.Play();
            childObjects = GetComponentsInChildren<ObjectDamage>();
            foreach (ObjectDamage obj in childObjects)
            {
                obj.ChangeSpriteObstacle(current);
            }
        }

        if (other.CompareTag("Player"))
        {
            current -= damage;
        }
    }

    public int GetCurrectValue()
    {
        return current;
    }




}
