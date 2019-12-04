using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxSpawner : MonoBehaviour
{
	public GameObject box;
	public ParticleSystem destroyBoxParticle;

	public Util.AudioGroup spawnAudio;
	public Util.AudioGroup destroyAudio;

	// Update is called once per frame
    void Update()
    {
	    if (Input.touchCount > 0)
	    {
		    foreach (Touch touch in Input.touches)
		    {
			    if (touch.phase == TouchPhase.Began)
			    {
				    Shoot(touch.position);
			    }
		    }
	    }
	    else
	    {
		    if (Input.GetMouseButtonDown(0)) Shoot(Input.mousePosition);
	    }
    }

    private void Shoot(Vector2 pos)
    {
	    Vector3 mousePos = Camera.main.ScreenToWorldPoint(new Vector3(pos.x, pos.y, 10));
	    Vector2 mousePos2D = new Vector2(mousePos.x, mousePos.y);

	    // Spawn at shoot point if there is no box; Destroy box if hit
	    RaycastHit2D hit = Physics2D.Raycast(mousePos2D, Vector2.zero);
	    if (hit.collider != null)
	    {
		    if (hit.collider.gameObject.layer != LayerMask.NameToLayer("Boxes")) return;

		    // Destroy box
		    Destroy(hit.collider.gameObject);

		    ParticleSystem p = Instantiate(destroyBoxParticle, hit.point, Quaternion.identity);
		    Destroy(p.gameObject, p.main.duration);

		    // Audio
		    destroyAudio.Play(hit.point, 1f);
	    }
	    else
	    {
		    // Spawn box
		    Instantiate(box, mousePos, Quaternion.Euler(0, 0, Random.value * 90));

		    // Audio
		    spawnAudio.Play(hit.point, 1f);
	    }
    }
}
