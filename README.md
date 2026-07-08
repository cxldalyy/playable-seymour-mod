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

### Additional Improvements:
Optional, but recommended additions that improve quality of life:
- My [Requiem Fix](https://www.nexusmods.com/finalfantasyxx2hdremaster/mods/266) to fix Seymour's Overdrive's camera angling outside the Sinspawn Gui Battle.

### Final Notes:
- It's critical that this mod is played on an entirely new save file. Many things will break if you just try to load a preexisting save with this mod.
- Seymour comes with the ability to summon Anima once recruited immediately after the Sinspawn Gui battle (though Anima is not able to be summoned for this particular battle). To summon Anima, Seymour requires a full Overdrive. The choice is down to the player whether or not to Summon or use Requiem at a time.
- Requiem and Anima have both been adjusted, with Requiem being far more powerful, and Anima slightly less overpowered, given her joining the party extremely early on.
- Despite Seymour being able to summon Anima, Yuna cannot until the Baaj Temple sidequest is completed as is the case in vanilla. If the player doesn't complete it, Seymour remains the only character able to summon her. Once the sidequest is cleared, Yuna can also summon Anima as usual.
- This mod is still a work in progress. There are things that need adjusting, and other things that need reworking. The mod in its current state is playable, start to finish, with no game-breaking issues that I know of.

### Made possible thanks to:
* **[VBF Browser](https://www.nexusmods.com/finalfantasy12/mods/3):** Topher, ffgriever and Vaan
* **[Fahrenheit](https://github.com/fahrenheit-crew/fahrenheit):** The Fahrenheit Crew
* **[xDelta3](https://github.com/jmacd/xdelta-gpl):** Joshua MacDonald
