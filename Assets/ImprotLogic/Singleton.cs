using System;
using UnityEngine;

// Token: 0x02000087 RID: 135
[DisallowMultipleComponent]
public abstract class Singleton<T> : MonoBehaviour where T : Singleton<T>
{
	// Token: 0x1700005B RID: 91
	// (get) Token: 0x060005A9 RID: 1449 RVA: 0x000204CC File Offset: 0x0001E8CC
	// (set) Token: 0x060005AA RID: 1450 RVA: 0x000206A8 File Offset: 0x0001EAA8
	public static T Instance
	{
		get
		{
			if (Singleton<T>.ApplicationIsQuitting)
			{
				Debug.LogWarningFormat("[Singleton] Instance '{0}' already destroyed on application quit. Won't create again - returning null.", new object[]
				{
					typeof(T)
				});
				return (T)((object)null);
			}
			object @lock = Singleton<T>._lock;
			T result;
			lock (@lock)
			{
				if (!Singleton<T>.instantiated)
				{
					UnityEngine.Object[] array;
					if (Singleton<T>.FindInactive)
					{
						array = Resources.FindObjectsOfTypeAll(typeof(T));
					}
					else
					{
						array = UnityEngine.Object.FindObjectsOfType(typeof(T));
					}
					if (array == null || array.Length < 1)
					{
						GameObject gameObject = new GameObject();
						gameObject.name = string.Format("{0} [Singleton]", typeof(T));
						Singleton<T>.Instance = gameObject.AddComponent<T>();
						Debug.LogWarningFormat("[Singleton] An Instance of '{0}' is needed in the scene, so '{1}' was created{2}", new object[]
						{
							typeof(T),
							gameObject.name,
							(!Singleton<T>.Persist) ? "." : " with DontDestoryOnLoad."
						});
					}
					else if (array.Length >= 1)
					{
						Singleton<T>.Instance = (array[0] as T);
						if (array.Length > 1)
						{
							Debug.LogWarningFormat("[Singleton] {0} instances of '{1}'!", new object[]
							{
								array.Length,
								typeof(T)
							});
							if (Singleton<T>.DestroyOthers)
							{
								for (int i = 1; i < array.Length; i++)
								{
									Debug.LogWarningFormat("[Singleton] Deleting extra '{0}' instance attached to '{1}'", new object[]
									{
										typeof(T),
										array[i].name
									});
									UnityEngine.Object.Destroy(array[i]);
								}
							}
						}
						return Singleton<T>.instance;
					}
				}
				result = Singleton<T>.instance;
			}
			return result;
		}
		protected set
		{
			Singleton<T>.instance = value;
			Singleton<T>.instantiated = true;
			Singleton<T>.instance.Init();
			if (Singleton<T>.Persist)
			{
				UnityEngine.Object.DontDestroyOnLoad(Singleton<T>.instance.gameObject);
			}
		}
	}

	// Token: 0x060005AB RID: 1451 RVA: 0x000206E8 File Offset: 0x0001EAE8
	private void Awake()
	{
		object @lock = Singleton<T>._lock;
		lock (@lock)
		{
			if (!Singleton<T>.instantiated)
			{
				Singleton<T>.Instance = (this as T);
			}
			else if (Singleton<T>.DestroyOthers)
			{
				T t = Singleton<T>.Instance;
				if (t.GetInstanceID() != base.GetInstanceID())
				{
					Debug.LogWarningFormat("[Singleton] Deleting extra '{0}' instance attached to '{1}'", new object[]
					{
						typeof(T),
						base.name
					});
					UnityEngine.Object.Destroy(this);
				}
			}
		}
	}

	// Token: 0x060005AC RID: 1452 RVA: 0x00020790 File Offset: 0x0001EB90
	protected virtual void Init()
	{
	}

	// Token: 0x060005AD RID: 1453 RVA: 0x00020792 File Offset: 0x0001EB92
	protected virtual void OnDestroy()
	{
		Singleton<T>.ApplicationIsQuitting = true;
		Singleton<T>.instantiated = false;
	}

	// Token: 0x04000308 RID: 776
	private static volatile T instance;

	// Token: 0x04000309 RID: 777
	private static object _lock = new object();

	// Token: 0x0400030A RID: 778
	public static bool FindInactive = true;

	// Token: 0x0400030B RID: 779
	public static bool Persist;

	// Token: 0x0400030C RID: 780
	public static bool DestroyOthers = true;

	// Token: 0x0400030D RID: 781
	private static bool instantiated;

	// Token: 0x0400030E RID: 782
	public static bool ApplicationIsQuitting;
}
