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

public class IgnoreCollision : MonoBehaviour {

    [SerializeField]
    private Collider2D other;

	// Use this for initialization
	void Awake () {
        Physics2D.IgnoreCollision(GetComponent<Collider2D>(), other, true);
	}

    public void setOther(GameObject another)
    {
        other = another.GetComponent<BoxCollider2D>();
    }
	
}
