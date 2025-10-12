# FA25-Motive


## Getting Started

### 1. Clone the repo

git clone https://github.com/csu-hci-projects/FA25-Motive.git
cd FA25-Motive

### 2. Open in Unity Hub
- Open Unity Hub
- Make sure you use **Unity 6.2 (6000.2.2f1)**.

### 3. Unity settings
- Asset serialization: **Force Text** (already set in project settings).
- Meta files: Unity 6.x generates them automatically — do **not** delete `.meta` files.
- Do not commit `Library/`, `Temp/`, or `Builds/` (these are already ignored in `.gitignore`).

---

##  Third-Party Asset: Vintage Living Room Pack

We don't want to commit this commercial pack into GitHub.  
Each team member will need to import it locally:

1. In Unity: **Window → Package Manager**  
2. Switch to **My Assets**  
3. Find **Vintage Living Room 3D Game Pack**  
4. **Download** (if needed) → **Import**

If you don't see the asset pack in the **My Assets**:
1. go to this link and add it https://assetstore.unity.com/packages/3d/environments/vintage-living-room-3d-game-pack-314464
2. open in Unity
3. once the project is open follow the above steps. It should be in My Assets

If you see missing meshes or pink materials, make sure you’ve imported this pack.  
(URP users may need to run **Edit → Rendering → Materials → Convert to URP**.)

you may also have to mess with the shaders. Mine weren't compatable here is what I did that worked:
1. In the Project window, go to the pack’s Materials folder (e.g.
Assets/ZNS3D/Vintage Living Room Game Pack/Materials/).
2. In the search box above the Project window type: t:Material (to list only materials).
3. Ctrl+A to select them all.
4. In Inspector set Shader → Universal Render Pipeline/Lit (or Simple Lit).
(This instantly fixes many; they’ll turn from magenta to gray until textures are hooked up.)

---

## Git Workflow

- **avoid** commiting large generated files. `.gitignore` already excludes them.  
- **Do commit** your scenes, scripts, prefabs, and `.meta` files.  
- The third-party asset folder (`Assets/VintageLivingRoom3D/`) is ignored — teammates import it themselves.

### Daily workflow
```bash
git pull         # get the latest changes
# work in Unity
git add -A       # stage changes
git commit -m "Describe what you changed"
git push         # send to GitHub
```

### Conflict avoidance
- Prefer creating new scenes or nested prefabs rather than all editing the same scene.  
- If two people must touch the same scene, coordinate and merge carefully.

---

## For Grading

- Unity version: **6.2 (6000.2.2f1)**  
- Open the project in Unity Hub after cloning.  
- If you want to view the optional **Vintage Living Room 3D Game Pack**, you will need to import it from Unity’s Asset Store (we cannot redistribute it in the repo).  
- Without the pack, the project will still compile, but certain props/environments will appear missing.

---

