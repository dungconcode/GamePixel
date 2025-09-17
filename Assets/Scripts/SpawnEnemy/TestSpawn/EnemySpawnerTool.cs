using UnityEngine;
using UnityEditor;
using UnityEngine.Rendering;

public class EnemySpawnerTool : EditorWindow
{
    private TestEnemySpawn enemySpawn;
    private Vector2 scrollPos;

    [MenuItem("Tools/Enemy Spawner Tool")]
    public static void ShowWindow()
    {
        GetWindow<EnemySpawnerTool>("Enemy Spawner Tool"); //Hiển thị cửa sổ với tiêu đề "Enemy Spawner Tool"
    }
    private void OnGUI()
    {
        GUILayout.Label("Enemy Spawner Tool", EditorStyles.boldLabel); // Hiển thị tiêu đề in đậm Fuck :))

        enemySpawn = (TestEnemySpawn) EditorGUILayout.ObjectField("Enemy Spawn Manager", enemySpawn, typeof(TestEnemySpawn), true);

        if (enemySpawn == null) return;
        scrollPos = EditorGUILayout.BeginScrollView(scrollPos);
        for (int i = 0;  i < enemySpawn.spawnAreas.Count; i++)
        {
            var area = enemySpawn.spawnAreas[i];
            EditorGUILayout.BeginVertical();
            GUILayout.Label($"Spawn Area {i + 1}", EditorStyles.boldLabel);
            area.spawnArea = (BoxCollider2D)EditorGUILayout.ObjectField("Spawn Area", area.spawnArea, typeof(BoxCollider2D), true);
            //EditorGUILayout.BeginVertical();
            area.areaName = (string)EditorGUILayout.TextField("Area Name", area.areaName);
            for(int j = 0; j < area.enemyWaves.Count; j++)
            {
                var wave = area.enemyWaves[j];
                EditorGUILayout.BeginHorizontal();
                wave.enemyPrefab = (GameObject)EditorGUILayout.ObjectField("Enemy Prefabs", wave.enemyPrefab, typeof(GameObject), true);
                wave.count = EditorGUILayout.IntField("Count enemy", wave.count);
                if(GUILayout.Button("X", GUILayout.Width(20)))
                {
                    area.enemyWaves.RemoveAt(j);
                }
                EditorGUILayout.EndHorizontal();
            }
            if (GUILayout.Button("Add Enemy Wave"))
            {
                area.enemyWaves.Add(new EnemyWaveData());
            }
            if(GUILayout.Button("Remove This Area"))
            {
                enemySpawn.spawnAreas.RemoveAt(i);
            }
            EditorGUILayout.EndVertical();
        }
        GUILayout.Space(200);
        if (GUILayout.Button("Add Spawn Area",GUILayout.Height(30)))
        {
            enemySpawn.spawnAreas.Add(new SpawnAreaData());
        }
        if (GUI.changed)
        {
            EditorUtility.SetDirty(enemySpawn);
        }
        EditorGUILayout.EndScrollView();
    }
}
