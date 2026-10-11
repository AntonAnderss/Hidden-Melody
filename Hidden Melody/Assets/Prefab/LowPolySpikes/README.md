# Low Poly Spikes for Unity

Five silver pyramid spikes on a charcoal plate, plus a single-spike version.

## Import

1. Unzip this download.
2. Drag the whole `LowPolySpikes` folder into your Unity project's `Assets` folder.
3. Wait for Unity to finish importing, then select **Tools > Low Poly Spikes > Create Prefabs**.
4. Drag `LowPolySpikes/Prefabs/SpikeTrap.prefab` into your scene.

The editor utility creates materials for URP, HDRP, or the built-in renderer,
and adds static MeshColliders. It does not replace existing prefabs or materials.
The models can also be imported directly without the utility.

## Models

| Model | Triangles | Width | Height | Depth |
|---|---:|---:|---:|---:|
| SpikeTrap.obj | 58 | 4.65 m | 1.25 m | 0.92 m |
| SingleSpike.obj | 34 | 1.05 m | 1.25 m | 0.92 m |

Y is up. The origin is at the center of the bottom of the base.
Flat normals and solid-color materials; no texture files are needed.
`Documentation/Preview.png` shows the actual exported geometry.

These are visual assets with physical colliders. Connect your own damage or
checkpoint respawn script separately. Use a separate trigger collider if your
existing script uses OnTriggerEnter. Keep these mesh colliders on static objects.

The mesh geometry, triangle counts, bounds, and closed surfaces were checked.
The prefab setup utility has not been run inside the Unity editor here.

Original asset created for your game. You may use, edit, and distribute it in
your game, including commercial projects.
