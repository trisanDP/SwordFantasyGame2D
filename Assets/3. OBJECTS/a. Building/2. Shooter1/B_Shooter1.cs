using OriginL.Building;
using OriginL.EnemySpace;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OriginL
{
    public class B_Shooter1 : BuildingBase {
        [SerializeField]private float AttackRange;
        [SerializeField]private LayerMask AttackLayers;

        [SerializeField]private GameObject bulletPrefab;
        [SerializeField]private Transform launchPoint;

        protected override void Awake() {
            base.Awake();
            MaxHealth = 100;
            Health = MaxHealth;
            if(AttackLayers == 0) {
                Debug.LogWarning("LayerMissing");
                AttackLayers = LayerMask.NameToLayer("Player");
            }
        }

        protected override void Stage_Node() {
            base.Stage_Node();
            
        }
        protected override void Stage_Build1() {
            base.Stage_Build1();
            animator.SetTrigger("Build1");
        }

        protected override void Stage_Build2() {
            base.Stage_Build2();
            animator.SetTrigger("Build2");
        }

        protected override void Stage_Build3() {
            base.Stage_Build3();
        }

        void OnEnemyDetect() {
            Collider2D hit = Physics2D.OverlapCircle(transform.position, AttackRange,AttackLayers);
            if(hit != null) {
                AttackEnemy(hit.gameObject);
            }
        }

        void AttackEnemy(GameObject Target) {

        }

        public void Shoot() {
            GameObject Bullet = Instantiate(bulletPrefab, launchPoint.position, launchPoint.rotation);
            Rigidbody2D rb = Bullet.GetComponent<Rigidbody2D>();
         /*   rb.AddForce(bulletForce * (distanceToPlayer * 10) * bulletPoint.up, ForceMode2D.Impulse);
            Destroy(Bullet, 5);*/
        }

        public override void SetSprite() {
            GameAssets gameAssets = GameAssets.i;
            mode1Sprite = gameAssets.Shooter1;
        }
    }
}
