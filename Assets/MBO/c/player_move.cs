using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player_move : MonoBehaviour
{
	// 移動施加的力量
    public float moveForce = 10f;

    // 最大速度限制（避免越加越快）
    public float maxSpeed = 5f;

    // Rigidbody2D
    private Rigidbody2D rb;
	
	// 角色原始縮放
    private Vector3 originalScale;
	
	// ✔ 手動綁定 Animator
    public Animator animator;
	
    // Start is called before the first frame update
    void Start()
    {
        // 取得 Rigidbody2D
        rb = GetComponent<Rigidbody2D>();
		
		// 記錄初始縮放
        originalScale = transform.localScale;
    }

    // Update is called once per frame
    void Update()
    {
		bool isMoving = false;
		
        // 按 A → 向左加力 + 翻轉
        if (Input.GetKey(KeyCode.A))
        {
            rb.AddForce(Vector2.left * moveForce);
			isMoving = true;

            // X = -1（面向左）
            transform.localScale = new Vector3(
                -Mathf.Abs(originalScale.x),
                originalScale.y,
                originalScale.z
            );
		
        }

        // 按 D → 向右加力 + 還原
        if (Input.GetKey(KeyCode.D))
        {
            rb.AddForce(Vector2.right * moveForce);
			isMoving = true;

            // X = 1（面向右）
            transform.localScale = new Vector3(
                Mathf.Abs(originalScale.x),
                originalScale.y,
                originalScale.z
            );
        }

        // 限制最大水平速度（避免滑到失控）
        if (rb.velocity.x > maxSpeed)
        {
            rb.velocity = new Vector2(maxSpeed, rb.velocity.y);
        }
        else if (rb.velocity.x < -maxSpeed)
        {
            rb.velocity = new Vector2(-maxSpeed, rb.velocity.y);
        }
		
		// ✔ 控制動畫參數 walk
        if (animator != null)
        {
            animator.SetBool("walk", isMoving);
        }
    }
}
