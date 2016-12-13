#define BASEGAMEPIECE

using UnityEngine;
using System.Collections;

public class BaseGamePiece : MonoBehaviour {

	// Tile currentTile;
	public Animator animator = null;
	public float moveSpeedToTile = 1.0f;
	private Vector3 moveTarget;
	private bool walking = false;

	// Combat Stuffs
	public int HP = 1;
	public int damage = 1;
	public float hitChance = 1.0f;
	public uint moveCount = 3;
	private bool alive;

	public bool Walking
	{
		get {return walking;}
		set 
		{
			walking = value;
			if (animator)
				animator.SetBool("Walking", value);
		}
	}

	public bool Alive
	{
		get { return alive;}
		set 
		{
			if (animator && !value && alive)
				animator.SetTrigger("Kill");
			alive = value; 
			if (animator)
				animator.SetBool("Dead", !value);
		}
	}

	
	//private bool attacking = false;
	//public bool Attacking
	//{
	//	get {return attacking;}
	//	set 
	//	{
	//		moving = value;
	//		if (animator)
	//			animator.SetTrigger("Attacking", true);
	//	}
	//}

	// Use this for initialization
	void Start () {
		GamePieceStart();
	}
	
	// Update is called once per frame
	void Update () {
		GamePieceUpdate();
	}

	protected virtual void GamePieceStart()
	{
		moveTarget = transform.position;
	}

	//  Used for testing and Debugging.
	protected virtual void GamePieceUpdate()
	{
		//  TEMPORARY:  Move on input
		if (Input.GetKeyDown(KeyCode.W))
			MoveNorth();
		if (Input.GetKeyDown(KeyCode.S))
			MoveSouth();
		if (Input.GetKeyDown(KeyCode.A))
			MoveWest();
		if (Input.GetKeyDown(KeyCode.D))
			MoveEast();
		if (Input.GetKeyDown(KeyCode.Mouse0))
			Attack();
		if (animator && Input.GetKeyDown(KeyCode.Mouse1))
			animator.SetTrigger("Kill");
		//if (animator && Input.GetKeyDown(KeyCode.Mouse0))
		//	animator.SetTrigger("Attack");

		//  Move toward the tile's position.
		if (Walking)
		{
			transform.position = Vector3.MoveTowards(transform.position, moveTarget, moveSpeedToTile * Time.deltaTime);
			if (transform.position == moveTarget)
				Walking = false;
			else
				transform.forward = new Vector3(moveTarget.x - transform.position.x, 0.0f, moveTarget.z - transform.position.z);
		}
	}

	public virtual void MoveNorth()
	{
		moveTarget += Vector3.forward;
		transform.forward = Vector3.forward;
		Walking = true;
	}
	
	public virtual void MoveSouth()
	{
		moveTarget -= Vector3.forward;
		transform.forward = -Vector3.forward;
		Walking = true;
	}
	
	public virtual void MoveWest()
	{
		moveTarget -= Vector3.right;
		transform.forward = -Vector3.right;
		Walking = true;
	}
	
	public virtual void MoveEast()
	{
		moveTarget += Vector3.right;
		transform.forward = Vector3.right;
		Walking = true;
	}
	
	//  Returns true if killed.
	public virtual bool TakeDamage(int _damage)
	{
		HP -= damage;
		if (HP <= 0)
		{
			Alive = false;
			return true;
		}
		else
			return false;
	}
	
	//  Returns true if hit.
	public virtual bool Attack(/*BasePlayer _target*/)
	{
		if (Random.value <= hitChance)
		{
			if (animator)
				animator.SetTrigger("Attack");
			return true;
		}
		else
		{
			// nothing
			return false;
		}
	}

#if TILE
	void SetTile(Tile _tile)
	{
		currentTile = _tile;
	}
	void Teleport(Tile _tile)
	{
		currentTile = _tile;
		// explicitly move the transform.position
	}
#endif
}
