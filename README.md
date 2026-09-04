This mod features modded files to be used in conjunction with [Fahrenheit](https://github.com/fahrenheit-crew/fahrenheit)'s External File Loader (EFL) to improve Seymour's integration and stability in the Final Fantasy X HD Remaster.

### You must own or obtain a legal copy of Final Fantasy X/X-2 HD Remaster.

### This mod must be played for the first time on a brand new save file. Loading a pre-existing save will break countless things, and Seymour may not be playable on that file.

### Prerequisites:
This mod entirely depends on the two following works:
- [Fahrenheit](https://github.com/fahrenheit-crew/fahrenheit) v1.0.0-alpha11
- [custom-character](https://github.com/Rurusachi/custom-character)

### Installation:
For the sake of conserving disk space, extract only the following files from the `FFX_Data` folder:
```
menu/abmap/dat00/D3D11/lines
menu/abmap/dat12/D3D11/lines
menu/D3D11/face_ply
menu_us/D3D11/icon
```
- Extract your original, unmodified `ffx_ps2` folder from `FFX_Data.vbf` using the [VBF Browser](https://www.nexusmods.com/finalfantasy12/mods/3).
- Place the extracted `FFX_Data` and `ffx_ps2` folders into the `\original` folder within this release.
- Run "apply_mods.bat".
- This will convert the .xdelta patches in `\patches` into the modded files ready for EFL use.
- Copy the new modded files in `efl\x` into the `mods\playable-seymour\efl\x` folder inside the release. You can now also delete the whole `efl files` folder.
- Finally, copy the entire `mods` folder from the release into your Fahrenheit folder in the game directory.
- Done! Now launch the game through Fahrenheit.
- Enjoy the story of Spira with Seymour permanently by your side!
(Once the installation is completed, you can also delete the `FFX_Data` and `ffx_ps2` folders from the `\original` folder to conserve disk space.)

### Features
This mod makes Seymour his own, standalone, 8th party member. 
- Access to the Sphere Grid (though he doesn't *currently* have his own path)
- His own Weapons and Armors, called "Scepters" and "Circlets" respectively, from Shops and enemy drops
- A new Overdrive alongside Requiem
- His own Celestial items
- New animations
- ...& more!
Naturally, Seymour is capable of wielding both black and white magic, following Lulu's path on the Sphere Grid by default. With this in mind, his base stats are slightly adjusted compared to the vanilla game, so he and Lulu can both be powerful black mages respectively and not *completely* overpower one or another.

### Summoning
Seymour comes with a second Overdrive alongside Requiem - the ability to summon Anima (and only Anima) immediately after he's recruited after the Sinspawn Gui battle (though she's not able to be summoned for that particular battle).
- Requiem and Anima have both been rebalanced, with Requiem now being far more powerful, and Anima's stats being slightly less overpowered early on (given that she joins the party much earlier), making it a genuine strategic decision to choose between the two for an Overdrive.
- Anima now scales solely with Seymour's stats instead of Yuna's. Be sure to level him on the Sphere Grid if you want Anima to grow in strength. Yuna and the other Aeons remain untouched, and scale as usual according to her stats.
- Despite Seymour being able to summon Anima as soon as he is recruited, Yuna retains vanilla behaviour and cannot summon her until the Baaj Temple sidequest is completed. If the player doesn't complete it, Seymour remains the only character able to summon her. Once the sidequest is cleared, both Yuna and Seymour can summon Anima as usual, but her stats still only scale off of Seymour's.

### Additional Improvements:
Optional, but recommended additions that improve quality of life:
- My [Requiem Fix](https://www.nexusmods.com/finalfantasyxx2hdremaster/mods/266) to fix Seymour's Overdrive Requiem's camera angling in battles outside of the Sinspawn Gui encounter.

### Final Notes:
The mod is still a work in progress. There are still things to be addressed, like equipment sorting and abilities that cause softlocks. The mod in its current state is entirely playable, start to finish. Of course, if you find any bugs, please raise an issue here.

### Made possible thanks to:
- **[VBF Browser](https://www.nexusmods.com/finalfantasy12/mods/3):** Topher, ffgriever and Vaan
- **[Fahrenheit](https://github.com/fahrenheit-crew/fahrenheit):** The Fahrenheit Crew
- **[custom-character](https://github.com/Rurusachi/custom-character):** Rurusachi, for the Sphere Grid work included in this mod to feature Seymour on the grid.
- **[xDelta3](https://github.com/jmacd/xdelta-gpl):** Joshua MacDonald

This mod would not have been possible without all of these incredible people listed above and their hard work. All credit goes to them.
