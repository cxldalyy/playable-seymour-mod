This mod features modded files to be used in conjunction with [Fahrenheit](https://github.com/fahrenheit-crew/fahrenheit)'s External File Loader (EFL) to improve Seymour's integration and stability in Final Fantasy X HD Remaster.

### You must own or obtain a legal copy of Final Fantasy X/X-2 HD Remaster.
### This mod does not contain any original SquareEnix assets, only .xdelta patches.

### Installation:
- For the sake of conserving disk space, extract only the following files from the `FFX_Data` folder:
`menu/abmap/dat00/D3D11/lines`
`menu/abmap/dat12/D3D11/lines`
`menu/D3D11/face_ply`
`menu_us/D3D11/icon`
- Extract your original, unmodified `ffx_ps2` folder from `FFX_Data.vbf` using the [VBF Browser](https://www.nexusmods.com/finalfantasy12/mods/3).
- Place the extracted `FFX_Data` and `ffx_ps2` folders into the `/original` folder within this release.
- Run "apply_mods.bat".
- This will convert the .xdelta patches in `/patches` into the modded files ready for EFL use.
- Copy the new modded files in `efl/x` into your Fahrenheit EFL.
- Done! Now launch Fahrenheit.
- Once installation is completed, you may remove/delete the `FFX_Data` and `ffx_ps2` folders from the `/original` folder to conserve disk space.

### Made possible thanks to:
* **xDelta3:** [Joshua MacDonald](https://github.com/jmacd/xdelta-gpl).
* **Fahrenheit:** [Fahrenheit-Crew](https://github.com/fahrenheit-crew/fahrenheit).
* **[VBF Browser](https://www.nexusmods.com/finalfantasy12/mods/3):** Topher, ffgriever and Vaan.
