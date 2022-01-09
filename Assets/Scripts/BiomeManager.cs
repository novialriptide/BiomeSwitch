using UnityEngine;
using UnityEngine.Tilemaps;
using TMPro;

public class BiomeManager : MonoBehaviour
{
    [Header("Game Objects")]
    public GameObject player;
    public GameObject platforms;

    [Header("Biome Switch Settings")]
    public float biomeStartChangeTime;
    public float biomeChangeTime;
    public int biome = -1;
    public ParticleSystem rainParticles;
    public TextMeshProUGUI biomeHint;
    public SpriteRenderer biomeBackground;
    public Sprite arcticBG;
    public Sprite beachBG;
    public Sprite rainforestBG;

    [HideInInspector]
    public float timeRemainingTilChange = 0;
    [HideInInspector]
    public float leftOff = 0f;
    public float maxMusicLength = 23f;

    int maxBiomes = 2;

    AudioManager audioManager;

    [Header("Biome Tile Palettes")]
    public GameObject arcticTundraPalette;
    public TileBase[] atPalette;
    public GameObject beachPalette;
    public TileBase[] bPalette;
    public GameObject rainForestPalette;
    public TileBase[] rfPalette;

    private void Awake()
    {
        audioManager = FindObjectOfType<AudioManager>().GetComponent<AudioManager>();

        int seed = System.DateTime.Now.Millisecond;
        Random.InitState(seed);
        biomeChangeTime = biomeStartChangeTime;

        Tilemap arcticTundraTileMap = arcticTundraPalette.GetComponentInChildren<Tilemap>();
        atPalette = arcticTundraTileMap.GetTilesBlock(arcticTundraTileMap.cellBounds);

        Tilemap beachTileMap = beachPalette.GetComponentInChildren<Tilemap>();
        bPalette = beachTileMap.GetTilesBlock(beachTileMap.cellBounds);

        Tilemap rainforestTileMap = rainForestPalette.GetComponentInChildren<Tilemap>();
        rfPalette = rainforestTileMap.GetTilesBlock(rainforestTileMap.cellBounds);
    }

    private void Update()
    {
        timeRemainingTilChange -= Time.deltaTime;
        
        if (timeRemainingTilChange <= 0)
        {
            timeRemainingTilChange = biomeChangeTime;

            biome += 1;
            if (biome > maxBiomes)
            {
                biome = 0;
            }

            if (biome == 0)
            {
                ReskinAllPlatforms(atPalette);
                audioManager.stopAllAudio();
                Camera.main.backgroundColor = new Color(87f / 255f, 135f / 255f, 212f / 255f, 255f / 255f);
                CharacterController2D characterController2D = player.GetComponent<CharacterController2D>();
                Player playerData = player.GetComponent<Player>();
                characterController2D.queueSlipperyFeet = true;
                playerData.sunburnEnabled = false;
                playerData.waterCollection = false;
                audioManager.PlayAtPosition("music_arctic_tundra_biome", leftOff);
                biomeHint.text = "The floor is slippery, be careful!";
                rainParticles.Stop();
                biomeBackground.sprite = arcticBG;
                audioManager.Play("sound_transition");
            }

            if (biome == 1)
            {
                ReskinAllPlatforms(bPalette);
                audioManager.stopAllAudio();
                Camera.main.backgroundColor = new Color(18f / 255f, 109f / 255f, 255f / 255f, 255f / 255f);
                CharacterController2D characterController2D = player.GetComponent<CharacterController2D>();
                Player playerData = player.GetComponent<Player>();
                characterController2D.queueNormalFeet = true;
                playerData.sunburnEnabled = true;
                playerData.waterCollection = false;
                audioManager.PlayAtPosition("music_beach_biome", leftOff);
                biomeHint.text = "It's getting hot, find some shade to avoid sunburns.";
                rainParticles.Stop();
                biomeBackground.sprite = beachBG;
                audioManager.Play("sound_transition");
            }

            if (biome == 2)
            {
                ReskinAllPlatforms(rfPalette);
                audioManager.stopAllAudio();
                Camera.main.backgroundColor = new Color(50f / 255f, 168f / 255f, 82f / 255f, 255f / 255f);
                CharacterController2D characterController2D = player.GetComponent<CharacterController2D>();
                Player playerData = player.GetComponent<Player>();
                characterController2D.queueNormalFeet = true;
                playerData.sunburnEnabled = false;
                playerData.waterCollection = true;
                audioManager.PlayAtPosition("music_rainforest_biome", leftOff);
                biomeHint.text = "Water! Get out of the shade to stay hydrated.";
                rainParticles.Play();
                biomeBackground.sprite = rainforestBG;
                audioManager.Play("sound_transition");
            }
        }

        foreach (AudioSource a in audioManager.GetComponents<AudioSource>())
        {
            if (a.isPlaying)
            {
                leftOff = a.time;
            }
        }
    }

    public void ReskinAllPlatforms(TileBase[] palette)
    {
        TilePaletteReskin[] reskins = platforms.GetComponentsInChildren<TilePaletteReskin>();
        foreach (TilePaletteReskin reskin in reskins)
            reskin.Reskin(palette);
    }
}
