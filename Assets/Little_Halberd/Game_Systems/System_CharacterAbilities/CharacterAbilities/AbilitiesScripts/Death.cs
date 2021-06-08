using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

namespace LittleHalberd
{
    [CreateAssetMenu(fileName = "New ability", menuName = "LittleHalberd/Ability/Death")]
    public class Death : CharacterAbility
    {
        private float opacity;
        private SpriteRenderer[] sprites;
        private bool IsPlayer;
        public override void OnEnter(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            characterState.control.CHARACTER_AI_DATA.FollowEnabled = false;
            sprites = characterState.control.gameObject.
                                GetComponentsInChildren<SpriteRenderer>();

            IsPlayer = characterState.control.gameObject.layer ==
                       CustomLayers.Instance.GetLayer(LH_Layer.Player);
        }
        public override void UpdateAbility(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            if (IsPlayer)
            {
                PauseGameComponent.Instance.SetGamePause();
                PauseGameComponent.Instance.SetTryAgainWindow();
                PauseGameComponent.Instance.SetEndGame(true);
                AudioListener.pause = false;
                return;
            }
            opacity = 1 - stateInfo.normalizedTime;
            if (opacity <= 0.01f)
            {
                PoolObjectLoader.Instance.GetObject(characterState.control.DeathVFXType,
                                                    characterState.control.transform.position,
                                                    Quaternion.identity);
                PoolObjectLoader.Instance.GetObject(ObjectType.SFX_DEATH);

                SpawnHealthPoints(characterState.control);
                CheckIsEnemySpawned(characterState.control);
                DestroyCharacter(characterState.control.gameObject);
                return;
            }
            DecreaseSpriteOpacity(sprites);
        }
        public override void OnExit(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {

        }
        private void DecreaseSpriteOpacity(SpriteRenderer[] sprites)
        {
            for (int i = 0; i < sprites.Length; i++)
            {
                if (opacity < 0f)
                {
                    opacity = 0f;
                }
                Color c = sprites[i].material.color;
                c.a = opacity;
                sprites[i].color = c;
            }
        }
        private void DestroyCharacter(GameObject obj)
        {
            IPooledObject pooledObj = obj.GetComponent<IPooledObject>();
            if (pooledObj != null)
            {
                if (PoolObjectLoader.Instance.EnemyTypeInfo.Any(e => e.objectType ==
                                                                     pooledObj.objectType))
                {
                    PoolObjectLoader.Instance.DestroyObject(obj);
                }
                else
                {
                    if (pooledObj.objectType == ObjectType.REAPER_RED)
                    {
                        HandleBossDeath();
                    }
                    Destroy(obj);
                }
            }
            else
            {
                obj.SetActive(false);
            }
        }
        private void SpawnHealthPoints(CharacterControl control)
        {
            int randPoints = Random.Range(0, 4);
            for (int i = 0; i < randPoints; i++)
            {
                float randX = Random.Range(-2, 3);
                float randY = Random.Range(10, 16);
                GameObject healthPointObj = PoolObjectLoader.Instance.GetObject(ObjectType.HEALTH_POINT,
                                                                                control.transform.position,
                                                                                Quaternion.identity);
                healthPointObj.GetComponent<Rigidbody2D>().velocity = new Vector2(randX, randY);
            }
        }
        private void CheckIsEnemySpawned(CharacterControl control)
        {
            if (control.isSpawned)
            {
                BossSpawnEnemiesCounter.Instance.CurrentEnemiesNumber--;
            }
        }
        private void HandleBossDeath()
        {
            PauseGameComponent.Instance.SetGamePause();
            PauseGameComponent.Instance.SetEndingWindow();
            PauseGameComponent.Instance.SetEndGame(true);
            AudioListener.pause = false;
            BackgroundMusic.Instance.SetEndingAudioClip();
        }
    }
}
