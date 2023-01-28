using Assets.Scripts.Utils;
using System;
using UnityEngine;

namespace Assets.Scripts
{
    enum SoundType
    {
        JumpUp, JumpDown, Move
    }
    public class Player : MonoBehaviour, ICollideObservable
    {

        public float jumpAmount = 10;
        public float moveSpeed = 1;
        private float moveSpeedMutliplier = 1;

        private Rigidbody2D body;
        private new CircleCollider2D collider2D;

        //temp
        public GameObject hookPref;

        DebounceDelegate moveCoroutiner, jumpUpCoroutiner, jumpDownCoroutiner;

        private void Awake()
        {
            body = GetComponent<Rigidbody2D>();
            collider2D = GetComponent<CircleCollider2D>();

            KeyBindManager.Instance
                .Bind()((_) => moveSpeedMutliplier = 1)
                .Bind(KeyCode.LeftShift, KeyCode.RightShift)((_) => moveSpeedMutliplier = 3)
                .Bind(KeyCode.LeftControl, KeyCode.RightControl)((_) => moveSpeedMutliplier = 0.5f)
                .Bind(KeyCode.LeftArrow, KeyCode.A)((_) => Move(-1))
                .Bind(KeyCode.RightArrow, KeyCode.D)((_) => Move(1))
                .Bind(KeyCode.E)((_) => Hook())
                .Bind(KeyCode.Space)((_) => Jump());

            moveCoroutiner = DebounceSound(SoundType.Move, () => 0.3f / GetCalculatedMovement());
            jumpUpCoroutiner = DebounceSound(SoundType.JumpUp, 0.25f);
            jumpDownCoroutiner = DebounceSound(SoundType.JumpDown, 1f);

            CameraController.Instance.ZoomOutAnimation(10);

            CollideObserver.Instance.SubscribeCollider(this, () => new()
            {
                origin = new(transform.position.x, transform.position.y - collider2D.radius),
                distance = 0.05f,
                direction = Vector2.down
            });

            Vars.Instance.CurrentPlayer = this;
        }
        DebounceDelegate DebounceSound(SoundType type, float delay) => DebounceSound(type, () => delay);
        DebounceDelegate DebounceSound(SoundType type, Func<float> delay) => InvokeUtils.Debounce(() => PlaySound(type), delay);

        void Hook()
        {
            /* TODO: change tool -> implements UseTool() with key binding -> implements Hook's tool - hooking.
            Hook hook = Instantiate(hookPref, transform.position, Quaternion.identity)
                .GetComponent<Hook>();
            hook.owner = this;
            hook.MoveTo();
            */
        }

        void Move(int direction)
        {
            StartCoroutine(moveCoroutiner());
            body.velocity = new(GetCalculatedMovement() * direction, body.velocity.y);
        }
        void Jump()
        {
            StartCoroutine(jumpUpCoroutiner());
            if (!GetCollision()) return;
            body.AddForce(transform.up * jumpAmount);
        }

        float GetCalculatedMovement() => moveSpeed * moveSpeedMutliplier;
        void PlaySound(SoundType type)
        {
            if (!(
                GetCollision(out GameObject collision) &&
                collision.TryGetComponent(out Obstacle obstacle)
            )) return;

            AudioClip clip = type switch
            {
                SoundType.Move => obstacle.moveSound,
                SoundType.JumpUp => obstacle.jumpUpSound,
                SoundType.JumpDown => obstacle.jumpDownSound,
                _ => throw new NotImplementedException(),
            };
            EffectSoundPool.Instance.PlayAudio(clip, transform.position.x, transform.position.y);
        }

        bool GetCollision(out GameObject @object)
        {
            @object = GetCollision();
            return @object != null;
        }
        GameObject GetCollision()
        {
            RaycastHit2D[] hits = Physics2D.CircleCastAll(transform.position, collider2D.radius, Vector2.down, 0.1f);
            foreach (RaycastHit2D hit in hits)
            {
                if (hit.rigidbody == null || hit.rigidbody.gameObject == gameObject) continue;
                return hit.rigidbody.gameObject;
            }
            return null;
        }
        GameObject ICollideObservable.GetGameObject() => gameObject;
        void ICollideObservable.OnCollided(GameObject gameObject)
        {
            if (body.velocity.y > 0.001f && body.velocity.y < 0.1f)
            {
                StartCoroutine(jumpDownCoroutiner());
            }
        }
    }
}