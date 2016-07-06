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

public class CameraFollow : MonoBehaviour {

    [SerializeField]
    private float xMax;

    [SerializeField]
    private float yMax;

    [SerializeField]
    private float xMin;

    [SerializeField]
    private float yMin;

    [SerializeField]
    private float distancePlayer = 6.5f;


    private Transform target;

	// Use this for initialization
	void Start () {
        target = GameObject.Find("Player").transform;
	}
	
    void LateUpdate()
    {
        transform.position = new Vector3(Mathf.Clamp(target.position.x + distancePlayer, xMin, xMax), Mathf.Clamp(target.position.y, yMin, yMax), transform.position.z);
    }


	// Update is called once per frame
	void Update () {
        Camera cam = Camera.main;
        cam.orthographicSize = 17.64762f / (2f*cam.aspect);
        
    }
}
